using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNodeInfo : MonoBehaviour {

	public CollisionMapInfo MyCollisionInfo;//Everything Needs To Have This To Be Able To Calculate Where The Object Is In The Nodemap

	public void Awake(){
		MyCollisionInfo.CalculateNodePos (transform.position);
	}

}
