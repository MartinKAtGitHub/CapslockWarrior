using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEvent_LevelIntro : ScriptedEvent 
{
	public Vector3 OldManPosFromPlayer;
	

	public override bool ScriptedEventEnd{get;set;}
	//[SerializeField] private LevelManager_Master levelManagerMaster;
	private GameObject player;
	private Animator playerAnimator;

	[SerializeField]private Animator levelIntroAnimator;
	[SerializeField]private Transform moveTarget;
//	private PlayerController playerController;

	private Rigidbody2D playerRigBdy;
	private GameObject OldMan;
	[SerializeField] private GameObject IntroCam;

	[SerializeField] private bool playerReady;
	[SerializeField] private bool StartRun;
	private bool StartCamPan;

	[SerializeField]
	private float animSpeed;
	[SerializeField]
	private float camPanSpeed;

	private IEnumerator ScriptedEvent;

	/*
	public GameObject PlayerSpawnPoint;
	public GameObject ActorSpawnPoint;
	public GameObject PlayerTargetPos;
	public Transform CamTargetPos;
	*/

	void Awake()
	{
		SetInitalRefs();
	}

	void Start()
	{
		LevelManager_Master.instance.StartScriptedEvent.AddListener(StartScriptedEvent);
	}

	void FixedUpdate()
	{
			//MoveActorToPositionVelocity(player, moveTarget);
			MoveActorToPositionTransform(player, moveTarget, 1, playerAnimator, "Running", StartRun ); // move to Update() ?
	}

	protected override void  SetInitalRefs()
	{
		ScriptedEvent = ScriptedEventScene();
		playerReady = false;
		ScriptedEventEnd = false;
		//levelManagerMaster = GetComponent<LevelManager_Master>(); // TODO make LevelManager Static ?
		//playerController = player.GetComponent<PlayerController>();
		levelIntroAnimator.gameObject.SetActive(false);
		/*
		OldMan.SetActive(true);
		OldMan.transform.SetParent(player.transform);
		OldMan.transform.localPosition = OldManPosFromPlayer;
		*/
		//IntroCam.GetComponent<CameraSmoothMotion>().enabled = false;
		//StartRun = false; // This is buged
	}

	public override IEnumerator ScriptedEventScene() // TODO change this to scriptable object så we can swape cutscenes
	{
		//Debug.Log("Scripted event Started....");
		AreComponentActiv(player, false);
		AreChildeGameObjectsActiv(player, false ,"GFX");
		StartRun = true;

		yield return new WaitForSeconds(1f);

		StartIntroBox();
		yield return new WaitUntil(IsPlayerReady);

		EndintroBox();

		yield return new WaitForSeconds((levelIntroAnimator.GetCurrentAnimatorClipInfo(0).Length + 1f));
		AreComponentActiv(player, true);
		AreChildeGameObjectsActiv(player, true ,"GFX");

		IntroCam.SetActive(false);
		LevelManager_Master.instance.PlayerCam.SetActive(true);

		ScriptedEventEnd = true;
		LevelManager_Master.instance.OnIntroEventEnd.Invoke();
		LevelManager_Master.instance.OnIntroEventEnd.RemoveListener(EndintroBox);

	}

	private void MoveActorToPositionVelocity(GameObject actor, Transform target, Animator animation, string runAnimName)
	{
		float distance = Vector3.Distance(actor.transform.position, target.position);

		if(distance >= 0)
		{
			Debug.Log("Start Running anim");
			//playerRigBdy.velocity = new Vector2 (1 * animSpeed, 0 * animSpeed);
			animation.SetBool(runAnimName, true);
		}
		else
		{
			Debug.Log("Stop Running anim");
			playerRigBdy.velocity = new Vector2 (1 * 0, 0 * 0);
			animation.SetBool(runAnimName, false);
			StartRun = false;
		}
	}

	//TODO Insted of coding and cutscene animation, why not just animate it 
	//TODO Actor might not have a animator, So i dont know if i sould start the animation here or let the event handle it
	private void MoveActorToPositionTransform(GameObject actor, Transform target, float speed, Animator animation, string runAnimName, bool isMoving) //TODO ADD SKIP LOGIC TO CUTSCENE
	{
		if(isMoving)
		{
			float distance = Vector3.Distance(actor.transform.position, target.position); // PERFORMANCE change to use Hotshot logic to find 
			//Debug.Log(distance);
			if(distance > 0 && ScriptedEventEnd == false) // anim can get stuck 0,9999 runs 24 times ? // TODO ADD SKIP LOGIC 
			{
				//Debug.Log("Start Running anim");
				actor.transform.position = Vector3.MoveTowards(actor.transform.position, target.position, speed * Time.deltaTime);
				animation.SetBool(runAnimName, true);
			}
			else
			{
				//Debug.Log("Stop Running anim");
				animation.SetBool(runAnimName, false);
				StartRun = false;
			}
		}
	}

	private void StartIntroBox()
	{
		levelIntroAnimator.gameObject.SetActive(true);
		Debug.Log("Start Intro .....");
	}

	private void EndintroBox()
	{
		levelIntroAnimator.SetBool("Fade", true);
		Debug.Log("End Intro .....");
	}

	private bool IsPlayerReady()
	{
		return playerReady;
	}

	public void SetPlayerReady()
	{
		playerReady = true;
	}

	void OnDisable()
	{
		LevelManager_Master.instance.StartScriptedEvent.RemoveListener(StartScriptedEvent);      
    }

	public override void StartScriptedEvent()
	{
		GetPlayerDataForCutscene();
		StartCoroutine(ScriptedEvent);
		Debug.Log("Starting Intro cutscene");
	}

	public override void StopScriptedEvent() // Use this to skip cutscene
	{
		StopCoroutine(ScriptedEvent);
		Debug.Log("CutScene Skiped ---- What to do when you skipped ?");
	}

	private void GetPlayerDataForCutscene()
	{
		player = GameManager.Instance.PlayerObject; // FindTag(Player1) // levelManagerMaster.player
		playerRigBdy = player.GetComponent<Rigidbody2D>();
		playerAnimator = player.GetComponentInChildren<Animator>();
	}
}

