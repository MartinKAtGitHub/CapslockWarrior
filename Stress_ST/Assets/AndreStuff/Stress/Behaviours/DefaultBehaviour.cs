using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class DefaultBehaviour : MonoBehaviour {

	[HideInInspector] public bool FreezeCharacter = false;

	public int[] HealthPoints = new int[1];
	public float[] Damage= new float[1];
	public float[] MovementSpeed = new float[1];

	public abstract void OnDestroyed ();
	public abstract void AttackTarget ();
	public abstract void RecievedDmg ();
	public abstract void ChangeMovementAdd(float a);

}
