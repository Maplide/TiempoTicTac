using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    public Transform target;

    [Header("Offset (desfase)")]
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    [Header("Suavizado")]
    public float smoothTime = 0.2f;

    [Header("Límites opcionales")]
    public bool useLimits = false;
    public float minX, maxX;
    public float minY, maxY;

    Vector3 currentVelocity;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        // Aplicar límites si están activos
        if (useLimits)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
        }

        // Movimiento suave
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            smoothTime
        );
    }
}
