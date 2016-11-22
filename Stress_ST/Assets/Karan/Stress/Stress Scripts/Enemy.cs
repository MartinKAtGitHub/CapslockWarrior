using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


	public int SpawnAmount; // TODO for now we need to set the amount we want to spawn a certain type of enemy here.
	//TODO if we can connect this to the Spawner script in some way, so we can just set it there it would be optimal.

	// Use this for initialization
	void Start () 
	{
		Debug.Log("Spawned = " + name);
	}


}
