using System.Threading.Tasks;

public abstract class TileAction
{
    public abstract Task<bool> ExecuteAction(GameStateManager stateManager);
}
