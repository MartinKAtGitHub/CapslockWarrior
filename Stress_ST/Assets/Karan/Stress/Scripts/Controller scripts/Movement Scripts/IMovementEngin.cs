using UnityEngine;
using System.Collections;

public interface IMovementEngin {

	 float Speed{ get;set; }
	 Rigidbody2D PlayerRigBdy{get;set;}
	 Vector2 Direction{get;set;}
	 bool SpriteFacingRigth{get;set;}


	void MovementLogic();
	void Flip();

}
