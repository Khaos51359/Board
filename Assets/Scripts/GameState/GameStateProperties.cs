using System.Collections.Generic;

public class GameStateProperties
{
    public int TotalTiles;
    public int TotalPlayers;
    public Dictionary<int, Player> PlayersDict;
    public Dictionary<int, Tile> TilesDict;

    public Player CurrentPlayer;

    public GameStateProperties()
    {
        TotalTiles = 0;
        TotalPlayers = 0;
        PlayersDict = new Dictionary<int, Player>();
    }
}
