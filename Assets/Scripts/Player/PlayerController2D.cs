// PlayerController2D.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground")]
    public LayerMask groundLayer;  // asignaremos la capa Ground

    [Header("Animación")]
    public Sprite[] idleFrames;    // frame 0 = quieto
    public Sprite[] walkFrames;    // frames 1-4 = caminar
    public Sprite[] jumpFrames;    // puede ser 1 frame si quieres
    public float frameRate = 10f;

    Rigidbody2D rb;
    SpriteRenderer sr;

    float horizontalInput;
    bool isGrounded;
    bool jumpRequest;

    enum AnimState
    {
        Idle,
        Walk,
        Jump
    }

    AnimState currentState;
    Sprite[] currentFrames;
    int currentFrameIndex;
    float frameTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        SetAnimState(AnimState.Idle);
    }

    void Update()
    {
        // input horizontal
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // flip visual según dirección
        if (horizontalInput > 0.01f)
            sr.flipX = false;
        else if (horizontalInput < -0.01f)
            sr.flipX = true;

        // salto por teclado
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }

        UpdateAnimState();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        // movimiento horizontal
        Vector2 v = rb.linearVelocity;
        v.x = horizontalInput * moveSpeed;
        rb.linearVelocity = v;

        // salto
        if (jumpRequest && isGrounded)
        {
            jumpRequest = false;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            if (GameAudioManager.Instance != null)
            {
                GameAudioManager.Instance.PlayJump();
            }
        }
        else
        {
            jumpRequest = false;
        }
    }

    void UpdateAnimState()
    {
        float speedX = Mathf.Abs(rb.linearVelocity.x);

        if (!isGrounded)
        {
            SetAnimState(AnimState.Jump);
        }
        else if (speedX > 0.05f)
        {
            SetAnimState(AnimState.Walk);
        }
        else
        {
            SetAnimState(AnimState.Idle);
        }
    }

    void SetAnimState(AnimState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;

        switch (currentState)
        {
            case AnimState.Idle:
                currentFrames = idleFrames;
                break;
            case AnimState.Walk:
                currentFrames = walkFrames;
                break;
            case AnimState.Jump:
                currentFrames = jumpFrames;
                break;
        }

        currentFrameIndex = 0;
        frameTimer = 0f;
        ApplyCurrentFrame();
    }

    void UpdateAnimation()
    {
        if (currentFrames == null || currentFrames.Length == 0 || sr == null)
            return;

        frameTimer += Time.deltaTime;
        float frameDuration = 1f / frameRate;

        if (frameTimer >= frameDuration)
        {
            frameTimer -= frameDuration;
            currentFrameIndex++;
            if (currentFrameIndex >= currentFrames.Length)
            {
                currentFrameIndex = 0;
            }
            ApplyCurrentFrame();
        }
    }

    void ApplyCurrentFrame()
    {
        if (currentFrames == null || currentFrames.Length == 0)
            return;

        int index = Mathf.Clamp(currentFrameIndex, 0, currentFrames.Length - 1);
        sr.sprite = currentFrames[index];
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }

    public void SetHorizontalInput(float value)
    {
        horizontalInput = value;
    }

    public void Jump()
    {
        if (isGrounded)
            jumpRequest = true;
    }
}
