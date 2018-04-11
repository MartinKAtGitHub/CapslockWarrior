using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeInfo  {

	public AStarTest MyAStar;


	[Tooltip("PathfindingNodeID is the cost to move to the different nodes //// 0 = normal nodes //// 1 = undestructable walls //// 2 = other units")]
	public float[] PathfindingNodeID = new float[StressCommonlyUsedInfo.PathCostSize];//Currently Normalground 0 - Wall 1 - Other Creatures 2. TODO add More
	[HideInInspector]
	float[] BasePathfindingNodeCost = new float[StressCommonlyUsedInfo.PathCostSize];//This Is The List That Stores Standard Values. These Can Changed, (scenairo) An Ice Creature Turns Info A Fire Creature, Then You Might Want Fire Nodes To Be Much More Desirable To Walk On. Then The Standard Is Also Changed. But If Its A Spell Effect Then Change The Array Above


	[HideInInspector]
	public int[,] MyNodes = new int[StressCommonlyUsedInfo.NodesWidth, StressCommonlyUsedInfo.NodesWidth];//Holds The Collision ID Of the NodeMAp

	[HideInInspector]
	public NodeTest[] MyNodePath = new NodeTest[StressCommonlyUsedInfo.NodesTotal + 1];


	public void AddOrRemoveNodeCost(int index, float cost){//Add/Remove Cost

		PathfindingNodeID [index] += cost;

	}

	public void SetNewBaseNodeCost(float[] cost){//TODO Apply CurrentNodeCost Increase Over? (Spell Active)
	
		for(int i = 0; i < StressCommonlyUsedInfo.PathCostSize; i++){
		
			BasePathfindingNodeCost [i] = cost [i];

		}

	}

	public void UpdatePathCost(){//TODO If PathfindingNodeID Is Affected By A Spell Then BasePathfinding Needs To Apply That To?

		for(int i = 0; i < StressCommonlyUsedInfo.PathCostSize; i++){

			PathfindingNodeID [i] = BasePathfindingNodeCost [i];

		}

	}

}
