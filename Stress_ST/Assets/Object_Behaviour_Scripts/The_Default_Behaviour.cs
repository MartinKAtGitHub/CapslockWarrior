using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class The_Default_Behaviour : MonoBehaviour {

	public enum ResetState { ResetOnEnter = 0, ResetOnPhaseChange = 1, ResetWhenComplete = 2, Never = 3 }
	[Tooltip("Reset When Complete Happens Automatically, But If You Only Want It To Reset There Choose That")]
	public ResetState TheResetState;
	protected The_Object_Behaviour _MyObject;

	/// <summary>
	/// This Is Called In Start(){} When The Object Is Created
	/// </summary>
	public virtual void SetMethod (The_Object_Behaviour myTransform){}

	/// <summary>
	/// This Is Called When The Object Enters The Behaviour Of The Phase
	/// </summary>
	public virtual void OnEnter(){}

	/// <summary>
	/// This Is The Behaviours Update
	/// </summary>
	public virtual void BehaviourUpdate(){}

	public virtual void Reset (){}

	/// <summary>
	/// 0 == Base.GetBool() == false, 1 == The Script I The Middle, 2 == Outer Script (With Spesific Logic)
	/// </summary>
	public virtual bool GetBool(int index){return false;}

	/// <summary>
	/// 0 == Bast.GetLayerMask(), 1 == Outer Script
	/// </summary>
	public virtual LayerMask[] GetLayerMask(int index){return null;}
	public virtual void SetCollision (Collision2D coll){}

	public virtual int GetInt (int index){return 0;}

}
