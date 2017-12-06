using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movevector : MonoBehaviour {
	public float speed = 1;
	public Vector3 MoveVector = Vector3.zero;


	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (Mathf.Sin (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)) * -1, Mathf.Cos (Mathf.Deg2Rad * (transform.eulerAngles.z + 180)), 0) * (speed * Time.deltaTime);
	}
}
