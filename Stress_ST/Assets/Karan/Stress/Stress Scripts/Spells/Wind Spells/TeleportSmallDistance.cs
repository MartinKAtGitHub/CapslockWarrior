using UnityEngine;
using System.Collections;

public class TeleportSmallDistance : Wind {


	public TeleportSmallDistance()
	{
		Debug.Log("TELPORTTTTTTTTTTT");
	}
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
		PlayerGameObject.GetComponent<Rigidbody2D>().position = new Vector2(0,0);
		//hero.rigidbody.position(direction + tele range, same)
	}
}
