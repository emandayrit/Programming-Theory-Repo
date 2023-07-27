using UnityEngine;

//INHERITANCE
public class ClickButton : ButtonBehavior
{
    UIManager manager;
    private void Start()
    {
        manager = GetComponent<UIManager>();
        OnUIClicked(manager._buttonMain);
    }

    //POLYMORPHISM
    protected override void UIBehavior()
    {
        GameManager.manager.clickPoints += GameManager.manager.clickValue;

        manager.SetButtonBehaviors(manager.SetPointsUI);
    }
}
