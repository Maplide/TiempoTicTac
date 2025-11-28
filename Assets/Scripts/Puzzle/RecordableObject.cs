using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RecordableObject : MonoBehaviour
{
    [System.Serializable]
    public struct RecordedState
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    [Header("Grabación")]
    public float recordInterval = 0.02f;

    [Header("Opciones de física")]
    [Tooltip("Solo marcar en objetos dinámicos como cajas. " +
             "NO marcar en plataformas que deben ser siempre kinematic.")]
    public bool controlKinematic = true;

    [Header("Selección visual")]
    [Tooltip("SpriteRenderer usado SOLO para el contorno/halo de selección. " +
             "Debe ser un hijo del objeto. Si se deja vacío, no se mostrará selección.")]
    public SpriteRenderer selectionRenderer;

    // Estados internos
    List<RecordedState> recordedStates = new List<RecordedState>();
    bool isRecording;
    bool isReplaying;
    float recordTimer;
    int replayIndex;

    Rigidbody2D rb;

    // Exponer estados para RecorderSelector
    public bool IsRecording => isRecording;
    public bool IsReplaying => isReplaying;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Si se asigna un halo de selección, se apaga al inicio
        if (selectionRenderer != null)
        {
            selectionRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (isRecording)
        {
            recordTimer -= Time.deltaTime;
            if (recordTimer <= 0f)
            {
                recordTimer = recordInterval;
                SaveCurrentState();
            }
        }
        else if (isReplaying)
        {
            if (recordedStates.Count == 0)
            {
                StopReplay();
                return;
            }

            if (replayIndex < recordedStates.Count)
            {
                var s = recordedStates[replayIndex];
                transform.position = s.position;
                transform.rotation = s.rotation;
                replayIndex++;
            }
            else
            {
                StopReplay();
            }
        }
    }

    void SaveCurrentState()
    {
        RecordedState s;
        s.position = transform.position;
        s.rotation = transform.rotation;
        recordedStates.Add(s);
    }

    public void StartRecording()
    {
        recordedStates.Clear();
        isRecording = true;
        isReplaying = false;
        recordTimer = 0f;

        // SOLO cajas dinámicas vuelven a modo físico
        if (rb != null && controlKinematic && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // Asegurar modo dinámico real
        }
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void StartReplay()
    {
        if (recordedStates.Count == 0)
            return;

        isRecording = false;
        isReplaying = true;
        replayIndex = 0;

        // SOLO cajas dinámicas pasan a kinematic durante replay
        if (rb != null && controlKinematic && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void StopReplay()
    {
        isReplaying = false;

        // SOLO cajas dinámicas recuperan dinámica normal
        if (rb != null && controlKinematic && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    // =========================
    //      SELECCIÓN VISUAL
    // =========================
    public void SetSelected(bool selected)
    {
        if (selectionRenderer == null)
            return;

        selectionRenderer.enabled = selected;
    }
}
