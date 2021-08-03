using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
	private enum Page
	{
		RollDices,
		Character
	}

	[SerializeField] private RollDicesPage _rollDicesPage;
	[SerializeField] private CharacterPage _characterPage;
	

	private Page _page = Page.RollDices;

	private void Start()
	{
		SetPage(Page.RollDices);
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

	public void OnSetCharacterPageButton()
	{
		SetPage(Page.Character);
	}

	public void OnSetCharacterPageButton()
	{
		SetPage(Page.Character);
	}


}
