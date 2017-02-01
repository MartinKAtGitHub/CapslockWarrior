using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class DefaultBehaviour : MonoBehaviour {

	[HideInInspector] public List<RoomConnectorCreating> NeighbourGroups = new List<RoomConnectorCreating>();
	[HideInInspector] public GameObject _GoAfter = null;
	[HideInInspector] public bool UpdateThePath = false;
	[HideInInspector] public bool StopMoveLogic = false;
	public bool RunPathfinding = true;

	public enum EnemyType {Rangd, Melle, Heeeeels, Spilling};
	public EnemyType thetype;

	public Nodes[] MyNodePosition = new Nodes[1];
	public float[,] myPos = new float[1,2];
	public float[] MovementSpeed = new float[1];

	Vector2 _MyPositionVector2;



	public abstract void OnDestroyed ();

	public abstract void SetNeighbourGroup (List<RoomConnectorCreating> neighbours);

	public abstract void AttackTarget ();
	public abstract void RecievedDmg ();

	public abstract void AddWallWithTrigger (GameObject collidingwithobject);
	public abstract void RemoveWallWithTrigger (GameObject collidingwithobject);
	public abstract void AddEnemyWithTrigger (GameObject collidingwithobject);
	public abstract void RemoveEnemyWithTrigger (GameObject collidingwithobject);


	public void ChangeMovementAdd(float a){
		MovementSpeed [0] += a;
	}

	public virtual Vector2 GetMyPositionVector2(){
		_MyPositionVector2.x = myPos [0,0];
		_MyPositionVector2.y = myPos [0,1];
		return _MyPositionVector2;
	}

	public virtual DefaultBehaviour GetTargetBehaviour(){
		if(_GoAfter != null)
			return _GoAfter.GetComponent<DefaultBehaviour>();

		return null;
	}

	public virtual void SetTarget(GameObject target){
		_GoAfter = target;
	}
}
