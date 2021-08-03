using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct DiceRollerComponentData : IComponentData
{
	public Random Random;
	public RollDiceResult RollDiceResult;

	public void Init(Entity entity, float timeSinceStartUp)
	{
		Random = new Random((uint)((entity.Index+1+ timeSinceStartUp)));
	}

	public void RollDices(in RollDiceConfig rollDiceConfig, ref DynamicBuffer<DiceResultBufferElementData> statefulCollisionEventBuffer)
	{
		statefulCollisionEventBuffer.Clear(); 
		for (int i = 0; i < rollDiceConfig.NbLaunchDice; i++)
		{
			int diceResult = Dice.Roll(ref Random);
			DiceResultBufferElementData diceResultBufferElementData = new DiceResultBufferElementData();
			diceResultBufferElementData.Result = diceResult;
			statefulCollisionEventBuffer.Add(diceResultBufferElementData); 
		}

		for (int i = 0; i < rollDiceConfig.NbLaunchDice; i++)
		{
			for (int j = 0; j < rollDiceConfig.NbLaunchDice; j++)
			{
				DiceResultBufferElementData curResult = statefulCollisionEventBuffer[i];
				DiceResultBufferElementData nextResult = statefulCollisionEventBuffer[j];
				if (curResult.Result > nextResult.Result)
				{
					DiceResultBufferElementData tmp = nextResult;
					statefulCollisionEventBuffer[j] = curResult;
					statefulCollisionEventBuffer[i] = nextResult;
				}
			}
		}

		int finalResult = rollDiceConfig.Bonus;

		for (int i = 0; i < rollDiceConfig.NbKeepDice; i++)
		{
			finalResult += statefulCollisionEventBuffer[i].Result;
		}
		RollDiceResult.RollDiceValue = finalResult;
	}

}
