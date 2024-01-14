using Godot;

namespace UI.MapEditorMapTile;

/// <summary>
/// 组合笔刷页签
/// </summary>
public partial class CombinationTileTab : Control, IUiNodeScript
{
    private MapEditorMapTile.Tab3 _uiNode;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _uiNode = (MapEditorMapTile.Tab3)uiNode;
    }

    public void OnDestroy()
    {
        
    }
}