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


	/*FireballSTD()
	{
		//base.CoolDownTimer = coolDownTimer;
	}*/

	void Start () 
	{
		//rb = GetComponent<Rigidbody2D>();
		ScanForClosestTarget();

	}
	void FixedUpdate () 
	{
		//rb.velocity = new Vector2(speed ,0.0f);
	}
	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position,fireBallTarget.transform.position, speed*Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{

		// TODO maybe add some checks to see what the fire ball hits thinking of a system of loops
		// enemy.list == enemy of ceratain type do somthing els what ever
		// TODO maybe make a LayerMask to make this more effecient beacuse the collider will ignore everything that is not on that layer
		if(other.tag == "Enemy")
		{
			Destroy(other.gameObject);
			Destroy(this.gameObject);   
			Debug.Log("HIT ->" + other.name);
		}

    }

	public override void Cast()
	{
		Instantiate(this, SpellSpawnPos.position,Quaternion.identity);
	}

	void ScanForClosestTarget()// TODO add if no enemys are in range
	{
		enemiesInRange = Physics2D.OverlapCircleAll(SpellSpawnPos.position,DetectionRange,FireBallDetection);
		if(enemiesInRange != null)
		{
			float distance = 0;
			float minDistance = DetectionRange; // i need a value that is higher then the distance that can be detected
			//Debug.Log(ProjectileSpawn.position + "CENTER OF RANGE");
			for (int i = 0; i < enemiesInRange.Length; i++) 
			{
				//TODO maybe use Math.abs ww mig have a pro with - valuse
				distance = Vector3.Distance(enemiesInRange[i].gameObject.transform.position, SpellSpawnPos.position);
				//distance = Mathf.Abs(distance);
				//Debug.Log("Distance IS = " + distance);
				if(distance < minDistance)
				{
					minDistance = distance;
					fireBallTarget = enemiesInRange[i].gameObject;
					Debug.Log(enemiesInRange[i].gameObject.name);
				}
			}
		}
		else
		{
			Debug.LogError("ENEMIS IN RANGE NULL --> create GUI to notify player");
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(SpellSpawnPos.position, DetectionRange);
	}
}
