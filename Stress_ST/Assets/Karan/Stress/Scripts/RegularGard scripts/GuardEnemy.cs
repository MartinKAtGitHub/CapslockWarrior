using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemy : MonoBehaviour {

    // stats

    [SerializeField] private Animator guardAnimator;

    private  readonly int attackHash = Animator.StringToHash("Attack"); // this seemses so bad this HardCoded trigger

	
    
    // Use this for initialization
	void Start ()
    {
		// Connect with all my scripts and out going scripts
	}
	
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Input is from GuardEnemy");
            MeleeAttack();
        }
           
	}

    void MeleeAttack()
    {
        guardAnimator.SetTrigger(attackHash);
    }

}
