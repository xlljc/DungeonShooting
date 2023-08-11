using System.Globalization;
using Godot;

namespace UI.MapEditorCreateMark;

public partial class NumberAttribute : AttributeBase
{
    private MapEditorCreateMark.NumberBar NumberBar;

    public override void SetUiNode(IUiNode uiNode)
    {
        NumberBar = (MapEditorCreateMark.NumberBar)uiNode;
    }

    public override void SetAttributeLabel(string text)
    {
        NumberBar.L_AttrName.Instance.Text = text;
    }

    public override string GetAttributeValue()
    {
        return NumberBar.L_NumInput.Instance.Value.ToString(CultureInfo.InvariantCulture);
    }
}