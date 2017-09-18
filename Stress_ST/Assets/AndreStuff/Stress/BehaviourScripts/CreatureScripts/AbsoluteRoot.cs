using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsoluteRoot : MonoBehaviour {

	public virtual void HealthChange(int _Damage){}
	public virtual void M_SpeedChange(float _Mspeed){}
	public virtual void A_SpeedChange(float _Aspeed){}
	public virtual void OnDestroyed(){}




	public virtual void RecievedDmg (int _damage) {}
	public virtual void ChangeMovementAdd(float a) {}
	public virtual void GotTheKill(int a) {}

}
