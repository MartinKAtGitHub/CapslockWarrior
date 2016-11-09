using UnityEngine;
using System.Collections;

public class GMDontDestroy : MonoBehaviour {

	void Awake()// singleton for the game master.
	{
		DontDestroyOnLoad(this);

		if(FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}

}
