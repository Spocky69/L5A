using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ring
{
	[SerializeField] private List<Trait> _traits = new List<Trait>(2);

	public List<Trait> Traits { get { return _traits; } }

	public void AddTrait(Trait trait)
	{
		_traits.Add(trait);
	}
}
