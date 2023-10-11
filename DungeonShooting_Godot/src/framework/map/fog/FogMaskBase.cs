
using System;
using System.Collections;
using Godot;

public partial class FogMaskBase : PointLight2D, IDestroy
{
    public bool IsDestroyed { get; private set; }
    
    /// <summary>
    /// 是否探索过迷雾
    /// </summary>
    public bool IsExplored { get; set; }
    
    /// <summary>
    /// 迷雾透明度值, 这个值在调用 TransitionAlpha() 时改变, 用于透明度判断
    /// </summary>
    public float TargetAlpha { get; set; }
    
    private long _cid = -1;
    
    /// <summary>
    /// 使颜色的 alpha 通道过渡到指定的值
    /// </summary>
    /// <param name="targetAlpha">透明度值</param>
    public void TransitionAlpha(float targetAlpha)
    {
        TargetAlpha = targetAlpha;
        if (_cid >= 0)
        {
            World.Current.StopCoroutine(_cid);
        }
        
        _cid = World.Current.StartCoroutine(RunTransitionAlpha(targetAlpha, GameConfig.FogTransitionTime, false));
    }

    /// <summary>
    /// 使颜色的 alpha 通道过渡到指定的值
    /// </summary>
    /// <param name="tartAlpha">初始透明度</param>
    /// <param name="targetAlpha">透明度值</param>
    public void TransitionAlpha(float tartAlpha, float targetAlpha)
    {
        Color = new Color(1, 1, 1, tartAlpha);
        TransitionAlpha(targetAlpha);
    }

    /// <summary>
    /// 使颜色的 alpha 通道过渡到 GameConfig.DarkFogAlpha
    /// </summary>
    public void TransitionToDark()
    {
        TransitionAlpha(GameConfig.DarkFogAlpha);
    }
    
    /// <summary>
    /// 使颜色的 alpha 通道过渡到 1
    /// </summary>
    public void TransitionToLight()
    {
        TransitionAlpha(1);
    }

    /// <summary>
    /// 使颜色的 alpha 通道过渡到 0, 然后隐藏该 Fog
    /// </summary>
    /// <param name="time">过渡时间</param>
    public void TransitionToHide(float time = GameConfig.FogTransitionTime)
    {
        TargetAlpha = 0;
        if (Visible)
        {
            if (_cid >= 0)
            {
                World.Current.StopCoroutine(_cid);
            }
        
            _cid = World.Current.StartCoroutine(RunTransitionAlpha(TargetAlpha, time, true));
        }
    }

    private IEnumerator RunTransitionAlpha(float targetAlpha, float time, bool hide)
    {
        var originColor = Color;
        var a = originColor.A;
        var delta = Mathf.Abs(a - targetAlpha) / time;
        while (Math.Abs(a - targetAlpha) > 0.001f)
        {
            a = Mathf.MoveToward(a, targetAlpha, delta * (float)World.Current.GetProcessDeltaTime());
            Color = new Color(1, 1, 1, a);
            yield return null;
        }
        _cid = -1;
        if (hide)
        {
            this.SetActive(false);
        }
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
    }
}