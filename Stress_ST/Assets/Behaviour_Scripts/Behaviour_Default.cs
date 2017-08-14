using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class Behaviour_Default : MonoBehaviour {

	//public bool CompleteBehaviourToReset = false;

	/// <summary>
	/// This is Called In Start() For All Behaviour In All Phases
	/// </summary>
	public virtual void SetMethod (Object_Behaviour myTransform, Transform targetTransform, int[] AnimatorValues){}
	/// <summary>
	/// Every Time The Object Changes Phase This Is Called For Every Behaviour In That Phase
	/// </summary>
	public virtual void OnSetup (){}	
	/// <summary>
	/// This Is Called Once Every Time The Object Enters A New Behaviour.
	/// </summary>
	public virtual void OnEnter(){}
	/// <summary>
	/// This Is The Update() 
	/// </summary>
	public virtual void BehaviourMethod (){}
	public virtual void Reset (){}

	public virtual int GetInt (int index){return 0;}
	public virtual float GetFloat (int index){return 0;}
	public virtual bool GetBool (int index){return false;}
	public virtual Vector3 GetVector (int index){return Vector3.zero;}
	public virtual LayerMask[] GetLayerMask (){return null;}
	public virtual void SetCollision (Collision2D coll){}

}