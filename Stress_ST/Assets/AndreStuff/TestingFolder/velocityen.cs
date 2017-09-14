using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocityen : MonoBehaviour {

	Rigidbody2D parentRigid2d;
	public float veloci = 1;
	void Start () {
		parentRigid2d = transform.parent.GetComponent<Rigidbody2D> ();
	}
	

	void FixedUpdate () {
		parentRigid2d.velocity = Vector2.right * veloci;
	}
}
