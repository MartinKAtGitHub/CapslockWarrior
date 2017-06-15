using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]

public class PlayerManager : DefaultBehaviour {


	public float HealthPoints = 100;
	public float CurrentMana = 100;
	public float MaxMana = 100;

	[Tooltip(" Sets the mana Regen Rate = value per sek")]
	public float ManaRegenRate = 1;

	// TODO ask andrew about prefab and ang drag and drop GM 
	public Text HealtPoints_Txt; // I HAVE DRAGED AND DROPED THE TXT OBJECT
	public Text ManaPoints_Txt; // I HAVE DRAGED AND DROPED THE TXT OBJECT

	void Awake(){
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y - 0.16f;

		if (myPos [0, 0] < 0) {
			if ((myPos [0, 0] % 0.25f) < -0.125f) {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f) - 0.25f;
			} else {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f);
			}
		} else {
			if ((myPos [0, 0] % 0.25f) < 0.125f) {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f);
			} else {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f) + 0.25f;
			}
		}

		if (myPos [0, 1] < 0) {
			if ((myPos [0, 1] % 0.25f) < -0.125f) {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f) - 0.25f;
			} else {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f);
			}
		} else {
			if ((myPos [0, 1] % 0.25f) < 0.125f) {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f);
			} else {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f) + 0.25f;
			}
		}

		MyNode [0] = new Nodes (myPos, 0);
	}

	// Update is called once per frame

	void FixedUpdate(){
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y - 0.16f;

		if (myPos [0, 0] < 0) {
			if ((myPos [0, 0] % 0.25f) < -0.125f) {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f) - 0.25f;
			} else {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f);
			}
		} else {
			if ((myPos [0, 0] % 0.25f) < 0.125f) {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f);
			} else {
				myPos [0, 0] +=	-(myPos [0, 0] % 0.25f) + 0.25f;
			}
		}

		if (myPos [0, 1] < 0) {
			if ((myPos [0, 1] % 0.25f) < -0.125f) {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f) - 0.25f;
			} else {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f);
			}
		} else {
			if ((myPos [0, 1] % 0.25f) < 0.125f) {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f);
			} else {
				myPos [0, 1] +=	-(myPos [0, 1] % 0.25f) + 0.25f;
			}
		}
	}

	void Update () 
	{
	//	myPos [0, 0] = transform.position.x;
	//	myPos [0, 1] = transform.position.y;

		isPlayerAlive();
		ManaRegen(ManaRegenRate);
	}

	public void TakeDamage(int damageAmount)
	{
		;

		HealthPoints -= damageAmount;

	}

	private void isPlayerAlive()
	{
		if(HealthPoints <= 0)
		{
			Debug.Log("<color=blue>PLAYER IS DEAD</color>:");
		}
	}

	public override void OnDestroyed()
	{
		Debug.Log("DEAD OMG");
	}
	public override void AttackTarget(Transform targetPos){

	}
	public override void RecievedDmg(int _damage)
	{
		float PercentageDmg;
		float Total;

		PercentageDmg = _damage / HealthPoints * 100;
		Total = float.Parse(HealtPoints_Txt.text) -  PercentageDmg;
		HealtPoints_Txt.text = Total.ToString();

		if(PercentageDmg == 0)
		{
			OnDestroyed();
		}

	}
	public override void ChangeMovementAdd(float a)
	{

	}

	public override void GotTheKill(int a)
	{
		//Debug.Log ("Score " + a);
		GetComponent<PlayerTyping> ().ResetTheText ();

	}

	public override void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){
		NeighbourGroups = neighbours;
	}


	public void ManaRegen(float manaRegenRate)// TODO Double check the ManaRegen Method in PlayerManager to see if it is not to expensive
	{
		if(CurrentMana < MaxMana) // this has some over flow soo it will stop at 100.001 feks Do we want to set it to 100? 
		{
			// Do this every second 
			CurrentMana += manaRegenRate * Time.deltaTime;
			Debug.Log("Regening Mana" + ManaRegenRate + " Per sek");
		}
		else
		{
			CurrentMana = MaxMana; 
		}
	}

}
