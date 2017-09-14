using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOnTheMap : DefaultBehaviourPosition {

	public Wall_ID PointPlacement;

	void Awake(){ 

		MyPos [0, 0] = transform.position.x; 
		MyPos [0, 1] = transform.position.y;	
		MyNode [0] = new Nodes (MyPos, 0);

		SetAiRoom (PointPlacement);

	}


}
