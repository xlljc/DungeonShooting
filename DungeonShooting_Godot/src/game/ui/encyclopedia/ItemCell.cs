using System.Collections;
using Config;

namespace UI.Encyclopedia;

public class ItemCell : UiCell<Encyclopedia.ObjectButton, ExcelConfig.ActivityBase>
{
    public override void OnInit()
    {
        CellNode.L_Select.Instance.Visible = false;
    }

    public override void OnSetData(ExcelConfig.ActivityBase data)
    {
        CellNode.L_PreviewImage.Instance.Texture = ResourceManager.LoadTexture2D(data.Icon);
    }

    public override IEnumerator OnSetDataCoroutine(ExcelConfig.ActivityBase data)
    {
        CellNode.L_PreviewImage.Instance.Texture = ResourceManager.LoadTexture2D(data.Icon);
        yield break;
    }

    public override void OnDisable()
    {
        CellNode.L_PreviewImage.Instance.Texture = null;
    }

    public override void OnSelect()
    {
        CellNode.L_Select.Instance.Visible = true;
        CellNode.UiPanel.SelectItem(Data);
    }
    
    public override void OnUnSelect()
    {
        CellNode.L_Select.Instance.Visible = false;
    }
}