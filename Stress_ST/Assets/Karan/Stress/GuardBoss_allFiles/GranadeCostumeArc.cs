using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeCostumeArc : MonoBehaviour
{
    [Tooltip("Position we want to hit")]
    public Transform Target;
    public Transform Grenade;
    public Transform Shadow;
    public GameObject ParticalSystemObj;
    float speed;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;

    [SerializeField] float AirTime;
    [SerializeField] float TimeOnGroundBeforeDetonation;
    [SerializeField] SpriteRenderer spriteRenderer;


    Animator nadeAnimator;
    SlowStatusEffect slowStatusEffect;
    ParticleSystem particleSystem;
    CircleCollider2D circleCollider2D;
    Vector3 startPos;
    Vector3 targetPos;


    bool inAir;

    void Start()
    {
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = Grenade.position;
        targetPos = Target.position;
        nadeAnimator = GetComponent<Animator>();
        slowStatusEffect = GetComponent<SlowStatusEffect>();
        particleSystem = ParticalSystemObj.GetComponent<ParticleSystem>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        var particleShape = particleSystem.shape;
        particleShape.radius = circleCollider2D.radius;

        circleCollider2D.enabled = false;
        inAir = true;
        
        //SpriteSorting();
    }

    void Update()
    {
        if(inAir)
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
       
       

    }

    void Arrived()
    {
        Debug.Log("Nade Reached Target");
        inAir = false;
       // ParticalSystem.GetComponent<ParticleSystem>().Play();
       // enabled = false;

        StartCoroutine(GranadeOnGroundEffect());
    }

    IEnumerator GranadeOnGroundEffect()
    {
        nadeAnimator.enabled = false;
        
        yield return new WaitForSeconds(TimeOnGroundBeforeDetonation);
        circleCollider2D.enabled = true;

        particleSystem.Play();
        Debug.Log(particleSystem.main.duration);
        yield return new WaitForSeconds(particleSystem.main.duration);

        circleCollider2D.enabled = false;
        //enabled = false;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Target.GetComponent<StatusEffectManager>().StatusEffectList.Add(slowStatusEffect);
    }


    private void SpriteSorting() //TODO sorting bugs at certain angels, the arc / boss postions are the cultprit
    {
        var selecSortOrgin = Target.position.y - transform.position.y;
        Debug.Log("SORT = " + transform.position.y);

        var sortFromThisPosition = transform.position.y - 0.3;
        Debug.Log("Sorting Nade From  = " + (int)sortFromThisPosition);
        spriteRenderer.sortingOrder = (int)sortFromThisPosition * -10;


        /*
         if target.y is > transform.y
            Sort from my pos
            else if(target.y > transform.y)
            sort from target pos
         
         */


        /*if (selecSortOrgin > 0)
        {
            var sortFromThisPosition = Target.position.y - 0.3;
            Debug.Log("Sorting Nade From  = " + (int)sortFromThisPosition);
            spriteRenderer.sortingOrder = (int)sortFromThisPosition * -10;
        }
        else
        {
            var sortFromThisPosition = transform.position.y;
            Debug.Log("Sorting Nade From  = " + (int)sortFromThisPosition);
            spriteRenderer.sortingOrder = (int)sortFromThisPosition * -10;
        }
        */

    }
}
