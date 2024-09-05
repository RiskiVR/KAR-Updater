using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [Header("Internal Elements")]
    public static MainUI instance;
    public TextMeshProUGUI headerText;
    public AudioSource audioSource;
    public AudioClip[] menu;
    private void Awake()
    {
        instance = this;
    }
    public void SwitchScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
}
public class MessageUI
{
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, String text, String caption, int options);
}