using System.Threading.Tasks;
using System.Collections.Generic;

public class GameStatePlayerMove : GameState
{
    private Player _currentPlayer;
    private Dictionary<int, Tile> _spawnedTiles;
    private int _diceValue;

    public GameStatePlayerMove(GameStateManager stateManager)
    {
        Start(stateManager);
    }

    public override void Start(GameStateManager stateManager)
    {
        m_stateManager = stateManager;

        Bind();
        Execute();
    }

    private async void Execute()
    {
        List<Tile> destinationTiles = DestionationsTiles();
        if (destinationTiles == null) return;

        foreach(Tile t in destinationTiles)
        {
            await PlayerMovement(t);
        }

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

    async Task<bool> PlayerMovement(Tile t)
    {
        _currentPlayer.Move(t);
        await Task.Delay(1000);
        return true;
    }

    private List<Tile> DestionationsTiles()
    {
        List<Tile> destinationTiles = new List<Tile>();

        int currentStep = _currentPlayer.CurrentStep;

        for (int i = 0; i < _diceValue; i++)
        {
            int index = currentStep + i + 1;
            if (!_spawnedTiles.ContainsKey(index))
            {
                GameOver();
                return null;
            }
            destinationTiles.Add(_spawnedTiles[index]);
        }

        return destinationTiles;
    }

    private void GameOver()
    {
        m_stateManager.SetState(GameStateManager.State.GameOver, string.Empty);
    }

    private void Bind()
    {
        _diceValue = m_stateManager.Properties.Dice;
        _currentPlayer = m_stateManager.Properties.CurrentPlayer;
        _spawnedTiles = m_stateManager.Properties.TilesDict;
    }

    private void Finished()
    {
        m_stateManager.SetState(GameStateManager.State.RollDice, string.Empty);
    }

    public override void Update()
    {
    }

    public override void OnDestroy()
    {
    }
}
