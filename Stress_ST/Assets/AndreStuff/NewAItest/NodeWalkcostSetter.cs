using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeWalkcostSetter {

	int LeftNodePos = 0;
	int RightNodePos = 0;
	int UpNodePos = 0;
	int DownNodePos = 0;

	CollisionMapInfo ZeroCost = new CollisionMapInfo();
	CollisionMapInfo DefaultCost = new CollisionMapInfo();


	public byte[,] CollisionAmount = new byte[(SceneSetupTest.TotalNodes),(SceneSetupTest.TotalNodes)];//This Holds NodeCost Length To Easely Update And Read The CollisoinMap
	public CollisionMapInfo[,,] CollisionMap = new CollisionMapInfo[(SceneSetupTest.TotalNodes),(SceneSetupTest.TotalNodes), 10];//This Holds NodeCostInfo
	public byte[,] BaseGroundLayer = new byte[(SceneSetupTest.TotalNodes),(SceneSetupTest.TotalNodes)];//SpriteInfo. This Will Never Change Untill The Sprite Is Changed/New Scenario Started Or Scene Loaded




	public void SetNodeSize(SceneSetupTest sceneStartup){//Only Happends Once + Can Be Changed On Every Map, Maybe Even Multiple Times Each Map


		ZeroCost.NodesCollisionID = 0;
		DefaultCost.NodesCollisionID = 1;

		for (int i = 0; i < (SceneSetupTest.TotalNodes); i++) {
			for (int j = 0; j < (SceneSetupTest.TotalNodes); j++) {
				for (int h = 0; h < SceneSetupTest.Nodes3D; h++) {
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
		if (RightNodePos >= (SceneSetupTest.TotalNodes - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
			RightNodePos = (SceneSetupTest.TotalNodes) - 2;
		}

		UpNodePos = spell.YNode + spell.NodesUp;
		if (UpNodePos >= (SceneSetupTest.TotalNodes - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
			UpNodePos = (SceneSetupTest.TotalNodes) - 2;
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
		if (RightNodePos >= (SceneSetupTest.TotalNodes - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
			RightNodePos = (SceneSetupTest.TotalNodes) - 2;
		}

		UpNodePos = ground.YNode + ground.NodesUp;
		if (UpNodePos >= (SceneSetupTest.TotalNodes - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
			UpNodePos = (SceneSetupTest.TotalNodes) - 2;
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
		if (RightNodePos >= (SceneSetupTest.TotalNodes - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
			RightNodePos = (SceneSetupTest.TotalNodes) - 2;
		}

		UpNodePos = ground.YNode + ground.NodesUp;
		if (UpNodePos >= (SceneSetupTest.TotalNodes - 1)) {//The SceneSetupTest.TotalNodes - 1 Node Is The Edge Which Is Closed Off
			UpNodePos = (SceneSetupTest.TotalNodes) - 2;
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



}
