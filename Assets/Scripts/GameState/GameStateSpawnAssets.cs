using System.Collections.Generic;

public class GameStateSpawnAssets : GameState
{
    public GameStateSpawnAssets(GameStateManager stateManager)
    {
        Start(stateManager);
    }

    public override void Start(GameStateManager stateManager)
    {
        m_stateManager = stateManager;
        Execute();
    }

    private void Execute()
    {
        GenerateTiles();
        GeneratePlayers();
        m_stateManager.SetState(GameStateManager.State.RollDice, string.Empty);
    }

    private void GenerateTiles()
    {
        int totalTiles = m_stateManager.Properties.TotalTiles;
        Dictionary<int, Tile> tileDict = m_stateManager.TileGeneratorManager.
            SpawnTiles(totalTiles, out Notifier notifier);

        if (notifier.Status == false)
        {
            m_stateManager.SetState(GameStateManager.State.GameError, notifier.Message);
            return;
        }

        m_stateManager.Properties.TilesDict = tileDict;
    }

    private void GeneratePlayers()
    {
        int totalPlayers = m_stateManager.Properties.TotalPlayers;
        Dictionary<int, Player> playerDict =
            m_stateManager.PlayerSpawnManager.SpawnPlayer(
                    totalPlayers, out bool spawnSuccess, out string message);

        if (spawnSuccess == false)
        {
            m_stateManager.SetState(GameStateManager.State.GameError, message);
        }

        m_stateManager.Properties.PlayersDict = playerDict;
        m_stateManager.Properties.CurrentPlayer = playerDict[0];
    }

    public override void Update()
    {
    }

    public override void OnDestroy()
    {
    }
}
