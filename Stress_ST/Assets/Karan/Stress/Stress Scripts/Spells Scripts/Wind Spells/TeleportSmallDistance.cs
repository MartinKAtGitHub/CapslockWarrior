using UnityEngine;
using System.Collections;

public class TeleportSmallDistance : Wind {


	IMovementEngin MoveData;
	public float TeleportRange;
	private Vector2 StadingStill = new Vector2(0,0);
	private ParticleSystem TPParticalSystem;

	private bool isSpellCasted;

	public override bool IsSpellCasted 
	{
		get
		{
			return isSpellCasted;
		} 
		set
		{
			isSpellCasted = value;
		}
	}
	// Use this for initialization

	void Start () 
	{
		//Debug.Log(PlayerGameObject.GetComponent<IMovementEngin>().Speed);
	
		//Destroy(this.gameObject, TPParticalSystem.duration);
		Destroy(this.gameObject, 2);


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
			Debug.Log("Cant find any movementScripts");
		}
		//Hero.position = Hero.position + MoveData.GetDirection() * TeleportRange;
		//Vector2 HeroPos = PlayerGameObject.GetComponent<Rigidbody2D>().position;
		if(MoveData.Direction != StadingStill) // Can couse a very minor bug where the player dosent TP rigth away
		{
			PlayerGameObject.GetComponent<Rigidbody2D>().position = 
				PlayerGameObject.GetComponent<Rigidbody2D>().position + MoveData.Direction * TeleportRange; 
			Debug.Log(MoveData.Direction);
			//HeroPos = HeroPos + test.Direction * TeleportRange;
			
			GameObject ChildeToHero = (GameObject)Instantiate(this.gameObject, SpellSpawnPos.position, Quaternion.identity);
			ChildeToHero.transform.SetParent(SpellSpawnPos.transform.parent);
			ChildeToHero.gameObject.transform.localScale = new Vector3(1,1); // Need this so the smoke effect flys in the right direction
			//SmokeTrailEffect();
			IsSpellCasted = true;
		}
		else
		{
			Debug.LogWarning("TELEPORT IS NOT USED YOU ARE STANDING STILL");
			IsSpellCasted = false;
		}
	}

	public override bool CastBoolienReturn ()
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
			Debug.Log("Cant find any movementScripts");
		}
		//Hero.position = Hero.position + MoveData.GetDirection() * TeleportRange;
		//Vector2 HeroPos = PlayerGameObject.GetComponent<Rigidbody2D>().position;
		if(MoveData.Direction != StadingStill) // Can couse a very minor bug where the player dosent TP rigth away
		{
			PlayerGameObject.GetComponent<Rigidbody2D>().position = 
				PlayerGameObject.GetComponent<Rigidbody2D>().position + MoveData.Direction * TeleportRange; 
			Debug.Log(MoveData.Direction);
			//HeroPos = HeroPos + test.Direction * TeleportRange;
			
			GameObject ChildeToHero = (GameObject)Instantiate(this.gameObject, SpellSpawnPos.position, Quaternion.identity);
			ChildeToHero.transform.SetParent(SpellSpawnPos.transform.parent);
			ChildeToHero.gameObject.transform.localScale = new Vector3(1,1); // Need this so the smoke effect flys in the right direction
			//SmokeTrailEffect();
			//IsSpellCasted = true;
			return true;
		}
		else
		{
			Debug.LogWarning("TELEPORT IS NOT USED YOU ARE STANDING STILL");
			//IsSpellCasted = false;
			return false;
		}
	}

	private void SmokeTrailEffect()
	{
		// Buggy dosent fully follow
		TPParticalSystem = this.gameObject.GetComponentInChildren<ParticleSystem>();
		ParticleSystem.VelocityOverLifetimeModule SmokeFollow = TPParticalSystem.velocityOverLifetime;
		Debug.Log("lolololo" + MoveData.Direction);
		SmokeFollow.x = MoveData.Direction.x ;
		SmokeFollow.y = MoveData.Direction.y ;
	}

}
