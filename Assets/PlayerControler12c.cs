using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControler12c : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 6;
    public float runSpeed = 10;

    [Header("Jump (Force)")]
    public float jumpForce = 300;
    public int maxJumps = 2;

    [Header("Refs")]
    public Rigidbody2D rb;
    public GroundChecker groundCheck;
    public SpriteRenderer spriteRenderer;
    public Animator anim; 

    private float _moveInput;
    private bool jumpQueued;
    private int jumpCount;
    private bool isJumping;

    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        if (!anim) anim = GetComponent<Animator>();
    }

    void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

       
        if (_moveInput > 0)
            spriteRenderer.flipX = false;
        else if (_moveInput < 0)
            spriteRenderer.flipX = true;

        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            jumpQueued = true;
            isJumping = true;
        }

        
        if (_moveInput != 0)
        {
            anim.SetFloat("IsMove", 1);
        }
        else
        {
            anim.SetFloat("IsMove", -1);
        }

       
        if (isJumping)
        {
            anim.SetBool("IsJump", true);
        }
        else
        {
            anim.SetBool("IsJump", false);
        }
    }

    void FixedUpdate()
    {
     
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(_moveInput * currentSpeed, rb.velocity.y);

        
        if (groundCheck != null && groundCheck.isGrounded)
        {
            jumpCount = 0;
            isJumping = false; 
        }

       
        if (jumpQueued)
        {
            rb.AddForce(Vector2.up * jumpForce);
            jumpQueued = false;
            jumpCount++;
        }
    }
}