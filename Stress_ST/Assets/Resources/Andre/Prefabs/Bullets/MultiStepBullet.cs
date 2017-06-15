using UnityEngine;
using System.Collections;

public class MultiStepBullet : BulletBehaviour {

	public int TransitionStages;
	public int StagesTransitioned = 0;

	public float DistanceToMove;//this is not the direction but the distance in the objects direction to move
	public bool TeleportOrWalk;//true == teleport

	public Animator TheAnimator;
	bool play = true;
	private Vector3 _direction = Vector3.zero;

	OnlyShootAfterAnimation GoTONextTransition;

	// Use this for initialization
	public override void SetObjectDirection(GameObject sender, Transform target){
		GoTONextTransition = TheAnimator.GetBehaviour<OnlyShootAfterAnimation> ();
		ImTheShooter = sender;

		_MyShootingDirection = (target.position - transform.position).normalized;
		_direction.z = Vector3.Angle (Vector3.right, _MyShootingDirection);

		if (_MyShootingDirection.y < 0) {
			_direction.z = _direction.z * -1;
		}  
		transform.rotation = Quaternion.Euler (_direction);

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (TeleportOrWalk == true) {
			if (GoTONextTransition.Shoot == true) {
				if (play == false) {
					Destroy (this.gameObject);
				}
				GoTONextTransition.Shoot = false;
				if (StagesTransitioned < TransitionStages) {
					StagesTransitioned++;
					MyRigidbody2D.position = transform.position + (_MyShootingDirection * DistanceToMove);
				} else {
					Destroy (this.gameObject);
				}
			} 
		} else {
			Debug.Log (_MyShootingDirection + " | " + BulletSpeed);
			MyRigidbody2D.velocity = _MyShootingDirection * BulletSpeed;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag(Wall)) {
			play = false;
		} else if(coll.gameObject != ImTheShooter) {//if im colliding with anything but myself(sender) make it recievedmg
			if (coll.transform.GetComponent<DefaultBehaviour> () != null) {
				coll.transform.GetComponent<DefaultBehaviour> ().RecievedDmg (1);
				Destroy (this.gameObject);
			}
		}
	}
}
