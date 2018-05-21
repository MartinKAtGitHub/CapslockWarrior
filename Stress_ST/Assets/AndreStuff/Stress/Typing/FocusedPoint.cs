using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusedPoint : MonoBehaviour {

	[HideInInspector]
	public PlayerAttack myParent;

	
	// Update is called once per frame
	void Update () {
		if (myParent.FocusTarget != null) {
			transform.position = myParent.FocusTarget.transform.position + (Vector3.back * 5);
		}else{
			gameObject.SetActive (false);
		}
	}
}