using UnityEngine;
using System.Collections;

public class SlowFieldSpell : Ability {


	public float SpellRadius; 
	private float OriginalSpeed;
	public float SlowRate;

	private CircleCollider2D cC2d;
	//private ParticleSystem.ShapeModule particalSys;
	private ParticleSystem test;

	private bool isSpellCasted;
	[SerializeField]private float coolDownTimer;
	[SerializeField]private Sprite abilityImageIcon;
	[SerializeField]private int manaCost;
	private GameObject playerGameObject;
	private Transform spellSpawnPos;

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

	// TODO Ask how this works little confused dose how the trigger retains info on what speeds to give back to the enemy
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			//TODO we must replace ENEMYCREEP with a class hierarchy so every ENEMY is effected by the slow
			if(other.GetComponent<ObjectStats>() != null){
			//	other.GetComponent<DefaultBehaviour>().Turnoffwithforcestuff = true;
				other.GetComponent<ObjectStats>().MovementSpeedChange(-SlowRate);
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
			if(other.GetComponent<ObjectStats>() != null){
		//		other.GetComponent<DefaultBehaviour>().Turnoffwithforcestuff = true;
				other.GetComponent<ObjectStats>().MovementSpeedChange(SlowRate);

			//	other.GetComponent<GolumMovementTest> ().speed = other.GetComponent<EnemyCreep> ().CreepSpeed;
			}
			//other.GetComponent<GolumMovementTest>().speed = other.GetComponent<EnemyCreep>().CreepSpeed;
		//	Debug.Log("Enemy name EXIT -> "+ other.name);
		}
	}


	public override bool Cast()
	{
		Instantiate(this, AbilitySpawnPos.position,Quaternion.identity);
		return true;
	}
}
