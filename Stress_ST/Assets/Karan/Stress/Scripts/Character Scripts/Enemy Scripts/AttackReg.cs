using UnityEngine;
using System.Collections;

public class AttackReg : MonoBehaviour 
{

	
	// TODO Small Bug that if u stand still in range after taking dmg u don't take dmg unless move around.
	// Migth use Range ditection rather then colliders 
	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.tag == transform.parent.GetComponent<EnemyCreep>().HeroTag)
		{
			other.GetComponent<CreatureRoot>().TookDmg(transform.parent.GetComponent<EnemyCreep>().Damage); // this doent work
			Debug.Log("PUNCH = " + transform.parent.GetComponent<EnemyCreep>().Damage);
			gameObject.SetActive(false);
		}
	}

}
