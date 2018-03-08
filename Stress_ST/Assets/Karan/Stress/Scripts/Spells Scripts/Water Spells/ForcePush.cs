using UnityEngine;
using System.Collections;

public class ForcePush : Water {

	public float ForcePushDestroyAfter;
	GameObject ForcePushArea;
	private ParticleSystem ForceParticalSystem;

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
	void Start()
	{
		ForceParticalSystem = this.gameObject.GetComponentInChildren<ParticleSystem>();
		//Destroy(this.gameObject, ForceParticalSystem.duration);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (ForceParticalSystem.IsAlive() == false) // TODO migth be to expensiv update() check if partical system isAlive
		{
			Destroy(this.gameObject);
		}
	}

	public override bool Cast ()
	{
		ForcePushArea = (GameObject)Instantiate(this.gameObject, AbilitySpawnPos.position,Quaternion.identity);
		return true;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			//Debug.Log(other.name);
			if(other.GetComponent<ObjectAI>() != null)//TODO
				other.GetComponent<ObjectAI>().TheObject.FreezeCharacter = true;
		//	other.GetComponent<GolumMovementTest>().IsMoving = false;

		}
	}



}
