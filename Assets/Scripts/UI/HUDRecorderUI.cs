using UnityEngine;
using TMPro;

public class HUDRecorderUI : MonoBehaviour
{
    [Header("Referencias")]
    public PuzzleRecorderManager recorder;
    public GameObject recIconGroup;        
    public TMP_Text instructionsText;

    void Update()
    {
        if (recorder == null)
            return;

        bool recording = recorder.IsRecording;

        if (recIconGroup != null)
        {
            recIconGroup.SetActive(recording);
        }

        if (instructionsText != null)
        {
            if (recording)
                instructionsText.text = "Grabando... pulsa T para reproducir";
            else
                instructionsText.text = "Pulsa R para grabar, T para reproducir";
        }
    }
}
