using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhichRoomObjectAt : MonoBehaviour {

	/*

	this script finds the current room/node on the object that this script is on

	*/

	RoomsPathCalculation[] _TargetRoom = new RoomsPathCalculation[1];
	Nodes[] _TargetNode = new Nodes[1];

	List<GameObject> _ColliderList = new List<GameObject>();
	public bool DissableUpdate = false;

	void OnCollisionEnter2D(Collision2D coll) {//if this object enters a room then that room is added to the list which im searching through late to see which im iside

		if(coll.gameObject.tag == "Walkable"){
			if (_ColliderList.Count == 0) {
				_ColliderList.Add (coll.gameObject);
			} else {
				if (_ColliderList.Contains (coll.gameObject) == false) {
					_ColliderList.Add (coll.gameObject);
				}
			}
			ChooseStarTRoom ();
		}
	}

	void OnCollisionExit2D(Collision2D coll) {//if this object exits a room then that room is removed to the list which im searching through late to see which im iside

		if(coll.gameObject.tag == "Walkable"){
			if (_ColliderList.Contains (coll.gameObject) == true) {
				_ColliderList.Remove (coll.gameObject);
			}
			ChooseStarTRoom ();
		}
	}


	void Update(){

		if (DissableUpdate == true) {
			return;
		} else {
			if (_TargetRoom [0] != null) //Im updating which node the object is on every update call
				_TargetNode [0] = _TargetRoom [0].GetMyNode (this.gameObject);
		}
	}
		

	void ChooseStarTRoom(){//here im deciding which room the object is in, or the end room for the A* search
		if (_ColliderList.Count == 1) {
			_TargetRoom [0] = _ColliderList [0].GetComponent<RoomsPathCalculation> ();
			_TargetNode [0] = _TargetRoom [0].GetMyNode (gameObject);
		} else {//if this.position is inside the collider2d borders. then set the end room to that room, (s).
			foreach (GameObject s in _ColliderList) {
				if ((s.GetComponent<Collider2D> ().bounds.min.x < this.transform.position.x) && (s.GetComponent<Collider2D> ().bounds.max.x > this.transform.position.x) && (s.GetComponent<Collider2D> ().bounds.min.y < this.transform.position.y) && (s.GetComponent<Collider2D> ().bounds.max.y > this.transform.position.y)) {
					_TargetRoom [0] = s.GetComponent<RoomsPathCalculation> ();
					_TargetNode [0] = _TargetRoom [0].GetMyNode (gameObject);
					return;
				}
			} 
		}
	}
		
	public RoomsPathCalculation[] GetRoomTargetAt(){
		return _TargetRoom;
	}

	public Nodes[] GetNodeTargetAt(){
		return _TargetNode;
	}
}