using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public abstract class DefaultBehaviour : MonoBehaviour {

	[HideInInspector] public bool FreezeCharacter = false;
	public EnemyWordChecker _WordChecker;
	public Text TextElement;
	public string _EnemyHealth;

	//----------------------- needed to know where this object is.
	public float[,] MyPos = new float[1,2];//used to update the position for the Objects node position 
	public Nodes[] MyNode = new Nodes[1];//my node and mypos is used to target objects for the AI so this is needed for everythin that is targetable
	[HideInInspector] public List<RoomConnectorCreating> NeighbourGroups = new List<RoomConnectorCreating>();//all objects that are moving need to know in which room they are
	//-----------------------

	[Tooltip("How Many Words. So Always 1 More Then HealthLength.Length...   Health.Length == 5 Then HealthLength.Length == 6")]
	public int[] Health = new int[1];
	[Tooltip("Length Of The Words. So Always 1 More Then HealthLength.Length...   Health.Length == 5 Then HealthLength.Length == 6")]
	public int[] HealthLength = new int[1];
	public int[] Energy = new int[1];
	public float[] Dmg = new float[1];
	public float[] ObjectStandardSpeed = new float[1];
	public float[] Range = new float[1];

	public The_Object_Behaviour ObjectBehaviour;

	[HideInInspector] public DefaultBehaviour _TheTarget = null;//the target
	public TheAnimator MyAnimator;

	public abstract void OnDestroyed ();
	public abstract void AttackTarget (Transform targetPos);
	public abstract void RecievedDmg (int _damage);
	public abstract void ChangeMovementAdd(float a);
	public abstract void GotTheKill(int a);
	public abstract void SetNeighbourGroup (List<RoomConnectorCreating> neighbours);

	public virtual void SetAiRoom(Wall_ID room){//just called once, and that is when spawning an object
		NeighbourGroups = room.Connectors;
	}

	public virtual DefaultBehaviour GetTargetBehaviour(){
		if (_TheTarget != null)
			return _TheTarget.GetComponent<DefaultBehaviour>(); 

		return null;
	}

	public virtual void SetTarget(GameObject target){
		_TheTarget = _TheTarget.GetComponent<DefaultBehaviour>();
	}

	public void setword(string a){
		_WordChecker._ObjectHealth = a;
		TextElement.text = a;
		_EnemyHealth = a;
	}
}
