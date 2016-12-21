using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall_ID : MonoBehaviour {//this class is placed on walls so that when an object collides it gets the collisionid of this object
	/// <param name="NOE">is the parameterID in XY coordinates,</param>
	[Tooltip("HEllo")]
	public int TheCollisionID;
	public List<RoomConnectorCreating> Connectors;

}
