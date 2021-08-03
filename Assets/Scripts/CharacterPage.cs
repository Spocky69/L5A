using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPage : MonoBehaviour
{
	[SerializeField] private CharacteristicsDrawer _characteristicsDrawer;
	[SerializeField] private CompetencesDrawer _competencesDrawer;

	[Header("Buttons")]
	[SerializeField] private Button _buttonLoad;
	[SerializeField] private ButtonEdit _buttonEdit;

	private Character _character = null;

	private void Start()
	{
		Reset();
		_buttonLoad.onClick.AddListener(Load);
	}

	private void Reset()
	{
		_characteristicsDrawer.Reset();
		_competencesDrawer.Reset();
		_buttonEdit.Reset();
		_buttonEdit.SaveEventHandler += Save;
		_buttonEdit.EditEventHandler += Edit;
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
}
