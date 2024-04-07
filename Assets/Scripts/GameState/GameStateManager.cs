using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static event Action<State> OnGameSwitchState;
    public static event Action<int>  OnPlayerChanged;

    public GameStateProperties Properties = new GameStateProperties();

    public GameStateCanvases Canvases = new GameStateCanvases();

    public PlayerSpawner PlayerSpawnManager = new PlayerSpawner();

    [HideInInspector]
    public TileGenerator TileGeneratorManager;

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

    private void Start()
    {
        _currentState.Start(this);
        TileGenerator[] tg = GameObject.FindObjectsOfType<TileGenerator>();
        if (tg == null || tg.Length > 1)
        {
            string error = "[GameStateMainMenu] make sure there is one TileGenerator in the scene";
            Debug.LogError(error);
            SetState(GameStateManager.State.GameError, error);
        }
        TileGeneratorManager = tg[0];
        InvokePlayerChanged(0);
    }

    private void Update()
    {
        _currentState.Update();

        if (Input.GetKeyDown(KeyCode.M))
        {
            SetState(State.MainMenu, string.Empty);
        }
    }

    public void InvokePlayerChanged(int playerID)
    {
        OnPlayerChanged?.Invoke(playerID);
    }

    private void OnDestroy()
    {
        _currentState.OnDestroy();
    }
}

