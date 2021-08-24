
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

class DiceRollerCharacterDrawer : MonoBehaviour
{
	[SerializeField] private TraitDrawer _nbBonusPointsDrawer = null;
	[SerializeField] private TraitDrawer _nbVoidPointsDrawer = null;
	[SerializeField] private StatisticsConfigDrawer _statisticsConfigDrawer = null;
	[SerializeField] private Button _launchButton = null;

	private CompetenceDrawer _selectedCompetenceDrawer = null;
	private ISelected _selectedTraitDrawer = null;
	private Trait _nbVoidPoints = new Trait("PTS DE VIDE", 0);
	private Trait _nbNonusPoints = new Trait("PTS BONUS", 0);

	public void Reset()
	{
		UpdateRollDicesConfig();
		_nbVoidPointsDrawer.Reset(null);
		_nbVoidPointsDrawer.Init(_nbNonusPoints);
		_nbBonusPointsDrawer.Reset(null);
		_nbBonusPointsDrawer.Init(_nbVoidPoints);
	}

	public void SetSelectedCompetenceDrawer(CompetenceDrawer competenceDrawer)
	{
		_selectedCompetenceDrawer = competenceDrawer;
		UpdateRollDicesConfig();
	}

	public void SetSelectedTraitDrawer(ISelected traitDrawer)
	{
		_selectedTraitDrawer = traitDrawer;
		UpdateRollDicesConfig();
	}

	private void UpdateRollDicesConfig()
	{
		RollDicesConfig rollDicesConfig = ComputeRollDicesConfig();
		_statisticsConfigDrawer.Fill(rollDicesConfig);
	}

	public void OnUpdateValue()
	{
		UpdateRollDicesConfig();
	}

	public RollDicesConfig ComputeRollDicesConfig()
	{
		RollDicesConfig rollDicesConfig = new RollDicesConfig();
		int nbKeepDices = _nbVoidPoints.Value;
		if (_selectedTraitDrawer != null)
		{
			nbKeepDices += _selectedTraitDrawer.Value;
		}

		int nbLaunchDices = nbKeepDices;
		int bonus = _nbNonusPoints.Value;
		int nbFreeAugmenations = 0;
		if (_selectedCompetenceDrawer != null)
		{
			nbLaunchDices += _selectedCompetenceDrawer.Value;
			bonus += _selectedCompetenceDrawer.BonusValue;
			nbFreeAugmenations += _selectedCompetenceDrawer.NbFreeAugmentations;
		}

		if (nbLaunchDices > 10)
		{
			int nbLaunchDicesAbove10 = nbLaunchDices - 10;
			int nbKeepDicesToAdd = nbLaunchDicesAbove10 / 2;
			nbKeepDices += nbKeepDicesToAdd;
			nbLaunchDices -= (nbKeepDicesToAdd * 2);
		}

		if (nbKeepDices > 10)
		{
			nbFreeAugmenations += (nbKeepDices - 10);
			nbKeepDices = 10;
		}

		rollDicesConfig.NbKeepDice = nbKeepDices;
		rollDicesConfig.NbLaunchDice = nbLaunchDices;
		rollDicesConfig.Bonus = bonus;
		rollDicesConfig.NbFreeAugmentations = nbFreeAugmenations;

		return rollDicesConfig;
	}

	public void Update()
	{
		UpdateRollDicesConfig();
	}
}
