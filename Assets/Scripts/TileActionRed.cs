using System.Threading.Tasks;
using System.Collections.Generic;

public class TileActionRed : TileAction
{
    private int _diamondValue = 5;
    private Player _currentPlayer;
    private Tile _currentTile;
    private Dictionary<int, Player> _otherPlayer;
    private GameStateManager _stateManager;

    public override async Task<bool> ExecuteAction(GameStateManager stateManager)
    {
        _stateManager = stateManager;
        Bind();
        await Finished();
        return true;
    }

    public async Task<bool> Finished()
    {
        await Task.Yield();
        foreach(Player p in _otherPlayer.Values)
        {
            _currentPlayer.AddDiamond(p.TakeDiamond(_diamondValue));
        }

        return true;
    }

    private void Bind()
    {
        _currentPlayer = _stateManager.Properties.CurrentPlayer;
        _currentTile = _stateManager.Properties.TilesDict[_currentPlayer.CurrentStep];
        _otherPlayer = _currentTile.GetOtherPlayers(_currentPlayer);
    }
}
