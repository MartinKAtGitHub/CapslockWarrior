using UnityEngine;
using System.Collections;

public class RunAnimationIfPlayerClose : MonoBehaviour {

	public Transform Player;
	public Transform me;

	float distance = 5f;
	// Use this for initialization
	void Start () {
		me = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (distance <= Vector3.Distance (Player.position, me.position)) {
			GetComponent<Animator> ().SetInteger ("Change", 2);
		} else {
			GetComponent<Animator> ().SetInteger ("Change", 1);
		}
	}
}
