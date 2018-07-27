using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpell : Spells {

	int PlayerMovementRestictionXRight = 0;//-1 == No Move Left.   0 == Neutral.    1 == No More Right
	int PlayerMovementRestictionXLeft= 0;//-1 == No Move Left.   0 == Neutral.    1 == No More Right
	int PlayerMovementRestictionYUp = 0;//-1 == No Move Down.   0 == Neutral.    1 == No More Up
	int PlayerMovementRestictionYDown = 0;//-1 == No Move Down.   0 == Neutral.    1 == No More Up

	float lowerLeftPosX = -1f;
	float lowerLeftPosY = -0.25f;

	int nodePosX = 0;
	int nodePosY = 0;

	Vector3 travelVector = Vector3.zero;
	TestWalkScript test;
	float HitTheSide = 0;
	float bounceStrength = 1;


	public virtual void ApplySpellEffect(TestWalkScript target) {

	}
	public override void ApplyTileEffect(TestWalkScript target) {//Called From The Tile. 'What Does The Spell Do' -> 'Im Applying A Buff' -> 'Apply Buff' 
		test = target;
		travelVector = target.transform.position - test.PreviousPosition;

		/*Improved Wall. Teleport Stop*/

		//nodePosX = Mathf.FloorToInt((target.transform.position.x - lowerLeftPosX) / 0.25f);//World Position Tranlated To Node Position
		//nodePosY = Mathf.FloorToInt((target.transform.position.y - lowerLeftPosY) / 0.25f);//World Position Tranlated To Node Position

		//if (test.PreviousPosition.x - lowerLeftPosX < nodePosX * 0.25f) {//If The Previous Position Were On The Left Side Of The Nodes Left Side
		//	target.transform.position = test.PreviousPosition + (((((nodePosX * 0.25f) + lowerLeftPosX) - test.PreviousPosition.x) / travelVector.x) * travelVector) + (Vector3.left * 0.001f/*TODO Directional Offset*/);//Finding The Point Which The Old And New Position Entered The Square. Then That Value * Travel Vector == On The Wall. 0.001 Is Just So That The Object Goes OutSide Of The Square

		//} else if (test.PreviousPosition.x - lowerLeftPosX > (1 + nodePosX) * 0.25f) {//If The Previous Position Were On The Right Side Of The Node
		//	target.transform.position = test.PreviousPosition + (((((1 + nodePosX) * 0.25f) + lowerLeftPosX) - test.PreviousPosition.x) / travelVector.x) * travelVector + (Vector3.right * 0.001f);

		//} else if (test.PreviousPosition.y - lowerLeftPosY > (1 + nodePosY) * 0.25f) {//If The Previous Position Were Over The Node
		//	target.transform.position = test.PreviousPosition + (((((1 + nodePosX) * 0.25f) + lowerLeftPosY) - test.PreviousPosition.y) / travelVector.y) * travelVector + (Vector3.up * 0.001f);

		//} else if (test.PreviousPosition.y - lowerLeftPosY < nodePosY * 0.25f) {//If The Previous Position Were Below The Node
		//	target.transform.position = test.PreviousPosition + (((((nodePosX) * 0.25f) + lowerLeftPosY) - test.PreviousPosition.y) / travelVector.y) * travelVector + (Vector3.down * 0.001f);

		//} 

		/*Force Pushback*/
		//test.FocePush();
		//test.MyBody2D.velocity = new Vector2(((target.transform.position.x - lowerLeftPosX) % 0.25f - 0.125f), ((target.transform.position.y - lowerLeftPosY) % 0.25f - 0.125f)).normalized / bounceStrength;

		/*Walk In Denial*/
		//		Debug.Log("entered " + target.transform.position.x + " | " + (target.transform.position.x % 0.25f));
		//		Debug.Log("entered " + (target.transform.position.x - lowerLeftPosX) + " | " + ((target.transform.position.x - lowerLeftPosX) % 0.25f));
		//		if ((target.transform.position.x - lowerLeftPosX) % 0.25f <= 0.125f) {
		//			target.GetComponent<TestWalkScript>().MoveRestricitonRight = 0;
		//		}
		//		if ((target.transform.position.x - lowerLeftPosX) % 0.25f >= 0.125f) {
		//			target.GetComponent<TestWalkScript>().MoveRestricitonLeft = 0;
		//		}
		//		if ((target.transform.position.y - lowerLeftPosY) % 0.25f <= 0.125f) {
		//			target.GetComponent<TestWalkScript>().MoveRestricitonUp = 0;
		//		}
		//		if ((target.transform.position.y - lowerLeftPosY) % 0.25f >= 0.125f) {
		//			target.GetComponent<TestWalkScript>().MoveRestricitonDown = 0;
		//		}

	}

	public override void RemoveTileEffect(TestWalkScript target) {//Called From The Tile. 'What Does The Spell Do' -> 'Im Applying A Buff' -> 'Apply Buff' 
	//	target.GetComponent<TestWalkScript>().ForcePush = false;

//		Debug.Log("exited");
//		target.GetComponent<TestWalkScript>().MoveRestricitonLeft = 1;
//		target.GetComponent<TestWalkScript>().MoveRestricitonRight = 1;
//		target.GetComponent<TestWalkScript>().MoveRestricitonUp = 1;
//		target.GetComponent<TestWalkScript>().MoveRestricitonDown = 1;

	}

}
