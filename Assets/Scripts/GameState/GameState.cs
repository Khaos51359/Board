public abstract class GameState
{
    protected GameStateManager m_stateManager;
    public abstract void Start(GameStateManager stateManager);
    public abstract void Update();
    public abstract void OnDestroy();
}
