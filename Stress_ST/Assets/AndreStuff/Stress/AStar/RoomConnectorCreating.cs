using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class RoomConnectorCreating : MonoBehaviour {

	public List<RoomConnectorCreating> MyRoom = new List<RoomConnectorCreating>();
	public List<RoomConnectorCreating> NeighboursOne = new List<RoomConnectorCreating>();
	public List<RoomConnectorCreating> NeighboursTwo = new List<RoomConnectorCreating>();

	public Wall_ID ConnectorHubOne;
	public Wall_ID ConnectorHubTwo;

	public Nodes RoomNode;

	public bool LeftOrRight;

	public Wall_ID GetLeftOrRight;

	int _NodesInHight = 0, _NodesInWidth = 0;

	List<Nodes> TheNodes = new List<Nodes>();


	void Awake(){//getting boxcollider dimensions and calculating distance from each node within the box collider

		RoomNode = new Nodes (new float[,] {{transform.position.x, transform.position.y}}, 0);
	
		float DistanceFromNodes = GetComponent<BoxCollider2D> ().size.x / Mathf.CeilToInt (GetComponent<BoxCollider2D> ().size.x);//if the colliders x pos is not an integer, then the remaining desimals is devided and added to each node

		float XIncrease = (float)(Math.Round (Math.Cos (Math.PI / 180 * transform.rotation.eulerAngles.z), 8)) * (DistanceFromNodes); //x position increase per node
		float YIncrease = (float)(Math.Round (Math.Sin (Math.PI / 180 * transform.rotation.eulerAngles.z), 8)) * (DistanceFromNodes); //y position increase per node

		float XStartPos = (float)(Math.Round (Math.Cos (Math.PI / 180 * transform.rotation.eulerAngles.z), 8)) * ((GetComponent<BoxCollider2D> ().size.x / 2) - DistanceFromNodes / 2);
		float YStartPos = (float)(Math.Round (Math.Sin (Math.PI / 180 * transform.rotation.eulerAngles.z), 8)) * ((GetComponent<BoxCollider2D> ().size.x / 2) - DistanceFromNodes / 2);

		for (int i = 0; i < Mathf.CeilToInt (GetComponent<BoxCollider2D> ().size.x); i++) {//adding all nodes to a list
			TheNodes.Add (new Nodes (new float[,]{ { (transform.position.x + XStartPos - (XIncrease * i)), transform.position.y + YStartPos - (YIncrease * i) } }, 1));
			TheNodes [i].SetRooms (GetComponent<RoomConnectorCreating>());
		}


		Nodes current = null;
		foreach(Nodes s in TheNodes){
			if (current != null) {
				current.SetNeighbors (s);
				s.SetNeighbors (current);
			} else {
				current = s;
			}
		}

		RoomNode.SetRooms (GetComponent<RoomConnectorCreating>());
		_NodesInWidth = TheNodes.Count;
		_NodesInHight = 1; 

	}


	#region Colliders 



	void OnTriggerEnter2D(Collider2D coll){//when a gameobject is inside the collider with tag == wall, then update the nodemap and recalculate the pathlist  TODO expand this to other sources of colliding objects?
	//	coll.transform.parent.gameObject.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (MyRoom);
		if(coll.transform.GetComponent<DefaultBehaviour> () != null)//TODO RemoveTHIS when AI is fully implemented(not fully but corretly) on everything
			coll.transform.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (MyRoom);


	}
	Vector2 RoomConnectorDirection = Vector2.zero;
	Vector2 ObjectFromRoomConnectorDirection = Vector2.zero;
	float MaxAngleDifference = 0.0f;
	float ObjectsAngleDifference = 0.0f;

	void OnTriggerExit2D (Collider2D coll){//when a gameobject is removed from the collider with tag == wall, then update the nodemap and recalculate the pathlist  TODO expand this to other sources of colliding objects?
		//0,01745329251 == math.pi / 180
		//	if (coll.gameObject.tag == "CreatureCollider") {//this might be abit heavy, or not. 
		if (coll.transform.GetComponent<DefaultBehaviour> () != null) {
			RoomConnectorDirection.x = Mathf.Cos (0.01745329251f * (transform.rotation.eulerAngles.z + 90));//calculating the vector (direction object is fazing) that the collider the objects colides with
			RoomConnectorDirection.y = Mathf.Sin (0.01745329251f * (transform.rotation.eulerAngles.z + 90));//calculating the vector (direction object is fazing) that the collider the objects colides with
			ObjectFromRoomConnectorDirection.x = coll.transform.position.x - transform.position.x;//calculating the vector the object has when exiting the collider
			ObjectFromRoomConnectorDirection.y = coll.transform.position.y - transform.position.y;//calculating the vector the object has when exiting the collider

			MaxAngleDifference = Vector2.Angle (RoomConnectorDirection, Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * new Vector2 (GetComponent<BoxCollider2D> ().size.x / 2, GetComponent<BoxCollider2D> ().size.y / 2));//calculating where the top right corner is (border.max (if object is rotated)) depending on boxcollider rotation 
			ObjectsAngleDifference = Vector2.Angle (RoomConnectorDirection, ObjectFromRoomConnectorDirection);

			if (LeftOrRight == true) {//checking if the object exited the collider inside of a specifik angle
				if (ObjectsAngleDifference > -MaxAngleDifference && ObjectsAngleDifference < MaxAngleDifference) {
					coll.transform.gameObject.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (ConnectorHubOne.Connectors);
					//coll.transform.parent.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (ConnectorHubOne.Connectors);
				} else {
					coll.transform.gameObject.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (ConnectorHubTwo.Connectors);
					//coll.transform.parent.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (ConnectorHubTwo.Connectors);
				}
			} else {
				if (!(ObjectsAngleDifference > -MaxAngleDifference && ObjectsAngleDifference < MaxAngleDifference)) {
					coll.transform.gameObject.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (ConnectorHubOne.Connectors);
					//coll.transform.parent.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (ConnectorHubOne.Connectors);
				} else {
					coll.transform.gameObject.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (ConnectorHubTwo.Connectors);
					//coll.transform.parent.GetComponent<DefaultBehaviour> ().SetNeighbourGroup (ConnectorHubTwo.Connectors);
				}
			}
		}
	}

	 
	#endregion

	#region Getmethods

	public int Getheight(){
		return _NodesInHight;
	}

	public int GetWidth(){
		return _NodesInWidth;
	}

	public List<RoomConnectorCreating> GetNeighbourGroupOne(){
		return ConnectorHubOne.Connectors;
	}

	public List<RoomConnectorCreating> GetNeighbourGroupTwo(){
		return ConnectorHubTwo.Connectors;
	}

	public List<RoomConnectorCreating> GetMyRoom(){
		return MyRoom;
	}

	public List<Nodes> GettheNodes(){
		return TheNodes;
	}

	#endregion

}

/*
void OnCollisionEnter2D(Collision2D coll) {//when a gameobject is inside the collider with tag == wall, then update the nodemap and recalculate the pathlist  TODO expand this to other sources of colliding objects?
		if (coll.gameObject.tag == "Wall") {
			UpdateCollisionID (coll.collider.bounds.max, coll.collider.bounds.min, 1);// TODO last parameter are going to be the effect added to the node (wind water lava ...... 1 6 3 dunno)
		}			
	}

	void OnCollisionExit2D(Collision2D coll) {//when a gameobject is removed from the collider with tag == wall, then update the nodemap and recalculate the pathlist  TODO expand this to other sources of colliding objects?
		if (gameObject.tag == "Wall") {
			UpdateCollisionID (coll.collider.bounds.max, coll.collider.bounds.min, 0);
		}
	}

	void UpdateCollisionID(Vector2 upperRightCorner, Vector2 lowerLeftCorner, int collisionID){//calculating where a collider is colliding with this collider <(o_O)> TODO currently only 1 effect can be applied on a node at a time, so if an effect is added upon another then whne the last one goes away the first on also does
		Debug.Log("Happenning");

		foreach(Nodes s in TheNodes){//going through each node to see if it was within the collision
			if ((s.GetID () [0, 0] < upperRightCorner.x && s.GetID () [0, 0] > lowerLeftCorner.x) && (s.GetID () [0, 1] < upperRightCorner.y && s.GetID () [0, 1] > lowerLeftCorner.y)) {
				s.SetCollision (collisionID);
			}
		}
	} 
	*/