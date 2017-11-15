using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : AbsoluteRoot {

	[Header("Object Stats")]
	[Space(5)]

	public int HealthWords = 1;
	public int[] HealthWordsLength;
	
	public float MovementSpeed = 1;

	public float AttackStrength = 1;
	public float AttackRange = 1;

	public int Ammo = 1;
	public int Energy = 1;


	public override void HealthWordChange(int _damage){
		HealthWords -= _damage;
	}//Total Word Decrease/Increase

	public override void RecievedDmg (int _damage) {
		HealthWords -= _damage;
	}//Reduction To Letters In Words (Or To Player)

	public override void MovementSpeedChange(float _moveSpeed){
		MovementSpeed += _moveSpeed;	
	}

	public virtual void AttackSpeedChange(float _attackSpeed){}

	public virtual void AttackStrengthChange(float _strength){}
	public virtual void AttackRangeChange(float _range){}

	public virtual void AmmoChange(float _ammo){}
	public virtual void EnergyChange(float _energy){}

	public virtual void GotTheKill(int _score) {}

	public virtual void OnDestroyed(){}

	public virtual void SetTarget(GameObject target){}



}
