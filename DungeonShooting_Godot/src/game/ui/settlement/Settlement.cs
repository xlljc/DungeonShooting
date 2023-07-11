namespace UI.Settlement;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Settlement : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Settlement.Bg
    /// </summary>
    public Settlement_Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Settlement_Bg(GetNodeOrNull<Godot.ColorRect>("Bg"));
            return _L_Bg;
        }
    }
    private Settlement_Bg _L_Bg;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Settlement.Title
    /// </summary>
    public Settlement_Title L_Title
    {
        get
        {
            if (_L_Title == null) _L_Title = new Settlement_Title(GetNodeOrNull<Godot.Label>("Title"));
            return _L_Title;
        }
    }
    private Settlement_Title _L_Title;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Settlement.ButtonList
    /// </summary>
    public Settlement_ButtonList L_ButtonList
    {
        get
        {
            if (_L_ButtonList == null) _L_ButtonList = new Settlement_ButtonList(GetNodeOrNull<Godot.VBoxContainer>("ButtonList"));
            return _L_ButtonList;
        }
    }
    private Settlement_ButtonList _L_ButtonList;


    public Settlement() : base(nameof(Settlement))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Settlement.Bg
    /// </summary>
    public class Settlement_Bg : IUiNode<Godot.ColorRect, Settlement_Bg>
    {
        public Settlement_Bg(Godot.ColorRect node) : base(node) {  }
        public override Settlement_Bg Clone() => new ((Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Settlement.Title
    /// </summary>
    public class Settlement_Title : IUiNode<Godot.Label, Settlement_Title>
    {
        public Settlement_Title(Godot.Label node) : base(node) {  }
        public override Settlement_Title Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Settlement.ButtonList.Restart
    /// </summary>
    public class Settlement_Restart : IUiNode<Godot.Button, Settlement_Restart>
    {
        public Settlement_Restart(Godot.Button node) : base(node) {  }
        public override Settlement_Restart Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Settlement.ButtonList.ToMenu
    /// </summary>
    public class Settlement_ToMenu : IUiNode<Godot.Button, Settlement_ToMenu>
    {
        public Settlement_ToMenu(Godot.Button node) : base(node) {  }
        public override Settlement_ToMenu Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Settlement.ButtonList
    /// </summary>
    public class Settlement_ButtonList : IUiNode<Godot.VBoxContainer, Settlement_ButtonList>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Settlement.Restart
        /// </summary>
        public Settlement_Restart L_Restart
        {
            get
            {
                if (_L_Restart == null) _L_Restart = new Settlement_Restart(Instance.GetNodeOrNull<Godot.Button>("Restart"));
                return _L_Restart;
            }
        }
        private Settlement_Restart _L_Restart;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Settlement.ToMenu
        /// </summary>
        public Settlement_ToMenu L_ToMenu
        {
            get
            {
                if (_L_ToMenu == null) _L_ToMenu = new Settlement_ToMenu(Instance.GetNodeOrNull<Godot.Button>("ToMenu"));
                return _L_ToMenu;
            }
        }
        private Settlement_ToMenu _L_ToMenu;

        public Settlement_ButtonList(Godot.VBoxContainer node) : base(node) {  }
        public override Settlement_ButtonList Clone() => new ((Godot.VBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.Settlement.SettlementPanel"/>, 节点路径: Settlement.Bg
    /// </summary>
    public Settlement_Bg S_Bg => L_Bg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.Settlement.SettlementPanel"/>, 节点路径: Settlement.Title
    /// </summary>
    public Settlement_Title S_Title => L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Settlement.ButtonList.Restart
    /// </summary>
    public Settlement_Restart S_Restart => L_ButtonList.L_Restart;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Settlement.ButtonList.ToMenu
    /// </summary>
    public Settlement_ToMenu S_ToMenu => L_ButtonList.L_ToMenu;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.Settlement.SettlementPanel"/>, 节点路径: Settlement.ButtonList
    /// </summary>
    public Settlement_ButtonList S_ButtonList => L_ButtonList;

}
