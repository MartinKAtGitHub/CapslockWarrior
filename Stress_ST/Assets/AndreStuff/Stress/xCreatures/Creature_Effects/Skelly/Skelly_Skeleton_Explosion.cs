using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_Skeleton_Explosion : MonoBehaviour {

	public Skelly_Worm_LazerBeam_AnimatorParameters test;
	public LayerMask mask;
	float _RngTimeTic = 0;
	public	float _StartTime = 0;
//	RaycastHit2D[] FoundObject;
	public int Dmg = 5;

	void Awake(){
		if (Physics2D.CircleCastAll (transform.position, 0.1f, Vector2.zero, 0, (1 << 0)).Length > 0) {
			Destroy (this.gameObject);
		}
		_RngTimeTic = Random.Range (1, 26) / 100;//Just To Reduce The Chance Of Everyone Refreshing At The Same Time.
		_StartTime = ClockTest.TheTime[0] + _RngTimeTic;
	}
	void FixedUpdate(){
		
		if (_StartTime <= ClockTest.TheTime[0]) {
			_StartTime = ClockTest.TheTime[0] + _RngTimeTic;
		//	FoundObject = Physics2D.CircleCastAll (transform.position, 0.1f, Vector2.zero, 0, mask);

			/*for (int i = 0; i < FoundObject.Length; i++) {
				if (FoundObject [i].transform.CompareTag ("Player1")) {
					FoundObject [i].transform.GetComponent<PlayerManager> ().RecievedDmg (Dmg);//TODO Also Inherits From AbsoluteRoot But Change It TO Not Override TODO Make It Override
				} else {
					FoundObject [i].transform.GetComponent<AbsoluteRoot> ().RecievedDmg (Dmg);
				}
			}*/
				

		
		}


	}


}
