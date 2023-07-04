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

    public void Process(float delta)
    {
        var prop = Player.Current?.PropsPack.ActiveItem;
        if (prop != null)
        {
            SetActivePropTexture(prop.GetCurrentTexture());
        }
        else
        {
            SetActivePropTexture(null);
        }
    }
    
    public void SetActivePropTexture(Texture2D texture)
    {
        if (texture != null)
        {
            _activePropBar.L_ActivePropPanel.L_ActivePropSprite.Instance.Texture = texture;
            _activePropBar.Instance.Visible = true;
        }
        else
        {
            _activePropBar.Instance.Visible = false;
        }
    }
}