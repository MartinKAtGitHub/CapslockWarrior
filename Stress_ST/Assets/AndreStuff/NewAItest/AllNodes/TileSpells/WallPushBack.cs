using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPushBack : Spells {
	/*Force Pushback*/

	Vector3 _ForceApplyer = Vector3.zero;
	public float _BounceStrength = 1;


	public override void ApplyTileEffect(TestWalkScript target) {//Called From The Tile. 'What Does The Spell Do' -> 'Im Applying A Buff' -> 'Apply Buff' 

		_ForceApplyer.x = ((target.transform.position.x - StressCommonlyUsedInfo.LowestXPos) % 0.25f) - 0.125f;
		_ForceApplyer.y = ((target.transform.position.y - StressCommonlyUsedInfo.LowestYPos) % 0.25f) - 0.125f;

		target.FocePush();
		target.MyBody2D.velocity = _ForceApplyer.normalized / _BounceStrength;

	
	}

	
}
