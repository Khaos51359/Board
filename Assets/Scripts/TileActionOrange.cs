using System.Threading.Tasks;

public class TileActionOrange : TileAction
{
    public override async Task<bool> ExecuteAction(GameStateManager stateManager)
    {
        await Finished();
        return true;
    }

    public async Task<bool> Finished()
    {
        await Task.Yield();
        return true;
    }
}
