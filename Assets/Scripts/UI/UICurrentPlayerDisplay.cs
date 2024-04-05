using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UICurrentPlayerDisplay : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private GameplayManager _gameplayManager;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        GameplayManager.UINextPlayer += OnNextPlayer;
    }

    private void OnNextPlayer(int currentPlayerIndex)
    {
        _text.text = "Player " + (currentPlayerIndex + 1) + "\'s turn";
    }
}
