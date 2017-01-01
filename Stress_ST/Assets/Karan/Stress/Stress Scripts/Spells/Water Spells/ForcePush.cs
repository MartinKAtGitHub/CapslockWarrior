﻿using UnityEngine;
using System.Collections;

public class ForcePush : Water {

	public float ForcePushDestroyAfter;
	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public override void Cast()
	{
		GameObject ForcePushArea = (GameObject)Instantiate(this.gameObject, SpellSpawnPos.position,Quaternion.identity);
		Destroy(ForcePushArea,ForcePushDestroyAfter);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			Debug.Log(other.name);
			other.GetComponent<GolumMovementTest>().IsMoving = false;

		}
	}



}
