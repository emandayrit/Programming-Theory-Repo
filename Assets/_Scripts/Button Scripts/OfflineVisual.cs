using UnityEngine.UIElements;

//INHERITANCE
public class OfflineVisual : ButtonBehavior
{
    UIManager manager;

    private void Start()
    {
        manager = GetComponent<UIManager>();
        OnUIClicked(manager._dividerOffline);
        SetOfflineSavePrefs();
    }

    //POLYMORPHISM
    protected override void UIBehavior(ClickEvent evt)
    {
        GameManager.manager.clickPoints += GameManager.manager.offlinePoints;

        manager.SetButtonBehaviors(manager.SetPointsUI);
        manager._dividerOffline.style.display = DisplayStyle.None;
    }

    void SetOfflineSavePrefs()
    {
        if (GameManager.manager.hasSavePref == true)
        {
            manager._dividerOffline.style.display = DisplayStyle.Flex;
        }
    }
}
