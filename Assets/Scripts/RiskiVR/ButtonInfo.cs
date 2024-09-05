using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [SerializeField] string info;
    private void OnEnable()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject) MainUI.instance.headerText.text = info;
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (TabLayout.usingController) OnUIHandle(true);
    }
    public void OnPointerEnter(PointerEventData eventData) => OnUIHandle(true);
    public void OnPointerExit(PointerEventData eventData) => OnUIHandle(false);
    public void OnUIHandle(bool enter)
    {
        if (enter)
        {
            MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[3]);
            MainUI.instance.headerText.text = info;
        }
    }
}