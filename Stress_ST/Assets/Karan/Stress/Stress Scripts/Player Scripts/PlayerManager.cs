using UnityEngine;
using System.Collections;

public class PlayerManager : DefaultBehaviour {


	public int HealthPoints;

	void Awake(){
		myPos [0, 0] = transform.position.x;
		myPos [0, 1] = transform.position.y;	
		MyNode [0] = new Nodes (myPos, 0);
	}

	// Use this for initialization
	void Start () 
	{
		
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
	public override void RecievedDmg(){

	}
	public override void ChangeMovementAdd(float a){

	}


}
