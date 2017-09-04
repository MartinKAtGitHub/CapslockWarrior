using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall_ID : MonoBehaviour {//this class is placed on walls so that when an object collides it gets the collisionid of this object

	public int TheCollisionID;//Currently not used, TODO connect this to the search

	public List<RoomConnectorCreating> Connectors;//used for pathconnectors as a hub of connecting them to create a room

}
