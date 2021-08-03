using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsResultColumnDrawer : MonoBehaviour
{
	[SerializeField] private List<StatisticsResultDrawer> _statisticsResultDrawers = new List<StatisticsResultDrawer>();

	public List<StatisticsResultDrawer> StatisticsResultDrawers { get { return _statisticsResultDrawers; } }

	// Start is called before the first frame update
	public void Reset()
	{
		foreach (StatisticsResultDrawer statisticsResultDrawer in _statisticsResultDrawers)
		{
			statisticsResultDrawer.Reset();
		}
	}
}
