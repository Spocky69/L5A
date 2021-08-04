using System;
using UnityEngine;

[Serializable]
public class Mon
{
	[SerializeField] private int _imageIndex = 0;

	public int ImageIndex { get { return _imageIndex; } set { _imageIndex = value; } }

}
