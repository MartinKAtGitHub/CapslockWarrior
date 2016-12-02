using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float maxSpeed = 10f;
	bool facingRigth;
	bool cancelMouseMovement;
	bool targetSet;
	Vector2 deltaTargetCurrentPos; // ugly solution
	Rigidbody2D playerRigBdy;

	Vector3 mousePos;
	// Use this for initialization
	void Awake () 
	{
		cancelMouseMovement = true;
		facingRigth = true;
		targetSet = false;
		deltaTargetCurrentPos = new Vector2(0,0);

		playerRigBdy = GetComponent<Rigidbody2D>();
		mousePos = Vector3.zero;
	}

	void FixedUpdate()
	{
		float moveX = Input.GetAxis("MouseAndArrowsX"); // remove AD keys in input Manager
		float moveY = Input.GetAxis("MouseAndArrowsY");	// Remove WS keys in input Manager

		playerRigBdy.velocity = new Vector2 (moveX * maxSpeed, moveY * maxSpeed);

		if(Mathf.Abs(moveX) > 0) // Might be better WITHOUT .abs()
		{
			cancelMouseMovement = true;
		}
		if(Mathf.Abs(moveY) > 0)// Might be better WITHOUT .abs()
		{
			cancelMouseMovement = true;
		}


		if(moveX > 0 && !facingRigth)
		{
			Flip();
			//cancelMouseMovement = true;
		}
		else if(moveX < 0 && facingRigth)
		{
			Flip();
			//cancelMouseMovement = true;
		}
		if(!cancelMouseMovement)
		{
			MovePlayerMouse(mousePos);

			//Debug.Log ("INSIDE = " + mousePos );
			targetSet = false;
		}

	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(1))
		{
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			targetSet = true;
			cancelMouseMovement = false;
		}
	}

	void Flip()
	{
		facingRigth = !facingRigth;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void MovePlayerMouse(Vector3 targetPos)
	{
		// this is bad we need to use the rigidbody and calulate a vector to the target and move in that direction
		//transform.position = Vector3.MoveTowards(transform.position, pos, maxSpeed * Time.deltaTime); 



		if(targetSet == true)
		{
			deltaTargetCurrentPos = targetPos - transform.position;
			Debug.Log("TargetSet");
		}

		playerRigBdy.velocity = deltaTargetCurrentPos.normalized * maxSpeed; // this speed needs to be the same as 1 * maxSpeed


		Debug.Log("DELTA Is = " + deltaTargetCurrentPos);

		//playerRigBdy.velocity = (targetPos - transform.position).normalized * maxSpeed;
		//playerRigBdy.velocity = new Vector2(targetPos.x ,targetPos.y);

		Debug.DrawLine( transform.position, targetPos, Color.cyan);
	}
}
