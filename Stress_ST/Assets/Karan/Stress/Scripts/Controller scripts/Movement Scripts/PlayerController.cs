using UnityEngine;

public class PlayerController : MonoBehaviour{

	public bool canPlayerMove;
    
    [SerializeField] private Animator heroAnimator;
	[SerializeField]private Transform heroGraphics;
	
	private Rigidbody2D playerRigBdy;
    private PlayerInputManager inputManager;
    private Player player;
	private bool facingRigth;

    private Vector2 direction;
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

	void Awake () 
	{
        inputManager = GameManager.Instance.PlayerInputManager;

        player = GetComponent<Player>();
		playerRigBdy = GetComponent<Rigidbody2D>();

        if (heroGraphics == null)
        {
            heroGraphics = transform.Find("GFX");
        }
        
		if(heroAnimator == null)
		{
			//heroAnimator = heroGraphics.GetComponent<Animator>();
			heroAnimator = GetComponentInChildren<Animator>();
		}
        //Debug.Log(heroAnimator.name);

        inputManager.MoveHorizontalRight += MoveRight;
        inputManager.MoveHorizontalLeft += MoveLeft;
        inputManager.MoveHorizontalNeutral += XNeutral;

        inputManager.MoveVerticalUp += MoveUp;
        inputManager.MoveVerticalDown += MoveDown;
        inputManager.MoveVerticalNeutral += YNeutral;


    }


    private void Start()
    {
        facingRigth = true;
        canPlayerMove = true;
      
    }

    void FixedUpdate()
	{

        //IF(!cutscene && CC)

        MovementLogic();
        PlayerRunningAnims();
        
		if(Direction.x > 0 && !facingRigth)
		{
			Flip();
		}
		else if(Direction.x < 0 && facingRigth)
		{
			Flip();
		}
	}

	public void Flip() // TODO update Flip() Method to use the sprite flip insted of scale *-1
	{
		facingRigth = !facingRigth;
		Vector3 theScale = heroGraphics.localScale;
		theScale.x *= -1;
		heroGraphics.localScale = theScale;
	}
    
	public void ScriptedEventMove(Transform actorPos, Transform targetPos )
	{
		Vector2 deltaVec = targetPos.position - actorPos.position;
		Direction = deltaVec.normalized;
		playerRigBdy.velocity = deltaVec.normalized * player.Stats.MovementSpeed;
	}
    
	private void PlayerRunningAnims() // Move all player anims to its own script
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


    #region InputEventMethods
    void MoveRight()
    {
        direction.x = 1;
    }
    void MoveLeft()
    {
        direction.x = -1;
    }
    void XNeutral()
    {
        direction.x = 0;
    }

    void MoveUp()
    {
        direction.y = 1;
    }
    void MoveDown()
    {
        direction.y = -1;
    }
    void YNeutral()
    {
        direction.y = 0;
    }
    #endregion

    void MovementLogic()
    {
       /// Direction = inputManager.MovementInputValues;
        playerRigBdy.AddForce(new Vector2(Direction.x * player.Stats.MovementSpeed, Direction.y * player.Stats.MovementSpeed));
    }
}
