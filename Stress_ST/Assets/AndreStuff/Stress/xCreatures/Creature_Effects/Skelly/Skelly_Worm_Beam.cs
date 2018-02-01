using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_Worm_Beam : The_Default_Bullet {

	public float TimeToSpin = 1;
	float StartTime = 0;
	ClockTest TheTimer;

	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);
		TheTimer = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ClockTest> ();
		StartTime = ClockTest.TheTime[0];
		//	EggHealth = new EnemyWordChecker(MyText, this);

	/*	if (_Shooter.MyAnimator.transform.eulerAngles.y == 0) {
			transform.position = (Quaternion.AngleAxis (_Shooter.MyAnimator.transform.eulerAngles.z, Vector3.forward) * _SpellInfo.AttackPosition) + MySender._MyTransform.position;
		} else {
			transform.position = (Quaternion.AngleAxis (_Shooter.MyAnimator.transform.eulerAngles.z, Vector3.back) * _SpellInfo.AttackPosition) + MySender._MyTransform.position;
		}
		transform.rotation = Quaternion.Euler (_Shooter.MyAnimator.transform.eulerAngles);*/
	}



	[SerializeField]  Transform _RotatingLazerBeam;
	[SerializeField]  Animator _LazerBeamAnimator;
	[SerializeField]  SpriteRenderer _LazerBeamSpriteRenderer;
	[SerializeField]  SpriteRenderer _SortingLayer_SR;

	[SerializeField] LayerMask _WhatCanIHit;

	[SerializeField] bool _RandomDirection = false;
	[SerializeField] float _LazerBeamLength = 1;
	[SerializeField] float _RotatingSpeed = 0;
	[SerializeField] float _OffsetValue = 2;
	[SerializeField]  Vector3 _RotationVector = Vector3.right;

	float _RotatingValue = 0f;
	Vector2 _SpriteSize = Vector2.zero;
	RaycastHit2D[] _RaycastValues;

	int[] _AnimatorVariables;

	void Start () {
		_AnimatorVariables = _LazerBeamAnimator.GetComponent<Skelly_Worm_LazerBeam_AnimatorParameters> ().AnimatorValue;
		_RotatingValue += 0;//TODO To Remove Warning

		_SpriteSize.y = _LazerBeamSpriteRenderer.size.y;
		_SpriteSize.x = _LazerBeamLength;

		if (_RandomDirection == true) {
			_RotationVector = Quaternion.Euler (0, 0, Random.Range (0, 361)) * (Vector3.right * _SpriteSize.x);
		} else {
			if (_RotationVector == Vector3.zero) {//If True Then I Will Target The Target; 
				_RotationVector = Vector3.right;//TODO Target The Target
			}
		}

	}



	void FixedUpdate () {

		if (_LazerBeamAnimator.GetInteger (_AnimatorVariables [0]) == 2) {
		
			if (_LazerBeamAnimator.GetBool (_AnimatorVariables [2]) == true) {
				Destroy (this.gameObject);
			}

		
		} else {

			if (StartTime + (TimeToSpin * _Shooter._TheObject.AttackStrength) < ClockTest.TheTime[0]) {
				_LazerBeamAnimator.SetInteger (_AnimatorVariables [0], 2);
				_Shooter.MyAnimator.SetInteger (_Shooter.MyAnimator.GetComponent<TheAnimator> ().AnimatorVariables[1], 2);

			} else {

				if (_Shooter._TheObject == null) {
					Destroy (this.gameObject);
				} else {
					if(_Shooter.MyAnimator.GetInteger(_Shooter.AnimatorVariables[1]) >= 2){
						_LazerBeamAnimator.SetInteger (_AnimatorVariables [0], 2);
						return;
					}
				}


				_RotationVector = (Quaternion.Euler (0, 0, (_RotatingValue = _RotatingSpeed * Time.deltaTime)) * _RotationVector).normalized;//Getting Rotation Vector

				if (_RotationVector.y < 0) {//Setting Rotation
					_RotatingLazerBeam.localRotation = Quaternion.Euler (0, 0, 360 - Vector3.Angle (Vector3.right, _RotationVector));
				} else {
					_RotatingLazerBeam.localRotation = Quaternion.Euler (0, 0, Vector3.Angle (Vector3.right, _RotationVector));
				}


				if (_LazerBeamAnimator.GetBool (_AnimatorVariables [1]) == true) {
					_RaycastValues = Physics2D.LinecastAll (transform.position, (transform.position + (_RotationVector * _LazerBeamLength)) - (_RotationVector / _OffsetValue), _WhatCanIHit);//Getting Targets Hit By The Beam (Currently LineCast, Might Change To RectCast Late TODO)
	
					if (_RaycastValues.Length > 0) {//Adjusting Length Of Beam Depending On What Objects Go Hit
						_LazerBeamAnimator.SetInteger (_AnimatorVariables [0], 1);

						for (int i = 0; i < _RaycastValues.Length; i++) {
							if (_RaycastValues [i].transform.gameObject.layer == 13 || _RaycastValues [i].transform.gameObject.layer == 12) {
								_SpriteSize.x = Vector3.Distance (transform.position, ((Vector3)_RaycastValues [i].point) + (_RotationVector / (_OffsetValue - 6)));// - 6 Makes It So That The End Point Will Go Alittle Bit More Forward
								break;//Dont Want To Deal Dmg To Objects Behind The Wall
							} else {//TODO Improve
								if (_RaycastValues [i].transform.CompareTag ("Player1")) {
									_RaycastValues [i].transform.GetComponent<PlayerManager> ().RecievedDmg (1);
								} else {
									Debug.LogWarning ("The Object Didnt Take Dmg??? " + _RaycastValues [i].transform.name);
								}
							}
						}
					} else {
						_SpriteSize.x = _LazerBeamLength;
						_LazerBeamAnimator.SetInteger (_AnimatorVariables [0], 0);
					}
				} else {
					_SpriteSize.x = _LazerBeamLength;
					_LazerBeamAnimator.SetInteger (_AnimatorVariables [0], 0);
				}

				_LazerBeamSpriteRenderer.size = _SpriteSize;
	
				_SortingLayer_SR.transform.localPosition = (_RotationVector * _SpriteSize.x) - (_RotationVector / _OffsetValue);//This is The Collision Point, Which Is The One Im Getting The "Correct" SortingOrder For The Beam
				_LazerBeamSpriteRenderer.sortingOrder = _SortingLayer_SR.sortingOrder;

				_RotatingLazerBeam.localPosition = (_RotationVector * _SpriteSize.x / 2) - (_RotationVector / _OffsetValue);//Setting The Position For The Beam
			}
		}
	
	}


}
