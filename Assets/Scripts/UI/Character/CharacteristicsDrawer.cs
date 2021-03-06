using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsDrawer : MonoBehaviour
{
	[SerializeField] private List<RingDrawer> _ringDrawers = new List<RingDrawer>();
	[SerializeField] private TraitDrawer _nbVoidPointsDrawer = null;
	[SerializeField] private MonDrawer _monDrawer = new MonDrawer();

	private CharacterPage _characterPage = null;

	public void Reset(CharacterPage characterPage)
	{
		_characterPage = characterPage;
		_nbVoidPointsDrawer.Reset(null);
		_monDrawer.Reset();
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
		_nbVoidPointsDrawer.Init(character.NbVoidPoints);
		_nbVoidPointsDrawer.Edit(true);
	}

	public void SetSelected(ISelected traitDrawerSelected)
	{
		foreach (RingDrawer ringDrawer in _ringDrawers)
		{
			foreach (ISelected traitDrawer in ringDrawer.TraitsDrawer)
			{
				traitDrawer.SetSelected(traitDrawer == traitDrawerSelected);
			}
			ringDrawer.SetSelected((ISelected)ringDrawer == traitDrawerSelected);
		}
		_characterPage.SetSelectedTraitDrawer(traitDrawerSelected);
	}

	public void Edit(bool editValue)
	{
		foreach (RingDrawer ringDrawer in _ringDrawers)
		{
			ringDrawer.Edit(editValue);
		}
		_monDrawer.Edit(editValue);
	}
}
