using Godot;

namespace UI.Debugger;

public partial class DebuggerPanel : Debugger
{
    private bool _showPanel = false;
    private int _len = 0;
    private bool _isDown = false;
    private Vector2 _offset;
    private Vector2 _prevPos;
    private bool _moveFlag;
    
    public override void OnCreateUi()
    {
        S_Bg.Instance.Visible = false;
        
        S_HoverButton.Instance.Pressed += OnClickHoverButton;
        S_HoverButton.Instance.ButtonDown += OnMouseDown;
        S_HoverButton.Instance.ButtonUp += OnMouseUp;
        
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
        else
        {
            if (_isDown)
            {
                var temp = GetGlobalMousePosition() - _offset;
                if (temp != _prevPos)
                {
                    _moveFlag = true;
                    _prevPos = temp;
                    S_HoverButton.Instance.GlobalPosition = temp;
                }
            }
        }

        S_Fps.Instance.Text = "FPS:" + Engine.GetFramesPerSecond();
    }

    private void OnMouseDown()
    {
        _isDown = true;
        _moveFlag = false;
        _prevPos = S_HoverButton.Instance.GlobalPosition;
        _offset = GetGlobalMousePosition() - _prevPos;
    }
    
    private void OnMouseUp()
    {
        _isDown = false;
    }
    
    private void OnClickHoverButton()
    {
        if (_moveFlag)
        {
            return;
        }
        _showPanel = true;
        S_Bg.Instance.Visible = _showPanel;
        S_HoverButton.Instance.Visible = false;
    }

    private void OnClear()
    {
        Debug.Clear();
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
