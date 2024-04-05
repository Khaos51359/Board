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

    void Start()
    {
        if (DependencyError()) return;
        int totalTiles = 100;
        SpawnTiles(totalTiles);
    }

    bool DependencyError()
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
        for (int i = 0, j = 0; i < totalTiles; i++, j++)
        {
            GameObject template = new GameObject();
            GameObject spawnedTile = Instantiate(template, transform);
            Destroy(template);

            ScriptableTile tile = _tiles[Random.Range(0, _tiles.Count - 1)];
            Vector3 size = new Vector3(tile.TileWidth, tile.TileHeight, 1);
            spawnedTile.transform.localScale = size;

            SpriteRenderer renderer = spawnedTile.AddComponent<SpriteRenderer>();
            renderer.color = tile.TileColor;
            renderer.sprite = tile.TileSprite;

            spawnedTile.name = "Tile" + (i + 1);
            spawnedTile.transform.localPosition = GetNextTilePosition(i, lastTilePosition, tile);
            lastTilePosition = spawnedTile.transform.localPosition;
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

    void Update()
    {

    }
}
