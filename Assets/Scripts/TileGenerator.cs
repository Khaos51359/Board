using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform _tileOrigin;

    [SerializeField]
    private List<ScriptableTile> _tiles;

    void Start()
    {
        if (DependencyError()) return;
        int totalTiles = 10;
        SpawnTiles(totalTiles);
    }

    bool DependencyError()
    {
        if (_tileOrigin == null)
        {
            Debug.LogError("[TileGenerator] origin tile spawn not assigned");
            return false;
        }

        if (_tiles == null || _tiles.Count < 1)
        {
            Debug.LogError("[TileGenerator] there is no tile to generate, please assign some");
            return false;
        }

        return true;
    }

    private void SpawnTiles(int totalTiles)
    {
        for (int i = 0; i < totalTiles; i++)
        {
        }
    }

    void Update()
    {

    }
}
