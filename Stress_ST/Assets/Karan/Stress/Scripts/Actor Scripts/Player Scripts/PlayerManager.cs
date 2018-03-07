using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



[RequireComponent(typeof(AudioSource))]

public class PlayerManager : AbsoluteRoot {

	const int _NewMapCenter = -100;//Previour Center Was 0,0. That Caused Some Problems When The Player Was On A 0 Value. -0.9 == 0. 0.9 = 0. So That Fixed It But That Means That You Cant Go Below -100xy. Change This To Change The Center
	const float _NodeDimentions = 0.08f;

	public float HealthPoints = 100;
	public float CurrentMana = 100;
	public float MaxMana = 100;

	[Tooltip(" Sets the mana Regen Rate = value per sek")]
	public float ManaRegenRate = 1;

	public Text HealtPoints_Txt;
	public Text ManaPoints_Txt; 

	public float totalmovementdecrease = 1;

	private int MaxHealtPoints;
	private int CurrentHealtPoints;
	[SerializeField]private	int healthPointsPerContainer = 4;
	[SerializeField]private Image [] heartContainers;




	void Awake(){
		
		MyPos [0, 0] = ((transform.position.x - _NewMapCenter) / _NodeDimentions) - (((transform.position.x - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyPos [0, 1] = ((transform.position.y - _NewMapCenter) / _NodeDimentions) - (((transform.position.y - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyNode [0] = new Nodes (MyPos, 0);
		HealtPoints_Txt.text = HealthPoints.ToString();
	}


	void Start()
	{
		MaxHealtPoints = healthPointsPerContainer * heartContainers.Length;
		CurrentHealtPoints = MaxHealtPoints;
	}

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

	private void ClampHealth()
	{
		CurrentHealtPoints = Mathf.Clamp(CurrentHealtPoints, 0 , MaxHealtPoints);
		OnHealthChanged(CurrentHealtPoints);
	}
	private void OnHealthChanged(int health)
	{
		int heartContainerIndex = health / healthPointsPerContainer;
		int heartFill = health % healthPointsPerContainer;


		if(heartFill == 0)
		{
			if(heartContainerIndex == heartContainers.Length)//indicates full HP
			{
				heartContainers[heartContainerIndex -1].fillAmount = 1;
				return;
			}
			if(heartContainerIndex > 0)// indicates anything but 0 health where there are only whole wearts or empty hearts
			{
				heartContainers[heartContainerIndex].fillAmount = 0;
				heartContainers[heartContainerIndex - 1].fillAmount = 1;

			}
			else // 0 health
			{
				heartContainers[heartContainerIndex].fillAmount = 0;
			}
			return;
		}
		heartContainers[heartContainerIndex].fillAmount = heartFill / (float)healthPointsPerContainer;
	}

	private void isPlayerAlive()
	{
		if(HealthPoints <= 0)
		{
			Debug.Log("<color=blue>PLAYER IS DEAD</color>:");
		}
	}


	public override void RecievedDmg(int _damage) // TODO Matf.Clamp the HP
	{
		/*HealtPoints_Txt.text = (HealthPoints -= _damage).ToString ();//Normal Health Subtraction Without Percentage Subtraction
		if (HealthPoints <= 0) 
		{
			OnDestroyed ();
		}*/
		CurrentHealtPoints -= _damage;
		ClampHealth();
	}

	public void HealDmg(int _heal) // TODO Matf.Clamp the HP
	{
		CurrentHealtPoints += _heal;
		ClampHealth();
	}


	public void OnDestroyed()
	{
		Debug.Log("Player Gameobject has been destroyed");
	}
	public void MovementSpeedChange(float a)//TODO TODO Speed Change Logic
	{
		totalmovementdecrease += a;

		if (totalmovementdecrease < 0) {
			GetComponent<PlayerController> ().Speed = 0.1f;
		} else {
			GetComponent<PlayerController> ().Speed = totalmovementdecrease;
		}

	}

	public void GotTheKill(int a){
		
		Debug.Log ("Score " + a);
	}

	public void ResetWord()
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
			//Debug.Log("Regening Mana " + ManaRegenRate + " Per sek");
			//Debug.Log("Current Mana " + CurrentMana);
		}
		else
		{
			CurrentMana = MaxMana;
			//Debug.Log("Final " + CurrentMana); 
		}
	}



}
