using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GoToDestination : MonoBehaviour {

	/*

	this is the place where the call to create the path is made, and the movement to end point is calculated here

	*/

	GameObject _GoAfter;
	AStartPathfinding_RoomPaths _CreateThePath = new AStartPathfinding_RoomPaths();

	int _ListIndex = 0;
	int _Nodeindex = 0;
	int _Reversed = 0;
	float _Distance = 1;
	float _DistFromX = 0;
	float _DistFromY = 0;
	float _DistFromX2 = 0;
	float _DistFromY2 = 0;

	List<List<Nodes>> _ThePaths;
	List<Nodes> _Walking;

	void Start(){
		_GoAfter = GameObject.FindGameObjectWithTag ("Player1");
		Debug.Log ("Setting GameObject.FindGameObjectWithTag (\"Player1\") Remember to remove this later");

		_CreateThePath.SetStartRoomAndNode (GetComponent<WhichRoomObjectAt> ().GetRoomTargetAt (),  GetComponent<WhichRoomObjectAt> ().GetNodeTargetAt ());
		UpdateTargetRoomAndNode ();
	}


	public void SetTarget(GameObject target){//if you want to change target, use this, TODO make it so that its possible to only send the node
		_GoAfter = target;
		UpdateTargetRoomAndNode ();
	} 

	void UpdateTargetRoomAndNode(){//sending the endpoints to the A*
		_CreateThePath.SetEndRoomAndNode (_GoAfter.GetComponent<WhichRoomObjectAt>().GetRoomTargetAt(), _GoAfter.GetComponent<WhichRoomObjectAt>().GetNodeTargetAt());
	}
		
	public void MakeNewPathSearch(){//sending a request to make the path again if nothing is null
		if (_CreateThePath.IsSomethingNull () == true) {
			_ThePaths =	_CreateThePath.CreatePath (); 
			_ListIndex = 0;
			_Walking = null;
		} else {
			_ThePaths = null;
		} 
	} 

	public void GoToNextNode(){//calculating where to move. (the next element in the list(of the list)) TODO tiny error here i think, havent managed to reproduse it
 
		if (_ThePaths != null && _ListIndex < _ThePaths.Count) {
			if (_ThePaths [_ListIndex] != _Walking) {
				_Walking = _ThePaths [_ListIndex];

				_DistFromX = _Walking.First ().GetID () [0, 0] - transform.position.x;
				_DistFromY = _Walking.First ().GetID () [0, 1] - transform.position.y;
				
				if (_DistFromX < 0)
					_DistFromX *= -1;
				if (_DistFromY < 0)
					_DistFromY *= -1;

				_DistFromX2 = _Walking.Last ().GetID () [0, 0] - transform.position.x;
				_DistFromY2 = _Walking.Last ().GetID () [0, 1] - transform.position.y;

				if (_DistFromX2 < 0)
					_DistFromX2 *= -1;
				if (_DistFromY2 < 0)
					_DistFromY2 *= -1;

				if (_DistFromX + _DistFromY < _DistFromX2 + _DistFromY2) {
					_Reversed = 1;
					_Nodeindex = 0;
				} else {
					_Reversed = -1;
					_Nodeindex = _Walking.Count - 1;
				}

				while (!(_Nodeindex < 0 || _Nodeindex >= _Walking.Count)) {
					if (((_Walking [_Nodeindex].GetID () [0, 0] - _Distance) < transform.position.x) && ((_Walking [_Nodeindex].GetID () [0, 0] + _Distance) > transform.position.x) && ((_Walking [_Nodeindex].GetID () [0, 1] - _Distance) < transform.position.y) && ((_Walking [_Nodeindex].GetID () [0, 1] + _Distance) > transform.position.y)) {
						_Nodeindex += _Reversed;
					} else {
						break;
					}
				}
			}

			if (_Nodeindex < _Walking.Count && _Nodeindex >= 0) {
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (_Walking [_Nodeindex].GetID () [0, 0], _Walking [_Nodeindex].GetID () [0, 1]), 0.05f);

				if (((_Walking [_Nodeindex].GetID () [0, 0] - _Distance) < transform.position.x) && ((_Walking [_Nodeindex].GetID () [0, 0] + _Distance) > transform.position.x) && ((_Walking [_Nodeindex].GetID () [0, 1] - _Distance) < transform.position.y) && ((_Walking [_Nodeindex].GetID () [0, 1] + _Distance) > transform.position.y)) {
					_Nodeindex += _Reversed;
				}
				if (_Nodeindex < 0 || _Nodeindex >= _Walking.Count) {
					_ListIndex += 1;
				}
			} else {
				if (_Nodeindex < 0 || _Nodeindex >= _Walking.Count) {
					_ListIndex += 1;
				}
			}
		}
	}
}
