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

    private void Start()
    {
        InitializeComponents();
        Clicky();
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


    void Clicky()
    {
        _buttonClicker.clicked += ButtonClicker;
        _buttonUpgrade.clicked += ButtonUpgrade;
        _buttonAutoClick.clicked += ButtonAutoClick;
    }

    void ButtonClicker()
    {
        PlaySFXClick();
        _dividerUpgrade.style.display = DisplayStyle.Flex;

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
            
            if(GameManager.manager._upgradeLevel >= 10)
            {
                _dividerAutoClick.style.display = DisplayStyle.Flex;
                _textAutoClickSecond.style.display = DisplayStyle.Flex;
            }
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

    void PlaySFXClick()
    {
        audioSource.Play();
    }

    void InitializeComponents()
    {
        _uiDocument = GetComponent<UIDocument>();
        _mainDivider = _uiDocument.rootVisualElement;

        _dividerMain = _mainDivider.Q<VisualElement>("dividermain");
        _textClickPoints = _dividerMain.Q<Label>("textclickpoints");
        _buttonClicker = _dividerMain.Q<Button>("buttonclicker");
        _textClickerGenerate = _dividerMain.Q<Label>("textclickgenerate");
        _textAutoClickSecond = _dividerMain.Q<Label>("textautoclicksecond");

        _dividerUpgrade = _mainDivider.Q<VisualElement>("dividerupgrade");
        _textUpgradeLevel = _dividerUpgrade.Q<Label>("textupgradelevel");
        _buttonUpgrade = _dividerUpgrade.Q<Button>("buttonupgrade");
        _textUpgradeCost = _dividerUpgrade.Q<Label>("textupgradecost");

        _dividerAutoClick = _mainDivider.Q<VisualElement>("dividerautoclick");
        _textAutoClickLevel = _dividerAutoClick.Q<Label>("textautoclicklevel");
        _buttonAutoClick = _dividerAutoClick.Q<Button>("buttonautoclick");
        _textAutoClickCost = _dividerAutoClick.Q<Label>("textautoclickcost");


        _textClickPoints.text = $"Click Points: {GameManager.manager._clickCounter}";
        _textClickerGenerate.text = $"Points per Click: {GameManager.manager._clickPoints}";
        _textAutoClickSecond.text = $"Auto Click per Second: {GameManager.manager._clickAutoPoints}";
        _textAutoClickSecond.style.display = DisplayStyle.None;

        _dividerUpgrade.style.display = DisplayStyle.None;
        _textUpgradeLevel.text = $"Upgrade Level: {GameManager.manager._upgradeLevel}";
        _textUpgradeCost.text = $"Upgrade Cost: {GameManager.manager._upgradeCost}";

        _dividerAutoClick.style.display = DisplayStyle.None;
        _textAutoClickLevel.text = $"Auto Click Level: {GameManager.manager._clickAutoLevel}";
        _textAutoClickCost.text = $"Auto Click Cost: {GameManager.manager._clickAutoCost}";
    }
}
