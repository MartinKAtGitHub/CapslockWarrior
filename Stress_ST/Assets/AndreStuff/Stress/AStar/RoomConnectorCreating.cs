using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class RoomConnectorCreating : MonoBehaviour {

	List<RoomConnectorCreating> _MyRoom = new List<RoomConnectorCreating>();

	public Wall_ID ConnectorHubOne;//refrence to hub
	public Wall_ID ConnectorHubTwo;//refrence to hub

	public Nodes RoomNode;

	public bool LeftOrRight;//this is used to check which side to check when player is exiting the pathconnector

	public Wall_ID GetLeftOrRight;


	List<Nodes> _TheNodes = new List<Nodes>();

	Vector2 _RoomConnectorDirection = Vector2.zero;
	Vector2 _ObjectFromRoomConnectorDirection = Vector2.zero;
	float _MaxAngleDifference = 0.0f;
	float _ObjectsAngleDifference = 0.0f;

	void Awake(){//getting boxcollider dimensions and calculating distance from each node within the box collider
		_MyRoom.Add(this);

		RoomNode = new Nodes (new float[,] {{transform.position.x, transform.position.y}}, 0);
	
		float DistanceFromNodes = GetComponent<BoxCollider2D> ().size.x / Mathf.CeilToInt (GetComponent<BoxCollider2D> ().size.x);//if the colliders x pos is not an integer, then the remaining desimals is devided and added to each node

		float XIncrease = (float)(Math.Round (Math.Cos (Math.PI / 180 * transform.rotation.eulerAngles.z), 8)) * (DistanceFromNodes); //x position increase per node
		float YIncrease = (float)(Math.Round (Math.Sin (Math.PI / 180 * transform.rotation.eulerAngles.z), 8)) * (DistanceFromNodes); //y position increase per node

		float XStartPos = (float)(Math.Round (Math.Cos (Math.PI / 180 * transform.rotation.eulerAngles.z), 8)) * ((GetComponent<BoxCollider2D> ().size.x / 2) - DistanceFromNodes / 2);
		float YStartPos = (float)(Math.Round (Math.Sin (Math.PI / 180 * transform.rotation.eulerAngles.z), 8)) * ((GetComponent<BoxCollider2D> ().size.x / 2) - DistanceFromNodes / 2);

		for (int i = 0; i < Mathf.CeilToInt (GetComponent<BoxCollider2D> ().size.x); i++) {//adding all nodes to a list
			_TheNodes.Add (new Nodes (new float[,]{ { (transform.position.x + XStartPos - (XIncrease * i)), transform.position.y + YStartPos - (YIncrease * i) } }, 1));
			_TheNodes [i].SetRooms (GetComponent<RoomConnectorCreating>());
		}


		Nodes current = null;
		foreach(Nodes s in _TheNodes){
			if (current != null) {
				current.SetRoomNeighbours (s);
				s.SetRoomNeighbours (current);
			} else {
				current = s;
			}
		}

		RoomNode.SetRooms (GetComponent<RoomConnectorCreating>());

	}

	public List<Nodes> GettheNodes(){
		return _TheNodes;
	}

	#region Colliders 



	void OnTriggerEnter2D(Collider2D coll){//when a gameobject is inside the collider with tag == wall, then update the nodemap and recalculate the pathlist 
		if (coll.transform.GetComponent<MovingCreatures> () != null) {//TODO RemoveTHIS when AI is fully implemented(not fully but corretly) on everything
			coll.transform.GetComponent<MovingCreatures> ().SetNeighbourGroup (_MyRoom);
		}
	}

	void OnTriggerExit2D (Collider2D coll){//when a gameobject is removed from the collider with tag == wall, then update the nodemap and recalculate the pathlist 	//0,01745329251 == math.pi / 180
	
		if (coll.transform.GetComponent<MovingCreatures> () != null) {
			_RoomConnectorDirection.x = Mathf.Cos (0.01745329251f * (transform.rotation.eulerAngles.z + 90));//calculating the vector (direction object is fazing) that the collider the objects colides with
			_RoomConnectorDirection.y = Mathf.Sin (0.01745329251f * (transform.rotation.eulerAngles.z + 90));//calculating the vector (direction object is fazing) that the collider the objects colides with
			_ObjectFromRoomConnectorDirection.x = coll.transform.position.x - transform.position.x;//calculating the vector the object has when exiting the collider
			_ObjectFromRoomConnectorDirection.y = coll.transform.position.y - transform.position.y;//calculating the vector the object has when exiting the collider

			_MaxAngleDifference = Vector2.Angle (_RoomConnectorDirection, Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * new Vector2 (GetComponent<BoxCollider2D> ().size.x / 2, GetComponent<BoxCollider2D> ().size.y / 2));//calculating where the top right corner is (border.max (if object is rotated)) depending on boxcollider rotation 
			_ObjectsAngleDifference = Vector2.Angle (_RoomConnectorDirection, _ObjectFromRoomConnectorDirection);

			if (LeftOrRight == true) {//checking if the object exited the collider inside of a specifik angle
				if (_ObjectsAngleDifference > -_MaxAngleDifference && _ObjectsAngleDifference < _MaxAngleDifference) {
					coll.transform.gameObject.GetComponent<MovingCreatures> ().SetNeighbourGroup (ConnectorHubOne.Connectors);
				} else {
					coll.transform.gameObject.GetComponent<MovingCreatures> ().SetNeighbourGroup (ConnectorHubTwo.Connectors);
				}
			} else {
				if (!(_ObjectsAngleDifference > -_MaxAngleDifference && _ObjectsAngleDifference < _MaxAngleDifference)) {
					coll.transform.gameObject.GetComponent<MovingCreatures> ().SetNeighbourGroup (ConnectorHubOne.Connectors);
				} else {
					coll.transform.gameObject.GetComponent<MovingCreatures> ().SetNeighbourGroup (ConnectorHubTwo.Connectors);
				}
			}
		}
	}

	 
	#endregion

}
