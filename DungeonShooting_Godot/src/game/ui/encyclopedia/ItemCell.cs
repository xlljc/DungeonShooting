using System.Collections;
using Config;

namespace UI.Encyclopedia;

public class ItemCell : UiCell<Encyclopedia.ObjectButton, ExcelConfig.ActivityBase>
{
    public override void OnInit()
    {
        CellNode.L_Select.Instance.Visible = false;
    }

    public override IEnumerator OnSetDataCoroutine(ExcelConfig.ActivityBase data)
    {
        CellNode.L_PreviewImage.Instance.Texture = ResourceManager.LoadTexture2D(data.Icon);
        yield break;
    }

    public override void OnSelect()
    {
        CellNode.L_Select.Instance.Visible = true;
    }
    
    public override void OnUnSelect()
    {
        CellNode.L_Select.Instance.Visible = false;
    }
}