using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue {

	public string CharacterName;
	[TextArea(3,10)]
	public string[] Sentences;
}
