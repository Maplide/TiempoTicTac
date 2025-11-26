using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject controlsPanel;

    public void PlayGame()
    {
        Debug.Log("[MainMenuUI] PlayGame llamado");
        SceneManager.LoadScene("Level_Lab01");
    }

    public void OpenControls()
    {
        Debug.Log("[MainMenuUI] OpenControls llamado");
        if (controlsPanel != null)
            controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        Debug.Log("[MainMenuUI] CloseControls llamado");
        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        Debug.Log("[MainMenuUI] OpenCredits llamado");
        SceneManager.LoadScene("Credits");
    }

    public void ExitGame()
    {
        Debug.Log("[MainMenuUI] ExitGame llamado");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
