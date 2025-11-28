using UnityEngine;

public class TimedPlatform : MonoBehaviour
{
    [Header("Tiempos (segundos)")]
    public float activeTime = 2f;    // tiempo visible
    public float inactiveTime = 2f;  // tiempo invisible
    public float startDelay = 0f;    // desfase inicial

    bool isActive = true;
    float timer;

    SpriteRenderer sr;
    Collider2D col;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        isActive = true;
        timer = startDelay > 0f ? startDelay : activeTime;
        UpdateState();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            isActive = !isActive;
            timer = isActive ? activeTime : inactiveTime;
            UpdateState();
        }
    }

    void UpdateState()
    {
        if (sr != null) sr.enabled = isActive;
        if (col != null) col.enabled = isActive;
    }
}
