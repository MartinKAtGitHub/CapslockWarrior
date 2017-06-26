using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xSheepTest : MonoBehaviour {

	bool way = false;
	public float movevalue = 1;
	public bool sprint = false;
	Animator myanim;
	AnimatorControllerParameter test ;
	xSheepTest2 animatorscript;

	// Use this for initialization
	void Start () {
		myanim = GetComponent<Animator> ();
		animatorscript = myanim.GetBehaviour<xSheepTest2> ();
		animatorscript.Animationfinished = true;
		myanim.SetFloat("AnimatorStage", 0);
		movevalue = 1;
	}

	bool walk;

	// Update is called once per frame
	void Update () {


		if (sprint == true) {
			sprint = false;
			movevalue *= 6;
			myanim.SetFloat("AnimatorStage", 1);
		}

		if (animatorscript.Animationfinished == true) {
			if (turnafterhit == true) {
				myanim.SetFloat("AnimatorStage", 0);
				turnafterhit = false;
				GetComponent<SpriteRenderer> ().flipX = !GetComponent<SpriteRenderer> ().flipX;
			}
			transform.position += Vector3.left * Time.deltaTime * (movevalue / 2);
		}
	}
	bool turnafterhit = false;

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Wall") {
			way = !way;

			myanim.SetFloat("AnimatorStage", 2);

			animatorscript.Animationfinished = false;
			turnafterhit = true;

			if (way == false)
				movevalue = 1;
			else
				movevalue = -1;
		}
	}
}
