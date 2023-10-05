namespace UI.Setting;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Setting : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Setting.ColorRect
    /// </summary>
    public ColorRect L_ColorRect
    {
        get
        {
            if (_L_ColorRect == null) _L_ColorRect = new ColorRect((SettingPanel)this, GetNode<Godot.ColorRect>("ColorRect"));
            return _L_ColorRect;
        }
    }
    private ColorRect _L_ColorRect;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Setting.Back
    /// </summary>
    public Back L_Back
    {
        get
        {
            if (_L_Back == null) _L_Back = new Back((SettingPanel)this, GetNode<Godot.Button>("Back"));
            return _L_Back;
        }
    }
    private Back _L_Back;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.Title
    /// </summary>
    public Title L_Title
    {
        get
        {
            if (_L_Title == null) _L_Title = new Title((SettingPanel)this, GetNode<Godot.Label>("Title"));
            return _L_Title;
        }
    }
    private Title _L_Title;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Setting.VBoxContainer
    /// </summary>
    public VBoxContainer L_VBoxContainer
    {
        get
        {
            if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer((SettingPanel)this, GetNode<Godot.VBoxContainer>("VBoxContainer"));
            return _L_VBoxContainer;
        }
    }
    private VBoxContainer _L_VBoxContainer;


    public Setting() : base(nameof(Setting))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Setting.ColorRect
    /// </summary>
    public class ColorRect : UiNode<SettingPanel, Godot.ColorRect, ColorRect>
    {
        public ColorRect(SettingPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override ColorRect Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Setting.Back
    /// </summary>
    public class Back : UiNode<SettingPanel, Godot.Button, Back>
    {
        public Back(SettingPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.Title
    /// </summary>
    public class Title : UiNode<SettingPanel, Godot.Label, Title>
    {
        public Title(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.VBoxContainer.FullScreen.Name
    /// </summary>
    public class Name : UiNode<SettingPanel, Godot.Label, Name>
    {
        public Name(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.CheckBox"/>, 路径: Setting.VBoxContainer.FullScreen.CheckBox
    /// </summary>
    public class CheckBox : UiNode<SettingPanel, Godot.CheckBox, CheckBox>
    {
        public CheckBox(SettingPanel uiPanel, Godot.CheckBox node) : base(uiPanel, node) {  }
        public override CheckBox Clone() => new (UiPanel, (Godot.CheckBox)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.VBoxContainer.FullScreen
    /// </summary>
    public class FullScreen : UiNode<SettingPanel, Godot.HBoxContainer, FullScreen>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.VBoxContainer.Name
        /// </summary>
        public Name L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CheckBox"/>, 节点路径: Setting.VBoxContainer.CheckBox
        /// </summary>
        public CheckBox L_CheckBox
        {
            get
            {
                if (_L_CheckBox == null) _L_CheckBox = new CheckBox(UiPanel, Instance.GetNode<Godot.CheckBox>("CheckBox"));
                return _L_CheckBox;
            }
        }
        private CheckBox _L_CheckBox;

        public FullScreen(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override FullScreen Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Setting.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<SettingPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.FullScreen
        /// </summary>
        public FullScreen L_FullScreen
        {
            get
            {
                if (_L_FullScreen == null) _L_FullScreen = new FullScreen(UiPanel, Instance.GetNode<Godot.HBoxContainer>("FullScreen"));
                return _L_FullScreen;
            }
        }
        private FullScreen _L_FullScreen;

        public VBoxContainer(SettingPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Setting.ColorRect
    /// </summary>
    public ColorRect S_ColorRect => L_ColorRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Setting.Back
    /// </summary>
    public Back S_Back => L_Back;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.Title
    /// </summary>
    public Title S_Title => L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.VBoxContainer.FullScreen.Name
    /// </summary>
    public Name S_Name => L_VBoxContainer.L_FullScreen.L_Name;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CheckBox"/>, 节点路径: Setting.VBoxContainer.FullScreen.CheckBox
    /// </summary>
    public CheckBox S_CheckBox => L_VBoxContainer.L_FullScreen.L_CheckBox;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.VBoxContainer.FullScreen
    /// </summary>
    public FullScreen S_FullScreen => L_VBoxContainer.L_FullScreen;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Setting.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_VBoxContainer;

}
