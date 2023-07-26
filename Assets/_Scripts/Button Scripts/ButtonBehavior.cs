using UnityEngine;
using UnityEngine.UIElements;

public class ButtonBehavior : MonoBehaviour
{
    //ABSTRACTION
    protected void OnUIClicked(Button _button) => _button.clicked += UIBehavior;
    protected void OnUIClicked(VisualElement _element) => _element.RegisterCallback<ClickEvent>(UIBehavior);

    //ABSTRACTION
    protected virtual void UIBehavior()
    {
        //The Action of the button
    }

    protected virtual void UIBehavior(ClickEvent evt)
    {
        //The Action of the button
    }
}
