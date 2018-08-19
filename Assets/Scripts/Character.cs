using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected ICharacterState currentState;
    private Rigidbody2D m_Rigidbody2D;

    protected float m_horizontalVelocity;
    protected float m_verticalVelocity;

    private float movementScale = 10f;
    private float gravityScale;
    [SerializeField] float m_JumpForce = 400f;                          // Amount of force added when the player jumps.

    public bool facingRight;

    private float collisionRaius = .5f;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsClimable;                        // A mask determining what is climable to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    protected HealthManager healthManager;
    protected int damageAmount = 1;

    public float HorizontalMove
    {
        get;
        set;
    }

    public float VerticalMove
    {
        get;
        protected set;
    }

    public bool IsRunning
    {
        get { return Mathf.Abs(HorizontalMove) > 0; }
    }

    public bool CanStand
    {
        get{return !CollidesWith(m_CeilingCheck, m_WhatIsGround);}
    }

    public bool CanClimb
    {
        get { return CollidesWith(m_GroundCheck, m_WhatIsClimable); }
    }

    public virtual bool JumpTriggered
    {
        get;
        set;
    }

    public bool IsImmune
    {
        get { return healthManager.IsImmune; }
    }

    public Animator CharAnimator
    {
        get;
        private set;
    }

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    private Vector3 m_Velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        CharAnimator = GetComponent<Animator>();
        gravityScale = m_Rigidbody2D.gravityScale;
        healthManager = GetComponent<HealthManager>();
    }


    // Update is called once per frame
    void Update()
    {
        GetInput();
        this.currentState.Update();
    }

    private void FixedUpdate()
    {
        this.currentState.FixedUpdate();
    }

    protected abstract void GetInput();

    public void Move(float horizontalVelocity)
    {
        Move(horizontalVelocity, m_Rigidbody2D.velocity.y);
    }

    public void Move(float horizontalVelocity, float verticalVelocity)
    {
        if ((horizontalVelocity > 0 && !facingRight) ||
            (horizontalVelocity < 0 && facingRight))
        {
            Flip();
        }
        Vector2 targetVelocity = new Vector2(horizontalVelocity, verticalVelocity);
                                             

        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity,
                                                    targetVelocity,
                                                    ref m_Velocity,
                                                    m_MovementSmoothing);
    }

    public void Jump()
    {
        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        JumpTriggered = false;
    }

    public bool IsGrounded()
    {
        return CollidesWith(m_GroundCheck, m_WhatIsGround);
    }

    public bool IsFalling()
    {
        return m_Rigidbody2D.velocity.y < 0 && !IsGrounded();
    }

    public void Crouch(bool shouldCrouch)
    {
        // Disable one of the colliders when crouching
        if (m_CrouchDisableCollider != null)
            m_CrouchDisableCollider.enabled = !shouldCrouch;
    }

    public void Climb(bool shouldClimb)
    {
        if(shouldClimb)
        {
            m_Rigidbody2D.gravityScale = 0;
        } else
        {
            m_Rigidbody2D.gravityScale = gravityScale;
        }
    }

    public virtual void Hit(int amount)
    {
        if(!healthManager.IsImmune)
        {
            healthManager.Hit(amount);
        }

    }

    public virtual void Damage(Character character)
    {
        character.Hit(damageAmount);
        m_Rigidbody2D.velocity = -1 * m_Rigidbody2D.velocity;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.OnTriggerExit2D(collision);
    }

    private bool CollidesWith(Collider2D collision, LayerMask mask)
    {
        return ((1 << collision.gameObject.layer) & mask) != 0;
    }

    private bool CollidesWith(Transform check, LayerMask mask)
    {
        Collider2D col = Physics2D.OverlapCircle(check.position, collisionRaius, mask);
        return col && CollidesWith(col, mask);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void ChangeState(ICharacterState nextState)
    {
        if (currentState != null)
        {
            this.currentState.Exit();
        }
        this.currentState = nextState;
        this.currentState.Enter(this);

    }

}
