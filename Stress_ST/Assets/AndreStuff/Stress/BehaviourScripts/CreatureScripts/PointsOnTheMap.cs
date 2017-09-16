using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOnTheMap : DefaultBehaviourPosition {

	public Wall_ID PointPlacement;
	const int _NewMapCenter = -100;//Previour Center Was 0,0. That Caused Some Problems When The Player Was On A 0 Value. -0.9 == 0. 0.9 = 0. So That Fixed It But That Means That You Cant Go Below -100xy. Change This To Change The Center
	const float _NodeDimentions = 0.08f;//update CreatureBehaviour -> NodeMapCollision -> PlayerManager

	void Awake(){ 

		MyPos [0, 0] = ((transform.position.x - _NewMapCenter) / _NodeDimentions) - (((transform.position.x - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map
		MyPos [0, 1] = ((transform.position.y - _NewMapCenter) / _NodeDimentions) - (((transform.position.y - _NewMapCenter) / _NodeDimentions) % 1);//Calculating Object World Position In The Node Map

		MyNode [0] = new Nodes (MyPos, 0);

		SetAiRoom (PointPlacement);

	}


}
