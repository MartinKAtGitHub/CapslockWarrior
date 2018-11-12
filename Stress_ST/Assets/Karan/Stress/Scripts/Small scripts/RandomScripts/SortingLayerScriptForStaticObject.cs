using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class SortingLayerScriptForStaticObject : MonoBehaviour {

	Renderer spriteRendrer;
	
	void Start () 
	{
		spriteRendrer = GetComponent<Renderer>();
		spriteRendrer.sortingOrder = (int)(transform.position.y * -10);
	}


	// 
	#if UNITY_EDITOR
	void Update () //TODO we need 1 script Thats sorts moving objects. We dont want Update in static once 
    {
		spriteRendrer.sortingOrder = (int)(transform.position.y * -10);
		//Debug.Log("TEST in EDITOR");
	}
	#endif
}
