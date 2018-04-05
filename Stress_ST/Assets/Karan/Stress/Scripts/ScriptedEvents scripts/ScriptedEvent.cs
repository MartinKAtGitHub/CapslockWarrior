using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptedEvent : MonoBehaviour 
{
	public abstract void  SetInitalRefs();
	public abstract void  TurnOffCompnants(GameObject actorGameObject);
	public abstract void  ScriptedEventEnd();
	public abstract IEnumerator ScriptedEventScene();
}
