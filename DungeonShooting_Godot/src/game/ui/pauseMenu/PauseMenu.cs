namespace UI.PauseMenu;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class PauseMenu : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: PauseMenu.ColorRect
    /// </summary>
    public ColorRect L_ColorRect
    {
        get
        {
            if (_L_ColorRect == null) _L_ColorRect = new ColorRect((PauseMenuPanel)this, GetNode<Godot.ColorRect>("ColorRect"));
            return _L_ColorRect;
        }
    }
    private ColorRect _L_ColorRect;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: PauseMenu.VBoxContainer
    /// </summary>
    public VBoxContainer L_VBoxContainer
    {
        get
        {
            if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer((PauseMenuPanel)this, GetNode<Godot.VBoxContainer>("VBoxContainer"));
            return _L_VBoxContainer;
        }
    }
    private VBoxContainer _L_VBoxContainer;


    public PauseMenu() : base(nameof(PauseMenu))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: PauseMenu.ColorRect
    /// </summary>
    public class ColorRect : UiNode<PauseMenuPanel, Godot.ColorRect, ColorRect>
    {
        public ColorRect(PauseMenuPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override ColorRect Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: PauseMenu.VBoxContainer.Continue
    /// </summary>
    public class Continue : UiNode<PauseMenuPanel, Godot.Button, Continue>
    {
        public Continue(PauseMenuPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Continue Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: PauseMenu.VBoxContainer.Restart
    /// </summary>
    public class Restart : UiNode<PauseMenuPanel, Godot.Button, Restart>
    {
        public Restart(PauseMenuPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Restart Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: PauseMenu.VBoxContainer.Encyclopedia
    /// </summary>
    public class Encyclopedia : UiNode<PauseMenuPanel, Godot.Button, Encyclopedia>
    {
        public Encyclopedia(PauseMenuPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Encyclopedia Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: PauseMenu.VBoxContainer.Setting
    /// </summary>
    public class Setting : UiNode<PauseMenuPanel, Godot.Button, Setting>
    {
        public Setting(PauseMenuPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Setting Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: PauseMenu.VBoxContainer.Exit
    /// </summary>
    public class Exit : UiNode<PauseMenuPanel, Godot.Button, Exit>
    {
        public Exit(PauseMenuPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Exit Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: PauseMenu.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<PauseMenuPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.Continue
        /// </summary>
        public Continue L_Continue
        {
            get
            {
                if (_L_Continue == null) _L_Continue = new Continue(UiPanel, Instance.GetNode<Godot.Button>("Continue"));
                return _L_Continue;
            }
        }
        private Continue _L_Continue;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.Restart
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.Encyclopedia
        /// </summary>
        public Encyclopedia L_Encyclopedia
        {
            get
            {
                if (_L_Encyclopedia == null) _L_Encyclopedia = new Encyclopedia(UiPanel, Instance.GetNode<Godot.Button>("Encyclopedia"));
                return _L_Encyclopedia;
            }
        }
        private Encyclopedia _L_Encyclopedia;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.Setting
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.Exit
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

        public VBoxContainer(PauseMenuPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: PauseMenu.ColorRect
    /// </summary>
    public ColorRect S_ColorRect => L_ColorRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.VBoxContainer.Continue
    /// </summary>
    public Continue S_Continue => L_VBoxContainer.L_Continue;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.VBoxContainer.Restart
    /// </summary>
    public Restart S_Restart => L_VBoxContainer.L_Restart;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.VBoxContainer.Encyclopedia
    /// </summary>
    public Encyclopedia S_Encyclopedia => L_VBoxContainer.L_Encyclopedia;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.VBoxContainer.Setting
    /// </summary>
    public Setting S_Setting => L_VBoxContainer.L_Setting;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: PauseMenu.VBoxContainer.Exit
    /// </summary>
    public Exit S_Exit => L_VBoxContainer.L_Exit;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: PauseMenu.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_VBoxContainer;

}
