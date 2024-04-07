using System.Collections.Generic;

public class GameStateProperties
{
    public int TotalTiles;
    public int TotalPlayers;
    public Dictionary<int, Player> PlayersDict;

    public Player CurrentPlayer;

    public GameStateProperties()
    {
        TotalTiles = 0;
        TotalPlayers = 0;
        PlayersDict = new Dictionary<int, Player>();
    }

    public void Add(Player player)
    {
        PlayersDict.TryAdd(player.ID, player);
    }
}
