using UnityEngine;

public class UIMainCanvas : MonoBehaviour
{
    public void Start()
    {
        UIStartButton.OnClicked += OnStartButtonClicked;
        GameStateManager.OnGameSwitchState += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStateManager.State newState)
    {
        if (newState == GameStateManager.State.MainMenu)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnStartButtonClicked()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UIStartButton.OnClicked -= OnStartButtonClicked;
        GameStateManager.OnGameSwitchState -= OnGameStateChanged;
    }
}
