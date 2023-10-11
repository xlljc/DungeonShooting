using Godot;

namespace UI.Debugger;

public partial class DebuggerPanel : Debugger
{
    private bool _showPanel = false;
    private int _len = 0;
    
    public override void OnCreateUi()
    {
        S_Bg.Instance.Visible = false;
        S_HoverButton.Instance.Pressed += OnClickHoverButton;
        S_Clear.Instance.Pressed += OnClear;
        S_Close.Instance.Pressed += OnClose;
    }

    public override void OnDestroyUi()
    {
        
    }

    public override void Process(float delta)
    {
        if (_showPanel)
        {
            if (Debug.AllLogMessage.Length != _len)
            {
                S_Label.Instance.Text = Debug.AllLogMessage;
                _len = Debug.AllLogMessage.Length;
            }
        }
    }

    private void OnClickHoverButton()
    {
        _showPanel = true;
        S_Bg.Instance.Visible = _showPanel;
        S_HoverButton.Instance.Visible = false;
    }

    private void OnClear()
    {
        S_Label.Instance.Text = "";
        _len = 0;
    }

    private void OnClose()
    {
        _showPanel = false;
        S_Bg.Instance.Visible = _showPanel;
        S_HoverButton.Instance.Visible = true;
    }
}
