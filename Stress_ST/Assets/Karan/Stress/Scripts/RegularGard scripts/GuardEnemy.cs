using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemy : MonoBehaviour {

    // stats

    [SerializeField] private Animator guardAnimator;

    private int attackHash = Animator.StringToHash("Attack"); // this seemses so bad this HardCoded trigger

	
    
    // Use this for initialization
	void Start ()
    {
		// Connect with all my scripts and out going scripts
	}
	
	
	void Update ()
    {
		


	}

    void MeleeAttack()
    {
        guardAnimator.SetTrigger(attackHash);
    }


}
