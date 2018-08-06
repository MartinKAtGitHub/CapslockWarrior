using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour {

	public TileObjects TheTileLogic;
	public float TileStrengthMultiplyer = 1;

	//TODO Aditional Effects

	private void Start() {

		StressCommonlyUsedInfo.TheSetter.BaseGroundTiles(Mathf.FloorToInt((transform.position.x - StressCommonlyUsedInfo.LowestXPos) / 0.25f), Mathf.FloorToInt((transform.position.y - StressCommonlyUsedInfo.LowestYPos) / 0.25f), this);

	}

	public virtual void TileDamaged(float Damage) {
		//Nothing
	}

}
