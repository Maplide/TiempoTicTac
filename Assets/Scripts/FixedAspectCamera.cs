using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedAspectCamera : MonoBehaviour
{
    [Header("Aspecto objetivo (ej. 16:9)")]
    public float targetWidth = 16f;
    public float targetHeight = 9f;

    Camera cam;
    int lastScreenW;
    int lastScreenH;

    void Awake()
    {
        cam = GetComponent<Camera>();
        ApplyAspect();
    }

    void Update()
    {
        // Si cambia el tamaño de la ventana/pantalla, volvemos a ajustar
        if (Screen.width != lastScreenW || Screen.height != lastScreenH)
        {
            ApplyAspect();
        }
    }

    void ApplyAspect()
    {
        lastScreenW = Screen.width;
        lastScreenH = Screen.height;

        float targetAspect = targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1f)
        {
            // Barra negra arriba/abajo (letterbox)
            Rect rect = cam.rect;
            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0f;
            rect.y = (1f - scaleHeight) * 0.5f;
            cam.rect = rect;
        }
        else
        {
            // Barra negra a los lados (pillarbox)
            float scaleWidth = 1f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) * 0.5f;
            rect.y = 0f;
            cam.rect = rect;
        }
    }
}
