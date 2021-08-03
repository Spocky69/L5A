using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDicesPage : MonoBehaviour
{
	[SerializeField] private DiceRollerPanel _diceRoller;
	[SerializeField] private StatisticsResultsPanel _results;
	[SerializeField] private Button _buttonLaunch = null;
	[SerializeField] private Button _buttonReset = null;
	[SerializeField] private StatisticsConfigDrawer _statisticsConfigDrawer = null;
	

	// Start is called before the first frame update
	private void Start()
	{
		Reset();
	}

	private void Reset()
	{
		_buttonLaunch.onClick.AddListener(OnButtonLaunch);
		_buttonReset.onClick.AddListener(OnButtonReset);
		_diceRoller.Reset();
		_results.Reset();
		_statisticsConfigDrawer.Reset();
	}

	public async void OnButtonLaunch()
	{
		_buttonLaunch.interactable = false;
		RollDiceConfig rollDiceConfig = _diceRoller.CreateRollConfig();
		await _diceRoller.Launch(rollDiceConfig);
		_results.FillValueFromResult();
		_buttonLaunch.interactable = true;
		_statisticsConfigDrawer.Fill(rollDiceConfig);
	}

	public void OnButtonReset()
	{
		Reset();
	}
}
