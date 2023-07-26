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
        GameManager.manager._clickCounter += GameManager.manager._offlineReward;

        manager.SetButtonBehaviors(manager.SetPointsUI);
        manager._dividerOffline.style.display = DisplayStyle.None;
    }

    void SetOfflineSavePrefs()
    {
        if (GameManager.manager.hasSaveProgress == true)
        {
            manager._dividerOffline.style.display = DisplayStyle.Flex;
        }
    }
}
