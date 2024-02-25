using Godot;
using static TerrainPeering;

namespace UI.MapEditorMapTile;

/// <summary>
/// 地形选项
/// </summary>
public class TerrainCell : UiCell<MapEditorMapTile.TerrainItem, TerrainData>
{
    private Image _image;
    private ImageTexture _texture;
    
    public override void OnInit()
    {
        _texture = new ImageTexture();
        CellNode.L_TerrainPreview.Instance.Texture = _texture;
        CellNode.L_Select.Instance.Visible = false;
        CellNode.L_ErrorIcon.Instance.Visible = false;
    }

    public override void OnSetData(TerrainData data)
    {
        //是否可以使用
        if (!data.TerrainInfo.Ready)
        {
            CellNode.L_ErrorIcon.Instance.Visible = true;
            CellNode.Instance.TooltipText = "该地形Bit配置未完成，请在TileSet编辑器中配置！";
        }
        else
        {
            CellNode.L_ErrorIcon.Instance.Visible = false;
            CellNode.Instance.TooltipText = "";
        }
        
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
        if (data.TerrainInfo.TerrainType == 0) //3x3
        {
            CellNode.L_TerrainName.Instance.Text = data.TerrainInfo.Name + "\n3x3";
            var src = CellNode.UiPanel.TileSetSourceInfo.GetSourceImage();
            
            SetImageSrc(src, Center | Right | Bottom | RightBottom, new Vector2I(0, 0));
            SetImageSrc(src, Center | Left | Right | Bottom | LeftBottom | RightBottom, new Vector2I(GameConfig.TileCellSize, 0));
            SetImageSrc(src, Center | Left | Bottom | LeftBottom, new Vector2I(GameConfig.TileCellSize * 2, 0));
            
            SetImageSrc(src, Center | Right | Top | Bottom | RightTop | RightBottom, new Vector2I(0, GameConfig.TileCellSize));
            SetImageSrc(src, Center | Left | Right | Bottom | Top | LeftTop | LeftBottom | RightTop | RightBottom, new Vector2I(GameConfig.TileCellSize, GameConfig.TileCellSize));
            SetImageSrc(src, Center | Left | Top | Bottom | LeftTop | LeftBottom, new Vector2I(GameConfig.TileCellSize * 2, GameConfig.TileCellSize));
            
            SetImageSrc(src, Center | Right | Top | RightTop, new Vector2I(0, GameConfig.TileCellSize * 2));
            SetImageSrc(src, Center | Left | Top | Right | LeftTop | RightTop, new Vector2I(GameConfig.TileCellSize, GameConfig.TileCellSize * 2));
            SetImageSrc(src, Center | Left | Top | LeftTop, new Vector2I(GameConfig.TileCellSize * 2, GameConfig.TileCellSize * 2));
        }
        else //2x2
        {
            CellNode.L_TerrainName.Instance.Text = data.TerrainInfo.Name + "\n2x2";
            var src = CellNode.UiPanel.TileSetSourceInfo.GetSourceImage();
            
            SetImageSrc(src, Center | RightBottom, new Vector2I(0, 0));
            SetImageSrc(src, Center | LeftBottom | RightBottom, new Vector2I(GameConfig.TileCellSize, 0));
            SetImageSrc(src, Center | LeftBottom, new Vector2I(GameConfig.TileCellSize * 2, 0));
            
            SetImageSrc(src, Center | RightTop | RightBottom, new Vector2I(0, GameConfig.TileCellSize));
            SetImageSrc(src, Center | LeftTop | LeftBottom | RightTop | RightBottom, new Vector2I(GameConfig.TileCellSize, GameConfig.TileCellSize));
            SetImageSrc(src, Center | LeftTop | LeftBottom, new Vector2I(GameConfig.TileCellSize * 2, GameConfig.TileCellSize));
            
            SetImageSrc(src, Center | RightTop, new Vector2I(0, GameConfig.TileCellSize * 2));
            SetImageSrc(src, Center | LeftTop | RightTop, new Vector2I(GameConfig.TileCellSize, GameConfig.TileCellSize * 2));
            SetImageSrc(src, Center | LeftTop, new Vector2I(GameConfig.TileCellSize * 2, GameConfig.TileCellSize * 2));
        }
        
        _texture.SetImage(_image);
    }

    private void SetImageSrc(Image src, uint bit, Vector2I dst)
    {
        if (Data.TerrainInfo.T.TryGetValue(bit, out var temp))
        {
            var pos = Data.TerrainInfo.GetPosition(temp);
            _image.BlitRect(src, new Rect2I(pos * GameConfig.TileCellSize, GameConfig.TileCellSizeVector2I), dst);
        }
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
        CellNode.UiPanel.EditorTileMap.SetCurrTerrain(Data);
    }

    public override void OnUnSelect()
    {
        CellNode.L_Select.Instance.Visible = false;
    }
}