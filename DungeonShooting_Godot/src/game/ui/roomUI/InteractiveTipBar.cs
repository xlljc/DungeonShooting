using Godot;

namespace UI.RoomUI;

/// <summary>
/// 互动提示文本
/// </summary>
public class InteractiveTipBar
{
    private RoomUI.UiNode_InteractiveTipBar _interactiveTipBar;
    private string _currImage;
    private EventBinder _binder;
    private ActivityObject _interactiveTarget;
    
    public InteractiveTipBar(RoomUI.UiNode_InteractiveTipBar interactiveTipBar)
    {
        interactiveTipBar.Instance.Visible = false;
        _interactiveTipBar = interactiveTipBar;
    }

    public void OnShow()
    {
        GameCamera.Main.OnPositionUpdateEvent += OnCameraPositionUpdate;
        _binder = EventManager.AddEventListener(EventEnum.OnPlayerChangeInteractiveItem, OnPlayerChangeInteractiveItem);
    }

    public void OnHide()
    {
        GameCamera.Main.OnPositionUpdateEvent -= OnCameraPositionUpdate;
        _binder.RemoveEventListener();
        _binder = null;
    }
    
    /// <summary>
    /// 隐藏互动提示ui
    /// </summary>
    public void HideBar()
    {
        _interactiveTipBar.Instance.Visible = false;
    }

    /// <summary>
    /// 显示互动提示ui
    /// </summary>
    /// <param name="target">所在坐标</param>
    /// <param name="showText">显示文本</param>
    /// <param name="icon">显示图标</param>
    public void ShowBar(ActivityObject target, string showText, string icon)
    {
        _interactiveTipBar.Instance.GlobalPosition = target.GlobalPosition;
        if (_currImage != icon)
        {
            _currImage = icon;
            _interactiveTipBar.L_Icon.Instance.Texture = ResourceManager.Load<Texture2D>(icon);
        }
        _interactiveTipBar.Instance.Visible = true;
        _interactiveTipBar.L_NameLabel.Instance.Text = showText;
    }

    public void OnPlayerChangeInteractiveItem(object o)
    {
        if (o == null)
        {
            _interactiveTarget = null;
            //隐藏互动提示
            HideBar();
        }
        else
        {
            var result = (CheckInteractiveResult)o;
            var interactiveItem = Player.Current.InteractiveItem;
            //if (interactiveItem is Weapon)
            if (!string.IsNullOrEmpty(result.ShowIcon))
            {
                _interactiveTarget = interactiveItem;
                //显示互动提示
                ShowBar(result.Target, result.Target.ItemConfig.Name, result.ShowIcon);
            }
            else
            {
                _interactiveTarget = null;
            }
        }
    }
    
    /// <summary>
    /// 相机更新回调
    /// </summary>
    public void OnCameraPositionUpdate(float delta)
    {
        if (_interactiveTarget != null)
        {
            _interactiveTipBar.Instance.GlobalPosition = GameApplication.Instance.ViewToGlobalPosition(_interactiveTarget.GlobalPosition);
        }
    }
}