namespace UI.MapEditorMapMark;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorMapMark : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer
    /// </summary>
    public VBoxContainer L_VBoxContainer
    {
        get
        {
            if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer(this, GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
            return _L_VBoxContainer;
        }
    }
    private VBoxContainer _L_VBoxContainer;


    public MapEditorMapMark() : base(nameof(MapEditorMapMark))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapMark.VBoxContainer.MarkLabel
    /// </summary>
    public class MarkLabel : UiNode<MapEditorMapMark, Godot.Label, MarkLabel>
    {
        public MarkLabel(MapEditorMapMark uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override MarkLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer.PreinstallOption
    /// </summary>
    public class PreinstallOption : UiNode<MapEditorMapMark, Godot.OptionButton, PreinstallOption>
    {
        public PreinstallOption(MapEditorMapMark uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override PreinstallOption Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer.AddPreinstall
    /// </summary>
    public class AddPreinstall : UiNode<MapEditorMapMark, Godot.Button, AddPreinstall>
    {
        public AddPreinstall(MapEditorMapMark uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override AddPreinstall Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer.EditPreinstall
    /// </summary>
    public class EditPreinstall : UiNode<MapEditorMapMark, Godot.Button, EditPreinstall>
    {
        public EditPreinstall(MapEditorMapMark uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override EditPreinstall Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer.DeletePreinstall
    /// </summary>
    public class DeletePreinstall : UiNode<MapEditorMapMark, Godot.Button, DeletePreinstall>
    {
        public DeletePreinstall(MapEditorMapMark uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override DeletePreinstall Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorMapMark, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorMapMark.VBoxContainer.PreinstallOption
        /// </summary>
        public PreinstallOption L_PreinstallOption
        {
            get
            {
                if (_L_PreinstallOption == null) _L_PreinstallOption = new PreinstallOption(UiPanel, Instance.GetNodeOrNull<Godot.OptionButton>("PreinstallOption"));
                return _L_PreinstallOption;
            }
        }
        private PreinstallOption _L_PreinstallOption;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.AddPreinstall
        /// </summary>
        public AddPreinstall L_AddPreinstall
        {
            get
            {
                if (_L_AddPreinstall == null) _L_AddPreinstall = new AddPreinstall(UiPanel, Instance.GetNodeOrNull<Godot.Button>("AddPreinstall"));
                return _L_AddPreinstall;
            }
        }
        private AddPreinstall _L_AddPreinstall;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.EditPreinstall
        /// </summary>
        public EditPreinstall L_EditPreinstall
        {
            get
            {
                if (_L_EditPreinstall == null) _L_EditPreinstall = new EditPreinstall(UiPanel, Instance.GetNodeOrNull<Godot.Button>("EditPreinstall"));
                return _L_EditPreinstall;
            }
        }
        private EditPreinstall _L_EditPreinstall;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.DeletePreinstall
        /// </summary>
        public DeletePreinstall L_DeletePreinstall
        {
            get
            {
                if (_L_DeletePreinstall == null) _L_DeletePreinstall = new DeletePreinstall(UiPanel, Instance.GetNodeOrNull<Godot.Button>("DeletePreinstall"));
                return _L_DeletePreinstall;
            }
        }
        private DeletePreinstall _L_DeletePreinstall;

        public HBoxContainer(MapEditorMapMark uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapMark.VBoxContainer.MarkLabel2
    /// </summary>
    public class MarkLabel2 : UiNode<MapEditorMapMark, Godot.Label, MarkLabel2>
    {
        public MarkLabel2(MapEditorMapMark uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override MarkLabel2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.DynamicTool.AddMark
    /// </summary>
    public class AddMark : UiNode<MapEditorMapMark, Godot.Button, AddMark>
    {
        public AddMark(MapEditorMapMark uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override AddMark Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.DynamicTool.EditMark
    /// </summary>
    public class EditMark : UiNode<MapEditorMapMark, Godot.Button, EditMark>
    {
        public EditMark(MapEditorMapMark uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override EditMark Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.DynamicTool.DeleteMark
    /// </summary>
    public class DeleteMark : UiNode<MapEditorMapMark, Godot.Button, DeleteMark>
    {
        public DeleteMark(MapEditorMapMark uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override DeleteMark Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.DynamicTool
    /// </summary>
    public class DynamicTool : UiNode<MapEditorMapMark, Godot.HBoxContainer, DynamicTool>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.AddMark
        /// </summary>
        public AddMark L_AddMark
        {
            get
            {
                if (_L_AddMark == null) _L_AddMark = new AddMark(UiPanel, Instance.GetNodeOrNull<Godot.Button>("AddMark"));
                return _L_AddMark;
            }
        }
        private AddMark _L_AddMark;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.EditMark
        /// </summary>
        public EditMark L_EditMark
        {
            get
            {
                if (_L_EditMark == null) _L_EditMark = new EditMark(UiPanel, Instance.GetNodeOrNull<Godot.Button>("EditMark"));
                return _L_EditMark;
            }
        }
        private EditMark _L_EditMark;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.DeleteMark
        /// </summary>
        public DeleteMark L_DeleteMark
        {
            get
            {
                if (_L_DeleteMark == null) _L_DeleteMark = new DeleteMark(UiPanel, Instance.GetNodeOrNull<Godot.Button>("DeleteMark"));
                return _L_DeleteMark;
            }
        }
        private DeleteMark _L_DeleteMark;

        public DynamicTool(MapEditorMapMark uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override DynamicTool Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.HBoxContainer.TextureButton
    /// </summary>
    public class TextureButton : UiNode<MapEditorMapMark, Godot.TextureButton, TextureButton>
    {
        public TextureButton(MapEditorMapMark uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override TextureButton Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.HBoxContainer.WaveButton
    /// </summary>
    public class WaveButton : UiNode<MapEditorMapMark, Godot.Button, WaveButton>
    {
        public WaveButton(MapEditorMapMark uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override WaveButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.HBoxContainer
    /// </summary>
    public class HBoxContainer_1 : UiNode<MapEditorMapMark, Godot.HBoxContainer, HBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.TextureButton
        /// </summary>
        public TextureButton L_TextureButton
        {
            get
            {
                if (_L_TextureButton == null) _L_TextureButton = new TextureButton(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("TextureButton"));
                return _L_TextureButton;
            }
        }
        private TextureButton _L_TextureButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.WaveButton
        /// </summary>
        public WaveButton L_WaveButton
        {
            get
            {
                if (_L_WaveButton == null) _L_WaveButton = new WaveButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("WaveButton"));
                return _L_WaveButton;
            }
        }
        private WaveButton _L_WaveButton;

        public HBoxContainer_1(MapEditorMapMark uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer_1 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.MarginContainer.HBoxContainer.MarkButton
    /// </summary>
    public class MarkButton : UiNode<MapEditorMapMark, Godot.Button, MarkButton>
    {
        public MarkButton(MapEditorMapMark uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override MarkButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.MarginContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer_2 : UiNode<MapEditorMapMark, Godot.HBoxContainer, HBoxContainer_2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.MarginContainer.MarkButton
        /// </summary>
        public MarkButton L_MarkButton
        {
            get
            {
                if (_L_MarkButton == null) _L_MarkButton = new MarkButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("MarkButton"));
                return _L_MarkButton;
            }
        }
        private MarkButton _L_MarkButton;

        public HBoxContainer_2(MapEditorMapMark uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer_2 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorMapMark, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.HBoxContainer
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

        public MarginContainer(MapEditorMapMark uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate
    /// </summary>
    public class WaveTemplate : UiNode<MapEditorMapMark, Godot.VBoxContainer, WaveTemplate>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.HBoxContainer
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.MarginContainer
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

        public WaveTemplate(MapEditorMapMark uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override WaveTemplate Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer_1 : UiNode<MapEditorMapMark, Godot.VBoxContainer, VBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.WaveTemplate
        /// </summary>
        public WaveTemplate L_WaveTemplate
        {
            get
            {
                if (_L_WaveTemplate == null) _L_WaveTemplate = new WaveTemplate(UiPanel, Instance.GetNodeOrNull<Godot.VBoxContainer>("WaveTemplate"));
                return _L_WaveTemplate;
            }
        }
        private WaveTemplate _L_WaveTemplate;

        public VBoxContainer_1(MapEditorMapMark uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer_1 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditorMapMark, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.VBoxContainer
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

        public ScrollContainer(MapEditorMapMark uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorMapMark, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapMark.MarkLabel
        /// </summary>
        public MarkLabel L_MarkLabel
        {
            get
            {
                if (_L_MarkLabel == null) _L_MarkLabel = new MarkLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("MarkLabel"));
                return _L_MarkLabel;
            }
        }
        private MarkLabel _L_MarkLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.HBoxContainer
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapMark.MarkLabel2
        /// </summary>
        public MarkLabel2 L_MarkLabel2
        {
            get
            {
                if (_L_MarkLabel2 == null) _L_MarkLabel2 = new MarkLabel2(UiPanel, Instance.GetNodeOrNull<Godot.Label>("MarkLabel2"));
                return _L_MarkLabel2;
            }
        }
        private MarkLabel2 _L_MarkLabel2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.DynamicTool
        /// </summary>
        public DynamicTool L_DynamicTool
        {
            get
            {
                if (_L_DynamicTool == null) _L_DynamicTool = new DynamicTool(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("DynamicTool"));
                return _L_DynamicTool;
            }
        }
        private DynamicTool _L_DynamicTool;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorMapMark.ScrollContainer
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

        public VBoxContainer(MapEditorMapMark uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapMark.VBoxContainer.MarkLabel
    /// </summary>
    public MarkLabel S_MarkLabel => L_VBoxContainer.L_MarkLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorMapMark.VBoxContainer.HBoxContainer.PreinstallOption
    /// </summary>
    public PreinstallOption S_PreinstallOption => L_VBoxContainer.L_HBoxContainer.L_PreinstallOption;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.HBoxContainer.AddPreinstall
    /// </summary>
    public AddPreinstall S_AddPreinstall => L_VBoxContainer.L_HBoxContainer.L_AddPreinstall;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.HBoxContainer.EditPreinstall
    /// </summary>
    public EditPreinstall S_EditPreinstall => L_VBoxContainer.L_HBoxContainer.L_EditPreinstall;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.HBoxContainer.DeletePreinstall
    /// </summary>
    public DeletePreinstall S_DeletePreinstall => L_VBoxContainer.L_HBoxContainer.L_DeletePreinstall;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapMark.VBoxContainer.MarkLabel2
    /// </summary>
    public MarkLabel2 S_MarkLabel2 => L_VBoxContainer.L_MarkLabel2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.DynamicTool.AddMark
    /// </summary>
    public AddMark S_AddMark => L_VBoxContainer.L_DynamicTool.L_AddMark;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.DynamicTool.EditMark
    /// </summary>
    public EditMark S_EditMark => L_VBoxContainer.L_DynamicTool.L_EditMark;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.DynamicTool.DeleteMark
    /// </summary>
    public DeleteMark S_DeleteMark => L_VBoxContainer.L_DynamicTool.L_DeleteMark;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.DynamicTool
    /// </summary>
    public DynamicTool S_DynamicTool => L_VBoxContainer.L_DynamicTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.HBoxContainer.TextureButton
    /// </summary>
    public TextureButton S_TextureButton => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveTemplate.L_HBoxContainer.L_TextureButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.HBoxContainer.WaveButton
    /// </summary>
    public WaveButton S_WaveButton => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveTemplate.L_HBoxContainer.L_WaveButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.MarginContainer.HBoxContainer.MarkButton
    /// </summary>
    public MarkButton S_MarkButton => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveTemplate.L_MarginContainer.L_HBoxContainer.L_MarkButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveTemplate.L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveTemplate
    /// </summary>
    public WaveTemplate S_WaveTemplate => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveTemplate;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_VBoxContainer.L_ScrollContainer;

}
