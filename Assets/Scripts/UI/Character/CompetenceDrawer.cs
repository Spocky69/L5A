using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class CompetenceDrawer : MonoBehaviour
{
	[SerializeField] private TMP_InputField _value = null;
	[SerializeField] private TMP_InputField _title = null;
	[SerializeField] private SpecialtyDrawer _specialtyDrawer = null;

	private Competence _competence = null;

	public void Reset()
	{
		_value.enabled = false;
		_title.enabled = false;
		_title.onSubmit.RemoveListener(OnSubmitTitleEvent);
		_title.onSubmit.AddListener(OnSubmitTitleEvent);
		_value.onSubmit.RemoveListener(OnSubmitValueEvent);
		_value.onSubmit.AddListener(OnSubmitValueEvent);
		_specialtyDrawer.Reset();
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
}
