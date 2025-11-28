// CronoBloc.cs
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
    public float angryDuration = 3f;
    float angryTimer;

    [Header("Visual")]
    public Color normalColor = Color.white;
    public Color angryColor = Color.red;

    [Header("Animación")]
    public Sprite[] idleFrames;   // puede ser 1 frame
    public Sprite[] moveFrames;   // 4 frames caminar
    public float frameRate = 8f;

    Rigidbody2D rb;
    SpriteRenderer sr;
    int direction = 1;

    Sprite[] currentFrames;
    int currentFrameIndex;
    float frameTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }

    void Start()
    {
        if (leftPoint != null)
            transform.position = new Vector3(leftPoint.position.x, leftPoint.position.y, transform.position.z);

        UpdateVisual();
        SetAnimFrames(moveFrames);
    }

    void Update()
    {
        if (currentState == State.Angry)
        {
            angryTimer -= Time.deltaTime;
            if (angryTimer <= 0f)
            {
                SetState(State.Normal);
            }
        }

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (leftPoint == null || rightPoint == null) return;

        float speed = (currentState == State.Angry) ? angrySpeed : normalSpeed;

        Vector2 velocity = rb.linearVelocity;
        velocity.x = direction * speed;
        rb.linearVelocity = velocity;

        if (direction > 0 && transform.position.x >= rightPoint.position.x)
        {
            direction = -1;
            sr.flipX = true;
        }
        else if (direction < 0 && transform.position.x <= leftPoint.position.x)
        {
            direction = 1;
            sr.flipX = false;
        }

        float absSpeed = Mathf.Abs(rb.linearVelocity.x);
        if (absSpeed > 0.01f)
        {
            SetAnimFrames(moveFrames);
        }
        else
        {
            SetAnimFrames(idleFrames);
        }
    }

    void SetState(State newState)
    {
        currentState = newState;

        if (currentState == State.Angry)
        {
            angryTimer = angryDuration;

            if (GameAudioManager.Instance != null)
            {
                GameAudioManager.Instance.PlayCronoStep();
            }
        }

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (sr == null) return;

        sr.color = (currentState == State.Angry) ? angryColor : normalColor;
    }

    void SetAnimFrames(Sprite[] frames)
    {
        if (frames == null || frames.Length == 0)
            return;

        if (currentFrames == frames)
            return;

        currentFrames = frames;
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
        if (currentFrames == null || currentFrames.Length == 0) return;

        int index = Mathf.Clamp(currentFrameIndex, 0, currentFrames.Length - 1);
        sr.sprite = currentFrames[index];
    }

    public void MakeAngry()
    {
        SetState(State.Angry);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            MakeAngry();
        }
    }
}
