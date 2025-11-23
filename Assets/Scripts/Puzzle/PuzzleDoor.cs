using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    [Header("Movimiento de apertura")]
    public Transform doorTransform;
    public Vector3 openOffset = new Vector3(0f, 3f, 0f);
    public float openSpeed = 3f;

    [Header("Estado")]
    public bool isOpen;

    Vector3 closedPosition;
    Vector3 targetPosition;

    void Awake()
    {
        if (doorTransform == null)
            doorTransform = transform;

        closedPosition = doorTransform.position;
        targetPosition = closedPosition;
    }

    void Update()
    {
        doorTransform.position = Vector3.Lerp(
            doorTransform.position,
            targetPosition,
            Time.deltaTime * openSpeed
        );
    }

    public void OpenDoor()
    {
        isOpen = true;
        targetPosition = closedPosition + openOffset;

        // Puedes desactivar collider si quieres que ya sea pasable
        var col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

    public void CloseDoor()
    {
        isOpen = false;
        targetPosition = closedPosition;

        var col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = true;
    }
}
