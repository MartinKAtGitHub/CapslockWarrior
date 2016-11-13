using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InTheMiddleManager : MonoBehaviour {
	//TODO going to implement the logic that happends here somewhere els, if not this will be the main manager for updating A* paths and statemachines(maybe)

	HashSet<GameObject> MovingObjects = new HashSet<GameObject>();

	float counter = 0;
	float counterend = 0;
	float counterstart = 0;
	float counterobjects = 0;

	/*Must be greater then 3, it's a bug in gotodestinaiton GoToNextNode(), working with objectsprframedevider == 4 and above. which means that the path is update every 4th frame*/
	float objectsprframedevider = 4; //deviding so that the amount of objects if devided by this amount, so if i have 15 enemies, i devide them by 5 which means that i update 3 enemies each frame/update call


	public void AddObject(GameObject obj){
		if(MovingObjects.Contains(obj) == false){
			MovingObjects.Add(obj);
		}
	}

	public void RemoveObject(GameObject obj){
		if(MovingObjects.Contains(obj) == true){
			MovingObjects.Remove(obj);
		}
	}

	// Update is called once per frame
	void Update () {

		if (MovingObjects.Count > 0) {

			counterobjects = MovingObjects.Count / objectsprframedevider;
			if (counterend >= MovingObjects.Count)
				counterend = 0;
			counterstart = counterend;
			counterend = counterend + counterobjects;
			counter = 0;

			foreach (GameObject s in MovingObjects) {

				if (counter >= counterstart && counter < counterend) {//if counter is within the number then update the path for that object
					s.GetComponent<GoToDestination>().MakeNewPathSearch ();
				}
				counter++;
				s.GetComponent<GoToDestination>().GoToNextNode ();
			}
		}
	}
}
