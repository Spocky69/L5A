
using System;

public class BackElement
{
	private Action _actionBack;

	public event Action ActionBackHandler
	{
		add
		{
			_actionBack -= value;
			_actionBack += value;
		}

		remove
		{
			_actionBack -= value;
		}
	}

	public void LaunchBackAction()
	{
		if(_actionBack != null)
		{
			_actionBack();
		}
	}
}
