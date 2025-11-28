using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RecordableRespawn : MonoBehaviour
{
    Vector3 startPosition;
    Rigidbody2D rb;
    RecordableObject rec;

    void Start()
    {
        // Guardamos la posición inicial de la caja al cargar la escena
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rec = GetComponent<RecordableObject>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si toca la zona de caída, se resetea
        if (other.CompareTag("FallZone"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        Debug.Log("[RecordableRespawn] Respawneando caja a posición inicial.");

        // Volver a la posición inicial
        transform.position = startPosition;

        // Resetear física
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Detener grabación/replay si estaban activos
        if (rec != null)
        {
            rec.StopRecording();
            rec.StopReplay();
        }
    }
}
