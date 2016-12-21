using UnityEngine;
using System.Collections;

public class ForcePush : Wind {

	public Vector2 ForceApplied;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public override void Cast()
	{
		Instantiate(this, ProjectileSpawn.position,Quaternion.identity);
	}

	/*void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
				Debug.Log(other.name);
				other.GetComponent<GolumMovementTest>().IsMoving = false;
				other.GetComponent<Rigidbody2D>().AddForce(ForceApplied);
		}
	}*/



}
