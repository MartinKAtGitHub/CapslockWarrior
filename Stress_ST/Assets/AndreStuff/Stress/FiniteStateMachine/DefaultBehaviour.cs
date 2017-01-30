using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class DefaultBehaviour : MonoBehaviour {

	/*[HideInInspector]*/ public List<RoomConnectorCreating> NeighbourGroups = new List<RoomConnectorCreating>();
	public Nodes[] MyNodePosition = new Nodes[1];

	[HideInInspector] public GameObject _GoAfter = null;

	public float[,] myPos = new float[1,2];
	[HideInInspector]  public bool UpdateThePath = false;

	Vector2 MyPositionVector2;

	public enum EnemyType {Rangd, Melle, Heeeeels, Spilling};
	public bool Turnoffwithforcestuff = false;
	public float[] MovementSpeed = new float[1];

//	public virtual List<RoomNodeCreating> GetNeighbourGroups(){
//		return NeighbourGroups;
//	}

	public virtual Nodes[] GetMyNode(){
		return MyNodePosition;
	}
		
	public virtual float[,] GetMyPosition(){
		return myPos;
	}

	public virtual Vector2 GetMyPositionVector2(){
		MyPositionVector2.x = myPos [0,0];
		MyPositionVector2.y = myPos [0,1];
		return MyPositionVector2;
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

	public virtual void AttackTarget(){}
	public virtual void RecievedDmg(){}

	public void ChangeMovementMuliply(float a){
		MovementSpeed [0] *= a;
	}

	public void ChangeMovementAdd(float a){
		MovementSpeed [0] += a;
	}

}
