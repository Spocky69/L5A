using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class Competence
{
	[SerializeField] private string _title = "";
	[SerializeField] private int _value = 0;
	[SerializeField] private bool _specialized = false;

	public int Value
	{
		get { return _value; }
		set
		{
			_value = math.clamp(value, 0, 10);
		}
	}
	public int BonusValue { get { return _specialized ? _value : 0; } }
	public int NbFreeAugmentations { get { return _value > 5 ? 1 : 0; } }

	public string Title { get { return _title; } set { _title = value; } }
	public bool Specialized { get { return _specialized; } set { _specialized = value; } }

	public void Init(string title, string value)
	{
		_title = title;
		_value = int.Parse(value);
	}
}
