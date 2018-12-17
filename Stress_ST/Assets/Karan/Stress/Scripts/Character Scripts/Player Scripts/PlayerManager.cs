using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



[RequireComponent(typeof(AudioSource))]

public class PlayerManager : CreatureRoot {

	public ObjectNodeInfo Node;
	const int _NewMapCenter = -100;//Previour Center Was 0,0. That Caused Some Problems When The Player Was On A 0 Value. -0.9 == 0. 0.9 = 0. So That Fixed It But That Means That You Cant Go Below -100xy. Change This To Change The Center
	const float _NodeDimentions = 0.08f;
	
    /*
	public float HealthPoints = 100;
	public float CurrentMana = 100;
	public float MaxMana = 100;
	*/

	[Tooltip(" Sets the mana Regen Rate = value per sek")]
	public float ManaRegenRate = 1;
	public int ManaGainPerTick = 1;
	float regenTick;

	//public Text HealtPoints_Txt;
	//public Text ManaPoints_Txt; 

	public float totalmovementdecrease = 1;

	private int maxHealtPoints;
	private int currentHealtPoints;
	private int maxManaPoints;
	private int currentManaPoints;
	private const int PointsPerContainer = 4;
	[SerializeField]private Image [] HeartContainers;
	[SerializeField]private Image [] ManaContainers;

	public int Test {get;set;}

	public int CurrentHealtPoints
	{
		get
		{
			return currentHealtPoints;
		}
		set 
		{
			currentHealtPoints = value;
		}
	}


	public int CurrentManaPoints
	{
		get
		{
			return currentManaPoints;
		}
	}


	void Awake()
	{
		
		Node.MyCollisionInfo.CalculateNodePos (transform.position);
		//HealtPoints_Txt.text = HealthPoints.ToString();
	}

	void Start()
	{		
		CalculateMaxHealthOrMana(ref maxHealtPoints, ref currentHealtPoints, PointsPerContainer, HeartContainers);
		CalculateMaxHealthOrMana(ref maxManaPoints, ref currentManaPoints, PointsPerContainer, ManaContainers);

		//Debug.Log(currentHealtPoints);

		UIContainerChecks(HeartContainers);
		UIContainerChecks(ManaContainers);
	}

	void FixedUpdate()
	{
		Node.MyCollisionInfo.CalculateNodePos (transform.position);
	}

	void Update () 
	{
		isPlayerAlive();
		ManaRegen(ManaRegenRate, ManaGainPerTick);
	}

	private void UIContainerChecks(Image [] containers)
	{
		for (int i = 0; i < containers.Length; i++) 
		{
			if(containers[i].type != Image.Type.Filled)
			{
				Debug.LogError("Wrong Fill Type, needs front img");
			}
		}
	}

	private void CalculateMaxHealthOrMana(ref int maxPoints, ref int currentPoints, int pointsPerContainer, Image [] containers)
	{
		maxPoints = pointsPerContainer * containers.Length;
		currentPoints = maxPoints;
	}

	private void ClampHealth()
	{
		currentHealtPoints = Mathf.Clamp(currentHealtPoints, 0 , maxHealtPoints);
		OnHealthOrManaChanged(currentHealtPoints, HeartContainers);
	}

	private void ClampMana()
	{
		currentManaPoints = Mathf.Clamp(currentManaPoints, 0 , maxManaPoints);
		OnHealthOrManaChanged(currentManaPoints,  ManaContainers);
	}

	private void OnHealthOrManaChanged(int currentValue, Image[] container)
	{
		int containerIndex = currentValue / PointsPerContainer;
		//Debug.Log("Current Container (" + containerIndex + ")");
		int fill = currentValue % PointsPerContainer;
		//Debug.Log("Current Fill (" + fill + ")");


		if(fill == 0)
		{
			if(containerIndex == container.Length)//indicates full HP
			{
				container[containerIndex -1].fillAmount = 1;
				return;
			}
			if(containerIndex > 0)// indicates anything but 0 health where there are only whole wearts or empty hearts
			{
				container[containerIndex].fillAmount = 0;
				container[containerIndex - 1].fillAmount = 1;

			}
			else // 0 health
			{
				container[containerIndex].fillAmount = 0;
			}
			return;
		}
		container[containerIndex].fillAmount = fill / (float)PointsPerContainer;
	}

	private void isPlayerAlive()
	{
		if(currentHealtPoints <= 0)
		{
			Debug.Log("<color=blue>PLAYER IS DEAD</color>:");
		}
	}


	public override void TookDmg(int damage) // TODO Matf.Clamp the HP
	{
		//Debug.Log(gameObject.name + " Recived = " + damage);

		for (int i = 0; i < (Mathf.FloorToInt(damage)); i++) //PERFORMANCE the system only works for 1 dmg(value)... so i need to calulate for every instance of dmg
		{
			currentHealtPoints -= 1;
			ClampHealth();
		}

	}

	public void HealDmg(int heal)
	{
		for (int i = 0; i < heal; i++)
		{	
			currentHealtPoints += 1;
			ClampHealth();
		}
	}

	public void AbilityManaCost(int manaCost)
	{
		for (int i = 0; i < manaCost; i++) 
		{
			currentManaPoints -= 1;
			ClampMana();
		}
	}

    public void AbilityManaGain(int amountGain)
	{
		for (int i = 0; i < amountGain; i++) 
		{
			currentManaPoints += 1;
			ClampMana();
		}
	}

	public void ManaRegen(float manaRegenRate, int amountGainPerTick)// TODO Double check the ManaRegen Method in PlayerManager to see if it is not to expensive
	{
		
		if( Time.time > regenTick)
		{
			AbilityManaGain(amountGainPerTick);
			regenTick = Time.time + manaRegenRate;
		}
	}

	public void OnDestroyed()
	{
		Debug.Log("Player Gameobject has been destroyed");
	}

	public override void MovementSpeedChange(float a)//TODO Speed Change Logic
	{
		totalmovementdecrease += a;

		if (totalmovementdecrease < 0) {
		//	Stats.Speed = 0.1f;
			GetComponent<PlayerController> ().CurrentSpeed = 0.1f;
		} else {
			//	Stats.Speed = totalmovementdecrease;
			GetComponent<PlayerController> ().CurrentSpeed = totalmovementdecrease;
		}

	}

	public void GotTheKill(int a){
		
		Debug.Log ("Score " + a);
	}

	public void ResetWord()
	{
		//Debug.Log ("Score " + a);
//		GetComponent<PlayerTyping> ().ResetTheText ();
	}

	public override void VelocityChange (float moveValue){
	
		//Do Some Player Spesific Stuff If You Want


		//base.VelocityChange (moveValue, goDirection);//Isnt Used, Base Is Empty
		/*if (StunImmunity <= ClockTest.TheTimes) {
			StunImmunity = ClockTest.TheTimes + 1;
			velocityPushback = true;
			_MyRigidbody.velocity = goDirection.normalized * moveValue;
		}*/
		//GetComponent<PlayerController>().canPlayerMove = false;
	//	GetComponent<Rigidbody2D>().velocity = goDirection.normalized * moveValue;
	}
}
