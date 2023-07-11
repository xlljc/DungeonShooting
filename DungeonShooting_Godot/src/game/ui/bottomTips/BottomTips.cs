namespace UI.BottomTips;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class BottomTips : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.PanelContainer"/>, 节点路径: BottomTips.Panel
    /// </summary>
    public BottomTips_Panel L_Panel
    {
        get
        {
            if (_L_Panel == null) _L_Panel = new BottomTips_Panel(GetNodeOrNull<Godot.PanelContainer>("Panel"));
            return _L_Panel;
        }
    }
    private BottomTips_Panel _L_Panel;


    public BottomTips() : base(nameof(BottomTips))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.AspectRatioContainer.TextureRect
    /// </summary>
    public class BottomTips_TextureRect : IUiNode<Godot.TextureRect, BottomTips_TextureRect>
    {
        public BottomTips_TextureRect(Godot.TextureRect node) : base(node) {  }
        public override BottomTips_TextureRect Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.AspectRatioContainer"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.AspectRatioContainer
    /// </summary>
    public class BottomTips_AspectRatioContainer : IUiNode<Godot.AspectRatioContainer, BottomTips_AspectRatioContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.TextureRect
        /// </summary>
        public BottomTips_TextureRect L_TextureRect
        {
            get
            {
                if (_L_TextureRect == null) _L_TextureRect = new BottomTips_TextureRect(Instance.GetNodeOrNull<Godot.TextureRect>("TextureRect"));
                return _L_TextureRect;
            }
        }
        private BottomTips_TextureRect _L_TextureRect;

        public BottomTips_AspectRatioContainer(Godot.AspectRatioContainer node) : base(node) {  }
        public override BottomTips_AspectRatioContainer Clone() => new ((Godot.AspectRatioContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.Label
    /// </summary>
    public class BottomTips_Label : IUiNode<Godot.Label, BottomTips_Label>
    {
        public BottomTips_Label(Godot.Label node) : base(node) {  }
        public override BottomTips_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer
    /// </summary>
    public class BottomTips_HBoxContainer : IUiNode<Godot.HBoxContainer, BottomTips_HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.AspectRatioContainer"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.AspectRatioContainer
        /// </summary>
        public BottomTips_AspectRatioContainer L_AspectRatioContainer
        {
            get
            {
                if (_L_AspectRatioContainer == null) _L_AspectRatioContainer = new BottomTips_AspectRatioContainer(Instance.GetNodeOrNull<Godot.AspectRatioContainer>("AspectRatioContainer"));
                return _L_AspectRatioContainer;
            }
        }
        private BottomTips_AspectRatioContainer _L_AspectRatioContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.Label
        /// </summary>
        public BottomTips_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new BottomTips_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private BottomTips_Label _L_Label;

        public BottomTips_HBoxContainer(Godot.HBoxContainer node) : base(node) {  }
        public override BottomTips_HBoxContainer Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.CenterContainer"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer
    /// </summary>
    public class BottomTips_CenterContainer : IUiNode<Godot.CenterContainer, BottomTips_CenterContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: BottomTips.Panel.MarginContainer.HBoxContainer
        /// </summary>
        public BottomTips_HBoxContainer L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new BottomTips_HBoxContainer(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private BottomTips_HBoxContainer _L_HBoxContainer;

        public BottomTips_CenterContainer(Godot.CenterContainer node) : base(node) {  }
        public override BottomTips_CenterContainer Clone() => new ((Godot.CenterContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: BottomTips.Panel.MarginContainer
    /// </summary>
    public class BottomTips_MarginContainer : IUiNode<Godot.MarginContainer, BottomTips_MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CenterContainer"/>, 节点路径: BottomTips.Panel.CenterContainer
        /// </summary>
        public BottomTips_CenterContainer L_CenterContainer
        {
            get
            {
                if (_L_CenterContainer == null) _L_CenterContainer = new BottomTips_CenterContainer(Instance.GetNodeOrNull<Godot.CenterContainer>("CenterContainer"));
                return _L_CenterContainer;
            }
        }
        private BottomTips_CenterContainer _L_CenterContainer;

        public BottomTips_MarginContainer(Godot.MarginContainer node) : base(node) {  }
        public override BottomTips_MarginContainer Clone() => new ((Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.PanelContainer"/>, 路径: BottomTips.Panel
    /// </summary>
    public class BottomTips_Panel : IUiNode<Godot.PanelContainer, BottomTips_Panel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: BottomTips.MarginContainer
        /// </summary>
        public BottomTips_MarginContainer L_MarginContainer
        {
            get
            {
                if (_L_MarginContainer == null) _L_MarginContainer = new BottomTips_MarginContainer(Instance.GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
                return _L_MarginContainer;
            }
        }
        private BottomTips_MarginContainer _L_MarginContainer;

        public BottomTips_Panel(Godot.PanelContainer node) : base(node) {  }
        public override BottomTips_Panel Clone() => new ((Godot.PanelContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.AspectRatioContainer"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.AspectRatioContainer.TextureRect
    /// </summary>
    public BottomTips_TextureRect S_TextureRect => L_Panel.L_MarginContainer.L_CenterContainer.L_HBoxContainer.L_AspectRatioContainer.L_TextureRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.AspectRatioContainer
    /// </summary>
    public BottomTips_AspectRatioContainer S_AspectRatioContainer => L_Panel.L_MarginContainer.L_CenterContainer.L_HBoxContainer.L_AspectRatioContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.Label
    /// </summary>
    public BottomTips_Label S_Label => L_Panel.L_MarginContainer.L_CenterContainer.L_HBoxContainer.L_Label;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CenterContainer"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer
    /// </summary>
    public BottomTips_HBoxContainer S_HBoxContainer => L_Panel.L_MarginContainer.L_CenterContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer
    /// </summary>
    public BottomTips_CenterContainer S_CenterContainer => L_Panel.L_MarginContainer.L_CenterContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.PanelContainer"/>, 节点路径: BottomTips.Panel.MarginContainer
    /// </summary>
    public BottomTips_MarginContainer S_MarginContainer => L_Panel.L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.BottomTips.BottomTipsPanel"/>, 节点路径: BottomTips.Panel
    /// </summary>
    public BottomTips_Panel S_Panel => L_Panel;

}
