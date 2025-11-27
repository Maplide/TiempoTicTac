using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDRecorderUI : MonoBehaviour
{
    [Header("Lógica")]
    public RecorderSelector recorderSelector;

    [Header("Iconos")]
    public Image recIcon;
    public Image playIcon;

    [Header("Textos")]
    public TMP_Text selectedObjectText;
    public TMP_Text helpText;

    [Header("Colores")]
    public Color idleColor = Color.white;
    public Color recActiveColor = Color.red;
    public Color replayActiveColor = Color.green;

    void Start()
    {
        if (helpText != null)
        {
            helpText.text = "Q/E: cambiar objeto | R: grabar/stop | T: reproducir/stop";
        }

        // Inicializamos íconos en color idle
        if (recIcon != null) recIcon.color = idleColor;
        if (playIcon != null) playIcon.color = idleColor;
    }

    void Update()
    {
        if (recorderSelector == null)
            return;

        var current = recorderSelector.Current;

        // Texto del objeto seleccionado
        if (selectedObjectText != null)
        {
            selectedObjectText.text = current != null
                ? "Objeto: " + current.gameObject.name
                : "Objeto: ninguno";
        }

        bool isRec = (current != null && current.IsRecording);
        bool isRep = (current != null && current.IsReplaying);

        // Colores de REC
        if (recIcon != null)
        {
            recIcon.color = isRec ? recActiveColor : idleColor;
        }

        // Colores de PLAY
        if (playIcon != null)
        {
            playIcon.color = isRep ? replayActiveColor : idleColor;
        }
    }
}
