using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RingDrawer : MonoBehaviour, ISelected
{
	[SerializeField] private TMP_Text _valueText = null;
	[SerializeField] private List<TraitDrawer> _traitsDrawer = new List<TraitDrawer>();
	[SerializeField] private Button _button = null;
	[SerializeField] private List<Image> _imagesSelection = new List<Image>();
	[SerializeField] private Color _neutralColor = Color.white;
	[SerializeField] private Color _selectColor = Color.red;

	private bool _select = false;
	private Ring _ring;
	private CharacteristicsDrawer _characteristicsDrawer;

	public CharacteristicsDrawer CharacteristicsDrawer { get { return _characteristicsDrawer; } }
	public List<TraitDrawer> TraitsDrawer { get { return _traitsDrawer; } }
	public int Value { get { return _ring.Value; } }

	public void Reset(CharacteristicsDrawer characteristicsDrawer)
	{
		_characteristicsDrawer = characteristicsDrawer;
		foreach (TraitDrawer traitDrawer in _traitsDrawer)
		{
			traitDrawer.Reset(this);
		}

		if (_button != null)
		{
			_button.onClick.AddListener(OnButtonSelect);
			_button.enabled = true;
		}

		SetSelected(false);
	}

	public void Init(Ring ring)
	{
		_ring = ring;
		for (int i = 0; i < _traitsDrawer.Count; i++)
		{
			_traitsDrawer[i].Init(_ring.Traits[i]);
		}
		Update();
	}

	public void Update()
	{
		if (_ring != null)
		{
			_valueText.text = Value.ToString();
		}

	}

	public void OnButtonSelect()
	{
		if (_select == false)
		{
			_characteristicsDrawer.SetSelected(this);
		}
		else
		{
			_characteristicsDrawer.SetSelected(null);
		}
	}

	public void Edit(bool editValue)
	{
		foreach (TraitDrawer traitDrawer in _traitsDrawer)
		{
			traitDrawer.Edit(editValue);
		}

		if (_button != null)
		{
			_button.enabled = !editValue;
		}
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
}
