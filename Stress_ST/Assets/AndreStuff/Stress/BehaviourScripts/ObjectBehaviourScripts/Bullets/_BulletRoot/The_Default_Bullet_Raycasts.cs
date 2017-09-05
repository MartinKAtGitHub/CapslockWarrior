using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Default_Bullet_Raycasts : The_Default_Bullet {

	public StressEnums.BulletCast WhatToCast;
	public Vector3 CastSize = Vector3.zero;
	public StressEnums.BulletRotationBehaviour BulletRotation;
	//	public StressEnums.BulletStyle BulletStyle;

//	public enum BulletOptions{SenderPosVectorPos = 0, SenderRotationAndVectorPos = 1, SenderRotationAndVectorPosAndRot = 2, Target = 3, PlaceAtTargetRotation = 4, PlaceAtTargetRotationAndRotate = 5}


	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);
	}


}
