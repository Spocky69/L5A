using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class Ring
{
	[SerializeField] private List<Trait> _traits = new List<Trait>(2);

	public List<Trait> Traits { get { return _traits; } }

	public int Value 
	{ 
		get 
		{
			int ring = 10;
			foreach (Trait trait in _traits)
			{
				ring = math.min(trait.Value, ring);
			}
			return ring;
		} 
	}

	public void AddTrait(Trait trait)
	{
		_traits.Add(trait);
	}
}
