
using TMPro;
using UnityEngine;

public class StatisticsConfigDrawer : MonoBehaviour
{
	[SerializeField] private TMP_Text _nbLaunchDices = null;
	[SerializeField] private TMP_Text _g = null;
	[SerializeField] private TMP_Text _nbKeepDices = null;
	[SerializeField] private TMP_Text _plus = null;
	[SerializeField] private TMP_Text _bonus = null;
	[SerializeField] private TMP_Text _nbAugmentation = null;

	public void Reset()
	{
		_g.gameObject.SetActive(false);
		_plus.gameObject.SetActive(false);
		_nbLaunchDices.gameObject.SetActive(false);
		_nbLaunchDices.text = "0";
		_nbKeepDices.gameObject.SetActive(false);
		_nbKeepDices.text = "0";
		_bonus.gameObject.SetActive(false);
		_bonus.text = "0";

		if (_nbAugmentation != null)
		{
			_nbAugmentation.gameObject.SetActive(false);
			_nbAugmentation.text = "0";
		}
	}

	public void Fill(RollDicesConfig rollDiceConfig)
	{
		_g.gameObject.SetActive(true);
		_plus.gameObject.SetActive(true);
		_nbLaunchDices.gameObject.SetActive(true);
		_nbLaunchDices.text = rollDiceConfig.NbLaunchDice.ToString();
		_nbKeepDices.gameObject.SetActive(true);
		_nbKeepDices.text = rollDiceConfig.NbKeepDice.ToString();
		_bonus.gameObject.SetActive(true);
		_bonus.text = rollDiceConfig.Bonus.ToString();
		if (_nbAugmentation != null)
		{
			_nbAugmentation.gameObject.SetActive(true);
			_nbAugmentation.text = rollDiceConfig.NbFreeAugmentations.ToString();
		}
	}


}