namespace UI.BottomTips;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class BottomTips : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.PanelContainer"/>, 节点路径: BottomTips.Panel
    /// </summary>
    public UiNode_Panel L_Panel
    {
        get
        {
            if (_L_Panel == null) _L_Panel = new UiNode_Panel(GetNodeOrNull<Godot.PanelContainer>("Panel"));
            return _L_Panel;
        }
    }
    private UiNode_Panel _L_Panel;


    public BottomTips() : base(nameof(BottomTips))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.AspectRatioContainer.TextureRect
    /// </summary>
    public class UiNode_TextureRect : IUiNode<Godot.TextureRect, UiNode_TextureRect>
    {
        public UiNode_TextureRect(Godot.TextureRect node) : base(node) {  }
        public override UiNode_TextureRect Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.AspectRatioContainer"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.AspectRatioContainer
    /// </summary>
    public class UiNode_AspectRatioContainer : IUiNode<Godot.AspectRatioContainer, UiNode_AspectRatioContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.TextureRect
        /// </summary>
        public UiNode_TextureRect L_TextureRect
        {
            get
            {
                if (_L_TextureRect == null) _L_TextureRect = new UiNode_TextureRect(Instance.GetNodeOrNull<Godot.TextureRect>("TextureRect"));
                return _L_TextureRect;
            }
        }
        private UiNode_TextureRect _L_TextureRect;

        public UiNode_AspectRatioContainer(Godot.AspectRatioContainer node) : base(node) {  }
        public override UiNode_AspectRatioContainer Clone() => new ((Godot.AspectRatioContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer.Label
    /// </summary>
    public class UiNode_Label : IUiNode<Godot.Label, UiNode_Label>
    {
        public UiNode_Label(Godot.Label node) : base(node) {  }
        public override UiNode_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer.HBoxContainer
    /// </summary>
    public class UiNode_HBoxContainer : IUiNode<Godot.HBoxContainer, UiNode_HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.AspectRatioContainer"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.AspectRatioContainer
        /// </summary>
        public UiNode_AspectRatioContainer L_AspectRatioContainer
        {
            get
            {
                if (_L_AspectRatioContainer == null) _L_AspectRatioContainer = new UiNode_AspectRatioContainer(Instance.GetNodeOrNull<Godot.AspectRatioContainer>("AspectRatioContainer"));
                return _L_AspectRatioContainer;
            }
        }
        private UiNode_AspectRatioContainer _L_AspectRatioContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: BottomTips.Panel.MarginContainer.CenterContainer.Label
        /// </summary>
        public UiNode_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new UiNode_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private UiNode_Label _L_Label;

        public UiNode_HBoxContainer(Godot.HBoxContainer node) : base(node) {  }
        public override UiNode_HBoxContainer Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.CenterContainer"/>, 路径: BottomTips.Panel.MarginContainer.CenterContainer
    /// </summary>
    public class UiNode_CenterContainer : IUiNode<Godot.CenterContainer, UiNode_CenterContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: BottomTips.Panel.MarginContainer.HBoxContainer
        /// </summary>
        public UiNode_HBoxContainer L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new UiNode_HBoxContainer(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private UiNode_HBoxContainer _L_HBoxContainer;

        public UiNode_CenterContainer(Godot.CenterContainer node) : base(node) {  }
        public override UiNode_CenterContainer Clone() => new ((Godot.CenterContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: BottomTips.Panel.MarginContainer
    /// </summary>
    public class UiNode_MarginContainer : IUiNode<Godot.MarginContainer, UiNode_MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CenterContainer"/>, 节点路径: BottomTips.Panel.CenterContainer
        /// </summary>
        public UiNode_CenterContainer L_CenterContainer
        {
            get
            {
                if (_L_CenterContainer == null) _L_CenterContainer = new UiNode_CenterContainer(Instance.GetNodeOrNull<Godot.CenterContainer>("CenterContainer"));
                return _L_CenterContainer;
            }
        }
        private UiNode_CenterContainer _L_CenterContainer;

        public UiNode_MarginContainer(Godot.MarginContainer node) : base(node) {  }
        public override UiNode_MarginContainer Clone() => new ((Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.PanelContainer"/>, 路径: BottomTips.Panel
    /// </summary>
    public class UiNode_Panel : IUiNode<Godot.PanelContainer, UiNode_Panel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: BottomTips.MarginContainer
        /// </summary>
        public UiNode_MarginContainer L_MarginContainer
        {
            get
            {
                if (_L_MarginContainer == null) _L_MarginContainer = new UiNode_MarginContainer(Instance.GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
                return _L_MarginContainer;
            }
        }
        private UiNode_MarginContainer _L_MarginContainer;

        public UiNode_Panel(Godot.PanelContainer node) : base(node) {  }
        public override UiNode_Panel Clone() => new ((Godot.PanelContainer)Instance.Duplicate());
    }

}
