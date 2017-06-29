using UnityEngine;
using System.Collections;

public class SlowFieldSpell : Earth {


	public float SpellRadius; 
	private float OriginalSpeed;
	public float SlowRate;
	private bool isSpellCasted;

	private CircleCollider2D cC2d;
	//private ParticleSystem.ShapeModule particalSys;
	private ParticleSystem test;

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
		cC2d = GetComponent<CircleCollider2D>();
		cC2d.radius = SpellRadius;

		//particalSys = GetComponentInChildren<ParticleSystem.ShapeModule>();
		//particalSys.radius = SpellRadius;

		test = GetComponentInChildren<ParticleSystem>();
		var sh = test.shape;
		sh.radius = SpellRadius;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override void Cast()
	{
		Instantiate(this, SpellSpawnPos.position,Quaternion.identity);
	}

	// TODO Ask how this works little confused dose how the trigger retains info on what speeds to give back to the enemy
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			//TODO we must replace ENEMYCREEP with a class hierarchy so every ENEMY is effected by the slow
			if(other.GetComponent<DefaultBehaviour>() != null){
			//	other.GetComponent<DefaultBehaviour>().Turnoffwithforcestuff = true;
				other.GetComponent<DefaultBehaviour>().ChangeMovementAdd(-SlowRate);
			}
			//other.GetComponent<GolumMovementTest>().speed *= SlowRate; 
	//		Debug.Log("Enemy name STAY -> "+ other.name);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			//TODO this is not good yo sould cache this i think somhow .. yup
			if(other.GetComponent<DefaultBehaviour>() != null){
		//		other.GetComponent<DefaultBehaviour>().Turnoffwithforcestuff = true;
				other.GetComponent<DefaultBehaviour>().ChangeMovementAdd(SlowRate);

			//	other.GetComponent<GolumMovementTest> ().speed = other.GetComponent<EnemyCreep> ().CreepSpeed;
			}
			//other.GetComponent<GolumMovementTest>().speed = other.GetComponent<EnemyCreep>().CreepSpeed;
		//	Debug.Log("Enemy name EXIT -> "+ other.name);
		}
	}


	public override bool CastBoolienReturn()
	{
		Instantiate(this, SpellSpawnPos.position,Quaternion.identity);
		return true;
	}
}
