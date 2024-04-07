using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICanvasGameError : MonoBehaviour
{
    public TextMeshProUGUI _text;

    private void Start()
    {
        Debug.LogError("[UICanvasGameError] please assign error text field");
    }

    public void SetErrorText(string message)
    {
        _text.text = message;
    }
}
