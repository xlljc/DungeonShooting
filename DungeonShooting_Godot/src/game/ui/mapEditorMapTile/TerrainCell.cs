using Godot;
using static TerrainPeering;

namespace UI.MapEditorMapTile;

/// <summary>
/// 地形选项, Data 为 TileSetTerrainInfo 的 index
/// </summary>
public class TerrainCell : UiCell<MapEditorMapTile.TerrainItem, int>
{
    /// <summary>
    /// 选中的地形配置数据
    /// </summary>
    public TileSetTerrainInfo TileSetTerrainInfo;

    private Image _image;
    private ImageTexture _texture;
    
    public override void OnInit()
    {
        _texture = new ImageTexture();
        CellNode.L_TerrainPreview.Instance.Texture = _texture;
        CellNode.L_Select.Instance.Visible = false;
        CellNode.L_ErrorIcon.Instance.Visible = false;
    }

    public override void OnSetData(int data)
    {
        TileSetTerrainInfo = CellNode.UiPanel.TileSetSourceInfo.Terrain[data];
        CellNode.L_TerrainName.Instance.Text = TileSetTerrainInfo.Name;
        
        //是否可以使用
        CellNode.L_ErrorIcon.Instance.Visible = !TileSetTerrainInfo.Ready;
        
        if (_image != null)
        {
            _image.Dispose();
        }

        //创建预览图
        _image = Image.Create(
            3 * GameConfig.TileCellSize,
            3 * GameConfig.TileCellSize,
            false, Image.Format.Rgba8
        );
        if (TileSetTerrainInfo.TerrainType == 0) //3x3
        {

        }
        else //2x2
        {
            var src = CellNode.UiPanel.TileSetSourceInfo.GetSourceImage();
            int[] temp;
            if (TileSetTerrainInfo.T.TryGetValue(Center | RightBottom, out temp))
            {
                var pos = TileSetTerrainInfo.GetPosition(temp);
                _image.BlitRect(src, new Rect2I(pos * GameConfig.TileCellSize, GameConfig.TileCellSizeVector2I), new Vector2I(0, 0));
            }
        }
        
        _texture.SetImage(_image);
    }

    public override void OnDestroy()
    {
        if (_texture != null)
        {
            _texture.Dispose();
            _texture = null;
        }
        
        if (_image != null)
        {
            _image.Dispose();
            _image = null;
        }
    }

    public override void OnSelect()
    {
        CellNode.L_Select.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_Select.Instance.Visible = false;
    }
}