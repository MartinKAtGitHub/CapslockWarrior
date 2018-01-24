using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSizeChange : MonoBehaviour {

	public ParticleSystem test;
	public ParticleSystem.ShapeModule a;
	Vector3 testi = Vector3.zero;

	void Start(){
		a = test.shape;
	}

	// Update is called once per frame
	void Update () {
		testi.x += 0.1f * Time.deltaTime;	
		a.scale = testi;
	//	a.box.x = est;
	}
}
