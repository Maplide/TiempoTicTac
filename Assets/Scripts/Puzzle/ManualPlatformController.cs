using UnityEngine;

[RequireComponent(typeof(RecordableObject))]
public class ManualPlatformController : MonoBehaviour
{
    [Header("Movimiento manual (modo debug)")]
    public float moveSpeed = 3f;

    [Tooltip("Tecla para mover hacia arriba")]
    public KeyCode upKey = KeyCode.I;

    [Tooltip("Tecla para mover hacia abajo")]
    public KeyCode downKey = KeyCode.K;

    [Tooltip("Tecla para mover a la izquierda (opcional)")]
    public KeyCode leftKey = KeyCode.J;

    [Tooltip("Tecla para mover a la derecha (opcional)")]
    public KeyCode rightKey = KeyCode.L;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(upKey))
            dir += Vector2.up;
        if (Input.GetKey(downKey))
            dir += Vector2.down;
        if (Input.GetKey(leftKey))
            dir += Vector2.left;
        if (Input.GetKey(rightKey))
            dir += Vector2.right;

        if (dir != Vector2.zero)
        {
            Vector2 newPos = rb.position + dir.normalized * moveSpeed * Time.deltaTime;
            rb.MovePosition(newPos);
        }
    }
}
