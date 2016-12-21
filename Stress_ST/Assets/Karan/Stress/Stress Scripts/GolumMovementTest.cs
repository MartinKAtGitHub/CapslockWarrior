using UnityEngine;
using System.Collections;

public class GolumMovementTest : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb;


	private Vector3 OldPos;
	public bool IsMoving;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		speed = GetComponent<EnemyCreep>().CreepSpeed;
		 OldPos = Vector3.zero;
		IsMoving = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		

		if(rb.velocity.magnitude < 0.01f || IsMoving == true)
		{
			rb.velocity = new Vector2(speed,0.0f);
			IsMoving = true;
		}
	}


}
