namespace UI.EditorTools;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class EditorTools : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: EditorTools.ScrollContainer
    /// </summary>
    public ScrollContainer L_ScrollContainer
    {
        get
        {
            if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer((EditorToolsPanel)this, GetNode<Godot.ScrollContainer>("ScrollContainer"));
            return _L_ScrollContainer;
        }
    }
    private ScrollContainer _L_ScrollContainer;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ConfirmationDialog"/>, 节点路径: EditorTools.Confirm
    /// </summary>
    public Confirm L_Confirm
    {
        get
        {
            if (_L_Confirm == null) _L_Confirm = new Confirm((EditorToolsPanel)this, GetNode<Godot.ConfirmationDialog>("Confirm"));
            return _L_Confirm;
        }
    }
    private Confirm _L_Confirm;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.AcceptDialog"/>, 节点路径: EditorTools.Tips
    /// </summary>
    public Tips L_Tips
    {
        get
        {
            if (_L_Tips == null) _L_Tips = new Tips((EditorToolsPanel)this, GetNode<Godot.AcceptDialog>("Tips"));
            return _L_Tips;
        }
    }
    private Tips _L_Tips;


    public EditorTools() : base(nameof(EditorTools))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer.Label
    /// </summary>
    public class Label : UiNode<EditorToolsPanel, Godot.Label, Label>
    {
        public Label(EditorToolsPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer.Button
    /// </summary>
    public class Button : UiNode<EditorToolsPanel, Godot.Button, Button>
    {
        public Button(EditorToolsPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Button Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<EditorToolsPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public Button L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new Button(UiPanel, Instance.GetNode<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private Button _L_Button;

        public HBoxContainer(EditorToolsPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.Label
    /// </summary>
    public class Label_1 : UiNode<EditorToolsPanel, Godot.Label, Label_1>
    {
        public Label_1(EditorToolsPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_1 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.LineEdit
    /// </summary>
    public class LineEdit : UiNode<EditorToolsPanel, Godot.LineEdit, LineEdit>
    {
        public LineEdit(EditorToolsPanel uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override LineEdit Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.Button
    /// </summary>
    public class Button_1 : UiNode<EditorToolsPanel, Godot.Button, Button_1>
    {
        public Button_1(EditorToolsPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Button_1 Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public class HBoxContainer3 : UiNode<EditorToolsPanel, Godot.HBoxContainer, HBoxContainer3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public Label_1 L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new Label_1(UiPanel, Instance.GetNode<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private Label_1 _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.LineEdit
        /// </summary>
        public LineEdit L_LineEdit
        {
            get
            {
                if (_L_LineEdit == null) _L_LineEdit = new LineEdit(UiPanel, Instance.GetNode<Godot.LineEdit>("LineEdit"));
                return _L_LineEdit;
            }
        }
        private LineEdit _L_LineEdit;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public Button_1 L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new Button_1(UiPanel, Instance.GetNode<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private Button_1 _L_Button;

        public HBoxContainer3(EditorToolsPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer3 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4.Label
    /// </summary>
    public class Label_2 : UiNode<EditorToolsPanel, Godot.Label, Label_2>
    {
        public Label_2(EditorToolsPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4.Button
    /// </summary>
    public class Button_2 : UiNode<EditorToolsPanel, Godot.Button, Button_2>
    {
        public Button_2(EditorToolsPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Button_2 Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4
    /// </summary>
    public class HBoxContainer4 : UiNode<EditorToolsPanel, Godot.HBoxContainer, HBoxContainer4>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public Label_2 L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new Label_2(UiPanel, Instance.GetNode<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private Label_2 _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public Button_2 L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new Button_2(UiPanel, Instance.GetNode<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private Button_2 _L_Button;

        public HBoxContainer4(EditorToolsPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer4 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5.Label
    /// </summary>
    public class Label_3 : UiNode<EditorToolsPanel, Godot.Label, Label_3>
    {
        public Label_3(EditorToolsPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_3 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5.Button
    /// </summary>
    public class Button_3 : UiNode<EditorToolsPanel, Godot.Button, Button_3>
    {
        public Button_3(EditorToolsPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Button_3 Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public class HBoxContainer5 : UiNode<EditorToolsPanel, Godot.HBoxContainer, HBoxContainer5>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public Label_3 L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new Label_3(UiPanel, Instance.GetNode<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private Label_3 _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public Button_3 L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new Button_3(UiPanel, Instance.GetNode<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private Button_3 _L_Button;

        public HBoxContainer5(EditorToolsPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer5 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer7.Label
    /// </summary>
    public class Label_4 : UiNode<EditorToolsPanel, Godot.Label, Label_4>
    {
        public Label_4(EditorToolsPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_4 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer7.Button
    /// </summary>
    public class Button_4 : UiNode<EditorToolsPanel, Godot.Button, Button_4>
    {
        public Button_4(EditorToolsPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Button_4 Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer7
    /// </summary>
    public class HBoxContainer7 : UiNode<EditorToolsPanel, Godot.HBoxContainer, HBoxContainer7>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Label
        /// </summary>
        public Label_4 L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new Label_4(UiPanel, Instance.GetNode<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private Label_4 _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.Button
        /// </summary>
        public Button_4 L_Button
        {
            get
            {
                if (_L_Button == null) _L_Button = new Button_4(UiPanel, Instance.GetNode<Godot.Button>("Button"));
                return _L_Button;
            }
        }
        private Button_4 _L_Button;

        public HBoxContainer7(EditorToolsPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer7 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<EditorToolsPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer
        /// </summary>
        public HBoxContainer L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer _L_HBoxContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer3
        /// </summary>
        public HBoxContainer3 L_HBoxContainer3
        {
            get
            {
                if (_L_HBoxContainer3 == null) _L_HBoxContainer3 = new HBoxContainer3(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer3"));
                return _L_HBoxContainer3;
            }
        }
        private HBoxContainer3 _L_HBoxContainer3;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer4
        /// </summary>
        public HBoxContainer4 L_HBoxContainer4
        {
            get
            {
                if (_L_HBoxContainer4 == null) _L_HBoxContainer4 = new HBoxContainer4(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer4"));
                return _L_HBoxContainer4;
            }
        }
        private HBoxContainer4 _L_HBoxContainer4;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer5
        /// </summary>
        public HBoxContainer5 L_HBoxContainer5
        {
            get
            {
                if (_L_HBoxContainer5 == null) _L_HBoxContainer5 = new HBoxContainer5(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer5"));
                return _L_HBoxContainer5;
            }
        }
        private HBoxContainer5 _L_HBoxContainer5;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.HBoxContainer7
        /// </summary>
        public HBoxContainer7 L_HBoxContainer7
        {
            get
            {
                if (_L_HBoxContainer7 == null) _L_HBoxContainer7 = new HBoxContainer7(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer7"));
                return _L_HBoxContainer7;
            }
        }
        private HBoxContainer7 _L_HBoxContainer7;

        public VBoxContainer(EditorToolsPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: EditorTools.ScrollContainer.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<EditorToolsPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.VBoxContainer
        /// </summary>
        public VBoxContainer L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer(UiPanel, Instance.GetNode<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer _L_VBoxContainer;

        public MarginContainer(EditorToolsPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: EditorTools.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<EditorToolsPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorTools.MarginContainer
        /// </summary>
        public MarginContainer L_MarginContainer
        {
            get
            {
                if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer(UiPanel, Instance.GetNode<Godot.MarginContainer>("MarginContainer"));
                return _L_MarginContainer;
            }
        }
        private MarginContainer _L_MarginContainer;

        public ScrollContainer(EditorToolsPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ConfirmationDialog"/>, 路径: EditorTools.Confirm
    /// </summary>
    public class Confirm : UiNode<EditorToolsPanel, Godot.ConfirmationDialog, Confirm>
    {
        public Confirm(EditorToolsPanel uiPanel, Godot.ConfirmationDialog node) : base(uiPanel, node) {  }
        public override Confirm Clone() => new (UiPanel, (Godot.ConfirmationDialog)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.AcceptDialog"/>, 路径: EditorTools.Tips
    /// </summary>
    public class Tips : UiNode<EditorToolsPanel, Godot.AcceptDialog, Tips>
    {
        public Tips(EditorToolsPanel uiPanel, Godot.AcceptDialog node) : base(uiPanel, node) {  }
        public override Tips Clone() => new (UiPanel, (Godot.AcceptDialog)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3.LineEdit
    /// </summary>
    public LineEdit S_LineEdit => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_LineEdit;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public HBoxContainer3 S_HBoxContainer3 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer4
    /// </summary>
    public HBoxContainer4 S_HBoxContainer4 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer4;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer5
    /// </summary>
    public HBoxContainer5 S_HBoxContainer5 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer5;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer.HBoxContainer7
    /// </summary>
    public HBoxContainer7 S_HBoxContainer7 => L_ScrollContainer.L_MarginContainer.L_VBoxContainer.L_HBoxContainer7;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_ScrollContainer.L_MarginContainer.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorTools.ScrollContainer.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_ScrollContainer.L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: EditorTools.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ConfirmationDialog"/>, 节点路径: EditorTools.Confirm
    /// </summary>
    public Confirm S_Confirm => L_Confirm;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.AcceptDialog"/>, 节点路径: EditorTools.Tips
    /// </summary>
    public Tips S_Tips => L_Tips;

}
