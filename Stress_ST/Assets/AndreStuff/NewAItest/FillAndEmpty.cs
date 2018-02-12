using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAndEmpty : MonoBehaviour {

	public SpriteRenderer FillingBar;
	public float FillingSpeed = 1;
	public float EnergyMax = 10;
	public float CurrentEnergy = 0;

	float FillingMaxSize = 0;
	Vector2 FillingSize = Vector2.zero;
	public bool StartFill = true;

	// Use this for initialization
	void Start () {
		FillingSize = FillingBar.size;
		FillingMaxSize = FillingBar.size.x;
		FillingBar.size = FillingSize;
	}
	
	// Update is called once per frame

	void Update () {

		if (StartFill == true) {


				if (FillingSize.x >= FillingMaxSize) {
							
					FillingSize.x = 0;
					if (CurrentEnergy < EnergyMax)
						CurrentEnergy++;
				}

				FillingSize.x += Time.deltaTime * FillingSpeed;
				FillingBar.size = FillingSize;
			
		}
	}
}

public static class Testingclass {

	public static void TEST1(){
	
	}

}