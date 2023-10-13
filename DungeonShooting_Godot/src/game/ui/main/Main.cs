namespace UI.Main;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Main : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Main.ColorRect
    /// </summary>
    public ColorRect L_ColorRect
    {
        get
        {
            if (_L_ColorRect == null) _L_ColorRect = new ColorRect((MainPanel)this, GetNode<Godot.ColorRect>("ColorRect"));
            return _L_ColorRect;
        }
    }
    private ColorRect _L_ColorRect;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.VBoxContainer
    /// </summary>
    public VBoxContainer L_VBoxContainer
    {
        get
        {
            if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer((MainPanel)this, GetNode<Godot.VBoxContainer>("VBoxContainer"));
            return _L_VBoxContainer;
        }
    }
    private VBoxContainer _L_VBoxContainer;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Main.Version
    /// </summary>
    public Version L_Version
    {
        get
        {
            if (_L_Version == null) _L_Version = new Version((MainPanel)this, GetNode<Godot.Label>("Version"));
            return _L_Version;
        }
    }
    private Version _L_Version;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LinkButton"/>, 节点路径: Main.LinkButton
    /// </summary>
    public LinkButton L_LinkButton
    {
        get
        {
            if (_L_LinkButton == null) _L_LinkButton = new LinkButton((MainPanel)this, GetNode<Godot.LinkButton>("LinkButton"));
            return _L_LinkButton;
        }
    }
    private LinkButton _L_LinkButton;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LinkButton"/>, 节点路径: Main.LinkButton2
    /// </summary>
    public LinkButton2 L_LinkButton2
    {
        get
        {
            if (_L_LinkButton2 == null) _L_LinkButton2 = new LinkButton2((MainPanel)this, GetNode<Godot.LinkButton>("LinkButton2"));
            return _L_LinkButton2;
        }
    }
    private LinkButton2 _L_LinkButton2;


    public Main() : base(nameof(Main))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Main.ColorRect
    /// </summary>
    public class ColorRect : UiNode<MainPanel, Godot.ColorRect, ColorRect>
    {
        public ColorRect(MainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override ColorRect Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Main.VBoxContainer.Title
    /// </summary>
    public class Title : UiNode<MainPanel, Godot.Label, Title>
    {
        public Title(MainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.VBoxContainer.ButtonList.Start
    /// </summary>
    public class Start : UiNode<MainPanel, Godot.Button, Start>
    {
        public Start(MainPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Start Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.VBoxContainer.ButtonList.Tools
    /// </summary>
    public class Tools : UiNode<MainPanel, Godot.Button, Tools>
    {
        public Tools(MainPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Tools Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.VBoxContainer.ButtonList.Setting
    /// </summary>
    public class Setting : UiNode<MainPanel, Godot.Button, Setting>
    {
        public Setting(MainPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Setting Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Main.VBoxContainer.ButtonList.Exit
    /// </summary>
    public class Exit : UiNode<MainPanel, Godot.Button, Exit>
    {
        public Exit(MainPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Exit Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Main.VBoxContainer.ButtonList
    /// </summary>
    public class ButtonList : UiNode<MainPanel, Godot.VBoxContainer, ButtonList>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.VBoxContainer.Start
        /// </summary>
        public Start L_Start
        {
            get
            {
                if (_L_Start == null) _L_Start = new Start(UiPanel, Instance.GetNode<Godot.Button>("Start"));
                return _L_Start;
            }
        }
        private Start _L_Start;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.VBoxContainer.Tools
        /// </summary>
        public Tools L_Tools
        {
            get
            {
                if (_L_Tools == null) _L_Tools = new Tools(UiPanel, Instance.GetNode<Godot.Button>("Tools"));
                return _L_Tools;
            }
        }
        private Tools _L_Tools;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.VBoxContainer.Setting
        /// </summary>
        public Setting L_Setting
        {
            get
            {
                if (_L_Setting == null) _L_Setting = new Setting(UiPanel, Instance.GetNode<Godot.Button>("Setting"));
                return _L_Setting;
            }
        }
        private Setting _L_Setting;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.VBoxContainer.Exit
        /// </summary>
        public Exit L_Exit
        {
            get
            {
                if (_L_Exit == null) _L_Exit = new Exit(UiPanel, Instance.GetNode<Godot.Button>("Exit"));
                return _L_Exit;
            }
        }
        private Exit _L_Exit;

        public ButtonList(MainPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override ButtonList Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Main.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MainPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Main.Title
        /// </summary>
        public Title L_Title
        {
            get
            {
                if (_L_Title == null) _L_Title = new Title(UiPanel, Instance.GetNode<Godot.Label>("Title"));
                return _L_Title;
            }
        }
        private Title _L_Title;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.ButtonList
        /// </summary>
        public ButtonList L_ButtonList
        {
            get
            {
                if (_L_ButtonList == null) _L_ButtonList = new ButtonList(UiPanel, Instance.GetNode<Godot.VBoxContainer>("ButtonList"));
                return _L_ButtonList;
            }
        }
        private ButtonList _L_ButtonList;

        public VBoxContainer(MainPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Main.Version
    /// </summary>
    public class Version : UiNode<MainPanel, Godot.Label, Version>
    {
        public Version(MainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Version Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LinkButton"/>, 路径: Main.LinkButton
    /// </summary>
    public class LinkButton : UiNode<MainPanel, Godot.LinkButton, LinkButton>
    {
        public LinkButton(MainPanel uiPanel, Godot.LinkButton node) : base(uiPanel, node) {  }
        public override LinkButton Clone() => new (UiPanel, (Godot.LinkButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LinkButton"/>, 路径: Main.LinkButton2
    /// </summary>
    public class LinkButton2 : UiNode<MainPanel, Godot.LinkButton, LinkButton2>
    {
        public LinkButton2(MainPanel uiPanel, Godot.LinkButton node) : base(uiPanel, node) {  }
        public override LinkButton2 Clone() => new (UiPanel, (Godot.LinkButton)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Main.ColorRect
    /// </summary>
    public ColorRect S_ColorRect => L_ColorRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Main.VBoxContainer.Title
    /// </summary>
    public Title S_Title => L_VBoxContainer.L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.VBoxContainer.ButtonList.Start
    /// </summary>
    public Start S_Start => L_VBoxContainer.L_ButtonList.L_Start;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.VBoxContainer.ButtonList.Tools
    /// </summary>
    public Tools S_Tools => L_VBoxContainer.L_ButtonList.L_Tools;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.VBoxContainer.ButtonList.Setting
    /// </summary>
    public Setting S_Setting => L_VBoxContainer.L_ButtonList.L_Setting;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Main.VBoxContainer.ButtonList.Exit
    /// </summary>
    public Exit S_Exit => L_VBoxContainer.L_ButtonList.L_Exit;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.VBoxContainer.ButtonList
    /// </summary>
    public ButtonList S_ButtonList => L_VBoxContainer.L_ButtonList;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Main.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Main.Version
    /// </summary>
    public Version S_Version => L_Version;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LinkButton"/>, 节点路径: Main.LinkButton
    /// </summary>
    public LinkButton S_LinkButton => L_LinkButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LinkButton"/>, 节点路径: Main.LinkButton2
    /// </summary>
    public LinkButton2 S_LinkButton2 => L_LinkButton2;

}
