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
	[SerializeField] private Button _buttonCancel;
	[SerializeField] private ButtonEdit _buttonEdit;
	[SerializeField] private Button _buttonLaunch;

	[Header("Edit Mode")]
	[SerializeField] List<Image> _editImages = new List<Image>();
	[SerializeField] List<GameObject> _editInactivate = new List<GameObject>();
	[SerializeField] private Color _normalColor = Color.white;
	[SerializeField] private Color _editColor = Color.green;

	private Character _character = null;
	private BackElement _backElement = new BackElement();
	private bool _edit = false;

	private void Start()
	{
		Reset();
		_buttonCancel.onClick.AddListener(Cancel);
		_buttonCancel.gameObject.SetActive(false);
		_backElement.ActionBackHandler += OnBackButton;
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
		Cancel();
	}

	private void Cancel()
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


		Edit(false);
		_buttonEdit.CancelEdit();
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
		Edit(false);

	}

	private void Edit()
	{
		Edit(true);
	}

	private void Edit(bool edit)
	{
		if(_edit != edit)
		{
			_edit = edit;
			_characteristicsDrawer.Edit(edit);
			_competencesDrawer.Edit(edit);
			_buttonCancel.gameObject.SetActive(edit);

			Color newColor = _normalColor;
			if (edit)
			{
				newColor = _editColor;
			}

			foreach (Image image in _editImages)
			{
				image.color = newColor;
			}

			foreach (GameObject gameObject in _editInactivate)
			{
				gameObject.SetActive(!edit);
			}

			if (edit)
			{
				BackContext.AddBackElement(_backElement);
			}
			else
			{
				BackContext.RemoveBackElement(_backElement);
			}
		}
	}

	public void SetSelectedCompetenceDrawer(CompetenceDrawer competenceDrawer)
	{
		_diceRollerCharacterDrawer.SetSelectedCompetenceDrawer(competenceDrawer);
	}

	public void SetSelectedTraitDrawer(ISelected traitDrawer)
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

	public void OnBackButton()
	{
		Cancel();
	}
}
