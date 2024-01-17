using Godot;

namespace UI.TileSetEditorCombination;

public partial class TileSelected : VBoxContainer, IUiNodeScript
{
    /// <summary>
    /// 存放所有组合数据
    /// </summary>
    public UiGrid<TileSetEditorCombination.CellButton, ImportCombinationData> Grid { get; private set; }
    
    private TileSetEditorCombination.RightBg _rightBg;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _rightBg = (TileSetEditorCombination.RightBg)uiNode;

        Grid = new UiGrid<TileSetEditorCombination.CellButton, ImportCombinationData>(_rightBg.L_ScrollContainer.L_CellButton, typeof(TileCell));
        Grid.SetCellOffset(new Vector2I(5, 5));
        Grid.SetAutoColumns(true);
        Grid.SetHorizontalExpand(true);
        
        _rightBg.UiPanel.AddEventListener(EventEnum.OnImportCombination, OnImportCombination);
        _rightBg.UiPanel.AddEventListener(EventEnum.OnRemoveCombination, OnRemoveCombination);
        _rightBg.UiPanel.AddEventListener(EventEnum.OnUpdateCombination, OnUpdateCombination);
    }
    
    /// <summary>
    /// 导入组合图块
    /// </summary>
    private void OnImportCombination(object obj)
    {
        if (obj is ImportCombinationData data)
        {
            _rightBg.UiPanel.EditorPanel.TileSetSourceInfo.Combination.Add(data.CombinationInfo);
            Grid.Add(data);
            EventManager.EmitEvent(EventEnum.OnTileSetDirty);
        }
    }
    
    /// <summary>
    /// 移除组合图块
    /// </summary>
    private void OnRemoveCombination(object obj)
    {
        if (obj is ImportCombinationData data)
        {
            var uiCell = Grid.Find(c => c.Data.CombinationInfo.Id == data.CombinationInfo.Id);
            _rightBg.UiPanel.EditorPanel.TileSetSourceInfo.Combination.Remove(data.CombinationInfo);
            if (uiCell != null)
            {
                Grid.RemoveByIndex(uiCell.Index);
            }
            EventManager.EmitEvent(EventEnum.OnTileSetDirty);
        }
    }

    /// <summary>
    /// 修改组合图块
    /// </summary>
    private void OnUpdateCombination(object obj)
    {
        if (obj is ImportCombinationData data)
        {
            var uiCell = Grid.Find(c => c.Data.CombinationInfo.Id == data.CombinationInfo.Id);
            if (uiCell != null)
            {
                uiCell.SetData(data);
            }
            EventManager.EmitEvent(EventEnum.OnTileSetDirty);
        }
    }

    public void OnDestroy()
    {
        Grid.Destroy();
    }
    

    /// <summary>
    /// 改变TileSet纹理
    /// </summary>
    public void OnChangeTileSetTexture()
    {
        //_grid.RemoveAll();
        //刷新预览图
        Grid.ForEach(cell =>
        {
            cell.Data.UpdatePreviewTexture(_rightBg.UiPanel.EditorPanel.TextureImage);
        });
    }
}