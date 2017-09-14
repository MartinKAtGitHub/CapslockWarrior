using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class DefaultBehaviour : DefaultBehaviourPosition {

	[Header("Object Behaviour")]
	public The_Object_Behaviour ObjectBehaviour;

	[Space(30)]
	[Header("Object Stat Variables")]
	[Tooltip("How Many Words. So Always 1 More Then HealthLength.Length...   Health.Length == 5 Then HealthLength.Length == 6")]
	public int[] Health = new int[1];
	[Tooltip("Length Of The Words. So Always 1 More Then HealthLength.Length...   Health.Length == 5 Then HealthLength.Length == 6")]
	public int[] HealthLength = new int[1];
	public int[] Energy = new int[1];
	public float[] Dmg = new float[1];
	public float[] MovementSpeed = new float[1];
	public float[] AttackSpeed = new float[1];
	public float[] Range = new float[1];

	[HideInInspector] public bool FreezeCharacter = false;//Dissables All Functions
	public EnemyWordChecker _WordChecker;
	[Space(20)]
	public Text TextElement;
	public Animator GfxObject;
	public Rigidbody2D MyRididBody;

	[HideInInspector] public DefaultBehaviourPosition _TheTarget = null;//the target




	public virtual void OnDestroyed () {}

	public virtual DefaultBehaviour GetTargetBehaviour(){
		if (_TheTarget != null)
			return _TheTarget.GetComponent<DefaultBehaviour>(); 

		return null;
	}

	public virtual void SetTarget(GameObject target){
		_TheTarget = _TheTarget.GetComponent<DefaultBehaviour>();
	}

}
