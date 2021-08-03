public struct Dice
{
	public int Result;

	static public int Roll(ref Unity.Mathematics.Random random)
	{
		int result = random.NextInt(1, 11);
		int finalResult = result;
		while (result == 10)
		{
			result = random.NextInt(1, 11);
			finalResult += result;
		}
		return finalResult;
	}
}
