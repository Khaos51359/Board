using UnityEngine;

[CreateAssetMenu(menuName = "Board/Tile")]
public class ScriptableTile : ScriptableObject
{
    public Color TileColor;
    public Sprite TileSprite;
    public float TileWidth;
    public float TileHeight;

    [HideInInspector]
    public Transform TileTransform;

    [HideInInspector]
    public Direction TileDirection;

    public ActionType ActionType_;

    public enum Direction
    {
        x,
        x_,
        y,
        y_,
    }

    public enum ActionType
    {
        Blue,
        Red,
        Green,
        Orange,
        Yellow,
    }

}

