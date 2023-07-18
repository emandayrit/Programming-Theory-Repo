using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main Button")]
    [SerializeField] int pointsValue = 0;
    [SerializeField] TMP_Text pointsValueText;

    [Header("Incremental Upgrade")]
    public int pointsIncrement = 1;
    public int incrementIncrease = 1;
    public float incrementMultiplier = 1.10f;
    [SerializeField] TMP_Text pointsIncrementText;
    public int incrementUpgradeLevel = 1;
    [SerializeField] TMP_Text pointsIncrementLevelText;
    public int incrementUpgradeCost = 4;
    [SerializeField] TMP_Text pointsIncrementCostText;

    [Header("Auto-Increment")]
    public int autoIncrementRequiredValue = 1000;
    [SerializeField] GameObject autoIncrementPanel;
    public int autoIncrementCost = 1000;
    [SerializeField] TMP_Text autoIncrementCostText;
    public float autoIncrementinSecond = 1f;
    public int autoIncrementValue = 1;
    [SerializeField] TMP_Text autoIncrementValueText;
    public int autoIncrementLevel = 0;
    [SerializeField] TMP_Text autoIncrementLevelText;

    private void Awake()
    {
        SetPointText();
        autoIncrementPanel.SetActive(false);
    }

    private void Start()
    {
        InvokeRepeating("AutoIncrementUpdate", autoIncrementinSecond, autoIncrementinSecond);
    }

    public void OnButtonClick()
    {
        pointsValue += pointsIncrement;
        pointsValueText.text = $"Button Points: {pointsValue}";

        if(pointsValue >= autoIncrementRequiredValue)
        {
            autoIncrementPanel.SetActive(true);
        }
    }

    public void OnUpgradeClick()
    {
        if(pointsValue >= incrementUpgradeCost)
        {
            pointsValue -= incrementUpgradeCost;
            pointsValueText.text = $"Button Points: {pointsValue}";

            pointsIncrement += incrementIncrease;
            incrementIncrease *= Mathf.RoundToInt(incrementMultiplier);
            pointsIncrementText.text = $"Click = {pointsIncrement}";

            ++incrementUpgradeLevel;
            pointsIncrementLevelText.text = $"Click Me! Level: {incrementUpgradeLevel}";

            incrementUpgradeCost += incrementUpgradeLevel * Mathf.RoundToInt(incrementMultiplier * 2.8f);
            pointsIncrementCostText.text = $"Upgrade Cost: {incrementUpgradeCost}";   
        }
        else
        {
            Debug.Log("Not enough money.");
        }
    }

    public void OnAutoUpgradeClick()
    {
        if (pointsValue >= autoIncrementCost)
        {
            pointsValue -= autoIncrementCost;
            pointsValueText.text = $"Button Points: {pointsValue}";

            ++autoIncrementLevel;
            autoIncrementLevelText.text = $"Auto-Click Level: {autoIncrementLevel}";

            autoIncrementCost *= Mathf.RoundToInt(1.5f);
            autoIncrementCostText.text = $"Auto-Upgrade Cost: {autoIncrementCost}";

            autoIncrementValue += (autoIncrementCost/10);
            autoIncrementValueText.text = $"Auto-Click per Second: {autoIncrementValue}";
        }
        else
        {
            Debug.Log("Not enough money for auto increment.");
        }
    }

    private void AutoIncrementUpdate()
    {
        if(autoIncrementLevel >= 1)
        {
            pointsValue += autoIncrementValue;
            pointsValueText.text = $"Button Points: {pointsValue}";
        }
    }

    private void SetPointText()
    {
        pointsValueText.text = $"Button Points: {pointsValue}";
        pointsIncrementText.text = $"Click = {pointsIncrement}";
        pointsIncrementCostText.text = $"Upgrade Cost: {incrementUpgradeCost}";
        pointsIncrementLevelText.text = $"Click Me! Level: {incrementUpgradeLevel}";

        autoIncrementLevelText.text = $"Auto-Click Level: {autoIncrementLevel}";
        autoIncrementValueText.text = $"Auto-Click per Second: {autoIncrementValue}";
        autoIncrementCostText.text = $"Auto-Upgrade Cost: {autoIncrementCost}";
    }


}
