using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CollisionMapInfo {//Where 1 is just this one node. 2 for going out on the side of the center node

	//Center Node Is The Transform.Position Turned Into NodePosition.
//	[HideInInspector]
	public int XNode = 0;
//	[HideInInspector]
	public int YNode = 0;

	[Tooltip("These 4 Variables Tells The NodeMap That This Object Take x,y Amount Of Nodes From Center. Where 1,1,1,1 == One Node, 2,2,2,2 == 4 Nodes")]
	[HideInInspector]public int NodesLeft = 1;
	[HideInInspector]	public int NodesRight = 1;
	[HideInInspector]public int NodesUp = 1;
	[HideInInspector]public int NodesDown = 1;

	[Tooltip("0 == Zero Cost, 1 == Normal Ground, 2 == Undestructable Walls, 3 == Destroyable Walls, 4 == fall To Death, 5 == ")]
	[HideInInspector]public byte NodesCollisionID = 2;

	float SaveVariable = 0;

	public void CalculateNodePos(Vector3 pos){

		SaveVariable = pos.x / StressCommonlyUsedInfo.DistanceBetweenNodes;

		if(SaveVariable < 0){
			if(SaveVariable % 1 < -0.5f){
				XNode = Mathf.FloorToInt(SaveVariable);
			}else{
				XNode = Mathf.CeilToInt(SaveVariable);
			}
		}else{
			if(SaveVariable % 1 > 0.5f){
				XNode = Mathf.CeilToInt(SaveVariable);
			}else{
				XNode = Mathf.FloorToInt(SaveVariable);
			}
		}

		SaveVariable = pos.y / StressCommonlyUsedInfo.DistanceBetweenNodes;

		if(SaveVariable < 0){
			if(SaveVariable % 1 < -0.5f){
				YNode = Mathf.FloorToInt(SaveVariable);
			}else{
				YNode = Mathf.CeilToInt(SaveVariable);
			}
		}else{
			if(SaveVariable % 1 > 0.5f){
				YNode = Mathf.CeilToInt(SaveVariable);
			}else{
				YNode = Mathf.FloorToInt(SaveVariable);
			}
		}


	}
}
