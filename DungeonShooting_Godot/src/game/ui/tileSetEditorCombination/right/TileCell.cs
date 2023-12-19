using Godot;

namespace UI.TileSetEditorCombination;

public class TileCell : UiCell<TileSetEditorCombination.CellButton, ImportCombinationData>
{
    public override void OnInit()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }
    
    public override void OnSetData(ImportCombinationData data)
    {
        CellNode.L_CellName.Instance.Text = data.CombinationInfo.Name;
        CellNode.L_PreviewImage.Instance.Texture = data.PreviewTexture;
    }

    public override void OnDoubleClick()
    {
        //双击移除Cell数据
        //EventManager.EmitEvent(EventEnum.OnRemoveTileCell, Data);
    }

    public override void OnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }
}