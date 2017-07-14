using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviourDefault : MonoBehaviour {

/*	public enum HowToBehaveVector { FollowTarget = 0, LockDirection = 1, ChangeDirection = 2 }
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
	}*/
	//	public BulletInfo[] BulletBehaviour;




	public float Damage;
	public float Speed;
	[HideInInspector] public Transform TheTarget;
	[HideInInspector] public GameObject ImTheShooter;

//	public BulletInfo tesTs;

	[HideInInspector] public int AnimatorControllerParameterDone = Animator.StringToHash ("Done");
//	int BulletIndex = 0;
	  
	FSM_DefaultBehavoirV2.SpellAttackInfo Modifyers;

	public void SetDmgModifiers(FSM_DefaultBehavoirV2.SpellAttackInfo modifyers, Transform theTarget, GameObject Sender){
		Modifyers = modifyers;
		TheTarget = theTarget;
		Modifyers.AttackDmg = Modifyers.AttackDmg * 1;//got an "warning" just did this to stop seeing it
		ImTheShooter = Sender;
	}



}