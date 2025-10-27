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

    [Header("Refs")]
    public Rigidbody2D rb;
    public GroundChecker groundCheck;
    public SpriteRenderer spriteRenderer;

    // Internal state (читання інпуту в Update, застосування у FixedUpdate)
    private float _moveInput;
    private bool _jumpQueued;

    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1) Вхід користувача
        _moveInput = Input.GetAxisRaw("Horizontal"); // Raw – більш “чіткий” контроль

        // 2) Фліп спрайта
        if (_moveInput > 0) spriteRenderer.flipX = false;
        else if (_moveInput < 0) spriteRenderer.flipX = true;

        // 3) Черга стрибка (натискання ловимо тут, виконуємо у FixedUpdate)
        if (Input.GetKeyDown(KeyCode.Space))
            _jumpQueued = true;
    }

    void FixedUpdate()
    {
        // 1) Рух (фізика)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(_moveInput * currentSpeed, rb.velocity.y);

        // 2) Стрибок (фізика)
        if (_jumpQueued && groundCheck != null && groundCheck.isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce); // ForceMode2D.Force за замовчуванням
        }

        // Стрибок опрацьовано — скидаємо прапорець
        _jumpQueued = false;
    }
}