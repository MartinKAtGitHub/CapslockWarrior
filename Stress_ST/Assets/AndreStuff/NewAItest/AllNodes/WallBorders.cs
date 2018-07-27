using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBorders : Spells {
	//Stops The Object Moving Inside This Node.

	int nodePosX = 0;
	int nodePosY = 0;

	Vector3 _TravelDirection = Vector3.zero;
	const float OffsetX = 0.01f;

	public override void ApplyTileEffect(TestWalkScript target) {//Called From The Tile. 'What Does The Spell Do' -> 'Im Applying A Buff' -> 'Apply Buff' 
		_TravelDirection = target.transform.position - target.PreviousPosition;

		nodePosX = Mathf.FloorToInt((target.transform.position.x - StressCommonlyUsedInfo.LowestXPos) / 0.25f);//World Position Translated To Node Position
		nodePosY = Mathf.FloorToInt((target.transform.position.y - StressCommonlyUsedInfo.LowestYPos) / 0.25f);//World Position Translated To Node Position

		if (target.PreviousPosition.x - StressCommonlyUsedInfo.LowestXPos < nodePosX * 0.25f) {//If The Previous Position Were On The Left Side Of The Nodes Left Side
			target.transform.position = target.PreviousPosition + (((((nodePosX * 0.25f) + StressCommonlyUsedInfo.LowestXPos) - target.PreviousPosition.x) / _TravelDirection.x) * _TravelDirection) + (Vector3.left * OffsetX/*TODO Directional Offset*/);//Finding The Point Which The Old And New Position Entered The Square. Then That Value * Travel Vector == On The Wall. 0.001 Is Just So That The Object Goes OutSide Of The Square

		} else if (target.PreviousPosition.x - StressCommonlyUsedInfo.LowestXPos > (1 + nodePosX) * 0.25f) {//If The Previous Position Were On The Right Side Of The Node
			target.transform.position = target.PreviousPosition + (((((1 + nodePosX) * 0.25f) + StressCommonlyUsedInfo.LowestXPos) - target.PreviousPosition.x) / _TravelDirection.x) * _TravelDirection + (Vector3.right * OffsetX);

		} else if (target.PreviousPosition.y - StressCommonlyUsedInfo.LowestYPos > (1 + nodePosY) * 0.25f) {//If The Previous Position Were Over The Node
			target.transform.position = target.PreviousPosition + (((((1 + nodePosY) * 0.25f) + StressCommonlyUsedInfo.LowestYPos) - target.PreviousPosition.y) / _TravelDirection.y) * _TravelDirection + (Vector3.up * OffsetX);

		} else if (target.PreviousPosition.y - StressCommonlyUsedInfo.LowestYPos < nodePosY * 0.25f) {//If The Previous Position Were Below The Node
			target.transform.position = target.PreviousPosition + (((((nodePosY) * 0.25f) + StressCommonlyUsedInfo.LowestYPos) - target.PreviousPosition.y) / _TravelDirection.y) * _TravelDirection + (Vector3.down * OffsetX);

		}

	}

}
