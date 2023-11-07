namespace UI.Debugger;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Debugger : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Debugger.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg((DebuggerPanel)this, GetNode<Godot.ColorRect>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Debugger.HoverButton
    /// </summary>
    public HoverButton L_HoverButton
    {
        get
        {
            if (_L_HoverButton == null) _L_HoverButton = new HoverButton((DebuggerPanel)this, GetNode<Godot.Button>("HoverButton"));
            return _L_HoverButton;
        }
    }
    private HoverButton _L_HoverButton;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Debugger.Fps
    /// </summary>
    public Fps L_Fps
    {
        get
        {
            if (_L_Fps == null) _L_Fps = new Fps((DebuggerPanel)this, GetNode<Godot.Label>("Fps"));
            return _L_Fps;
        }
    }
    private Fps _L_Fps;


    public Debugger() : base(nameof(Debugger))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Debugger.Bg.Clear
    /// </summary>
    public class Clear : UiNode<DebuggerPanel, Godot.Button, Clear>
    {
        public Clear(DebuggerPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Clear Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Debugger.Bg.Close
    /// </summary>
    public class Close : UiNode<DebuggerPanel, Godot.Button, Close>
    {
        public Close(DebuggerPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Close Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Debugger.Bg.ScrollContainer.Label
    /// </summary>
    public class Label : UiNode<DebuggerPanel, Godot.Label, Label>
    {
        public Label(DebuggerPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: Debugger.Bg.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<DebuggerPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Debugger.Bg.Label
        /// </summary>
        public Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new Label(UiPanel, Instance.GetNode<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private Label _L_Label;

        public ScrollContainer(DebuggerPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Debugger.Bg
    /// </summary>
    public class Bg : UiNode<DebuggerPanel, Godot.ColorRect, Bg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Debugger.Clear
        /// </summary>
        public Clear L_Clear
        {
            get
            {
                if (_L_Clear == null) _L_Clear = new Clear(UiPanel, Instance.GetNode<Godot.Button>("Clear"));
                return _L_Clear;
            }
        }
        private Clear _L_Clear;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Debugger.Close
        /// </summary>
        public Close L_Close
        {
            get
            {
                if (_L_Close == null) _L_Close = new Close(UiPanel, Instance.GetNode<Godot.Button>("Close"));
                return _L_Close;
            }
        }
        private Close _L_Close;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: Debugger.ScrollContainer
        /// </summary>
        public ScrollContainer L_ScrollContainer
        {
            get
            {
                if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer(UiPanel, Instance.GetNode<Godot.ScrollContainer>("ScrollContainer"));
                return _L_ScrollContainer;
            }
        }
        private ScrollContainer _L_ScrollContainer;

        public Bg(DebuggerPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Debugger.HoverButton
    /// </summary>
    public class HoverButton : UiNode<DebuggerPanel, Godot.Button, HoverButton>
    {
        public HoverButton(DebuggerPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override HoverButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Debugger.Fps
    /// </summary>
    public class Fps : UiNode<DebuggerPanel, Godot.Label, Fps>
    {
        public Fps(DebuggerPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Fps Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Debugger.Bg.Clear
    /// </summary>
    public Clear S_Clear => L_Bg.L_Clear;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Debugger.Bg.Close
    /// </summary>
    public Close S_Close => L_Bg.L_Close;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Debugger.Bg.ScrollContainer.Label
    /// </summary>
    public Label S_Label => L_Bg.L_ScrollContainer.L_Label;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: Debugger.Bg.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_Bg.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Debugger.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Debugger.HoverButton
    /// </summary>
    public HoverButton S_HoverButton => L_HoverButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Debugger.Fps
    /// </summary>
    public Fps S_Fps => L_Fps;

}
