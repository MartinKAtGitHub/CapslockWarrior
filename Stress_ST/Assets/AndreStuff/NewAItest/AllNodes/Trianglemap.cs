using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trianglemap : MonoBehaviour {

	public NodeWalkcostSetter _WalkCost;



	float[] PathfindingNodeID = new float[StressCommonlyUsedInfo.PathCostSize];//Currently Normalground 0 - Wall 1 - Other Creatures 2. TODO add More And Remember To Update
	NodeTest[] _OpenList = new NodeTest[StressCommonlyUsedInfo.NodesTotal];//list that holds nodes that i havent searched through
	NodeTest[] _ClosedList = new NodeTest[StressCommonlyUsedInfo.NodesTotal];//list that have been searched through

	public NodeTest[] ol() {//Delete this
		return _OpenList;
	}
	public NodeTest[] cl() {//Delete this
		return _ClosedList;
	}

	NodeTest[,] _NodeMap = new NodeTest[StressCommonlyUsedInfo.NodesWidth, StressCommonlyUsedInfo.NodesWidth];


	NodeTest _TheStartNode; //Will hold the refrence to the startnode 
	NodeTest _EndNode; //Will hold the refrence to the endnode


	//	int[,] CollisionID;


	int _NodeIndexSaved = 0; //used to store the index of a node
	int _OpenListAtIndex = 0; //this holds the next position which to add the next element
	int _ClosedListAtIndex = 0; //closed list index holder

	NodeTest _NodeHolder = null; //Holds The Neighbour Node Of CurrentNode
	NodeTest _CurrentNode; //Holds The Node The A* Is Searching With, In The Current Loop Iteration 
	float _LowerstFScore = 100000; //This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased

	int ArrayLengthSaver = 0;

	int _NodeXPos = 0;
	int _NodeYPos = 0;


	/*public void Setup() {

		//	_WalkCost.SetNodeSize (sceneStartup);//Creating Node Cost Array
		int Added = 0;

		for (int x = 0; x < StressCommonlyUsedInfo.NodesWidth; x++) {//Creating Middle Nodes
			for (int y = 0; y < StressCommonlyUsedInfo.NodesWidth; y++) {
				if ((x == 0 && y == 0) || (x == 0 && y == StressCommonlyUsedInfo.NodesWidth - 1) || (x == StressCommonlyUsedInfo.NodesWidth - 1 && y == 0) || (x == StressCommonlyUsedInfo.NodesWidth - 1 && y == StressCommonlyUsedInfo.NodesWidth - 1)) {
					_NodeMap[x, y] = new NodeTest(x, y, 0);
				} else if (x == 0 || x == StressCommonlyUsedInfo.NodesWidth - 1 || y == 0 || y == StressCommonlyUsedInfo.NodesWidth - 1) {
					_NodeMap[x, y] = new NodeTest(x, y, 1);
				} else {
					_NodeMap[x, y] = new NodeTest(x, y, 2);
				}
				_NodeMap[x, y].PathfindingNodeID = PathfindingNodeID;

			}
		}


		for (int x = 1; x < StressCommonlyUsedInfo.NodesWidth - 1; x++) {//Giving Middle Nodes Neighbour Nodes
			for (int y = 1; y < StressCommonlyUsedInfo.NodesWidth - 1; y++) {
				Added = 0;
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
							_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
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
								if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
									_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
								}
							}
						}
					} else {//Up Left
						for (int k = 0; k < 2; k++) {
							for (int h = -1; h < 1; h++) {
								if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
									_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
								}
							}
						}
					}
				} else {//Down Right
					if (y == 0) {
						for (int k = -1; k < 1; k++) {
							for (int h = 0; h < 2; h++) {
								if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
									_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
								}
							}
						}
					} else {//Up Right
						for (int k = -1; k < 1; k++) {
							for (int h = -1; h < 1; h++) {
								if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
									_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
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
							if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
								_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
							}
						}
					}

				} else {

					for (int k = -1; k < 2; k++) {
						for (int h = -1; h < 1; h++) {
							if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
								_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
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
							if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
								_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
							}
						}
					}

				} else {

					for (int k = -1; k < 1; k++) {
						for (int h = -1; h < 2; h++) {
							if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
								_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
							}
						}
					}

				}
			}
		}




		//	_TheStartNode = _NodeMap [StressCommonlyUsedInfo.NodeDimentions / 2 - 1,StressCommonlyUsedInfo.NodeDimentions / 2 - 1];
		_TheStartNode = _NodeMap[((StressCommonlyUsedInfo.NodesWidth - 1) / 2), ((StressCommonlyUsedInfo.NodesWidth - 1) / 2)];
		_OpenListAtIndex = 0;
		_ClosedListAtIndex = 0;

	}*/
}