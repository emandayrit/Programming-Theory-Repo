using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIManager : MonoBehaviour
{
    AudioSource _audioSource;
    UIDocument _uiDocument;
    VisualElement _mainDivider;

//ENCAPSULATION
#region Main Button Visual
    VisualElement _dividerMain;
    public Button _buttonMain { get; private set; }
    Label _textPoints;
    Label _textValue;
    Label _textAutoValue;
#endregion

#region Upgrade Visual
    VisualElement _dividerUpgrade;
    public Button _buttonUpgrade { get; private set; }
    Label _textUpgradeLevel;
    Label _textUpgradeCost;
#endregion

#region Auto Click Visual
    VisualElement _dividerAuto;
    public Button _buttonAuto { get; private set; }
    Label _textAutoLevel;
    Label _textAutoCost;
#endregion

#region Offline Visual
    public VisualElement _dividerOffline { get; private set; }
    Label _textOfflineWelcome;
    Label _textOfflineMessage;
    Label _textOfflineReward;
#endregion

    private void Awake()
    {
        InitializeComponents();
    }
    
    public void SetButtonBehaviors(Action _event)
    {
        PlaySFX();
        CheckUnlockableButton();
        _event.Invoke();
    }

    private void InitializeComponents()
    {
        SetUIDocuments();
        SetMainButton();
        SetUpgradeButton();
        SetAutoButton();
        SetOfflineVisual();

        SetAllLabels();

        _audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }
    private void SetAllLabels()
    {
        SetPointsUI();
        SetUpgradeUI();
        SetAutoUI();
        SetOfflineUI();
    }
    private void CheckUnlockableButton()
    {
        //for upgrade click event
        if (GameManager.manager._clickCounter >= GameManager.manager._upgradeCost)
        {
            _dividerUpgrade.style.display = DisplayStyle.Flex;
        }

        //for auto-click event
        if (GameManager.manager._clickCounter >= GameManager.manager._clickAutoCost && GameManager.manager._upgradeLevel >= 10)
        {
            _dividerAuto.style.display = DisplayStyle.Flex;
            _textAutoValue.style.display = DisplayStyle.Flex;
        }
    }
    private void PlaySFX()
    {
        _audioSource.Play();
    }


    //ENCAPSULATION
    #region Initialization Components

    void SetUIDocuments()
    {
        _uiDocument = GetComponent<UIDocument>();
        _mainDivider = _uiDocument.rootVisualElement;
    }

    void SetMainButton()
    {
        _dividerMain = _mainDivider.Q<VisualElement>("dividermain");
        _buttonMain = _dividerMain.Q<Button>("buttonclicker");
        _textPoints = _dividerMain.Q<Label>("textclickpoints");
        _textValue = _dividerMain.Q<Label>("textclickgenerate");
        _textAutoValue = _dividerMain.Q<Label>("textautoclicksecond");

        _textAutoValue.style.display = DisplayStyle.None;
    }

    void SetUpgradeButton()
    {
        _dividerUpgrade = _mainDivider.Q<VisualElement>("dividerupgrade");
        _buttonUpgrade = _dividerUpgrade.Q<Button>("buttonupgrade");
        _textUpgradeLevel = _dividerUpgrade.Q<Label>("textupgradelevel");
        _textUpgradeCost = _dividerUpgrade.Q<Label>("textupgradecost");

        _dividerUpgrade.style.display = DisplayStyle.None;
    }

    void SetAutoButton()
    {
        _dividerAuto = _mainDivider.Q<VisualElement>("dividerautoclick");
        _buttonAuto = _dividerAuto.Q<Button>("buttonautoclick");
        _textAutoLevel = _dividerAuto.Q<Label>("textautoclicklevel");
        _textAutoCost = _dividerAuto.Q<Label>("textautoclickcost");

        _dividerAuto.style.display = DisplayStyle.None;
    }

    void SetOfflineVisual()
    {
        _dividerOffline = _mainDivider.Q<VisualElement>("dividerofflinereward");
        _textOfflineWelcome = _mainDivider.Q<Label>("textofflinewelcome");
        _textOfflineMessage = _mainDivider.Q<Label>("textofflinereward1");
        _textOfflineReward = _mainDivider.Q<Label>("textofflinereward2");

    }
    #endregion

    //ENCAPSULATION
    #region For Label Update
    public void SetPointsUI()
    {
        _textPoints.text = $"Click Points: {GameManager.manager.FormattedNumber(GameManager.manager._clickCounter)}";
    }

    public void SetUpgradeUI()
    {
        _textPoints.text = $"Click Points: {GameManager.manager.FormattedNumber(GameManager.manager._clickCounter)}";
        _textValue.text = $"Points per Click: {GameManager.manager.FormattedNumber(GameManager.manager._clickPoints)}";

        _textUpgradeLevel.text = $"Upgrade Level: {GameManager.manager.FormattedNumber(GameManager.manager._upgradeLevel)}";
        _textUpgradeCost.text = $"Upgrade Cost: {GameManager.manager.FormattedNumber(GameManager.manager._upgradeCost)}";
    }

    public void SetAutoUI()
    {
        _textPoints.text = $"Click Points: {GameManager.manager.FormattedNumber(GameManager.manager._clickCounter)}";

        _textAutoValue.text = $"Auto Click per Second: {GameManager.manager.FormattedNumber(GameManager.manager._clickAutoPoints)}";
        _textAutoLevel.text = $"Auto Click Level: {GameManager.manager.FormattedNumber(GameManager.manager._clickAutoLevel)}";
        _textAutoCost.text = $"Auto Click Cost: {GameManager.manager.FormattedNumber(GameManager.manager._clickAutoCost)}";
    }

    public void SetOfflineUI()
    {
        _textOfflineWelcome.text = $"Welcome Back!";
        _textOfflineMessage.text = $"Your offline reward:";
        _textOfflineReward.text = $"{GameManager.manager.FormattedNumber(GameManager.manager._offlineReward)}";
    }
    #endregion
}
