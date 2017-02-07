using UnityEngine;
using System.Collections;
using System;

public class NodeMapCollision : MonoBehaviour {

	MovingCreatures _ParentBehaviour;
	const String Wall = "Wall";
	const String Enemy = "Enemy";

	void Start(){
		_ParentBehaviour = transform.parent.GetComponent<MovingCreatures> ();
	}

	void OnTriggerEnter2D(Collider2D coll){//when this object collides with a wall, tell the parent to update
		if(coll.gameObject.CompareTag(Wall)){
			_ParentBehaviour.AddWallWithTrigger (coll.gameObject);
		}else if (coll.gameObject.CompareTag (Enemy)) {
			if (coll.GetComponent<MovingCreatures> () != null) {
			_ParentBehaviour.AddEnemyWithTrigger (coll.gameObject);
			}
		}
	}
	void OnTriggerExit2D(Collider2D coll){//when this object exits a wall, tell the parent to update
		if (coll.gameObject.CompareTag (Wall)) {
			_ParentBehaviour.RemoveWallWithTrigger (coll.gameObject);
		} else if (coll.gameObject.CompareTag (Enemy)) {
			if (coll.GetComponent<MovingCreatures> () != null) {
				_ParentBehaviour.RemoveWallWithTrigger (coll.gameObject);
			}
		}
	}
}
