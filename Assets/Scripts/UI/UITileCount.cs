using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class UITileCount : MonoBehaviour
{
    public static event Action<int> OnValueChanged;

    private TMP_InputField _inputField;

    void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
        _inputField.onValueChanged.AddListener(delegate { ValueChanged();});
    }

    private void ValueChanged()
    {
        int value = 0;
        if (!int.TryParse(_inputField.text, out value)) return;
        OnValueChanged?.Invoke(value);
    }
}
