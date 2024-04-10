using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UICanvasInsertNiceThing : MonoBehaviour
{
    public static event Action<string> OnNiceThingSubmitClicked;

    [SerializeField]
    private TMP_InputField _inputTextField;

    [SerializeField]
    private Button _btnSubmit;

    private void Start()
    {
        if (_inputTextField == null)
        {
            Debug.LogError("[UICanvasInsertNiceThing] input text field is null");

        }

        if (_btnSubmit == null)
        {
            Debug.LogError("[UICanvasInsertNiceThing] submit button is null");
        }

        AddButtonListener();
    }

    private void AddButtonListener()
    {
        _btnSubmit.onClick.AddListener(() =>
        {
            OnNiceThingSubmitClicked?.Invoke(_inputTextField.text);
            Destroy(this.gameObject);
        });
    }

}
