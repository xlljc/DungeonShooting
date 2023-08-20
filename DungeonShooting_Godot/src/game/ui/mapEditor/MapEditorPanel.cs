using Godot;

namespace UI.MapEditor;

public partial class MapEditorPanel : MapEditor
{
    private EditorTileMapBar _editorTileMapBar;
    private string _title;
    
    public override void OnCreateUi()
    {
        S_TabContainer.Instance.SetTabTitle(0, "地图");
        S_TabContainer.Instance.SetTabTitle(1, "对象");
        //S_MapLayer.Instance.Init(S_MapLayer);
        S_Left.Instance.Resized += OnMapViewResized;
        S_Back.Instance.Pressed += OnBackClick;
        S_Save.Instance.Pressed += OnSave;
        
        _editorTileMapBar = new EditorTileMapBar(this, S_TileMap);
    }

    public override void OnShowUi()
    {
        OnMapViewResized();
        _editorTileMapBar.OnShow();
    }

    public override void OnHideUi()
    {
        _editorTileMapBar.OnHide();
    }

    public override void OnDestroyUi()
    {
        _editorTileMapBar.OnDestroy();
    }
    
    public override void Process(float delta)
    {
        _editorTileMapBar.Process(delta);
    }

    /// <summary>
    /// 保存地图数据
    /// </summary>
    public void OnSave()
    {
        S_TileMap.Instance.TriggerSave();
    }

    /// <summary>
    /// 加载地牢, 返回是否加载成功
    /// </summary>
    public bool LoadMap(DungeonRoomSplit roomSplit)
    {
        _title = "正在编辑：" + roomSplit.RoomInfo.RoomName;
        S_Title.Instance.Text = _title;
        return S_TileMap.Instance.Load(roomSplit);
    }
    
    //调整地图显示区域大小
    private void OnMapViewResized()
    {
        S_SubViewport.Instance.Size = S_Left.Instance.Size.AsVector2I() - new Vector2I(4, 4);
    }

    private void OnBackClick()
    {
        EditorWindowManager.ShowConfirm("提示", "是否退出编辑房间？", v =>
        {
            if (v)
            {
                //返回上一个Ui
                OpenPrevUi();
            }
        });
    }
}
