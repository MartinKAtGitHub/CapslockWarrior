using UnityEngine;
using System.Collections;

public class WalkingScript : MonoBehaviour {
	public float walkingSpeed = 50;
	//Really simple walking script

	void Update() {

		if (Input.GetKey("up")) {
	//		GetComponent<Animator>().SetInteger("ChangeAnimation", 3);
			transform.position = new Vector3(transform.position.x, transform.position.y + (Time.smoothDeltaTime * walkingSpeed), transform.position.z);
		}

		if (Input.GetKey("down")) {
	//		GetComponent<Animator>().SetInteger("ChangeAnimation", 0);
			transform.position = new Vector3(transform.position.x, transform.position.y - (Time.smoothDeltaTime * walkingSpeed), transform.position.z);
		}

		if (Input.GetKey("left")) {
	//		GetComponent<Animator>().SetInteger("ChangeAnimation", 2);
			transform.position = new Vector3(transform.position.x - (Time.smoothDeltaTime * walkingSpeed) , transform.position.y, transform.position.z);
		}

		if (Input.GetKey("right")) {
	//		GetComponent<Animator>().SetInteger("ChangeAnimation", 1);
			transform.position = new Vector3(transform.position.x + (Time.smoothDeltaTime * walkingSpeed), transform.position.y, transform.position.z);
		}
	}
}
