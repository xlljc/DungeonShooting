namespace UI.Settlement;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Settlement : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Settlement.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg((SettlementPanel)this, GetNode<Godot.ColorRect>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Settlement.Title
    /// </summary>
    public Title L_Title
    {
        get
        {
            if (_L_Title == null) _L_Title = new Title((SettlementPanel)this, GetNode<Godot.Label>("Title"));
            return _L_Title;
        }
    }
    private Title _L_Title;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Settlement.ButtonList
    /// </summary>
    public ButtonList L_ButtonList
    {
        get
        {
            if (_L_ButtonList == null) _L_ButtonList = new ButtonList((SettlementPanel)this, GetNode<Godot.VBoxContainer>("ButtonList"));
            return _L_ButtonList;
        }
    }
    private ButtonList _L_ButtonList;


    public Settlement() : base(nameof(Settlement))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Settlement.Bg
    /// </summary>
    public class Bg : UiNode<SettlementPanel, Godot.ColorRect, Bg>
    {
        public Bg(SettlementPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Settlement.Title
    /// </summary>
    public class Title : UiNode<SettlementPanel, Godot.Label, Title>
    {
        public Title(SettlementPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Settlement.ButtonList.Restart
    /// </summary>
    public class Restart : UiNode<SettlementPanel, Godot.Button, Restart>
    {
        public Restart(SettlementPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Restart Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Settlement.ButtonList.ToMenu
    /// </summary>
    public class ToMenu : UiNode<SettlementPanel, Godot.Button, ToMenu>
    {
        public ToMenu(SettlementPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ToMenu Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Settlement.ButtonList
    /// </summary>
    public class ButtonList : UiNode<SettlementPanel, Godot.VBoxContainer, ButtonList>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Settlement.Restart
        /// </summary>
        public Restart L_Restart
        {
            get
            {
                if (_L_Restart == null) _L_Restart = new Restart(UiPanel, Instance.GetNode<Godot.Button>("Restart"));
                return _L_Restart;
            }
        }
        private Restart _L_Restart;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Settlement.ToMenu
        /// </summary>
        public ToMenu L_ToMenu
        {
            get
            {
                if (_L_ToMenu == null) _L_ToMenu = new ToMenu(UiPanel, Instance.GetNode<Godot.Button>("ToMenu"));
                return _L_ToMenu;
            }
        }
        private ToMenu _L_ToMenu;

        public ButtonList(SettlementPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override ButtonList Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Settlement.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Settlement.Title
    /// </summary>
    public Title S_Title => L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Settlement.ButtonList.Restart
    /// </summary>
    public Restart S_Restart => L_ButtonList.L_Restart;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Settlement.ButtonList.ToMenu
    /// </summary>
    public ToMenu S_ToMenu => L_ButtonList.L_ToMenu;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Settlement.ButtonList
    /// </summary>
    public ButtonList S_ButtonList => L_ButtonList;

}
