using System.Collections.Generic;

[System.Serializable]
public class TileLayout
{
    public List<ScriptableTile.Direction> Layout;

    public int Count { get {return Layout.Count;} }
}
