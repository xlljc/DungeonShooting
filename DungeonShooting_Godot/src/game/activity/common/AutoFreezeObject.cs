
using Godot;

/// <summary>
/// 停止移动后自动冻结对象
/// </summary>
[Tool]
public partial class AutoFreezeObject : ActivityObject
{
    /// <summary>
    /// 自动播放的动画, 物体会等待该动画播完完成后进入冻结状态, 该动画不能是循环动画
    /// </summary>
    [Export]
    public string AnimationName { get; set; }
    
    /// <summary>
    /// 在冻结前是否变灰
    /// </summary>
    [Export]
    public bool AutoToGrey { get; set; }

    /// <summary>
    /// 冻结次数
    /// </summary>
    public int FreezeCount { get; private set; }
    
    private bool _playFlag = false;
    private float _grey = 0;

    /// <summary>
    /// 冻结时调用
    /// </summary>
    protected virtual void OnFreeze()
    {
    }
    
    public override void OnInit()
    {
        if (!string.IsNullOrEmpty(AnimationName))
        {
            _playFlag = true;
            AnimatedSprite.AnimationFinished += OnAnimationFinished;
            AnimatedSprite.Play(AnimationName);
        }
    }

    protected override void Process(float delta)
    {
        //落地静止后将弹壳变为静态贴图
        if (!_playFlag &&!IsThrowing && Altitude <= 0 && MoveController.IsMotionless())
        {
            if (AutoToGrey && _grey < 1)
            {
                //变灰动画时间, 0.5秒
                _grey = Mathf.Min(1, _grey + delta / 0.5f);
                Grey = _grey;
                return;
            }
            if (AffiliationArea != null)
            {
                OnFreeze();
                Freeze();
                FreezeCount++;
            }
            else
            {
                Debug.Log(Name + "投抛到画布外了, 强制消除...");
                Destroy();
            }
        }
    }

    private void OnAnimationFinished()
    {
        _playFlag = false;
    }
}