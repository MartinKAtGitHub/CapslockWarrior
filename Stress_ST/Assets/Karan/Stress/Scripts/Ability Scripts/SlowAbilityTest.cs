using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAbilityTest : MonoBehaviour {

    public GameObject Player;
    PlayerController PC;

    public float slowPower;
    public float slowTimer;

    float currentSpeed;

	void Start ()
    {
        PC = Player.GetComponent<PlayerController>();

       currentSpeed = PC.CurrentSpeed;
      //  PC.CurrentSpeed = currentSpeed - (currentSpeed * slowPower) ;

        Debug.Log(this.name +" Slow = " + PC.CurrentSpeed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        slowTimer -= Time.deltaTime;

        Debug.Log(this.name + " Timer = " + (int)slowTimer);
        if(slowTimer <= 0)
        {
            //slowPower = 0;
            //PC.CurrentSpeed = currentSpeed - (currentSpeed * slowPower);
            PC.CurrentSpeed = PC.MaxSpeed;
            Debug.Log("--------SLOW OVER -----");
            this.enabled = false;
        }
        else
        {
            currentSpeed = currentSpeed - (currentSpeed * slowPower);
            PC.CurrentSpeed = currentSpeed;
        }
	}
}
