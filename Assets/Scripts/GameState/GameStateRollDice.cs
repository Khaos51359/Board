public class GameStateRollDice : GameState
{
    public GameStateRollDice()
    {
    }

    public GameStateRollDice(GameStateManager stateManager)
    {
        Start(stateManager);
    }

    public override void Start(GameStateManager stateManager)
    {
        m_stateManager = stateManager;
    }

    public override void Update()
    {
    }

    public override void OnDestroy()
    {
    }
}
