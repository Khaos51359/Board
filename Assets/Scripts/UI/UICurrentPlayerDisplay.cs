using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UICurrentPlayerDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        GameStateManager.OnPlayerChanged += OnNextPlayer;
        OnNextPlayer(0);
    }

    private void OnNextPlayer(int currentPlayerIndex)
    {
        _text.text = "Player " + (currentPlayerIndex + 1) + "\'s turn";
    }

    private void OnDestroy()
    {
        GameStateManager.OnPlayerChanged -= OnNextPlayer;
    }
}
