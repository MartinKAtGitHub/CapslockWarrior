using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour {
	 
	public EnergyBar EnergyBar;

	public AbilityInfo Spells;

	
	// Update is called once per frame
	void Update () {


	/*	TheTime += Time.deltaTime;

		for(int i = 0; i < 6; i++){
			if (Spells [i].SpellCost <= EnergyBar.CurrentEnergy) {
			
				if (Spells [i].GetCurrentTime() <= TheTime) {

					Spells [i].StartFill = true;
					Spells [i].SetCurrentTime(TheTime);
					EnergyBar.CurrentEnergy -= Spells [i].SpellCost;
				}
			}
		}
*/


	}
}
