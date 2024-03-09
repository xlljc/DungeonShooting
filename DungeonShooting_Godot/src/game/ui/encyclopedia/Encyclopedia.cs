namespace UI.Encyclopedia;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Encyclopedia : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: Encyclopedia.Panel2
    /// </summary>
    public Panel2 L_Panel2
    {
        get
        {
            if (_L_Panel2 == null) _L_Panel2 = new Panel2((EncyclopediaPanel)this, GetNode<Godot.Panel>("Panel2"));
            return _L_Panel2;
        }
    }
    private Panel2 _L_Panel2;


    public Encyclopedia() : base(nameof(Encyclopedia))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.MarginContainer.HBoxContainer.LineEdit
    /// </summary>
    public class LineEdit : UiNode<EncyclopediaPanel, Godot.LineEdit, LineEdit>
    {
        public LineEdit(EncyclopediaPanel uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override LineEdit Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.MarginContainer.HBoxContainer.Search
    /// </summary>
    public class Search : UiNode<EncyclopediaPanel, Godot.Button, Search>
    {
        public Search(EncyclopediaPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Search Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.MarginContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<EncyclopediaPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.MarginContainer.LineEdit
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.MarginContainer.Search
        /// </summary>
        public Search L_Search
        {
            get
            {
                if (_L_Search == null) _L_Search = new Search(UiPanel, Instance.GetNode<Godot.Button>("Search"));
                return _L_Search;
            }
        }
        private Search _L_Search;

        public HBoxContainer(EncyclopediaPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.MarginContainer
    /// </summary>
    public class MarginContainer_1 : UiNode<EncyclopediaPanel, Godot.MarginContainer, MarginContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.HBoxContainer
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

        public MarginContainer_1(EncyclopediaPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_1 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.ObjectButton.PreviewImage
    /// </summary>
    public class PreviewImage : UiNode<EncyclopediaPanel, Godot.TextureRect, PreviewImage>
    {
        public PreviewImage(EncyclopediaPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override PreviewImage Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.ObjectButton.ObjectName
    /// </summary>
    public class ObjectName : UiNode<EncyclopediaPanel, Godot.Label, ObjectName>
    {
        public ObjectName(EncyclopediaPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override ObjectName Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.ObjectButton.Select
    /// </summary>
    public class Select : UiNode<EncyclopediaPanel, Godot.NinePatchRect, Select>
    {
        public Select(EncyclopediaPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override Select Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.ObjectButton
    /// </summary>
    public class ObjectButton : UiNode<EncyclopediaPanel, Godot.Button, ObjectButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.PreviewImage
        /// </summary>
        public PreviewImage L_PreviewImage
        {
            get
            {
                if (_L_PreviewImage == null) _L_PreviewImage = new PreviewImage(UiPanel, Instance.GetNode<Godot.TextureRect>("PreviewImage"));
                return _L_PreviewImage;
            }
        }
        private PreviewImage _L_PreviewImage;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.ObjectName
        /// </summary>
        public ObjectName L_ObjectName
        {
            get
            {
                if (_L_ObjectName == null) _L_ObjectName = new ObjectName(UiPanel, Instance.GetNode<Godot.Label>("ObjectName"));
                return _L_ObjectName;
            }
        }
        private ObjectName _L_ObjectName;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.Select
        /// </summary>
        public Select L_Select
        {
            get
            {
                if (_L_Select == null) _L_Select = new Select(UiPanel, Instance.GetNode<Godot.NinePatchRect>("Select"));
                return _L_Select;
            }
        }
        private Select _L_Select;

        public ObjectButton(EncyclopediaPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ObjectButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<EncyclopediaPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ObjectButton
        /// </summary>
        public ObjectButton L_ObjectButton
        {
            get
            {
                if (_L_ObjectButton == null) _L_ObjectButton = new ObjectButton(UiPanel, Instance.GetNode<Godot.Button>("ObjectButton"));
                return _L_ObjectButton;
            }
        }
        private ObjectButton _L_ObjectButton;

        public ScrollContainer(EncyclopediaPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2
    /// </summary>
    public class VBoxContainer2 : UiNode<EncyclopediaPanel, Godot.VBoxContainer, VBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.MarginContainer
        /// </summary>
        public MarginContainer_1 L_MarginContainer
        {
            get
            {
                if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer_1(UiPanel, Instance.GetNode<Godot.MarginContainer>("MarginContainer"));
                return _L_MarginContainer;
            }
        }
        private MarginContainer_1 _L_MarginContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.ScrollContainer
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

        public VBoxContainer2(EncyclopediaPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer2 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: Encyclopedia.Panel2.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<EncyclopediaPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Encyclopedia.Panel2.VBoxContainer2
        /// </summary>
        public VBoxContainer2 L_VBoxContainer2
        {
            get
            {
                if (_L_VBoxContainer2 == null) _L_VBoxContainer2 = new VBoxContainer2(UiPanel, Instance.GetNode<Godot.VBoxContainer>("VBoxContainer2"));
                return _L_VBoxContainer2;
            }
        }
        private VBoxContainer2 _L_VBoxContainer2;

        public MarginContainer(EncyclopediaPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: Encyclopedia.Panel2
    /// </summary>
    public class Panel2 : UiNode<EncyclopediaPanel, Godot.Panel, Panel2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: Encyclopedia.MarginContainer
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

        public Panel2(EncyclopediaPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Panel2 Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.MarginContainer.HBoxContainer.LineEdit
    /// </summary>
    public LineEdit S_LineEdit => L_Panel2.L_MarginContainer.L_VBoxContainer2.L_MarginContainer.L_HBoxContainer.L_LineEdit;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.MarginContainer.HBoxContainer.Search
    /// </summary>
    public Search S_Search => L_Panel2.L_MarginContainer.L_VBoxContainer2.L_MarginContainer.L_HBoxContainer.L_Search;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.MarginContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_Panel2.L_MarginContainer.L_VBoxContainer2.L_MarginContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.ObjectButton.PreviewImage
    /// </summary>
    public PreviewImage S_PreviewImage => L_Panel2.L_MarginContainer.L_VBoxContainer2.L_ScrollContainer.L_ObjectButton.L_PreviewImage;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.ObjectButton.ObjectName
    /// </summary>
    public ObjectName S_ObjectName => L_Panel2.L_MarginContainer.L_VBoxContainer2.L_ScrollContainer.L_ObjectButton.L_ObjectName;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.ObjectButton.Select
    /// </summary>
    public Select S_Select => L_Panel2.L_MarginContainer.L_VBoxContainer2.L_ScrollContainer.L_ObjectButton.L_Select;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer.ObjectButton
    /// </summary>
    public ObjectButton S_ObjectButton => L_Panel2.L_MarginContainer.L_VBoxContainer2.L_ScrollContainer.L_ObjectButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_Panel2.L_MarginContainer.L_VBoxContainer2.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Encyclopedia.Panel2.MarginContainer.VBoxContainer2
    /// </summary>
    public VBoxContainer2 S_VBoxContainer2 => L_Panel2.L_MarginContainer.L_VBoxContainer2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: Encyclopedia.Panel2
    /// </summary>
    public Panel2 S_Panel2 => L_Panel2;

}
