using UnityEngine;
using System.Collections;

public class Spells : MonoBehaviour{

	public Spells()
	{
		MonoBehaviour.print("Spells INI");
	}

	public virtual void Cast()
	{
		MonoBehaviour.print("I dont know what to cast");
	}
}
