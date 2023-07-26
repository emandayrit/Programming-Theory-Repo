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
        if (GameManager.manager._clickCounter >= GameManager.manager._clickAutoCost)
        {
            GameManager.manager.SetNextAutoClick();

            manager.SetButtonBehaviors(manager.SetAutoUI);
        }
    }

    IEnumerator AutoClickCoroutine()
    {
        while (true)
        {
            if (GameManager.manager.isAutoClickEnable)
            {
                yield return new WaitForSeconds(1f);

                GameManager.manager._clickCounter += GameManager.manager._clickAutoPoints;
                manager.SetPointsUI();
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
