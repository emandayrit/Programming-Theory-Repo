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
        GameManager.manager._clickCounter += GameManager.manager._clickPoints;

        manager.SetButtonBehaviors(manager.SetPointsUI);
    }
}
