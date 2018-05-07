using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptedEvent : MonoBehaviour 
{
	public abstract bool ScriptedEventEnd{get;set;}
	// Maybe make this justa regualr class, these things dont need to be public but all SScenes will need them
	protected abstract void  SetInitalRefs();
	public abstract IEnumerator ScriptedEventScene();


	public delegate void OnScriptedEventEndDelegate();//TODO ScriptedEvent OnScriptedEventEndEvent sould be a Action or func
	public /*event*/ OnScriptedEventEndDelegate OnScriptedEventEndEvent;


	//TODO In cut scene i just turn off all Componants(scripts), only igoring types maybe a better way
	protected virtual void AreComponentActiv(GameObject actorGameObject, bool status) // Can this be protected ?
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
	//TODO In cut scene i just turn off all GOs, only igoring string "GFX" maybe a better way
	protected virtual void AreChildeGameObjectsActiv(GameObject actorGameObject, bool status, string ignore)
	{
		foreach (Transform Child in actorGameObject.transform) 
		{
			
			// If you ever need to turn of all but specific component ps: might not find componant
			if(Child.gameObject.name != "GFX") 
			{
				Child.gameObject.SetActive(status);
			}
		
			//Child.gameObject.SetActive(status);
		}
	}

	public virtual void StartScriptedEvent()
	{
		Debug.Log("Starting Intro cutscene");
		StartCoroutine(ScriptedEventScene());
	}

	public virtual void StopScriptedEvent()
	{
		StopCoroutine(ScriptedEventScene());
		Debug.Log("CutScene Skiped ---- What to do when you skipped ?");
	}
}
