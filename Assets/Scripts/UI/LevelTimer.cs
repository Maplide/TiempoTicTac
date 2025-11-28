using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [Header("Tiempo del nivel (segundos)")]
    public float totalTime = 180f;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject timeOverPanel;

    float currentTime;

    void Start()
    {
        currentTime = totalTime;

        if (timeOverPanel != null)
            timeOverPanel.SetActive(false);
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            currentTime = 0;
            TimeOver();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimeOver()
    {
        Time.timeScale = 0f;

        if (timeOverPanel != null)
            timeOverPanel.SetActive(true);
    }

    // Botón: Reintentar
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Botón: Volver al menú
    public void GoToMainMenu(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
