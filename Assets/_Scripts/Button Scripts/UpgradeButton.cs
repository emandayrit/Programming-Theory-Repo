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
        if (GameManager.manager._clickCounter >= GameManager.manager._upgradeCost)
        {
            GameManager.manager.SetNextUpgrade();

            manager.SetButtonBehaviors(manager.SetUpgradeUI);
        }
    }
}
