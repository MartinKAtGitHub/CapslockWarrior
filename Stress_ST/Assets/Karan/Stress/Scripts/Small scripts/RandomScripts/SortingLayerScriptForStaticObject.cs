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
	#if UNITY_EDITOR // This code will only run when in editor
	void Update ()
    {
		spriteRendrer.sortingOrder = (int)(transform.position.y * -10);
		//Debug.Log("TEST in EDITOR");
	}
	#endif

}
