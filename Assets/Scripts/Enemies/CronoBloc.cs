using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class CronoBloc : MonoBehaviour
{
    public enum State
    {
        Normal,
        Angry
    }

    [Header("Patrulla")]
    public Transform leftPoint;
    public Transform rightPoint;
    public float normalSpeed = 2f;
    public float angrySpeed = 4f;

    [Header("Estados")]
    public State currentState = State.Normal;
    public float angryDuration = 3f;   // cuánto dura enojado
    float angryTimer;

    [Header("Visual")]
    public Color normalColor = Color.white;
    public Color angryColor = Color.red;

    Rigidbody2D rb;
    SpriteRenderer sr;
    int direction = 1;  // 1 -> derecha, -1 -> izquierda

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.bodyType = RigidbodyType2D.Kinematic;   // plataforma enemiga, no la afectará la física
        rb.gravityScale = 0f;
    }

    void Start()
    {
        // Empezamos en el punto izquierdo si existe
        if (leftPoint != null)
            transform.position = new Vector3(leftPoint.position.x, leftPoint.position.y, transform.position.z);

        UpdateVisual();
    }

    void Update()
    {
        // manejo del temporizador enojado
        if (currentState == State.Angry)
        {
            angryTimer -= Time.deltaTime;
            if (angryTimer <= 0f)
            {
                SetState(State.Normal);
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (leftPoint == null || rightPoint == null) return;

        float speed = (currentState == State.Angry) ? angrySpeed : normalSpeed;

        // movimiento horizontal simple
        Vector2 velocity = rb.linearVelocity;
        velocity.x = direction * speed;
        rb.linearVelocity = velocity;

        // cambiar de dirección al llegar a extremos
        if (direction > 0 && transform.position.x >= rightPoint.position.x)
        {
            direction = -1;
        }
        else if (direction < 0 && transform.position.x <= leftPoint.position.x)
        {
            direction = 1;
        }
    }

    void SetState(State newState)
    {
        currentState = newState;

        if (currentState == State.Angry)
        {
            angryTimer = angryDuration;
        }

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (sr == null) return;

        if (currentState == State.Angry)
            sr.color = angryColor;
        else
            sr.color = normalColor;
    }

    /// <summary>
    /// Llamar a esto desde otros scripts cuando el jugador "falle".
    /// </summary>
    public void MakeAngry()
    {
        SetState(State.Angry);
    }

    // OPCIONAL: si el jugador choca con Crono-Bloc, lo puedes tomar como "fallo"
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Aquí puedes decidir: ¿lo mata? ¿solo se enoja?
            // Por ahora: solo nos enojamos para complicar el patrón.
            MakeAngry();
        }
    }
}
