using UnityEngine;
using System.Collections;

public class PlayerControllerCTRL : MonoBehaviour, IMovementEngin{


	/*public float MaxSpeed = 1.5f;
	float moveX;
	float moveY;
	Rigidbody2D PlayerOBj;*/
	public float  _speed;
	public Rigidbody2D PlayerOBj;

	private Vector2 Faceing;
	private bool spriteFacingRigth;



	public float Speed
	{ 
		get
		{
			return _speed;
		} 
		set 
		{ 
			_speed = value;
		} 
	}
	public Rigidbody2D PlayerRigBdy
	{
		get
		{
			return PlayerOBj;
		}
		set
		{
			PlayerOBj = value;
		}
	}
	public Vector2 Direction
	{
		get
		{
			return Faceing;
		}
		set
		{
		 Faceing = value;
		}
	}
	public bool SpriteFacingRigth
	{
		get
		{
			return spriteFacingRigth;
		}
		set
		{
			spriteFacingRigth = value;
		}
	}

	public PlayerControllerCTRL()
	{
		
	}
	// Use this for initialization
	void Start () 
	{

		PlayerRigBdy = GetComponent<Rigidbody2D>();
		spriteFacingRigth = true;
		/*moveX = 0;
		moveY = 0;	
		*/
	}
	

	void FixedUpdate()
	{
		MovementLogic();

		if(Faceing.x > 0 && !spriteFacingRigth)
		{
			Flip();
			//cancelMouseMovement = true;
		}
		else if(Faceing.x < 0 && spriteFacingRigth)
		{
			Flip();
			//cancelMouseMovement = true;
		}
	}

	public void MovementLogic()
	{
		if(Input.GetKey(KeyCode.LeftControl)) // Ctrl + s Trys to Save the game so dosent work in editor
		{
			/*moveX = Input.GetAxisRaw("Horizontal"); // TODO adjust these so HERO dont SLIDE AROUND
			moveY = Input.GetAxisRaw("Vertical");
			*/
			Faceing.x = Input.GetAxisRaw("Horizontal");
			Faceing.y = Input.GetAxisRaw("Vertical");

			//TODO Ctrl release works but when i press ctrl again i stop. Maybe keep him going but give back control
		}

		PlayerRigBdy.velocity = new Vector2(Faceing.x * Speed, Faceing.y * Speed);
	}

	public void Flip()
	{
		spriteFacingRigth = !spriteFacingRigth;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		//transform.localScale.x *= -1f;
	}
}
