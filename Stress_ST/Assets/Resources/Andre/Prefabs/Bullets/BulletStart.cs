using UnityEngine;
using System.Collections;

public class BulletStart : MonoBehaviour {

	GameObject ImTheShooter;
	Rigidbody2D MyRigidbody2D;

	Vector3 _MyShootingDirection;
	const string Wall = "Wall";

	public float BulletSpeed;

	void Start () {
		MyRigidbody2D = GetComponent<Rigidbody2D> ();
	}

	public void SetParent(GameObject sender, GameObject target){
		ImTheShooter = sender;
		_MyShootingDirection = (target.transform.position - transform.position).normalized;

		if (target.transform.position.y < transform.position.y) {//this desides which way im rotating
	/*unity -> obsolete so change this TODO*/		transform.RotateAround (new Vector3 (0, 0, 1), Mathf.Deg2Rad * (Vector3.Angle(Vector3.right, _MyShootingDirection) * -1));//vec3.ang returns a deg value so changing it to rad
		} else {
	/*unity -> obsolete so change this TODO*/		transform.RotateAround (new Vector3 (0, 0, 1), Mathf.Deg2Rad * (Vector3.Angle(Vector3.right, _MyShootingDirection) * 1));
		}
	}

	void Update () {
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
