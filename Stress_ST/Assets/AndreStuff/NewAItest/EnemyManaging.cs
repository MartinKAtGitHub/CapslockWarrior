using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManaging : MonoBehaviour {

	public FillAndEmpty EnergyBar;
	public Animator MyAnimator;

	public SpellsInTransition[] Spells;

	float TheTime = 0;
	public int TransitionCounter = 0;
	public bool ButtonOnCD = false;
	public float ButtonCD = 0.5f;
	public float CurrentButtonCD = 0;

	public bool StopEverything = false;

	public void ButtonOnCoolDown (int OnOff){
		if (OnOff == 0) {
			ButtonOnCD = false;
		} else {
			ButtonOnCD = true;
		}
	}


	// Update is called once per frame
	void Update () {

		if (StopEverything == false) {

			TheTime += Time.deltaTime;

			if (ButtonOnCD == false) {

				for (int i = 0; i < 6; i++) {

					CurrentButtonCD = TheTime + ButtonCD;

					if (Spells [TransitionCounter].Spells [i].SpellCost <= EnergyBar.CurrentEnergy) {

						if (Spells [TransitionCounter].Spells [i].GetCurrentTime () <= TheTime) {

							if (Spells [TransitionCounter].Spells [i].RunSpellCheck () == true) {

								ButtonOnCD = true;
								Spells [TransitionCounter].Spells [i].SetCurrentTime (TheTime);
								EnergyBar.CurrentEnergy -= Spells [TransitionCounter].Spells [i].SpellCost;
								break;
							}
						}
					}
				} 
			} else {

				if (CurrentButtonCD < TheTime)
					ButtonOnCD = false;

			}
		}
	}
}
