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

    public bool SpawnPlayer(int playerCount, out string errorMessage)
    {
        string message;
        if (DependenciesError(out message))
        {
            errorMessage = message;
            return false;
        }
        for (int i = 0; i < playerCount; i++)
        {
            Player spawnedPlayer = GameObject.Instantiate(_playerPrefab);
            spawnedPlayer.ID = i;
            spawnedPlayer.Move(_spawnPoint);
        }

        errorMessage = string.Empty;
        return true;
    }

    private void OnDestroy()
    {
    }
}
