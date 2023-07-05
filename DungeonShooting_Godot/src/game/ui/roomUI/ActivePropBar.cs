using Godot;

namespace UI.RoomUI;

public class ActivePropBar
{
    private RoomUI.UiNode_ActivePropBar _activePropBar;
    private ShaderMaterial _shaderMaterial;
    private Rect2 _startRect;

    public ActivePropBar(RoomUI.UiNode_ActivePropBar activePropBar)
    {
        _activePropBar = activePropBar;
        _shaderMaterial = (ShaderMaterial)_activePropBar.L_ActivePropSprite.Instance.Material;
        _startRect = _activePropBar.L_ActivePropGrey.Instance.GetRect();

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
        var prop = Player.Current?.ActivePropsPack.ActiveItem;
        if (prop != null)
        {
            SetActivePropCount(prop.Count);
            SetActivePropTexture(prop.GetCurrentTexture());

            //是否可以使用该道具
            if (prop.CheckUse())
            {
                _shaderMaterial.SetShaderParameter("schedule", 0);
            }
            else
            {
                _shaderMaterial.SetShaderParameter("schedule", 0.6f);
            }
            
            //冷却
            SetActivePropCooldownProgress(prop.GetCooldownProgress());
        }
        else
        {
            SetActivePropTexture(null);
        }
    }
    
    /// <summary>
    /// 设置道具图标
    /// </summary>
    public void SetActivePropTexture(Texture2D texture)
    {
        if (texture != null)
        {
            _activePropBar.L_ActivePropSprite.Instance.Texture = texture;
            _activePropBar.Instance.Visible = true;
        }
        else
        {
            _activePropBar.Instance.Visible = false;
        }
    }

    /// <summary>
    /// 设置道具数量
    /// </summary>
    public void SetActivePropCount(int count)
    {
        if (count > 1)
        {
            _activePropBar.L_ActivePropCount.Instance.Visible = true;
            _activePropBar.L_ActivePropCount.Instance.Text = count.ToString();
        }
        else
        {
            _activePropBar.L_ActivePropCount.Instance.Visible = false;
        }
    }

    /// <summary>
    /// 设置道具冷却进度
    /// </summary>
    /// <param name="progress">进度: 0 - 1</param>
    public void SetActivePropCooldownProgress(float progress)
    {
        progress = 1 - progress;
        var colorRect = _activePropBar.L_ActivePropGrey.Instance;
        if (progress <= 0)
        {
            colorRect.Visible = false;
        }
        else
        {
            colorRect.Visible = true;

            var height = _startRect.Size.Y * progress;
            var size = colorRect.Size;
            size.Y = height;
            colorRect.Size = size;

            var position = colorRect.Position;
            position.Y = _startRect.Position.Y + (_startRect.Size.Y - height);
            colorRect.Position = position;
        }
    }
}