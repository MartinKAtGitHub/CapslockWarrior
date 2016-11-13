﻿using UnityEngine;
using System.Collections;

class CaveEnemy1 : EnemyBlueprint {

	//This is how the enemies is created, add the character info, sprite/animation health dmg speed  and so on......
	//so that when we want to create another enemie we simply just dublicate this and name it what we want and set the stats for the monster

	Sprite _Enemy;
	RuntimeAnimatorController _Controller;
	int _Health;
	float _Damage;
	float _MovementSpeed;

	override public Sprite GetEnemySprite (){
		_Enemy = Resources.Load("Andre/Characters/Golem1", typeof(Sprite)) as Sprite;
		return _Enemy;
	}

	override public RuntimeAnimatorController GetEnemyAnimator (){
		_Controller = Resources.Load("Andre/Animations/GolemAnimationController", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
		return _Controller;
	}

	override public int GetEnemyHealth (){
		_Health = 5 + TalentBonusStats.EnemyHealthBonus;
		return _Health;
	}
	override public float GetEnemyDamage (){
		_Damage = 1 + TalentBonusStats.EnemyDamageBonus;
		return _Damage;
	}
	override public float GetEnemyMovementSpeed (){
		_MovementSpeed = 2 + TalentBonusStats.EnemyMovementBonus;
		return _MovementSpeed;
	}
}
