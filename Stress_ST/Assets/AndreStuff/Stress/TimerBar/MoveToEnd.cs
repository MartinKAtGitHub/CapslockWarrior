using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveToEnd : MonoBehaviour {

	//This is attatched to the object that holds the info about which enemie to spawn, and the amount

	Transform _EndPosition;
	public float ObjectSpeed = 100;
	KeyValuePair<string,int> _DifficultyAndNumbers;
	GameObject _EnemyGenerator;

	void Start(){
		_EndPosition = transform.parent.FindChild("EndPosition");
	}

	void Update() {
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, _EndPosition.localPosition, ObjectSpeed * Time.deltaTime);
		if (transform.localPosition == _EndPosition.localPosition) {
			_EnemyGenerator.GetComponent<EnemyGenerator> ().StartSpawning (_DifficultyAndNumbers);
			Destroy (this.gameObject);
		}
	}

	public void SetDifficultyAndNumbers(KeyValuePair<string,int> difnum, GameObject enemyGeneratorRefrence){
		_DifficultyAndNumbers = difnum;
		_EnemyGenerator = enemyGeneratorRefrence;
	}
}