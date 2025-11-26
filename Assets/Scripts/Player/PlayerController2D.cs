using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground")]
    public LayerMask groundLayer;  // asignaremos la capa Ground

    Rigidbody2D rb;
    float horizontalInput;
    bool isGrounded;
    bool jumpRequest;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // input horizontal
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // salto por teclado
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }
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
        }
        else
        {
            jumpRequest = false;
        }
    }

    // Detectamos si está tocando suelo por colisiones
    void OnCollisionStay2D(Collision2D collision)
    {
        // Revisamos si el otro está en la capa Ground
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            // Miramos los contactos: si alguno viene desde abajo (normal.y > 0.5) lo tomamos como suelo
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

    // Para botones móviles
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
