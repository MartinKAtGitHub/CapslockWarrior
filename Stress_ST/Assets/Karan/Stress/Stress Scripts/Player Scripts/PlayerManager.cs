using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]

public class PlayerManager : DefaultBehaviour {


	public int HealthPoints;

	public Text HealtPoints_Txt;
	public Text ManaPoints_Txt;

	void Awake(){
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;	
		MyNode [0] = new Nodes (myPos, 0);
	}

	// Update is called once per frame
	void Update () 
	{
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;

		isPlayerAlive();
	}

	public void TakeDamage(int damageAmount)
	{
		Debug.Log("HP REMAINING =" + (damageAmount / HealthPoints) * 100);

		HealthPoints -= damageAmount;

	}

	private void isPlayerAlive()
	{
		if(HealthPoints <= 0)
		{
			Debug.Log("<color=blue>PLAYER IS DEAD</color>:");
		}
	}

	public override void OnDestroyed(){

	}
	public override void AttackTarget(){

	}
	public override void RecievedDmg(int _damage){
		Debug.Log("LOLOLOLOLOLOLO = " + _damage);
	}
	public override void ChangeMovementAdd(float a){

	}

	public override void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){
		NeighbourGroups = neighbours;
	}

}
