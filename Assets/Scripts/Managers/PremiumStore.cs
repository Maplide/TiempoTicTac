using UnityEngine;
using UnityEngine.UI;

public class PremiumStore : MonoBehaviour
{
    [Header("Botones UI")]
    public Button buyButton;              // "Comprar Premium"
    public Button premiumContentButton;   // "Nivel Premium"

    private const string PREMIUM_KEY = "PremiumUnlocked";

    public void Start()
    {
        if (buyButton != null)
            buyButton.onClick.AddListener(BuyPremium);

        bool purchased = PlayerPrefs.GetInt(PREMIUM_KEY, 0) == 1;
        UpdateUI(purchased);
    }

    void BuyPremium()
    {
        PlayerPrefs.SetInt(PREMIUM_KEY, 1);
        PlayerPrefs.Save();

        Debug.Log("[PremiumStore] Contenido premium desbloqueado.");
        UpdateUI(true);
    }

    void UpdateUI(bool unlocked)
    {
        if (buyButton != null)
            buyButton.gameObject.SetActive(!unlocked);

        if (premiumContentButton != null)
            premiumContentButton.gameObject.SetActive(unlocked);
    }
}
