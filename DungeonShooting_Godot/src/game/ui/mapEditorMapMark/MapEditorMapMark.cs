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
            if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer((MapEditorMapMarkPanel)this, GetNode<Godot.VBoxContainer>("VBoxContainer"));
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
    public class MarkLabel : UiNode<MapEditorMapMarkPanel, Godot.Label, MarkLabel>
    {
        public MarkLabel(MapEditorMapMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override MarkLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer.PreinstallOption
    /// </summary>
    public class PreinstallOption : UiNode<MapEditorMapMarkPanel, Godot.OptionButton, PreinstallOption>
    {
        public PreinstallOption(MapEditorMapMarkPanel uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override PreinstallOption Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer.AddPreinstall
    /// </summary>
    public class AddPreinstall : UiNode<MapEditorMapMarkPanel, Godot.Button, AddPreinstall>
    {
        public AddPreinstall(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override AddPreinstall Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer.EditPreinstall
    /// </summary>
    public class EditPreinstall : UiNode<MapEditorMapMarkPanel, Godot.Button, EditPreinstall>
    {
        public EditPreinstall(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override EditPreinstall Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer.DeletePreinstall
    /// </summary>
    public class DeletePreinstall : UiNode<MapEditorMapMarkPanel, Godot.Button, DeletePreinstall>
    {
        public DeletePreinstall(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override DeletePreinstall Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorMapMarkPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorMapMark.VBoxContainer.PreinstallOption
        /// </summary>
        public PreinstallOption L_PreinstallOption
        {
            get
            {
                if (_L_PreinstallOption == null) _L_PreinstallOption = new PreinstallOption(UiPanel, Instance.GetNode<Godot.OptionButton>("PreinstallOption"));
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
                if (_L_AddPreinstall == null) _L_AddPreinstall = new AddPreinstall(UiPanel, Instance.GetNode<Godot.Button>("AddPreinstall"));
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
                if (_L_EditPreinstall == null) _L_EditPreinstall = new EditPreinstall(UiPanel, Instance.GetNode<Godot.Button>("EditPreinstall"));
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
                if (_L_DeletePreinstall == null) _L_DeletePreinstall = new DeletePreinstall(UiPanel, Instance.GetNode<Godot.Button>("DeletePreinstall"));
                return _L_DeletePreinstall;
            }
        }
        private DeletePreinstall _L_DeletePreinstall;

        public HBoxContainer(MapEditorMapMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapMark.VBoxContainer.MarkLabel2
    /// </summary>
    public class MarkLabel2 : UiNode<MapEditorMapMarkPanel, Godot.Label, MarkLabel2>
    {
        public MarkLabel2(MapEditorMapMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override MarkLabel2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.DynamicTool.EditButton
    /// </summary>
    public class EditButton : UiNode<MapEditorMapMarkPanel, Godot.Button, EditButton>
    {
        public EditButton(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override EditButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.DynamicTool.DeleteButton
    /// </summary>
    public class DeleteButton : UiNode<MapEditorMapMarkPanel, Godot.Button, DeleteButton>
    {
        public DeleteButton(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override DeleteButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.DynamicTool
    /// </summary>
    public class DynamicTool : UiNode<MapEditorMapMarkPanel, Godot.HBoxContainer, DynamicTool>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.EditButton
        /// </summary>
        public EditButton L_EditButton
        {
            get
            {
                if (_L_EditButton == null) _L_EditButton = new EditButton(UiPanel, Instance.GetNode<Godot.Button>("EditButton"));
                return _L_EditButton;
            }
        }
        private EditButton _L_EditButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.DeleteButton
        /// </summary>
        public DeleteButton L_DeleteButton
        {
            get
            {
                if (_L_DeleteButton == null) _L_DeleteButton = new DeleteButton(UiPanel, Instance.GetNode<Godot.Button>("DeleteButton"));
                return _L_DeleteButton;
            }
        }
        private DeleteButton _L_DeleteButton;

        public DynamicTool(MapEditorMapMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override DynamicTool Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.AddWaveButton
    /// </summary>
    public class AddWaveButton : UiNode<MapEditorMapMarkPanel, Godot.Button, AddWaveButton>
    {
        public AddWaveButton(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override AddWaveButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer.TextureButton
    /// </summary>
    public class TextureButton : UiNode<MapEditorMapMarkPanel, Godot.TextureButton, TextureButton>
    {
        public TextureButton(MapEditorMapMarkPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override TextureButton Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer.WaveButton.Select
    /// </summary>
    public class Select : UiNode<MapEditorMapMarkPanel, Godot.NinePatchRect, Select>
    {
        public Select(MapEditorMapMarkPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override Select Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer.WaveButton
    /// </summary>
    public class WaveButton : UiNode<MapEditorMapMarkPanel, Godot.Button, WaveButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer.Select
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

        public WaveButton(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override WaveButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer.WaveVisibleButton
    /// </summary>
    public class WaveVisibleButton : UiNode<MapEditorMapMarkPanel, Godot.Button, WaveVisibleButton>
    {
        public WaveVisibleButton(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override WaveVisibleButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer
    /// </summary>
    public class WaveContainer : UiNode<MapEditorMapMarkPanel, Godot.HBoxContainer, WaveContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.TextureButton
        /// </summary>
        public TextureButton L_TextureButton
        {
            get
            {
                if (_L_TextureButton == null) _L_TextureButton = new TextureButton(UiPanel, Instance.GetNode<Godot.TextureButton>("TextureButton"));
                return _L_TextureButton;
            }
        }
        private TextureButton _L_TextureButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveButton
        /// </summary>
        public WaveButton L_WaveButton
        {
            get
            {
                if (_L_WaveButton == null) _L_WaveButton = new WaveButton(UiPanel, Instance.GetNode<Godot.Button>("WaveButton"));
                return _L_WaveButton;
            }
        }
        private WaveButton _L_WaveButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveVisibleButton
        /// </summary>
        public WaveVisibleButton L_WaveVisibleButton
        {
            get
            {
                if (_L_WaveVisibleButton == null) _L_WaveVisibleButton = new WaveVisibleButton(UiPanel, Instance.GetNode<Godot.Button>("WaveVisibleButton"));
                return _L_WaveVisibleButton;
            }
        }
        private WaveVisibleButton _L_WaveVisibleButton;

        public WaveContainer(MapEditorMapMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override WaveContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarginContainer.AddMarkButton
    /// </summary>
    public class AddMarkButton : UiNode<MapEditorMapMarkPanel, Godot.Button, AddMarkButton>
    {
        public AddMarkButton(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override AddMarkButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorMapMarkPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.AddMarkButton
        /// </summary>
        public AddMarkButton L_AddMarkButton
        {
            get
            {
                if (_L_AddMarkButton == null) _L_AddMarkButton = new AddMarkButton(UiPanel, Instance.GetNode<Godot.Button>("AddMarkButton"));
                return _L_AddMarkButton;
            }
        }
        private AddMarkButton _L_AddMarkButton;

        public MarginContainer(MapEditorMapMarkPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkItem.MarkButton.MarkIcon
    /// </summary>
    public class MarkIcon : UiNode<MapEditorMapMarkPanel, Godot.TextureRect, MarkIcon>
    {
        public MarkIcon(MapEditorMapMarkPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override MarkIcon Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkItem.MarkButton.Select
    /// </summary>
    public class Select_1 : UiNode<MapEditorMapMarkPanel, Godot.NinePatchRect, Select_1>
    {
        public Select_1(MapEditorMapMarkPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override Select_1 Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkItem.MarkButton
    /// </summary>
    public class MarkButton : UiNode<MapEditorMapMarkPanel, Godot.Button, MarkButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkItem.MarkIcon
        /// </summary>
        public MarkIcon L_MarkIcon
        {
            get
            {
                if (_L_MarkIcon == null) _L_MarkIcon = new MarkIcon(UiPanel, Instance.GetNode<Godot.TextureRect>("MarkIcon"));
                return _L_MarkIcon;
            }
        }
        private MarkIcon _L_MarkIcon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkItem.Select
        /// </summary>
        public Select_1 L_Select
        {
            get
            {
                if (_L_Select == null) _L_Select = new Select_1(UiPanel, Instance.GetNode<Godot.NinePatchRect>("Select"));
                return _L_Select;
            }
        }
        private Select_1 _L_Select;

        public MarkButton(MapEditorMapMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override MarkButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkItem
    /// </summary>
    public class MarkItem : UiNode<MapEditorMapMarkPanel, Godot.HBoxContainer, MarkItem>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkButton
        /// </summary>
        public MarkButton L_MarkButton
        {
            get
            {
                if (_L_MarkButton == null) _L_MarkButton = new MarkButton(UiPanel, Instance.GetNode<Godot.Button>("MarkButton"));
                return _L_MarkButton;
            }
        }
        private MarkButton _L_MarkButton;

        public MarkItem(MapEditorMapMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override MarkItem Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer
    /// </summary>
    public class MarkContainer : UiNode<MapEditorMapMarkPanel, Godot.MarginContainer, MarkContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkItem
        /// </summary>
        public MarkItem L_MarkItem
        {
            get
            {
                if (_L_MarkItem == null) _L_MarkItem = new MarkItem(UiPanel, Instance.GetNode<Godot.HBoxContainer>("MarkItem"));
                return _L_MarkItem;
            }
        }
        private MarkItem _L_MarkItem;

        public MarkContainer(MapEditorMapMarkPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarkContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem
    /// </summary>
    public class WaveItem : UiNode<MapEditorMapMarkPanel, Godot.VBoxContainer, WaveItem>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveContainer
        /// </summary>
        public WaveContainer L_WaveContainer
        {
            get
            {
                if (_L_WaveContainer == null) _L_WaveContainer = new WaveContainer(UiPanel, Instance.GetNode<Godot.HBoxContainer>("WaveContainer"));
                return _L_WaveContainer;
            }
        }
        private WaveContainer _L_WaveContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.MarginContainer
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.MarkContainer
        /// </summary>
        public MarkContainer L_MarkContainer
        {
            get
            {
                if (_L_MarkContainer == null) _L_MarkContainer = new MarkContainer(UiPanel, Instance.GetNode<Godot.MarginContainer>("MarkContainer"));
                return _L_MarkContainer;
            }
        }
        private MarkContainer _L_MarkContainer;

        public WaveItem(MapEditorMapMarkPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override WaveItem Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer_1 : UiNode<MapEditorMapMarkPanel, Godot.VBoxContainer, VBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.AddWaveButton
        /// </summary>
        public AddWaveButton L_AddWaveButton
        {
            get
            {
                if (_L_AddWaveButton == null) _L_AddWaveButton = new AddWaveButton(UiPanel, Instance.GetNode<Godot.Button>("AddWaveButton"));
                return _L_AddWaveButton;
            }
        }
        private AddWaveButton _L_AddWaveButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.WaveItem
        /// </summary>
        public WaveItem L_WaveItem
        {
            get
            {
                if (_L_WaveItem == null) _L_WaveItem = new WaveItem(UiPanel, Instance.GetNode<Godot.VBoxContainer>("WaveItem"));
                return _L_WaveItem;
            }
        }
        private WaveItem _L_WaveItem;

        public VBoxContainer_1(MapEditorMapMarkPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer_1 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorMapMark.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditorMapMarkPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.VBoxContainer
        /// </summary>
        public VBoxContainer_1 L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer_1(UiPanel, Instance.GetNode<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer_1 _L_VBoxContainer;

        public ScrollContainer(MapEditorMapMarkPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorMapMark.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorMapMarkPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapMark.MarkLabel
        /// </summary>
        public MarkLabel L_MarkLabel
        {
            get
            {
                if (_L_MarkLabel == null) _L_MarkLabel = new MarkLabel(UiPanel, Instance.GetNode<Godot.Label>("MarkLabel"));
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
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer"));
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
                if (_L_MarkLabel2 == null) _L_MarkLabel2 = new MarkLabel2(UiPanel, Instance.GetNode<Godot.Label>("MarkLabel2"));
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
                if (_L_DynamicTool == null) _L_DynamicTool = new DynamicTool(UiPanel, Instance.GetNode<Godot.HBoxContainer>("DynamicTool"));
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
                if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer(UiPanel, Instance.GetNode<Godot.ScrollContainer>("ScrollContainer"));
                return _L_ScrollContainer;
            }
        }
        private ScrollContainer _L_ScrollContainer;

        public VBoxContainer(MapEditorMapMarkPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
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
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapMark.VBoxContainer.MarkLabel2
    /// </summary>
    public MarkLabel2 S_MarkLabel2 => L_VBoxContainer.L_MarkLabel2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.DynamicTool.EditButton
    /// </summary>
    public EditButton S_EditButton => L_VBoxContainer.L_DynamicTool.L_EditButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.DynamicTool.DeleteButton
    /// </summary>
    public DeleteButton S_DeleteButton => L_VBoxContainer.L_DynamicTool.L_DeleteButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.DynamicTool
    /// </summary>
    public DynamicTool S_DynamicTool => L_VBoxContainer.L_DynamicTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.AddWaveButton
    /// </summary>
    public AddWaveButton S_AddWaveButton => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_AddWaveButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer.TextureButton
    /// </summary>
    public TextureButton S_TextureButton => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_WaveContainer.L_TextureButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer.WaveButton
    /// </summary>
    public WaveButton S_WaveButton => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_WaveContainer.L_WaveButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer.WaveVisibleButton
    /// </summary>
    public WaveVisibleButton S_WaveVisibleButton => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_WaveContainer.L_WaveVisibleButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.WaveContainer
    /// </summary>
    public WaveContainer S_WaveContainer => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_WaveContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarginContainer.AddMarkButton
    /// </summary>
    public AddMarkButton S_AddMarkButton => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_MarginContainer.L_AddMarkButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkItem.MarkButton.MarkIcon
    /// </summary>
    public MarkIcon S_MarkIcon => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_MarkContainer.L_MarkItem.L_MarkButton.L_MarkIcon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkItem.MarkButton
    /// </summary>
    public MarkButton S_MarkButton => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_MarkContainer.L_MarkItem.L_MarkButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer.MarkItem
    /// </summary>
    public MarkItem S_MarkItem => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_MarkContainer.L_MarkItem;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem.MarkContainer
    /// </summary>
    public MarkContainer S_MarkContainer => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem.L_MarkContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer.VBoxContainer.WaveItem
    /// </summary>
    public WaveItem S_WaveItem => L_VBoxContainer.L_ScrollContainer.L_VBoxContainer.L_WaveItem;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorMapMark.VBoxContainer.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_VBoxContainer.L_ScrollContainer;

}
