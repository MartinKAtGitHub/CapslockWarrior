using UnityEngine;

public class ShieldDash : MonoBehaviour
{


    Vector2 ForceAndDirection;

    [SerializeField] Transform target;

    public Animator BossAnimator;
    public float PushBackForce;
    public float ChargeSpeed;
    public float ChargeRange;
    public bool IsChargeing;
    public LayerMask WallLayer;

    Vector3 MaxRangeVector;
    Vector3 ChargeDirectionAndSpeed;
    Vector3 initialChargePos;

    Rigidbody2D rb2d;

    SpriteRenderer bossSprite;

    StatusEffectManager statusEffectManager;

    SlowStatusEffect shieldSlowStatusEffect;

    bool facingRigth;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        BossAnimator = GetComponent<Animator>();
        bossSprite = GetComponent<SpriteRenderer>();

        IsChargeing = false;
        facingRigth = true;

        statusEffectManager = target.GetComponent<StatusEffectManager>();

        /*slowStatusEffect = new SlowStatusEffect(); // This is intresting, making a instance in the script no need to add as a componant
        slowStatusEffect.Power = 50;
        */

        shieldSlowStatusEffect = GetComponent<SlowStatusEffect>();
        shieldSlowStatusEffect.Target = target.gameObject;

    }

    public void ShieldChargeMovement()// TODO Add Timer --> if the Boss gets stuck we will have a finale check on TIME so teh boss isent stuck in the charge state
    {
        if (IsChargeing)
        {
            rb2d.MovePosition(transform.position + ChargeDirectionAndSpeed * Time.deltaTime);
            CheckMaxChargeRange();
        }
    }

    private void CheckMaxChargeRange()
    {
        var dist = Vector3.Distance(transform.position, initialChargePos + MaxRangeVector);

        if (dist <= 0.5f) // Switch this to TIMER insted of Position, we dont want him to get stuck in loop
        {
            Debug.Log("Max Charge Reached");
            IsChargeing = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsChargeing == true)
        {
            if (collision.gameObject.tag == target.gameObject.tag) /// TAG is not safe, maybe find player script
            {
                OnPlayerImpact(collision);
            }

            //if (collision.gameObject.tag ==  "Wall") // tags are scary in case we change them
            //if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Walls"))
            if (1 << collision.gameObject.layer == WallLayer.value) // SO -> WallLayer.value = 2^ layernum  = (13)
            {
                Debug.Log("Wall Name = " + collision.gameObject.name);
                IsChargeing = false;
            }
        }
    }

    public void StartShieldCharge()
    {
        BossAnimator.SetTrigger("Charge");

        IsChargeing = !IsChargeing;

        initialChargePos = transform.position;

        var targetVector = target.position - transform.position;
        var targetVectorNorm = targetVector.normalized;

        MaxRangeVector = targetVectorNorm * ChargeRange;
        ChargeDirectionAndSpeed = targetVectorNorm * ChargeSpeed;

        if (ChargeDirectionAndSpeed.x > 0 && !facingRigth)
        {
            //bossSprite.flipX = false;

            FlipScale();
        }
        else if (ChargeDirectionAndSpeed.x < 0 && facingRigth)
        {
            // bossSprite.flipX = true;
            FlipScale();
        }
    }


    public void FlipScale()
    {
        facingRigth = !facingRigth;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void OnPlayerImpact(Collision2D collision)
    {
        Debug.Log("Player Hit");
        BossAnimator.SetTrigger("Idle"); // After impact anim
        IsChargeing = false;
        var targetVector = target.position - transform.position;
        var PushForceVector = targetVector.normalized * PushBackForce;

        // Add Status effect to list
        //Debug.Log(shieldSlowStatusEffect.BaseActiveTime);

        statusEffectManager.StatusEffectList.Add(shieldSlowStatusEffect);

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(PushForceVector);

    }





}
