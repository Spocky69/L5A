using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPage : MonoBehaviour
{
	[SerializeField] private CharacteristicsDrawer _characteristicsDrawer = null;
	[SerializeField] private CompetencesDrawer _competencesDrawer = null;
	[SerializeField] private DiceRollerCharacterDrawer _diceRollerCharacterDrawer = null;
	[SerializeField] private StatisticsResultsPanel _statisticsResultsPanel = null;

	[Header("Buttons")]
	[SerializeField] private Button _buttonLoad;
	[SerializeField] private ButtonEdit _buttonEdit;
	[SerializeField] private Button _buttonLaunch;

	private Character _character = null;

	private void Start()
	{
		Reset();
		_buttonLoad.onClick.AddListener(Load);
	}

	private void Reset()
	{
		_characteristicsDrawer.Reset(this);
		_competencesDrawer.Reset(this);
		_buttonEdit.Reset();
		_buttonEdit.SaveEventHandler += Save;
		_buttonEdit.EditEventHandler += Edit;
		_buttonLaunch.onClick.RemoveListener(OnButtonLaunch);
		_buttonLaunch.onClick.AddListener(OnButtonLaunch);
		_diceRollerCharacterDrawer.Reset();
		_statisticsResultsPanel.Reset();
		Load();
	}

	private void Load()
	{
		string filaPath = Character.ComputeFilePath();
		if (File.Exists(filaPath))
		{
			string data = File.ReadAllText(filaPath);
			if (string.IsNullOrEmpty(data) == false)
			{
				_character = JsonUtility.FromJson<Character>(data);
			}
		}

		if (_character == null)
		{
			_character = new Character();
		}
		_character.InitCharacter(_competencesDrawer.NbCompencesDrawer);

		_characteristicsDrawer.Init(_character);
		_competencesDrawer.Init(_character);
	}

	public void OnUpdateValue()
	{
		_diceRollerCharacterDrawer.OnUpdateValue();
	}

	private void Save()
	{
		string data = JsonUtility.ToJson(_character);
		string filePath = Character.ComputeFilePath();
		File.WriteAllText(filePath, data);
		_characteristicsDrawer.Edit(false);
		_competencesDrawer.Edit(false);
	}

	private void Edit()
	{
		_characteristicsDrawer.Edit(true);
		_competencesDrawer.Edit(true);
	}

	public void SetSelectedCompetenceDrawer(CompetenceDrawer competenceDrawer)
	{
		_diceRollerCharacterDrawer.SetSelectedCompetenceDrawer(competenceDrawer);
	}

	public void SetSelectedTraitDrawer(TraitDrawer traitDrawer)
	{
		_diceRollerCharacterDrawer.SetSelectedTraitDrawer(traitDrawer);
	}

	public async void OnButtonLaunch()
	{
		_buttonLaunch.interactable = false;
		RollDicesConfig rollDicesConfig = _diceRollerCharacterDrawer.ComputeRollDicesConfig();
		await RollDicesSystem.Launch(rollDicesConfig);
		_statisticsResultsPanel.FillValueFromResult();
		_buttonLaunch.interactable = true;

	}
}
