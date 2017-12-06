using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouly_Spike : The_Default_Bullet {

	void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if (col.gameObject.GetComponent<AbsoluteRoot> () != null) {
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt(_Shooter._TheObject.AttackStrength));
				GameObject.Destroy (transform.gameObject);
			}else {
				GameObject.Destroy (transform.gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
		if (_Shooter._MyTransform.gameObject != col.gameObject) {
			if (col.gameObject.GetComponent<AbsoluteRoot> () != null) {
				col.gameObject.GetComponent<AbsoluteRoot> ().RecievedDmg (Mathf.FloorToInt(_Shooter._TheObject.AttackStrength));
				GameObject.Destroy (transform.gameObject);
			}else {
				GameObject.Destroy (transform.gameObject);
			}
		}
	}

}
