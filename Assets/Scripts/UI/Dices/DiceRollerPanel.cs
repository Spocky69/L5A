using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollerPanel : MonoBehaviour
{
	

	[SerializeField] private ValueChooserDrawer _launchDicesChooser = null;
	[SerializeField] private ValueChooserDrawer _keepDicesChooser = null;
	[SerializeField] private ValueChooserDrawer _bonusChooser = null;

	public void Reset()
	{
		_launchDicesChooser.DownHandler += OnLaunchDiceDown;
		_launchDicesChooser.UpHandler += OnLaunchDiceUp;
		_keepDicesChooser.DownHandler += OnKeepDiceDown;
		_keepDicesChooser.UpHandler += OnKeepDiceUp;

		_launchDicesChooser.Reset(0, 11);
		_keepDicesChooser.Reset(0, 10);
		_bonusChooser.Reset(int.MinValue, int.MaxValue);
	}

	public RollDicesConfig CreateRollConfig()
	{
		RollDicesConfig rollDiceConfig = new RollDicesConfig();
		rollDiceConfig.NbLaunchDice = _launchDicesChooser.CurValue;
		rollDiceConfig.NbKeepDice = _keepDicesChooser.CurValue;
		rollDiceConfig.Bonus = _bonusChooser.CurValue;
		return rollDiceConfig;
	}

	private void OnLaunchDiceDown()
	{
		_keepDicesChooser.SetValue(math.min(_launchDicesChooser.CurValue, _keepDicesChooser.CurValue));
	}

	private void OnLaunchDiceUp()
	{

	}

	private void OnKeepDiceDown()
	{

	}

	private void OnKeepDiceUp()
	{
		_launchDicesChooser.SetValue(math.max(_launchDicesChooser.CurValue, _keepDicesChooser.CurValue));
	}
}
