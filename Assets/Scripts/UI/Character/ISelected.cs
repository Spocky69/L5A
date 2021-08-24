
public interface ISelected
{
	int Value { get; }
	void SetSelected(bool select);
	void OnButtonSelect();
}
