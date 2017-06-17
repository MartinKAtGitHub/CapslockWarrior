using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xMoleTest : MonoBehaviour {

	int AnimatorControllerParameter = Animator.StringToHash ("Stop");


	public float DistanceToStartAttacking = 3;
	public float UnderGroundTime = 10;
	public float UnderGroundAttackingTime = 6;
	public float AttackSpeedTime = 0.4f;

	// Use this for initialization

	void Start () {
		MyAnim = GetComponent<Animator> ();
	}
	Animator MyAnim;
	Vector3 MovementVector = Vector3.zero;

	bool CheckAndUpdate = true;
	bool AmIAttacking = false;

	float DistanceFromPlayer = 0;
	float Clock = 0;
	float WalkSpeed = 1;
	float AttackSpeed = 1.5f;
	float TimeToStop = 0;
	float TimeToStopAttacking = 0;


	public GameObject target;
	public GameObject spike;

	void Update (){


		if (MyAnim.GetBool (AnimatorControllerParameter) == false) {
			Clock += Time.deltaTime;

			if (CheckAndUpdate == true) {
				TimeToStop = Clock + UnderGroundTime;
				CheckAndUpdate = false;
				MyAnim.SetFloat ("AnimatorStage", 0);
			}

			if (Clock >= TimeToStop) {
				MyAnim.SetFloat ("AnimatorStage", 1);
			} else {
			
				if (AmIAttacking == false) {
					DistanceFromPlayer = Vector3.Distance (target.transform.position, transform.position);

					if (target.transform.position.x - transform.position.x > 0) {
						GetComponent<SpriteRenderer> ().flipX = true;
					} else {
						GetComponent<SpriteRenderer> ().flipX = false;
					}

					if (DistanceToStartAttacking > DistanceFromPlayer) {
						TimeToStop = Clock + UnderGroundAttackingTime;
						AmIAttacking = true;
					} 

					MovementVector = (target.transform.position - transform.position).normalized;
				} else {
				
					if (TimeToStopAttacking + AttackSpeedTime < Clock) {
						TimeToStopAttacking = Clock;
						Instantiate (spike, transform.position, Quaternion.identity);
					}
				}

				if (AmIAttacking == true) {
					transform.position += MovementVector * Time.deltaTime * AttackSpeed;
				} else {
					transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * WalkSpeed;
				}
			}

		} else {
		
			if (CheckAndUpdate == false) {
				Clock = 0;
				TimeToStopAttacking = 0;
				CheckAndUpdate = true;
				AmIAttacking = false;
			}
		}
	}
}
