
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAI : ObjectStats {

	public The_Object_Behaviour TheObject;//Anything Inhereting From This Have Object Logic


	public override void SetTarget(GameObject target){
		TheObject._TheTarget = target.GetComponent<AbsoluteRoot> ();
	}

	#region What to do when colliding with objects  

	public virtual void AddStaticObject(BoxCollider2D collidingwithobject){
		TheObject._PersonalNodeMap.AddWalls (collidingwithobject);
	}

	public virtual void RemoveStaticObjects(BoxCollider2D collidingwithobject){
		TheObject._PersonalNodeMap.RemoveWalls (collidingwithobject);
	}

	public virtual void AddEnemy (BoxCollider2D collidingwithobject){
		TheObject._PersonalNodeMap.AddEnemyPositions (collidingwithobject);
	}

	public virtual void RemoveEnemy (BoxCollider2D collidingwithobject){
		TheObject._PersonalNodeMap.RemoveEnemyPositions (collidingwithobject);
	}

	public virtual void RemoveMyselfFromOthers (){}

	#endregion


}
