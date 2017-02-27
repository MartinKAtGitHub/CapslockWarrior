using UnityEngine;
using System.Collections;

public abstract class BulletBehaviour : MonoBehaviour {

	[HideInInspector] public GameObject ImTheShooter;

	[HideInInspector] public Vector3 _MyShootingDirection;
	[HideInInspector] public const string Wall = "Wall";

	public Rigidbody2D MyRigidbody2D;
	public float BulletSpeed;


	public abstract void SetObjectDirection (GameObject sender, Vector3 target);

}
