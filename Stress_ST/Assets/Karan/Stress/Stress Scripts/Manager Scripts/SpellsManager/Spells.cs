using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Spells : MonoBehaviour{


	public float CoolDownTimer;
	public int ManaCost;
	public Sprite SpellIcon;

	// This code i feel to be convinent 
	public Transform SpellSpawnPos;
	public GameObject PlayerGameObject;
	public abstract bool IsSpellCasted {get;set;}
	public GameObject InGameSpellRef;
	//----------

	/// <summary>
	/// Use this to cast the spell
	/// </summary>
	public abstract void Cast();
	public abstract bool CastBoolienReturn();
}
