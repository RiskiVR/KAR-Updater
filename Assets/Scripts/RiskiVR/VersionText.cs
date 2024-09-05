using TMPro;
using UnityEngine;
public class VersionText : MonoBehaviour
{
    TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = $"v{Application.version}";
    }
}