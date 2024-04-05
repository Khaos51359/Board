using UnityEngine;

[CreateAssetMenu(menuName = "Board/Tile")]
public class ScriptableTile : ScriptableObject
{
    public Color TileColor;
    [HideInInspector]
    public Direction TileDirection;
    public float TileWidth;
    public float TileHeight;
    public Transform TileTransform;

    public enum Direction
    {
        x,
        y,
        z,
    }

}
