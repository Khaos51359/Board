using System.Threading.Tasks;

public class GameStateTileAction : GameState
{
    private Tile _currentTile;
    private Player _currentPlayer;

    public GameStateTileAction()
    {
    }

    public GameStateTileAction(GameStateManager stateManager)
    {
        Start(stateManager);
    }

    public override async void Start(GameStateManager stateManager)
    {
        m_stateManager = stateManager;
        Bind();
        await Execute();
        Finished();
    }

    private async Task<bool> Execute()
    {
        await _currentTile.TileAction_.ExecuteAction(m_stateManager);

        return true;
    }

    private void Bind()
    {
        _currentPlayer = m_stateManager.Properties.CurrentPlayer;
        int currentTileId = _currentPlayer.CurrentStep;
        _currentTile= m_stateManager.Properties.TilesDict[currentTileId];
    }

    private void Finished()
    {
        m_stateManager.Properties.CurrentPlayer = NextPlayer();
        m_stateManager.SetState(GameStateManager.State.RollDice, string.Empty);
    }

    private Player NextPlayer()
    {
        int maxPlayerCount = m_stateManager.Properties.TotalPlayers;
        int currentPlayerID = _currentPlayer.ID;
        int nextPlayerIndex = (currentPlayerID + 1) % maxPlayerCount;

        return m_stateManager.Properties.PlayersDict[nextPlayerIndex];
    }

    public override void Update()
    {
    }

    public override void OnDestroy()
    {
    }
}
