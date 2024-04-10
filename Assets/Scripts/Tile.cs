using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    public int ID;

    public Dictionary<int, Player> OccupyingPlayers = new Dictionary<int, Player>();

    public TileAction TileAction_;

    public void AddPlayer(Player newPlayer)
    {
        int playerID = newPlayer.ID;

        if (!OccupyingPlayers.TryAdd(playerID, newPlayer))
        {
            Debug.LogError("[Tile] can't add player with id " + newPlayer.ID);
        }
    }

    public Dictionary<int, Player> GetOtherPlayers(Player player)
    {
        Dictionary<int, Player> otherPlayerDict = new Dictionary<int, Player>();
        foreach(Player otherPlayer in OccupyingPlayers.Values)
        {
            if (otherPlayer != player)
            {
                otherPlayerDict.Add(otherPlayer.ID, otherPlayer);
            }
        }

        return otherPlayerDict;
    }

    public void RemovePlayer(Player newPlayer)
    {
        int playerID = newPlayer.ID;
        if (!OccupyingPlayers.ContainsKey(playerID)) return;
        OccupyingPlayers.Remove(playerID);
    }

    public void ExecuteAction(GameStateManager stateManager)
    {
        TileAction_.ExecuteAction(stateManager);
    }
}

