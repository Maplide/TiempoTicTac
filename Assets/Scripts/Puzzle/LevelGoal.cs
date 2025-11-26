using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    [Header("Nombre de la escena de créditos")]
    public string creditsSceneName = "Credits";

    [Header("Tag del jugador")]
    public string playerTag = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("[LevelGoal] Nivel completado. Cargando créditos...");
            SceneManager.LoadScene(creditsSceneName);
        }
    }
}
