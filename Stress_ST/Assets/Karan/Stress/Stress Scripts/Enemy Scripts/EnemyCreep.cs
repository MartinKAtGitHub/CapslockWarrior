using UnityEngine;
using System.Collections;

public class EnemyCreep : MonoBehaviour 
{

	public int Damage;
	[Tooltip("This is the Tag(s) that the enemy detects and starts attack anim Need to adjust this for more players")]
	public string HeroTag;

	public Animator AnimController;

	private Transform PunchHitBox;
	private Transform PunchRange;

	// Use this for initialization
	void Start () 
	{
		AnimController = GetComponent<Animator>();
		PunchHitBox = transform.Find("Punch");  // TODO check this out beacuse i have also read that foreach is also expensiv
		PunchRange = transform.Find("PunchRange");
		if(PunchHitBox == null)
		{
			Debug.LogError("DID NOT FIND = PUNCH");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == HeroTag)
		{
			AnimController.SetBool("MeleeRange",true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			AnimController.SetBool("MeleeRange",false);
		}
	}

	public void Punch()
	{
		PunchHitBox.gameObject.SetActive(true);
		//PunchRange.gameObject.SetActive(false);
	}

	public void PunchDone()
	{
		PunchHitBox.gameObject.SetActive(false);
	}

}
