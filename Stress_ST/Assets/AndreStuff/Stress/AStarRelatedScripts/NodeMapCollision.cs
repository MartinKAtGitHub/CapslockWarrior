﻿using UnityEngine;
using System.Collections;
using System;

//There Are Two Different Objects, Those That Can Move And Those That Can't.
public class NodeMapCollision : MonoBehaviour {

	const float _NodeDimentions = 0.08f;

	public ObjectAI _ParentBehaviour;
	const String Wall = "Wall";
	const String CreatureCollider = "CreatureCollider";
	Vector2 MyPosition = Vector2.zero;

	void Start(){
		MyPosition.x = _ParentBehaviour.MyPos [0, 0] * _NodeDimentions - 100;
		MyPosition.y = _ParentBehaviour.MyPos [0, 1] * _NodeDimentions - 100;
	}

	void Update(){
		MyPosition.x = _ParentBehaviour.MyPos [0, 0] * _NodeDimentions - 100;//Calculating A Clipping Position For The WalkingCollider
		MyPosition.y = _ParentBehaviour.MyPos [0, 1] * _NodeDimentions - 100;//Calculating A Clipping Position For The WalkingCollider
		transform.position = MyPosition;
	} 

	void OnTriggerEnter2D(Collider2D coll){//when this object collides with a wall, tell the parent to update
		if (_ParentBehaviour.TheObject._TheTarget != null) {
			
			if (_ParentBehaviour.TheObject._TheTarget.gameObject != coll.transform.parent.gameObject) {
				if (coll.gameObject.CompareTag (Wall)) {
					_ParentBehaviour.AddStaticObject (coll.GetComponent<BoxCollider2D> ());
				} else if (coll.gameObject.CompareTag (CreatureCollider)) {
					if (coll.transform.parent.gameObject != _ParentBehaviour.gameObject)
						_ParentBehaviour.AddEnemy (coll.GetComponent<BoxCollider2D> ());
				}
			}
		}
	}
	void OnTriggerExit2D(Collider2D coll){//when this object exits a wall, tell the parent to update
		if (_ParentBehaviour.TheObject._TheTarget != null) {
			if (_ParentBehaviour.TheObject._TheTarget.gameObject != coll.transform.parent.gameObject) {
				if (coll.gameObject.CompareTag (Wall)) {
					_ParentBehaviour.RemoveStaticObjects (coll.GetComponent<BoxCollider2D> ());
				} else if (coll.gameObject.CompareTag (CreatureCollider)) {
					if (coll.transform.parent.gameObject != _ParentBehaviour.gameObject)
						_ParentBehaviour.RemoveEnemy (coll.GetComponent<BoxCollider2D> ());
				}
			}
		}
	}
}
 