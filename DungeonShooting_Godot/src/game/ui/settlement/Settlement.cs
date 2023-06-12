namespace UI.Settlement;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Settlement : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Settlement.Bg
    /// </summary>
    public UiNode_Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new UiNode_Bg(GetNodeOrNull<Godot.ColorRect>("Bg"));
            return _L_Bg;
        }
    }
    private UiNode_Bg _L_Bg;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Settlement.Title
    /// </summary>
    public UiNode_Title L_Title
    {
        get
        {
            if (_L_Title == null) _L_Title = new UiNode_Title(GetNodeOrNull<Godot.Label>("Title"));
            return _L_Title;
        }
    }
    private UiNode_Title _L_Title;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Settlement.ButtonList
    /// </summary>
    public UiNode_ButtonList L_ButtonList
    {
        get
        {
            if (_L_ButtonList == null) _L_ButtonList = new UiNode_ButtonList(GetNodeOrNull<Godot.VBoxContainer>("ButtonList"));
            return _L_ButtonList;
        }
    }
    private UiNode_ButtonList _L_ButtonList;


    public Settlement() : base(nameof(Settlement))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Settlement.Bg
    /// </summary>
    public class UiNode_Bg : IUiNode<Godot.ColorRect, UiNode_Bg>
    {
        public UiNode_Bg(Godot.ColorRect node) : base(node) {  }
        public override UiNode_Bg Clone() => new ((Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Settlement.Title
    /// </summary>
    public class UiNode_Title : IUiNode<Godot.Label, UiNode_Title>
    {
        public UiNode_Title(Godot.Label node) : base(node) {  }
        public override UiNode_Title Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Settlement.ButtonList.Restart
    /// </summary>
    public class UiNode_Restart : IUiNode<Godot.Button, UiNode_Restart>
    {
        public UiNode_Restart(Godot.Button node) : base(node) {  }
        public override UiNode_Restart Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Settlement.ButtonList.ToMenu
    /// </summary>
    public class UiNode_ToMenu : IUiNode<Godot.Button, UiNode_ToMenu>
    {
        public UiNode_ToMenu(Godot.Button node) : base(node) {  }
        public override UiNode_ToMenu Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Settlement.ButtonList
    /// </summary>
    public class UiNode_ButtonList : IUiNode<Godot.VBoxContainer, UiNode_ButtonList>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Settlement.Restart
        /// </summary>
        public UiNode_Restart L_Restart
        {
            get
            {
                if (_L_Restart == null) _L_Restart = new UiNode_Restart(Instance.GetNodeOrNull<Godot.Button>("Restart"));
                return _L_Restart;
            }
        }
        private UiNode_Restart _L_Restart;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Settlement.ToMenu
        /// </summary>
        public UiNode_ToMenu L_ToMenu
        {
            get
            {
                if (_L_ToMenu == null) _L_ToMenu = new UiNode_ToMenu(Instance.GetNodeOrNull<Godot.Button>("ToMenu"));
                return _L_ToMenu;
            }
        }
        private UiNode_ToMenu _L_ToMenu;

        public UiNode_ButtonList(Godot.VBoxContainer node) : base(node) {  }
        public override UiNode_ButtonList Clone() => new ((Godot.VBoxContainer)Instance.Duplicate());
    }

}
