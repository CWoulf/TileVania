using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config (Stuff set before we start)
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    // State
    bool isAlive = true;

    // Cached Component References
    Rigidbody2D rb;
    Animator anim;
    Collider2D myCollider2D;
    float gravityScaleAtStart;

    //Messages then methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
    }

    private void Run()
    {
        float movement = Input.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVel = new Vector2(movement * runSpeed, rb.velocity.y);
        rb.velocity = playerVel;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", playerHasHorizontalSpeed);
    }
    private void Jump()
    {
        if(!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }
        
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelToAdd = new Vector2(0f, jumpSpeed);
            rb.velocity += jumpVelToAdd;
        }
    }
    private void ClimbLadder()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            anim.SetBool("isClimbing", false);
            rb.gravityScale = gravityScaleAtStart;
            return; 
        }

        float climbing = Input.GetAxis("Vertical"); // value is between -1 to +1
        Vector2 climbVel = new Vector2(rb.velocity.x, climbing * climbSpeed);
        rb.velocity = climbVel;
        rb.gravityScale = 0;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        anim.SetBool("isClimbing", playerHasVerticalSpeed);
        
    }    
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
