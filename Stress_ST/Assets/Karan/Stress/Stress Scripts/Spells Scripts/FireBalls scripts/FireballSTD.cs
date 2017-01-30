using UnityEngine;
using System.Collections;

public class FireballSTD : Fire {

	public float speed;
	public float DetectionRange;

	public LayerMask FireBallDetection;
	//private float coolDownTimer; this is comming from the parent ?!??! i can see this in the inspector 

	//private Rigidbody2D rb;
	private Collider2D[] enemiesInRange;
	private GameObject fireBallTarget;
	private ParticleSystem HSParticleSystem;
	private ParticleSystem ImpactGM;
	private GameObject graphics;
	private BoxCollider2D hotShotHitBox;

	void Start () 
	{
		//rb = GetComponent<Rigidbody2D>();
		HSParticleSystem = this.gameObject.GetComponentInChildren<ParticleSystem>(); // I can do this to the Top/ first child but nt second
		ImpactGM = this.gameObject.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>(); // This is not Code firednly as i am manually telling where the child is. 
		graphics = this.gameObject.transform.GetChild(0).gameObject;
		hotShotHitBox = this.gameObject.GetComponent<BoxCollider2D>();
		ScanForClosestTarget();


	}
	/*void FixedUpdate () 
	{
		//rb.velocity = new Vector2(speed ,0.0f);
	}*/
	void Update()
	{
		if(fireBallTarget != null)
		{
			transform.position = Vector3.MoveTowards(transform.position,fireBallTarget.transform.position, speed*Time.deltaTime);
			//transform.LookAt();
			// TODO HOTSHOT This is heavy i think Check this out
			Vector3 dir = fireBallTarget.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
			Destroy(other.gameObject); //  TODO this needs to be some logic on enemy death;
			hotShotHitBox.enabled = false;
			graphics.SetActive(false);  
			HSParticleSystem.Stop();
			ImpactGM.Play();
			Destroy(this.gameObject, 2); 

		}
    }

	public override void Cast()
	{
		//TODO Can change the HotShot Code to ScanForClosestTarget() then instanciate if enemy range
		 //ScanForClosestTarget();

		Instantiate(this, SpellSpawnPos.position,Quaternion.identity);
	}

	void ScanForClosestTarget()// TODO add if no enemys are in range
	{
		enemiesInRange = Physics2D.OverlapCircleAll(SpellSpawnPos.position,DetectionRange,FireBallDetection);
		if(enemiesInRange.Length > 0) // I get back an array of targets if in range, so if > 0 then i got someone in range
		{
			float distance = 0;
			float minDistance = DetectionRange; // i need a value that is higher then the distance that can be detected
			//Debug.Log(ProjectileSpawn.position + "CENTER OF RANGE");
			for (int i = 0; i < enemiesInRange.Length; i++) 
			{
				//TODO maybe use Math.abs
				distance = Vector3.Distance(enemiesInRange[i].gameObject.transform.position, SpellSpawnPos.position);
				//distance = Mathf.Abs(distance);
				//Debug.Log("Distance IS = " + distance);
				if(distance < minDistance)
				{
					minDistance = distance;
					fireBallTarget = enemiesInRange[i].gameObject;
					Debug.Log(enemiesInRange[i].gameObject.name);
					//Instantiate(this.gameObject, SpellSpawnPos.position,Quaternion.identity); Wont spawn GM
				}
			}
		}
		else
		{
			Debug.LogError("ENEMIS ARE NOT IN RANGE --> create GUI to notify player AND RESET TIMER");
			Destroy(this.gameObject);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(SpellSpawnPos.position, DetectionRange);
	}
}
