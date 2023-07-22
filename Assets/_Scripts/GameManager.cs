using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region for safe keeping
    //private void Awake()
    //{
    //    SetPointText();
    //    autoIncrementPanel.SetActive(false);
    //}

    //private void Start()
    //{
    //    StartCoroutine(AutoIncrementUpdateCoroutine());
    //}

    //public void OnButtonClick()
    //{
    //    pointsValue += pointsIncrement;
    //    pointsValueText.text = $"Button Points: {pointsValue}";

    //    if (pointsValue >= autoIncrementRequiredValue)
    //    {
    //        autoIncrementPanel.SetActive(true);
    //    }
    //}

    //public void OnUpgradeClick()
    //{
    //    if (pointsValue >= incrementUpgradeCost)
    //    {
    //        pointsValue -= incrementUpgradeCost;
    //        pointsValueText.text = $"Button Points: {pointsValue}";

    //        pointsIncrement += incrementIncrease;
    //        incrementIncrease *= Mathf.RoundToInt(incrementMultiplier);
    //        pointsIncrementText.text = $"Click = {pointsIncrement}";

    //        ++incrementUpgradeLevel;
    //        pointsIncrementLevelText.text = $"Click Me! Level: {incrementUpgradeLevel}";

    //        incrementUpgradeCost += incrementUpgradeLevel * Mathf.RoundToInt(incrementMultiplier * 2.8f);
    //        pointsIncrementCostText.text = $"Upgrade Cost: {incrementUpgradeCost}";
    //    }
    //    else
    //    {
    //        Debug.Log("Not enough money.");
    //    }
    //}

    //public void OnAutoUpgradeClick()
    //{
    //    if (pointsValue >= autoIncrementCost)
    //    {
    //        pointsValue -= autoIncrementCost;
    //        pointsValueText.text = $"Button Points: {pointsValue}";

    //        ++autoIncrementLevel;
    //        autoIncrementLevelText.text = $"Auto-Click Level: {autoIncrementLevel}";

    //        autoIncrementValue += (autoIncrementCost / 2);
    //        autoIncrementValueText.text = $"Auto-Click per Second: {autoIncrementValue}";

    //        autoIncrementCost *= Mathf.RoundToInt(1.5f);
    //        autoIncrementCostText.text = $"Auto-Upgrade Cost: {autoIncrementCost}";
    //    }
    //    else
    //    {
    //        Debug.Log("Not enough money for auto increment.");
    //    }

    //    if(autoIncrementLevel >= autoIncrementSpeedRequiredValue)
    //    {
    //        autoIncrementSpeedPanel.SetActive(true);
    //    }
    //}

    //public void OnUpgradeAutoIncrementSpeed()
    //{
    //    if(autoIncrementLevel < autoIncrementSpeedRequiredValue)
    //    {
    //        return;
    //    }

    //    if (pointsValue >= autoIncrementSpeedCost)
    //    {
    //        if(autoIncrementinSecond >= 0.01f)
    //        {
    //            pointsValue -= autoIncrementSpeedCost;
    //            float _value = 1;
    //            autoIncrementSpeedCost *= Mathf.RoundToInt(_value + autoIncrementinSecond);
    //            autoIncrementSpeedCostText.text = $"Auto Increment Speed Cost: {autoIncrementSpeedCost}";

    //            autoIncrementinSecond -= 0.1f;
    //            if(autoIncrementinSecond <= 0.01f)
    //            {
    //                autoIncrementinSecond = 0.01f;
    //                Debug.Log("You've reached the max speed!");
    //            }

    //            autoIncrementSpeedText.text = $"Auto Increment Speed: {Math.Round(autoIncrementinSecond, 2)} second/s";
    //        }

    //    }
    //    else
    //    {
    //        Debug.Log("Not enough money for auto increment speed");
    //    }
    //}

    //IEnumerator AutoIncrementUpdateCoroutine()
    //{
    //    yield return new WaitForSeconds(autoIncrementinSecond);

    //    if(autoIncrementLevel >= 1)
    //    {
    //        pointsValue += autoIncrementValue;
    //        pointsValueText.text = $"Button Points: {pointsValue}";

    //    }

    //    StartCoroutine(AutoIncrementUpdateCoroutine());
    //}

    //private void SetPointText()
    //{
    //    pointsValueText.text = $"Button Points: {pointsValue}";
    //    pointsIncrementText.text = $"Click = {pointsIncrement}";
    //    pointsIncrementCostText.text = $"Upgrade Cost: {incrementUpgradeCost}";
    //    pointsIncrementLevelText.text = $"Click Me! Level: {incrementUpgradeLevel}";

    //    autoIncrementLevelText.text = $"Auto-Click Level: {autoIncrementLevel}";
    //    autoIncrementValueText.text = $"Auto-Click per Second: {autoIncrementValue}";
    //    autoIncrementCostText.text = $"Auto-Upgrade Cost: {autoIncrementCost}";

    //    autoIncrementSpeedCostText.text = $"Auto Increment Speed Cost: {autoIncrementSpeedCost}";
    //    autoIncrementSpeedText.text = $"Auto Increment Speed: {Math.Round(autoIncrementinSecond, 2)} second/s";
    //}
    #endregion

    public static GameManager manager;

    public double _clickCounter = 0;
    public double _clickPoints = 1;
    public double _clickAutoPoints = 0;
    public double _upgradeLevel = 0;
    public double _upgradeCost = 4;
    public double _clickAutoLevel = 0;
    public double _clickAutoCost = 1;

    public bool isAutoClickEnable = false;

    private double _upgradeBaseCost = 4;
    private double _upgradeCostMultiplier = 1.15f;
    private double _clickAutoMultiplier = 1.53f;
    private double _clickMultiplier = 2;

    private void Awake()
    {
        if (manager != null && manager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetNextUpgrade()
    {
        _clickCounter -= _upgradeCost;
        _clickCounter = Math.Round(_clickCounter, 2);

        _upgradeLevel++;
        _upgradeCost = _upgradeBaseCost * Math.Pow(_upgradeCostMultiplier, _upgradeLevel);
        _upgradeCost = Math.Round(_upgradeCost, 2);

        _clickPoints = _clickMultiplier * _upgradeLevel;
        _clickPoints = Math.Round(_clickPoints, 2);
    }

    public void SetNextAutoClick()
    {
        _clickCounter -= _clickAutoCost;
        _clickCounter = Math.Round(_clickCounter, 2);

        _clickAutoLevel++;
        _clickAutoCost = _upgradeBaseCost * Math.Pow(_upgradeCostMultiplier+0.75f, _clickAutoLevel);
        _clickAutoCost = Math.Round(_clickAutoCost, 2);

        _clickAutoPoints = _clickAutoMultiplier * _clickAutoLevel;
        _clickAutoPoints = Math.Round(_clickAutoPoints, 2);

        isAutoClickEnable = true;
    }

    public string FormattedNumber(double _value)
    {
        double absNumber = Math.Abs(_value);
        string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc", "Ud", "Dd", "Td", "Qad", "Qid", "Sxd", "Spd", "Ocd", "Nod", "Vg", "Uvg", "XXX" };

        int index = 0;
        while (absNumber >= 1000 && index < suffixes.Length - 1)
        {
            absNumber /= 1000;
            index++;
        }


        if(index == 0)
        {
            return $"{absNumber}{suffixes[index]}";
        }
        else
        {
            return $"{absNumber:F2}{suffixes[index]}";
        }   
    }

}