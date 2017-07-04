using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerenderingTest : MonoBehaviour {

	public Transform LineSource;
	public Transform LineTarget;
	LineRenderer Line;

	 	float counter;
	 float dist;

	public float lineDrawSpeed = 6f;


	// Use this for initialization
	void Start () {

		Line = GetComponent<LineRenderer> ();
		Line.SetPosition (0, LineSource.position);
		Line.SetPosition (1, LineTarget.position);

		dist = Vector3.Distance (LineSource.position, LineTarget.position);
	}

	// Update is called once per frame

	public Material Material0;
	public Material Material1;
	public Material Material2;
	public Material Material3;

	public float changematerialtime = 0.1f;
	 float counterr = 0;
	int materialcount;
	public bool ropeorlightning = false;

	 float x;
	void Update () {

		counterr += Time.deltaTime;
		if (changematerialtime < counterr) {
			materialcount++;
			counterr = 0;

			if (materialcount == 0) {
				Line.material = Material0;
			} else if (materialcount == 1) {
				Line.material = Material1;
			} else if (materialcount == 2) {
				Line.material = Material2;
			} else if (materialcount == 3) {
				Line.material = Material3;
				materialcount = 0;
			} 
		}




		if (ropeorlightning == false) {

			if (counter < dist) {
				dist = Vector3.Distance (LineTarget.position, LineSource.position);

				counter += 0.1f / lineDrawSpeed;
				float x = Mathf.Lerp (0, dist, counter);
				Vector3 pointA = LineSource.position;
				Vector3 pointB = LineTarget.position;

				Line.SetPosition (1, (x * (LineTarget.position - LineSource.position).normalized) + LineSource.position);



			} else {
				Line.SetPosition (0, LineSource.position);
				Line.SetPosition (1, LineTarget.position);
			}
		} else {

			if (x < dist) {
				dist = Vector3.Distance (LineSource.position, LineTarget.position);


				counter += 0.1f / lineDrawSpeed;
				x = Mathf.Lerp (0, dist, counter);

				Line.SetPosition (1, (x * (LineTarget.position - LineSource.position).normalized) + LineSource.position);
				Line.SetPosition (0, LineSource.position);


			} else {
				x = 0;
				counter = 0;
			}

		}


	}
}
