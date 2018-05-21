using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Travels Towards The Targeted Position
//When At The Destination, Do Dmg To The Target
public class SentenceBullet : MonoBehaviour {

	public float Speed = 1;

	Transform _Target;
	Vector3 _Directions = Vector3.zero;
	Vector3 _travelPoint = Vector2.zero;

	public void SetAttackTarget (Transform target) {
		_Target = target;
	}

	// Update is called once per frame
	void Update () {

		if (_Target != null) {
			_travelPoint = _Target.transform.position;
		} 


		if ((_travelPoint - transform.position).y < 0) {
			_Directions.z = 180 + Vector3.Angle (Vector3.left, (_travelPoint - transform.position)) - 90;
		} else {
			_Directions.z = Vector3.Angle (Vector3.right, (_travelPoint - transform.position)) - 90;
		}

		transform.eulerAngles = _Directions;
		transform.position += (_travelPoint - transform.position).normalized * Time.deltaTime * Speed;

		if (Vector3.Distance (transform.position, _travelPoint) < 0.1f) {
			if (_Target != null) {
				if (_Target.GetComponent<BoxHealth> () != null) {
				_Target.GetComponent<BoxHealth> ().MyHealth--;
			
				if (_Target.GetComponent<BoxHealth> ().MyHealth <= 0)
						Destroy (_Target.gameObject);
				}
			}
			
			Destroy (this.gameObject);
		}
	}
}
