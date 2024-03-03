using Godot;

namespace UI.WeaponRoulette;

/// <summary>
/// 武器轮盘
/// </summary>
public partial class WeaponRoulettePanel : WeaponRoulette
{
    
    //是否展开轮盘
    private bool _pressRouletteFlag = false;

    public override void OnCreateUi()
    {
        
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
    }
}
