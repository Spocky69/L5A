using System;

public struct StatisticsRollerResult
{
	public int RollValue;
	public int NbGoodValue;
	public float PercentValue;

	public void Clear()
	{
		NbGoodValue = 0;
		PercentValue = 0.0f;
	}

	public void AddGoodValue()
	{
		NbGoodValue++;
	}

	internal void FillPercent(int nbRollDice)
	{
		PercentValue = 100.0f * (float)NbGoodValue / nbRollDice;
	}
}
