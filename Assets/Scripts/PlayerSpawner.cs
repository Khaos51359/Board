using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSpawner
{
    public Player _playerPrefab;

    public Transform _spawnPoint;

    private bool DependenciesError(out string message)
    {
        if (_spawnPoint == null)
        {
            message = "[PlayerSpawner] spawn point not assigned";
            Debug.LogError(message);
            return true;
        }

        if (_playerPrefab == null)
        {
            message = "[PlayerSpawner] spawn prefab not assigned";
            Debug.LogError(message);
            return true;
        }

        message = string.Empty;
        return false;
    }

    public Dictionary<int, Player> SpawnPlayer(int playerCount, out bool spawnSuccess, out string errorMessage)
    {
        Dictionary<int, Player> playerDict = new Dictionary<int, Player>();
        string message;
        if (DependenciesError(out message))
        {
            errorMessage = message;
            spawnSuccess = false;
            return playerDict;
        }
        for (int i = 0; i < playerCount; i++)
        {
            Player spawnedPlayer = GameObject.Instantiate(_playerPrefab);
            spawnedPlayer.ID = i;
            spawnedPlayer.Move(_spawnPoint);
            playerDict.Add(i, spawnedPlayer);
        }

        errorMessage = string.Empty;
        spawnSuccess = true;
        return playerDict;
    }

    private void OnDestroy()
    {
    }
}
