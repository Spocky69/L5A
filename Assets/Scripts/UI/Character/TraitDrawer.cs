using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TraitDrawer : MonoBehaviour
{
	[SerializeField] private TMP_Text _title = null;
	[SerializeField] private TMP_InputField _inputField = null;
	[SerializeField] private Button _button = null;
	[SerializeField] private List<Image> _imagesSelection = new List<Image>();
	[SerializeField] private Color _neutralColor = Color.white;
	[SerializeField] private Color _selectColor = Color.red;

	private Trait _trait = null;
	private bool _select = false;
	private RingDrawer _ringDrawer = null;

	public void Reset(RingDrawer ringDrawer)
	{
		_ringDrawer = ringDrawer;
		_inputField.onValueChanged.RemoveListener(OnValueChangeText);
		_inputField.onValueChanged.AddListener(OnValueChangeText);
		_inputField.enabled = false;
		_select = false;
		_button.onClick.AddListener(OnButtonSelect);
		_button.enabled = true;
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
		_button.enabled = !editValue;
		_ringDrawer.CharacteristicsDrawer.SetSelected(null);
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
		if(_select == false)
		{
			_ringDrawer.CharacteristicsDrawer.SetSelected(this);
		}
		else
		{
			_ringDrawer.CharacteristicsDrawer.SetSelected(null);
		}
	}
}
