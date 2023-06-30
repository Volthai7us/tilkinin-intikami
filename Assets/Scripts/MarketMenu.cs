using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketMenu : MonoBehaviour
{
    public Fox fox;
    public TextMeshProUGUI deadBirdsText;
    public TextMeshProUGUI upgradeCostText;

    private void OnEnable()
    {
        upgradeCostText.text = "Upgrade Cost: " + fox.NextUpgradeCost();
    }

    // Market menüsündeki yükseltmeler için metotlar
    public void BuySpeedUpgrade()
    {
        int cost = fox.NextUpgradeCost();
        int [] levels = fox.levels;

        // Eğer ölü kuş sayısı yeterliyse yükseltme yapılıyor
        if (fox.DeadBirds >= cost)
        {
            fox.DeadBirds -= cost;
            fox.UpgradeSpeed();
            UpdateDeadBirdsText();
        }

        upgradeCostText.text = "Upgrade Cost: " + fox.NextUpgradeCost();
    }

    public void BuyJumpUpgrade()
    {
        int cost = fox.NextUpgradeCost();
        int [] levels = fox.levels;
        if (fox.DeadBirds >= cost)
        {
            fox.DeadBirds -= cost;
            fox.UpgradeJump();
            UpdateDeadBirdsText();
        }

        upgradeCostText.text = "Upgrade Cost: " + fox.NextUpgradeCost();
    }

    public void BuyAttackUpgrade()
    {
        int cost = fox.NextUpgradeCost();
        int [] levels = fox.levels;
        if (fox.DeadBirds >= cost)
        {
            fox.DeadBirds -= cost;
            fox.UpgradeAttack();
            UpdateDeadBirdsText();
        }

        upgradeCostText.text = "Upgrade Cost: " + fox.NextUpgradeCost();
    }

    public void BuyRunUpgrade()
    {
        int cost = fox.NextUpgradeCost();
        int [] levels = fox.levels;
        if (fox.DeadBirds >= cost)
        {
            fox.DeadBirds -= cost;
            fox.UpgradeRun();
            UpdateDeadBirdsText();
        }

        upgradeCostText.text = "Upgrade Cost: " + fox.NextUpgradeCost();
    }

    private void UpdateDeadBirdsText()
    {
        deadBirdsText.text = ": " + fox.DeadBirds;
    }
}
