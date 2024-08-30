using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class TabLayout : MonoBehaviour
{
    [SerializeField] Color inactiveTabColor;
    [SerializeField] Color activeTabColor;
    [SerializeField] string[] tabInfo;
    [SerializeField] Image[] tabImages;
    [SerializeField] GameObject[] tabs;
    [SerializeField] InputActionReference l;
    [SerializeField] InputActionReference r;
    public static int currentTab;
    public void UpdateTab(int tab)
    {
        currentTab = tab;
        foreach (Image i in tabImages) i.color = inactiveTabColor;
        tabImages[tab].color = activeTabColor;
        foreach (GameObject g in tabs) g.SetActive(false);
        tabs[tab].SetActive(true);
        MainUI.instance.headerText.text = tabInfo[currentTab];
        MainUI.instance.audioSource.PlayOneShot(MainUI.instance.menu[2]);
    }
    private void Start()
    {
        InputSystem.onActionChange += InputSystem_onActionChange;
        UpdateTab(0);
    }
    private void InputSystem_onActionChange(object obj, InputActionChange change)
    {
        if (l.action.triggered) UpdateTab(currentTab + 1);
        if (r.action.triggered) UpdateTab(currentTab - 1);
        
        if (change == InputActionChange.ActionPerformed)
        {
            if (obj.ToString().Contains("Navigate")) UseController(true);
            if (obj.ToString().Contains("Point")) UseController(false);
        }
    }
    private void OnDestroy() => InputSystem.onActionChange -= InputSystem_onActionChange;
    private void UseController(bool ctrl)
    {
        Cursor.visible = !ctrl;
        if (ctrl)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                EventSystem.current.SetSelectedGameObject(tabs[currentTab].transform.GetChild(0).transform.GetChild(0).gameObject);
        }
        else EventSystem.current.SetSelectedGameObject(null);
        foreach (Button b in tabs[currentTab].GetComponentsInChildren<Button>())
        {
            Navigation n = new Navigation();
            n.mode = ctrl ? Navigation.Mode.Automatic : Navigation.Mode.None;
            b.navigation = n;
        }
    }
}