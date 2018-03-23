using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static_Node {

	static int x = 0;
	static int y = 0;

	public static void SetStartNode(NodeTest me, NodeTest theEnd) {//setting parent gcost and hcost
		me.NodeSearchedThrough = true;
		me._ParentNode = me;

		x = theEnd.PosX - me.PosX;
		y = theEnd.PosY - me.PosY;

		if (x < 0)
			x *= -1;
		if (y < 0)
			y *= -1;

		me._HCost = x + y;
		me.GCost = 0;
		me.FCost = me._HCost;
	}

	public static void SetParentAndHCost(NodeTest me, NodeTest theParent, NodeTest theEnd) {//setting parent gcost and hcost
		me.NodeSearchedThrough = true;
		me._ParentNode = theParent;

		x = theEnd.PosX - me.PosX;
		y = theEnd.PosY - me.PosY;

		if (x < 0)
			x *= -1;
		if (y < 0)
			y *= -1;

		me._HCost = x + y;
		me.GCost = (me.PathfindingNodeID[me.MapCollision] * 1.4f) + me._ParentNode.GCost;
		me.FCost = me._HCost + me.GCost;
	}

	public static void SetNewParent(NodeTest me, NodeTest theParent) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

		me._ParentNode = theParent;
		me.GCost = (me.PathfindingNodeID[me.MapCollision] * 1.4f) + me._ParentNode.GCost;
		me.FCost = me._HCost + me.GCost;
	}

}
