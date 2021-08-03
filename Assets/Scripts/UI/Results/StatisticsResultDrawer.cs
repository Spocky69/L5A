using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsResultDrawer : MonoBehaviour
{
	[SerializeField] private TMP_Text _rollValueText = null;
	[SerializeField] private TMP_Text _rollPercentText = null;
	[SerializeField] private Image _border;
	[SerializeField] private Color _beginColor;
	[SerializeField] private Color _middleColor;
	[SerializeField] private Color _endColor;


	private int _rollValue = 0;
	private float _rollPercent = 0.0f;

	public static bool IsAutoSuccess(float rollPercent)
	{
		return rollPercent > 99.7f;
	}

	// Start is called before the first frame update
	public void Reset()
	{
		_border.color = Color.white;
		_rollValue = 0;
		_rollPercent = 0.0f;
		UpdateTextValue();
		gameObject.SetActive(false);
	}

	public void UpdateTextValue()
	{
		_rollValueText.text = _rollValue.ToString();

		int roundRollPercent = (int)math.round(_rollPercent);

		if (IsAutoSuccess(_rollPercent))
		{
			_rollPercentText.text = "--";
			_border.color = _beginColor;
		}
		else if (roundRollPercent >= 1)
		{
			// Do not allow to draw 100%
			if (_rollPercent > 99.0f)
			{
				roundRollPercent = 99;
			}
			_rollPercentText.text = roundRollPercent.ToString() + "%";

			float colorRatio = math.clamp(math.unlerp(99.0f, 10.0f, _rollPercent), 0.0f, 1.0f);

			Color color;
			if (colorRatio < 0.5f)
			{
				color = Color.Lerp(_beginColor, _middleColor, colorRatio);
			}
			else
			{
				color = Color.Lerp(_middleColor, _endColor, colorRatio);
			}
			_border.color = color;

		}
		else if (_rollPercent > 0.1f)
		{
			_rollPercentText.text = "<1%";
			_border.color = _endColor;
		}
		else
		{
			_rollPercentText.text = "--";
			_border.color = Color.black;
		}
	}

	public void Fill(int value, float percent)
	{
		if (value > 0)
		{
			gameObject.SetActive(true);
			_rollValue = value;
			_rollPercent = percent;
			UpdateTextValue();
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
}
