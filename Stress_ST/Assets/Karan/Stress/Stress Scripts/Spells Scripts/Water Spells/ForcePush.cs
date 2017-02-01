using UnityEngine;
using System.Collections;

public class ForcePush : Water {

	public float ForcePushDestroyAfter;
	GameObject ForcePushArea;
	private ParticleSystem ForceParticalSystem;
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

	public override void Cast()
	{
		ForcePushArea = (GameObject)Instantiate(this.gameObject, SpellSpawnPos.position,Quaternion.identity);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			//Debug.Log(other.name);
			if(other.GetComponent<DefaultBehaviour>() != null)//TODO
				other.GetComponent<DefaultBehaviour>().StopMoveLogic = true;
		//	other.GetComponent<GolumMovementTest>().IsMoving = false;

		}
	}



}
