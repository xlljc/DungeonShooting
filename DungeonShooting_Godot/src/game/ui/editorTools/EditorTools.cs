namespace UI.EditorTools;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class EditorTools : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: EditorTools.ScrollContainer
    /// </summary>
    public EditorTools_ScrollContainer L_ScrollContainer
    {
        get
        {
            if (_L_ScrollContainer == null) _L_ScrollContainer = new EditorTools_ScrollContainer(GetNodeOrNull<Godot.ScrollContainer>("ScrollContainer"));
            return _L_ScrollContainer;
        }
    }
    private EditorTools_ScrollContainer _L_ScrollContainer;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ConfirmationDialog"/>, 节点路径: EditorTools.Confirm
    /// </summary>
    public EditorTools_Confirm L_Confirm
    {
        get
        {
            if (_L_Confirm == null) _L_Confirm = new EditorTools_Confirm(GetNodeOrNull<Godot.ConfirmationDialog>("Confirm"));
            return _L_Confirm;
        }
    }
    private EditorTools_Confirm _L_Confirm;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.AcceptDialog"/>, 节点路径: EditorTools.Tips
    /// </summary>
    public EditorTools_Tips L_Tips
    {
        get
        {
            if (_L_Tips == null) _L_Tips = new EditorTools_Tips(GetNodeOrNull<Godot.AcceptDialog>("Tips"));
            return _L_Tips;
        }
    }
    private EditorTools_Tips _L_Tips;


    public EditorTools() : base(nameof(EditorTools))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer.Label
    /// </summary>
    public class EditorTools_Label : IUiNode<Godot.Label, EditorTools_Label>
    {
        public EditorTools_Label(Godot.Label node) : base(node) {  }
        public override EditorTools_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer.Button
    /// </summary>
    public class EditorTools_Button : IUiNode<Godot.Button, EditorTools_Button>
    {
        public EditorTools_Button(Godot.Button node) : base(node) {  }
        public override EditorTools_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class EditorTools_HBoxContainer : IUiNode<Godot.HBoxContainer, EditorTools_HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public EditorTools_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new EditorTools_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private EditorTools_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public EditorTools_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new EditorTools_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private EditorTools_Button _L_Button;

        public EditorTools_HBoxContainer(Godot.HBoxContainer node) : base(node) {  }
        public override EditorTools_HBoxContainer Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.Label
    /// </summary>
    public class EditorTools1_Label : IUiNode<Godot.Label, EditorTools1_Label>
    {
        public EditorTools1_Label(Godot.Label node) : base(node) {  }
        public override EditorTools1_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.LineEdit
    /// </summary>
    public class EditorTools_LineEdit : IUiNode<Godot.LineEdit, EditorTools_LineEdit>
    {
        public EditorTools_LineEdit(Godot.LineEdit node) : base(node) {  }
        public override EditorTools_LineEdit Clone() => new ((Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.Button
    /// </summary>
    public class EditorTools1_Button : IUiNode<Godot.Button, EditorTools1_Button>
    {
        public EditorTools1_Button(Godot.Button node) : base(node) {  }
        public override EditorTools1_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public class EditorTools_HBoxContainer3 : IUiNode<Godot.HBoxContainer, EditorTools_HBoxContainer3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public EditorTools1_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new EditorTools1_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private EditorTools1_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.LineEdit
        /// </summary>
        public EditorTools_LineEdit L_LineEdit
        {
            get
            {
                if (_L_LineEdit == null) _L_LineEdit = new EditorTools_LineEdit(Instance.GetNodeOrNull<Godot.LineEdit>("LineEdit"));
                return _L_LineEdit;
            }
        }
        private EditorTools_LineEdit _L_LineEdit;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public EditorTools1_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new EditorTools1_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private EditorTools1_Button _L_Button;

        public EditorTools_HBoxContainer3(Godot.HBoxContainer node) : base(node) {  }
        public override EditorTools_HBoxContainer3 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4.Label
    /// </summary>
    public class EditorTools2_Label : IUiNode<Godot.Label, EditorTools2_Label>
    {
        public EditorTools2_Label(Godot.Label node) : base(node) {  }
        public override EditorTools2_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4.Button
    /// </summary>
    public class EditorTools2_Button : IUiNode<Godot.Button, EditorTools2_Button>
    {
        public EditorTools2_Button(Godot.Button node) : base(node) {  }
        public override EditorTools2_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4
    /// </summary>
    public class EditorTools_HBoxContainer4 : IUiNode<Godot.HBoxContainer, EditorTools_HBoxContainer4>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public EditorTools2_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new EditorTools2_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private EditorTools2_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public EditorTools2_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new EditorTools2_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private EditorTools2_Button _L_Button;

        public EditorTools_HBoxContainer4(Godot.HBoxContainer node) : base(node) {  }
        public override EditorTools_HBoxContainer4 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5.Label
    /// </summary>
    public class EditorTools3_Label : IUiNode<Godot.Label, EditorTools3_Label>
    {
        public EditorTools3_Label(Godot.Label node) : base(node) {  }
        public override EditorTools3_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5.Button
    /// </summary>
    public class EditorTools3_Button : IUiNode<Godot.Button, EditorTools3_Button>
    {
        public EditorTools3_Button(Godot.Button node) : base(node) {  }
        public override EditorTools3_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public class EditorTools_HBoxContainer5 : IUiNode<Godot.HBoxContainer, EditorTools_HBoxContainer5>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public EditorTools3_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new EditorTools3_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private EditorTools3_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public EditorTools3_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new EditorTools3_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private EditorTools3_Button _L_Button;

        public EditorTools_HBoxContainer5(Godot.HBoxContainer node) : base(node) {  }
        public override EditorTools_HBoxContainer5 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Label
    /// </summary>
    public class EditorTools4_Label : IUiNode<Godot.Label, EditorTools4_Label>
    {
        public EditorTools4_Label(Godot.Label node) : base(node) {  }
        public override EditorTools4_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.RoomNameInput
    /// </summary>
    public class EditorTools_RoomNameInput : IUiNode<Godot.LineEdit, EditorTools_RoomNameInput>
    {
        public EditorTools_RoomNameInput(Godot.LineEdit node) : base(node) {  }
        public override EditorTools_RoomNameInput Clone() => new ((Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Label2
    /// </summary>
    public class EditorTools_Label2 : IUiNode<Godot.Label, EditorTools_Label2>
    {
        public EditorTools_Label2(Godot.Label node) : base(node) {  }
        public override EditorTools_Label2 Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.RoomGroupSelect
    /// </summary>
    public class EditorTools_RoomGroupSelect : IUiNode<Godot.OptionButton, EditorTools_RoomGroupSelect>
    {
        public EditorTools_RoomGroupSelect(Godot.OptionButton node) : base(node) {  }
        public override EditorTools_RoomGroupSelect Clone() => new ((Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Label3
    /// </summary>
    public class EditorTools_Label3 : IUiNode<Godot.Label, EditorTools_Label3>
    {
        public EditorTools_Label3(Godot.Label node) : base(node) {  }
        public override EditorTools_Label3 Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.RoomTypeSelect
    /// </summary>
    public class EditorTools_RoomTypeSelect : IUiNode<Godot.OptionButton, EditorTools_RoomTypeSelect>
    {
        public EditorTools_RoomTypeSelect(Godot.OptionButton node) : base(node) {  }
        public override EditorTools_RoomTypeSelect Clone() => new ((Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Button
    /// </summary>
    public class EditorTools4_Button : IUiNode<Godot.Button, EditorTools4_Button>
    {
        public EditorTools4_Button(Godot.Button node) : base(node) {  }
        public override EditorTools4_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6
    /// </summary>
    public class EditorTools_HBoxContainer6 : IUiNode<Godot.HBoxContainer, EditorTools_HBoxContainer6>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public EditorTools4_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new EditorTools4_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private EditorTools4_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.RoomNameInput
        /// </summary>
        public EditorTools_RoomNameInput L_RoomNameInput
        {
            get
            {
                if (_L_RoomNameInput == null) _L_RoomNameInput = new EditorTools_RoomNameInput(Instance.GetNodeOrNull<Godot.LineEdit>("RoomNameInput"));
                return _L_RoomNameInput;
            }
        }
        private EditorTools_RoomNameInput _L_RoomNameInput;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label2
        /// </summary>
        public EditorTools_Label2 L_Label2
        {
            get
            {
                if (_L_Label2 == null) _L_Label2 = new EditorTools_Label2(Instance.GetNodeOrNull<Godot.Label>("Label2"));
                return _L_Label2;
            }
        }
        private EditorTools_Label2 _L_Label2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.RoomGroupSelect
        /// </summary>
        public EditorTools_RoomGroupSelect L_RoomGroupSelect
        {
            get
            {
                if (_L_RoomGroupSelect == null) _L_RoomGroupSelect = new EditorTools_RoomGroupSelect(Instance.GetNodeOrNull<Godot.OptionButton>("RoomGroupSelect"));
                return _L_RoomGroupSelect;
            }
        }
        private EditorTools_RoomGroupSelect _L_RoomGroupSelect;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label3
        /// </summary>
        public EditorTools_Label3 L_Label3
        {
            get
            {
                if (_L_Label3 == null) _L_Label3 = new EditorTools_Label3(Instance.GetNodeOrNull<Godot.Label>("Label3"));
                return _L_Label3;
            }
        }
        private EditorTools_Label3 _L_Label3;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.RoomTypeSelect
        /// </summary>
        public EditorTools_RoomTypeSelect L_RoomTypeSelect
        {
            get
            {
                if (_L_RoomTypeSelect == null) _L_RoomTypeSelect = new EditorTools_RoomTypeSelect(Instance.GetNodeOrNull<Godot.OptionButton>("RoomTypeSelect"));
                return _L_RoomTypeSelect;
            }
        }
        private EditorTools_RoomTypeSelect _L_RoomTypeSelect;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public EditorTools4_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new EditorTools4_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private EditorTools4_Button _L_Button;

        public EditorTools_HBoxContainer6(Godot.HBoxContainer node) : base(node) {  }
        public override EditorTools_HBoxContainer6 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2.Label
    /// </summary>
    public class EditorTools5_Label : IUiNode<Godot.Label, EditorTools5_Label>
    {
        public EditorTools5_Label(Godot.Label node) : base(node) {  }
        public override EditorTools5_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2.Button
    /// </summary>
    public class EditorTools5_Button : IUiNode<Godot.Button, EditorTools5_Button>
    {
        public EditorTools5_Button(Godot.Button node) : base(node) {  }
        public override EditorTools5_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public class EditorTools_HBoxContainer2 : IUiNode<Godot.HBoxContainer, EditorTools_HBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public EditorTools5_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new EditorTools5_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private EditorTools5_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public EditorTools5_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new EditorTools5_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private EditorTools5_Button _L_Button;

        public EditorTools_HBoxContainer2(Godot.HBoxContainer node) : base(node) {  }
        public override EditorTools_HBoxContainer2 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer7.Label
    /// </summary>
    public class EditorTools6_Label : IUiNode<Godot.Label, EditorTools6_Label>
    {
        public EditorTools6_Label(Godot.Label node) : base(node) {  }
        public override EditorTools6_Label Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer7.Button
    /// </summary>
    public class EditorTools6_Button : IUiNode<Godot.Button, EditorTools6_Button>
    {
        public EditorTools6_Button(Godot.Button node) : base(node) {  }
        public override EditorTools6_Button Clone() => new ((Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer7
    /// </summary>
    public class EditorTools_HBoxContainer7 : IUiNode<Godot.HBoxContainer, EditorTools_HBoxContainer7>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public EditorTools6_Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new EditorTools6_Label(Instance.GetNodeOrNull<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private EditorTools6_Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public EditorTools6_Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new EditorTools6_Button(Instance.GetNodeOrNull<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private EditorTools6_Button _L_Button;

        public EditorTools_HBoxContainer7(Godot.HBoxContainer node) : base(node) {  }
        public override EditorTools_HBoxContainer7 Clone() => new ((Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer
    /// </summary>
    public class EditorTools_VBoxContainer : IUiNode<Godot.VBoxContainer, EditorTools_VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer
        /// </summary>
        public EditorTools_HBoxContainer L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new EditorTools_HBoxContainer(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private EditorTools_HBoxContainer _L_HBoxContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer3
        /// </summary>
        public EditorTools_HBoxContainer3 L_HBoxContainer3
        {
            get
            {
                if (_L_HBoxContainer3 == null) _L_HBoxContainer3 = new EditorTools_HBoxContainer3(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer3"));
                return _L_HBoxContainer3;
            }
        }
        private EditorTools_HBoxContainer3 _L_HBoxContainer3;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer4
        /// </summary>
        public EditorTools_HBoxContainer4 L_HBoxContainer4
        {
            get
            {
                if (_L_HBoxContainer4 == null) _L_HBoxContainer4 = new EditorTools_HBoxContainer4(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer4"));
                return _L_HBoxContainer4;
            }
        }
        private EditorTools_HBoxContainer4 _L_HBoxContainer4;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer5
        /// </summary>
        public EditorTools_HBoxContainer5 L_HBoxContainer5
        {
            get
            {
                if (_L_HBoxContainer5 == null) _L_HBoxContainer5 = new EditorTools_HBoxContainer5(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer5"));
                return _L_HBoxContainer5;
            }
        }
        private EditorTools_HBoxContainer5 _L_HBoxContainer5;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer6
        /// </summary>
        public EditorTools_HBoxContainer6 L_HBoxContainer6
        {
            get
            {
                if (_L_HBoxContainer6 == null) _L_HBoxContainer6 = new EditorTools_HBoxContainer6(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer6"));
                return _L_HBoxContainer6;
            }
        }
        private EditorTools_HBoxContainer6 _L_HBoxContainer6;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer2
        /// </summary>
        public EditorTools_HBoxContainer2 L_HBoxContainer2
        {
            get
            {
                if (_L_HBoxContainer2 == null) _L_HBoxContainer2 = new EditorTools_HBoxContainer2(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer2"));
                return _L_HBoxContainer2;
            }
        }
        private EditorTools_HBoxContainer2 _L_HBoxContainer2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer7
        /// </summary>
        public EditorTools_HBoxContainer7 L_HBoxContainer7
        {
            get
            {
                if (_L_HBoxContainer7 == null) _L_HBoxContainer7 = new EditorTools_HBoxContainer7(Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer7"));
                return _L_HBoxContainer7;
            }
        }
        private EditorTools_HBoxContainer7 _L_HBoxContainer7;

        public EditorTools_VBoxContainer(Godot.VBoxContainer node) : base(node) {  }
        public override EditorTools_VBoxContainer Clone() => new ((Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer
    /// </summary>
    public class EditorTools_MarginContainer : IUiNode<Godot.MarginContainer, EditorTools_MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.VBoxContainer
        /// </summary>
        public EditorTools_VBoxContainer L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new EditorTools_VBoxContainer(Instance.GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private EditorTools_VBoxContainer _L_VBoxContainer;

        public EditorTools_MarginContainer(Godot.MarginContainer node) : base(node) {  }
        public override EditorTools_MarginContainer Clone() => new ((Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: EditorTools.ScrollContainer
    /// </summary>
    public class EditorTools_ScrollContainer : IUiNode<Godot.ScrollContainer, EditorTools_ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorTools.MarginContainer
        /// </summary>
        public EditorTools_MarginContainer L_MarginContainer
        {
            get
            {
                if (_L_MarginContainer == null) _L_MarginContainer = new EditorTools_MarginContainer(Instance.GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
                return _L_MarginContainer;
            }
        }
        private EditorTools_MarginContainer _L_MarginContainer;

        public EditorTools_ScrollContainer(Godot.ScrollContainer node) : base(node) {  }
        public override EditorTools_ScrollContainer Clone() => new ((Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ConfirmationDialog"/>, 路径: EditorTools.Confirm
    /// </summary>
    public class EditorTools_Confirm : IUiNode<Godot.ConfirmationDialog, EditorTools_Confirm>
    {
        public EditorTools_Confirm(Godot.ConfirmationDialog node) : base(node) {  }
        public override EditorTools_Confirm Clone() => new ((Godot.ConfirmationDialog)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.AcceptDialog"/>, 路径: EditorTools.Tips
    /// </summary>
    public class EditorTools_Tips : IUiNode<Godot.AcceptDialog, EditorTools_Tips>
    {
        public EditorTools_Tips(Godot.AcceptDialog node) : base(node) {  }
        public override EditorTools_Tips Clone() => new ((Godot.AcceptDialog)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public EditorTools_HBoxContainer S_HBoxContainer => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.LineEdit
    /// </summary>
    public EditorTools_LineEdit S_LineEdit => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_LineEdit;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public EditorTools_HBoxContainer3 S_HBoxContainer3 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4
    /// </summary>
    public EditorTools_HBoxContainer4 S_HBoxContainer4 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer4;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public EditorTools_HBoxContainer5 S_HBoxContainer5 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer5;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.RoomNameInput
    /// </summary>
    public EditorTools_RoomNameInput S_RoomNameInput => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer6.L_RoomNameInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Label2
    /// </summary>
    public EditorTools_Label2 S_Label2 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer6.L_Label2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.RoomGroupSelect
    /// </summary>
    public EditorTools_RoomGroupSelect S_RoomGroupSelect => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer6.L_RoomGroupSelect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.Label3
    /// </summary>
    public EditorTools_Label3 S_Label3 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer6.L_Label3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6.RoomTypeSelect
    /// </summary>
    public EditorTools_RoomTypeSelect S_RoomTypeSelect => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer6.L_RoomTypeSelect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer6
    /// </summary>
    public EditorTools_HBoxContainer6 S_HBoxContainer6 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer6;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer2
    /// </summary>
    public EditorTools_HBoxContainer2 S_HBoxContainer2 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer7
    /// </summary>
    public EditorTools_HBoxContainer7 S_HBoxContainer7 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer7;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer
    /// </summary>
    public EditorTools_VBoxContainer S_VBoxContainer => L_ScrollContainer.L_MarginContainer.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer
    /// </summary>
    public EditorTools_MarginContainer S_MarginContainer => L_ScrollContainer.L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.EditorTools.EditorToolsPanel"/>, 节点路径: EditorTools.ScrollContainer
    /// </summary>
    public EditorTools_ScrollContainer S_ScrollContainer => L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.EditorTools.EditorToolsPanel"/>, 节点路径: EditorTools.Confirm
    /// </summary>
    public EditorTools_Confirm S_Confirm => L_Confirm;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.EditorTools.EditorToolsPanel"/>, 节点路径: EditorTools.Tips
    /// </summary>
    public EditorTools_Tips S_Tips => L_Tips;

}
