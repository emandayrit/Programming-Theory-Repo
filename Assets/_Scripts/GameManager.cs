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
    public int autoIncrementRequiredValue = 2;
    [SerializeField] GameObject autoIncrementPanel;
    public int autoIncrementCost = 2;
    [SerializeField] TMP_Text autoIncrementCostText;
    public int autoIncrementValue = 1;
    [SerializeField] TMP_Text autoIncrementValueText;
    public int autoIncrementLevel = 0;
    [SerializeField] TMP_Text autoIncrementLevelText;

    [Header("Auto-Increment Speed")]
    public int autoIncrementSpeedRequiredValue = 2;
    [SerializeField] GameObject autoIncrementSpeedPanel;
    public int autoIncrementSpeedCost = 5;
    public float autoIncrementinSecond = 1f;
    [SerializeField] TMP_Text autoIncrementSpeedText;
    [SerializeField] TMP_Text autoIncrementSpeedCostText;

    private void Awake()
    {
        SetPointText();
        autoIncrementPanel.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(AutoIncrementUpdateCoroutine());
    }

    public void OnButtonClick()
    {
        pointsValue += pointsIncrement;
        pointsValueText.text = $"Button Points: {pointsValue}";

        if (pointsValue >= autoIncrementRequiredValue)
        {
            autoIncrementPanel.SetActive(true);
        }
    }

    public void OnUpgradeClick()
    {
        if (pointsValue >= incrementUpgradeCost)
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

            autoIncrementValue += (autoIncrementCost / 2);
            autoIncrementValueText.text = $"Auto-Click per Second: {autoIncrementValue}";

            autoIncrementCost *= Mathf.RoundToInt(1.5f);
            autoIncrementCostText.text = $"Auto-Upgrade Cost: {autoIncrementCost}";
        }
        else
        {
            Debug.Log("Not enough money for auto increment.");
        }

        if(autoIncrementLevel >= autoIncrementSpeedRequiredValue)
        {
            autoIncrementSpeedPanel.SetActive(true);
        }
    }

    public void OnUpgradeAutoIncrementSpeed()
    {
        if(autoIncrementLevel < autoIncrementSpeedRequiredValue)
        {
            return;
        }

        if (pointsValue >= autoIncrementSpeedCost)
        {
            if(autoIncrementinSecond >= 0.01f)
            {
                pointsValue -= autoIncrementSpeedCost;
                float _value = 1;
                autoIncrementSpeedCost *= Mathf.RoundToInt(_value + autoIncrementinSecond);
                autoIncrementSpeedCostText.text = $"Auto Increment Speed Cost: {autoIncrementSpeedCost}";

                autoIncrementinSecond -= 0.1f;
                if(autoIncrementinSecond <= 0.01f)
                {
                    autoIncrementinSecond = 0.01f;
                    Debug.Log("You've reached the max speed!");
                }

                autoIncrementSpeedText.text = $"Auto Increment Speed: {Math.Round(autoIncrementinSecond, 2)} second/s";
            }

        }
        else
        {
            Debug.Log("Not enough money for auto increment speed");
        }
    }

    IEnumerator AutoIncrementUpdateCoroutine()
    {
        yield return new WaitForSeconds(autoIncrementinSecond);

        if(autoIncrementLevel >= 1)
        {
            pointsValue += autoIncrementValue;
            pointsValueText.text = $"Button Points: {pointsValue}";

        }

        StartCoroutine(AutoIncrementUpdateCoroutine());
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

        autoIncrementSpeedCostText.text = $"Auto Increment Speed Cost: {autoIncrementSpeedCost}";
        autoIncrementSpeedText.text = $"Auto Increment Speed: {Math.Round(autoIncrementinSecond, 2)} second/s";
    }

}