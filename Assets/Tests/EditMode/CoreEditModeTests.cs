using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class CoreEditModeTests
{
    [Test]
    public void PlayerController_HasPositiveMoveAndJump()
    {
        // Creamos un objeto con los componentes requeridos
        var go = new GameObject("PlayerTest");
        go.AddComponent<BoxCollider2D>();     // Collider requerido
        go.AddComponent<Rigidbody2D>();       // Rigidbody2D requerido

        var player = go.AddComponent<PlayerController2D>();

        Assert.Greater(player.moveSpeed, 0f, "moveSpeed debe ser > 0");
        Assert.Greater(player.jumpForce, 0f, "jumpForce debe ser > 0");
    }

    [Test]
    public void DailyReward_FirstTimeUser_ButtonIsActive()
    {
        const string LAST_REWARD_KEY = "LastRewardDate";
        PlayerPrefs.DeleteKey(LAST_REWARD_KEY);

        var go = new GameObject("DailyRewardManagerTest");
        var mgr = go.AddComponent<DailyRewardManager>();

        var buttonGO = new GameObject("RewardButton");
        var button = buttonGO.AddComponent<Button>();
        mgr.rewardButton = button;

        // Llamamos Start (ahora es public)
        mgr.Start();

        Assert.IsTrue(button.gameObject.activeSelf,
            "Para un usuario nuevo, el botón de recompensa diaria debe estar activo.");
    }

    [Test]
    public void PremiumStore_BuyPremium_UnlocksContentAndHidesBuyButton()
    {
        const string PREMIUM_KEY = "PremiumUnlocked";
        PlayerPrefs.DeleteKey(PREMIUM_KEY);

        var go = new GameObject("PremiumStoreTest");
        var store = go.AddComponent<PremiumStore>();

        var buyGO = new GameObject("BuyButton");
        var buyButton = buyGO.AddComponent<Button>();

        var premiumGO = new GameObject("PremiumContentButton");
        var premiumButton = premiumGO.AddComponent<Button>();

        store.buyButton = buyButton;
        store.premiumContentButton = premiumButton;

        // Inicializa UI y listener
        store.Start();

        // Simulamos clic en comprar
        buyButton.onClick.Invoke();

        int premiumValue = PlayerPrefs.GetInt(PREMIUM_KEY, 0);
        Assert.AreEqual(1, premiumValue, "PremiumUnlocked debe quedar en 1.");

        Assert.IsFalse(buyButton.gameObject.activeSelf,
            "El botón de comprar debe ocultarse tras la compra.");
        Assert.IsTrue(premiumButton.gameObject.activeSelf,
            "El botón de contenido premium debe mostrarse tras la compra.");
    }
}
