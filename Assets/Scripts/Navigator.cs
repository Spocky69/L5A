using UnityEngine;
using UnityEngine.UI;

public class Navigator : MonoBehaviour
{
	private enum Page
	{
		RollDices,
		Character
	}

	[SerializeField] private RollDicesPage _rollDicesPage;
	[SerializeField] private CharacterPage _characterPage;
	[SerializeField] private Button _characterPageButton;
	[SerializeField] private Button _rollDicesPageButton;


	private Page _page = Page.RollDices;

	private void Start()
	{
		SetPage(Page.Character);
		_characterPageButton.onClick.AddListener(OnSetRollDicesPageButton);
		_rollDicesPageButton.onClick.AddListener(OnSetCharacterPageButton);
	}

	private void SetPage(Page page)
	{
		_page = page;
		switch (page)
		{
			case Page.RollDices:
				{
					_characterPage.gameObject.SetActive(false);
					_rollDicesPage.gameObject.SetActive(true);
				}
				break;

			case Page.Character:
				{
					_characterPage.gameObject.SetActive(true);
					_rollDicesPage.gameObject.SetActive(false);
				}
				break;
		}
	}

	public void OnSetRollDicesPageButton()
	{
		SetPage(Page.RollDices);
	}

	public void OnSetCharacterPageButton()
	{
		SetPage(Page.Character);
	}

	public void Update()
	{
		// Check if Back was pressed this frame
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			BackContext.RemoveTopBackElement();
		}
	}
}
