using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class RingDrawer : MonoBehaviour
{
	[SerializeField] private TMP_Text _valueText = null;
	[SerializeField] private List<TraitDrawer> _traitsDrawer = new List<TraitDrawer>();

	private Ring _ring;

	public void Init(Ring ring)
	{
		_ring = ring;
		for (int i = 0; i < _traitsDrawer.Count; i++)
		{
			_traitsDrawer[i].Init(_ring.Traits[i]);
		}
		Update();
	}

	public void Reset()
	{
		foreach (TraitDrawer traitDrawer in _traitsDrawer)
		{
			traitDrawer.Reset();
		}
	}

	public void Update()
	{
		int ring = 10;
		foreach (Trait trait in _ring.Traits)
		{
			ring = math.min(trait.Value, ring);
		}
		_valueText.text = ring.ToString();
	}

	public void Edit(bool editValue)
	{
		foreach (TraitDrawer traitDrawer in _traitsDrawer)
		{
			traitDrawer.Edit(editValue);
		}
	}
}
