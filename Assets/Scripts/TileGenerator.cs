using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileGenerator
{
    [SerializeField]
    private Transform _tileOrigin;

    [SerializeField]
    private Transform _tileParent;

    [SerializeField]
    private TileLayout _tileLayout;

    [SerializeField]
    private List<ScriptableTile> _tiles;

    public TileGenerator()
    {
    }

    public void Start()
    {
        DependencyError();
    }

    private bool DependencyError()
    {
        if (_tileOrigin == null)
        {
            Debug.LogError("[TileGenerator] origin tile spawn not assigned");
            return true;
        }

        if (_tileParent == null)
        {
            Debug.LogError("[TileGenerator] Tiles parent transform not assigned");
            return true;
        }

        if (_tiles == null || _tiles.Count < 1)
        {
            Debug.LogError("[TileGenerator] there is no tile to generate, please assign some");
            return true;
        }

        if (_tileLayout.Count < 1)
        {
            Debug.LogError("[TileGenerator] please assign some layout");
            return true;
        }

        return false;
    }

    public Dictionary<int, Tile> SpawnTiles(int totalTiles, out Notifier notifier)
    {
        Dictionary<int, Tile> spawnedTiles = new Dictionary<int, Tile>();
        notifier = new Notifier();
        Vector3 lastTilePosition = _tileOrigin.localPosition;
        for (int i = 0; i < totalTiles; i++)
        {
            if (_tileOrigin == null)
            {
                notifier.Status = false;
                notifier.Message = "[TileGenerator] please assign transform parent for new tiles";
                return spawnedTiles;
            }
            GameObject template = new GameObject();
            GameObject spawnedTile = GameObject.Instantiate(template, _tileParent);
            GameObject.Destroy(template);

            Tile tile = spawnedTile.AddComponent<Tile>();
            tile.ID = i;

            if (_tiles == null)
            {
                notifier.Status = false;
                notifier.Message = "[TileGenerator] please assign tiles variant";
                return spawnedTiles;
            }
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
            spawnedTiles.Add(i, tile);
        }

        notifier.Status = true;
        notifier.Message = string.Empty;

        return spawnedTiles;
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

    private void OnDestroy()
    {
    }
}
