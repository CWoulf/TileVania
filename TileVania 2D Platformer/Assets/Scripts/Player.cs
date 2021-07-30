using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config (Stuff set before we start)
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    // State
    bool isAlive = true;

    // Cached Component References
    Rigidbody2D rb;
    Animator anim;

    //Messages then methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        Jump();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVel = new Vector2(controlThrow * runSpeed, rb.velocity.y);
        rb.velocity = playerVel;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", playerHasHorizontalSpeed);
    }
    private void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelToAdd = new Vector2(0f, jumpSpeed);
            rb.velocity += jumpVelToAdd;
        }
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
