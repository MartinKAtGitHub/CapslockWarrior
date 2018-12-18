using UnityEngine;
using System.Collections;

public class SlowFieldSpell : Ability {




	[SerializeField]private float abilityRadius; 
	[SerializeField]private float slowRate;
	[SerializeField]private float coolDownTimer;
	[SerializeField]private float slowFieldDuration;
	[SerializeField]private Sprite abilityImageIcon;
	[SerializeField]private int manaCost;

	private float  slowFieldDurationCounDown;
	private CircleCollider2D cC2d;
	//private ParticleSystem.ShapeModule particalSys;
	private ParticleSystem test;
	private GameObject playerGameObject;
	private Transform spellSpawnPos;



	public float SlowRate
	{
		get
		{
			return slowRate;
		}
		set
		{
			Debug.Log("SlowField SlowRate Set = " + value);
			slowRate = value;
		}
	}

	public float AbilityRadius
	{
		get
		{
			return abilityRadius;
		}
		set 
		{
			Debug.Log("SlowField Ability Radius Set = " + value);
			abilityRadius = value;
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
		slowFieldDurationCounDown = slowFieldDuration; 
		cC2d = GetComponent<CircleCollider2D>();
		cC2d.radius = AbilityRadius;

		//particalSys = GetComponentInChildren<ParticleSystem.ShapeModule>();
		//particalSys.radius = SpellRadius;

		test = GetComponentInChildren<ParticleSystem>();
		var sh = test.shape;
		sh.radius = AbilityRadius;
	}

	void Update()
	{
		SlowAbilityDuration();
	}

	void SlowAbilityDuration()
	{
		

		if((slowFieldDurationCounDown -= Time.deltaTime) <= 0)
		{
			slowFieldDurationCounDown = slowFieldDuration;
			Destroy(this.gameObject);
			Debug.Log("SlowField ---> Destroyed BUT PEOPLE STILL SLOWED SINCE ONTRIGGEREXIT(FIX in COMMENTS -->)"); // Save Ref from all Effected Enemis. OnEffectEnd Release them from slow or have a CD that will time it out
		}
	}
	// TODO Ask how this works little confused dose how the trigger retains info on what speeds to give back to the enemy
	void OnTriggerEnter2D(Collider2D other)
	{
		// TODO Add all enemys in list. Slow them. On Effect duration over, Reset all enemy speed values


		if(other.tag == "Enemy")
		{
			//TODO we must replace ENEMYCREEP with a class hierarchy so every ENEMY is effected by the slow
			if(other.GetComponent<ObjectStats>() != null){ // THIS needs to be reuerd on enemy
			//	other.GetComponent<DefaultBehaviour>().Turnoffwithforcestuff = true;
				other.GetComponent<ObjectStats>().MovementSpeedChange(-SlowRate);
			}

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
