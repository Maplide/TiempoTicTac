using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDoor : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;

    public bool isOpen;

    SpriteRenderer sr;
    Collider2D col;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        UpdateVisual();
    }

    public void OpenDoor()
    {
        isOpen = true;
        UpdateVisual();
    }

    public void CloseDoor()
    {
        isOpen = false;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (sr != null)
            sr.sprite = isOpen ? openSprite : closedSprite;

        if (col != null)
            col.enabled = !isOpen;   // si está abierta, no bloquea
    }
}
