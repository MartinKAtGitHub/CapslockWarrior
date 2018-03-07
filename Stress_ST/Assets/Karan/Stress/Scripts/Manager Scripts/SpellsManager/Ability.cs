using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour{


	public abstract float CoolDownTimer{get;set;}
	public abstract int ManaCost{get;set;}
	public abstract Sprite AbilityImageIcon{get;set;}

	// This code i feel to be convinent 
	public abstract Transform SpellSpawnPos{get;set;}
	public abstract GameObject PlayerGameObject{get;set;}
	public abstract bool IsSpellCasted {get;set;}
	public abstract GameObject InGameSpellRef{get;set;}
	public abstract AudioClip AbilityActivation{get;set;}
	//----------

	/// <summary>
	/// Use this to cast the spell
	/// </summary>
	public abstract bool Cast();

}
