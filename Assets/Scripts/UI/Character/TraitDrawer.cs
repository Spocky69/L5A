using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class TraitDrawer : MonoBehaviour
{
	[SerializeField] private TMP_Text _title = null;
	[SerializeField] private TMP_InputField _inputField = null;

	private Trait _trait;

	public void Reset()
	{
		_inputField.onValueChanged.RemoveListener(OnValueChangeText);
		_inputField.onValueChanged.AddListener(OnValueChangeText);
		_inputField.enabled = false;
	}

	private void OnValueChangeText(string text)
	{
		int value;
		if (int.TryParse(text, out value))
		{
			SetValue(value);
		}
	}

	public void Init(Trait trait)
	{
		_trait = trait;
		if (_title != null)
		{
			_title.text = trait.Title;
		}
		_inputField.text = trait.Value.ToString();
	}

	private void UpdateText()
	{
		_inputField.text = _trait.Value.ToString();
	}

	public void SetValue(int value)
	{
		if (_trait != null)
		{
			_trait.Value = math.clamp(value, 1, 10);
			UpdateText();
		}
	}

	public void Edit(bool editValue)
	{
		_inputField.enabled = editValue;
	}
}
