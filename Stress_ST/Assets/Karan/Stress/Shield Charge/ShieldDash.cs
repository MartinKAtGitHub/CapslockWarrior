using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDash : MonoBehaviour {

    
    Vector2 ForceAndDirection;
    [SerializeField]
    Transform target;

    public Animator BossAnimator;
    public float PushBackForce;
    public float ChargeSpeed;
    public float ChargeRange;
    public bool StartCharge;
    public GameObject Nade;
    public Transform NadeSpawn;

    Vector3 MaxRangeVector;
    Vector3 ChargeSpeedVector;
    Vector3 StartChargPos;
    Rigidbody2D rb2d;
	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        BossAnimator = GetComponent<Animator>();
        StartCharge = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

        GrenadeThrow();


        if (Input.GetKeyDown(KeyCode.Space))
        {

            BossAnimator.SetTrigger("Charge");
            Debug.Log("CHARGE");
            StartCharge = !StartCharge;
            StartChargPos = transform.position;
            
            var targetVector = target.position - transform.position;

            MaxRangeVector = targetVector.normalized * ChargeRange;
            ChargeSpeedVector = targetVector.normalized * ChargeSpeed;
            
        }
            StartShieldDash(MaxRangeVector, ChargeSpeedVector);
    }

    private void StartShieldDash(Vector3 maxRangeVec, Vector3 chargeSpeedVec)
    {
        if(StartCharge == true)
        {
            rb2d.MovePosition(transform.position + chargeSpeedVec * Time.deltaTime);
            
            var dist = Vector3.Distance(transform.position, StartChargPos + maxRangeVec);
     
            if (dist  <= 0.5f)// this is bad 0.5 needs to be 0 bit the distance is not that precise
            {
                Debug.Log("Max Charge Reached");
                StartCharge = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") /// Tag needs to be handeld --> gameManger.getplayerTAG cant hard code
        {
            Debug.Log("Player Hit");
            BossAnimator.SetTrigger("Idle");
            StartCharge = false;
            var targetVector = target.position - transform.position;
            var finalRange = targetVector.normalized * PushBackForce ;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(finalRange);
        }
    }


    void GrenadeThrow()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            var nade = Instantiate(Nade, NadeSpawn.position, Quaternion.identity);
            nade.SetActive(true);
        }
    }
}
