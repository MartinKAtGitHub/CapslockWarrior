using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {


	public int HealthPoints;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		isPlayerAlive();
	}

	public void TakeDamage(int damageAmount)
	{
		HealthPoints =- damageAmount;
	}

	private void isPlayerAlive()
	{
		if(HealthPoints <= 0)
		{
			Debug.Log("<color=blue>PLAYER IS DEAD/color>");
		}
	}
}
