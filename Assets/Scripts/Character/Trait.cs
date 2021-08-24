using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class Trait
{
	[SerializeField] private string _title = "";
	[SerializeField] private int _value = 0;

	public int Value
	{
		get { return _value; }
		set { _value = value; }
	}

	public string Title { get { return _title; } }

	public Trait(string title, int value)
	{
		_title = title;
		_value = value;
	}
}
