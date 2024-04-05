using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIRollDiceButton : MonoBehaviour
{
    public static event Action OnDiceClick;
    private Button _btn;

    public void Awake()
    {
        Player.OnTurnFinished += OnPlayerTurnFinished;
    }
    public void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(delegate { OnRollDiceButtonClick(); });
    }

    public void OnPlayerTurnFinished()
    {
        gameObject.SetActive(true);
    }

    public void OnRollDiceButtonClick()
    {
        gameObject.SetActive(false);
        OnDiceClick?.Invoke();
    }

    private void OnDestroy()
    {
        Player.OnTurnFinished -= OnPlayerTurnFinished;
    }
}
