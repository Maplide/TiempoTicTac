using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Estado visual")]
    public Sprite inactiveSprite;   // rojo
    public Sprite activeSprite;     // verde

    [Header("Referencia a puerta")]
    public SpriteDoor linkedDoor;

    [Header("Detección")]
    public string requiredTag = "RecordableBox";

    int objectsOnPlate;
    bool lastActive;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = inactiveSprite;
        sr.color = Color.white; // importante, para que no se tiña raro
        lastActive = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(requiredTag))
        {
            objectsOnPlate++;
            UpdateState();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(requiredTag))
        {
            objectsOnPlate = Mathf.Max(0, objectsOnPlate - 1);
            UpdateState();
        }
    }

    void UpdateState()
    {
        bool isActive = objectsOnPlate > 0;

        sr.sprite = isActive ? activeSprite : inactiveSprite;

        // AUDIO: activar/desactivar plataforma
        if (GameAudioManager.Instance != null && isActive != lastActive)
        {
            if (isActive)
                GameAudioManager.Instance.PlayPlatformOn();
            else
                GameAudioManager.Instance.PlayPlatformOff();
        }

        lastActive = isActive;

        if (linkedDoor != null)
        {
            if (isActive)
                linkedDoor.OpenDoor();
            else
                linkedDoor.CloseDoor();
        }
    }
}
