using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossController : MonoBehaviour {

    GranadeCostumeArc flashNade;
    ShieldDash shieldCharge;

    public LayerMask LayerMask;
    public Transform Target;
    RaycastHit2D[] raycastHit2D;


    public GameObject Nade;
    public Transform NadeSpawn;

    public float LOSOffsetY;
    public float TargetLOSOffsetY;

    void Start ()
    {
        flashNade = GetComponent<GranadeCostumeArc>();
        shieldCharge = GetComponent<ShieldDash>();
	}

    
    void Update ()
    {
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + LOSOffsetY),
                                                new Vector3(Target.position.x, Target.position.y + TargetLOSOffsetY), Color.red);

        raycastHit2D =  Physics2D.LinecastAll(new Vector3(transform.position.x, transform.position.y + LOSOffsetY),
                                                new Vector3(Target.position.x, Target.position.y + TargetLOSOffsetY), LayerMask);

       // Debug.Log("Can i see player = " + LOSCheck());    
       
        GrenadeThrow();

        ShieldCharge();
    }
    

    bool LOSCheck()
    {
        

        if(raycastHit2D.Length > 0 && raycastHit2D[0].transform.tag == Target.tag) // TODO need to handel error if 0 elemts in array
        {
            return true;
        }
        else
        {
            return false;
        }
    
    }

    void GrenadeThrow()
    {
        if (Input.GetKeyDown(KeyCode.G) && LOSCheck())
        {
            var nade = Instantiate(Nade, NadeSpawn.position, Quaternion.identity);
            nade.SetActive(true);
        }
    }

    void ShieldCharge()
    {
        if (Input.GetKeyDown(KeyCode.Space) && LOSCheck())
        {
           shieldCharge.StartShieldCharge();
        }
        shieldCharge.ShieldChargeMovement();
    }
    
}
