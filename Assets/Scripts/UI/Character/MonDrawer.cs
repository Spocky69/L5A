using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MonDrawer : MonoBehaviour
{
	[SerializeField] private Button _button = null;
	[SerializeField] private Image _image = null;
	[SerializeField] private List<Sprite> _monTextures = new List<Sprite>();

	private Mon _mon = null;

	public void Reset()
	{
		_button.onClick.AddListener(OnMonButtonPush);
		_button.enabled = false;
	}

	public void Init(Mon mon)
	{
		_mon = mon;
		SetImageIndex(_mon.ImageIndex);
	}

	private void SetImageIndex(int index)
	{
		_image.sprite = _monTextures[index];
	}

	public void OnMonButtonPush()
	{
		int newIndex = (_mon.ImageIndex + 1) % _monTextures.Count;
		_mon.ImageIndex = newIndex;
		SetImageIndex(_mon.ImageIndex);
	}

	public void Edit(bool editValue)
	{
		_button.enabled = editValue;
	}
}
