namespace UI.MapEditorProject;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorProject : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg(this, GetNodeOrNull<Godot.Panel>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;


    public MapEditorProject() : base(nameof(MapEditorProject))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorProject.Bg.VBoxContainer.Head.Back
    /// </summary>
    public class Back : UiNode<MapEditorProject, Godot.Button, Back>
    {
        public Back(MapEditorProject uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditorProject.Bg.VBoxContainer.Head
    /// </summary>
    public class Head : UiNode<MapEditorProject, Godot.Panel, Head>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.Back
        /// </summary>
        public Back L_Back
        {
            get
            {
                if (_L_Back == null) _L_Back = new Back(UiPanel, Instance.GetNodeOrNull<Godot.Button>("Back"));
                return _L_Back;
            }
        }
        private Back _L_Back;

        public Head(MapEditorProject uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Head Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.GroupSearchInput
    /// </summary>
    public class GroupSearchInput : UiNode<MapEditorProject, Godot.LineEdit, GroupSearchInput>
    {
        public GroupSearchInput(MapEditorProject uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override GroupSearchInput Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.GroupSearchButton
    /// </summary>
    public class GroupSearchButton : UiNode<MapEditorProject, Godot.Button, GroupSearchButton>
    {
        public GroupSearchButton(MapEditorProject uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override GroupSearchButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.GroupAddButton
    /// </summary>
    public class GroupAddButton : UiNode<MapEditorProject, Godot.Button, GroupAddButton>
    {
        public GroupAddButton(MapEditorProject uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override GroupAddButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer_1 : UiNode<MapEditorProject, Godot.HBoxContainer, HBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.GroupSearchInput
        /// </summary>
        public GroupSearchInput L_GroupSearchInput
        {
            get
            {
                if (_L_GroupSearchInput == null) _L_GroupSearchInput = new GroupSearchInput(UiPanel, Instance.GetNodeOrNull<Godot.LineEdit>("GroupSearchInput"));
                return _L_GroupSearchInput;
            }
        }
        private GroupSearchInput _L_GroupSearchInput;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.GroupSearchButton
        /// </summary>
        public GroupSearchButton L_GroupSearchButton
        {
            get
            {
                if (_L_GroupSearchButton == null) _L_GroupSearchButton = new GroupSearchButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("GroupSearchButton"));
                return _L_GroupSearchButton;
            }
        }
        private GroupSearchButton _L_GroupSearchButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.GroupAddButton
        /// </summary>
        public GroupAddButton L_GroupAddButton
        {
            get
            {
                if (_L_GroupAddButton == null) _L_GroupAddButton = new GroupAddButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("GroupAddButton"));
                return _L_GroupAddButton;
            }
        }
        private GroupAddButton _L_GroupAddButton;

        public HBoxContainer_1(MapEditorProject uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer_1 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.GroupButton
    /// </summary>
    public class GroupButton : UiNode<MapEditorProject, Godot.Button, GroupButton>
    {
        public GroupButton(MapEditorProject uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override GroupButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditorProject, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.GroupButton
        /// </summary>
        public GroupButton L_GroupButton
        {
            get
            {
                if (_L_GroupButton == null) _L_GroupButton = new GroupButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("GroupButton"));
                return _L_GroupButton;
            }
        }
        private GroupButton _L_GroupButton;

        public ScrollContainer(MapEditorProject uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer_1 : UiNode<MapEditorProject, Godot.VBoxContainer, VBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.HBoxContainer
        /// </summary>
        public HBoxContainer_1 L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer_1(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer_1 _L_HBoxContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.ScrollContainer
        /// </summary>
        public ScrollContainer L_ScrollContainer
        {
            get
            {
                if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer(UiPanel, Instance.GetNodeOrNull<Godot.ScrollContainer>("ScrollContainer"));
                return _L_ScrollContainer;
            }
        }
        private ScrollContainer _L_ScrollContainer;

        public VBoxContainer_1(MapEditorProject uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer_1 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorProject, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.VBoxContainer
        /// </summary>
        public VBoxContainer_1 L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer_1(UiPanel, Instance.GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer_1 _L_VBoxContainer;

        public MarginContainer(MapEditorProject uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel
    /// </summary>
    public class Panel : UiNode<MapEditorProject, Godot.Panel, Panel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.MarginContainer
        /// </summary>
        public MarginContainer L_MarginContainer
        {
            get
            {
                if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer(UiPanel, Instance.GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
                return _L_MarginContainer;
            }
        }
        private MarginContainer _L_MarginContainer;

        public Panel(MapEditorProject uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Panel Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.HBoxContainer.RoomSearchInput
    /// </summary>
    public class RoomSearchInput : UiNode<MapEditorProject, Godot.LineEdit, RoomSearchInput>
    {
        public RoomSearchInput(MapEditorProject uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override RoomSearchInput Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.HBoxContainer.RoomTypeButton
    /// </summary>
    public class RoomTypeButton : UiNode<MapEditorProject, Godot.OptionButton, RoomTypeButton>
    {
        public RoomTypeButton(MapEditorProject uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override RoomTypeButton Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.HBoxContainer.RoomSearchButton
    /// </summary>
    public class RoomSearchButton : UiNode<MapEditorProject, Godot.Button, RoomSearchButton>
    {
        public RoomSearchButton(MapEditorProject uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override RoomSearchButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.HBoxContainer.RoomAddButton
    /// </summary>
    public class RoomAddButton : UiNode<MapEditorProject, Godot.Button, RoomAddButton>
    {
        public RoomAddButton(MapEditorProject uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override RoomAddButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer_2 : UiNode<MapEditorProject, Godot.HBoxContainer, HBoxContainer_2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.RoomSearchInput
        /// </summary>
        public RoomSearchInput L_RoomSearchInput
        {
            get
            {
                if (_L_RoomSearchInput == null) _L_RoomSearchInput = new RoomSearchInput(UiPanel, Instance.GetNodeOrNull<Godot.LineEdit>("RoomSearchInput"));
                return _L_RoomSearchInput;
            }
        }
        private RoomSearchInput _L_RoomSearchInput;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.RoomTypeButton
        /// </summary>
        public RoomTypeButton L_RoomTypeButton
        {
            get
            {
                if (_L_RoomTypeButton == null) _L_RoomTypeButton = new RoomTypeButton(UiPanel, Instance.GetNodeOrNull<Godot.OptionButton>("RoomTypeButton"));
                return _L_RoomTypeButton;
            }
        }
        private RoomTypeButton _L_RoomTypeButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.RoomSearchButton
        /// </summary>
        public RoomSearchButton L_RoomSearchButton
        {
            get
            {
                if (_L_RoomSearchButton == null) _L_RoomSearchButton = new RoomSearchButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("RoomSearchButton"));
                return _L_RoomSearchButton;
            }
        }
        private RoomSearchButton _L_RoomSearchButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.RoomAddButton
        /// </summary>
        public RoomAddButton L_RoomAddButton
        {
            get
            {
                if (_L_RoomAddButton == null) _L_RoomAddButton = new RoomAddButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("RoomAddButton"));
                return _L_RoomAddButton;
            }
        }
        private RoomAddButton _L_RoomAddButton;

        public HBoxContainer_2(MapEditorProject uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer_2 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomButton.PreviewImage
    /// </summary>
    public class PreviewImage : UiNode<MapEditorProject, Godot.TextureRect, PreviewImage>
    {
        public PreviewImage(MapEditorProject uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override PreviewImage Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomButton.RoomName
    /// </summary>
    public class RoomName : UiNode<MapEditorProject, Godot.Label, RoomName>
    {
        public RoomName(MapEditorProject uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override RoomName Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomButton.RoomType
    /// </summary>
    public class RoomType : UiNode<MapEditorProject, Godot.Label, RoomType>
    {
        public RoomType(MapEditorProject uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override RoomType Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomButton
    /// </summary>
    public class RoomButton : UiNode<MapEditorProject, Godot.Button, RoomButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.PreviewImage
        /// </summary>
        public PreviewImage L_PreviewImage
        {
            get
            {
                if (_L_PreviewImage == null) _L_PreviewImage = new PreviewImage(UiPanel, Instance.GetNodeOrNull<Godot.TextureRect>("PreviewImage"));
                return _L_PreviewImage;
            }
        }
        private PreviewImage _L_PreviewImage;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomName
        /// </summary>
        public RoomName L_RoomName
        {
            get
            {
                if (_L_RoomName == null) _L_RoomName = new RoomName(UiPanel, Instance.GetNodeOrNull<Godot.Label>("RoomName"));
                return _L_RoomName;
            }
        }
        private RoomName _L_RoomName;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomType
        /// </summary>
        public RoomType L_RoomType
        {
            get
            {
                if (_L_RoomType == null) _L_RoomType = new RoomType(UiPanel, Instance.GetNodeOrNull<Godot.Label>("RoomType"));
                return _L_RoomType;
            }
        }
        private RoomType _L_RoomType;

        public RoomButton(MapEditorProject uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override RoomButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer_1 : UiNode<MapEditorProject, Godot.ScrollContainer, ScrollContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.RoomButton
        /// </summary>
        public RoomButton L_RoomButton
        {
            get
            {
                if (_L_RoomButton == null) _L_RoomButton = new RoomButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("RoomButton"));
                return _L_RoomButton;
            }
        }
        private RoomButton _L_RoomButton;

        public ScrollContainer_1(MapEditorProject uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer_1 Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer_2 : UiNode<MapEditorProject, Godot.VBoxContainer, VBoxContainer_2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.HBoxContainer
        /// </summary>
        public HBoxContainer_2 L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer_2(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer_2 _L_HBoxContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer
        /// </summary>
        public ScrollContainer_1 L_ScrollContainer
        {
            get
            {
                if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer_1(UiPanel, Instance.GetNodeOrNull<Godot.ScrollContainer>("ScrollContainer"));
                return _L_ScrollContainer;
            }
        }
        private ScrollContainer_1 _L_ScrollContainer;

        public VBoxContainer_2(MapEditorProject uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer_2 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer
    /// </summary>
    public class MarginContainer_1 : UiNode<MapEditorProject, Godot.MarginContainer, MarginContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.VBoxContainer
        /// </summary>
        public VBoxContainer_2 L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer_2(UiPanel, Instance.GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer_2 _L_VBoxContainer;

        public MarginContainer_1(MapEditorProject uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_1 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2
    /// </summary>
    public class Panel2 : UiNode<MapEditorProject, Godot.Panel, Panel2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.MarginContainer
        /// </summary>
        public MarginContainer_1 L_MarginContainer
        {
            get
            {
                if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer_1(UiPanel, Instance.GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
                return _L_MarginContainer;
            }
        }
        private MarginContainer_1 _L_MarginContainer;

        public Panel2(MapEditorProject uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Panel2 Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorProject, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.Panel
        /// </summary>
        public Panel L_Panel
        {
            get
            {
                if (_L_Panel == null) _L_Panel = new Panel(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Panel"));
                return _L_Panel;
            }
        }
        private Panel _L_Panel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.Panel2
        /// </summary>
        public Panel2 L_Panel2
        {
            get
            {
                if (_L_Panel2 == null) _L_Panel2 = new Panel2(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Panel2"));
                return _L_Panel2;
            }
        }
        private Panel2 _L_Panel2;

        public HBoxContainer(MapEditorProject uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorProject, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg.Head
        /// </summary>
        public Head L_Head
        {
            get
            {
                if (_L_Head == null) _L_Head = new Head(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Head"));
                return _L_Head;
            }
        }
        private Head _L_Head;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorProject.Bg.HBoxContainer
        /// </summary>
        public HBoxContainer L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer _L_HBoxContainer;

        public VBoxContainer(MapEditorProject uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditorProject.Bg
    /// </summary>
    public class Bg : UiNode<MapEditorProject, Godot.Panel, Bg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorProject.VBoxContainer
        /// </summary>
        public VBoxContainer L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer(UiPanel, Instance.GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer _L_VBoxContainer;

        public Bg(MapEditorProject uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.Head.Back
    /// </summary>
    public Back S_Back => L_Bg.L_VBoxContainer.L_Head.L_Back;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.Head
    /// </summary>
    public Head S_Head => L_Bg.L_VBoxContainer.L_Head;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.GroupSearchInput
    /// </summary>
    public GroupSearchInput S_GroupSearchInput => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_GroupSearchInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.GroupSearchButton
    /// </summary>
    public GroupSearchButton S_GroupSearchButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_GroupSearchButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.GroupAddButton
    /// </summary>
    public GroupAddButton S_GroupAddButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_GroupAddButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.GroupButton
    /// </summary>
    public GroupButton S_GroupButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_ScrollContainer.L_GroupButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel
    /// </summary>
    public Panel S_Panel => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.HBoxContainer.RoomSearchInput
    /// </summary>
    public RoomSearchInput S_RoomSearchInput => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_RoomSearchInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.HBoxContainer.RoomTypeButton
    /// </summary>
    public RoomTypeButton S_RoomTypeButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_RoomTypeButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.HBoxContainer.RoomSearchButton
    /// </summary>
    public RoomSearchButton S_RoomSearchButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_RoomSearchButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.HBoxContainer.RoomAddButton
    /// </summary>
    public RoomAddButton S_RoomAddButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_RoomAddButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomButton.PreviewImage
    /// </summary>
    public PreviewImage S_PreviewImage => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_VBoxContainer.L_ScrollContainer.L_RoomButton.L_PreviewImage;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomButton.RoomName
    /// </summary>
    public RoomName S_RoomName => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_VBoxContainer.L_ScrollContainer.L_RoomButton.L_RoomName;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomButton.RoomType
    /// </summary>
    public RoomType S_RoomType => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_VBoxContainer.L_ScrollContainer.L_RoomButton.L_RoomType;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.VBoxContainer.ScrollContainer.RoomButton
    /// </summary>
    public RoomButton S_RoomButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_VBoxContainer.L_ScrollContainer.L_RoomButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2
    /// </summary>
    public Panel2 S_Panel2 => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

}
