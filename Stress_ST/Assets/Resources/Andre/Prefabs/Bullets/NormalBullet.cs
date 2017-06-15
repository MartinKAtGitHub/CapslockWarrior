using UnityEngine;
using System.Collections;

public class NormalBullet : BulletBehaviour {

	private Vector3 _direction = Vector3.zero;


	void Start () {
		MyRigidbody2D = GetComponent<Rigidbody2D> ();
	}

	public override void SetObjectDirection(GameObject sender, Transform target){
		ImTheShooter = sender;
		_MyShootingDirection = (target.position - transform.position).normalized;

		_MyShootingDirection = (target.position - transform.position).normalized;
		_direction.z = Vector3.Angle (Vector3.right, _MyShootingDirection);

		if (_MyShootingDirection.y < 0) {
			_direction.z = _direction.z * -1;
		}  
		transform.rotation = Quaternion.Euler (_direction);
	}

	void FixedUpdate () {
		MyRigidbody2D.velocity = _MyShootingDirection * BulletSpeed;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag(Wall)) {
			Destroy (this.gameObject);
		} else if(coll.gameObject != ImTheShooter) {//if im colliding with anything but myself(sender) make it recievedmg
			if (coll.transform.GetComponent<DefaultBehaviour> () != null) {
				coll.transform.GetComponent<DefaultBehaviour> ().RecievedDmg (1);
				Destroy (this.gameObject);
			}
		}
	}
}
