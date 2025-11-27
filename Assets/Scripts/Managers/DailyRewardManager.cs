using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyRewardManager : MonoBehaviour
{
    public Button rewardButton;

    private const string LAST_REWARD_KEY = "LastRewardDate";

    void Start()
    {
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

        // Disponibilidad cada 24 horas
        if (difference.TotalHours >= 24)
        {
            rewardButton.gameObject.SetActive(true);
        }
        else
        {
            rewardButton.gameObject.SetActive(false);
        }
    }

    void ClaimReward()
    {
        Debug.Log("Recompensa diaria reclamada!");
        PlayerPrefs.SetString(LAST_REWARD_KEY, DateTime.Now.ToString());
        PlayerPrefs.Save();

        rewardButton.gameObject.SetActive(false);
    }
}
