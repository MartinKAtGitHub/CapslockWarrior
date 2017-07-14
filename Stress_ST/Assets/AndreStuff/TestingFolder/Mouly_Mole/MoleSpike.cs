﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpike : BulletBehaviourDefault {


	void OnTriggerEnter2D(Collider2D col){//objects without rigidbody and box2d ontrigger true
		if (ImTheShooter != col.gameObject) {
			if (col.gameObject.GetComponent<CreatureBehaviourUpdate> () != null) {
				col.gameObject.GetComponent<CreatureBehaviourUpdate> ().ObjectGotHurt (Damage);
				GameObject.Destroy (transform.gameObject);
			}else {
				GameObject.Destroy (transform.gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){//objects with rigidbody and box2d ontrigger false
		if (ImTheShooter != col.gameObject) {
			if (col.gameObject.GetComponent<CreatureBehaviourUpdate> () != null) {
				col.gameObject.GetComponent<CreatureBehaviourUpdate> ().ObjectGotHurt (Damage);
				GameObject.Destroy (transform.gameObject);
			}else {
				GameObject.Destroy (transform.gameObject);
			}
		}
	}
}
