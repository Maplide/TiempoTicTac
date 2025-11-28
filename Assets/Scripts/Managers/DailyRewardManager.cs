using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DailyRewardManager : MonoBehaviour
{
    [Header("Botón de recompensa")]
    public Button rewardButton;

    [Header("Mensaje visual")]
    public TextMeshProUGUI rewardMessage;  
    public float fadeDuration = 3f;

    private const string LAST_REWARD_KEY = "LastRewardDate";

    public void Start()
    {
        if (rewardMessage != null)
        {
            // Ocultar mensaje al inicio
            Color c = rewardMessage.color;
            c.a = 0f;
            rewardMessage.color = c;
        }

        CheckRewardAvailability();
        rewardButton.onClick.AddListener(ClaimReward);
    }

    void CheckRewardAvailability()
    {
        string lastDateStr = PlayerPrefs.GetString(LAST_REWARD_KEY, "");

        if (string.IsNullOrEmpty(lastDateStr))
        {
            rewardButton.gameObject.SetActive(true);
            return;
        }

        DateTime lastDate = DateTime.Parse(lastDateStr);
        TimeSpan difference = DateTime.Now - lastDate;

        // Ajusta esto a 24 horas reales, aquí está 1 minuto para pruebas.
        if (difference.TotalMinutes >= 1)
            rewardButton.gameObject.SetActive(true);
        else
            rewardButton.gameObject.SetActive(false);
    }

    void ClaimReward()
    {
        Debug.Log("Recompensa diaria reclamada!");

        PlayerPrefs.SetString(LAST_REWARD_KEY, DateTime.Now.ToString());
        PlayerPrefs.Save();

        rewardButton.gameObject.SetActive(false);

        // Mostrar mensaje
        if (rewardMessage != null)
            StartCoroutine(FadeRewardMessage());
    }

    // ======================
    //     FADE OUT SUAVE
    // ======================
    System.Collections.IEnumerator FadeRewardMessage()
    {
        // Fase 1: Mostrar inmediatamente (alpha 1)
        Color color = rewardMessage.color;
        color.a = 1f;
        rewardMessage.color = color;

        // Espera antes de iniciar desvanecimiento
        yield return new WaitForSeconds(1f);

        // Fase 2: Desvanecer en fadeDuration segundos
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            color.a = alpha;
            rewardMessage.color = color;

            yield return null;
        }

        // Asegurar que termine invisible
        color.a = 0f;
        rewardMessage.color = color;
    }
}
