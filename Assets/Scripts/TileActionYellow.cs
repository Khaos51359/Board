using System.Collections.Generic;
using System.Threading.Tasks;

public class TileActionYellow : TileAction
{
    private GameStateManager _stateManager;
    private GameStateProperties _stateProperties;
    private Player _currentPlayer;
    private List<Player> _otherPlayers;
    private Tile _currentTile;

    public override async Task<bool> ExecuteAction(GameStateManager stateManager)
    {
        _stateManager = stateManager;

        Bind();

        await Task.Yield();
        await Execute();
        return true;
    }

    private void Bind()
    {
        _stateProperties = _stateManager.Properties;
        _currentPlayer = _stateProperties.CurrentPlayer;
        _currentTile = CurrentTile();
        _otherPlayers = OtherPlayers();
    }

    private Tile CurrentTile()
    {
        return _stateProperties.TilesDict[_currentPlayer.CurrentStep];
    }

    private List<Player> OtherPlayers()
    {
        List<Player> otherPlayers = new List<Player>();

        foreach (Player p in _stateProperties.PlayersDict.Values)
        {
            if (_currentPlayer == p) continue;
            otherPlayers.Add(p);
        }

        return otherPlayers;
    }

    private async Task Execute()
    {
        await Task.Yield();

        foreach (Player p in _otherPlayers)
        {
            string otherNiceThing = p.TakeNiceThing(out bool success);
            if (success == false)
            {
                continue;
            }

            _currentPlayer.WriteNiceThing(otherNiceThing);
        }
    }

}
