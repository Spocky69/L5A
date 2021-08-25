using System.Collections.Generic;
using UnityEngine;

static public class BackContext
{
	static private List<BackElement> _backElements = new List<BackElement>();


	static public void AddBackElement(BackElement backElement)
	{
		_backElements.Add(backElement);
	}

	static public void RemoveBackElement(BackElement backElement)
	{
		if(_backElements.Contains(backElement))
		{
			_backElements.Remove(backElement);
		}
	}

	static public void RemoveTopBackElement()
	{
		if(_backElements.Count > 0)
		{
			_backElements[0].LaunchBackAction();
			_backElements.RemoveAt(0);
		}
		else
		{
			// Quit the application
			Application.Quit();
		}
	}
}
