using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IMovementEngin {


	public float maxSpeed = 10f;
	public Animator heroAnimator;

	bool facingRigth;
	bool cancelMouseMovement;
	bool targetSet;
	Vector2 deltaTargetCurrentPos; // ugly solution
	Vector2 facing;
	Rigidbody2D playerRigBdy;
	Transform heroGraphics;
	Vector3 mousePos;


	public float Speed
	{
		get
		{
			return maxSpeed;	
		}
		set
		{
			maxSpeed = value;		
		}
	}

	public Rigidbody2D PlayerRigBdy
	{
		get
		{
			return playerRigBdy;
		}
		set
		{
			playerRigBdy = value;
		}
	}
	public Vector2 Direction
	{
		get
		{
			return facing;
		}
		set
		{
			facing = value;
		}
	}
	public bool SpriteFacingRigth
	{
		get
		{
			return facingRigth;
		}
		set
		{
			facingRigth = value;
		}
	}

	// Use this for initialization
	void Awake () 
	{
		cancelMouseMovement = true;
		facingRigth = true;
		targetSet = false;
		deltaTargetCurrentPos = new Vector2(0,0);

		playerRigBdy = GetComponent<Rigidbody2D>();
		mousePos = Vector3.zero;

		 heroGraphics = transform.Find("GFX"); // use thesprite rendrer.flip(X) insted of scale maybe?
		//Debug.LogWarning("ADD THE INTERFACE TO PLAYERCONTOLLER MOUSE");


		// heroAnimator = GetComponent<Animator>();
		heroAnimator = heroGraphics.GetComponent<Animator>();
		if(heroAnimator == null)
		{
			Debug.LogWarning("SHITT IS NULL DOG");
		}
	}


	void FixedUpdate()
	{
		MovementLogic();
		// Changing the sprite so the guy is facing the rigth direction acording to movement
		if(facing.x > 0 && !facingRigth)
		{
			Flip();
			//cancelMouseMovement = true;
		}
		else if(facing.x < 0 && facingRigth)
		{
			Flip();
			//cancelMouseMovement = true;
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

	public void Flip()
	{
		facingRigth = !facingRigth;
		Vector3 theScale = heroGraphics.localScale;
		theScale.x *= -1;
		heroGraphics.localScale = theScale;

		//transform.localScale.x *= -1f;
	}

	void MovePlayerMouse(Vector3 targetPos)
	{
		// this is bad we need to use the rigidbody and calulate a vector to the target and move in that direction
		//transform.position = Vector3.MoveTowards(transform.position, pos, maxSpeed * Time.deltaTime); 



		if(targetSet == true)
		{
			deltaTargetCurrentPos = targetPos - transform.position;
		//	Debug.Log("TargetSet");
		}
		facing.x = deltaTargetCurrentPos.normalized.x;
		facing.y = deltaTargetCurrentPos.normalized.y;

		//Debug.Log("Mouse Directions (" + facing.x + ", " + facing.y + ")");
		playerRigBdy.velocity = deltaTargetCurrentPos.normalized * maxSpeed; // this speed needs to be the same as 1 * maxSpeed


		//Debug.Log("DELTA Is = " + deltaTargetCurrentPos);

		//playerRigBdy.velocity = (targetPos - transform.position).normalized * maxSpeed;
		//playerRigBdy.velocity = new Vector2(targetPos.x ,targetPos.y);

		Debug.DrawLine( transform.position, targetPos, Color.cyan);
	}

	public void  MovementLogic()
	{
		facing.x = Input.GetAxisRaw("MouseAndArrowsX"); // remove AD keys in input Manager
		facing.y = Input.GetAxisRaw("MouseAndArrowsY");	// Remove WS keys in input Manager



		playerRigBdy.velocity = new Vector2 (facing.x * maxSpeed, facing.y * maxSpeed);

		//heroAnimator.SetFloat("Speed", PlayerRigBdy.velocity.x); Need to figure out how to connect anim


		if(Mathf.Abs(facing.x) > 0) // Might be better WITHOUT .abs()
		{

			cancelMouseMovement = true;
		}
		if(Mathf.Abs(facing.y) > 0)// Might be better WITHOUT .abs()
		{

			cancelMouseMovement = true;
		}
		if(!cancelMouseMovement)
		{
			MovePlayerMouse(mousePos);

			//Debug.Log ("INSIDE = " + mousePos );
			targetSet = false;
		}
	}
}
