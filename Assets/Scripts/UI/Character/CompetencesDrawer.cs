using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetencesDrawer : MonoBehaviour
{
	[SerializeField] private List<CompetenceDrawer> _competencesDrawers = new List<CompetenceDrawer>();

	public int NbCompencesDrawer { get { return _competencesDrawers.Count; } }

	public void Reset()
	{
		foreach (CompetenceDrawer competenceDrawer in _competencesDrawers)
		{
			competenceDrawer.Reset();
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
}
