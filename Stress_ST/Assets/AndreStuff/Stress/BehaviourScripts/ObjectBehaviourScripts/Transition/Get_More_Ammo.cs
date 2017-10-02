using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_More_Ammo : The_Default_Transition_Info {

	public int AmmoGained = 1;
	public bool RemoveOnExit = false;

	[Tooltip("0 == Set Ammo To Zero, 1 And Up Is Reduction (subtraction)")]
	public int Removing = 0; 
	public ObjectStats _TheObject;

	public override void OnEnter(){
		_TheObject.Ammo += AmmoGained;
	}

	public override void OnExit(){
		if (RemoveOnExit == true) {
			if (Removing == 0) {
				_TheObject.Ammo = 0;
			} else {
				_TheObject.Ammo -= Removing;
				if (_TheObject.Ammo < 0)
					_TheObject.Ammo = 0;
			}
		}
			
	}

	public  override void OnReset(){

	}

}
