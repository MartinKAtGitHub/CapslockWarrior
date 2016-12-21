using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class DefaultBehaviour : MonoBehaviour {

	public List<RoomConnectorCreating> NeighbourGroups = new List<RoomConnectorCreating>();
	public Nodes[] MyNodePosition = new Nodes[1];

	public GameObject _GoAfter = null;

	public float[,] myPos = new float[1,2];
	public bool UpdateThePath = false;


//	public virtual List<RoomNodeCreating> GetNeighbourGroups(){
//		return NeighbourGroups;
//	}

	public virtual Nodes[] GetMyNode(){
		return MyNodePosition;
	}
		
	public virtual float[,] GetMyPosition(){
		return myPos;
	}

	public virtual GameObject GetTraget(){
		return _GoAfter;
	}

	public virtual DefaultBehaviour GetTargetBehaviour(){
		if(_GoAfter != null)
			return _GoAfter.GetComponent<DefaultBehaviour>();

		return null;
	}

	public virtual void SetTarget(GameObject target){
		_GoAfter = target;
	}

	public abstract void OnDestroyed ();

	public virtual void SetNeighbourGroup(List<RoomConnectorCreating> neighbours){}


}
