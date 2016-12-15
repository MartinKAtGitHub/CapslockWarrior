using UnityEngine;
using System.Collections;

public class Spells : MonoBehaviour{


	public float CoolDownTimer;

	public Transform ProjectileSpawn;
	/*public virtual float CoolDownTimer{ 
		get{ return ran;} 
	 
		set{ ran = value;}
	}*/



	public Spells()
	{
			//MonoBehaviour.print("Spells INI");
			//CoolDownTimer = 5;
	}

	public virtual void Cast()
	{
		//MonoBehaviour.print("I dont know what to cast");
	}
}
