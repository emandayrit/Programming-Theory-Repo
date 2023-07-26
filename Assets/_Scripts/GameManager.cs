using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    #region make this data encapsulation
    public double _clickCounter = 0;
    public double _clickPoints = 1;
    public double _clickAutoPoints = 0;
    public double _upgradeLevel = 0;
    public double _upgradeCost = 4;
    public double _clickAutoLevel = 0;
    public double _clickAutoCost = 1;

    public double _offlineReward = 0;

    public bool isAutoClickEnable = false;
    public bool hasSaveProgress = false;

    private double _upgradeBaseCost = 4;
    private double _upgradeCostMultiplier = 1.15f;
    private double _clickAutoMultiplier = 1.53f;
    private double _clickMultiplier = 2;
    #endregion

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

    #region Do an abstration for this codes
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

        return $"{absNumber:F2}{suffixes[index]}"; 
    }
    #endregion
}