using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public int PlayerMaximumDamage = 10;
	public float AttackRange = 1;//The Range At Which The Player Can Attack
	public float RateOfFire = 0.5f;//Fire Rate, The Lower The Value The Faster You Shoot.

	public int TargetState = 1;
	public LayerMask WhatToFocusTarget;//Which Layers The Focus On Click Can Detect
	public LayerMask WhatToAutoTarget;//Which Layers The AutoAttack Can Detect

	GameObject _FocusedPoint;//The Target Circle Object
//	[HideInInspector]
	public GameObject FocusTarget;//Focused Target
	public GameObject FocusTargetPoint;//The Target Circle Prefab Is Attached Here
	public GameObject Bullet;//The Bullet Which Shall Be Spawned On Player Fire.

	public Transform AttackTarget;//Target That Im Attacking

	float _ObjectCloser = 0;
	RaycastHit2D[] _ObjectsHit;

	float _SavedTime = 0;//TIme Until Player Can Fire Again
public	int _DmgCharged = 0;//Player Dmg.


	public void DmgGained(int gain){
		if (_DmgCharged + gain > PlayerMaximumDamage) {
			_DmgCharged = PlayerMaximumDamage;
		} else {
			_DmgCharged += gain;
		}
	}

	public void DmgUsed(int used){
		if (_DmgCharged - used < 0) {
			_DmgCharged = 0;
		} else {
			_DmgCharged -= used;
		}
	}


	void Start(){
		_FocusedPoint = Instantiate (FocusTargetPoint, transform.position + (Vector3.back * 5), Quaternion.identity, transform).gameObject;
		_FocusedPoint.GetComponent<FocusedPoint> ().myParent = this;
		_FocusedPoint.SetActive(false);
	}


	void Update () {

		if (Input.GetKeyDown (KeyCode.Mouse2) || Input.GetKeyDown (KeyCode.LeftControl)) {//Middle Mouse Button Or Left Control To Change TargetState (Auto Attack On Close Targets, Only Attack FocusedTarget, AutoAttack But Prioritize FocusedTarget)
			if (TargetState == 2) {
				TargetState = 0;
			} else {
				TargetState++;
			}
		}


		if (Input.GetKeyDown (KeyCode.Mouse1)) {//Right Mouse Button Click. Search After Target On That Position
			_ObjectsHit = Physics2D.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition).origin, Vector3.forward, 1, WhatToFocusTarget);

			if (_ObjectsHit.Length > 0) {
				if (FocusTarget == _ObjectsHit [0].collider.gameObject) {
					FocusTarget = null;
					_FocusedPoint.SetActive (false);
				} else {
					FocusTarget = _ObjectsHit [0].collider.gameObject;
					_FocusedPoint.SetActive (true);
				}
			}
		}

		if (_SavedTime < ClockTest.TheTimes && _DmgCharged > 0) {
			_ObjectCloser = 100;

			if (TargetState == 0) {//Auto Closest
				AttackTarget = null; 

				_ObjectsHit = Physics2D.CircleCastAll (transform.position, AttackRange, Vector2.zero, 0, WhatToAutoTarget);

				for (int i = 0; i < _ObjectsHit.Length; i++) {
					if (Vector3.Distance (_ObjectsHit [i].transform.position, transform.position) < _ObjectCloser) {
						_ObjectCloser = Vector3.Distance (_ObjectsHit [i].transform.position, transform.position);
						AttackTarget = _ObjectsHit [i].transform;
					}
				}

				if (AttackTarget != null) {
					_SavedTime = ClockTest.TheTimes + RateOfFire;
					Instantiate (Bullet, transform.position, Quaternion.identity).GetComponent<SentenceBullet> ().SetAttackTarget (AttackTarget);
					_DmgCharged--;
				}

			} else if (TargetState == 1) {//Auto Focus

				if (FocusTarget != null) {
					if (Vector3.Distance (FocusTarget.transform.position, transform.position) <= AttackRange) {
						AttackTarget = FocusTarget.transform;
						_SavedTime = ClockTest.TheTimes + RateOfFire;
						Instantiate (Bullet, transform.position, Quaternion.identity).GetComponent<SentenceBullet> ().SetAttackTarget (AttackTarget);
						_DmgCharged--;
					} else {
						AttackTarget = null; 
						_ObjectsHit = Physics2D.CircleCastAll (transform.position, AttackRange, Vector2.zero, 0, WhatToAutoTarget);

						for (int i = 0; i < _ObjectsHit.Length; i++) {
							if (Vector3.Distance (_ObjectsHit [i].transform.position, transform.position) < _ObjectCloser) {
								_ObjectCloser = Vector3.Distance (_ObjectsHit [i].transform.position, transform.position);
								AttackTarget = _ObjectsHit [i].transform;
							}
						}

						if (AttackTarget != null) {
							_SavedTime = ClockTest.TheTimes + RateOfFire;
							Instantiate (Bullet, transform.position, Quaternion.identity).GetComponent<SentenceBullet> ().SetAttackTarget (AttackTarget);
							_DmgCharged--;
						}
					
					}
				} else {
					AttackTarget = null; 
					_ObjectsHit = Physics2D.CircleCastAll (transform.position, AttackRange, Vector2.zero, 0, WhatToAutoTarget);

					for (int i = 0; i < _ObjectsHit.Length; i++) {
						if (Vector3.Distance (_ObjectsHit [i].transform.position, transform.position) < _ObjectCloser) {
							_ObjectCloser = Vector3.Distance (_ObjectsHit [i].transform.position, transform.position);
							AttackTarget = _ObjectsHit [i].transform;
						}
					}

					if (AttackTarget != null) {
						_SavedTime = ClockTest.TheTimes + RateOfFire;
						Instantiate (Bullet, transform.position, Quaternion.identity).GetComponent<SentenceBullet> ().SetAttackTarget (AttackTarget);
						_DmgCharged--;
					}
			
				}

			} else if (TargetState == 2) {//Focus
		
				if (FocusTarget != null) {
					if (Vector3.Distance (FocusTarget.transform.position, transform.position) <= AttackRange) {
						AttackTarget = FocusTarget.transform;
						_SavedTime = ClockTest.TheTimes + RateOfFire;
						Instantiate (Bullet, transform.position, Quaternion.identity).GetComponent<SentenceBullet> ().SetAttackTarget (AttackTarget);
						_DmgCharged--;
					}
				}
			}
		}
	}
}
