using UnityEngine;
using System.Collections;

public class GolumMovementTest : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		speed = GetComponent<EnemyCreep>().CreepSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rb.velocity = new Vector2(speed,0.0f);
	}
}
