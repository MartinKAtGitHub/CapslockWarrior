using UnityEngine;
using System.Collections;

public abstract class Spells : MonoBehaviour{


	public float CoolDownTimer;

	// This code i feel to be convinent 
	public Transform SpellSpawnPos;
	public GameObject PlayerGameObject;
	//----------



	/*public virtual float CoolDownTimer{ 
		get{ return ran;} 
	 
		set{ ran = value;}
	}*/



	public abstract void Cast();
	//{
		//MonoBehaviour.print("I dont know what to cast");
	//}
}
