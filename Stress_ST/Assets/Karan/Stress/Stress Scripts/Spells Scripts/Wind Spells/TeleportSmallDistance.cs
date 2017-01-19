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

		Destroy(this.gameObject , 2.0f);

		//TODO we need wall detection. If we are to close to a wall we dont want to TP into the wall
			// Raycast --> detect wall && calulate the range to wall
			// If wall is detected
			// take raycast range + padding = new TP RANGE
		//
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
		}
		else
		{
			Debug.Log("Other is not activ");
		}
		//Hero.position = Hero.position + MoveData.GetDirection() * TeleportRange;
		//Vector2 HeroPos = PlayerGameObject.GetComponent<Rigidbody2D>().position;
		PlayerGameObject.GetComponent<Rigidbody2D>().position = 
			PlayerGameObject.GetComponent<Rigidbody2D>().position + MoveData.Direction * TeleportRange; 
		Debug.Log(MoveData.Direction);
		//HeroPos = HeroPos + test.Direction * TeleportRange;

		GameObject ChildeToHero = (GameObject)Instantiate(this.gameObject, SpellSpawnPos.position, Quaternion.identity);
		ChildeToHero.transform.SetParent(SpellSpawnPos.transform.parent);
		ChildeToHero.gameObject.transform.localScale = new Vector3(1,1); // Need this so the smoke effect flys in the right direction
	}
}
