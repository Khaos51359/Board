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
        m_stateManager.SetState(GameStateManager.State.SpawnAssets, string.Empty);
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
