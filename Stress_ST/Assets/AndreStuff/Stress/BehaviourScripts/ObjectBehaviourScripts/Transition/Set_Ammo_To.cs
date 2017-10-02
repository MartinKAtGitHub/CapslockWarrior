using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Ammo_To : The_Default_Transition_Info {

	public int TheAmmo = 1;
	public bool AttackStrengthMultiplyer = true;
	public ObjectStats MyStat;

	public override void OnEnter(){
		MyStat.Ammo = TheAmmo * (int)MyStat.AttackStrength;
	}

	public override void OnExit(){

	}

	public  override void OnReset(){

	}

}
