using System;
using UnityEngine;
using UnityEngine.UIElements;

//ABSTRACTION
[RequireComponent(typeof(UIDocument))]
public class UIManager : UIManagerBase
{
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
        if (GameManager.manager.clickPoints >= GameManager.manager.upgradeCost)
        {
            _dividerUpgrade.style.display = DisplayStyle.Flex;
        }

        //for auto-click event
        if (GameManager.manager.clickPoints >= GameManager.manager.autoCost && GameManager.manager.upgradeLevel >= 10)
        {
            _dividerAuto.style.display = DisplayStyle.Flex;
            _textAutoValue.style.display = DisplayStyle.Flex;
        }
    }
    private void PlaySFX()
    {
        _audioSource.Play();
    }
}

//ENCAPSULATION
public class UIManagerBase : MonoBehaviour
{
    protected AudioSource _audioSource;
    protected UIDocument _uiDocument;
    protected VisualElement _mainDivider;

    protected VisualElement _dividerMain;
    protected Label _textPoints;
    protected Label _textValue;
    protected Label _textAutoValue;

    protected VisualElement _dividerUpgrade;
    protected Label _textUpgradeLevel;
    protected Label _textUpgradeCost;

    protected VisualElement _dividerAuto;
    protected Label _textAutoLevel;
    protected Label _textAutoCost;
    protected Label _textOfflineWelcome;
    protected Label _textOfflineMessage;
    protected Label _textOfflineReward;
    public Button _buttonMain { get; private set; }
    public Button _buttonUpgrade { get; private set; }
    public Button _buttonAuto { get; private set; }

    public VisualElement _dividerOffline { get; private set; }


    //ENCAPSULATION
    #region Initialization Components

    protected void SetUIDocuments()
    {
        _uiDocument = GetComponent<UIDocument>();
        _mainDivider = _uiDocument.rootVisualElement;
    }

    protected void SetMainButton()
    {
        _dividerMain = _mainDivider.Q<VisualElement>("dividermain");
        _buttonMain = _dividerMain.Q<Button>("buttonclicker");
        _textPoints = _dividerMain.Q<Label>("textclickpoints");
        _textValue = _dividerMain.Q<Label>("textclickgenerate");
        _textAutoValue = _dividerMain.Q<Label>("textautoclicksecond");

        _textAutoValue.style.display = DisplayStyle.None;
    }

    protected void SetUpgradeButton()
    {
        _dividerUpgrade = _mainDivider.Q<VisualElement>("dividerupgrade");
        _buttonUpgrade = _dividerUpgrade.Q<Button>("buttonupgrade");
        _textUpgradeLevel = _dividerUpgrade.Q<Label>("textupgradelevel");
        _textUpgradeCost = _dividerUpgrade.Q<Label>("textupgradecost");

        _dividerUpgrade.style.display = DisplayStyle.None;
    }

    protected void SetAutoButton()
    {
        _dividerAuto = _mainDivider.Q<VisualElement>("dividerautoclick");
        _buttonAuto = _dividerAuto.Q<Button>("buttonautoclick");
        _textAutoLevel = _dividerAuto.Q<Label>("textautoclicklevel");
        _textAutoCost = _dividerAuto.Q<Label>("textautoclickcost");

        _dividerAuto.style.display = DisplayStyle.None;
    }

    protected void SetOfflineVisual()
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
        _textPoints.text = $"Click Points: {GameManager.manager.SetNotate(GameManager.manager.clickPoints)}";
    }

    public void SetUpgradeUI()
    {
        _textPoints.text = $"Click Points: {GameManager.manager.SetNotate(GameManager.manager.clickPoints)}";
        _textValue.text = $"Points per Click: {GameManager.manager.SetNotate(GameManager.manager.clickValue)}";

        _textUpgradeLevel.text = $"Upgrade Level: {GameManager.manager.SetNotate(GameManager.manager.upgradeLevel)}";
        _textUpgradeCost.text = $"Upgrade Cost: {GameManager.manager.SetNotate(GameManager.manager.upgradeCost)}";
    }

    public void SetAutoUI()
    {
        _textPoints.text = $"Click Points: {GameManager.manager.SetNotate(GameManager.manager.clickPoints)}";

        _textAutoValue.text = $"Auto Click per Second: {GameManager.manager.SetNotate(GameManager.manager.clickAutoValue)}";
        _textAutoLevel.text = $"Auto Click Level: {GameManager.manager.SetNotate(GameManager.manager.autoLevel)}";
        _textAutoCost.text = $"Auto Click Cost: {GameManager.manager.SetNotate(GameManager.manager.autoCost)}";
    }

    public void SetOfflineUI()
    {
        _textOfflineWelcome.text = $"Welcome Back!";
        _textOfflineMessage.text = $"Your offline reward:";
        _textOfflineReward.text = $"{GameManager.manager.SetNotate(GameManager.manager.offlinePoints)}";
    }
    #endregion
}

