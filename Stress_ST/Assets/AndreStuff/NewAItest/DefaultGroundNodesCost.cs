using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefaultGroundNodesCost {



//	[HideInInspector]
//	public CollisionMapInfo[] NodeCost = new CollisionMapInfo[SceneSetupTest.AmountOfDifferentNodes];//How Many Different NodeValues. Sand, Water, Mud....... You Get It

	[Tooltip("Base Level Node Strength. Sets An Overall 'Strength' To The Node, Which Tells The Nodes How Expensive It Is To Walk On")]
	public byte[] TheNodeCost = new byte[StressCommonlyUsedInfo.AmountOfDifferentNodes];//How Many Different NodeValues. Sand, Water, Mud....... You Get It

	/*
	Grass
	Sand
	Gravel
	Quicksand
	Mud
	Water
	Fire
	Walls
	Whole
	Bridge
	NodesThatMeanDeath
	*/


	public DefaultGroundNodesCost(){
	
		for (int i = 0; i < StressCommonlyUsedInfo.AmountOfDifferentNodes; i++) {//Giving The First Node A ValueIndex. So That When Sand Is Present On The Map, It Knows Its Index And The NodeCost Is Set(When Calculating Suff).
		//	NodeCost [i] = new CollisionMapInfo ();
		//	NodeCost [i].NodesCollisionID = (byte)i;
		}


	}




}
