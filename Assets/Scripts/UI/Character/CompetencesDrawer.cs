using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetencesDrawer : MonoBehaviour
{
	[SerializeField] private List<CompetenceDrawer> _competencesDrawers = new List<CompetenceDrawer>();

	public int NbCompencesDrawer { get { return _competencesDrawers.Count; } }

	private CharacterPage _characterPage = null;

	public void Reset(CharacterPage characterPage)
	{
		_characterPage = characterPage;
		foreach (CompetenceDrawer competenceDrawer in _competencesDrawers)
		{
			competenceDrawer.Reset(this);
		}
	}

	public void Init(Character character)
	{
		int nbCompetences = _competencesDrawers.Count;
		for (int i = 0; i < nbCompetences; i++)
		{
			if (i < character.Competences.Count)
			{
				_competencesDrawers[i].Init(character.Competences[i]);
			}
		}
	}

	public void Edit(bool edit)
	{
		foreach (CompetenceDrawer competenceDrawer in _competencesDrawers)
		{
			competenceDrawer.Edit(edit);
		}
	}

	public void SetSelected(CompetenceDrawer competenceDrawerSelected)
	{
		foreach (CompetenceDrawer competenceDrawer in _competencesDrawers)
		{
			competenceDrawer.SetSelected(competenceDrawer == competenceDrawerSelected);
		}
		_characterPage.SetSelectedCompetenceDrawer(competenceDrawerSelected);
	}

	public void OnUpdateValue()
	{
		_characterPage.OnUpdateValue();
	}
}
