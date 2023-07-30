using Godot;
using UI.MapEditorTools;

namespace UI.MapEditor;

public partial class MapEditorPanel : MapEditor
{
    private EditorTileMapBar _editorTileMapBar;
    private EditorLayerBar _editorLayerBar;

    public override void OnCreateUi()
    {
        S_TabContainer.Instance.SetTabTitle(0, "地图");
        //S_MapLayer.Instance.Init(S_MapLayer);
        
        _editorTileMapBar = new EditorTileMapBar(this, S_TileMap);
        _editorLayerBar = new EditorLayerBar(this, S_MapLayer);
    }

    public override void OnShowUi()
    {
        S_Left.Instance.Resized += OnMapViewResized;
        S_Back.Instance.Pressed += OnBackClick;
        OnMapViewResized();
        
        _editorTileMapBar.OnShow();
        _editorLayerBar.OnShow();
    }

    public override void OnHideUi()
    {
        S_Left.Instance.Resized -= OnMapViewResized;
        S_Back.Instance.Pressed -= OnBackClick;
        _editorTileMapBar.OnHide();
        _editorLayerBar.OnHide();
    }

    public override void OnDestroyUi()
    {
        _editorTileMapBar.OnDestroy();
        _editorLayerBar.OnDestroy();
    }
    
    public override void Process(float delta)
    {
        _editorTileMapBar.Process(delta);
        _editorLayerBar.Process(delta);
    }

    /// <summary>
    /// 加载地牢, 返回是否加载成功
    /// </summary>
    public bool LoadMap(DungeonRoomSplit roomSplit)
    {
        return S_TileMap.Instance.Load(roomSplit);
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
            Destroy();
            PrevUi.ShowUi();
        }
    }
}
