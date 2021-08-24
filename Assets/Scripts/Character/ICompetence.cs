

public interface ICompetence
{
	public int Value { get; set; }
	public int BonusValue { get; }
	public int NbFreeAugmentations { get; }
	public string Title { get; set; }
	public bool Specialized { get; set; }
}
