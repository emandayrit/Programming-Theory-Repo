using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    UIDocument _uiDocument;
    VisualElement _mainDivider;

    VisualElement _dividerMain;
    Label _textClickPoints;
    Button _buttonClicker;
    Label _textClickerGenerate;
    Label _textAutoClickSecond;

    VisualElement _dividerUpgrade;
    Label _textUpgradeLevel;
    Button _buttonUpgrade;
    Label _textUpgradeCost;

    VisualElement _dividerAutoClick;
    Label _textAutoClickLevel;
    Button _buttonAutoClick;
    Label _textAutoClickCost;

    VisualElement _dividerOfflineReward;
    Label _textOfflineWelcomeText;
    Label _textOfflineRewardText1;
    Label _textOfflineRewardText2;

    private void Start()
    {
        InitializeAllComponents();
        OnButtonClick();
        StartCoroutine(AutoClickCoroutine());
    }

    IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            if (GameManager.manager.isAutoClickEnable)
            {
                yield return new WaitForSeconds(1f);
                GameManager.manager._clickCounter += GameManager.manager._clickAutoPoints;
                _textClickPoints.text = $"Click Points: {GameManager.manager.FormattedNumber(GameManager.manager._clickCounter)}";
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void OnButtonClick()
    {
        _buttonClicker.clicked += ButtonClicker;
        _buttonUpgrade.clicked += ButtonUpgrade;
        _buttonAutoClick.clicked += ButtonAutoClick;
        _dividerOfflineReward.RegisterCallback<ClickEvent>(CloseOfflineDivider);
    }

    void ButtonClicker()
    {
        PlaySFXClick();
        NextButtonProgressEventChecker();

        GameManager.manager._clickCounter += GameManager.manager._clickPoints;
        _textClickPoints.text = $"Click Points: {GameManager.manager.FormattedNumber(GameManager.manager._clickCounter)}";
    }

    void ButtonUpgrade()
    {
        if (GameManager.manager._clickCounter >= GameManager.manager._upgradeCost)
        {
            PlaySFXClick();
            GameManager.manager.SetNextUpgrade();

            _textClickPoints.text = $"Click Points: {GameManager.manager.FormattedNumber(GameManager.manager._clickCounter)}";
            _textClickerGenerate.text = $"Points per Click: {GameManager.manager.FormattedNumber(GameManager.manager._clickPoints)}";

            _textUpgradeLevel.text = $"Upgrade Level: {GameManager.manager.FormattedNumber(GameManager.manager._upgradeLevel)}";
            _textUpgradeCost.text = $"Upgrade Cost: {GameManager.manager.FormattedNumber(GameManager.manager._upgradeCost)}";

            NextButtonProgressEventChecker();
        }
        else
        {
            Debug.Log("Not Enough Points!");
        }
    }

    void ButtonAutoClick()
    {
        if (GameManager.manager._clickCounter >= GameManager.manager._clickAutoCost)
        {
            PlaySFXClick();
            GameManager.manager.SetNextAutoClick();

            _textClickPoints.text = $"Click Points: {GameManager.manager.FormattedNumber(GameManager.manager._clickCounter)}";

            _textAutoClickLevel.text = $"Auto Click Level: {GameManager.manager.FormattedNumber(GameManager.manager._clickAutoLevel)}";
            _textAutoClickCost.text = $"Auto Click Cost: {GameManager.manager.FormattedNumber(GameManager.manager._clickAutoCost)}";
            _textAutoClickSecond.text = $"Auto Click per Second: {GameManager.manager.FormattedNumber(GameManager.manager._clickAutoPoints)}";
        }
        else
        {
            Debug.Log("Not Enough Points!");
        }
    }



    void CloseOfflineDivider(ClickEvent evt)
    {
        Debug.Log(evt);
        GameManager.manager._clickCounter += GameManager.manager._offlineReward;

        _textClickPoints.text = $"Click Points: {GameManager.manager.FormattedNumber(GameManager.manager._clickCounter)}";
        _dividerOfflineReward.style.display = DisplayStyle.None;

        NextButtonProgressEventChecker();
    }

    void PlaySFXClick()
    {
        audioSource.Play();
    }

    void NextButtonProgressEventChecker()
    {
        //for upgrade click event
        if(GameManager.manager._clickCounter >= GameManager.manager._upgradeCost)
        {
            _dividerUpgrade.style.display = DisplayStyle.Flex;
        }

        //for auto-click event
        if(GameManager.manager._clickCounter >= GameManager.manager._clickAutoCost && GameManager.manager._upgradeLevel >= 10)
        {
            _dividerAutoClick.style.display = DisplayStyle.Flex;
            _textAutoClickSecond.style.display = DisplayStyle.Flex;
        }
    }



    void InitializeAllComponents()
    {
        _uiDocument = GetComponent<UIDocument>();
        _mainDivider = _uiDocument.rootVisualElement;

        InitializeClick();
        InitializeUpgradeClick();
        InitializeAutoClick();
        InitializeOfflineReward();
    }

    void InitializeClick()
    {
        _dividerMain = _mainDivider.Q<VisualElement>("dividermain");
        _textClickPoints = _dividerMain.Q<Label>("textclickpoints");
        _buttonClicker = _dividerMain.Q<Button>("buttonclicker");
        _textClickerGenerate = _dividerMain.Q<Label>("textclickgenerate");
        _textAutoClickSecond = _dividerMain.Q<Label>("textautoclicksecond");

        _textClickPoints.text = $"Click Points: {GameManager.manager.FormattedNumber(GameManager.manager._clickCounter)}";
        _textClickerGenerate.text = $"Points per Click: {GameManager.manager.FormattedNumber(GameManager.manager._clickPoints)}";
        _textAutoClickSecond.text = $"Auto Click per Second: {GameManager.manager.FormattedNumber(GameManager.manager._clickAutoPoints)}";
        _textAutoClickSecond.style.display = DisplayStyle.None;
    }

    void InitializeUpgradeClick()
    {
        _dividerUpgrade = _mainDivider.Q<VisualElement>("dividerupgrade");
        _textUpgradeLevel = _dividerUpgrade.Q<Label>("textupgradelevel");
        _buttonUpgrade = _dividerUpgrade.Q<Button>("buttonupgrade");
        _textUpgradeCost = _dividerUpgrade.Q<Label>("textupgradecost");

        _dividerUpgrade.style.display = DisplayStyle.None;
        _textUpgradeLevel.text = $"Upgrade Level: {GameManager.manager.FormattedNumber(GameManager.manager._upgradeLevel)}";
        _textUpgradeCost.text = $"Upgrade Cost: {GameManager.manager.FormattedNumber(GameManager.manager._upgradeCost)}";
    }

    void InitializeAutoClick()
    {
        _dividerAutoClick = _mainDivider.Q<VisualElement>("dividerautoclick");
        _textAutoClickLevel = _dividerAutoClick.Q<Label>("textautoclicklevel");
        _buttonAutoClick = _dividerAutoClick.Q<Button>("buttonautoclick");
        _textAutoClickCost = _dividerAutoClick.Q<Label>("textautoclickcost");

        _dividerAutoClick.style.display = DisplayStyle.None;
        _textAutoClickLevel.text = $"Auto Click Level: {GameManager.manager.FormattedNumber(GameManager.manager._clickAutoLevel)}";
        _textAutoClickCost.text = $"Auto Click Cost: {GameManager.manager.FormattedNumber(GameManager.manager._clickAutoCost)}";
    }

    void InitializeOfflineReward()
    {
        _dividerOfflineReward = _mainDivider.Q<VisualElement>("dividerofflinereward");
        _textOfflineWelcomeText = _mainDivider.Q<Label>("textofflinewelcome");
        _textOfflineRewardText1 = _mainDivider.Q<Label>("textofflinereward1");
        _textOfflineRewardText2 = _mainDivider.Q<Label>("textofflinereward2");

        if (GameManager.manager.hasSaveProgress)
        {
            _dividerOfflineReward.style.display = DisplayStyle.Flex;
        }
        else
        {
            _dividerOfflineReward.style.display = DisplayStyle.None;
        }

        _textOfflineWelcomeText.text = $"Welcome Back!";
        _textOfflineRewardText1.text = $"Your offline reward:";
        _textOfflineRewardText2.text = $"{GameManager.manager.FormattedNumber(GameManager.manager._offlineReward)}";
    }
}
