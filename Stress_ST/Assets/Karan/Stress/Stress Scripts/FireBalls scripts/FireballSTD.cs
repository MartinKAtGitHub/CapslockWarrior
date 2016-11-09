using UnityEngine;
using System.Collections;

public class FireballSTD : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		rb.velocity = new Vector2(speed ,0.0f);
	}
}
