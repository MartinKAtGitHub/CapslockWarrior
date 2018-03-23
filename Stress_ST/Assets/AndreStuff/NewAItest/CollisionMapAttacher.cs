using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollisionMapAttacher {

	public static AStarTest testin;

	public static List<ObjectNodeInfo> UpdateGroundObjects = new List<ObjectNodeInfo>();//Walls, Pillars, Bridges spawned outside of sprite base ground layer
	public static List<ObjectNodeInfo> UpdateEnviourmentalObjects = new List<ObjectNodeInfo>();//Blizzard, Sandstorm, Enviourmental stuff
	public static List<ObjectNodeInfo> UpdateAllTheTimeObjects = new List<ObjectNodeInfo>();//Spells which are Updated Constantly

	public static List<ObjectNodeInfo> RemoveSpellEffect = new List<ObjectNodeInfo>();


	public static void NewSceen(){
		UpdateGroundObjects.Clear();
		UpdateEnviourmentalObjects.Clear();
		UpdateAllTheTimeObjects.Clear();
	}

	public static void RemoveFromGround (ObjectNodeInfo me){
		UpdateGroundObjects.Remove(me);
//		testin._WalkCost.RemoveGroundObjects (me.MyCollisionInfo);
	}

	public static void RemoveEnvourmental (ObjectNodeInfo me){
		UpdateEnviourmentalObjects.Remove(me);
//		testin._WalkCost.RemoveEnviourmentEffect (me.MyCollisionInfo);
	}

	public static void AddToRemoveSpell (ObjectNodeInfo me){
		RemoveSpellEffect.Add (me);
	}

	public static void RemoveFromUpdateAllTheTimeList(ObjectNodeInfo me){
		UpdateAllTheTimeObjects.Remove(me);
	}

}
