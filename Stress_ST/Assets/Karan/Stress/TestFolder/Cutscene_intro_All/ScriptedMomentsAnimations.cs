using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMomentsAnimations : MonoBehaviour {

	public GameObject PlayerObject;
	public GameObject BossObject;
	public Camera IntroCam;



	// Use this for initialization
	void Start () 
	{
		

		PlayerObject = Instantiate(GameManager_Master.instance.PlayerCharacter, new Vector3(0f,0f),Quaternion.identity);

		PlayerObject.GetComponent<PlayerManager>().HealthPoints = 200;

		StartCoroutine(Cutscene());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator Cutscene()
	{
		Debug.Log("Start Intro");
		// player walks in to cam view
		// player ref.startwalkanim
		// player ref

		yield return new WaitForSeconds(3f);

		Debug.Log("Start Countown");

		yield return new WaitForSeconds(6f);

		Debug.Log("Start Gameplay");

	}


	private void MoveCharIntoPosition()
	{
		// Start from Pos(1,0) --> then move to new pos(5)
	}

	private void StartDialog()
	{
		// enable dialog box
		// wait for player
	}

	private void PanCamToShowBoss()
	{
		// we move the camera to show the boss.
	}

	private void IsReadyCutSceneFinished()
	{

	}

}
