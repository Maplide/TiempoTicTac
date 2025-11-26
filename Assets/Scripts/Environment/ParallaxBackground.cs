using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("A qué seguir (normalmente la cámara)")]
    public Transform target;   // Main Camera

    [Header("Factor de parallax")]
    [Tooltip("0 = fondo fijo, 1 = se mueve igual que la cámara. Recomiendo 0.2 - 0.5")]
    public float parallaxFactor = 0.3f;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        if (target == null && Camera.main != null)
        {
            target = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Solo queremos que acompañe en X (y si quieres en Y también)
        float moveX = target.position.x * parallaxFactor;
        float moveY = target.position.y * parallaxFactor * 0.3f; // menos en Y

        transform.position = new Vector3(
            startPosition.x + moveX,
            startPosition.y + moveY,
            startPosition.z
        );
    }
}
