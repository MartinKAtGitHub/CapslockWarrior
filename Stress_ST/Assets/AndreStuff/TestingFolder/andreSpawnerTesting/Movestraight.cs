using UnityEngine;
using System.Collections;

public class Movestraight : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + (Vector3.left * Time.deltaTime);
		if (transform.position.x < -3)
			Destroy (this.gameObject);
	}
}

