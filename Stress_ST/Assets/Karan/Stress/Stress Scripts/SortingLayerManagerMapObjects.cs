using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class SortingLayerManagerMapObjects : MonoBehaviour {

	Renderer spriteRendrer;
	// Use this for initialization
	void Start () 
	{
		spriteRendrer = GetComponent<Renderer>();
		spriteRendrer.sortingOrder = (int)(transform.position.y * -10);
	}


	// 
	#if UNITY_EDITOR
	void Update () 
	{
		// We want to do this in Start in the final game since thay are static objects that wont move.
		spriteRendrer.sortingOrder = (int)(transform.position.y * -10);
		//Debug.Log("TEST in EDITOR");
	}
	#endif
}
