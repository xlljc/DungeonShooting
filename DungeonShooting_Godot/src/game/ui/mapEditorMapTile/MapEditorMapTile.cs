namespace UI.MapEditorMapTile;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorMapTile : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapTile.VBoxContainer
    /// </summary>
    public VBoxContainer L_VBoxContainer
    {
        get
        {
            if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer((MapEditorMapTilePanel)this, GetNode<Godot.VBoxContainer>("VBoxContainer"));
            return _L_VBoxContainer;
        }
    }
    private VBoxContainer _L_VBoxContainer;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: MapEditorMapTile.MaskBg
    /// </summary>
    public MaskBg L_MaskBg
    {
        get
        {
            if (_L_MaskBg == null) _L_MaskBg = new MaskBg((MapEditorMapTilePanel)this, GetNode<Godot.ColorRect>("MaskBg"));
            return _L_MaskBg;
        }
    }
    private MaskBg _L_MaskBg;


    public MapEditorMapTile() : base(nameof(MapEditorMapTile))
    {
    }

    public sealed override void OnInitNestedUi()
    {
        _ = L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab1;
        _ = L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab2;
        _ = L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab3;

    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapTile.VBoxContainer.HBoxContainer.Label
    /// </summary>
    public class Label : UiNode<MapEditorMapTilePanel, Godot.Label, Label>
    {
        public Label(MapEditorMapTilePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: MapEditorMapTile.VBoxContainer.HBoxContainer.SourceOption
    /// </summary>
    public class SourceOption : UiNode<MapEditorMapTilePanel, Godot.OptionButton, SourceOption>
    {
        public SourceOption(MapEditorMapTilePanel uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override SourceOption Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapTile.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorMapTilePanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapTile.VBoxContainer.Label
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorMapTile.VBoxContainer.SourceOption
        /// </summary>
        public SourceOption L_SourceOption
        {
            get
            {
                if (_L_SourceOption == null) _L_SourceOption = new SourceOption(UiPanel, Instance.GetNode<Godot.OptionButton>("SourceOption"));
                return _L_SourceOption;
            }
        }
        private SourceOption _L_SourceOption;

        public HBoxContainer(MapEditorMapTilePanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapTile.VBoxContainer.HBoxContainer2.Label
    /// </summary>
    public class Label_1 : UiNode<MapEditorMapTilePanel, Godot.Label, Label_1>
    {
        public Label_1(MapEditorMapTilePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_1 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: MapEditorMapTile.VBoxContainer.HBoxContainer2.HandleOption
    /// </summary>
    public class HandleOption : UiNode<MapEditorMapTilePanel, Godot.OptionButton, HandleOption>
    {
        public HandleOption(MapEditorMapTilePanel uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override HandleOption Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorMapTile.VBoxContainer.HBoxContainer2
    /// </summary>
    public class HBoxContainer2 : UiNode<MapEditorMapTilePanel, Godot.HBoxContainer, HBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapTile.VBoxContainer.Label
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorMapTile.VBoxContainer.HandleOption
        /// </summary>
        public HandleOption L_HandleOption
        {
            get
            {
                if (_L_HandleOption == null) _L_HandleOption = new HandleOption(UiPanel, Instance.GetNode<Godot.OptionButton>("HandleOption"));
                return _L_HandleOption;
            }
        }
        private HandleOption _L_HandleOption;

        public HBoxContainer2(MapEditorMapTilePanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer2 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.TabRoot.TileSprite
    /// </summary>
    public class TileSprite : UiNode<MapEditorMapTilePanel, Godot.Sprite2D, TileSprite>
    {
        public TileSprite(MapEditorMapTilePanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override TileSprite Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.TabRoot.Brush
    /// </summary>
    public class Brush : UiNode<MapEditorMapTilePanel, Godot.Control, Brush>
    {
        public Brush(MapEditorMapTilePanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override Brush Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.TabRoot
    /// </summary>
    public class TabRoot : UiNode<MapEditorMapTilePanel, Godot.Control, TabRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.TileSprite
        /// </summary>
        public TileSprite L_TileSprite
        {
            get
            {
                if (_L_TileSprite == null) _L_TileSprite = new TileSprite(UiPanel, Instance.GetNode<Godot.Sprite2D>("TileSprite"));
                return _L_TileSprite;
            }
        }
        private TileSprite _L_TileSprite;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.Brush
        /// </summary>
        public Brush L_Brush
        {
            get
            {
                if (_L_Brush == null) _L_Brush = new Brush(UiPanel, Instance.GetNode<Godot.Control>("Brush"));
                return _L_Brush;
            }
        }
        private Brush _L_Brush;

        public TabRoot(MapEditorMapTilePanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override TabRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.Grid
    /// </summary>
    public class Grid : UiNode<MapEditorMapTilePanel, Godot.ColorRect, Grid>
    {
        public Grid(MapEditorMapTilePanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.FocusBtn
    /// </summary>
    public class FocusBtn : UiNode<MapEditorMapTilePanel, Godot.TextureButton, FocusBtn>
    {
        public FocusBtn(MapEditorMapTilePanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorMapTile.FreeTileTab"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1
    /// </summary>
    public class Tab1 : UiNode<MapEditorMapTilePanel, UI.MapEditorMapTile.FreeTileTab, Tab1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.TabRoot
        /// </summary>
        public TabRoot L_TabRoot
        {
            get
            {
                if (_L_TabRoot == null) _L_TabRoot = new TabRoot(UiPanel, Instance.GetNode<Godot.Control>("TabRoot"));
                return _L_TabRoot;
            }
        }
        private TabRoot _L_TabRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Grid
        /// </summary>
        public Grid L_Grid
        {
            get
            {
                if (_L_Grid == null) _L_Grid = new Grid(UiPanel, Instance.GetNode<Godot.ColorRect>("Grid"));
                return _L_Grid;
            }
        }
        private Grid _L_Grid;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.FocusBtn
        /// </summary>
        public FocusBtn L_FocusBtn
        {
            get
            {
                if (_L_FocusBtn == null) _L_FocusBtn = new FocusBtn(UiPanel, Instance.GetNode<Godot.TextureButton>("FocusBtn"));
                return _L_FocusBtn;
            }
        }
        private FocusBtn _L_FocusBtn;

        public Tab1(MapEditorMapTilePanel uiPanel, UI.MapEditorMapTile.FreeTileTab node) : base(uiPanel, node) {  }
        public override Tab1 Clone() => new (UiPanel, (UI.MapEditorMapTile.FreeTileTab)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem.Select
    /// </summary>
    public class Select : UiNode<MapEditorMapTilePanel, Godot.NinePatchRect, Select>
    {
        public Select(MapEditorMapTilePanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override Select Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem.TerrainName
    /// </summary>
    public class TerrainName : UiNode<MapEditorMapTilePanel, Godot.Label, TerrainName>
    {
        public TerrainName(MapEditorMapTilePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override TerrainName Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem.ErrorIcon
    /// </summary>
    public class ErrorIcon : UiNode<MapEditorMapTilePanel, Godot.TextureRect, ErrorIcon>
    {
        public ErrorIcon(MapEditorMapTilePanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override ErrorIcon Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem.TerrainPreview
    /// </summary>
    public class TerrainPreview : UiNode<MapEditorMapTilePanel, Godot.TextureRect, TerrainPreview>
    {
        public TerrainPreview(MapEditorMapTilePanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TerrainPreview Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem
    /// </summary>
    public class TerrainItem : UiNode<MapEditorMapTilePanel, Godot.Button, TerrainItem>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.Select
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainName
        /// </summary>
        public TerrainName L_TerrainName
        {
            get
            {
                if (_L_TerrainName == null) _L_TerrainName = new TerrainName(UiPanel, Instance.GetNode<Godot.Label>("TerrainName"));
                return _L_TerrainName;
            }
        }
        private TerrainName _L_TerrainName;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.ErrorIcon
        /// </summary>
        public ErrorIcon L_ErrorIcon
        {
            get
            {
                if (_L_ErrorIcon == null) _L_ErrorIcon = new ErrorIcon(UiPanel, Instance.GetNode<Godot.TextureRect>("ErrorIcon"));
                return _L_ErrorIcon;
            }
        }
        private ErrorIcon _L_ErrorIcon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainPreview
        /// </summary>
        public TerrainPreview L_TerrainPreview
        {
            get
            {
                if (_L_TerrainPreview == null) _L_TerrainPreview = new TerrainPreview(UiPanel, Instance.GetNode<Godot.TextureRect>("TerrainPreview"));
                return _L_TerrainPreview;
            }
        }
        private TerrainPreview _L_TerrainPreview;

        public TerrainItem(MapEditorMapTilePanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override TerrainItem Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditorMapTilePanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.TerrainItem
        /// </summary>
        public TerrainItem L_TerrainItem
        {
            get
            {
                if (_L_TerrainItem == null) _L_TerrainItem = new TerrainItem(UiPanel, Instance.GetNode<Godot.Button>("TerrainItem"));
                return _L_TerrainItem;
            }
        }
        private TerrainItem _L_TerrainItem;

        public ScrollContainer(MapEditorMapTilePanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorMapTile.TerrainTileTab"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2
    /// </summary>
    public class Tab2 : UiNode<MapEditorMapTilePanel, UI.MapEditorMapTile.TerrainTileTab, Tab2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.ScrollContainer
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

        public Tab2(MapEditorMapTilePanel uiPanel, UI.MapEditorMapTile.TerrainTileTab node) : base(uiPanel, node) {  }
        public override Tab2 Clone() => new (UiPanel, (UI.MapEditorMapTile.TerrainTileTab)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.CellButton.PreviewImage
    /// </summary>
    public class PreviewImage : UiNode<MapEditorMapTilePanel, Godot.TextureRect, PreviewImage>
    {
        public PreviewImage(MapEditorMapTilePanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override PreviewImage Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.CellButton.CellName
    /// </summary>
    public class CellName : UiNode<MapEditorMapTilePanel, Godot.Label, CellName>
    {
        public CellName(MapEditorMapTilePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override CellName Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.CellButton.SelectTexture
    /// </summary>
    public class SelectTexture : UiNode<MapEditorMapTilePanel, Godot.NinePatchRect, SelectTexture>
    {
        public SelectTexture(MapEditorMapTilePanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override SelectTexture Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.CellButton
    /// </summary>
    public class CellButton : UiNode<MapEditorMapTilePanel, Godot.Button, CellButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.PreviewImage
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.CellName
        /// </summary>
        public CellName L_CellName
        {
            get
            {
                if (_L_CellName == null) _L_CellName = new CellName(UiPanel, Instance.GetNode<Godot.Label>("CellName"));
                return _L_CellName;
            }
        }
        private CellName _L_CellName;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.SelectTexture
        /// </summary>
        public SelectTexture L_SelectTexture
        {
            get
            {
                if (_L_SelectTexture == null) _L_SelectTexture = new SelectTexture(UiPanel, Instance.GetNode<Godot.NinePatchRect>("SelectTexture"));
                return _L_SelectTexture;
            }
        }
        private SelectTexture _L_SelectTexture;

        public CellButton(MapEditorMapTilePanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override CellButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer
    /// </summary>
    public class ScrollContainer_1 : UiNode<MapEditorMapTilePanel, Godot.ScrollContainer, ScrollContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.CellButton
        /// </summary>
        public CellButton L_CellButton
        {
            get
            {
                if (_L_CellButton == null) _L_CellButton = new CellButton(UiPanel, Instance.GetNode<Godot.Button>("CellButton"));
                return _L_CellButton;
            }
        }
        private CellButton _L_CellButton;

        public ScrollContainer_1(MapEditorMapTilePanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer_1 Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorMapTile.CombinationTileTab"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3
    /// </summary>
    public class Tab3 : UiNode<MapEditorMapTilePanel, UI.MapEditorMapTile.CombinationTileTab, Tab3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.ScrollContainer
        /// </summary>
        public ScrollContainer_1 L_ScrollContainer
        {
            get
            {
                if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer_1(UiPanel, Instance.GetNode<Godot.ScrollContainer>("ScrollContainer"));
                return _L_ScrollContainer;
            }
        }
        private ScrollContainer_1 _L_ScrollContainer;

        public Tab3(MapEditorMapTilePanel uiPanel, UI.MapEditorMapTile.CombinationTileTab node) : base(uiPanel, node) {  }
        public override Tab3 Clone() => new (UiPanel, (UI.MapEditorMapTile.CombinationTileTab)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorMapTilePanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorMapTile.FreeTileTab"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.Tab1
        /// </summary>
        public Tab1 L_Tab1
        {
            get
            {
                if (_L_Tab1 == null) _L_Tab1 = new Tab1(UiPanel, Instance.GetNode<UI.MapEditorMapTile.FreeTileTab>("Tab1"));
                return _L_Tab1;
            }
        }
        private Tab1 _L_Tab1;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorMapTile.TerrainTileTab"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.Tab2
        /// </summary>
        public Tab2 L_Tab2
        {
            get
            {
                if (_L_Tab2 == null) _L_Tab2 = new Tab2(UiPanel, Instance.GetNode<UI.MapEditorMapTile.TerrainTileTab>("Tab2"));
                return _L_Tab2;
            }
        }
        private Tab2 _L_Tab2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorMapTile.CombinationTileTab"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.Tab3
        /// </summary>
        public Tab3 L_Tab3
        {
            get
            {
                if (_L_Tab3 == null) _L_Tab3 = new Tab3(UiPanel, Instance.GetNode<UI.MapEditorMapTile.CombinationTileTab>("Tab3"));
                return _L_Tab3;
            }
        }
        private Tab3 _L_Tab3;

        public MarginContainer(MapEditorMapTilePanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditorMapTile.VBoxContainer.Panel
    /// </summary>
    public class Panel : UiNode<MapEditorMapTilePanel, Godot.Panel, Panel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorMapTile.VBoxContainer.MarginContainer
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

        public Panel(MapEditorMapTilePanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Panel Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorMapTile.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorMapTilePanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapTile.HBoxContainer
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapTile.HBoxContainer2
        /// </summary>
        public HBoxContainer2 L_HBoxContainer2
        {
            get
            {
                if (_L_HBoxContainer2 == null) _L_HBoxContainer2 = new HBoxContainer2(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer2"));
                return _L_HBoxContainer2;
            }
        }
        private HBoxContainer2 _L_HBoxContainer2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorMapTile.Panel
        /// </summary>
        public Panel L_Panel
        {
            get
            {
                if (_L_Panel == null) _L_Panel = new Panel(UiPanel, Instance.GetNode<Godot.Panel>("Panel"));
                return _L_Panel;
            }
        }
        private Panel _L_Panel;

        public VBoxContainer(MapEditorMapTilePanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorMapTile.MaskBg.Label
    /// </summary>
    public class Label_2 : UiNode<MapEditorMapTilePanel, Godot.Label, Label_2>
    {
        public Label_2(MapEditorMapTilePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: MapEditorMapTile.MaskBg
    /// </summary>
    public class MaskBg : UiNode<MapEditorMapTilePanel, Godot.ColorRect, MaskBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapTile.Label
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

        public MaskBg(MapEditorMapTilePanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override MaskBg Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorMapTile.VBoxContainer.HBoxContainer.SourceOption
    /// </summary>
    public SourceOption S_SourceOption => L_VBoxContainer.L_HBoxContainer.L_SourceOption;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapTile.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorMapTile.VBoxContainer.HBoxContainer2.HandleOption
    /// </summary>
    public HandleOption S_HandleOption => L_VBoxContainer.L_HBoxContainer2.L_HandleOption;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorMapTile.VBoxContainer.HBoxContainer2
    /// </summary>
    public HBoxContainer2 S_HBoxContainer2 => L_VBoxContainer.L_HBoxContainer2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.TabRoot.TileSprite
    /// </summary>
    public TileSprite S_TileSprite => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab1.L_TabRoot.L_TileSprite;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.TabRoot.Brush
    /// </summary>
    public Brush S_Brush => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab1.L_TabRoot.L_Brush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.TabRoot
    /// </summary>
    public TabRoot S_TabRoot => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab1.L_TabRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.Grid
    /// </summary>
    public Grid S_Grid => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab1.L_Grid;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1.FocusBtn
    /// </summary>
    public FocusBtn S_FocusBtn => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab1.L_FocusBtn;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorMapTile.FreeTileTab"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab1
    /// </summary>
    public Tab1 S_Tab1 => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab1;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem.Select
    /// </summary>
    public Select S_Select => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab2.L_ScrollContainer.L_TerrainItem.L_Select;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem.TerrainName
    /// </summary>
    public TerrainName S_TerrainName => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab2.L_ScrollContainer.L_TerrainItem.L_TerrainName;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem.ErrorIcon
    /// </summary>
    public ErrorIcon S_ErrorIcon => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab2.L_ScrollContainer.L_TerrainItem.L_ErrorIcon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem.TerrainPreview
    /// </summary>
    public TerrainPreview S_TerrainPreview => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab2.L_ScrollContainer.L_TerrainItem.L_TerrainPreview;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2.ScrollContainer.TerrainItem
    /// </summary>
    public TerrainItem S_TerrainItem => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab2.L_ScrollContainer.L_TerrainItem;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorMapTile.TerrainTileTab"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab2
    /// </summary>
    public Tab2 S_Tab2 => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.CellButton.PreviewImage
    /// </summary>
    public PreviewImage S_PreviewImage => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab3.L_ScrollContainer.L_CellButton.L_PreviewImage;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.CellButton.CellName
    /// </summary>
    public CellName S_CellName => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab3.L_ScrollContainer.L_CellButton.L_CellName;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.CellButton.SelectTexture
    /// </summary>
    public SelectTexture S_SelectTexture => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab3.L_ScrollContainer.L_CellButton.L_SelectTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3.ScrollContainer.CellButton
    /// </summary>
    public CellButton S_CellButton => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab3.L_ScrollContainer.L_CellButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorMapTile.CombinationTileTab"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer.Tab3
    /// </summary>
    public Tab3 S_Tab3 => L_VBoxContainer.L_Panel.L_MarginContainer.L_Tab3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_VBoxContainer.L_Panel.L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorMapTile.VBoxContainer.Panel
    /// </summary>
    public Panel S_Panel => L_VBoxContainer.L_Panel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorMapTile.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: MapEditorMapTile.MaskBg
    /// </summary>
    public MaskBg S_MaskBg => L_MaskBg;

}
