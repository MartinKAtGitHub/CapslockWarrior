using UnityEngine;
using System.Collections;

public class AttackReg : MonoBehaviour 
{

	void OnTriggerEnter2D(Collider2D other)
	{

	Debug.Log(other.name);


		if(other.tag == transform.parent.GetComponent<EnemyCreep>().HeroTag)
		{
			other.GetComponent<PlayerManager>().TakeDamage(transform.parent.GetComponent<EnemyCreep>().Damage); // this doent work
			Debug.Log("PUNCH = " + transform.parent.GetComponent<EnemyCreep>().Damage);
			gameObject.SetActive(false);
		}
	}


}
