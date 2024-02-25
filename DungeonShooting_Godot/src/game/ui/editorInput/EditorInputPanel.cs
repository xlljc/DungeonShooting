using Godot;

namespace UI.EditorInput;

public partial class EditorInputPanel : EditorInput
{

    public override void OnCreateUi()
    {
        
    }

    public override void OnDestroyUi()
    {
        
    }
    
    public void Init(string labelText, string value = null)
    {
        S_Label.Instance.Text = labelText;
        if (value != null)
        {
            S_LineEdit.Instance.Text = value;
        }
    }

    public string GetValue()
    {
        return S_LineEdit.Instance.Text;
    }
    
}
