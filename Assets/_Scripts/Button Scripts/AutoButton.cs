using System.Collections;
using UnityEngine;

//INHERITANCE
public class AutoButton : ButtonBehavior
{
    UIManager manager;
    private void Start()
    {
        manager = GetComponent<UIManager>();

        OnUIClicked(manager._buttonAuto);
        StartCoroutine(AutoClickCoroutine());
    }

    //POLYMORPHISM
    protected override void UIBehavior()
    {
        if (GameManager.manager.clickPoints >= GameManager.manager.autoCost)
        {
            GameManager.manager.SetAutoCost();

            manager.SetButtonBehaviors(manager.SetAutoUI);
        }
    }

    IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            if (GameManager.manager.isAutoEnable)
            {
                yield return new WaitForSeconds(1f);

                GameManager.manager.clickPoints += GameManager.manager.clickAutoValue;
                manager.SetPointsUI();
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
