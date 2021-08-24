using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class ButtonEdit : MonoBehaviour
{
	private enum States
	{
		Edit,
		Sauvegarder
	}

	[SerializeField] private Button _button;
	[SerializeField] private TMP_Text _text;
	[SerializeField] private string _editString = "EDITER";
	[SerializeField] private string _saveString = "SAUVEGARDER";

	private States _state = States.Edit;

	private Action _saveEventHandler;
	public event Action SaveEventHandler
	{
		add
		{
			_saveEventHandler -= value;
			_saveEventHandler += value;
		}

		remove
		{
			_saveEventHandler -= value;
		}
	}

	private Action _editEventHandler;
	public event Action EditEventHandler
	{
		add
		{
			_editEventHandler -= value;
			_editEventHandler += value;
		}

		remove
		{
			_editEventHandler -= value;
		}
	}

	public void Reset()
	{
		_text.text = _editString;
		_text.fontSize = 37.5f;
		_state = States.Edit;
		_button.onClick.RemoveListener(OnButtonPush);
		_button.onClick.AddListener(OnButtonPush);
	}


	private void OnButtonPush()
	{
		switch (_state)
		{
			case States.Sauvegarder:
				{
					_text.text = _editString;
					_text.fontSize = 37.5f;
					_saveEventHandler();
					_state = States.Edit;
				}
				break;

			case States.Edit:
				{
					_text.text = _saveString;
					_text.fontSize = 25.0f;
					_editEventHandler();
					_state = States.Sauvegarder;
				}
				break;
		}
	}

	public void CancelEdit()
	{
		_text.text = _editString;
		_text.fontSize = 37.5f;
		_state = States.Edit;
	}
}
