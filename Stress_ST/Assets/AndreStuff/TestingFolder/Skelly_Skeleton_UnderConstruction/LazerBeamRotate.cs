using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeamRotate : MonoBehaviour {
	
	public bool RandomDirection = false;
	public float StartDistance = 1;
	public float RotatingSpeed = 0;
	public Vector3 StartVector = Vector3.right;
	public LayerMask WhatCanIHit;
	public GameObject EndOfBeam;
	public SpriteRenderer EndPoint_SR;

	float RotatingValue = 0f;
	SpriteRenderer MySpriterenderer;
	bool Changed = false;
	Vector2 SpriteSize = Vector2.zero;
	RaycastHit2D[] RaycastValues;
	public Animator MyAnimator;

	void Start () {
		RotatingValue = 1;
		RotatingValue = RotatingValue * 2;
		MySpriterenderer = GetComponent<SpriteRenderer> ();
		SpriteSize.y = MySpriterenderer.size.y;
		SpriteSize.x = StartDistance;

		if (RandomDirection == true) {
			StartVector = Quaternion.Euler (0, 0, Random.Range (0, 361)) * (Vector3.right * SpriteSize.x);
		} else {
			if (StartVector == Vector3.zero) {//if the startvector == 0,0,0 then im getting the position of the target
				//	StartVector = (target.position - transform.parent.position).normalized * SpriteSize.x;
				StartVector = Vector3.right * SpriteSize.x;
			}
		}
	}
	public bool test = true;
	public RuntimeAnimatorController testi1;
	public RuntimeAnimatorController testi2;

	void Update () {

	
		StartVector = (Quaternion.Euler(0,0, (RotatingValue = RotatingSpeed * Time.deltaTime)) * StartVector).normalized * SpriteSize.x;
		MySpriterenderer.sortingOrder = EndPoint_SR.sortingOrder - 1;

		if (StartVector.y < 0) {
			transform.rotation = Quaternion.Euler (0, 0, 360 - Vector3.Angle (Vector3.right, StartVector));
		} else {
			transform.rotation = Quaternion.Euler (0, 0, Vector3.Angle (Vector3.right, StartVector));
		}

		RaycastValues = Physics2D.LinecastAll (transform.parent.position, transform.parent.position + (StartVector.normalized * StartDistance), WhatCanIHit);
		if (RaycastValues.Length > 0) {

			if (Changed == true) {
				if (test == true) {
					
					EndPoint_SR.enabled = true;
				} else {
					MyAnimator.runtimeAnimatorController = testi2;
				}
				Changed = false;
			}

			SpriteSize.x = Vector3.Distance (transform.parent.position, ((Vector3)RaycastValues [0].point * Offset));
			MySpriterenderer.size = SpriteSize;
			StartVector = ((Vector3)RaycastValues [0].point - transform.parent.position).normalized * SpriteSize.x;
			EndPoint_SR.gameObject.transform.position = RaycastValues [0].point;

		} else {
			if (Changed == false) {
				Changed = true;
				SpriteSize.x = StartDistance;
				MySpriterenderer.size = SpriteSize;
				StartVector = StartVector.normalized * SpriteSize.x;
				if(test == true)
				EndPoint_SR.enabled = false;
				else
				MyAnimator.runtimeAnimatorController = testi1;
			}
		}

		transform.localPosition = StartVector / 2;

	}
	public float Offset = 0;
}
