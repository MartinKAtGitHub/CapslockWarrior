using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour {

	public TileObjects TheTileLogic;
	public float TileStrengthMultiplyer = 1;

	//TODO Aditional Effects

	public virtual void TileDamaged(float Damage) {
		//Nothing
	}

}
