using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CompetenceDrawer : MonoBehaviour
{
	[SerializeField] private TMP_InputField _value = null;
	[SerializeField] private TMP_InputField _title = null;
	[SerializeField] private SpecialtyDrawer _specialtyDrawer = null;
	[SerializeField] private Button _button = null;
	[SerializeField] private List<Image> _imagesSelection = new List<Image>();
	[SerializeField] private Color _neutralColor = Color.white;
	[SerializeField] private Color _selectColor = Color.red;

	private Competence _competence = null;
	private bool _select = false;
	private CompetencesDrawer _competencesDrawer = null;

	public void Reset(CompetencesDrawer competencesDrawer)
	{
		_value.enabled = false;
		_title.enabled = false;
		_title.onSubmit.RemoveListener(OnSubmitTitleEvent);
		_title.onSubmit.AddListener(OnSubmitTitleEvent);
		_value.onSubmit.RemoveListener(OnSubmitValueEvent);
		_value.onSubmit.AddListener(OnSubmitValueEvent);
		_specialtyDrawer.Reset();
		_select = false;
		_button.onClick.AddListener(OnButtonSelect);
		_button.enabled = true;
		_competencesDrawer = competencesDrawer;
	}

	public void Init(Competence competence)
	{
		_competence = competence;

		_value.text = ComputeTextFromValue(_competence.Value);
		_title.text = ComputeTextFromTitle(_competence.Title);
	}

	private string ComputeTextFromValue(int value)
	{
		string text = "";
		if (value > 1)
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
}
