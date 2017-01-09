using UnityEngine;
using System.Collections;

public class TeleportSmallDistance : Wind {


	IMovementEngin MoveData;
	public float TeleportRange;
	
	// Use this for initialization
	void Start () 
	{
		Debug.Log(PlayerGameObject.GetComponent<IMovementEngin>().Speed);
		Debug.Log("HELLOOO");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override void Cast()
	{
		
		if(PlayerGameObject.GetComponent<PlayerControllerCTRL>().enabled)
		{
			MoveData = PlayerGameObject.GetComponent<PlayerControllerCTRL>();
		}
		else
		{
			Debug.Log("Other is not activ");
		}
		//Hero.position = Hero.position + MoveData.GetDirection() * TeleportRange;
		//Vector2 HeroPos = PlayerGameObject.GetComponent<Rigidbody2D>().position;
		PlayerGameObject.GetComponent<Rigidbody2D>().position = 
			PlayerGameObject.GetComponent<Rigidbody2D>().position+ MoveData.Direction * TeleportRange;
		Debug.Log(MoveData.Direction);
		//HeroPos = HeroPos + test.Direction * TeleportRange;

	}
}
