using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeCostumeArc : MonoBehaviour
{

   
    
    [Tooltip("Position we want to hit")]
    public Transform Target;
    public Transform Grenade;
    public Transform Shadow;
    public GameObject ParticalSystem;
    float speed;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;

    public float AirTime;
    
    Vector3 startPos;
    Vector3 targetPos;


    void Start()
    {
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = Grenade.position;
        targetPos = Target.position;
    }

    void Update()
    {
        var dist = targetPos.x - startPos.x;
      
        speed = dist / AirTime;
        speed = Mathf.Abs(speed);
       
        float nextX = Mathf.MoveTowards(Grenade.position.x, targetPos.x, speed * Time.deltaTime);

        float baseY = Mathf.Lerp(startPos.y, /*Target.position.y*/ targetPos.y, (nextX - startPos.x) / dist);
        float arc = arcHeight * (nextX - startPos.x) * (nextX - targetPos.x) / (-0.25f * dist * dist);

       
       // gameObject.transform.localScale = Vector3.one * (baseY + arc);

        var nextPos = new Vector3(nextX, baseY + arc, Grenade.position.z);
        var ShadePos = new Vector3(nextX /*+ arc*/, baseY);
        //Rotate to face the next position, and then move there < dont need this as it is this will make the object look the same way it arcs.
        //Grenade.rotation = LookAt2D(nextPos - Grenade.position);
        Grenade.position = nextPos;
        Shadow.position = ShadePos;
        // Do something when we reach the target
        if (nextPos == targetPos)
        {
            Arrived();
        }

    }

    void Arrived()
    {
        //Destroy(gameObject);
        Debug.Log("Nade Reached Target");
        ParticalSystem.GetComponent<ParticleSystem>().Play(); // Needs 2 sec delay ?? some bounce when it hits
        enabled = false;
    }


    void CloserToScreenEffect()
    {
        // I need to Normelize the arc from 1 -> 2 right now by grenade is at 0 at start and 4 at the highest point and back too 0

        //Debug.Log("Y + ARC  == " + (baseY + arc));
       
    }

    /// 
    /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
    /// that makes the local +X axis point in the given forward direction.
    /// 
    /// forward direction
    /// Quaternion that rotates +X to align with forward
    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
