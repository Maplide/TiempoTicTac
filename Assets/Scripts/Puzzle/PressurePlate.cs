using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Referencia a puerta")]
    public PuzzleDoor linkedDoor;

    [Header("Detección")]
    public string requiredTag = "RecordableBox"; // tag del objeto que debe presionar

    int objectsOnPlate;
    SpriteRenderer sr;
    Color defaultColor;
    public Color activeColor = Color.green;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            defaultColor = sr.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (requiredTag == "" || other.CompareTag(requiredTag))
        {
            objectsOnPlate++;
            UpdateState();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (requiredTag == "" || other.CompareTag(requiredTag))
        {
            objectsOnPlate = Mathf.Max(0, objectsOnPlate - 1);
            UpdateState();
        }
    }

    void UpdateState()
    {
        bool isActive = objectsOnPlate > 0;

        if (linkedDoor != null)
        {
            if (isActive)
                linkedDoor.OpenDoor();
            else
                linkedDoor.CloseDoor();
        }

        if (sr != null)
            sr.color = isActive ? activeColor : defaultColor;
    }
}
