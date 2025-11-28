using UnityEngine;

public class DevResetPrefs : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs borrados (Premium y Daily Reward reseteados).");
    }
}