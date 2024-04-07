using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform _tileOrigin;

    [SerializeField]
    private TileLayout _tileLayout;

    [SerializeField]
    private List<ScriptableTile> _tiles;

    [SerializeField]
    private int _tileCount;

    private Dictionary<int, Tile> _tileDict = new Dictionary<int, Tile>();

    [HideInInspector]
    public Dictionary<int, Tile> TileDict => _tileDict;

    private void Start()
    {
        if (DependencyError()) return;
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        UITileCount.OnValueChanged += TileCountChanged;
        UIStartButton.OnClicked += OnStartButtonClicked;
    }

    private void OnStartButtonClicked()
    {
        SpawnTiles(_tileCount);
    }

    private void TileCountChanged(int value)
    {
        _tileCount = value;
    }

    private bool DependencyError()
    {
        if (_tileOrigin == null)
        {
            Debug.LogError("[TileGenerator] origin tile spawn not assigned");
            return true;
        }

        if (_tiles == null || _tiles.Count < 1)
        {
            Debug.LogError("[TileGenerator] there is no tile to generate, please assign some");
            return true;
        }

        return false;
    }

    private void SpawnTiles(int totalTiles)
    {
        Vector3 lastTilePosition = _tileOrigin.localPosition;
        for (int i = 0; i < totalTiles; i++)
        {
            GameObject template = new GameObject();
            GameObject spawnedTile = Instantiate(template, transform);
            Destroy(template);

            Tile tile = spawnedTile.AddComponent<Tile>();
            tile.ID = i;

            ScriptableTile tileSO = _tiles[UnityEngine.Random.Range(0, _tiles.Count - 1)];
            Vector3 size = new Vector3(tileSO.TileWidth, tileSO.TileHeight, 1);
            spawnedTile.transform.localScale = size;

            SpriteRenderer renderer = spawnedTile.AddComponent<SpriteRenderer>();
            renderer.color = tileSO.TileColor;
            renderer.sprite = tileSO.TileSprite;

            spawnedTile.name = "Tile" + (i + 1);

            spawnedTile.transform.localPosition =
                GetNextTilePosition(i, lastTilePosition, tileSO);
            lastTilePosition = spawnedTile.transform.localPosition;
            _tileDict.Add(i, tile);
        }
    }

    Vector3 GetNextTilePosition(int index, Vector3 lastTilePosition, ScriptableTile tile)
    {
        Vector3 pos = lastTilePosition;

        if (index == 0) return pos;
        if (index >= _tileLayout.Count) index = _tileLayout.Count - 1;
        switch (_tileLayout.Layout[index])
        {
            case ScriptableTile.Direction.x:
                pos.x += tile.TileWidth;
                break;
            case ScriptableTile.Direction.x_:
                pos.x -= tile.TileWidth;
                break;
            case ScriptableTile.Direction.y:
                pos.y += tile.TileHeight;
                break;
            case ScriptableTile.Direction.y_:
                pos.y -= tile.TileHeight;
                break;
        }
        return pos;
    }

    private void UnregisterEvents()
    {
        UITileCount.OnValueChanged -= TileCountChanged;
        UIStartButton.OnClicked -= OnStartButtonClicked;
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }
}
