using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsDrawer : MonoBehaviour
{
	[SerializeField] private List<RingDrawer> _ringDrawers = new List<RingDrawer>();
	[SerializeField] private MonDrawer _monDrawer = new MonDrawer();

	public void Reset()
	{
		foreach (RingDrawer ringDrawer in _ringDrawers)
		{
			ringDrawer.Reset(this);
		}
	}

	public void Init(Character character)
	{
		int nbRingType = Enum.GetNames(typeof(Character.RingType)).Length;
		for (int i = 0; i < nbRingType; i++)
		{
			_ringDrawers[i].Init(character.Rings[i]);
		}
		_monDrawer.Init(character.Mon);
	}

	public void SetSelected(TraitDrawer traitDrawerSelected)
	{
		foreach (RingDrawer ringDrawer in _ringDrawers)
		{
			foreach (TraitDrawer traitDrawer in ringDrawer.TraitsDrawer)
			{
				traitDrawer.SetSelected(traitDrawer == traitDrawerSelected);
			}
		}
	}

	public void Edit(bool editValue)
	{
		foreach (RingDrawer ringDrawer in _ringDrawers)
		{
			ringDrawer.Edit(editValue);
		}
	}
}
