namespace UI.EditorTools;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class EditorTools : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: EditorTools.ScrollContainer
    /// </summary>
    public UiNode_ScrollContainer L_ScrollContainer
    {
        get
        {
            if (_L_ScrollContainer == null) _L_ScrollContainer = new UiNode_ScrollContainer(GetNodeOrNull<Godot.ScrollContainer>("ScrollContainer"));
            return _L_ScrollContainer;
        }
    }
    private UiNode_ScrollContainer _L_ScrollContainer;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ConfirmationDialog"/>, 节点路径: EditorTools.Confirm
    /// </summary>
    public UiNode_Confirm L_Confirm
    {
        get
        {
            if (_L_Confirm == null) _L_Confirm = new UiNode_Confirm(GetNodeOrNull<Godot.ConfirmationDialog>("Confirm"));
            return _L_Confirm;
        }
    }
    private UiNode_Confirm _L_Confirm;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.AcceptDialog"/>, 节点路径: EditorTools.Tips
    /// </summary>
    public UiNode_Tips L_Tips
    {
        get
        {
            if (_L_Tips == null) _L_Tips = new UiNode_Tips(GetNodeOrNull<Godot.AcceptDialog>("Tips"));
            return _L_Tips;
        }
    }
    private UiNode_Tips _L_Tips;



    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer.Label
    /// </summary>
    public class UiNode_Label : IUiNode<Godot.Label, UiNode_Label>
    {
        public UiNode_Label(Godot.Label node) : base(node) {  }
        public override UiNode_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer.Button
    /// </summary>
    public class UiNode_Button : IUiNode<Godot.Button, UiNode_Button>
    {
        public UiNode_Button(Godot.Button node) : base(node) {  }
        public override UiNode_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class UiNode_HBoxContainer : IUiNode<Godot.HBoxContainer, UiNode_HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public UiNode_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new UiNode_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private UiNode_Button _L_Button;

        public UiNode_HBoxContainer(Godot.HBoxContainer node) : base(node) {  }
        public override UiNode_HBoxContainer Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2.Label
    /// </summary>
    public class UiNode1_Label : IUiNode<Godot.Label, UiNode1_Label>
    {
        public UiNode1_Label(Godot.Label node) : base(node) {  }
        public override UiNode1_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2.Button
    /// </summary>
    public class UiNode1_Button : IUiNode<Godot.Button, UiNode1_Button>
    {
        public UiNode1_Button(Godot.Button node) : base(node) {  }
        public override UiNode1_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public class UiNode_HBoxContainer2 : IUiNode<Godot.HBoxContainer, UiNode_HBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public UiNode1_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new UiNode1_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private UiNode1_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public UiNode1_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new UiNode1_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private UiNode1_Button _L_Button;

        public UiNode_HBoxContainer2(Godot.HBoxContainer node) : base(node) {  }
        public override UiNode_HBoxContainer2 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.Label
    /// </summary>
    public class UiNode2_Label : IUiNode<Godot.Label, UiNode2_Label>
    {
        public UiNode2_Label(Godot.Label node) : base(node) {  }
        public override UiNode2_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.LineEdit
    /// </summary>
    public class UiNode_LineEdit : IUiNode<Godot.LineEdit, UiNode_LineEdit>
    {
        public UiNode_LineEdit(Godot.LineEdit node) : base(node) {  }
        public override UiNode_LineEdit Clone() => new ((Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.Button
    /// </summary>
    public class UiNode2_Button : IUiNode<Godot.Button, UiNode2_Button>
    {
        public UiNode2_Button(Godot.Button node) : base(node) {  }
        public override UiNode2_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public class UiNode_HBoxContainer3 : IUiNode<Godot.HBoxContainer, UiNode_HBoxContainer3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public UiNode2_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new UiNode2_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private UiNode2_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.LineEdit
        /// </summary>
        public UiNode_LineEdit L_LineEdit
        {
            get
            {
                if (_L_LineEdit == null) _L_LineEdit = new UiNode_LineEdit(Instance.GetNodeOrNull<Godot.LineEdit>("LineEdit"));
                return _L_LineEdit;
            }
        }
        private UiNode_LineEdit _L_LineEdit;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public UiNode2_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new UiNode2_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private UiNode2_Button _L_Button;

        public UiNode_HBoxContainer3(Godot.HBoxContainer node) : base(node) {  }
        public override UiNode_HBoxContainer3 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer
    /// </summary>
    public class UiNode_VBoxContainer : IUiNode<Godot.VBoxContainer, UiNode_VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer2
        /// </summary>
        public UiNode_HBoxContainer2 L_HBoxContainer2
        {
            get
            {
                if (_L_HBoxContainer2 == null) _L_HBoxContainer2 = new UiNode_HBoxContainer2(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer2"));
                return _L_HBoxContainer2;
            }
        }
        private UiNode_HBoxContainer2 _L_HBoxContainer2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer3
        /// </summary>
        public UiNode_HBoxContainer3 L_HBoxContainer3
        {
            get
            {
                if (_L_HBoxContainer3 == null) _L_HBoxContainer3 = new UiNode_HBoxContainer3(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer3"));
                return _L_HBoxContainer3;
            }
        }
        private UiNode_HBoxContainer3 _L_HBoxContainer3;

        public UiNode_VBoxContainer(Godot.VBoxContainer node) : base(node) {  }
        public override UiNode_VBoxContainer Clone() => new ((Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer
    /// </summary>
    public class UiNode_MarginContainer : IUiNode<Godot.MarginContainer, UiNode_MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.VBoxContainer
        /// </summary>
        public UiNode_VBoxContainer L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new UiNode_VBoxContainer(Instance.GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private UiNode_VBoxContainer _L_VBoxContainer;

        public UiNode_MarginContainer(Godot.MarginContainer node) : base(node) {  }
        public override UiNode_MarginContainer Clone() => new ((Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: EditorTools.ScrollContainer
    /// </summary>
    public class UiNode_ScrollContainer : IUiNode<Godot.ScrollContainer, UiNode_ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorTools.MarginContainer
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

        public UiNode_ScrollContainer(Godot.ScrollContainer node) : base(node) {  }
        public override UiNode_ScrollContainer Clone() => new ((Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ConfirmationDialog"/>, 路径: EditorTools.Confirm
    /// </summary>
    public class UiNode_Confirm : IUiNode<Godot.ConfirmationDialog, UiNode_Confirm>
    {
        public UiNode_Confirm(Godot.ConfirmationDialog node) : base(node) {  }
        public override UiNode_Confirm Clone() => new ((Godot.ConfirmationDialog)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.AcceptDialog"/>, 路径: EditorTools.Tips
    /// </summary>
    public class UiNode_Tips : IUiNode<Godot.AcceptDialog, UiNode_Tips>
    {
        public UiNode_Tips(Godot.AcceptDialog node) : base(node) {  }
        public override UiNode_Tips Clone() => new ((Godot.AcceptDialog)Instance.Duplicate());
    }

}
