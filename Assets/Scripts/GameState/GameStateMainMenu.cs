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
        TileGenerator tg = GameObject.FindObjectOfType<TileGenerator>();
        if (tg == null)
        {
            string error = "[GameStateMainMenu] can't find TileGenerator in the scene";
            Debug.LogError(error);
            m_stateManager.SetState(GameStateManager.State.GameError, error);
        }


        if (!m_stateManager.PlayerSpawnManager.SpawnPlayer(
                    m_stateManager.Properties.TotalPlayers, out string message))
        {
            Debug.LogError(message);
            m_stateManager.SetState(GameStateManager.State.GameError, message);
        }

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
