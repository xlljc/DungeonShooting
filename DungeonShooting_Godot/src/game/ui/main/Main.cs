namespace UI.Main;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Main : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Main.Title
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
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.ButtonList
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

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Main.Version
    /// </summary>
    public UiNode_Version L_Version
    {
        get
        {
            if (_L_Version == null) _L_Version = new UiNode_Version(GetNodeOrNull<Godot.Label>("Version"));
            return _L_Version;
        }
    }
    private UiNode_Version _L_Version;


    public Main() : base(nameof(Main))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Main.Title
    /// </summary>
    public class UiNode_Title : IUiNode<Godot.Label, UiNode_Title>
    {
        public UiNode_Title(Godot.Label node) : base(node) {  }
        public override UiNode_Title Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.ButtonList.Start
    /// </summary>
    public class UiNode_Start : IUiNode<Godot.Button, UiNode_Start>
    {
        public UiNode_Start(Godot.Button node) : base(node) {  }
        public override UiNode_Start Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.ButtonList.Setting
    /// </summary>
    public class UiNode_Setting : IUiNode<Godot.Button, UiNode_Setting>
    {
        public UiNode_Setting(Godot.Button node) : base(node) {  }
        public override UiNode_Setting Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.ButtonList.Exit
    /// </summary>
    public class UiNode_Exit : IUiNode<Godot.Button, UiNode_Exit>
    {
        public UiNode_Exit(Godot.Button node) : base(node) {  }
        public override UiNode_Exit Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Main.ButtonList
    /// </summary>
    public class UiNode_ButtonList : IUiNode<Godot.VBoxContainer, UiNode_ButtonList>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.Start
        /// </summary>
        public UiNode_Start L_Start
        {
            get
            {
                if (_L_Start == null) _L_Start = new UiNode_Start(Instance.GetNodeOrNull<Godot.Button>("Start"));
                return _L_Start;
            }
        }
        private UiNode_Start _L_Start;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.Setting
        /// </summary>
        public UiNode_Setting L_Setting
        {
            get
            {
                if (_L_Setting == null) _L_Setting = new UiNode_Setting(Instance.GetNodeOrNull<Godot.Button>("Setting"));
                return _L_Setting;
            }
        }
        private UiNode_Setting _L_Setting;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.Exit
        /// </summary>
        public UiNode_Exit L_Exit
        {
            get
            {
                if (_L_Exit == null) _L_Exit = new UiNode_Exit(Instance.GetNodeOrNull<Godot.Button>("Exit"));
                return _L_Exit;
            }
        }
        private UiNode_Exit _L_Exit;

        public UiNode_ButtonList(Godot.VBoxContainer node) : base(node) {  }
        public override UiNode_ButtonList Clone() => new ((Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Main.Version
    /// </summary>
    public class UiNode_Version : IUiNode<Godot.Label, UiNode_Version>
    {
        public UiNode_Version(Godot.Label node) : base(node) {  }
        public override UiNode_Version Clone() => new ((Godot.Label)Instance.Duplicate());
    }

}
