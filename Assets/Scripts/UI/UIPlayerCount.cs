using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class UIPlayerCount : MonoBehaviour
{
    public static event Action<int> OnUIPlayerCountChanged;

    private TMP_InputField _inputField;

    void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onValueChanged.AddListener(delegate { OnValueChanged();});
    }

    private void OnValueChanged()
    {
        int value = 0;
        if (!int.TryParse(_inputField.text, out value)) return;
        OnUIPlayerCountChanged?.Invoke(value);
    }
}
