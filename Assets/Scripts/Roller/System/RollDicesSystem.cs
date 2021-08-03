using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

class RollDicesSystem : SystemBase
{
	private const int NB_ROLL_DICE = 1000000;

	public enum States
	{
		Waiting,
		Launching,
		Launched
	}

	private States _launchingState = States.Waiting;
	private RollDiceConfig _rollDiceConfig = new RollDiceConfig();
	private NativeArray<StatisticsRollerResult> _nativeArrayStatisticsRollerResult;

	public States LaunchingState { get { return _launchingState; } }
	public NativeArray<StatisticsRollerResult> NativeArrayStatisticsRollerResult { get { return _nativeArrayStatisticsRollerResult; } }
	

	public void LaunchRollDices(RollDiceConfig rollDiceConfig)
	{
		if (_launchingState == States.Waiting)
		{
			_launchingState = States.Launching;
			_rollDiceConfig = rollDiceConfig;
		}
	}

	protected override void OnCreate()
	{
		base.OnCreate();
		CreateEntities();
		_nativeArrayStatisticsRollerResult = new NativeArray<StatisticsRollerResult>(50, Allocator.Persistent);
		for (int i = 0; i < _nativeArrayStatisticsRollerResult.Length; i++)
		{
			StatisticsRollerResult statisticsRollerResult = _nativeArrayStatisticsRollerResult[i];
			statisticsRollerResult.RollValue = 5 * (i + 1);
			_nativeArrayStatisticsRollerResult[i] = statisticsRollerResult;
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_nativeArrayStatisticsRollerResult.Dispose();
	}

	protected override void OnUpdate()
	{
		RollDiceConfig rollDiceConfig = _rollDiceConfig;

		float timeSinceStartUp = (uint)(UnityEngine.Time.realtimeSinceStartup);

		switch (_launchingState)
		{
			case States.Waiting: return;
			case States.Launching:
				{
					Entities.ForEach((Entity entity, ref DiceRollerComponentData raycastComponentData,
									ref DynamicBuffer<DiceResultBufferElementData> statefulCollisionEventBuffer) =>
					{
						raycastComponentData.Init(entity, timeSinceStartUp);
						raycastComponentData.RollDices(rollDiceConfig, ref statefulCollisionEventBuffer);
					}).WithName("RollDicesSystemLaunchDices").ScheduleParallel();
					
					_launchingState = States.Launched;
				}
				break;
			case States.Launched:
				{
					if (Dependency.IsCompleted)
					{
						for (int i = 0; i < _nativeArrayStatisticsRollerResult.Length; i++)
						{
							StatisticsRollerResult statisticsRollerResult = _nativeArrayStatisticsRollerResult[i];
							statisticsRollerResult.Clear();
							_nativeArrayStatisticsRollerResult[i] = statisticsRollerResult;
						}

						Entities.ForEach((in DiceRollerComponentData raycastComponentData) =>
						{
							for (int i = 0; i < _nativeArrayStatisticsRollerResult.Length; i++)
							{
								if (_nativeArrayStatisticsRollerResult[i].RollValue <= raycastComponentData.RollDiceResult.RollDiceValue)
								{
									StatisticsRollerResult statisticsRollerResult = _nativeArrayStatisticsRollerResult[i];
									statisticsRollerResult.AddGoodValue();
									_nativeArrayStatisticsRollerResult[i] = statisticsRollerResult;
								}
								else
								{
									break;
								}
							}
						}).WithoutBurst().WithName("RollDicesSystemStatisticsRollerResult").Run();

						for (int i = 0; i < _nativeArrayStatisticsRollerResult.Length; i++)
						{
							StatisticsRollerResult statisticsRollerResult = _nativeArrayStatisticsRollerResult[i];
							statisticsRollerResult.FillPercent(NB_ROLL_DICE);
							_nativeArrayStatisticsRollerResult[i] = statisticsRollerResult;

						}

						_launchingState = States.Waiting;
					}
				}
				break;
		}
	}

	private void CreateEntities()
	{
		EntityArchetype entityArchetype = EntityManager.CreateArchetype(typeof(DiceRollerComponentData), typeof(DiceResultBufferElementData));
		EntityManager.CreateEntity(entityArchetype, NB_ROLL_DICE);
	}
}
