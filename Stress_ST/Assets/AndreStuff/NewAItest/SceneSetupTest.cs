using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetupTest : MonoBehaviour {

	public const int NodesInUnityMeter = 20;
	public const float DistanceBetweenNodes = 0.0625f;
	public const int TotalNodes = (int)(NodesInUnityMeter / DistanceBetweenNodes);
	public const int Nodes3D = 10;
	public const int AmountOfDifferentNodes = 10;

	public AStarTest testin;

	void Awake(){
	//	CollisionMapAttacher.NewSceen ();
	//	CollisionMapAttacher.testin = testin;
	//	testin.Setup ();
	//	Application.targetFrameRate = 60;//Makes Update Run 60 Times A Sec

	}

}
