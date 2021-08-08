using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsResultsPanel : MonoBehaviour
{
	[SerializeField] private bool _activateByDefault = true;
	[SerializeField] private Button _button = null;
	[SerializeField] private List<StatisticsResultColumnDrawer> _statisticsResultDrawers = new List<StatisticsResultColumnDrawer>();

	public void Reset()
	{
		foreach (StatisticsResultColumnDrawer statisticsResultCollumnDrawer in _statisticsResultDrawers)
		{
			statisticsResultCollumnDrawer.Reset();
		}
		gameObject.SetActive(_activateByDefault);

		if (_button != null)
		{
			_button.onClick.RemoveListener(OnButtonClose);
			_button.onClick.AddListener(OnButtonClose);
		}
	}

	public void FillValueFromResult()
	{
		gameObject.SetActive(true);
		RollDicesSystem rollDicesSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<RollDicesSystem>();
		NativeArray<StatisticsRollerResult> statisticsRollerResults = rollDicesSystem.NativeArrayStatisticsRollerResult;
		int statisticsRollerResultsIndex = 0;
		int nbResultsDrawing = _statisticsResultDrawers.Count * _statisticsResultDrawers[0].StatisticsResultDrawers.Count;
		int maxBeginIndex = math.max(0, statisticsRollerResults.Length - nbResultsDrawing);

		for (int i = 0; i <= maxBeginIndex; i++)
		{
			if (StatisticsResultDrawer.IsAutoSuccess(statisticsRollerResults[i].PercentValue))
			{
				statisticsRollerResultsIndex = i;
			}
			else
			{
				break;
			}
		}

		foreach (StatisticsResultColumnDrawer statisticsResultColumnDrawer in _statisticsResultDrawers)
		{
			foreach (StatisticsResultDrawer statisticsResultDrawer in statisticsResultColumnDrawer.StatisticsResultDrawers)
			{
				if (statisticsRollerResultsIndex < statisticsRollerResults.Length)
				{
					StatisticsRollerResult statisticsRollerResult = statisticsRollerResults[statisticsRollerResultsIndex];
					statisticsResultDrawer.Fill((statisticsRollerResultsIndex + 1) * 5, statisticsRollerResult.PercentValue);
					statisticsRollerResultsIndex++;
				}
				else
				{
					statisticsResultDrawer.Fill(0, 0);
				}
			}
		}
	}

	public void OnButtonClose()
	{
		gameObject.SetActive(false);
	}
}
