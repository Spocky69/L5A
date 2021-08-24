using UnityEngine;
using UnityEngine.UI;

class SpecialtyDrawer : MonoBehaviour
{
	[SerializeField] private Button _button = null;
	[SerializeField] private Color _selectedColor = Color.green;
	[SerializeField] private Color _unselectedColor = Color.white;

	private ICompetence _competence = null;
	private CompetenceDrawer _competencesDrawer = null;

	public void Reset(CompetenceDrawer competencesDrawer)
	{
		_competencesDrawer = competencesDrawer;
		_button.onClick.AddListener(OnButtonPush);
	}

	public void Init(ICompetence competence)
	{
		_competence = competence;
		if(_competence.Specialized)
		{
			SetColor(_selectedColor);
		}
		else
		{
			SetColor(_unselectedColor);
		}
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
		_competence.Specialized = !_competence.Specialized;
		if (_competence.Specialized)
		{
			Select();
		}
		else
		{
			Unselect();
		}
		_competencesDrawer.OnUpdateValue();
	}
}
