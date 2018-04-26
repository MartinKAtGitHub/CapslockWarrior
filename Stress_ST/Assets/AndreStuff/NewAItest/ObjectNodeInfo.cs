using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNodeInfo : MonoBehaviour {

	[Tooltip("Attach The AI Behaviour.")]
	public CreatureRoot ObjectWithBehaviour;
	public CollisionMapInfo MyCollisionInfo;//Everything Needs To Have This To Be Able To Calculate Where The Object Is In The Nodemap

	[HideInInspector]
	public bool UpdateEnabled = false;

	public void Awake(){
		ObjectWithBehaviour = GetComponent<CreatureRoot> ();
		MyCollisionInfo.CalculateNodePos (transform.position);
	}

	public void Update(){

		if (UpdateEnabled == true) {
			MyCollisionInfo.CalculateNodePos (transform.position);
		}

	}

}
