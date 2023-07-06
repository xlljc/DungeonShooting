using Godot;

namespace UI.RoomUI;

public class ActivePropBar
{
    private RoomUI.UiNode_ActivePropBar _activePropBar;
    private ShaderMaterial _shaderMaterial;
    private Vector2 _startPos;
    private Vector2 _startSize;

    public ActivePropBar(RoomUI.UiNode_ActivePropBar activePropBar)
    {
        _activePropBar = activePropBar;
        _shaderMaterial = (ShaderMaterial)_activePropBar.L_ActivePropSprite.Instance.Material;
        _startPos = _activePropBar.L_ActivePropGrey.Instance.Position;
        _startSize = _activePropBar.L_ActivePropGrey.Instance.Scale;

        SetActivePropTexture(null);
        SetChargeProgressVisible(false);
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

            //调整蒙板高度
            var rect = colorRect.RegionRect;
            var size = rect.Size;
            size.Y = progress;
            rect.Size = size;
            colorRect.RegionRect = rect;

            //调整蒙板位置
            var height = _startSize.Y * progress;
            var position = colorRect.Position;
            position.Y = _startPos.Y + (_startSize.Y - height);
            colorRect.Position = position;
        }
    }

    /// <summary>
    /// 设置充能进度条是否显示
    /// </summary>
    public void SetChargeProgressVisible(bool visible)
    {
        var ninePatchRect = _activePropBar.L_ActivePropChargeProgress.Instance;
        ninePatchRect.Visible = visible;
        //调整冷却蒙板大小
        var sprite = _activePropBar.L_ActivePropGrey.Instance;
        if (visible)
        {
            var rect = ninePatchRect.GetRect();
            
            var position = sprite.Position;
            position.X = _startPos.X + rect.Size.X - 1;
            sprite.Position = position;

            var scale = sprite.Scale;
            scale.X = _startSize.X - rect.Size.X + 1;
            sprite.Scale = scale;
        }
        else
        {
            sprite.Position = _startPos;
            sprite.Scale = _startSize;
        }
    }
}