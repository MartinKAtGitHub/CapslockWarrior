using UnityEngine;
using System.Collections;
using OldScript;

public class TeleportSmallDistance : OldScript.Ability {


	PlayerController MoveData;
	public float TeleportRange;
	private Vector2 StadingStill = new Vector2(0,0);
	private ParticleSystem TPParticalSystem;

	private bool isSpellCasted;
	[SerializeField]private float coolDownTimer;
	[SerializeField]private Sprite abilityImageIcon;
	[SerializeField]private int manaCost;
	private GameObject playerGameObject;
	private Transform spellSpawnPos;

	public override Sprite AbilityImageIcon {
		get {
			return abilityImageIcon;
		}
		set {
			abilityImageIcon = value;
		}
	}

	public override float BaseCoolDownTimer {
		get {
			return coolDownTimer;
		}
		set {
			coolDownTimer = value;
		}
	}

	public override GameObject InGameSpellRef {
		get {
			throw new System.NotImplementedException ();
		}
		set {
			throw new System.NotImplementedException ();
	
		}
	}

	public override int ManaCost {
		get {
			return manaCost;
		}
		set {
			manaCost = value;
		}
	}

	public override GameObject PlayerGameObject {
		get {
			return playerGameObject;
		}
		set {
			playerGameObject = value;
		}
	}

	public override Transform AbilitySpawnPos {
		get{
			return spellSpawnPos;
		}
		set{
			spellSpawnPos = value;	
		}
	}

	public override AudioClip AbilityActivation{get;set;}


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

	public override bool Cast ()
	{
		if(PlayerGameObject.GetComponent<PlayerController>().enabled)
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
			
			GameObject ChildeToHero = (GameObject)Instantiate(this.gameObject, AbilitySpawnPos.position, Quaternion.identity);
			ChildeToHero.transform.SetParent(AbilitySpawnPos.transform.parent);
			ChildeToHero.gameObject.transform.localScale = new Vector3(1,1); // Need this so the smoke effect flys in the right direction
			//SmokeTrailEffect();
			//IsSpellCasted = true;
			return true;
		}
		else
		{
//			Debug.LogWarning("TELEPORT IS NOT USED YOU ARE STANDING STILL");
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
