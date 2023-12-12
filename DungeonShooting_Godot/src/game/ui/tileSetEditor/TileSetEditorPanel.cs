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
    public Texture Texture;

    private UiGrid<Tab, TileSetEditorTabData> _tabGrid;

    public override void OnCreateUi()
    {
        S_Back.Instance.Visible = PrevUi != null;
        S_Back.Instance.Pressed += OnBackClick;

        _tabGrid = new UiGrid<Tab, TileSetEditorTabData>(S_Tab, typeof(object));
    }

    public override void OnDestroyUi()
    {
        _tabGrid.Destroy();
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
