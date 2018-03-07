using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NullCheckGameObject {



	public static void NullCheckFindWithTag(ref GameObject _gameObject, string _findTagName) // Key word Ref. I basicly say dont make a copy of Object passed in, just make direct changes
	{
		if(_gameObject == null)
		{
			Debug.Log("Did not find GameObject Searching with Tag..." );
			
			_gameObject = GameObject.FindWithTag(_findTagName);

			if( _gameObject == null)
			{
				Debug.Log("Can't Find Tag" + _findTagName + " Object is NULL");	
			}
		}
	}

	public static void NullCheckFindWithName(ref GameObject _gameObject, string _findName)
	{
		if(_gameObject == null)
		{
			Debug.Log("Did not find GameObject Searching with Name..." );
			
			_gameObject = GameObject.Find(_findName);

			if( _gameObject == null)
			{
				Debug.Log("Can't Find Name(" + _findName + ") Object is NULL");
			}
		}
	}
}
