using System;
using UnityEngine;

//INHERITANCE & SINGLETON
public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    #region make this data encapsulation
    public double clickPoints = 0;
    public double clickValue = 1;
    public double clickAutoValue = 0;
    public double upgradeLevel = 0;
    public double upgradeCost = 4;
    public double autoLevel = 0;
    public double autoCost = 4;
    public double offlinePoints = 0;

    public bool isAutoEnable = false;
    public bool hasSavePref = false;

    private double _upgradeBaseCost = 4;
    private double _autoBaseCost = 4;
    private double _upgradeCostMultiplier = 1.07f;
    private double _autoCostMultiplier = 1.14f;
    private double _clickValueMultiplier = 1.8f;
    private double _autoValueMultiplier = 1.3f;
    #endregion

    private void Awake() => SetSingleton();

    private void SetSingleton()
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

    public void SetUpgradeCost()
    {
        clickPoints -= upgradeCost;
        upgradeLevel++;
        upgradeCost = _upgradeBaseCost * Math.Pow(_upgradeCostMultiplier, upgradeLevel);
        clickValue = _clickValueMultiplier * upgradeLevel;
        
        upgradeCost = Math.Round(upgradeCost, 2);
        clickPoints = Math.Round(clickPoints, 2);
        clickValue = Math.Round(clickValue, 2);
    }

    public void SetAutoCost()
    {
        clickPoints -= autoCost;
        autoLevel++;
        autoCost = _autoBaseCost * Math.Pow(_autoCostMultiplier, autoLevel);
        clickAutoValue = _autoValueMultiplier * autoLevel;
        
        clickPoints = Math.Round(clickPoints, 2);
        autoCost = Math.Round(autoCost, 2);
        clickAutoValue = Math.Round(clickAutoValue, 2);

        isAutoEnable = true;
    }

    public string SetNotate(double _value)
    {
        double absNumber = Math.Abs(_value);
        string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc", "Ud", "Dd", "Td", "Qad", "Qid", "Sxd", "Spd", "Ocd", "Nod", "Vg", "Uvg", "XXX" };

        int index = 0;
        while (absNumber >= 1000 && index < suffixes.Length - 1)
        {
            absNumber /= 1000;
            index++;
        }

        return $"{absNumber:F2}{suffixes[index]}"; 
    }
}