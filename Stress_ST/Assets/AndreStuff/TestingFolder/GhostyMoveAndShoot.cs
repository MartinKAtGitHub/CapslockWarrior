using UnityEngine;
using System.Collections;

public class GhostyMoveAndShoot : MonoBehaviour {

/*	float walkingSpeed = 3;
	float changeDirection = 0;
	bool fireOnce = false;
	public GameObject Bullet;
	GameObject player;



	// Use this for initialization
	void Start () {
		GetComponent<Animator>().SetFloat("ChangeAnimation", 0);
		player = GameObject.FindWithTag ("Player1");
	}
	
	// Update is called once per frame
	void Update () {

		/*RaycastHit2D[] collisions;
		LayerMask LineOfSight;'
		LineOfSight = 1 << LayerMask.NameToLayer("Walls"); // LayerMask.NameToLayer returns an integer, and the layermask needs something more to turn the integer into the required value so "1 <<" turns the integer(from the colliderlayers) into the correct layer to use. do this or make the variable public
		collisions = Physics2D.LinecastAll ((Vector2)player.transform.position, (Vector2)transform.position, LineOfSight);
		for (int i = 0; i < collisions.Length; i++) {
			Debug.Log (collisions[i].collider.name);
		}*/

	/*	if (Input.GetKeyUp ("space")) {
			GetComponent<Animator> ().SetFloat ("ChangeAnimation", 0);
		}

		if (fireOnce == true) {
			if (GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Ghosty Idle")) {
				fireOnce = false;
				Instantiate (Bullet, new Vector3(transform.position.x,transform.position.y - 0.25f,transform.position.z ), Quaternion.identity);
			}
		}

		if (Input.GetKeyUp ("left") || Input.GetKeyUp ("right")) {
			changeDirection = 0;
			GetComponent<Animator> ().SetFloat ("ChangeAnimation", 0);
		}



		if (Input.GetKey ("space")) {
			fireOnce = true;
			GetComponent<Animator> ().SetFloat ("ChangeAnimation", 5);
		}

		if (Input.GetKey ("up")) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + (Time.smoothDeltaTime * walkingSpeed), transform.position.z);
		}

		if (Input.GetKey ("down")) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - (Time.smoothDeltaTime * walkingSpeed), transform.position.z);
		}

		if (Input.GetKey ("left")) {

			if (changeDirection != 2) {
				changeDirection = 2;
				GetComponent<Animator> ().SetFloat ("ChangeAnimation", 1);
				if (transform.localScale.x > 0) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, 1);
				}
			}
			transform.position = new Vector3 (transform.position.x - (Time.smoothDeltaTime * walkingSpeed), transform.position.y, transform.position.z);
		}
			
		if (Input.GetKey ("right")) {
			if (changeDirection != 1) {
				changeDirection = 1;
				GetComponent<Animator> ().SetFloat ("ChangeAnimation", 1);
				if (transform.localScale.x < 0) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, 1);
				}
			}
			transform.position = new Vector3 (transform.position.x + (Time.smoothDeltaTime * walkingSpeed), transform.position.y, transform.position.z);
		}
	}*/
}
