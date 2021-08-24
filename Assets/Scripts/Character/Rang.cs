using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class Rang : ICompetence
{
	[SerializeField] private string _title = "";
	[SerializeField] private int _value = 0;

	public Rang(string title)
	{
		_title = title;
	}

	public int Value
	{
		get { return _value; }
		set
		{
			_value = math.clamp(value, 0, 10);
		}
	}
	public int NbFreeAugmentations { get { return 0; } }
	public int BonusValue { get { return 0; } }
	public bool Specialized { get { return false;  } set { } }
	public string Title { get { return _title; } set { } }
	public bool IsValid()
	{
		if (string.IsNullOrEmpty(_title) && _value == 0)
		{
			return false;
		}
		return true;
	}

}
