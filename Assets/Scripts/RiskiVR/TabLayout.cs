using UnityEngine;
using UnityEngine.UI;
public class TabLayout : MonoBehaviour
{
    [SerializeField] private Color inactiveTabColor;
    [SerializeField] private Color activeTabColor;
    [SerializeField] private Image[] tabImages;
    [SerializeField] private GameObject[] tabs;
    public void UpdateTab(int activeTab)
    {
        foreach (Image i in tabImages) i.color = inactiveTabColor;
        tabImages[activeTab].color = activeTabColor;
        foreach (GameObject g in tabs) g.SetActive(false);
        tabs[activeTab].SetActive(true);
    }
    void Awake() => UpdateTab(0);
}