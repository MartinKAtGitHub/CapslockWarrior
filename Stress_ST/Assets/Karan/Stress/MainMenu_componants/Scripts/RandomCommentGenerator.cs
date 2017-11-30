using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCommentGenerator : MonoBehaviour {

	public GameObject[] Comments;
	
	// Use this for initialization
	void Start () 
	{
		startSpawningComments(Comments,50,50);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void startSpawningComments(GameObject[] CommentsArray, int offsetWidth, int offsetHight)
	{
		int screenWidth = Screen.width;
		int screenHight = Screen.height;

		
		for (int j = 0; j < 10 ; j++) {
			for (int i = 0; i < CommentsArray.Length; i++) {
				
				GameObject Test = (GameObject)Instantiate(Comments[i], new Vector3(Random.Range(offsetWidth ,screenWidth - offsetWidth), Random.Range(offsetHight,screenHight - offsetHight ), 0) , Quaternion.identity);
				Test.transform.SetParent(this.transform);
				//Debug.Log(Test.name);
			}
		}



	}
}
