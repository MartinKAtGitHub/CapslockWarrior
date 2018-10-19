using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCController : MonoBehaviour {

    public float time;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Slow(float slowAmount, float slowTime)
    {
        Debug.Log(name + " Has been SLOWED by = " + slowAmount + " For = " + slowTime);
        time = slowTime;
    }

    public void Stun(float stunAmount, float stunTime)
    {
        // turn off momvement
    }
    public void Blind (float stunAmount, float stunTime)
    {
        // Lower visual range
    }
}
