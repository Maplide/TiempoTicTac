using UnityEngine;

public class BoxRespawner : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Entró al trigger: " + col.name);

        if (col.CompareTag("RecordableBox"))
        {
            Debug.Log("Caja detectada, reiniciando posición...");
            
            col.transform.position = respawnPoint.position;

            var rb = col.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
        else
        {
            Debug.Log("El objeto que entró NO tiene el tag correcto.");
        }
    }
}
