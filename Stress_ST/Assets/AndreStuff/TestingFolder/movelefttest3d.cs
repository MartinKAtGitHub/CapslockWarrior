using UnityEngine;
using System.Collections;

public class movelefttest3d : MonoBehaviour {

	Vector3 speed = new Vector3(1,0,0);
	Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = transform;
	}


	// Update is called once per frame
	byte change = 1;

	void FixedUpdate () {
		change = 1;
	}
	void Update(){
		if (change == 1) {
			change = 0;
		} else {
	
			myTransform.position = Vector3.MoveTowards (myTransform.position, myTransform.position + speed, 0.1f);
		
		}
	}
}
