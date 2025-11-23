using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    float horizontalInput;
    bool isGrounded;
    bool jumpRequest;
    bool hasJumped;   // evita múltiples saltos seguidos en el aire

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- Movimiento horizontal (teclado) ---
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Actualizar estado de suelo
        UpdateGrounded();

        // Salto: solo permitimos pedir salto si está en el suelo y aún no hemos saltado
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded && !hasJumped)
            {
                jumpRequest = true;
            }
        }

        // (Más adelante aquí conectamos input móvil)
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        Vector2 velocity = rb.linearVelocity;
        velocity.x = horizontalInput * moveSpeed;
        rb.linearVelocity = velocity;

        // Salto
        if (jumpRequest)
        {
            jumpRequest = false;
            hasJumped = true; // ya saltamos, bloquear hasta tocar suelo

            // resetear velocidad vertical para que el salto sea consistente
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void UpdateGrounded()
    {
        if (groundCheck == null)
        {
            isGrounded = false;
            return;
        }

        Collider2D hit = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // solo consideramos grounded si tocamos suelo y estamos bajando o casi quietos
        bool groundedNow = (hit != null) && rb.linearVelocity.y <= 0.05f;

        // si acabamos de tocar el suelo, reseteamos hasJumped
        if (groundedNow && !isGrounded)
        {
            hasJumped = false;
        }

        isGrounded = groundedNow;
    }

    // Para móvil, estos siguen igual:
    public void SetHorizontalInput(float value)
    {
        horizontalInput = value;
    }

    public void Jump()
    {
        if (isGrounded && !hasJumped)
        {
            jumpRequest = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
