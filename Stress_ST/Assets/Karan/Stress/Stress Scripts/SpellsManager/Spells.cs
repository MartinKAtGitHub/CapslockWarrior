using UnityEngine;
using System.Collections;

public abstract class Spells : MonoBehaviour{


	public float CoolDownTimer;

	// This code i feel to be convinent 
	public Transform SpellSpawnPos;
	public GameObject PlayerGameObject;
	public abstract bool IsSpellCasted {get;set;}
	//----------

	/// <summary>
	/// Use this to cast the spell
	/// </summary>
	public abstract void Cast();

}
