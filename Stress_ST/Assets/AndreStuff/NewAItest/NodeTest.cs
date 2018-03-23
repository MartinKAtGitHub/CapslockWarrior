using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTest  {

	public bool NodeSearchedThrough = false;//if bool is used to check if the node have been searched through

	public float[] PathfindingNodeID;//this array holds the move cost to each tile (wall , water, sand .....)
	public NodeTest[] NeighbourNodess;//neighbours for the nodes

	public int PosX = 0;
	public int PosY = 0;

	public int MapCollision = 1;//this is used to set the correct id for the tile (wall, water, sand .....)

	int _XValue = 0, _YValue = 0; //just some values that im using

	public NodeTest _ParentNode = null;
	public float GCost = 0;//GCost is the cost that have been used to get to this node
	public float FCost = 0;//Gcost+Hcost
	public int _HCost = 0;//HCost is how far away the end node is from this node


	public NodeTest(int posX, int posY, int placement) {

		PosX = posX;
		PosY = posY;

		if (placement == 0) {//Corner
			NeighbourNodess = new NodeTest[3];
		}else if (placement == 1) {//Side
			NeighbourNodess = new NodeTest[5];
		}else {//Middle
			NeighbourNodess = new NodeTest[8];
		}


	}

	public void SetStartNode(NodeTest theParent, NodeTest theEnd) {//setting parent gcost and hcost
		NodeSearchedThrough = true;
		_ParentNode = theParent;

		_XValue = theEnd.PosX - PosX;
		_YValue = theEnd.PosY - PosY;

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;
		GCost = 0;
		FCost = _HCost;
	}

	public void SetParentAndHCost(NodeTest theParent, NodeTest theEnd) {//setting parent gcost and hcost
		NodeSearchedThrough = true;
		_ParentNode = theParent;
	
		_XValue = theEnd.PosX - PosX;
		_YValue = theEnd.PosY - PosY;


		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;
	//	GCost = (1 * 1.4f) + _ParentNode.GCost;

		if (theParent.PosX - PosX + theParent.PosY - PosY == 0 || theParent.PosX - PosX + theParent.PosY - PosY == 2 || theParent.PosX - PosX + theParent.PosY - PosY == -2) {
			GCost = (1 * 1.4f) + _ParentNode.GCost;
		} else {
			GCost = (1 * 1f) + _ParentNode.GCost;
		}
		FCost = _HCost + GCost;
	}

	public void SetNewParent(NodeTest theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

		_ParentNode = theParent;
		if (theParent.PosX - PosX + theParent.PosY - PosY == 0 || theParent.PosX - PosX + theParent.PosY - PosY == 2 || theParent.PosX - PosX + theParent.PosY - PosY == -2) {
			GCost = (1 * 1.4f) + _ParentNode.GCost;
		} else {
			GCost = (1 * 1f) + _ParentNode.GCost;
		//	Debug.Log (PathfindingNodeID[MapCollision]);
		}

		FCost = _HCost + GCost;
	}

}