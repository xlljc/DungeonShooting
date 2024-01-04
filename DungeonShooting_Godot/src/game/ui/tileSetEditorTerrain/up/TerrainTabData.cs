using System;

namespace UI.TileSetEditorTerrain;

public class TerrainTabData
{
    public string Text;
    public TileSetEditorTerrain.LeftBottomBg LeftBottomBg;

    public TerrainTabData(string text, TileSetEditorTerrain.LeftBottomBg leftBottomBg)
    {
        Text = text;
        LeftBottomBg = leftBottomBg;
    }
}