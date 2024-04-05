using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private Player _playerPrefab;

    [SerializeField]
    private Transform _spawnPoint;

    public int PlayerCount { get; private set;}

    public PlayerSpawner()
    {
        PlayerCount = 0;
    }

    private void Start()
    {
        if (DependenciesError()) return;
        RegisterEvents();
    }

    private bool DependenciesError()
    {
        if (_spawnPoint == null)
        {
            Debug.LogError("[PlayerSpawner] spawn point not assigned");
            return true;
        }

        if (_playerPrefab == null)
        {
            Debug.LogError("[PlayerSpawner] spawn prefab not assigned");
            return true;
        }

        return false;
    }

    private void RegisterEvents()
    {
        UIPlayerCount.OnUIPlayerCountChanged += OnPlayerCountChanged;
        UIStartButton.OnClicked += OnStartGame;
    }
    private void OnPlayerCountChanged(int value)
    {
        PlayerCount = value;
    }

    private void OnStartGame()
    {
        for (int i = 0; i < PlayerCount; i++)
        {
            Player spawnedPlayer = Instantiate(_playerPrefab);
            spawnedPlayer.ID = i;
            spawnedPlayer.Move(_spawnPoint);
        }
    }

    private void UnregisterEvents()
    {
        UIPlayerCount.OnUIPlayerCountChanged -= OnPlayerCountChanged;
        UIStartButton.OnClicked -= OnStartGame;
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }
}
