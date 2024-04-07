using UnityEngine;

public class GameStateGameError : GameState
{
    private UICanvasGameError _gameErrorCanvas;

    public GameStateGameError(GameStateManager stateManager, string message)
    {
        Start(stateManager, message);
    }

    public override void Start(GameStateManager stateManager)
    {
        m_stateManager = stateManager;
        _gameErrorCanvas = GameObject.Instantiate(m_stateManager.Canvases.CanvasGameError);
    }

    public void Start(GameStateManager stateManager, string message)
    {
        m_stateManager = stateManager;
        _gameErrorCanvas = GameObject.Instantiate(m_stateManager.Canvases.CanvasGameError);
        _gameErrorCanvas.SetErrorText(message);
    }

    public override void Update()
    {
    }

    public override void OnDestroy()
    {
        GameObject.Destroy(_gameErrorCanvas);
    }
}

