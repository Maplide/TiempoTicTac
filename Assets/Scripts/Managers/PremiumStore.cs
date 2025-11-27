using UnityEngine;
using UnityEngine.UI;

public class PremiumStore : MonoBehaviour
{
    [Header("Botones UI")]
    public Button buyButton;              // Botón "Comprar Premium"
    public Button premiumContentButton;   // Botón "Contenido Premium"

    private const string PREMIUM_KEY = "PremiumUnlocked";

    void Start()
    {
        bool purchased = PlayerPrefs.GetInt(PREMIUM_KEY, 0) == 1;
        UpdateUI(purchased);

        buyButton.onClick.AddListener(BuyPremium);
    }

    void BuyPremium()
    {
        // Simulamos compra exitosa
        PlayerPrefs.SetInt(PREMIUM_KEY, 1);
        PlayerPrefs.Save();

        Debug.Log("Contenido Premium desbloqueado!");
        UpdateUI(true);
    }

    void UpdateUI(bool unlocked)
    {
        buyButton.gameObject.SetActive(!unlocked);
        premiumContentButton.gameObject.SetActive(unlocked);
    }
}
