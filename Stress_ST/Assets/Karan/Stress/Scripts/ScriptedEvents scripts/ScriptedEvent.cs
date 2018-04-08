using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptedEvent : MonoBehaviour 
{
	// Maybe make this justa regualr class, these things dont need to be public but all SScenes will need them
	public abstract void  SetInitalRefs();
	public abstract IEnumerator ScriptedEventScene();

	public virtual void TurnOffCompnants(GameObject actorGameObject) // Can this be protected ?
	{
		foreach (MonoBehaviour Scripts in actorGameObject.GetComponents<MonoBehaviour>()) 
		{
			/*
			// If you ever need to turn of all but specific component ps: might not find componant
			if(Scripts.GetType() != gameObject.GetComponent<PlayerTyping>().GetType()) 
			{
				Scripts.enabled = false;
			}
			*/
			Scripts.enabled = false;
		}
	}

	public virtual void StartScriptedEvent()
	{
		StartCoroutine(ScriptedEventScene());
	}

	public virtual void StopScriptedEvent()
	{
		StopCoroutine(ScriptedEventScene());
	}

}
