using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveatAtTimeInterval : MonoBehaviour {

	public float Times = 0;
	public int Children = 0;
	public float counter = 0;
	public int child = 0;
	// Update is called once per frame

	void Update () {
		counter += Time.deltaTime;

		if (counter > Times) {
			counter = 0;
			transform.GetChild (child++).transform.gameObject.SetActive (true);
			if (child >= Children) {
				this.enabled = false;
			}
		}

	}
}
