using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEvent_LevelIntro : ScriptedEvent 
{

	[SerializeField] private LevelManager_Master levelManagerMaster;

	[SerializeField] private GameObject player;
	[SerializeField] private Animator HeroAnimator;
	[SerializeField] private GameObject IntroGameObject;

	private Rigidbody2D playerRigBdy;
	private GameObject OldMan;
	private GameObject IntroCam;


	/*public GameObject PlayerSpawnPoint;
	public GameObject ActorSpawnPoint;
	public GameObject PlayerTargetPos;
	public Transform CamTargetPos;*/


	public Vector3 OldManPosFromPlayer;
	public DialogueManager DManager;

	private bool StartRun;
	private bool StartCamPan;

	[SerializeField]
	private float animSpeed;
	[SerializeField]
	private float camPanSpeed;

	public override void  SetInitalRefs()
	{
		levelManagerMaster = GetComponent<LevelManager_Master>(); // TODO make LevelManager Static ?

		player = GameManager_Master.instance.PlayerObject; // FindTag(Player1) // levelManagerMaster.player
		playerRigBdy = player.GetComponent<Rigidbody2D>();
		HeroAnimator = player.transform.Find("GFX").GetComponent<Animator>();

		//StartRun = false;
		/*
		OldMan.SetActive(true);
		OldMan.transform.SetParent(player.transform);
		OldMan.transform.localPosition = OldManPosFromPlayer;
		*/
		//IntroCam.GetComponent<CameraSmoothMotion>().enabled = false;

	}
	public override void TurnOffCompnants(GameObject actorGameObject) // Can this be protected ?
	{
		foreach (MonoBehaviour Scripts in actorGameObject.GetComponents<MonoBehaviour>()) 
		{
			/*if(Scripts.GetType() != gameObject.GetComponent<PlayerTyping>().GetType()) // If you ever need to turn of all but specific component ps: might not find componant
			{
				
				Scripts.enabled = false;
			}*/

			Scripts.enabled = false;
		}
	}
	public override void ScriptedEventEnd()
	{

	}
	public override IEnumerator ScriptedEventScene()
	{
		TurnOffCompnants(player);

		Debug.Log("Scripted event Started....");
		yield return new WaitForSeconds(3f);
		Debug.Log("Scripted event END ");

	}

}
