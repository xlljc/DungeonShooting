namespace UI.Main;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Main : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Main.Title
    /// </summary>
    public Main_Title L_Title
    {
        get
        {
            if (_L_Title == null) _L_Title = new Main_Title(this, GetNodeOrNull<Godot.Label>("Title"));
            return _L_Title;
        }
    }
    private Main_Title _L_Title;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.ButtonList
    /// </summary>
    public Main_ButtonList L_ButtonList
    {
        get
        {
            if (_L_ButtonList == null) _L_ButtonList = new Main_ButtonList(this, GetNodeOrNull<Godot.VBoxContainer>("ButtonList"));
            return _L_ButtonList;
        }
    }
    private Main_ButtonList _L_ButtonList;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Main.Version
    /// </summary>
    public Main_Version L_Version
    {
        get
        {
            if (_L_Version == null) _L_Version = new Main_Version(this, GetNodeOrNull<Godot.Label>("Version"));
            return _L_Version;
        }
    }
    private Main_Version _L_Version;


    public Main() : base(nameof(Main))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Main.Title
    /// </summary>
    public class Main_Title : UiNode<Main, Godot.Label, Main_Title>
    {
        public Main_Title(Main uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Main_Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.ButtonList.Start
    /// </summary>
    public class Main_Start : UiNode<Main, Godot.Button, Main_Start>
    {
        public Main_Start(Main uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Main_Start Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.ButtonList.Tools
    /// </summary>
    public class Main_Tools : UiNode<Main, Godot.Button, Main_Tools>
    {
        public Main_Tools(Main uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Main_Tools Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.ButtonList.Setting
    /// </summary>
    public class Main_Setting : UiNode<Main, Godot.Button, Main_Setting>
    {
        public Main_Setting(Main uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Main_Setting Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.ButtonList.Exit
    /// </summary>
    public class Main_Exit : UiNode<Main, Godot.Button, Main_Exit>
    {
        public Main_Exit(Main uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Main_Exit Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Main.ButtonList
    /// </summary>
    public class Main_ButtonList : UiNode<Main, Godot.VBoxContainer, Main_ButtonList>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.Start
        /// </summary>
        public Main_Start L_Start
        {
            get
            {
                if (_L_Start == null) _L_Start = new Main_Start(UiPanel, Instance.GetNodeOrNull<Godot.Button>("Start"));
                return _L_Start;
            }
        }
        private Main_Start _L_Start;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.Tools
        /// </summary>
        public Main_Tools L_Tools
        {
            get
            {
                if (_L_Tools == null) _L_Tools = new Main_Tools(UiPanel, Instance.GetNodeOrNull<Godot.Button>("Tools"));
                return _L_Tools;
            }
        }
        private Main_Tools _L_Tools;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.Setting
        /// </summary>
        public Main_Setting L_Setting
        {
            get
            {
                if (_L_Setting == null) _L_Setting = new Main_Setting(UiPanel, Instance.GetNodeOrNull<Godot.Button>("Setting"));
                return _L_Setting;
            }
        }
        private Main_Setting _L_Setting;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.Exit
        /// </summary>
        public Main_Exit L_Exit
        {
            get
            {
                if (_L_Exit == null) _L_Exit = new Main_Exit(UiPanel, Instance.GetNodeOrNull<Godot.Button>("Exit"));
                return _L_Exit;
            }
        }
        private Main_Exit _L_Exit;

        public Main_ButtonList(Main uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override Main_ButtonList Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Main.Version
    /// </summary>
    public class Main_Version : UiNode<Main, Godot.Label, Main_Version>
    {
        public Main_Version(Main uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Main_Version Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.Main.MainPanel"/>, 节点路径: Main.Title
    /// </summary>
    public Main_Title S_Title => L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.ButtonList.Start
    /// </summary>
    public Main_Start S_Start => L_ButtonList.L_Start;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.ButtonList.Tools
    /// </summary>
    public Main_Tools S_Tools => L_ButtonList.L_Tools;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.ButtonList.Setting
    /// </summary>
    public Main_Setting S_Setting => L_ButtonList.L_Setting;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.ButtonList.Exit
    /// </summary>
    public Main_Exit S_Exit => L_ButtonList.L_Exit;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.Main.MainPanel"/>, 节点路径: Main.ButtonList
    /// </summary>
    public Main_ButtonList S_ButtonList => L_ButtonList;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.Main.MainPanel"/>, 节点路径: Main.Version
    /// </summary>
    public Main_Version S_Version => L_Version;

}
