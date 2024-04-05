using System;
using UnityEngine;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
    public static event Action GameOver;
    public static event Action<int> UINextPlayer;
    private Dictionary<int, Player> _playerDictionary =
        new Dictionary<int, Player>();

    private Dictionary<int, Tile> _tileDictionary =
        new Dictionary<int, Tile>();

    public int CurrentPlayerIndex { get; private set; }

    public Player CurrentPlayer
    {
        get { return _playerDictionary[CurrentPlayerIndex]; }
    }

    public GameplayManager()
    {
        CurrentPlayerIndex = 0;
    }

    private void Awake()
    {
        Player.OnPlayerSpawned += NewPlayerSpawned;
        TileGenerator.OnTileSpawn += OnTileSpawn;
        UIRollDiceButton.OnDiceClick += OnRollDiceButtonClick;
        Player.OnTurnFinished += OnTurnFinished;
    }

    private void OnTurnFinished()
    {
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % _playerDictionary.Count;
        UINextPlayer?.Invoke(CurrentPlayerIndex);
    }

    private void OnRollDiceButtonClick()
    {
        int dice = UnityEngine.Random.Range(1,6);
        if (!_tileDictionary.ContainsKey(CurrentPlayer.CurrentStep + dice))
        {
            GameOver?.Invoke();
            return;
        }
        CurrentPlayer.Move(_tileDictionary[CurrentPlayer.CurrentStep + dice]);
    }

    private void OnTileSpawn(Tile tile)
    {
        _tileDictionary.Add(tile.ID, tile);
    }
    private void NewPlayerSpawned(Player player)
    {
        _playerDictionary.Add(player.ID, player);
    }

    public List<Player> Players
    {
        get { return new List<Player>(_playerDictionary.Values); }
        private set { Players = value; }
    }

    private void OnDestroy()
    {
        Player.OnPlayerSpawned -= NewPlayerSpawned;
        UIRollDiceButton.OnDiceClick -= OnRollDiceButtonClick;
        TileGenerator.OnTileSpawn -= OnTileSpawn;
        Player.OnTurnFinished -= OnTurnFinished;
    }
}
