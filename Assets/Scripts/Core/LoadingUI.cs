using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private Slider progressBar;

    [SerializeField]
    private TextMeshProUGUI text;

    public void SetProgress(float value)
    {
        progressBar.value = value;
    }

    public void SetText(string message)
    {
        text.text = message;
    }
}