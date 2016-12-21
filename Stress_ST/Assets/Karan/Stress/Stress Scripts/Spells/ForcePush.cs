using UnityEngine;
using System.Collections;

public class ForcePush : Wind {

	public float ForcePushDestroyAfter;
	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public override void Cast()
	{
		GameObject ForcePushArea = (GameObject)Instantiate(this.gameObject, ProjectileSpawn.position,Quaternion.identity);
		Destroy(ForcePushArea,ForcePushDestroyAfter);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			Debug.Log(other.name);
			other.GetComponent<GolumMovementTest>().IsMoving = false;

		}
	}



}
