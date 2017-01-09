using UnityEngine;
using System.Collections;

public class movelefttest : MonoBehaviour {

	Rigidbody2D myRigid;
	Vector3 speed = new Vector3(1,0,0);
	byte change = 1;

	void Start () {
		myRigid = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		change = 1;
	}

	void Update(){
		if (change == 1) {
			change = 0;
		} else {

			myRigid.velocity = speed;

		}
	}
}
