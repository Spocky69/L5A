using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CompetenceDrawer : MonoBehaviour, ISelected
{
	[SerializeField] private TMP_InputField _value = null;
	[SerializeField] private TMP_InputField _title = null;
	[SerializeField] private SpecialtyDrawer _specialtyDrawer = null;
	[SerializeField] private Button _button = null;
	[SerializeField] private List<Image> _imagesSelection = new List<Image>();
	[SerializeField] private Color _neutralColor = Color.white;
	[SerializeField] private Color _selectColor = Color.red;

	private ICompetence _competence = null;
	private bool _select = false;
	private CompetencesDrawer _competencesDrawer = null;

	public int Value { get { return _competence.Value; } }
	public int BonusValue { get { return _competence.BonusValue; } }
	public int NbFreeAugmentations { get { return _competence.NbFreeAugmentations; } }

	public void Reset(CompetencesDrawer competencesDrawer)
	{
		_title.enabled = false;
		_title.onEndEdit.RemoveListener(OnSubmitTitleEvent);
		_title.onEndEdit.AddListener(OnSubmitTitleEvent);
		_value.enabled = false;
		_value.onEndEdit.RemoveListener(OnSubmitValueEvent);
		_value.onEndEdit.AddListener(OnSubmitValueEvent);
		if (_specialtyDrawer != null)
		{
			_specialtyDrawer.Reset(this);
		}
		_select = false;
		_button.onClick.AddListener(OnButtonSelect);
		_button.enabled = true;
		_competencesDrawer = competencesDrawer;
		SetSelected(false);
	}

	public void Init(ICompetence competence)
	{
		_competence = competence;
		_value.text = ComputeTextFromValue(_competence.Value);
		_title.text = ComputeTextFromTitle(_competence.Title);
		if (_specialtyDrawer != null)
		{
			_specialtyDrawer.Init(competence);
		}
	}

	private string ComputeTextFromValue(int value)
	{
		string text = "";
		if (value >= 0)
		{
			text = value.ToString();
		}
		else
		{
			text = "-";
		}
		return text;
	}

	private string ComputeTextFromTitle(string title)
	{
		string textToDraw = title;
		if (string.IsNullOrEmpty(title))
		{
			textToDraw = " ------- ";
		}
		else
		{
			textToDraw = title;
		}
		return textToDraw;
	}

	private void OnSubmitValueEvent(string newValue)
	{
		int newValueInt = 0;
		if (int.TryParse(newValue, out newValueInt))
		{
			newValueInt = math.clamp(newValueInt, 0, 10);
		}
		_competence.Value = newValueInt;
		_value.SetTextWithoutNotify(ComputeTextFromValue(_competence.Value));
	}

	private void OnSubmitTitleEvent(string newValue)
	{
		_competence.Title = ComputeTextFromTitle(newValue);
	}

	public void Edit(bool editValue)
	{
		_value.enabled = editValue;
		_title.enabled = editValue;
		_competencesDrawer.SetSelected(null);
	}

	public void SetSelected(bool select)
	{
		_select = select;
		Color color = _neutralColor;
		if (_select)
		{
			color = _selectColor;
		}

		foreach (Image image in _imagesSelection)
		{
			image.color = color;
		}
	}

	public void OnButtonSelect()
	{
		if (_select == false)
		{
			_competencesDrawer.SetSelected(this);
		}
		else
		{
			_competencesDrawer.SetSelected(null);
		}
	}

	public void OnUpdateValue()
	{
		_competencesDrawer.OnUpdateValue();
	}
}
