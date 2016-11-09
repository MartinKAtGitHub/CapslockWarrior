using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class CreateRoomLinks : MonoBehaviour {

	HashSet<Rooms> _OpenList = new HashSet<Rooms>();//Holds the nodes that im going to search
	HashSet<Rooms> _ClosedList = new HashSet<Rooms>();//Holds the nodes that have been search
	List<Nodes> _CurrentPath = new List<Nodes>();//node path
	List<Rooms> _ThePath = new List<Rooms>();//room path


	Rooms[,] _TheNodeMap;//This holds the roommap refrence
	Rooms _StartNode; //This is the room that this object is an and starts searching from 
	Rooms[] _EndNode; //end room, destination room. i have to do it like this because then i get the memory path which i only have to get once

	Rooms _NodeSaver; //just to hold some nodes while searching
	Rooms _NSaver; //another node holder

	float _LowerstFScore = 100000; //TODO (not realy a todo but just read the comment ;-p) remember to switch this value below tooooooooooooooooo. This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased
	public int GoAfterPlayer = 1;//Might need to improve this when we start on the multiplayer part, TODO

	int _Counter = 0;


	void Start(){
		_EndNode = GameObject.FindGameObjectWithTag ("Player" + GoAfterPlayer).GetComponent<WhichRoomPlayerAt>().GetPathfindingRoom();//gets a refrence to the players current room
	}

	void OnCollisionEnter2D(Collision2D coll) {
		_StartNode = coll.gameObject.GetComponent<Rooms> ();//gets objects current room
	}



	void Update() {

		if (!(_ThePath.Count () > 0)) {//if the rooms to the player is greater then 0 this does not run
			if (_EndNode [0] != null && _StartNode != null && (_StartNode != _EndNode [0])) {//if the start room == end room then dont run
				CreatePath (_StartNode, _EndNode [0]);
				_Counter = 0;

				if (_ThePath.Count > 1) {
					_CurrentPath = _ThePath [0].GetComponent<RoomsPathCalculation> ().GetMiddleOfRoomPath (_ThePath [0], _ThePath [1], _ThePath [0].GetComponent<RoomsPathCalculation> ().GetMyNode (gameObject));//adds the walkable path to the objects walkpath
				}
			}
		} else if (_ThePath.Count () > 0) {
			if (_EndNode [0] != _ThePath.Last()) {//if player went to different room run the A* again
				CreatePath (_StartNode, _EndNode [0]);
				_Counter = 0;

				if (_ThePath.Count > 1) {//add the path to the walkpath again
					_CurrentPath = _ThePath [0].GetComponent<RoomsPathCalculation> ().GetMiddleOfRoomPath (_ThePath [0], _ThePath [1], _ThePath [0].GetComponent<RoomsPathCalculation> ().GetMyNode (gameObject));
				}

			}

			if (_CurrentPath.Count() - 1 > _Counter ) {//if the object isnt at the last node
				if (((transform.position.x < (_CurrentPath[_Counter].GetID()[0,0] + 0.5f)) && (transform.position.x > (_CurrentPath[_Counter].GetID () [0, 0] - 0.5f))) && ((transform.position.y < (_CurrentPath[_Counter].GetID () [0, 1] + 0.5f)) && (transform.position.y > (_CurrentPath[_Counter].GetID () [0, 1] - 0.5f)))) { //checking if enemy is inside its goto node
					_Counter++;

				}
				if (_CurrentPath.Count () - 1 >= _Counter) {//Goes to the goto node
					transform.position = Vector2.MoveTowards (transform.position, new Vector2 (_CurrentPath[_Counter].GetID () [0, 0], _CurrentPath[_Counter].GetID () [0, 1]), 0.1f);
				}

				if (_CurrentPath.Count () - 1 <= _Counter) {//if object is at last node 
					if (_ThePath.Count () > 2) {//if there are more then 2 rooms left
						_CurrentPath = _ThePath [1].GetComponent<RoomsPathCalculation> ().GetPath (_ThePath [0], _ThePath [2], _ThePath [0].GetComponent<RoomsPathCalculation> ().GetMyNode (gameObject));
						_Counter = 0;
						_ThePath.RemoveAt (0);
					} else if (_ThePath.Count () > 1) {//if there are more then 1 room left
						_CurrentPath = _ThePath [1].GetComponent<RoomsPathCalculation> ().GetLastRoomPath (_ThePath [1].GetComponent<RoomsPathCalculation> ().GetMyNode (gameObject));
						_Counter = 0;
						_ThePath.RemoveAt (0);
					} else {
						Debug.Log ("uhm what am i doing here?");
					}
				}
			}	
		}
	}
		
	public void CreatePath(Rooms startNode, Rooms endNode) {//Starts A* and clears all the Rooms so that they are rdy for the next search
		AStartAlgorithm(startNode, endNode);

		foreach (Rooms n in _OpenList) {
			n.ClearAll();
		}
		foreach (Rooms n in _ClosedList) {
			n.ClearAll();
		}
	}

	public  void AStartAlgorithm(Rooms starts, Rooms ends)  {//A*. 
		_OpenList.Clear();
		_ClosedList.Clear();
		_NodeSaver = null;//Holds the current node that im searching with

		starts.SetHCost(ends);
		_OpenList.Add(starts);

		while (_OpenList.Count > 0) {

			_LowerstFScore = 100000;

			foreach (Rooms n in _OpenList) {//Goes through the _OpenList to search after the lowest F value, 			TODO improve this in some way, if i can manage to somehow sort this in an efficient way then (Y) will improve performance alot but has to be sorted thought. i can make a dictionary of sorts and search after key (just some searches to find lowest in a hugh list?)
				if (n.GetFValue() < _LowerstFScore) {
					_NodeSaver = n;
					_LowerstFScore = n.GetFValue();
				}
			}
				
			_OpenList.Remove(_NodeSaver);
			_ClosedList.Add(_NodeSaver);

			if (_NodeSaver == ends) {//If _NodeSaver == ends then the search is complete and sending _closedlist to calculate the path from start to end
				RemakePath(_ClosedList.Last());
				return ;
			}

			foreach (KeyValuePair<GameObject,float> n in _NodeSaver.GetNeighbours()) {
				_NSaver = n.Key.GetComponent<Rooms> ();
				if (_NSaver.GetCollision() != 1) {
					if (_NSaver.GetParent() == null && _NSaver != starts) {//if n dont have a parent and n isnt the starting node (starts must be there else it will cause an FATAL error ;-P, your warned :D)
						_NSaver.SetParent(_NodeSaver);
						_NSaver.SetHCost(ends);
					} else if (_NSaver.GetGCost() > _NodeSaver.GetGCost() + n.Value) {//calculates best route through these values
						_NSaver.SetParent(_NodeSaver);
					}
					if (_OpenList.Contains(_NSaver) == false && _ClosedList.Contains(_NSaver) == false) {//adds all neighbour to openlist if it isnt already there or if it hasnt already been searched through
						_OpenList.Add(_NSaver);
					}
				}
			}
		}
		RemakePath(_ClosedList.Last());
	}

	public void RemakePath(Rooms checkedNodes) {//Makes the path by going to the end node and get the parent, then parent of the parent ....... until your at the start node
		_ThePath.Clear();
		_NodeSaver = checkedNodes;

		_ThePath.Add(_NodeSaver);
		while (true) {
			if (_NodeSaver != null && _NodeSaver.GetParent() != null) {
				_ThePath.Add(_NodeSaver.GetParent());
				_NodeSaver = _NodeSaver.GetParent();
			} else {
				_ThePath.Reverse ();
				return;
			}
		}
	}


/*	public int drawgizmos = 0;

	void OnDrawGizmos(){

		if (drawgizmos == 1) {
			float a;
			float b;

			Gizmos.color = Color.yellow;
			foreach (Rooms n in _OpenList) {
				a = n.GetID () [0, 1];
				b = n.GetID () [0, 0];

				Gizmos.DrawCube (new Vector3 (b, a, 105), new Vector3 (0.9f, 0.9f, 1));
			}
			Gizmos.color = Color.red;
			foreach (Rooms n in _ClosedList) {
				a = n.GetID () [0, 1];
				b = n.GetID () [0, 0];

				Gizmos.DrawCube (new Vector3 (b, a, 105), new Vector3 (0.9f, 0.9f, 1));
			}
			Gizmos.color = Color.blue;
			foreach (Rooms n in _ThePath) {
				a = n.GetID () [0, 1];
				b = n.GetID () [0, 0];

				Gizmos.DrawCube (new Vector3 (b, a, 105), new Vector3 (0.9f, 0.9f, 1));
			}

			Gizmos.color = Color.grey;
			Gizmos.DrawCube (new Vector3(transform.position.x, transform.position.y, 105), new Vector3 (0.9f, 0.9f, 1));
		}
	}*/
}