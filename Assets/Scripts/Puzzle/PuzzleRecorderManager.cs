using System.Collections.Generic;
using UnityEngine;

public class PuzzleRecorderManager : MonoBehaviour
{
    [Header("Objetos grabables")]
    public List<RecordableObject> recordables = new List<RecordableObject>();

    [Header("Teclas debug")]
    public KeyCode recordKey = KeyCode.R;
    public KeyCode replayKey = KeyCode.T;

    // Expuesto para el HUD
    public bool IsRecording { get; private set; }

    void Update()
    {
        if (Input.GetKeyDown(recordKey))
        {
            StartRecordingAll();
        }

        if (Input.GetKeyDown(replayKey))
        {
            StopRecordingAndReplayAll();
        }
    }

    public void StartRecordingAll()
    {
        IsRecording = true;

        foreach (var r in recordables)
        {
            if (r != null)
                r.StartRecording();
        }

        Debug.Log("[Recorder] START RECORDING");
    }

    public void StopRecordingAndReplayAll()
    {
        IsRecording = false;

        foreach (var r in recordables)
        {
            if (r != null)
            {
                r.StopRecording();
                r.StartReplay();
            }
        }

        Debug.Log("[Recorder] STOP RECORDING & REPLAY");
    }

    // Métodos para UI (los usaremos después en móvil)
    public void StartRecordingFromUI()
    {
        StartRecordingAll();
    }

    public void StopRecordingAndReplayFromUI()
    {
        StopRecordingAndReplayAll();
    }
}
