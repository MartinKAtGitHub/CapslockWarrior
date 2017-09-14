using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]

public class PlayerManager : DefaultBehaviourPosition {

	const int _NewMapCenter = -100;//Previour Center Was 0,0. That Caused Some Problems When The Player Was On A 0 Value. -0.9 == 0. 0.9 = 0. So That Fixed It But That Means That You Cant Go Below -100xy. Change This To Change The Center
	const float _NodeDimentions = 0.08f;

	public float HealthPoints = 100;
	public float CurrentMana = 100;
	public float MaxMana = 100;

	[Tooltip(" Sets the mana Regen Rate = value per sek")]
	public float ManaRegenRate = 1;

	// TODO ask andrew about prefab and ang drag and drop GM 
	public Text HealtPoints_Txt; // I HAVE DRAGED AND DROPED THE TXT OBJECT
	public Text ManaPoints_Txt; // I HAVE DRAGED AND DROPED THE TXT OBJECT

	void Awake(){
		
		MyPos [0, 0] = ((transform.position.x - _NewMapCenter) / _NodeDimentions) - (((transform.position.x - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyPos [0, 1] = ((transform.position.y - _NewMapCenter) / _NodeDimentions) - (((transform.position.y - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyNode [0] = new Nodes (MyPos, 0);
		HealtPoints_Txt.text = HealthPoints.ToString();
	}

	// Update is called once per frame

	void FixedUpdate(){
		MyPos [0, 0] = ((transform.position.x - _NewMapCenter) / _NodeDimentions) - (((transform.position.x - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyPos [0, 1] = ((transform.position.y - _NewMapCenter) / _NodeDimentions) - (((transform.position.y - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
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
		

		HealthPoints -= damageAmount;

	}

	private void isPlayerAlive()
	{
		if(HealthPoints <= 0)
		{
			Debug.Log("<color=blue>PLAYER IS DEAD</color>:");
		}
	}

	public void OnDestroyed()
	{
		Debug.Log("DEAD OMG");
	}

	public override void RecievedDmg(int _damage)
	{

		/*HealtPoints_Txt.text = (HealthPoints -= _damage).ToString ();//Normal Health Subtraction Without Percentage Subtraction
		if (HealthPoints <= 0) {OnDestroyed ();}*/


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
