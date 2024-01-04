using System;

namespace UI.TileSetEditorTerrain;

public class TerrainTabData
{
    public string Text;
    public TileSetEditorTerrain.TopBg TopBg;

    public TerrainTabData(string text, TileSetEditorTerrain.TopBg topBg)
    {
        Text = text;
        TopBg = topBg;
    }
}