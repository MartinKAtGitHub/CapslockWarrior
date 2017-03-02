using UnityEngine;
using System.Collections;

public class CameraSmoothMotion : MonoBehaviour {

	[SerializeField] private float smoothRate;
	[SerializeField] private Transform player;
	[SerializeField] private Vector2 mapSize;// i can create logic to automate finding the map size my doing (imgSize in pixels / pixel per units) -> 2000/100
											 // = 20 / 2 = 10(rigth) | 10* -1(left);
	[SerializeField] private Vector3 limitCamAt;

											




	// Use this for initialization
	void Start () 
	{

		if(player == null)
		{
			Debug.Log("Camera cant find Player Object using GM.FINDTAG");
			player = GameObject.FindGameObjectWithTag("Player1").transform;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * smoothRate);
		LimitCameraMapEdge();
	}

	private void LimitCameraMapEdge()
	{
		transform.position = Vector2.Lerp(new Vector2(Mathf.Clamp (transform.position.x, -limitCamAt.x, limitCamAt.x),
														Mathf.Clamp (transform.position.y, -limitCamAt.y, limitCamAt.y) ),
														new Vector2 ( Mathf.Clamp( player.position.x, -limitCamAt.x, limitCamAt.x), 
														Mathf.Clamp( player.position.y, -limitCamAt.y, limitCamAt.y)), Time.deltaTime * smoothRate);
	}



}
