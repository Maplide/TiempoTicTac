using System.Collections.Generic;
using UnityEngine;

public class RecorderSelector : MonoBehaviour
{
    [Header("Objetos grabables (cajas, plataformas)")]
    public List<RecordableObject> recordables = new List<RecordableObject>();

    [Header("Teclas de control")]
    public KeyCode nextKey = KeyCode.E;      // siguiente objeto
    public KeyCode prevKey = KeyCode.Q;      // objeto anterior
    public KeyCode recordKey = KeyCode.R;    // grabar / stop
    public KeyCode replayKey = KeyCode.T;    // reproducir / stop

    int currentIndex = 0;

    // Hacemos Current PÚBLICO para que el HUD pueda leerlo
    public RecordableObject Current
    {
        get
        {
            if (recordables.Count == 0) return null;
            if (currentIndex < 0 || currentIndex >= recordables.Count) return null;
            return recordables[currentIndex];
        }
    }

    void Start()
    {
        UpdateSelectionVisual();
    }

    void Update()
    {
        if (recordables.Count == 0)
            return;

        // Cambiar selección
        if (Input.GetKeyDown(nextKey))
        {
            currentIndex++;
            if (currentIndex >= recordables.Count)
                currentIndex = 0;
            UpdateSelectionVisual();
        }
        else if (Input.GetKeyDown(prevKey))
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = recordables.Count - 1;
            UpdateSelectionVisual();
        }

        // Grabar / stop
        if (Input.GetKeyDown(recordKey))
        {
            StartRecordingCurrent();
        }

        // Reproducir / stop
        if (Input.GetKeyDown(replayKey))
        {
            StartReplayCurrent();
        }
    }

    void StartRecordingCurrent()
    {
        var obj = Current;
        if (obj == null) return;

        // Si ya está grabando, R detiene
        if (obj.IsRecording)
        {
            obj.StopRecording();
            Debug.Log("[RecorderSelector] Deteniendo grabación de: " + obj.name);
            return;
        }

        // Si estaba en replay, lo paramos
        if (obj.IsReplaying)
        {
            obj.StopReplay();
        }

        obj.StartRecording();
        Debug.Log("[RecorderSelector] Comenzando grabación de: " + obj.name);
    }

    void StartReplayCurrent()
    {
        var obj = Current;
        if (obj == null) return;

        // Si ya está reproduciendo, T detiene
        if (obj.IsReplaying)
        {
            obj.StopReplay();
            Debug.Log("[RecorderSelector] Deteniendo replay de: " + obj.name);
            return;
        }

        if (obj.IsRecording)
        {
            obj.StopRecording();
        }

        obj.StartReplay();
        Debug.Log("[RecorderSelector] Reproduciendo objeto: " + obj.name);
    }

    void UpdateSelectionVisual()
    {
        for (int i = 0; i < recordables.Count; i++)
        {
            if (recordables[i] == null) continue;
            bool isSelected = (i == currentIndex);
            recordables[i].SetSelected(isSelected);
        }

        if (Current != null)
        {
            Debug.Log("[RecorderSelector] Objeto seleccionado: " + Current.name);
        }
    }
}
