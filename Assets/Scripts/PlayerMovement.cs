using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    private bool isGliding = false;
    private float initialGravityScale;
    int jumpCount = 0;

    [SerializeField] private float MoveSpeed = 7f;
    [SerializeField] private float JumpForce = 14f;
    [SerializeField] private int   maxJumps = 2;
    [SerializeField] private float GlidingSpeed = -3f;


    private enum MovementState { idle, running, jumping, falling, gliding}

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        initialGravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    private void Update()
    {
        
        dirX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2( dirX * MoveSpeed, rb.velocity.y);
        if (IsGrounded())
        {
            jumpCount = 0;
        }
        if (Input.GetButtonDown("Jump") && (jumpCount < maxJumps || IsGrounded()))
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            jumpCount++;
        }
        
        if (Input.GetButton("Jump") && rb.velocity.y <= 0 && !IsGrounded())
        {
            isGliding = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, -GlidingSpeed);
        }
        else
        {
            isGliding = false;
            rb.gravityScale = initialGravityScale;
        }
        UpdateAnimationState();
    }
    
    
    
    
    
    private void UpdateAnimationState() 
    {
        MovementState state;
       
        if ((dirX > 0f) && IsGrounded())
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if ((dirX < 0f) && IsGrounded())
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state |= MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            if (isGliding)
            {
                state = MovementState.gliding;
            }
            else
            {
                state |= MovementState.falling;
            }
        }
        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        bool IsGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        return IsGrounded;
    }
}


