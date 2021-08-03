using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsDrawer : MonoBehaviour
{
	[SerializeField] private List<RingDrawer> _ringDrawers = new List<RingDrawer>();

	public void Reset()
	{
		foreach (RingDrawer ringDrawer in _ringDrawers)
		{
			ringDrawer.Reset();
		}
	}

	public void Init(Character character)
	{
		int nbRingType = Enum.GetNames(typeof(Character.RingType)).Length;
		for (int i = 0; i < nbRingType; i++)
		{
			_ringDrawers[i].Init(character.Rings[i]);
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
