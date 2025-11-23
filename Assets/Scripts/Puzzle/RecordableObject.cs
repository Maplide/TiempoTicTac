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
    public float recordInterval = 0.02f; // cada cuánto se guarda un frame (~50 fps)

    List<RecordedState> recordedStates = new List<RecordedState>();
    bool isRecording;
    bool isReplaying;
    float recordTimer;
    int replayIndex;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (rb != null)
        {
            rb.isKinematic = false;  // dejar que la física actúe mientras grabas
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

        if (rb != null)
        {
            rb.isKinematic = true;  // congelamos la física para seguir la grabación
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void StopReplay()
    {
        isReplaying = false;

        if (rb != null)
        {
            rb.isKinematic = false;  // vuelve a la física normal
        }
    }
}
