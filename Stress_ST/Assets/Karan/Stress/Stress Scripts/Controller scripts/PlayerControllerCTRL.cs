using UnityEngine;
using System.Collections;

public class PlayerControllerCTRL : MonoBehaviour {


	public float MaxSpeed = 1.5f;
	float moveX;
	float moveY;

	Rigidbody2D PlayerOBj;
	// Use this for initialization
	void Awake () 
	{
		PlayerOBj = GetComponent<Rigidbody2D>();
		moveX = 0;
		moveY = 0;	

	}
	

	void FixedUpdate()
	{

		if(Input.GetKey(KeyCode.LeftControl)) // Ctrl + s Trys to Save the game so dosent work in editor
		{
			moveX = Input.GetAxis("Horizontal"); 
			moveY = Input.GetAxis("Vertical");


		}
			//PlayerOBj.velocity = Vector2.zero;
			PlayerOBj.velocity = new Vector2(moveX * MaxSpeed, moveY * MaxSpeed);
			Debug.Log("MoveX  = " + moveX);
			Debug.Log("MoveY  = " + moveY);
		

	}


	// Update is called once per frame
	void Update () 
	{
	
	}
}
