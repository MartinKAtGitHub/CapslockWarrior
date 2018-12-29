using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMomentsAnimations : MonoBehaviour {

	public GameObject PlayerObject;
	public GameObject BossObject;

	public GameObject PlayerSpawnPoint;
	public GameObject OldMan;
	public GameObject ActorSpawnPoint;
	public GameObject PlayerTargetPos;
	public Transform CamTargetPos;

	public GameObject IntroCam;

	public Vector3 OldManPosFromPlayer;
	//public DialogueManager DManager;

	private Animator HeroAnimator;
	private Rigidbody2D playerRigBdy;
	private bool StartRun;
	private bool StartCamPan;

	[SerializeField]
	private float animSpeed = 0;
	[SerializeField]
	private float camPanSpeed = 0;

	/// <summary>
	/// This whole scene was made without PixelPerfect script And 1920/1080 (16:9) res. Cam size is 2.7
	/// this Casues the pixel to be Floats, not Ints. -> 0.3 pixel size insted 1 feks
	/// </summary>




	void Start () 
	{
		
		RefSetup();
		StartCoroutine(Cutscene());
	}

	void FixedUpdate()
	{
		if(StartRun)
		{
			MoveCharIntoPosition();
		}
	}
	void Update()
	{
		PanCamToShowBoss();
	}

	IEnumerator Cutscene()
	{
		
		//PlayerObject = Instantiate(GameManager_Master.instance.PlayerCharacter, PlayerSpawnPoint.transform.position ,Quaternion.identity);
		//Debug.Log("Start Intro . PlayerSpawn");

		// Player turn off all scripts.
		TurnOffAllScriptsOnGameobject(PlayerObject);
		TurnOffChildern(PlayerObject);
		yield return new WaitForSeconds(1f);

		// player walks in to cam view
		StartRun = true;
		HeroAnimator.SetBool("Running", true);

		// Start dialogue old man
		StartDialog(OldMan);

		//yield return new WaitForSeconds(6f);
		yield return new WaitUntil(IsDialogueFinished);
		yield return new WaitForSeconds(1f);

		StartCamPan = true;
		StartDialog(BossObject);

		yield return new WaitUntil(IsDialogueFinished);
		StartCamPan = false;

		CutSceneFinished();
	

	}


	private void MoveCharIntoPosition()
	{
		if(PlayerObject.transform.position.x <= PlayerTargetPos.transform.position.x)
		{
			//Debug.Log("Start Running anim");
			playerRigBdy.velocity = new Vector2 (1 * animSpeed, 0 * animSpeed);
			//HeroAnimator.SetBool("Running", true); // cost .> moved under Startrunbool
		}
		else
		{
			Debug.Log("Stop Running anim");
			playerRigBdy.velocity = new Vector2 (1 * 0, 0 * 0);
			HeroAnimator.SetBool("Running", false);
			StartRun = false;
		}
	}

	private void StartDialog(GameObject character)
	{
		// enable dialog box
		//character.GetComponent<DialogueTrigger>().TriggerDialouge();
        //TODO Fix Dialogue in ScriptedMomentsANimations
	}

	private void PanCamToShowBoss()
	{
		if(StartCamPan)
		{
			IntroCam.transform.position = Vector3.Lerp(IntroCam.transform.position, CamTargetPos.position, camPanSpeed * Time.deltaTime); // Can be animated
		}
	}

	private void CutSceneFinished()
	{
		IntroCam.GetComponent<CameraSmoothMotion>().enabled = true;
		TurnOnAllScriptsOnGameobject(PlayerObject); 
		TurnOnAllScriptsOnGameobject(BossObject);
		DisableOldMan();

		// enable boss script
	}

	private bool IsDialogueFinished()// This checks to see if the Chasracter talking has ended the Dialogue he has.
	{
        //return DManager.isDialogueEnd;
        Debug.LogError("NEED TO ADD NEW DILOGUE SYSTEM");
        return false;
    }

	private void RefSetup()
	{
		PlayerObject = Instantiate(GameManager.Instance.PlayerPrefab, PlayerSpawnPoint.transform.position ,Quaternion.identity);
		//Instantiate(BossObject, ActorSpawnPoint.transform.position, Quaternion.identity);

		playerRigBdy = PlayerObject.GetComponent<Rigidbody2D>();
		HeroAnimator = PlayerObject.transform.Find("GFX").GetComponent<Animator>();
		StartRun = false;

		OldMan.SetActive(true);
		OldMan.transform.SetParent(PlayerObject.transform);
		OldMan.transform.localPosition = OldManPosFromPlayer;

		IntroCam.GetComponent<CameraSmoothMotion>().enabled = false;

	}

	private void DisableOldMan()
	{
		//Start RunAwayAnim
		//if(RunAwayAnim is done)
		OldMan.SetActive(false);
	}

	private void TurnOffAllScriptsOnGameobject(GameObject gameObject)
	{
		foreach (MonoBehaviour Scripts in gameObject.GetComponents<MonoBehaviour>()) 
		{
			/*if(Scripts.GetType() != gameObject.GetComponent<PlayerTyping>().GetType()) // If you ever need to turn of all but specific component ps: might not find componant
			{
				
				Scripts.enabled = false;
			}*/

			Scripts.enabled = false;
		}
	}

	private void TurnOnAllScriptsOnGameobject(GameObject gameObject)
	{
		foreach (MonoBehaviour Scripts in gameObject.GetComponents<MonoBehaviour>()) 
		{
			/*if(Scripts.GetType() != gameObject.GetComponent<PlayerTyping>().GetType()) // If you ever need to turn of all but specific component ps: might not find componant
			{
				Scripts.enabled = true;
			}*/

			Scripts.enabled = true;
		}
	}

	private void TurnOffChildern(GameObject gameObject)
	{

		string[] exeptions = {"GFX","OldMan_Guardian"};

		//string gfxName = "GFX";
		//string oldManName = "OldMan_Guardian";

		foreach (Transform Child in gameObject.transform) 
		{
			for (int i = 0; i < exeptions.Length; i++) 
			{
				if(Child.name == exeptions[i])
				{
					Child.gameObject.SetActive(true);
					break;
				}
				else
				{
					Child.gameObject.SetActive(false);
				}
			}
		}
	}
}
