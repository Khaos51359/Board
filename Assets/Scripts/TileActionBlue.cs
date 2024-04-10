using System.Threading.Tasks;
using System.Collections.Generic;

public class TileActionBlue : TileAction
{
    private Player _currentPlayer;
    private Tile _currentTile;
    private Dictionary<int, Player> _otherPlayer;
    private GameStateManager _stateManager;

    private int _diamondCount = 5;

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
        foreach (Player p in _otherPlayer.Values)
        {
            p.AddDiamond(_diamondCount);
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
