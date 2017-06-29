using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_BulletBehaviour : MonoBehaviour {

	public enum HowToBehaveVector { FollowTarget = 0, LockDirection = 1, ChangeDirection = 2 }
	public enum HowToBehave { Stop = 3, SizeChangeGraduallyTime = 4, SpeedChangeGraduallyTime = 5, DetonateInTime = 6, DevideInTime = 7, WaitForAnimation = 10}//the curcher i go the faster i get, the further i go the bigger i get
	public enum OnCollisionBehaviour { Despawn = 8, Detonate = 6, Devide = 7, ChangeSize = 4, ChangeSpeed = 5, ChangeDirection = 2, ContinueAhead = 9 }//the curcher i go the faster i get, the further i go the bigger i get
	public delegate void FunctionPointerBullets();
	FunctionPointerBullets[] FunctionPointerBullet = new FunctionPointerBullets[10];

	public enum DevidingBehaviour { RandomVector = 0, ChosenVector = 1, RandomPosition = 2, ChosenPosition = 3 }//the curcher i go the faster i get, the further i go the bigger i get

	[System.Serializable]
	public struct BulletChildrenInfo{
		public DevidingBehaviour ChildrenBehaviour;

		public GameObject[] Spells;
		public Vector3[] VectorPoints;
	}

	[System.Serializable]
	public struct BulletInfo{
		public HowToBehaveVector TheVector;
		public int TheVectorIndex;
		public HowToBehave TheMovement;
		public int TheMovementIndex;
		public OnCollisionBehaviour TheCollision;
		public int TheCollisionIndex;

		public BulletChildrenInfo DevidedBullets;
	}

	public float Speed;
	public float Damage;
	public int HowManyTimeCanICollide;
	public BulletInfo[] BulletBehaviour;
	public Vector3 MovementVector;

	FSM_DefaultBehavoirV2.SpellAttackInfo Modifyers;



	Vector3 target = Vector3.zero;
	public BulletInfo tesTs;

	int AnimatorControllerParameterStop = Animator.StringToHash ("Done");


	int BulletIndex = 0;
	public bool DestroyOnHit = true;

	public void SetDmgModifiers(FSM_DefaultBehavoirV2.SpellAttackInfo modifyers, Transform theTarget){
		Modifyers = modifyers;
		TheTarget = theTarget;
	}

	// Use this for initialization
	void Awake () {
	/*	if (BulletBehaviour == HowToBehave.FollowTarget) {
			FunctionPointerBullet [0] = FollowTheTarget;
			BulletIndex = 0;
		} else if (BulletBehaviour == HowToBehave.GoStraight) {
			FunctionPointerBullet [1] = GoingStraight;
			BulletIndex = 1;
		} else if (BulletBehaviour == HowToBehave.StandStill) {
			FunctionPointerBullet [2] = StandStill;
			BulletIndex = 2;
		}*/
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

