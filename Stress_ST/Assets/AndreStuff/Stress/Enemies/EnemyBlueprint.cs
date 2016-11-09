using UnityEngine;
using System.Collections;

abstract class EnemyBlueprint : MonoBehaviour {

	//This is a blueprint for the enemies, enemies need to inherit from this class. its just the way i made the system ;-p

	abstract public Sprite GetEnemySprite ();
	abstract public int GetEnemyHealth ();
	abstract public float GetEnemyDamage ();
	abstract public float GetEnemyMovementSpeed ();
	abstract public RuntimeAnimatorController GetEnemyAnimator ();
}
