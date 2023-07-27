using UnityEngine;

//INHERITANCE
public class UpgradeButton : ButtonBehavior
{
    UIManager manager;
    private void Start()
    {
        manager = GetComponent<UIManager>();
        OnUIClicked(manager._buttonUpgrade);
    }

    //POLYMORPHISM
    protected override void UIBehavior()
    {
        if (GameManager.manager.clickPoints >= GameManager.manager.upgradeCost)
        {
            GameManager.manager.SetUpgradeCost();

            manager.SetButtonBehaviors(manager.SetUpgradeUI);
        }
    }
}
