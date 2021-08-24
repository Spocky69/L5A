using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
	public enum RingType : byte
	{
		Feu,
		Air,
		Terre,
		Eau,
		Vide
	};

	private const string FILENAME = "Character";
	private static string[] TRAITS_NAME = new string[] { "AGI", "INTEL", "REF", "INTUI", "CON", "VOL", "FOR", "PER", "VIDE" };

	private const int NB_RING = 5;

	[SerializeField] private string _name = "";
	[SerializeField] private List<Ring> _rings = new List<Ring>(5);
	[SerializeField] private List<Competence> _competences = new List<Competence>();
	[SerializeField] private Rang _rangDeMaitrise = null;
	[SerializeField] private Rang _rangDeReputation = null;
	[SerializeField] private Trait _nbVoidPoints = new Trait("PTS DE VIDE", 2);
	[SerializeField] private Mon _mon = new Mon();

	public List<Ring> Rings { get { return _rings; } }
	public List<Competence> Competences { get { return _competences; } }
	public Rang RangDeMaitrise { get { return _rangDeMaitrise; } }
	public Rang RangDeReputation { get { return _rangDeReputation; } }
	public Trait NbVoidPoints { get { return _nbVoidPoints; } }
	public Mon Mon { get { return _mon; } }

	public void InitCharacter(int nbCompetences)
	{
		if (_rings.Count < NB_RING)
		{
			_rings.Clear();
			int traitIndex = 0;
			for (int i = 0; i < NB_RING; i++)
			{
				Ring ring = new Ring();

				if (traitIndex < TRAITS_NAME.Length)
				{
					Trait trait = new Trait(TRAITS_NAME[traitIndex], 2);
					ring.AddTrait(trait);
					traitIndex++;
				}

				if (traitIndex < TRAITS_NAME.Length)
				{
					Trait trait = new Trait(TRAITS_NAME[traitIndex], 2);
					ring.AddTrait(trait);
					traitIndex++;
				}
				_rings.Add(ring);
			}
		}

		if(_rangDeMaitrise == null || _rangDeMaitrise.IsValid() == false)
		{
			_rangDeMaitrise = new Rang("Rang De Maitrise");
			_rangDeMaitrise.Value = 5;
		}

		if (_rangDeReputation == null || _rangDeReputation.IsValid() == false)
		{
			_rangDeReputation = new Rang("Rang De Reputation");
			_rangDeReputation.Value = 5;
		}

		while (_competences.Count < nbCompetences)
		{
			Competence newCompetence = new Competence();
			_competences.Add(new Competence());
		}
	}

	public static string ComputeFilePath()
	{
		string filePath = Application.persistentDataPath + "/" + FILENAME + ".sav";
		return filePath;
	}	
}
