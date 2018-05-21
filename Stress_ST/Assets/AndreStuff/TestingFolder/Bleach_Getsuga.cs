using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleach_Getsuga : The_Default_Bullet {

	Vector3 MyShootingDirection;
//	Vector3 _Direction = Vector3.zero;

//	Vector3 TargetVector = Vector3.zero;
//	bool KillMyself = false;

	public float DisableColliderAt = 2;
//	float theTime = 0;
	public GameObject TheCollider;
	public GameObject Particleeffect;
	


//	int theX = 0;
//	int theY = 0;
//	byte iterations = 0; 
	public int AnimatorStateCheck = 0;

	public LayerMask WhatNotToHit;

//	Vector3 test = Vector3.zero;
	RaycastHit2D[] hitted;
	public float TeleportDistance = 2;

//	int rngtries = 4;
//	bool foundIt = false;

	EnemyManaging _MyObject;
//	StressCommonlyUsedInfo.TheAbility[] teste = new StressCommonlyUsedInfo.TheAbility[1];
	Quaternion parentRotaitons;
	public LayerMask StopOnHit;

	public override void SetMethod (GameManagerTestingWhileWaiting.SpellAttackInfo SpellInfo, The_Object_Behaviour MySender){
		base.SetMethod (SpellInfo, MySender);

//		TargetVector = _Shooter.ObjectCurrentVector[0];

	}

	public override void SetMethod (CreatureRoot objectChecking){

	/*	_MyObject = objectChecking;
		parentRotaitons = _MyObject.transform.Find("GFX").rotation;

		for (int t = 0; t < _MyObject.MyAbilityInfo.tes.Count; t++) {//Goint Through The Transitions To Find This Spell Transition
			for (int g = 0; g < _MyObject.MyAbilityInfo.tes [t].AllAbilities.Length; g++) {//Going Through This Spells Transition To Find The Spell
				if (_MyObject.MyAbilityInfo.tes [t].AllAbilities [g].SpellRef.bulletID == bulletID) {//If SpellRef ID == This SpellID. Then This Is That Spell
					teste [0] = _MyObject.MyAbilityInfo.tes [t].AllAbilities [g];
				}
			}
		}






		foundIt = false;

		for (int i = 0; i < rngtries; i++) {
			test = (Quaternion.Euler (0, 0, Random.Range (1, rngtries + 1) * Random.Range (0, 90)) * (Vector3.right * TeleportDistance));//Deviding The Circle In 4, Then I Make Choose A RNG Side To Check

			hitted = Physics2D.LinecastAll (objectChecking.Targeting.MyMovementTarget.transform.position + test, objectChecking.Targeting.MyMovementTarget.transform.position, WhatNotToHit);
			if (hitted.Length > 0) {
				//		Debug.Log ("Hit Something " + hitted [hitted.Length - 1].point.x + " | " + hitted [hitted.Length - 1].point.y + " | " + test.normalized);
			} else {
				foundIt = true;
				//		Debug.Log ("No Wall Found");
				test = objectChecking.Targeting.MyMovementTarget.transform.position + test;
				i = rngtries;
			}

		}

		if (foundIt == false) {
			test = (Quaternion.Euler (0, 0, Random.Range (1, rngtries + 1) * Random.Range (0, 90)) * (Vector3.right * TeleportDistance));
			hitted = Physics2D.LinecastAll (objectChecking.Targeting.MyMovementTarget.transform.position + test, objectChecking.Targeting.MyMovementTarget.transform.position, WhatNotToHit);
			if (hitted.Length > 0) {
				hitted = Physics2D.LinecastAll (objectChecking.Targeting.MyMovementTarget.transform.position, (Vector3)hitted [hitted.Length - 1].point, WhatNotToHit);
				//	Debug.Log ( "Oposite Side Of Wall ");
				test = (Vector3)(hitted [0].point) - (test.normalized * 0.05f);

			} else {
				//	Debug.Log ("No New Wall Found");
				test = objectChecking.Targeting.MyMovementTarget.transform.position + test;
			}
		}

		objectChecking.transform.position = test;
		transform.position = objectChecking.transform.position + Quaternion.Euler (0, parentRotaitons.y, parentRotaitons.z) * teste [0].SpawnPosition;//Setting The Start Location

		_Direction.z = Vector3.Angle (Vector3.right, (objectChecking.Targeting.MyMovementTarget.transform.position - objectChecking.transform.position));

		if ((objectChecking.Targeting.MyMovementTarget.transform.position - objectChecking.transform.position).y < 0) {
			_Direction.z = _Direction.z * -1.0f;
		}  
		transform.rotation = Quaternion.Euler (_Direction);

		gameObject.SetActive(true);
		Particleeffect.GetComponent<MoveForwardFast> ().starting = true;


		hitted = Physics2D.LinecastAll (transform.position, ((objectChecking.Targeting.MyMovementTarget.transform.position - objectChecking.transform.position).normalized * teste[0].SpellVariables[1]), WhatNotToHit);

		hitted = Physics2D.BoxCastAll (transform.position, boxSize, _Direction.z,  ((Vector2)transform.position - hitted[0].point).normalized, 0, StopOnHit );
		Debug.Log ("HIT " + hitted.Length);*/

		//	Physics2D.BoxCastAll();
	
	//	_MyObject = GameObject.Find ("Otaku_Boss").GetComponent<EnemyManaging>();

		hitted = Physics2D.LinecastAll (transform.position, (Vector2)transform.position + (Vector2.right * 3), WhatNotToHit);
		hitted = Physics2D.BoxCastAll (transform.position, boxSize, 0,  Vector2.right, 0, StopOnHit );
	//	hitted = Physics2D.BoxCastAll (transform.position, boxSize, 0,  ((Vector2)transform.position - hitted[0].point).normalized, 0, StopOnHit );

		ShootingVector = objectChecking.GetWhatToTarget ().TargetVector - transform.position;//Vector from My Position To The Target
		transform.eulerAngles = (Quaternion.Euler(0,0,transform.eulerAngles.z) * Vector2.right);
	}


	Vector2 boxSize = new Vector2(5,0.125f);





//	float activeTime = 0.25f;

	void Start(){

/*	//	MyShootingDirection = TargetVector - transform.position;
		MyShootingDirection = _Shooter.ObjectTargetVector[0];
		_Direction.z = Vector3.Angle (Vector3.right, MyShootingDirection);

		if (MyShootingDirection.y < 0) {
			_Direction.z = _Direction.z * -1.0f;
		}  
		transform.rotation = Quaternion.Euler (_Direction);
	
		theTime = _Shooter.TheTime [0];
		Particleeffect.GetComponent<MoveForwardFast> ().starting = true;
		activeTime += _Shooter.TheTime [0];
		Particleeffect.GetComponent<MoveForwardFast> ().EndPoint = ShootingVector;*/
	/*	hitted = Physics2D.LinecastAll (transform.position, (Vector2)transform.position + (Vector2.right * 5), WhatNotToHit);
		for(int i = 0; i < hitted.Length; i++)
			Debug.Log ("wall" + hitted[i].point);
		if (hitted.Length > 0) {
		
			_Direction.z = Vector3.Angle (Vector3.right, (Vector2)transform.position, hitted[0].point);

			if (((Vector2)transform.position, hitted[0].point)).y < 0) {
				_Direction.z = _Direction.z * -1.0f;
			}  


			hitted = Physics2D.BoxCastAll (transform.position, new Vector2(Vector2.Distance((Vector2)transform.position, hitted[0].point), 0.5f), _Direction.z,  Vector2.right, 1, StopOnHit );
		}
	//	hitted = Physics2D.BoxCastAll (transform.position, new Vector2(hitted[0].point.x - transform.position.x, 0.125f), 0,  Vector2.right, 1, StopOnHit );

		for(int i = 0; i < hitted.Length; i++)
			Debug.Log ("object" + hitted[i].point);*/




	/*	ShootingVector = targetposiiont.position - transform.position;//This Is The Vector That The Ability Is Going To Have. This Is Updated Through The Animator->Animation->"Event"
		if (ShootingVector.y < 0) {
			_Directions.z = 180 + Vector3.Angle (Vector3.left, ShootingVector);
		}  else{
			_Directions.z = Vector3.Angle (Vector3.right, ShootingVector);
		}

		transform.eulerAngles = _Directions;
		hitted = Physics2D.LinecastAll (transform.position, (Vector2)(((ShootingVector - transform.position).normalized * attackDistance) + transform.position), StopOnHit);

		if (hitted.Length > 0) {
			Debug.Log ("wall here or after so stop at that point");
			ShootingVector = hitted [0].transform.position - transform.position; 
		//	attackDistance = Vector3.Distance (hitted [0].transform.position - transform.position, Vector3.zero);
		} else {
			ShootingVector = ShootingVector.normalized * attackDistance;
		}

		//Doing The BoxCast To Find The Point Where The Ability Need To Stop
		//	hitted = Physics2D.BoxCastAll (transform.position + (((Quaternion.AngleAxis (transform.eulerAngles.z, Vector3.forward) * Vector3.right).normalized * attackDistance) / 2),
		//		new Vector2(Vector3.Distance ((((Quaternion.AngleAxis (transform.eulerAngles.z, Vector3.forward) * Vector3.right).normalized * attackDistance)), 
		//			Vector3.zero), YDimention), transform.eulerAngles.z, Vector3.zero , 1, StopOnHit);

		effect = Particleeffect.GetComponent<MoveForwardFast> ();
		effect.EndPoint = ShootingVector;
		effect.starting = true;
		effect.gameObject.SetActive (true);

		hitted = Physics2D.BoxCastAll (transform.position + (ShootingVector / 2), new Vector2(Vector3.Distance (Vector3.zero, ShootingVector), YDimention), transform.eulerAngles.z, Vector3.zero , 1, WhatNotToHit);


		*/


		ShootingVector = targetposiiont.position - transform.position;//This Is The Vector That The Ability Is Going To Have. This Is Updated Through The Animator->Animation->"Event"


		if (ShootingVector.y < 0) {
			_Directions.z = 180 + Vector3.Angle (Vector3.left, ShootingVector);
		} else {
			_Directions.z = Vector3.Angle (Vector3.right, ShootingVector);
		}

		transform.eulerAngles = _Directions;
		hitted = Physics2D.LinecastAll (transform.position, (Vector2)(((ShootingVector - transform.position).normalized * attackDistance) + transform.position), StopOnHit);

		if (hitted.Length > 0) {
			Debug.Log ("wall here or after so stop at that point");
			ShootingVector = (Vector3)hitted [0].point - transform.position; 
		} else {
			Debug.Log ("hit nothing");
			ShootingVector = ShootingVector.normalized * attackDistance;
		}

		hitted = Physics2D.BoxCastAll (transform.position + (ShootingVector / 2), new Vector2 (Vector3.Distance (Vector3.zero, ShootingVector), YDimention), transform.eulerAngles.z, Vector3.zero, 1, WhatNotToHit);

		if (hitted.Length > 0) {
			for (int i = 0; i < hitted.Length; i++) {
				Debug.Log (hitted [i].transform.name);
			}
		}

		effect = Particleeffect.GetComponent<MoveForwardFast> ();
		effect.EndPoint = transform.position + ShootingVector;
		effect.starting = true;
		effect.gameObject.SetActive (true);

	}

	MoveForwardFast effect;
	public Vector3 ShootingVector = Vector3.zero;

	Vector3 startpos = Vector3.zero;
	Vector3 middlePos = Vector3.zero;


//	Vector3 newPos = Vector3.zero;
	public float XDimention = 2;
	public float YDimention = 1;

	public Vector3 _Directions = Vector3.zero;

	public float attackDistance = 1;


	public Transform targetposiiont;

	void FixedUpdate () {

		if (effect.starting == true) {
		/*	ShootingVector = targetposiiont.position - transform.position;//This Is The Vector That The Ability Is Going To Have. This Is Updated Through The Animator->Animation->"Event"
			if (ShootingVector.y < 0) {
				_Directions.z = 180 + Vector3.Angle (Vector3.left, ShootingVector);
			} else {
				_Directions.z = Vector3.Angle (Vector3.right, ShootingVector);
			}

			transform.eulerAngles = _Directions;
			hitted = Physics2D.LinecastAll (transform.position, (Vector2)(((ShootingVector - transform.position).normalized * attackDistance) + transform.position), StopOnHit);
	
			if (hitted.Length > 0) {
				ShootingVector = (Vector3)hitted [0].point - transform.position; 
			} else {
				ShootingVector = ShootingVector.normalized * attackDistance;
			}
			
			hitted = Physics2D.BoxCastAll (transform.position + (ShootingVector / 2), new Vector2 (Vector3.Distance (Vector3.zero, ShootingVector), YDimention), transform.eulerAngles.z, Vector3.zero, 1, WhatNotToHit);

			if (hitted.Length > 0) {
				for (int i = 0; i < hitted.Length; i++) {
					Debug.Log (hitted [i].transform.name);
				}
			}*/

		} else {
			Destroy (this.gameObject);
		}


		//		transform.localPosition = Quaternion.Euler (0, transform.parent.rotation.y, transform.parent.rotation.z) * test [0].SpawnPosition;//Setting The Start Location
		
		
		/*	if ((_MyObject.Targeting.MyMovementTarget.transform.position - transform.position).y < 0) {
			transform.rotation = Quaternion.Euler (0, 0, Vector3.Angle (Vector3.right, (_MyObject.Targeting.MyMovementTarget.transform.position - transform.position)) * -1);
		} else {
			transform.rotation = Quaternion.Euler (0, 0, Vector3.Angle (Vector3.right, (_MyObject.Targeting.MyMovementTarget.transform.position - transform.position)));
		}*/


	/*	hitted = Physics2D.BoxCastAll (transform.position, new Vector2(XDimention, YDimention), transform.eulerAngles.z, transform.position + (Quaternion.Euler(0,0,transform.eulerAngles.z) * Vector2.right) , 1, StopOnHit);





	
		hitted = Physics2D.BoxCastAll (transform.position, new Vector2(XDimention, YDimention), transform.eulerAngles.z, transform.position + (Quaternion.Euler(0,0,transform.eulerAngles.z) * Vector2.right) , 1, StopOnHit);
//		hitted = Physics2D.BoxCastAll (startpos, new Vector2(linesize, yWidth), transform.eulerAngles.z, transform.position + (Quaternion.Euler(0,0,transform.eulerAngles.z) * Vector2.right * linesize), 1, StopOnHit);
		//hitted = Physics2D.BoxCastAll (startpos, new Vector2(Vector2.Distance((Vector2)startpos, hitted[0].point), yWidth), _Direction.z,  (hitted[0].point - (Vector2)startpos), 1, StopOnHit );

		if(hitted.Length > 0)
		Debug.Log (startpos + " | " +  middlePos + " | " + hitted[0].point + " § " + (new Vector2(Vector2.Distance((Vector2)startpos, hitted[0].point), YDimention)));


		hitted = Physics2D.LinecastAll (transform.position, transform.position + (Quaternion.Euler(0,0,transform.eulerAngles.z) * Vector2.right * XDimention), WhatNotToHit);
		for (int i = 0; i < hitted.Length; i++) {
		//	Debug.Log ("wall" + hitted[i].point);
		}



		if (hitted.Length > 0) {



			_Direction.z = Vector3.Angle (Vector3.right, hitted[0].point - (Vector2)startpos);

			if ((hitted[0].point - (Vector2)startpos).y < 0) {
				_Direction.z = _Direction.z * -1.0f;
			}  

			middlePos = startpos + ((Vector3)(hitted [0].point - (Vector2)transform.position) / 2);


		//	transform.position = startpos + ((Vector3)(hitted[0].point - (Vector2)transform.position) / 2);
			hitted = Physics2D.BoxCastAll (startpos, new Vector2(Vector2.Distance((Vector2)startpos, hitted[0].point), YDimention), _Direction.z,  (hitted[0].point - (Vector2)startpos), 1, StopOnHit );
		
		//	Debug.Log (startpos + " | " +  middlePos + " | " + hitted[0].point + " § " + (new Vector2(Vector2.Distance((Vector2)startpos, hitted[0].point), yWidth)));

		}
		//	hitted = Physics2D.BoxCastAll (transform.position, new Vector2(hitted[0].point.x - transform.position.x, 0.125f), 0,  Vector2.right, 1, StopOnHit );

		for (int i = 0; i < hitted.Length; i++) {
		//	Debug.Log ("object" + hitted[i].point);
		}
	


		*/











	/*	if (_MyObject.MyAnimatorVariables.AnimatorStage == 1000) {
			gameObject.SetActive(true);
			Particleeffect.GetComponent<MoveForwardFast> ().starting = true;
		}*/

	/*	if (activeTime < _Shooter.TheTime [0]) {
			activeTime = activeTime + 100;
			transform.GetChild (1).gameObject.SetActive (false);
		}

		if (_Shooter._CreaturePhase > 3)//Quickfix need to look into this problem
			Destroy (gameObject);
		
		if (KillMyself == false) {
		
			if (theTime + DisableColliderAt < _Shooter.TheTime [0]) {
				KillMyself = true;
				theTime = _Shooter.TheTime [0];
				TheCollider.SetActive (false);
				Particleeffect.GetComponent<ParticleSystem> ().Stop (true);
			}

		} else {
		
			if (theTime + DisableColliderAt < _Shooter.TheTime [0]) {
			
				Destroy (gameObject);
			
			}
		}*/

	}

/*void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
	if (_Shooter._MyTransform.gameObject != col.gameObject) {
		if(col.CompareTag("Wall"))
			GameObject.Destroy (transform.gameObject);

			if (col.CompareTag ("Player1")) {
				Debug.Log ("SENDING DMG");
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			}
	}


}

void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
	if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if (col.gameObject.CompareTag ("Player1")) {
				Debug.Log ("SENDING DMG");
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt (_Shooter._TheObject.AttackStrength));
			}
	}


}
*/

}