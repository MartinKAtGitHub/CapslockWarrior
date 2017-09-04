using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTestingWhileWaiting  {

	public enum VectorDirection { StraightToTraget = 0, LockVector = 1, LockAtTarget = 2, Nothing = 3}


	[System.Serializable]
	public struct SpellAttackInfo{
		public GameObject Bullets;
		public float MovementMultiplyer;
		public float DamageMultiplyer;
		public float RangeMultiplyer;
		public float Size;
		public Vector3 AttackPosition;
	}

}
