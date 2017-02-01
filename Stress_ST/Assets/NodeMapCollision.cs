using UnityEngine;
using System.Collections;
using System;

public class NodeMapCollision : MonoBehaviour {

	DefaultBehaviour _ParentBehaviour;
	const String Wall = "Wall";
	const String Enemy = "Enemy";

	void Start(){
		_ParentBehaviour = transform.parent.GetComponent<DefaultBehaviour> ();
	}

	void OnTriggerEnter2D(Collider2D coll){//when this object collides with a wall, tell the parent to update
		if(coll.gameObject.CompareTag(Wall)){
			_ParentBehaviour.AddWallWithTrigger (coll.gameObject);
		}else if (coll.gameObject.CompareTag (Enemy)) {
			_ParentBehaviour.AddEnemyWithTrigger (coll.gameObject);
		}
	}
	void OnTriggerExit2D(Collider2D coll){//when this object exits a wall, tell the parent to update
		if (coll.gameObject.CompareTag (Wall)) {
			_ParentBehaviour.RemoveWallWithTrigger (coll.gameObject);
		} else if (coll.gameObject.CompareTag (Enemy)) {
			_ParentBehaviour.RemoveWallWithTrigger (coll.gameObject);
		}
	}
}
