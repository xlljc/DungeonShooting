using Godot;

namespace UI.TileSetEditor;

public partial class TileSetEditorPanel : TileSetEditor
{
    /// <summary>
    /// 纹理路径
    /// </summary>
    public string TexturePath;

    /// <summary>
    /// 纹理
    /// </summary>
    public Texture2D Texture;

    /// <summary>
    /// 背景颜色
    /// </summary>
    public Color BgColor;

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
        TabGrid.SelectIndex = 0;
    }

    public override void OnDestroyUi()
    {
        TabGrid.Destroy();
    }

    public void InitData(TileSetInfo tileSetInfo)
    {
        S_Title.Instance.Text = "正在编辑：" + tileSetInfo.Name;
    }

    //返回上一级按钮点击
    private void OnBackClick()
    {
        OpenPrevUi();
    }
}
