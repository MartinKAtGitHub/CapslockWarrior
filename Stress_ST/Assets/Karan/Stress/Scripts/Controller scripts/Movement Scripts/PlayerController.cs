using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour{


	[SerializeField]private float maxSpeed = 10f;
	public Animator heroAnimator;
	public bool MouseControll;
	public bool canPlayerMove;
	bool AutoRun;
	bool InputRecived; 
	bool KeyLock;

	bool facingRigth;
	bool cancelAutoRun;
	bool targetSet;
	Vector2 deltaTargetCurrentPos; // ugly solution
	Vector2 direction;
	Rigidbody2D playerRigBdy;
	Transform heroGraphics;
	Vector3 mousePos;

	/*public ControllerType SelectedControllerType;
	public enum ControllerType
	{
		MouseAndArrows,
		OnlyArrows,
	};*/


	public float MaxSpeed
	{
		get
		{
            
			return maxSpeed;	
		}
		set
		{
			Debug.Log("PlayerController Speed Set To  = " + value);
			maxSpeed = value;		
		}
	}

	public Vector2 Direction
	{
		get
		{
			return direction;
		}
		set
		{
			direction = value;
		}
	}

	// Use this for initialization
	void Awake () 
	{
		canPlayerMove = true;
		AutoRun = false;
		InputRecived = false;
		KeyLock = false;
		
		cancelAutoRun = true;
		facingRigth = true;
		targetSet = false;
		deltaTargetCurrentPos = new Vector2(0,0);

		playerRigBdy = GetComponent<Rigidbody2D>();
		mousePos = Vector3.zero;

		heroGraphics = transform.Find("GFX");

		// heroAnimator = GetComponent<Animator>();
		if(heroAnimator == null)
		{
			//heroAnimator = heroGraphics.GetComponent<Animator>();
			heroAnimator = GetComponent<Animator>();
		}
		//Debug.Log(heroAnimator.name);
	}


	void FixedUpdate()
	{

		//IF(!cutscene && CC)
		if(canPlayerMove)
		{
			Movement();
		}

		/*switch (SelectedControllerType) 
		{
		case ControllerType.MouseAndArrows:
			Movement();
			break;
		
		case ControllerType.OnlyArrows:
			ArrowsAutoRun(KeyCode.LeftControl, KeyCode.RightControl);
			break;

		default:
			Debug.LogError("No Controller Type Selected");
			break;
		}*/



		if(Direction.x > 0 && !facingRigth)
		{
			Flip();
		}
		else if(Direction.x < 0 && facingRigth)
		{
			Flip();
		}

	}

	// Update is called once per frame
	void Update () 
	{
		PlayerInputs(KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.LeftControl,KeyCode.RightControl); // TODO Dose not work on MAC KeyCode.Command lel
	}

	public void Flip() // TODO update Flip() Method to use the sprite flip insted of scale *-1
	{
		facingRigth = !facingRigth;
		Vector3 theScale = heroGraphics.localScale;
		theScale.x *= -1;
		heroGraphics.localScale = theScale;
	}

	private void  Movement() // MainMovement
	{
		//ArrowsRun();
		ArrowsAutoRun();


		if(Mathf.Abs(Direction.x) > 0 || Mathf.Abs(Direction.y) > 0) 
		{
			cancelAutoRun = true;
		}


		if(!cancelAutoRun)
		{
			MouseAutoRun();
			targetSet = false;
			mousePos = Vector3.zero;
		}

		IsPlayerRunning();
	}

	private void ArrowsRun()
	{
		Direction = new Vector2(Input.GetAxisRaw("MouseAndArrowsX"), Input.GetAxisRaw("MouseAndArrowsY"));
		playerRigBdy.velocity = new Vector2 (Direction.x * MaxSpeed, Direction.y * MaxSpeed);
	}
	public void ScriptedEventMove(Transform actorPos, Transform targetPos )
	{
		Vector2 deltaVec = targetPos.position - actorPos.position;
		Direction = deltaVec.normalized;
		playerRigBdy.velocity = deltaVec.normalized * maxSpeed;
	}
    

	private void MouseAutoRun()
	{
		if(targetSet)
		{
			deltaTargetCurrentPos = mousePos - transform.position;
//			Debug.Log("TargetSet = start running");
		}
		Direction = deltaTargetCurrentPos.normalized;
		playerRigBdy.velocity = deltaTargetCurrentPos.normalized * maxSpeed;
	}


	private void ArrowsAutoRun() // UNDONE ArrowsAutoRun() GetKeyDown --> creats a bug where you need to Re-press buttons if you hold opposit buttons Not really a bug but
	{
		if(KeyLock) // when i press this
		{
			AutoRun = !AutoRun;
			Debug.Log("AutoRun = " + AutoRun);
			KeyLock = false;
		}

		if(AutoRun)
		{
			if(InputRecived) // TODO GetKeyDown --> creats a bug where you need to Re-press buttons if you hold opposit buttons Not really a bug but
			{
				Direction = new Vector2(Input.GetAxisRaw("MouseAndArrowsX"), Input.GetAxisRaw("MouseAndArrowsY"));// <-- This is in FIXEDUPDATE() might lose input
				playerRigBdy.velocity = new Vector2(Direction.x * MaxSpeed, Direction.y * MaxSpeed);
				InputRecived = false;
				Debug.Log("DIR = " + Direction );
			}
		}
		else
		{
			Direction = new Vector2(Input.GetAxisRaw("MouseAndArrowsX"), Input.GetAxisRaw("MouseAndArrowsY"));
            //playerRigBdy.velocity = new Vector2(Direction.x * MaxSpeed, Direction.y * MaxSpeed);
            playerRigBdy.AddForce(new Vector2(Direction.x * MaxSpeed, Direction.y * MaxSpeed));
		}


		/*
		bool keyLock = false;
		Direction = new Vector2(Input.GetAxisRaw("MouseAndArrowsX"), Input.GetAxisRaw("MouseAndArrowsY"));

		if((Input.GetKey(primaryButtonToHold) || Input.GetKey(SeconderyButtonToHold))) // when i press this
		{
			keyLock = true;
			autoRunDir = Direction;

			if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
			{
				AutoRun = true;
				Debug.Log("AutoRun = " + AutoRun + ") --- AutoDir = (" + autoRunDir + ") ---- Dir = ( " + Direction + ")");
			}
		}

		if(((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)) && !keyLock))
		{
			AutoRun = false;
			Debug.Log("AutoRun = " + AutoRun + ") --- AutoDir = (" + autoRunDir + ") ---- Dir = ( " + Direction + ")");
		}



		playerRigBdy.velocity = new Vector2(Direction.x * MaxSpeed, Direction.y * MaxSpeed);

		*/



	/*	Vector2 currentDir = new Vector2(Input.GetAxisRaw("MouseAndArrowsX"), Input.GetAxisRaw("MouseAndArrowsY"));

		if((Input.GetKey(primaryButtonToHold) || Input.GetKey(SeconderyButtonToHold))) // when i press this
		{
			if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
			{
				//Debug.Log("Same Diraction");
				AutoRun = true;
				//Debug.Log("AutoRun = " + AutoRun);
			}
		}
		else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
		{
			if(Direction.x != Input.GetAxisRaw("MouseAndArrowsX"))
			{
				AutoRun = false;
				Debug.Log("ONLY RIGHT IS CHEKCKED");
			}
		}


		if(AutoRun)
		{
			playerRigBdy.velocity = new Vector2(Direction.x * MaxSpeed, Direction.y * MaxSpeed);
		}
		else
		{
			Direction = new Vector2(Input.GetAxisRaw("MouseAndArrowsX"), Input.GetAxisRaw("MouseAndArrowsY"));
			playerRigBdy.velocity = new Vector2(Direction.x * MaxSpeed, Direction.y * MaxSpeed);
		}

*/


		/*else if(AutoRun = false) // dont do this
		{
			//Direction = new Vector2(Input.GetAxisRaw("MouseAndArrowsX"), Input.GetAxisRaw("MouseAndArrowsY"));
			Debug.Log("AutoRun --> " + AutoRun);
		}*/

		//playerRigBdy.velocity = new Vector2(Direction.x * MaxSpeed, Direction.y * MaxSpeed);
	}
	private void PlayerInputs(KeyCode L, KeyCode R, KeyCode U, KeyCode D , KeyCode primaryButtonToHold, KeyCode SeconderyButtonToHold)
	{
		if(Input.GetKeyDown(L) || Input.GetKeyDown(R) || Input.GetKeyDown(D) || Input.GetKeyDown(U))
		{
			InputRecived = true;
		}
		if((Input.GetKeyDown(primaryButtonToHold) || Input.GetKeyDown(SeconderyButtonToHold)))
		{
			KeyLock = true;
		}
		if(Input.GetMouseButtonDown(1) && MouseControll)
		{
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	Debug.Log("<color=teal>Mouse Click " + mousePos +" </color>");
			targetSet = true;
			cancelAutoRun = false;
		}
	}

	private void IsPlayerRunning()
	{
		if(Mathf.Abs(Direction.x)> 0 || Mathf.Abs(Direction.y) > 0)
		{
			heroAnimator.SetBool("Running" , true);
			//Debug.Log("Run Anim");
		}
		else
		{
			heroAnimator.SetBool("Running" , false);
			//Debug.Log("Idle Anim");
		}

	}
}
