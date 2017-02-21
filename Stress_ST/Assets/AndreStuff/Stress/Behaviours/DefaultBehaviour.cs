using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class DefaultBehaviour : MonoBehaviour {

	[HideInInspector] public bool FreezeCharacter = false;
	public StressEnums.EnemyType CreatureType;


	//----------------------- needed to know where this object is.
	public float[,] myPos = new float[1,2];//used to update the position for the Objects node position 
	public Nodes[] MyNode = new Nodes[1];//my node and mypos is used to target objects for the AI so this is needed for everythin that is targetable
	[HideInInspector] public List<RoomConnectorCreating> NeighbourGroups = new List<RoomConnectorCreating>();//all objects that are moving need to know in which room they are
	//-----------------------

	public int[] MyHealthPoints = new int[1];
	public float[] Damage= new float[1];
	public float[] MovementSpeed = new float[1];

	public abstract void OnDestroyed ();
	public abstract void AttackTarget ();
	public abstract void RecievedDmg ();
	public abstract void ChangeMovementAdd(float a);

	public virtual Nodes[] GetMyNode(){
		return MyNode;
	}

	public virtual void SetAiRoom(Wall_ID room){//just called once, and that is when spawning an object
		NeighbourGroups = room.Connectors;
	}

	public abstract void SetNeighbourGroup (List<RoomConnectorCreating> neighbours);
}
