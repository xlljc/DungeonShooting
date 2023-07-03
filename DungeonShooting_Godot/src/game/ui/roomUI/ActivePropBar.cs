using Godot;

namespace UI.RoomUI;

public class ActivePropBar
{
    private RoomUI.UiNode_ActivePropBar _activePropBar;

    public ActivePropBar(RoomUI.UiNode_ActivePropBar activePropBar)
    {
        _activePropBar = activePropBar;
        SetActivePropTexture(null);
    }
    
    public void OnShow()
    {
        
    }

    public void OnHide()
    {
        
    }

    public void SetActivePropTexture(Texture2D texture)
    {
        if (texture != null)
        {
            _activePropBar.Instance.Visible = true;
        }
        else
        {
            _activePropBar.Instance.Visible = false;
        }
    }
}