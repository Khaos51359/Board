using System.Collections.Generic;
using UnityEngine;

public class GameStateMainMenu : GameState
{
    public GameStateMainMenu()
    {
    }

    public GameStateMainMenu(GameStateManager stateManager)
    {
        Start(stateManager);
    }

    public override void Start(GameStateManager stateManager)
    {
        m_stateManager = stateManager;
        RegisterUIEvents();
    }

    private void RegisterUIEvents()
    {
        UITileCount.OnValueChanged += OnTileCountValueChanged;
        UIPlayerCount.OnUIPlayerCountChanged += OnPlayerCountValueChanged;
        UIStartButton.OnClicked += OnStartButtonClicked;
    }

    private void OnStartButtonClicked()
    {
        int totalPlayers = m_stateManager.Properties.TotalPlayers;
        Dictionary<int, Player> playerDict =
            m_stateManager.PlayerSpawnManager.SpawnPlayer(
                    totalPlayers, out bool spawnSuccess, out string message);

        if (spawnSuccess == false)
        {
            Debug.LogError(message);
            m_stateManager.SetState(GameStateManager.State.GameError, message);
        }

        m_stateManager.Properties.PlayersDict = playerDict;
        m_stateManager.Properties.CurrentPlayer = playerDict[0];

        m_stateManager.SetState(GameStateManager.State.RollDice, string.Empty);
    }

    private void OnTileCountValueChanged(int value)
    {
        m_stateManager.Properties.TotalTiles = value;
    }

    private void OnPlayerCountValueChanged(int value)
    {
        m_stateManager.Properties.TotalPlayers = value;
    }

    public override void Update()
    {
    }

    public override void OnDestroy()
    {
        UITileCount.OnValueChanged -= OnTileCountValueChanged;
        UIPlayerCount.OnUIPlayerCountChanged -= OnPlayerCountValueChanged;
        UIStartButton.OnClicked -= OnStartButtonClicked;
    }
}
