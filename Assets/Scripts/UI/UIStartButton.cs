using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIStartButton : MonoBehaviour
{
    public static event Action OnClicked;
    private Button _btn;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(delegate {OnStartButtonClicked();});
    }

    private void OnStartButtonClicked()
    {
        OnClicked?.Invoke();
    }
}

