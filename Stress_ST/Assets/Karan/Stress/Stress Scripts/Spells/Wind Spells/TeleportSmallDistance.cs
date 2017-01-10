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

	public override void Cast()
	{
		
		if(PlayerGameObject.GetComponent<PlayerControllerCTRL>().enabled)
		{
			MoveData = PlayerGameObject.GetComponent<PlayerControllerCTRL>();
		}
		else if(PlayerGameObject.GetComponent<PlayerController>().enabled)
		{
			MoveData = PlayerGameObject.GetComponent<PlayerController>();
			Debug.Log("LOLOLOLOLOL");
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
