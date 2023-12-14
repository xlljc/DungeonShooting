using Godot;

namespace UI.TileSetEditor;

public partial class TileSetEditorPanel : TileSetEditor
{
    // /// <summary>
    // /// 纹理路径
    // /// </summary>
    // public string TexturePath { get; private set; }

    /// <summary>
    /// 纹理
    /// </summary>
    public Texture2D Texture { get; private set; }
    
    /// <summary>
    /// 纹理的Image对象
    /// </summary>
    public Image TextureImage { get; private set; }

    /// <summary>
    /// 背景颜色
    /// </summary>
    public Color BgColor { get; private set; }
    
    /// <summary>
    /// Cell 横轴数量
    /// </summary>
    public int CellHorizontal { get; private set; }
    
    /// <summary>
    /// Cell 纵轴数量
    /// </summary>
    public int CellVertical { get; private set; }

    /// <summary>
    /// 页签对象
    /// </summary>
    public UiGrid<Tab, TileSetEditorTabData> TabGrid { get; private set; }

    public override void OnCreateUi()
    {
        S_Back.Instance.Visible = PrevUi != null;
        S_Back.Instance.Pressed += OnBackClick;

        TabGrid = new UiGrid<Tab, TileSetEditorTabData>(S_Tab, typeof(TileSetEditorTabCell));
        TabGrid.SetHorizontalExpand(true);
        TabGrid.SetCellOffset(new Vector2I(0, 5));
        TabGrid.Add(new TileSetEditorTabData()
        {
            Text = "纹理",
            UiName = UiManager.UiNames.TileSetEditorImport,
        });
        TabGrid.Add(new TileSetEditorTabData()
        {
            Text = "图块",
            UiName = UiManager.UiNames.TileSetEditorSegment,
        });
        TabGrid.Add(new TileSetEditorTabData()
        {
            Text = "地形",
            UiName = UiManager.UiNames.TileSetEditorTerrain,
        });
    }

    public override void OnDestroyUi()
    {
        TabGrid.Destroy();
        if (TextureImage != null)
        {
            TextureImage.Dispose();
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData(TileSetInfo tileSetInfo)
    {
        S_Title.Instance.Text = "正在编辑：" + tileSetInfo.Name;
        
        SetTexture(ImageTexture.CreateFromImage(Image.LoadFromFile("resource/tileSprite/map1/16x16 dungeon ii wall reconfig v04 spritesheet.png")));
        TabGrid.SelectIndex = 0;
    }

    /// <summary>
    /// 设置纹理
    /// </summary>
    public void SetTexture(Texture2D texture)
    {
        Texture = texture;
        if (TextureImage != null)
        {
            TextureImage.Dispose();
        }
        TextureImage = texture.GetImage();
        CellHorizontal = texture.GetWidth() / GameConfig.TileCellSize;
        CellVertical = texture.GetHeight() / GameConfig.TileCellSize;
    }

    /// <summary>
    /// 设置背景颜色
    /// </summary>
    public void SetBgColor(Color color)
    {
        BgColor = color;
    }
    
    /// <summary>
    /// 将二维位置转换为索引的函数
    /// </summary>
    public int CellPositionToIndex(Vector2I pos)
    {
        return pos.Y * CellHorizontal + pos.X;
    }

    //返回上一级按钮点击
    private void OnBackClick()
    {
        OpenPrevUi();
    }
}
