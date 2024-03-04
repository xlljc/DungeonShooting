using Godot;

namespace UI.WeaponRoulette;

/// <summary>
/// 武器轮盘
/// </summary>
public partial class WeaponRoulettePanel : WeaponRoulette
{
    /// <summary>
    /// 武器槽数量
    /// </summary>
    public const int SlotCount = 6;
    
    //是否展开轮盘
    private bool _pressRouletteFlag = false;
    private bool _isMagnifyRoulette = false;

    public override void OnCreateUi()
    {
        S_RouletteBg.Instance.Visible = false;
        S_Bg.Instance.Visible = false;

        for (var i = 0; i < SlotCount; i++)
        {
            var angle = i * (360 / SlotCount);
            var clone = S_WeaponSlot.CloneAndPut();
            clone.Instance.RotationDegrees = angle;
        }
        
        S_WeaponSlot.Instance.Visible = false;
    }

    public override void OnDestroyUi()
    {
        
    }

    public override void Process(float delta)
    {
        if (!InputManager.Roulette)
        {
            _pressRouletteFlag = false;
        }

        //按下地图按键
        if (InputManager.Roulette && !_isMagnifyRoulette) //打开轮盘
        {
            if (UiManager.GetUiInstanceCount(UiManager.UiNames.PauseMenu) == 0)
            {
                ExpandRoulette();
            }
        }
        else if (!InputManager.Roulette && _isMagnifyRoulette) //缩小轮盘
        {
            ShrinkRoulette();
        }
    }
    
    private void ExpandRoulette()
    {
        World.Current.Pause = true;
        _pressRouletteFlag = true;
        _isMagnifyRoulette = true;
        
        S_RouletteBg.Instance.Visible = true;
        S_Bg.Instance.Visible = true;
    }
    
    private void ShrinkRoulette()
    {
        S_RouletteBg.Instance.Visible = false;
        S_Bg.Instance.Visible = false;
        
        _isMagnifyRoulette = false;
        World.Current.Pause = false;
    }
}
