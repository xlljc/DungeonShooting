using Godot;
using UI.MapEditorTools;

namespace UI.MapEditor;

public partial class MapEditorPanel : MapEditor
{
    /// <summary>
    /// 左上角工具面板
    /// </summary>
    public MapEditorToolsPanel ToolsPanel { get; private set; }
    
    private EditorTileMapBar _editorTileMapBar;

    public override void OnCreateUi()
    {
        _editorTileMapBar = new EditorTileMapBar(this, S_TileMap);
        ToolsPanel = S_CanvasLayer.OpenNestedUi<MapEditorToolsPanel>(UiManager.UiName.MapEditorTools);
        
        //S_HSplitContainer.Instance.AnchorLeft
    }

    public override void OnShowUi()
    {
        S_Left.Instance.Resized += OnMapViewResized;
        S_Back.Instance.Pressed += OnBackClick;
        OnMapViewResized();
        
        _editorTileMapBar.OnShow();
    }

    public override void OnHideUi()
    {
        S_Left.Instance.Resized -= OnMapViewResized;
        S_Back.Instance.Pressed -= OnBackClick;
        _editorTileMapBar.OnHide();
    }

    public override void Process(float delta)
    {
        _editorTileMapBar.Process(delta);
    }

    /// <summary>
    /// 加载地牢, 返回是否加载成功
    /// </summary>
    /// <param name="dir">文件夹路径</param>
    /// <param name="groupName">房间组名</param>
    /// <param name="roomType">房间类型</param>
    /// <param name="roomName">房间名称</param>
    public bool LoadMap(string dir, string groupName, DungeonRoomType roomType, string roomName)
    {
        return S_TileMap.Instance.Load(dir, groupName, roomType, roomName);
    }
    
    //调整地图显示区域大小
    private void OnMapViewResized()
    {
        S_SubViewport.Instance.Size = S_Left.Instance.Size.AsVector2I() - new Vector2I(4, 4);
    }

    private void OnBackClick()
    {
        //返回上一个Ui
        if (PrevUi != null)
        {
            DisposeUi();
            PrevUi.ShowUi();
        }
    }
}
