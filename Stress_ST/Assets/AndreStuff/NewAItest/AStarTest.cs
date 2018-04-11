using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarTest : MonoBehaviour {

//	public DefaultGroundNodesCost GroundNodeCost;

	public NodeWalkcostSetter _WalkCost;



	float[] PathfindingNodeID = new float[StressCommonlyUsedInfo.PathCostSize];//Currently Normalground 0 - Wall 1 - Other Creatures 2. TODO add More And Remember To Update
	NodeTest[] _OpenList = new NodeTest[StressCommonlyUsedInfo.NodesTotal];//list that holds nodes that i havent searched through
	NodeTest[] _ClosedList = new NodeTest[StressCommonlyUsedInfo.NodesTotal];//list that have been searched through

	public NodeTest[] ol(){//Delete this
		return _OpenList;
	}
	public NodeTest[] cl(){//Delete this
		return _ClosedList;
	}

	NodeTest[,] _NodeMap = new NodeTest[StressCommonlyUsedInfo.NodesWidth , StressCommonlyUsedInfo.NodesWidth];


	NodeTest _TheStartNode; //Will hold the refrence to the startnode 
	NodeTest _EndNode; //Will hold the refrence to the endnode

	int[,] CollisionID;


	int _NodeIndexSaved = 0; //used to store the index of a node
	int _OpenListAtIndex = 0; //this holds the next position which to add the next element
	int _ClosedListAtIndex = 0; //closed list index holder

	NodeTest _NodeHolder = null; //Holds The Neighbour Node Of CurrentNode
	NodeTest _CurrentNode; //Holds The Node The A* Is Searching With, In The Current Loop Iteration 
	float _LowerstFScore = 100000; //This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased

	int ArrayLengthSaver = 0;

	int	_NodeXPos = 0;
	int	_NodeYPos = 0;


	public void Setup(){

	//	_WalkCost.SetNodeSize (sceneStartup);//Creating Node Cost Array

		int Added = 0;

		for (int x = 0; x < StressCommonlyUsedInfo.NodesWidth; x++) {//Creating Middle Nodes
			for (int y = 0; y < StressCommonlyUsedInfo.NodesWidth; y++) {
				if ((x == 0 && y == 0) || (x == 0 && y == StressCommonlyUsedInfo.NodesWidth - 1) || (x == StressCommonlyUsedInfo.NodesWidth - 1 && y == 0) || (x == StressCommonlyUsedInfo.NodesWidth - 1 && y == StressCommonlyUsedInfo.NodesWidth - 1)) {
					_NodeMap [x, y] = new NodeTest (x, y, 0);
				} else if (x == 0 || x == StressCommonlyUsedInfo.NodesWidth - 1 || y == 0 || y == StressCommonlyUsedInfo.NodesWidth - 1) {
					_NodeMap [x, y] = new NodeTest (x, y, 1);
				} else {
					_NodeMap [x, y] = new NodeTest (x, y, 2);
				}
				_NodeMap [x, y].PathfindingNodeID = PathfindingNodeID;
	
			}
		}


		for (int x = 1; x < StressCommonlyUsedInfo.NodesWidth - 1; x++) {//Giving Middle Nodes Neighbour Nodes
			for (int y = 1; y < StressCommonlyUsedInfo.NodesWidth - 1; y++) {
				Added = 0;
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if (_NodeMap [x, y] != _NodeMap [x + k, y + h]) {
							_NodeMap [x, y].NeighbourNodess [Added++] = _NodeMap [x + k, y + h];
						}
					}
				}
			}
		}


		for (int x = 0; x < StressCommonlyUsedInfo.NodesWidth; x += (StressCommonlyUsedInfo.NodesWidth - 1)) {//Giving Corner Nodes Neighbour Nodes
			for (int y = 0; y < StressCommonlyUsedInfo.NodesWidth; y += (StressCommonlyUsedInfo.NodesWidth - 1)) {

				Added = 0;
				if (x == 0) {
					if (y == 0) {//Down Left
						for (int k = 0; k < 2; k++) {
							for (int h = 0; h < 2; h++) {
								if (_NodeMap [x, y] != _NodeMap [x + k, y + h]) {
									_NodeMap [x, y].NeighbourNodess [Added++] = _NodeMap [x + k, y + h];
								}
							}
						}
					} else {//Up Left
						for (int k = 0; k < 2; k++) {
							for (int h = -1; h < 1; h++) {
								if (_NodeMap [x, y] != _NodeMap [x + k, y + h]) {
									_NodeMap [x, y].NeighbourNodess [Added++] = _NodeMap [x + k, y + h];
								}
							}
						}
					}
				} else {//Down Right
					if (y == 0) {
						for (int k = -1; k < 1; k++) {
							for (int h = 0; h < 2; h++) {
								if (_NodeMap [x, y] != _NodeMap [x + k, y + h]) {
									_NodeMap [x, y].NeighbourNodess [Added++] = _NodeMap [x + k, y + h];
								}
							}
						}
					} else {//Up Right
						for (int k = -1; k < 1; k++) {
							for (int h = -1; h < 1; h++) {
								if (_NodeMap [x, y] != _NodeMap [x + k, y + h]) {
									_NodeMap [x, y].NeighbourNodess [Added++] = _NodeMap [x + k, y + h];
								}
							}
						}
					}
				}
			}
		}

		for (int x = 1; x < StressCommonlyUsedInfo.NodesWidth - 1; x++) {//Giving X-Side Nodes Neighbour Nodes
			for (int y = 0; y < StressCommonlyUsedInfo.NodesWidth; y += (StressCommonlyUsedInfo.NodesWidth - 1)) {

				Added = 0;
				if (y == 0) {

					for (int k = -1; k < 2; k++) {
						for (int h = 0; h < 2; h++) {
							if (_NodeMap [x, y] != _NodeMap [x + k, y + h]) {
								_NodeMap [x, y].NeighbourNodess [Added++] = _NodeMap [x + k, y + h];
							}
						}
					}

				} else {

					for (int k = -1; k < 2; k++) {
						for (int h = -1; h < 1; h++) {
							if (_NodeMap [x, y] != _NodeMap [x + k, y + h]) {
								_NodeMap [x, y].NeighbourNodess [Added++] = _NodeMap [x + k, y + h];
							}
						}
					}

				}
			}
		}

		for (int x = 0; x < StressCommonlyUsedInfo.NodesWidth; x += (StressCommonlyUsedInfo.NodesWidth - 1)) {//Creating Y-SideNodes
			for (int y = 1; y < StressCommonlyUsedInfo.NodesWidth - 1; y++) {
		
				Added = 0;
				if (x == 0) {

					for (int k = 0; k < 2; k++) {
						for (int h = -1; h < 2; h++) {
							if (_NodeMap [x, y] != _NodeMap [x + k, y + h]) {
								_NodeMap [x, y].NeighbourNodess [Added++] = _NodeMap [x + k, y + h];
							}
						}
					}

				} else {

					for (int k = -1; k < 1; k++) {
						for (int h = -1; h < 2; h++) {
							if (_NodeMap [x, y] != _NodeMap [x + k, y + h]) {
								_NodeMap [x, y].NeighbourNodess [Added++] = _NodeMap [x + k, y + h];
							}
						}
					}

				}
			}
		}


	

	//	_TheStartNode = _NodeMap [StressCommonlyUsedInfo.NodeDimentions / 2 - 1,StressCommonlyUsedInfo.NodeDimentions / 2 - 1];
		_TheStartNode = _NodeMap [((StressCommonlyUsedInfo.NodesWidth - 1) / 2),((StressCommonlyUsedInfo.NodesWidth - 1) / 2)];
		_OpenListAtIndex = 0;
		_ClosedListAtIndex = 0;

	}

	float one,two;

	public void StartRunning (ObjectNodeInfo me, NodeInfo meNodeInfo, ObjectNodeInfo taget){
	
		if (_TheStartNode == null) {
			Setup ();
		}

		#region Startup Phase

		CollisionID = meNodeInfo.MyNodes;

		for (int i = 0; i < PathfindingNodeID.Length; i++) {//'Copying' Over The Objects Pathfinding Node Cost Over
			PathfindingNodeID [i] = meNodeInfo.PathfindingNodeID [i];
		}


		for (int i = 0; i < _OpenListAtIndex; i++) {//The Used Nodes Is In These Two Lists. So I Only Need To Reset The Nodes Used
			_OpenList [i].NodeSearchedThrough = false;
		}

		for (int i = 0; i < _ClosedListAtIndex; i++) {
			_ClosedList [i].NodeSearchedThrough = false;
		}

		_OpenListAtIndex = 0;
		_ClosedListAtIndex = 0;

		_NodeXPos = Mathf.FloorToInt(_TheStartNode.PosX + (taget.MyCollisionInfo.XNode - me.MyCollisionInfo.XNode));
		_NodeYPos = Mathf.FloorToInt(_TheStartNode.PosY + (taget.MyCollisionInfo.YNode - me.MyCollisionInfo.YNode));

		if (_NodeXPos >= StressCommonlyUsedInfo.NodesWidth) {
			_NodeXPos = StressCommonlyUsedInfo.NodesWidth - 1;
		}else if(_NodeXPos < 0){
			_NodeXPos = 0;
		}
		if (_NodeYPos >= StressCommonlyUsedInfo.NodesWidth) {
			_NodeYPos = StressCommonlyUsedInfo.NodesWidth - 1;
		}else if(_NodeYPos < 0){
			_NodeYPos = 0;
		}

		_EndNode = _NodeMap [_NodeXPos, _NodeYPos];
		_TheStartNode.SetStartNode (_TheStartNode, _EndNode);//Giving The StartNode its Costs
	
		_OpenList [_OpenListAtIndex++] = _TheStartNode;//Giving The Search Through List Its First Node


		#endregion

		#region A* Algorythm

		while (_ClosedListAtIndex < StressCommonlyUsedInfo.NodesTotal) {//If The ClosedListAtIndex Is Equalt To Or Greater The Total Amount Of Nodes Then This Is False And The Search Is Stopped
			_LowerstFScore = 10000000;

			for (int i = 0; i < _OpenListAtIndex; i++) {//Iterating Through The List With Unused Nodes To Find The Node With The Lowerst FCost
				
				if (_OpenList [i].FCost < _LowerstFScore) {
					_CurrentNode = _OpenList [i];
					_NodeIndexSaved = i;
					_LowerstFScore = _CurrentNode.FCost;
				}
			} 

			_ClosedList [_ClosedListAtIndex++] = _CurrentNode;//The Node That Was Chosen From Openlist Is Put In Here
			_OpenList [_NodeIndexSaved] = _OpenList [--_OpenListAtIndex];//The Node That Was Added To ClosedList Is Being Removed Here And Replaced With The Last Node In The Openlist


			if (_CurrentNode == _EndNode) {//If True Then A Path Was Found And The Search Is Complete
				RemakePath (meNodeInfo);
				return;
			}

			ArrayLengthSaver = _CurrentNode.NeighbourNodess.Length;//This Is An Improvement Rather Then Getting The Length Each i++ (Not Much But Some)

			for (int i = 0; i < ArrayLengthSaver; i++) {

				_NodeHolder = _CurrentNode.NeighbourNodess [i];
			
				if (_NodeHolder.NodeSearchedThrough == false) {//If false Then The Node Havent Been Searched Through And Info Need To Be Set
					_NodeHolder.MapCollision = 0;//Giving The Node Its Value For Calculating Move Cost
				//	_NodeHolder.MapCollision = CollisionID [_NodeHolder.PosX, _NodeHolder.PosY];//Giving The Node Its Value For Calculating Move Cost
					_NodeHolder.SetParentAndHCost (_CurrentNode, _EndNode);
				//	Static_Node.SetParentAndHCost(_NodeHolder, _CurrentNode, _EndNode);
					_OpenList [_OpenListAtIndex++] = _NodeHolder;
				} else if (_CurrentNode.GCost < _NodeHolder._ParentNode.GCost) {//If Current.Gcost Is Less Then Nodeholder.parent.Gcost Then A New ParentNode Is Set  ...... If Errors Occur Use (_NodeHolder.GCost > _CurrentNode.GCost + (PathfindingNodeID[CollisionID[_NodeHolder.PosX, _NodeHolder.PosY]] * 1.4f))
					_NodeHolder.SetNewParent (_CurrentNode);
				//	Static_Node.SetNewParent(_NodeHolder, _EndNode);
				}
			}

		}

		#endregion

		Debug.LogWarning ("No Path Detected, Initiating Self Destruct Algorythms.... 3..... 2..... 1..... .");

	}

	void RemakePath(NodeInfo meNodeInfo) {//Backtracking From The EndNode. When The Backtracking Reaches The StartNode, Then The Path Is Set

		ArrayLengthSaver = 0;//Just A Reused Variable For Holding The Index Number For The Next Node To Enter In The Path

		_CurrentNode = _EndNode;
		meNodeInfo.MyNodePath [ArrayLengthSaver++] = _CurrentNode;


		if (_EndNode == _TheStartNode) {
			return;
		} else {
			_CurrentNode = _EndNode;
		}

		while (true) {//Going Backwards Parent To Parent To Parent.....
			if (_CurrentNode._ParentNode != _TheStartNode) {
				meNodeInfo.MyNodePath [ArrayLengthSaver++] = _CurrentNode._ParentNode;
				_CurrentNode = _CurrentNode._ParentNode;
			} else {
				meNodeInfo.MyNodePath [ArrayLengthSaver++] = _CurrentNode._ParentNode;
				meNodeInfo.MyNodePath [ArrayLengthSaver--] = null;//Setting This To Be 'null' To Symbolize That This Is The End

				for (int i = 0; i < ArrayLengthSaver / 2; i++){//Turning The List Around
					_CurrentNode = meNodeInfo.MyNodePath [i];
					meNodeInfo.MyNodePath [i] = meNodeInfo.MyNodePath [ArrayLengthSaver - i];
					meNodeInfo.MyNodePath [ArrayLengthSaver - i] = _CurrentNode;
				}

				return;
			}
		}

	}
		
	public bool testa = false;
	void Update(){
	
		if (testa == true) {
			testa = false;
		//	_WalkCost = new NodeWalkcostSetter ();

		}

	}



}



/*

while (_ClosedListAtIndex < TotalNodes) {//If The ClosedListAtIndex Is Equalt To Or Greater The Total Amount Of Nodes Then This Is False And The Search Is Stopped
			_LowerstFScore = 10000000;

			for (int i = 0; i < _OpenListAtIndex; i++) {//Iterating Through The List With Unused Nodes To Find The Node With The Lowerst FCost
				_NodeHolder = _OpenList [i];
				if (_NodeHolder.FCost < _LowerstFScore) {
					_CurrentNode = _NodeHolder;
					_NodeIndexSaved = i;
					_LowerstFScore = _CurrentNode.FCost;
				}
			} 

		
			_ClosedList [_ClosedListAtIndex++] = _CurrentNode;//The Node That Was Chosen From Openlist Is Put In Here

			if (_CurrentNode == _EndNode) {//If True Then A Path Was Found And The Search Is Complete
				RemakePath ();
				return;
			}

			_OpenList [_NodeIndexSaved] = _OpenList [--_OpenListAtIndex];//The Node That Was Added To ClosedList Is Being Removed Here And Replaced With The Last Node In The Openlist


			ArrayLengthSaver = _CurrentNode.NeighbourNodess.Length;//This Is An Improvement Rather Then Getting The Length Each i++ (Not Much But Some)
			for (int i = 0; i < ArrayLengthSaver; i++) {


_NodeHolder = _CurrentNode.NeighbourNodess [i];


if (_NodeHolder.NodeSearchedThrough == false) {//If false Then The Node Havent Been Searched Through And Info Need To Be Set
	_NodeHolder.MapCollision = CollisionID [_NodeHolder.PosX, _NodeHolder.PosY];//Giving The Node Its Value For Calculating Move Cost

	_NodeHolder.NodeSearchedThrough = true;
	_NodeHolder._ParentNode = _CurrentNode;

	_XValue = _EndNode.PosX - _NodeHolder.PosX;
	_YValue = _EndNode.PosY - _NodeHolder.PosY;

	if (_XValue < 0)
		_XValue *= -1;
	if (_YValue < 0)
		_YValue *= -1;

	_NodeHolder._HCost = _XValue + _YValue;
	_NodeHolder.GCost = (PathfindingNodeID[_NodeHolder.MapCollision] * 1.4f) + _CurrentNode.GCost;
	_NodeHolder.FCost = _NodeHolder._HCost + _NodeHolder.GCost;


	_OpenList [_OpenListAtIndex++] = _NodeHolder;

} else if (_CurrentNode.GCost >  _NodeHolder._ParentNode.GCost) {//If Current.Gcost Is Less Then Nodeholder.parent.Gcost Then A New ParentNode Is Set |||| If Errors Occur Use (_NodeHolder.GCost > _CurrentNode.GCost + (PathfindingNodeID[CollisionID[_NodeHolder.PosX, _NodeHolder.PosY]] * 1.4f))

	_NodeHolder._ParentNode = _CurrentNode;
	_NodeHolder.GCost = (PathfindingNodeID[_NodeHolder.MapCollision] * 1.4f) + _CurrentNode.GCost;
	_NodeHolder.FCost = _NodeHolder._HCost + _NodeHolder.GCost;

}
}

*/