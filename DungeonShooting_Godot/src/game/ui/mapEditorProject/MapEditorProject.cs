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
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.Panel.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorProject, Godot.MarginContainer, MarginContainer>
    {
        public MarginContainer(MapEditorProject uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditorProject.Bg.VBoxContainer.Panel
    /// </summary>
    public class Panel : UiNode<MapEditorProject, Godot.Panel, Panel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.MarginContainer
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
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.ScrollContainer.GroupButton
    /// </summary>
    public class GroupButton : UiNode<MapEditorProject, Godot.Button, GroupButton>
    {
        public GroupButton(MapEditorProject uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override GroupButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditorProject, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.GroupButton
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
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer
    /// </summary>
    public class MarginContainer_1 : UiNode<MapEditorProject, Godot.MarginContainer, MarginContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.ScrollContainer
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

        public MarginContainer_1(MapEditorProject uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_1 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel
    /// </summary>
    public class Panel_1 : UiNode<MapEditorProject, Godot.Panel, Panel_1>
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

        public Panel_1(MapEditorProject uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Panel_1 Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomButton.PreviewImage
    /// </summary>
    public class PreviewImage : UiNode<MapEditorProject, Godot.TextureRect, PreviewImage>
    {
        public PreviewImage(MapEditorProject uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override PreviewImage Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomButton.RoomName
    /// </summary>
    public class RoomName : UiNode<MapEditorProject, Godot.Label, RoomName>
    {
        public RoomName(MapEditorProject uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override RoomName Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomButton.RoomType
    /// </summary>
    public class RoomType : UiNode<MapEditorProject, Godot.Label, RoomType>
    {
        public RoomType(MapEditorProject uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override RoomType Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomButton
    /// </summary>
    public class RoomButton : UiNode<MapEditorProject, Godot.TextureButton, RoomButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.PreviewImage
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomName
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomType
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

        public RoomButton(MapEditorProject uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override RoomButton Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer_1 : UiNode<MapEditorProject, Godot.ScrollContainer, ScrollContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.RoomButton
        /// </summary>
        public RoomButton L_RoomButton
        {
            get
            {
                if (_L_RoomButton == null) _L_RoomButton = new RoomButton(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("RoomButton"));
                return _L_RoomButton;
            }
        }
        private RoomButton _L_RoomButton;

        public ScrollContainer_1(MapEditorProject uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer_1 Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer
    /// </summary>
    public class MarginContainer_2 : UiNode<MapEditorProject, Godot.MarginContainer, MarginContainer_2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.ScrollContainer
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

        public MarginContainer_2(MapEditorProject uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_2 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2
    /// </summary>
    public class Panel2 : UiNode<MapEditorProject, Godot.Panel, Panel2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.MarginContainer
        /// </summary>
        public MarginContainer_2 L_MarginContainer
        {
            get
            {
                if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer_2(UiPanel, Instance.GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
                return _L_MarginContainer;
            }
        }
        private MarginContainer_2 _L_MarginContainer;

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
        public Panel_1 L_Panel
        {
            get
            {
                if (_L_Panel == null) _L_Panel = new Panel_1(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Panel"));
                return _L_Panel;
            }
        }
        private Panel_1 _L_Panel;

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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg.Panel
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
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.ScrollContainer.GroupButton
    /// </summary>
    public GroupButton S_GroupButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_ScrollContainer.L_GroupButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomButton.PreviewImage
    /// </summary>
    public PreviewImage S_PreviewImage => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_ScrollContainer.L_RoomButton.L_PreviewImage;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomButton.RoomName
    /// </summary>
    public RoomName S_RoomName => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_ScrollContainer.L_RoomButton.L_RoomName;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomButton.RoomType
    /// </summary>
    public RoomType S_RoomType => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_ScrollContainer.L_RoomButton.L_RoomType;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2.MarginContainer.ScrollContainer.RoomButton
    /// </summary>
    public RoomButton S_RoomButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2.L_MarginContainer.L_ScrollContainer.L_RoomButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer.Panel2
    /// </summary>
    public Panel2 S_Panel2 => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_Bg.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorProject.Bg.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_Bg.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorProject.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

}
