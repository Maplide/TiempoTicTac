using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsUI : MonoBehaviour
{
    [Header("Nombre de la escena del menú")]
    public string menuSceneName = "MainMenu";

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
