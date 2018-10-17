using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossController : MonoBehaviour {

    GranadeCostumeArc flashNade;
    ShieldDash shieldCharge;

    public LayerMask LayerMask;
    public Transform Target;
    RaycastHit2D[] raycastHit2D;

	void Start ()
    {
        flashNade = GetComponent<GranadeCostumeArc>();
        shieldCharge = GetComponent<ShieldDash>();
	}

    
    void Update ()
    {
        Debug.DrawLine(transform.position, Target.position, Color.red);

        raycastHit2D =  Physics2D.LinecastAll(transform.position, Target.position, LayerMask);

        //Debug.Log(Test[0].transform.name);

        /*for (int i = 0; i < Test.Length; i++)
        {
            Debug.Log(Test[i].transform.name);
        }*/
        // if Space && LOS clear

        // if G && LOS clear
        //Debug.Log(LOSCheck());
        //Debug.Log(raycastHit2D[0].transform.name);


	}
    

    bool LOSCheck()
    {
        if(raycastHit2D.Length >= 0 && raycastHit2D[0].transform.tag == Target.tag)
        {
            return true;
        }
        else
        {
            return false;
        }
    
    }









}
