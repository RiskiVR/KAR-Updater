using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("Internal Elements")]
    public static MainUI instance;
    public TextMeshProUGUI headerText;
    public AudioSource audioSource;
    public AudioClip[] menu;
    private Selectable[] allSelectables;
    void Awake()
    { 
        instance = this;
        Screen.SetResolution(1037, 961, false);
    }
    private void Start()
    {
        allSelectables = FindObjectsOfType<Selectable>(true);

        for(int i = 0; i < allSelectables.Length; i++)
        {
            var trigger = allSelectables[i].gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((eventData) => { audioSource.PlayOneShot(menu[3]); });
            trigger.triggers.Add(entry);
        }
    }
    public void SwitchScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
}
public class MessageUI
{
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, String text, String caption, int options);
}