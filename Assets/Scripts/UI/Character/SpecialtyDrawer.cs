using UnityEngine;
using UnityEngine.UI;

class SpecialtyDrawer : MonoBehaviour
{
	[SerializeField] private Button _button = null;
	[SerializeField] private Color _selectedColor = Color.green;
	[SerializeField] private Color _unselectedColor = Color.white;

	private bool _selected = false;

	public void Reset()
	{
		_button.onClick.AddListener(OnButtonPush);
	}

	public void Unselect()
	{
		SetColor(_unselectedColor);
	}

	public void Select()
	{
		SetColor(_selectedColor);
	}

	private void SetColor(Color colorIn)
	{
		ColorBlock colorBlock = _button.colors;
		colorBlock.normalColor = colorIn;

		float alpha = colorBlock.highlightedColor.a;
		Color color = colorIn;
		color.a = alpha;
		colorBlock.highlightedColor = colorIn;
		colorBlock.selectedColor = colorIn;
		colorBlock.pressedColor = colorIn;
		_button.colors = colorBlock;
	}

	public void OnButtonPush()
	{
		_selected = !_selected;
		if (_selected)
		{
			Select();
		}
		else
		{
			Unselect();
		}
	}
}
