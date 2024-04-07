using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static event Action<GameStateManager.State> OnGameSwitchState;

    public GameStateProperties Properties = new GameStateProperties();

    public GameStateCanvases Canvases = new GameStateCanvases();

    public PlayerSpawner PlayerSpawnManager = new PlayerSpawner();

    public enum State
    {
        MainMenu,
        RollDice,
        PlayerMove,
        GameOver,
        GameError,
    }

    public void SetState(State newState, string message)
    {
        switch (newState)
        {
            case State.MainMenu:
                _currentState = new GameStateMainMenu(this);
                break;
            case State.RollDice:
                _currentState = new GameStateRollDice(this);
                break;
            case State.PlayerMove:
                break;
            case State.GameOver:
                break;
            case State.GameError:
                _currentState = new GameStateGameError(this, message);
                break;
            default:
                break;
        }

        OnGameSwitchState?.Invoke(newState);
    }

    private GameState _currentState = new GameStateMainMenu();

    public void Start()
    {
        _currentState.Start(this);
    }

    public void Update()
    {
        _currentState.Update();

        if (Input.GetKeyDown(KeyCode.M))
        {
            SetState(State.MainMenu, string.Empty);
        }
    }

    public void OnDestroy()
    {
        _currentState.OnDestroy();
    }
}

