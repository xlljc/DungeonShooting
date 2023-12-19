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

    public override void OnDisable()
    {
        if (Data.PreviewTexture != null)
        {
            Data.PreviewTexture.Dispose();
            Data.PreviewTexture = null;
        }
    }

    public override void OnDestroy()
    {
        if (Data.PreviewTexture != null)
        {
            Data.PreviewTexture.Dispose();
            Data.PreviewTexture = null;
        }
    }

    public override void OnDoubleClick()
    {
        //双击移除Cell数据
        EditorWindowManager.ShowEditCombination(
            Data.CombinationInfo.Name,
            CellNode.UiPanel.EditorPanel.BgColor,
            Data.PreviewTexture,
            (newName) => //修改
            {
                Data.CombinationInfo.Name = newName;
                EventManager.EmitEvent(EventEnum.OnUpdateCombination, Data);
            },
            () => //删除
            {
                EventManager.EmitEvent(EventEnum.OnRemoveCombination, Data);
            }
        );
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