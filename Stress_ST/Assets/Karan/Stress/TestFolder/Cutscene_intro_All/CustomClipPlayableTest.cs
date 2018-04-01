using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CustomClipPlayableTest : PlayableTrack 
{
	void OnEnable()
	{
		Debug.Log("My custom Timeline Clip ENABLE");
		CreateClip<PlayableTrack>();
	}

	void Awake()
	{
		Debug.Log("My custom Timeline Clip AWAKE");
	}


}
