using Godot;

namespace UI.RoomUI;

public class ActivePropBar
{
    private RoomUI.UiNode_ActivePropBar _activePropBar;
    private ShaderMaterial _shaderMaterial;
    private Vector2 _startCooldownPos;
    private Vector2 _startCooldownSize;
    private Vector2 _startChargePos;
    private Rect2 _startChargeRect;

    private bool _initCooldown = false;

    public ActivePropBar(RoomUI.UiNode_ActivePropBar activePropBar)
    {
        _activePropBar = activePropBar;
        _shaderMaterial = (ShaderMaterial)_activePropBar.L_ActivePropSprite.Instance.Material;
        _startCooldownPos = _activePropBar.L_CooldownProgress.Instance.Position;
        _startCooldownSize = _activePropBar.L_CooldownProgress.Instance.Scale;
        _startChargePos = _activePropBar.L_ChargeProgress.Instance.Position;
        _startChargeRect = _activePropBar.L_ChargeProgress.Instance.RegionRect;

        SetActivePropTexture(null);
        SetChargeProgress(1);
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
            SetCooldownProgress(prop.GetCooldownProgress());
            
            //充能进度
            SetChargeProgress(prop.ChargeProgress);
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
    public void SetCooldownProgress(float progress)
    {
        progress = 1 - progress;
        var colorRect = _activePropBar.L_CooldownProgress.Instance;
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
            var height = _startCooldownSize.Y * progress;
            var position = colorRect.Position;
            position.Y = _startCooldownPos.Y + (_startCooldownSize.Y - height);
            colorRect.Position = position;
        }
    }

    /// <summary>
    /// 设置充能进度条是否显示
    /// </summary>
    public void SetChargeProgressVisible(bool visible)
    {
        var ninePatchRect = _activePropBar.L_ChargeProgressBar.Instance;
        _activePropBar.L_ChargeProgress.Instance.Visible = visible;
        if (ninePatchRect.Visible == visible && _initCooldown)
        {
            return;
        }

        _initCooldown = true;

        var sprite = _activePropBar.L_CooldownProgress.Instance;
        ninePatchRect.Visible = visible;
        //调整冷却蒙板大小
        if (visible)
        {
            var rect = ninePatchRect.GetRect();
            
            var position = sprite.Position;
            position.X = _startCooldownPos.X + rect.Size.X - 1;
            sprite.Position = position;

            var scale = sprite.Scale;
            scale.X = _startCooldownSize.X - rect.Size.X + 1;
            sprite.Scale = scale;
        }
        else
        {
            sprite.Position = _startCooldownPos;
            sprite.Scale = _startCooldownSize;
        }
    }

    /// <summary>
    /// 设置充能进度
    /// </summary>
    /// <param name="progress">进度: 0 - 1</param>
    public void SetChargeProgress(float progress)
    {
        if (progress >= 1)
        {
            SetChargeProgressVisible(false);
        }
        else
        {
            SetChargeProgressVisible(true);

            var height = _startChargeRect.Size.Y * progress;
            var rectY = _startChargeRect.Size.Y * (1 - progress);
            var posY = _startChargePos.Y + rectY;

            var sprite = _activePropBar.L_ChargeProgress.Instance;
            
            var position = sprite.Position;
            position.Y = posY;
            sprite.Position = position;

            var rect = sprite.RegionRect;
            var rectPosition = rect.Position;
            rectPosition.Y = rectY;
            rect.Position = rectPosition;

            var rectSize = rect.Size;
            rectSize.Y = height;
            rect.Size = rectSize;

            sprite.RegionRect = rect;
        }
    }
}