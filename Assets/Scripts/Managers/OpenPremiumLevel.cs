using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPremiumLevel : MonoBehaviour
{
    public string premiumScene;

    public void OpenLevel()
    {
        SceneManager.LoadScene(premiumScene);
    }
}
