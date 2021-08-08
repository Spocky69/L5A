
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

class DiceRollerCharacterDrawer : MonoBehaviour
{
	[SerializeField] private TraitDrawer _nbVoidPointDrawer = null;
	[SerializeField] private StatisticsConfigDrawer _statisticsConfigDrawer = null;
	[SerializeField] private Button _launchButton = null;

	private CompetenceDrawer _selectedCompetenceDrawer = null;
	private TraitDrawer _selectedTraitDrawer = null;
	private Trait _nbVoidPoints = new Trait("PTS DE VIDE", 0);

	public void Reset()
	{
		UpdateRollDicesConfig();
		_nbVoidPointDrawer.Reset(null);
		_nbVoidPointDrawer.Init(_nbVoidPoints);
	}

	public void SetSelectedCompetenceDrawer(CompetenceDrawer competenceDrawer)
	{
		_selectedCompetenceDrawer = competenceDrawer;
		UpdateRollDicesConfig();
	}

	public void SetSelectedTraitDrawer(TraitDrawer traitDrawer)
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
		int bonus = 0;
		int nbFreeAugmenations = 0;
		if (_selectedCompetenceDrawer != null)
		{
			nbLaunchDices += _selectedCompetenceDrawer.Value;
			bonus += _selectedCompetenceDrawer.BonusValue;
			nbFreeAugmenations += _selectedCompetenceDrawer.NbFreeAugmentations;
		}

		if (nbKeepDices > 10)
		{
			bonus += (nbKeepDices - 10) * 5;
			nbKeepDices = 10;
		}

		if (nbLaunchDices > 10)
		{
			int nbLaunchDicesAbove10 = nbLaunchDices - 10;
			int nbKeepDicesToAdd = nbLaunchDicesAbove10 / 2;

			nbKeepDices += nbKeepDicesToAdd;
			nbLaunchDices -= (nbKeepDicesToAdd * 2);
			nbFreeAugmenations = math.max(0, nbKeepDices - 10);
			nbKeepDices -= nbFreeAugmenations;
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
