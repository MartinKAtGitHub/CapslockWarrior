using UnityEngine;
using System.Collections;

public class WhichRoomPlayerAt : MonoBehaviour {

	//updates player info, so that the we know which node the player is on and which room he/she is in

	Rooms[] _PathfindingRoom = new Rooms[1];
	Nodes[] _MyNode = new Nodes[1];

	void OnCollisionEnter2D(Collision2D coll) {
		_PathfindingRoom[0] = coll.gameObject.GetComponent<Rooms>();
	}


	public Rooms[] GetPathfindingRoom(){
		return _PathfindingRoom;
	}



	public Nodes[] GetNodeImAt(){
		return _MyNode;
	}

	void Update(){
			_MyNode[0] = _PathfindingRoom [0].GetComponent<RoomsPathCalculation> ().GetMyNode (this.gameObject);
	}
}