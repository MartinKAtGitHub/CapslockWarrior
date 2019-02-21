using UnityEngine;
using System.Collections;
using OldScript;

public class HotShot : OldScript.Ability
{

	public float speed;
	public float DetectionRange;
	public LayerMask FireBallDetectionLayer;
	//private float coolDownTimer; this is comming from the parent ?!??! i can see this in the inspector 

	//private Rigidbody2D rb;
	private Collider2D[] enemiesInRange;
	private GameObject fireBallTarget;

	[SerializeField]private ParticleSystem HSParticleSystem;
	[SerializeField]private ParticleSystem ImpactGM;
	[SerializeField]private GameObject graphics;
	[SerializeField]private BoxCollider2D hotShotHitBox;
	//private float SaveCoolDownTimer;
	//public float SaveCoolDownTimer;


	private bool isSpellCasted;

	[SerializeField]private float coolDownTimer;
	[SerializeField]private Sprite abilityImageIcon;
	[SerializeField]private int manaCost;
	private GameObject playerGameObject;
	private Transform spellSpawnPos;
	private GameObject inGameSpellRef;

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
			return inGameSpellRef;
		}
		set {
			inGameSpellRef = value;
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
		/*
		// NOTE Since all objects are children of this prefab, i can DragDrop HOWEVER i can NullCheck if needed using what is below
		HSParticleSystem = this.gameObject.GetComponentInChildren<ParticleSystem>(); // I can do this to the Top/ first child but nt second
		ImpactGM = this.gameObject.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>(); // This is not Code firednly as i am manually telling where the child is. 
		graphics = this.gameObject.transform.GetChild(0).gameObject;
		hotShotHitBox = this.gameObject.GetComponent<BoxCollider2D>();
		*/

		Debug.Log("Fire ball target = " + fireBallTarget.name); // IF this is null that means that the Missile was activ without target(very bad).
	}

	void Update()
	{
		if(fireBallTarget != null)
		{
			transform.position = Vector3.MoveTowards(transform.position,fireBallTarget.transform.position, speed*Time.deltaTime);
			// PERFORMANCE HOTSHOT This is heavy i think Check this out
			Vector3 dir = fireBallTarget.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		else
		{
			//OnHotShotMissleDeath();//UNDONE HOTSHOT needs to selfDestroy if target dies while missle is in transit 
			Debug.Log("Target Died WHAT DO I DO NOW ?!?");
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		// TODO maybe add some checks to see what the fire ball hits thinking of a system of loops
		// enemy.list == enemy of ceratain type do somthing els what ever
		// TODO maybe make a LayerMask to make this more effecient beacuse the collider will ignore everything that is not on that layer
		if(other.tag == "Enemy")
		{
			Debug.Log("HotShot HIT ->" + other.name);
			if (other.GetComponent<ObjectStats> () != null) 
			{
				other.GetComponent<ObjectStats> ().RecievedDmg (1);
				other.GetComponent<ObjectAI> ().RemoveMyselfFromOthers ();
			}
			Destroy(other.gameObject); //  TODO this needs to be some logic on enemy death;

			OnHotShotMissleDeath();

		}
    }

	
	public override bool Cast()
	{
		return ScanForClosesTargetBoolienCheck();
	}

	private bool ScanForClosesTargetBoolienCheck()
	{
		enemiesInRange = Physics2D.OverlapCircleAll(AbilitySpawnPos.position, DetectionRange, FireBallDetectionLayer);

		if(enemiesInRange.Length > 0) // I get back an array of targets if in range, so if > 0 then i got someone in range
		{
			
			float distance = 0;
			float minDistance = Mathf.Infinity; // i need a value that is higher then the distance that can be detected
			//Debug.Log(ProjectileSpawn.position + "CENTER OF RANGE");

			for (int i = 0; i < enemiesInRange.Length; i++) 
			{
				//Debug.Log("ALL ENEMIS IN RANGE ----------->" + enemiesInRange[i].name + " LENGTH = " + enemiesInRange.Length );
		
				//The Vector3.distance() uses oprations that is expenicv, so we just do it manualy
				Vector3 vectorToTarget = enemiesInRange[i].gameObject.transform.position -AbilitySpawnPos.position;
				distance = vectorToTarget.sqrMagnitude;

				if(distance < minDistance)
				{
					minDistance = distance;
					fireBallTarget = enemiesInRange[i].gameObject;
				}
			}
			InGameSpellRef = (GameObject)Instantiate(this.gameObject, AbilitySpawnPos.position,Quaternion.identity);
			InGameSpellRef.GetComponent<HotShot>().fireBallTarget = fireBallTarget;
			return true;
		}
		else
		{
			Debug.Log("<color=#ff0000ff>ENEMIS ARE NOT IN RANGE --> create GUI to notify player AND RESET TIMER</color>");
			return false;
		}
	}

	private void OnHotShotMissleDeath()
	{
		hotShotHitBox.enabled = false;
		graphics.SetActive(false);  
		HSParticleSystem.Stop();
		ImpactGM.Play();
		Destroy(this.gameObject, 2); 
	} 

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, DetectionRange);
	}
}
