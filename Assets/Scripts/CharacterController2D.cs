using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
    //[Range(0, 1)] [SerializeField] private float m_ClimbSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsClimbable;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
    [SerializeField] private Transform m_ClimbCheck;

	const float k_GroundedRadius = .5f; // Radius of the overlap circle to determine if grounded
    const float k_ClimbRadius = .5f;
	private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_Falling;
    private bool m_Climbable;
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

    private float m_GravityScale;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
    public UnityEvent OffGroundEvent;
    public UnityEvent ClimbEnterEvent;
    public UnityEvent ClimbExitEvent;
    public UnityEvent OnJumpEvent;
    public UnityEvent OnFallEvent;


	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;
    private bool m_wasClimbing = false;



	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_GravityScale = m_Rigidbody2D.gravityScale;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
        
        if (OffGroundEvent == null)
            OffGroundEvent = new UnityEvent();
        
        if (ClimbEnterEvent == null)
            ClimbEnterEvent = new UnityEvent();

        if (ClimbExitEvent == null)
            ClimbExitEvent = new UnityEvent();

        if (OnJumpEvent == null)
            OnJumpEvent = new UnityEvent();
        
        if (OnFallEvent == null)
            OnFallEvent = new UnityEvent();
        
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
        bool wasFalling = m_Falling;
		
		if(IsGrounded())
        {
    		m_Grounded = true;
            m_Falling = false;
            if (!wasGrounded)
            {
                OnLandEvent.Invoke();
            }
        }else
        {
            m_Grounded = false;
            if(wasGrounded)
            {
                OffGroundEvent.Invoke();
            }
            if(!wasFalling && m_Rigidbody2D.gravityScale > 0.01 && m_Rigidbody2D.velocity.y < 0)
            {
                m_Falling = true;
                OnFallEvent.Invoke(); 
            }
        }

	}

    private bool IsGrounded()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    bool IsCrouched(float verticalMove)
    {
        bool crouch = verticalMove < 0;
        // If not crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }
        return crouch;
    }

    private bool CollidesWith(Collider2D collision, LayerMask mask)
    {
        return ((1 << collision.gameObject.layer) & mask) != 0;
    }

    private bool CollidesWith(LayerMask mask)
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, k_GroundedRadius, mask);
        return col && CollidesWith(col, mask);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit " + collision.name);
        if (CollidesWith(collision, m_WhatIsClimbable))
        {
            SetClimbing(false);
        }
    }

    private void SetClimbing(bool climbing)
    {
        if (!m_wasClimbing && climbing)
        {
            ClimbEnterEvent.Invoke();
            m_wasClimbing = true;
            m_Rigidbody2D.gravityScale = 0;
        } else if (m_wasClimbing && !climbing)
        {
            ClimbExitEvent.Invoke();
            m_wasClimbing = false;
            m_Rigidbody2D.gravityScale = m_GravityScale;
        }
    }


    public void Move(float horizontalMove, float verticalMove, bool jump)
	{

        bool crouch = IsCrouched(verticalMove) && m_Grounded;
        Vector3 targetVelocity;
        m_Climbable = CollidesWith(m_WhatIsClimbable);
        //Debug.Log("Climbable?: " + CollidesWith(m_WhatIsClimbable));

		//only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl || m_Climbable)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				horizontalMove *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

            // Move the character by finding the target velocity


            if(m_Climbable && (m_Rigidbody2D.gravityScale < 0.01 || verticalMove > 0))
            {
                SetClimbing(true);
                targetVelocity = new Vector2(horizontalMove * 10f, verticalMove * 10f);
            } else
            {
                SetClimbing(false);
                targetVelocity = new Vector2(horizontalMove * 10f, m_Rigidbody2D.velocity.y);   
            }
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (horizontalMove > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (horizontalMove < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		// If the player should jump...
        if ((m_Grounded || m_Climbable) && jump)
		{
            // Add a vertical force to the player.
            SetClimbing(false);
            OnJumpEvent.Invoke();
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
