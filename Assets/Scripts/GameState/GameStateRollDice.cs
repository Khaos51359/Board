public class GameStateRollDice : GameState
{
    private int _min = 1;
    private int _max = 6;

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

        UIRollDiceButton.OnDiceClick += OnDiceClick;
    }

    private void OnDiceClick()
    {
        Execute();
    }

    private void Execute()
    {
        m_stateManager.Properties.Dice = RollDice();

        m_stateManager.SetState(GameStateManager.State.PlayerMove, string.Empty);

        UIRollDiceButton.OnDiceClick -= OnDiceClick;
    }

    private int RollDice()
    {
        return UnityEngine.Random.Range(_min, _max);
    }

    public override void Update()
    {
    }

    public override void OnDestroy()
    {
    }
}
