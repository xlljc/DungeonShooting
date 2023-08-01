using Config;

namespace UI.MapEditorSelectObject;

public class ObjectButtonCell : UiCell<MapEditorSelectObject.ObjectButton, ExcelConfig.ActivityObject>
{
    public override void OnSetData(ExcelConfig.ActivityObject data)
    {
        CellNode.L_ObjectName.Instance.Text = data.Name;
        if (!string.IsNullOrEmpty(data.Icon))
        {
            CellNode.L_PreviewImage.Instance.Texture = ResourceManager.LoadTexture2D(data.Icon);
        }
    }
}