using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsoluteRoot : MonoBehaviour {

	public float[,] MyPos = new float[1,2];//used to update the position for the Objects node position 
	public Nodes[] MyNode = new Nodes[1];//my node and mypos is used to target objects for the AI so this is needed for everythin that is targetable
	[HideInInspector] public List<RoomConnectorCreating> NeighbourGroups = new List<RoomConnectorCreating>();//all objects that are moving need to know in which room they are


	public virtual void SetAiRoom(Wall_ID room){//just called once, and that is when spawning an object
		NeighbourGroups = room.Connectors;
	}

	public virtual void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){
		NeighbourGroups = neighbours;
	}



}
