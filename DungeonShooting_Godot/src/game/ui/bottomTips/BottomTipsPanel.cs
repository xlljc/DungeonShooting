using System.Collections;
using Godot;

namespace UI.BottomTips;

/// <summary>
/// 底部提示面板
/// </summary>
public partial class BottomTipsPanel : BottomTips
{
    private long _id = -1;
    
    private float _offsetY;
    //动画播放时间
    private float _animationTime = 0.5f;
    //动画移动的像素
    private int _movePixel = 153;

    public override void OnCreateUi()
    {
        _offsetY = L_Panel.Instance.Position.Y - (Position.Y + Size.Y);
    }
    

    /// <summary>
    /// 执行入场流程
    /// </summary>
    public void PlayInStep(Texture2D icon, string message)
    {
        if (_id >= 0)
        {
            StopCoroutine(_id);
            HideUi();
        }
        _id = StartCoroutine(RunAnimation(icon, message));
    }

    /// <summary>
    /// 设置图标
    /// </summary>
    public void SetIcon(Texture2D icon)
    {
        S_TextureRect.Instance.Texture = icon;
    }
    
    /// <summary>
    /// 设置文本内容
    /// </summary>
    public void SetMessage(string message)
    {
        S_Label.Instance.Text = message;
    }

    private IEnumerator RunAnimation(Texture2D icon, string message)
    {
        //还原位置
        var pos = L_Panel.Instance.Position;
        pos.Y = Position.Y + Size.Y + _offsetY;
        L_Panel.Instance.Position = pos;
        
        SetIcon(icon);
        SetMessage(message);
        
        yield return 0;
        ShowUi();
        L_Panel.Instance.ResetSize();
        yield return 0;
        //重新计算中心点
        pos.X = Size.X / 2 - L_Panel.Instance.Size.X / 2;
        L_Panel.Instance.Position = pos;
        yield return 0;

        //向上移动
        var frame = 60 * _animationTime;
        var stepPixel = _movePixel / frame;
        for (var i = 0; i < frame; i++)
        {
            pos.X = L_Panel.Instance.Position.X;
            pos.Y -= stepPixel;
            L_Panel.Instance.Position = pos;
            yield return 0;
        }

        yield return new WaitForSeconds(3.5f);
        
        //向下移动
        for (var i = 0; i < frame; i++)
        {
            pos.X = L_Panel.Instance.Position.X;
            pos.Y += stepPixel;
            L_Panel.Instance.Position = pos;
            yield return 0;
        }

        HideUi();
        _id = -1;
    }
    
    private static BottomTipsPanel _instance;
    public static void Init()
    {
        _instance = UiManager.CreateUi<BottomTipsPanel>(UiManager.UiName.BottomTips);
        _instance.ShowUi();
    }

    /// <summary>
    /// 打开Tips, 并设置图标和内容
    /// </summary>
    /// <param name="icon">显示图标</param>
    /// <param name="message">显示消息</param>
    public static void ShowTips(string icon, string message)
    {
        ShowTips(ResourceManager.Load<Texture2D>(icon), message);
    }

    /// <summary>
    /// 打开Tips, 并设置图标和内容
    /// </summary>
    /// <param name="icon">显示图标</param>
    /// <param name="message">显示消息</param>
    public static void ShowTips(Texture2D icon, string message)
    {
        _instance.PlayInStep(icon, message);
    }
}
