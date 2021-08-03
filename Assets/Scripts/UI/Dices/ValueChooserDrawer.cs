using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ValueChooserDrawer : MonoBehaviour
{
	[SerializeField] private TMP_Text _value = null;
	[SerializeField] private int _defaultValue = 5;

	private int _curValue = 0;
	private int _minValue = int.MinValue;
	private int _maxValue = int.MaxValue;
	private Action _upEvent = null;
	private Action _downEvent = null;

	public int CurValue { get { return _curValue; } }

	// Events
	public event Action UpHandler
	{
		add { _upEvent -= value; _upEvent += value; }
		remove { _upEvent -= value; }
	}

	public event Action DownHandler
	{
		add { _downEvent -= value; _downEvent += value; }
		remove { _downEvent -= value; }
	}

	public void Reset(int minValue, int maxValue)
	{
		_curValue = _defaultValue;
		_minValue = minValue;
		_maxValue = maxValue;
		UpdateTextValue();
	}

	public void SetValue(int value)
	{
		_curValue = math.clamp(value, _minValue, _maxValue);
		UpdateTextValue();
	}

	private void UpdateTextValue()
	{
		if (_value != null)
		{
			_value.text = _curValue.ToString();
		}
	}

	public void OnButtonUp()
	{
		SetValue(_curValue + 1);
		_upEvent?.Invoke();
	}

	public void OnButtonDown()
	{
		SetValue(_curValue - 1);
		_downEvent?.Invoke();
	}
}
