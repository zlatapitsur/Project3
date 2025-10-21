using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControler12c : MonoBehaviour
{
    public float moveSpeed = 1000;
    public float moveInput = 0;
    public float jumpForce = 300;
    public bool isJump = false;

    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public GroundChecker groundChecker;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput *
            moveSpeed *
            Time.deltaTime,
            rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isGrounded)
        {
            isJump = true; 
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput *
           moveSpeed *
           Time.fixedDeltaTime,
           rb.velocity.y);
        if (isJump && groundChecker.isGrounded)
        { 
        rb.AddForce(Vector2.up * jumpForce);
        isJump = false;
        }
    }
}

