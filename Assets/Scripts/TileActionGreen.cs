using UnityEngine;
using System.Threading.Tasks;

public class TileActionGreen : TileAction
{
    private GameStateManager _stateManager;
    private Player _currentPlayer;

    private UICanvasInsertNiceThing _spawnedCanvas;
    private UICanvasInsertNiceThing _niceThingsCanvasPrefab;

    private bool _isFinished = false;

    public override async Task<bool> ExecuteAction(GameStateManager stateManager)
    {
        _stateManager = stateManager;
        Bind();
        RegisterEvents();
        await Finished();

        return true;
    }

    public async Task<bool> Finished()
    {
        await Execute();
        UnregisterEvents();

        return true;
    }

    private void UnregisterEvents()
    {
        UICanvasInsertNiceThing.OnNiceThingSubmitClicked -= OnNiceThingSubmit;
    }

    private void OnNiceThingSubmit(string niceThing)
    {
        _currentPlayer.WriteNiceThing(niceThing);
        _isFinished = true;
    }

    private void RegisterEvents()
    {
        UICanvasInsertNiceThing.OnNiceThingSubmitClicked += OnNiceThingSubmit;
    }

    private void Bind()
    {
        _niceThingsCanvasPrefab = _stateManager.Canvases.CanvasInsertNiceThing;
        _currentPlayer = _stateManager.Properties.CurrentPlayer;
    }

    private async Task<bool> Execute()
    {
        _spawnedCanvas = GameObject.Instantiate(_niceThingsCanvasPrefab);

        await WaitButton();

        return true;
    }

    private async Task<bool> WaitButton()
    {
        while (_isFinished == false)
        {
            await Task.Yield();
        }
        return true;
    }

}
