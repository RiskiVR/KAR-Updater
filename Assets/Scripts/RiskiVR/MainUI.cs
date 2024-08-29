using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI errorText;
    public static MainUI instance;
    void Awake()
    { 
        instance = this;
        headerText.text = "Select a button to get started";
        errorText.text = String.Empty;
        Screen.SetResolution(1037, 961, false);
    }

    public void SwitchScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
}