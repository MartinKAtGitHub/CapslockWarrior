using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptedEvent : MonoBehaviour 
{
	public abstract bool ScriptedEventEnd{get;set;}
	// Maybe make this justa regualr class, these things dont need to be public but all SScenes will need them
	public abstract void  SetInitalRefs();
	public abstract IEnumerator ScriptedEventScene();

	public delegate void OnScriptedEventEndDelegate();
	public OnScriptedEventEndDelegate OnScriptedEventEndEvent;

	public virtual void AreComponentActiv(GameObject actorGameObject, bool status) // Can this be protected ?
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
			Scripts.enabled = status;
		}
	}

	public virtual void StartScriptedEvent()
	{
		StartCoroutine(ScriptedEventScene());
	}

	public virtual void StopScriptedEvent()
	{
		StopCoroutine(ScriptedEventScene());
		Debug.Log("CutScene Skiped ---- What to do when you skipped ?");
	}
}
