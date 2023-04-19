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


    public EditorTools() : base(nameof(EditorTools))
    {
    }

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
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.Label
    /// </summary>
    public class UiNode1_Label : IUiNode<Godot.Label, UiNode1_Label>
    {
        public UiNode1_Label(Godot.Label node) : base(node) {  }
        public override UiNode1_Label Clone() => new ((Godot.Label)Instance.Duplicate());
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
    public class UiNode1_Button : IUiNode<Godot.Button, UiNode1_Button>
    {
        public UiNode1_Button(Godot.Button node) : base(node) {  }
        public override UiNode1_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public class UiNode_HBoxContainer3 : IUiNode<Godot.HBoxContainer, UiNode_HBoxContainer3>
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
        public UiNode1_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new UiNode1_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private UiNode1_Button _L_Button;

        public UiNode_HBoxContainer3(Godot.HBoxContainer node) : base(node) {  }
        public override UiNode_HBoxContainer3 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4.Label
    /// </summary>
    public class UiNode2_Label : IUiNode<Godot.Label, UiNode2_Label>
    {
        public UiNode2_Label(Godot.Label node) : base(node) {  }
        public override UiNode2_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4.Button
    /// </summary>
    public class UiNode2_Button : IUiNode<Godot.Button, UiNode2_Button>
    {
        public UiNode2_Button(Godot.Button node) : base(node) {  }
        public override UiNode2_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4
    /// </summary>
    public class UiNode_HBoxContainer4 : IUiNode<Godot.HBoxContainer, UiNode_HBoxContainer4>
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

        public UiNode_HBoxContainer4(Godot.HBoxContainer node) : base(node) {  }
        public override UiNode_HBoxContainer4 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5.Label
    /// </summary>
    public class UiNode3_Label : IUiNode<Godot.Label, UiNode3_Label>
    {
        public UiNode3_Label(Godot.Label node) : base(node) {  }
        public override UiNode3_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5.Button
    /// </summary>
    public class UiNode3_Button : IUiNode<Godot.Button, UiNode3_Button>
    {
        public UiNode3_Button(Godot.Button node) : base(node) {  }
        public override UiNode3_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public class UiNode_HBoxContainer5 : IUiNode<Godot.HBoxContainer, UiNode_HBoxContainer5>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public UiNode3_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new UiNode3_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private UiNode3_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public UiNode3_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new UiNode3_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private UiNode3_Button _L_Button;

        public UiNode_HBoxContainer5(Godot.HBoxContainer node) : base(node) {  }
        public override UiNode_HBoxContainer5 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Label
    /// </summary>
    public class UiNode4_Label : IUiNode<Godot.Label, UiNode4_Label>
    {
        public UiNode4_Label(Godot.Label node) : base(node) {  }
        public override UiNode4_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.RoomNameInput
    /// </summary>
    public class UiNode_RoomNameInput : IUiNode<Godot.LineEdit, UiNode_RoomNameInput>
    {
        public UiNode_RoomNameInput(Godot.LineEdit node) : base(node) {  }
        public override UiNode_RoomNameInput Clone() => new ((Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Label2
    /// </summary>
    public class UiNode_Label2 : IUiNode<Godot.Label, UiNode_Label2>
    {
        public UiNode_Label2(Godot.Label node) : base(node) {  }
        public override UiNode_Label2 Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.RoomGroupSelect
    /// </summary>
    public class UiNode_RoomGroupSelect : IUiNode<Godot.OptionButton, UiNode_RoomGroupSelect>
    {
        public UiNode_RoomGroupSelect(Godot.OptionButton node) : base(node) {  }
        public override UiNode_RoomGroupSelect Clone() => new ((Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Label3
    /// </summary>
    public class UiNode_Label3 : IUiNode<Godot.Label, UiNode_Label3>
    {
        public UiNode_Label3(Godot.Label node) : base(node) {  }
        public override UiNode_Label3 Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.RoomTypeSelect
    /// </summary>
    public class UiNode_RoomTypeSelect : IUiNode<Godot.OptionButton, UiNode_RoomTypeSelect>
    {
        public UiNode_RoomTypeSelect(Godot.OptionButton node) : base(node) {  }
        public override UiNode_RoomTypeSelect Clone() => new ((Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Button
    /// </summary>
    public class UiNode4_Button : IUiNode<Godot.Button, UiNode4_Button>
    {
        public UiNode4_Button(Godot.Button node) : base(node) {  }
        public override UiNode4_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6
    /// </summary>
    public class UiNode_HBoxContainer6 : IUiNode<Godot.HBoxContainer, UiNode_HBoxContainer6>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public UiNode4_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new UiNode4_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private UiNode4_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.RoomNameInput
        /// </summary>
        public UiNode_RoomNameInput L_RoomNameInput
        {
            get
            {
                if (_L_RoomNameInput == null) _L_RoomNameInput = new UiNode_RoomNameInput(Instance.GetNodeOrNull<Godot.LineEdit>("RoomNameInput"));
                return _L_RoomNameInput;
            }
        }
        private UiNode_RoomNameInput _L_RoomNameInput;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label2
        /// </summary>
        public UiNode_Label2 L_Label2
        {
            get
            {
                if (_L_Label2 == null) _L_Label2 = new UiNode_Label2(Instance.GetNodeOrNull<Godot.Label>("Label2"));
                return _L_Label2;
            }
        }
        private UiNode_Label2 _L_Label2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.RoomGroupSelect
        /// </summary>
        public UiNode_RoomGroupSelect L_RoomGroupSelect
        {
            get
            {
                if (_L_RoomGroupSelect == null) _L_RoomGroupSelect = new UiNode_RoomGroupSelect(Instance.GetNodeOrNull<Godot.OptionButton>("RoomGroupSelect"));
                return _L_RoomGroupSelect;
            }
        }
        private UiNode_RoomGroupSelect _L_RoomGroupSelect;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label3
        /// </summary>
        public UiNode_Label3 L_Label3
        {
            get
            {
                if (_L_Label3 == null) _L_Label3 = new UiNode_Label3(Instance.GetNodeOrNull<Godot.Label>("Label3"));
                return _L_Label3;
            }
        }
        private UiNode_Label3 _L_Label3;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.RoomTypeSelect
        /// </summary>
        public UiNode_RoomTypeSelect L_RoomTypeSelect
        {
            get
            {
                if (_L_RoomTypeSelect == null) _L_RoomTypeSelect = new UiNode_RoomTypeSelect(Instance.GetNodeOrNull<Godot.OptionButton>("RoomTypeSelect"));
                return _L_RoomTypeSelect;
            }
        }
        private UiNode_RoomTypeSelect _L_RoomTypeSelect;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public UiNode4_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new UiNode4_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private UiNode4_Button _L_Button;

        public UiNode_HBoxContainer6(Godot.HBoxContainer node) : base(node) {  }
        public override UiNode_HBoxContainer6 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2.Label
    /// </summary>
    public class UiNode5_Label : IUiNode<Godot.Label, UiNode5_Label>
    {
        public UiNode5_Label(Godot.Label node) : base(node) {  }
        public override UiNode5_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2.Button
    /// </summary>
    public class UiNode5_Button : IUiNode<Godot.Button, UiNode5_Button>
    {
        public UiNode5_Button(Godot.Button node) : base(node) {  }
        public override UiNode5_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public class UiNode_HBoxContainer2 : IUiNode<Godot.HBoxContainer, UiNode_HBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public UiNode5_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new UiNode5_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private UiNode5_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public UiNode5_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new UiNode5_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private UiNode5_Button _L_Button;

        public UiNode_HBoxContainer2(Godot.HBoxContainer node) : base(node) {  }
        public override UiNode_HBoxContainer2 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer4
        /// </summary>
        public UiNode_HBoxContainer4 L_HBoxContainer4
        {
            get
            {
                if (_L_HBoxContainer4 == null) _L_HBoxContainer4 = new UiNode_HBoxContainer4(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer4"));
                return _L_HBoxContainer4;
            }
        }
        private UiNode_HBoxContainer4 _L_HBoxContainer4;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer5
        /// </summary>
        public UiNode_HBoxContainer5 L_HBoxContainer5
        {
            get
            {
                if (_L_HBoxContainer5 == null) _L_HBoxContainer5 = new UiNode_HBoxContainer5(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer5"));
                return _L_HBoxContainer5;
            }
        }
        private UiNode_HBoxContainer5 _L_HBoxContainer5;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer6
        /// </summary>
        public UiNode_HBoxContainer6 L_HBoxContainer6
        {
            get
            {
                if (_L_HBoxContainer6 == null) _L_HBoxContainer6 = new UiNode_HBoxContainer6(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer6"));
                return _L_HBoxContainer6;
            }
        }
        private UiNode_HBoxContainer6 _L_HBoxContainer6;

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
