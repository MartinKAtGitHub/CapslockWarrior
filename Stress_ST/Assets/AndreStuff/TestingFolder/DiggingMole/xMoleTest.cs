using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xMoleTest : MonoBehaviour {

	public float movevalue = 1;
	Animator myanim;
	AnimatorControllerParameter test ;
	xSheepTest2 animatorscript;
	public GameObject target;
	public GameObject spike;
	// Use this for initialization
	void Start () {
		myanim = GetComponent<Animator> ();
		animatorscript = myanim.GetBehaviour<xSheepTest2> ();
		animatorscript.Animationfinished = false;
		movevalue = -1;

		movevect = (transform.position - target.transform.position).normalized;
		lastdist = startDist;
	}

	float dist = 0;
	Vector3 movevect = Vector3.zero;
	float startDist = 3;
	float distinbetween = 0.49f;
	float lastdist = 0;

	void Update (){
		dist = Vector3.Distance (target.transform.position, transform.position);

		if (dist < lastdist) {
			lastdist -= distinbetween;
			Instantiate (spike, transform.position, Quaternion.identity);
		}

		if (animatorscript.Animationfinished == true) {
			transform.position += movevect * Time.deltaTime * movevalue;
		}
	}
}
