using UnityEngine;
using System.Collections;

public class GolumMovementTest : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb;


	private Vector3 OldPos;
	public Vector2 Force;
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
		
			//rb.velocity = new Vector2(speed,0.0f);

			// IF old pos == new pos then start moving again.
			//Logic find vector from center of Forcepush to target, then addForce( new vector * force)
		/*if(test)
		{
			rb.AddForce(new Vector2(-500,500),ForceMode2D.Force);

			test = false;
		}*/


		/*if(Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(new Vector2(Force.x, Force.y),ForceMode2D.Force);
			IsMoving = false;
		}*/

		if(rb.velocity.magnitude < 0.01f || IsMoving == true)
		{
			rb.velocity = new Vector2(speed,0.0f);
			IsMoving = true;
		}
	}


}
