using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_BulletBehaviour : MonoBehaviour {


	FSM_DefaultBehavoirV2.SpellAttackInfo Modifyers;

	public enum HowToBehave { FollowTarget = 0, GoStraight = 1, StandStill = 2 }
	public HowToBehave BulletBehaviour;

	public delegate void FunctionPointerBullets();
	FunctionPointerBullets[] FunctionPointerBullet = new FunctionPointerBullets[3];
	Vector3 target = Vector3.zero;

	int BulletIndex = 0;
	public bool DestroyOnHit = true;

	public void SetDmgModifiers(FSM_DefaultBehavoirV2.SpellAttackInfo modifyers, Transform theTarget){
		Modifyers = modifyers;
		TheTarget = theTarget;
	}

	// Use this for initialization
	void Awake () {
		if (BulletBehaviour == HowToBehave.FollowTarget) {
			FunctionPointerBullet [0] = FollowTheTarget;
			BulletIndex = 0;
		} else if (BulletBehaviour == HowToBehave.GoStraight) {
			FunctionPointerBullet [1] = GoingStraight;
			BulletIndex = 1;
		} else if (BulletBehaviour == HowToBehave.StandStill) {
			FunctionPointerBullet [2] = StandStill;
			BulletIndex = 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
	//	FunctionPointerBullet [BulletIndex] ();
	}

	void FollowTheTarget(){
		transform.position += (transform.position - target).normalized * Modifyers.AttackMovement * Time.deltaTime;
	}

	void GoingStraight(){
		transform.position += Modifyers.NeededVectors[0] * Modifyers.AttackMovement * Time.deltaTime;
	}

	void StandStill(){
	//	transform.position += new Vector3 (0, 0.01f, 0);
	}




	//-----------------------------

	[HideInInspector] public GameObject ImTheShooter;

	[HideInInspector] public Vector3 _MyShootingDirection;
	[HideInInspector] public const string Wall = "Wall";

	public Rigidbody2D MyRigidbody2D;
	public float BulletSpeed;


//	public abstract void SetObjectDirection (GameObject sender, Transform target);

	public bool FollowWhenCreating = true;
	public bool StartMoving = false;

	Transform TheTarget;
	ShootingAfterAnimation ShootingAnimation;
	private Vector3 _direction = Vector3.zero;

	// Use this for initialization
	void Start () {
		if (name != "MoleBullet(Clone)") {
		MyRigidbody2D = GetComponent<Rigidbody2D> ();
		ShootingAnimation = GetComponent<Animator> ().GetBehaviour<ShootingAfterAnimation> ();
		ShootingAnimation.ShootingAnimationFinished = false;
		}
	}
	void FixedUpdate () {
		if (name != "MoleBullet(Clone)") {

			if (StartMoving == true) {
				MyRigidbody2D.velocity = _MyShootingDirection * BulletSpeed;
			} else {
				if (FollowWhenCreating == true) {

					_MyShootingDirection = TheTarget.position - transform.position;
					_direction.z = Vector3.Angle (Vector3.right, _MyShootingDirection);

					if (_MyShootingDirection.y < 0) {
						_direction.z = _direction.z * -1;
					}  
					transform.rotation = Quaternion.Euler (_direction);

					if (ShootingAnimation.ShootingAnimationFinished == true) {
						_MyShootingDirection = _MyShootingDirection.normalized;
						StartMoving = true;
					}
				}
			}
		}
	}


	public void SetObjectDirection(GameObject sender, Transform target){
		TheTarget = target;
		ImTheShooter = sender;

	}

}

