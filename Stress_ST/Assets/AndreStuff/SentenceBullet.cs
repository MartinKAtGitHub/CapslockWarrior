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


	public float BulletDmg = 1.5f;
	CreatureRoot _TargetStats;



	public void SetAttackTarget (Transform target) {
		_Target = target;
		_TargetStats = _Target.GetComponent<CreatureRoot>();
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
		if (Vector3.Distance(transform.position, _travelPoint) > Vector3.Distance(transform.position, transform.position + (_travelPoint - transform.position).normalized * Time.deltaTime * Speed)) {//Checking If New Position Is Closer Then My Current, If False Then The Object Teleports Through To The Other Side
			transform.position += (_travelPoint - transform.position).normalized * Time.deltaTime * Speed;
		} else {
			transform.position += _travelPoint - transform.position;
		}



		if (Vector3.Distance (transform.position, _travelPoint) < 0.1f) {
			if (_Target != null) {
				if (_Target.gameObject.tag == "Enemy") {

					if (_TargetStats.Stats.Shield > 0) {//Shield Is There
						float DmgToShields = 1.5f * _TargetStats.Stats.PhysicalResistence * 2;

						if (DmgToShields > _TargetStats.Stats.Shield) {
							_TargetStats.TookDmg(Mathf.FloorToInt(((DmgToShields - _TargetStats.Stats.Shield) / 2) + _TargetStats.Stats.Shield));//Calculating Dmg + Sending Dmg
						} else {
							_TargetStats.TookDmg(Mathf.FloorToInt(DmgToShields));//Sending Dmg
						}

					} else {
						float DmgToHealth = 1.5f * _TargetStats.Stats.PhysicalResistence;

						if (DmgToHealth > _TargetStats.Stats.Shield) {
							_TargetStats.TookDmg(Mathf.FloorToInt(1.5f * _TargetStats.Stats.PhysicalResistence));//Calculating Dmg + Sending
						}
					}
				}
			}
			
			Destroy (this.gameObject);//Destoying On Contact With Target
		}
	}
}
