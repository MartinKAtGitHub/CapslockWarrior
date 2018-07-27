using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeWalkcostSetter {

	public BaseTile[,] BaseGround = new BaseTile[StressCommonlyUsedInfo.NodesWidth, StressCommonlyUsedInfo.NodesWidth];//This Is The Base Ground. Like A Wall Or Lava Or Something Like Those Where There Are Nothing Beneath

	public BaseTile[,,] NonMovingObjects = new BaseTile[StressCommonlyUsedInfo.NodesWidth, StressCommonlyUsedInfo.NodesWidth, StressCommonlyUsedInfo.Nodes3D];//Non-BaseGround StandStill Objects. Pools Of Poinson, Turrets, Destroyable Walls
	public int[,] NonMovingObjectsLayer = new int[(StressCommonlyUsedInfo.NodesWidth), (StressCommonlyUsedInfo.NodesWidth)];//Migth Removing This. Only An Array That Holds NonMovingObjects Length

	public BaseTile[,,] MovingObjects = new BaseTile[StressCommonlyUsedInfo.NodesWidth, StressCommonlyUsedInfo.NodesWidth, StressCommonlyUsedInfo.Nodes3D];//Any Object That Can Move
	public int[,] MovingObjectsLayer = new int[(StressCommonlyUsedInfo.NodesWidth), (StressCommonlyUsedInfo.NodesWidth)];//Migth Removing This. Only An Array That Holds MovingObjects Length


	public void BaseGroundTiles(int a, int b, BaseTile changeTile) {

		BaseGround[a, b] = changeTile;

	}

	/// <summary>
	/// Using This Method Implies That Said Object Never Or Rarely Moves. This Is To Reduce The SeachLoad On Object That Move
	/// </summary>
	/// <param name="a">X Position</param>
	/// <param name="b">Y Position</param>
	/// <param name="changeTile">The Tile That Are Being Changed</param>
	public void AddNonMovingObjectsTiles(int a, int b, BaseTile changeTile) {
		Debug.Log(NonMovingObjectsLayer[a, b]);
		NonMovingObjects[a, b, (++NonMovingObjectsLayer[a,b] - 1)] = changeTile;

	}

	/// <summary>
	/// Remove Tile Which Are Stationary
	/// </summary>
	/// <param name="a">X Position</param>
	/// <param name="b">Y Position</param>
	/// <param name="changeTile">The Tile That Are Being Changed</param>
	public void RemoveNonMovingObjectsTiles(int a, int b, BaseTile changeTile) {

		for (int i = 0; i < NonMovingObjectsLayer[a, b]; i++) {
			if(NonMovingObjects[a, b, i] == changeTile) {
				NonMovingObjects[a, b, i] = null;
				NonMovingObjects[a, b, i] = NonMovingObjects[a, b, --NonMovingObjectsLayer[a, b]];
				return;
			}
		}

	}

	/// <summary>
	/// Using This Method For Object That Moves Quite Alot.
	/// </summary>
	/// <param name="a">X Position</param>
	/// <param name="b">Y Position</param>
	/// <param name="changeTile">The Tile That Are Being Changed</param>
	public void AddMovingObjectsTiles(int a, int b, BaseTile changeTile) {

		MovingObjects[a, b, (++MovingObjectsLayer[a, b] - 1)] = changeTile;

	}

	/// <summary>
	/// Remove Moving Object, Seperating Object Reduces The Cost Of Search And Replace
	/// </summary>
	/// <param name="a">X Position</param>
	/// <param name="b">Y Position</param>
	/// <param name="changeTile">The Tile That Are Being Changed</param>
	public void RemoveMovingObjectsTiles(int a, int b, BaseTile changeTile) {

		for (int i = 0; i < MovingObjectsLayer[a, b]; i++) {
			if (MovingObjects[a, b, i] == changeTile) {
				MovingObjects[a, b, i] = null;
				MovingObjects[a, b, i] = MovingObjects[a, b, --MovingObjectsLayer[a, b]];
				return;
			}
		}

	}

	//TODO Add Fancy Adding Methods, Like Give Circle Dimention And Add And Remove, Rectangles, Triangles...... This Is Just To Reduce The Call Amount, Instead Of The Tile Itself Calculating And Sending Node 1 == 0, Node 2 == 1 For Every Node Changed. If We Are Changing 1000 Nodes Then That Is 1000 Method Calls.
	//So.... TODO Improvement!




	/*	int LeftNodePos = 0;
		int RightNodePos = 0;
		int UpNodePos = 0;
		int DownNodePos = 0;

		CollisionMapInfo ZeroCost = new CollisionMapInfo();
		CollisionMapInfo DefaultCost = new CollisionMapInfo();


		public byte[,] CollisionAmount = new byte[(StressCommonlyUsedInfo.NodesWidth),(StressCommonlyUsedInfo.NodesWidth)];//This Holds NodeCost Length To Easely Update And Read The CollisoinMap
		public CollisionMapInfo[,,] CollisionMap = new CollisionMapInfo[StressCommonlyUsedInfo.NodesWidth,StressCommonlyUsedInfo.NodesWidth, StressCommonlyUsedInfo.Nodes3D];//This Holds NodeCostInfo
		public byte[,] BaseGroundLayer = new byte[(StressCommonlyUsedInfo.NodesWidth),(StressCommonlyUsedInfo.NodesWidth)];//SpriteInfo. This Will Never Change Untill The Sprite Is Changed/New Scenario Started Or Scene Loaded







		public Vector2 WorldCornerPosition = new Vector2();

		public void SetNodeSize(SceneSetupTest sceneStartup){//Only Happends Once + Can Be Changed On Every Map, Maybe Even Multiple Times Each Map

			ZeroCost.NodesCollisionID = 0;
			DefaultCost.NodesCollisionID = 1;

			for (int i = 0; i < (StressCommonlyUsedInfo.NodesWidth); i++) {
				for (int j = 0; j < (StressCommonlyUsedInfo.NodesWidth); j++) {
					for (int h = 0; h < StressCommonlyUsedInfo.Nodes3D; h++) {
						if (h == 0) {
							CollisionMap [i, j, h] = DefaultCost;
						} else {
							CollisionMap [i, j, h] = ZeroCost;
						}
					}
				}
			}
		}


		public void AddBaseGroundLayer(byte[,] cost, int x, int y){//Setting Node Value To The cost Values

			LeftNodePos = cost.GetLength (0);//Getting Dimention A
			RightNodePos = x - (LeftNodePos / 2);//Getting Lowest X Value
			UpNodePos = cost.GetLength (1);//Getting Dimention B
			DownNodePos = y - (UpNodePos / 2);//Getting Lowest Y Value

			for (int i = 0; i < LeftNodePos; i++) {
				for (int j = 0; j < UpNodePos; j++) {
					BaseGroundLayer [RightNodePos + i, DownNodePos + j] = cost[i,j];//Setting The NodeValue From GroundSprite To The NodeMap
				}
			}

		}

		public void RemoveBaseGroundLayer(byte[,] cost, int x, int y){//Resetting Node Value To 0 Which Is Standard Walking Node

			LeftNodePos = cost.GetLength (0);//Getting Dimention A
			RightNodePos = x - (LeftNodePos / 2);//Getting Lowest X Value
			UpNodePos = cost.GetLength (1);//Getting Dimention B
			DownNodePos = y - (UpNodePos / 2);//Getting Lowest Y Value

			for (int i = 0; i < LeftNodePos; i++) {
				for (int j = 0; j < UpNodePos; j++) {
					BaseGroundLayer [RightNodePos + i, DownNodePos + j] = 0;//Setting The NodeValue From GroundSprite To The NodeMap
				}
			}

		}















		public void RemoveSpellSquare (CollisionMapInfo spell){//The Spell Isnt Removed But The Spot Itself Is Decreased

			LeftNodePos = spell.XNode - spell.NodesLeft;
			if (LeftNodePos < 1) {//The 0 Node Is The Edge Which Is Closed Off
				LeftNodePos = 1;
			}

			RightNodePos = spell.XNode + spell.NodesRight;
			if (RightNodePos >= (StressCommonlyUsedInfo.NodesWidth - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
				RightNodePos = (StressCommonlyUsedInfo.NodesWidth) - 2;
			}

			UpNodePos = spell.YNode + spell.NodesUp;
			if (UpNodePos >= (StressCommonlyUsedInfo.NodesWidth - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
				UpNodePos = (StressCommonlyUsedInfo.NodesWidth) - 2;
			}

			DownNodePos = spell.YNode - spell.NodesDown;
			if (DownNodePos < 1) {//The 0 Node Is The Edge Which Is Closed Off
				DownNodePos = 1;
			}


			for(int i = LeftNodePos; i < RightNodePos; i++){
				for(int j = DownNodePos; j < UpNodePos; j++){

					CollisionAmount [i, j] -= 1;

				}
			}


		}

		public void RemoveGroundObjects (CollisionMapInfo ground){//This Is For Static Objects That Stand Still And Is Mainly Walls, Wholes And Bridges And So On.....

			LeftNodePos = ground.XNode - ground.NodesLeft;
			if (LeftNodePos < 1) {//The 0 Node Is The Edge Which Is Closed Off
				LeftNodePos = 1;
			}

			RightNodePos = ground.XNode + ground.NodesRight;
			if (RightNodePos >= (StressCommonlyUsedInfo.NodesWidth - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
				RightNodePos = (StressCommonlyUsedInfo.NodesWidth) - 2;
			}

			UpNodePos = ground.YNode + ground.NodesUp;
			if (UpNodePos >= (StressCommonlyUsedInfo.NodesWidth - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
				UpNodePos = (StressCommonlyUsedInfo.NodesWidth) - 2;
			}

			DownNodePos = ground.YNode - ground.NodesDown;
			if (DownNodePos < 1) {//The 0 Node Is The Edge Which Is Closed Off
				DownNodePos = 1;
			}


			for(int i = LeftNodePos; i < RightNodePos; i++){
				for(int j = DownNodePos; j < UpNodePos; j++){

					CollisionMap [i, j, 1] = ZeroCost;

				}
			}

		}

		//This Is To Apply/Remove The Map Base Environmental Spots, Like If There Is An Area With Alot Of Heat Or A Blizzard.
		public void RemoveEnviourmentEffect (CollisionMapInfo ground){//This Is For When There Are A Blizzard In An Area, Which You Only Need To Update Once, Where The Newest Effect Nullifies The Old On The Overlapping Area

			LeftNodePos = ground.XNode - ground.NodesLeft;
			if (LeftNodePos < 1) {//The 0 Node Is The Edge Which Is Closed Off
				LeftNodePos = 1;
			}

			RightNodePos = ground.XNode + ground.NodesRight;
			if (RightNodePos >= (StressCommonlyUsedInfo.NodesWidth - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
				RightNodePos = (StressCommonlyUsedInfo.NodesWidth) - 2;
			}

			UpNodePos = ground.YNode + ground.NodesUp;
			if (UpNodePos >= (StressCommonlyUsedInfo.NodesWidth - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
				UpNodePos = (StressCommonlyUsedInfo.NodesWidth) - 2;
			}

			DownNodePos = ground.YNode - ground.NodesDown;
			if (DownNodePos < 1) {//The 0 Node Is The Edge Which Is Closed Off
				DownNodePos = 1;
			}


			for(int i = LeftNodePos; i < RightNodePos; i++){
				for(int j = DownNodePos; j < UpNodePos; j++){

					CollisionMap [i, j, 0] = ZeroCost;

				}
			}

		}

		int thePos = 0;

		public int CornerNodePosX = 0;
		public int CornerNodePosY = 0;

		public void Setups(){
			CornerNodePosX = (int)(WorldCornerPosition.x / StressCommonlyUsedInfo.DistanceBetweenNodes);
			CornerNodePosY = (int)(WorldCornerPosition.y / StressCommonlyUsedInfo.DistanceBetweenNodes);
		}

		public bool ValidPositions(int x, int y){
			if (x < CornerNodePosX) {
				return false;
			} 
			if (x >= StressCommonlyUsedInfo.NodesWidth + CornerNodePosX) {
				return false;
			}

			if (y < CornerNodePosY) {
				return false;
			}
			if (y >= StressCommonlyUsedInfo.NodesWidth + CornerNodePosY) {
				return false;
			}

			return true;

		}

		public int GetXPos(int x){//Getting The Base Node Of The Position

			if (x < CornerNodePosX) {
				thePos = 0;
			} else if (x >= StressCommonlyUsedInfo.NodesWidth + CornerNodePosX) {
				thePos = StressCommonlyUsedInfo.NodesWidth - 1;
			} else {
				thePos = x - CornerNodePosX;
			}


			return thePos;
		}

		public int GetYPos(int y){//Getting The Base Node Of The Position

			if (y < CornerNodePosY) {
				thePos = 0;
			} else if (y >= StressCommonlyUsedInfo.NodesWidth + CornerNodePosY) {
				thePos = StressCommonlyUsedInfo.NodesWidth - 1;
			} else {
				thePos = y - CornerNodePosY;
			}


			return thePos;
		}
		*/


}
