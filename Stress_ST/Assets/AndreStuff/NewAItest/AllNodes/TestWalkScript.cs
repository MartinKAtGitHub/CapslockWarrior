using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWalkScript : MonoBehaviour {

	float lowerLeftPosX = -1f;
	float lowerLeftPosY = -0.25f;

	int posX = 0;
	int posY = 0;


	public Vector3 PreviousPosition = Vector3.zero;


	NodeWalkcostSetter test = new NodeWalkcostSetter();

	int CurrentTileX = 0;
	int CurrentTileY = 0;

	public float Speed = 1;

	public int MoveRestricitonLeft = 1;
	public int MoveRestricitonRight = 1;
	public int MoveRestricitonUp = 1;
	public int MoveRestricitonDown = 1;

	public bool ForcePush = false;
	public Vector3 ForcePushValue = Vector3.zero;
	public Rigidbody2D MyBody2D;

	private void Start() {
		test.BaseGroundTiles(1, 1, GameObject.Find("Tile_Asphalt_Blue").GetComponent<BaseTile>());
		test.BaseGroundTiles(7, 1, GameObject.Find("Tile_Asphalt_Red").GetComponent<BaseTile>());
		test.BaseGroundTiles(0, 1, GameObject.Find("Tile_Asphalt_Red (1)").GetComponent<BaseTile>());
		test.BaseGroundTiles(0, 2, GameObject.Find("Tile_Asphalt_Red (2)").GetComponent<BaseTile>());
		test.BaseGroundTiles(1, 2, GameObject.Find("Tile_Asphalt_Red (3)").GetComponent<BaseTile>());
		test.BaseGroundTiles(2, 2, GameObject.Find("Tile_Asphalt_Red (4)").GetComponent<BaseTile>());
		test.BaseGroundTiles(2, 1, GameObject.Find("Tile_Asphalt_Red (5)").GetComponent<BaseTile>());
		test.BaseGroundTiles(0, 0, GameObject.Find("Tile_Asphalt_Red (6)").GetComponent<BaseTile>());
		test.BaseGroundTiles(1, 0, GameObject.Find("Tile_Asphalt_Red (7)").GetComponent<BaseTile>());
		test.BaseGroundTiles(2, 0, GameObject.Find("Tile_Asphalt_Red (8)").GetComponent<BaseTile>());

		PreviousPosition = transform.position;
		posX = Mathf.FloorToInt((transform.position.x - lowerLeftPosX) / 0.25f);
		posY = Mathf.FloorToInt((transform.position.y - lowerLeftPosY) / 0.25f);

		DoEnter();

	}
	bool onces = false;

	// Update is called once per frame
	void Update () {


		if (ForcePush == false) {
			

			if (Input.GetKey(KeyCode.UpArrow)) {
				movementVector.y = 1 * MoveRestricitonUp * Speed;
			} else if (Input.GetKeyUp(KeyCode.UpArrow)) {
				movementVector.y = 0;
			}

			if (Input.GetKey(KeyCode.DownArrow)) {
				movementVector.y = -1 * MoveRestricitonDown * Speed;
			} else if (Input.GetKeyUp(KeyCode.DownArrow)) {
				movementVector.y = 0;
			}

			if (Input.GetKey(KeyCode.LeftArrow)) {
				movementVector.x = -1 * MoveRestricitonLeft * Speed;
			} else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
				movementVector.x = 0;
			}

			if (Input.GetKey(KeyCode.RightArrow)) {
				movementVector.x = 1 * MoveRestricitonRight * Speed;
			} else if (Input.GetKeyUp(KeyCode.RightArrow)) {
				movementVector.x = 0;
			}

		}

		DoMovement();
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (onces == false) {
				Debug.Log("HERE");
				onces = true;
				PreviousPosition = transform.position;
				transform.position += Vector3.right * 0.125f;//TODO Works, Might Abit To Well Cuz Of /100
				transform.position += Vector3.up * 0.125f;//TODO Works, Might Abit To Well Cuz Of /100
			}
		}
		//	if (Input.GetKey(KeyCode.Space)) {
		//	PreviousPosition = transform.position;
		//		transform.position += Vector3.right;
		//	}

		posX = Mathf.FloorToInt((transform.position.x - lowerLeftPosX) / 0.25f);
		posY = Mathf.FloorToInt((transform.position.y - lowerLeftPosY) / 0.25f);
//		Debug.Log(Mathf.FloorToInt((transform.position.x - lowerLeftPosX) / 0.25f) + " " + Mathf.FloorToInt((transform.position.y - lowerLeftPosY) / 0.25f) + " | " + transform.position.x);



		if (posX != CurrentTileX || posY != CurrentTileY) {
			//	test.BaseGround[CurrentTileX, CurrentTileY].TheTileLogic.OnExit();

			/*if ((posX > -1 && posY > -1) && (posX < StressCommonlyUsedInfo.NodesWidth && posY < StressCommonlyUsedInfo.NodesWidth)) {
				Debug.Log("ExitingOld");
				test.BaseGround[CurrentTileX, CurrentTileY].TheTileLogic.OnExit(gameObject);
				CurrentTileX = posX;
				CurrentTileY = posY;
			}*/



			//	if ((CurrentTileX == 1 && CurrentTileY == 0) || (CurrentTileX == 7 && CurrentTileY == 0)) {
				DoExit();
		//	}

		//	if ((CurrentTileX == 1 && CurrentTileY == 0) || (CurrentTileX == 7 && CurrentTileY == 0)) {
				DoEnter();
			//	}

	//		PreviousPosition = transform.position;



		}


	}

	void DoExit() {
		if(test.BaseGround[CurrentTileX, CurrentTileY] != null)
			test.BaseGround[CurrentTileX, CurrentTileY].TheTileLogic.OnExit(this);

		if (Mathf.FloorToInt((transform.position.x - lowerLeftPosX) / 0.25f) != posX || Mathf.FloorToInt((transform.position.y - lowerLeftPosY) / 0.25f) != posY){
			posX = Mathf.FloorToInt((transform.position.x - lowerLeftPosX) / 0.25f);
			posY = Mathf.FloorToInt((transform.position.y - lowerLeftPosY) / 0.25f);
			DoEnter();
		}
	}

	void DoEnter() {
	
		if (test.BaseGround[posX, posY] != null) {
			Debug.Log("ENTER");

			CurrentTileX = Mathf.FloorToInt((transform.position.x - lowerLeftPosX) / 0.25f);
			CurrentTileY = Mathf.FloorToInt((transform.position.y - lowerLeftPosY) / 0.25f);

			test.BaseGround[CurrentTileX, CurrentTileY].TheTileLogic.OnEnter(this);
		
			if (Mathf.FloorToInt((transform.position.x - lowerLeftPosX) / 0.25f) != posX || Mathf.FloorToInt((transform.position.y - lowerLeftPosY) / 0.25f) != posY) {
				DoExit();
			}
		}
	}

	public void FocePush() {
		ForcePush = true;
		movementVector = Vector3.zero;
	}

	Vector3 movementVector = Vector3.zero;

	void DoMovement() {

		if (ForcePush == true) {
			movementVector = Vector3.zero;

			if (Vector2.Distance(GetComponent<Rigidbody2D>().velocity, Vector2.zero) < 0.25f) {
				ForcePush = false;
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			} else {
		//		transform.position += (Vector3)GetComponent<Rigidbody2D>().velocity;
			}
		} else {

			PreviousPosition = transform.position;
			transform.position += movementVector * 0.002f;

		}


	}

}

