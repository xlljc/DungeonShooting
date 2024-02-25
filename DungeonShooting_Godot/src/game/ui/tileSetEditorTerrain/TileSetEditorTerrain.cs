namespace UI.TileSetEditorTerrain;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class TileSetEditorTerrain : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VSplitContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer
    /// </summary>
    public VSplitContainer L_VSplitContainer
    {
        get
        {
            if (_L_VSplitContainer == null) _L_VSplitContainer = new VSplitContainer((TileSetEditorTerrainPanel)this, GetNode<Godot.VSplitContainer>("VSplitContainer"));
            return _L_VSplitContainer;
        }
    }
    private VSplitContainer _L_VSplitContainer;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.DragSprite
    /// </summary>
    public DragSprite L_DragSprite
    {
        get
        {
            if (_L_DragSprite == null) _L_DragSprite = new DragSprite((TileSetEditorTerrainPanel)this, GetNode<Godot.Sprite2D>("DragSprite"));
            return _L_DragSprite;
        }
    }
    private DragSprite _L_DragSprite;


    public TileSetEditorTerrain() : base(nameof(TileSetEditorTerrain))
    {
    }

    public sealed override void OnInitNestedUi()
    {
        _ = L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg;
        _ = L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg.L_TerrainRoot.L_Brush;
        _ = L_VSplitContainer.L_PanelBottom.L_BottomBg;

    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.HBoxContainer.Label
    /// </summary>
    public class Label : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label>
    {
        public Label(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.HBoxContainer.AddButton
    /// </summary>
    public class AddButton : UiNode<TileSetEditorTerrainPanel, Godot.Button, AddButton>
    {
        public AddButton(TileSetEditorTerrainPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override AddButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.HBoxContainer.EditButton
    /// </summary>
    public class EditButton : UiNode<TileSetEditorTerrainPanel, Godot.Button, EditButton>
    {
        public EditButton(TileSetEditorTerrainPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override EditButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.HBoxContainer.DeleteButton
    /// </summary>
    public class DeleteButton : UiNode<TileSetEditorTerrainPanel, Godot.Button, DeleteButton>
    {
        public DeleteButton(TileSetEditorTerrainPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override DeleteButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<TileSetEditorTerrainPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.Label
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.AddButton
        /// </summary>
        public AddButton L_AddButton
        {
            get
            {
                if (_L_AddButton == null) _L_AddButton = new AddButton(UiPanel, Instance.GetNode<Godot.Button>("AddButton"));
                return _L_AddButton;
            }
        }
        private AddButton _L_AddButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.EditButton
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.DeleteButton
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

        public HBoxContainer(TileSetEditorTerrainPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer.TerrainTab.SelectTexture
    /// </summary>
    public class SelectTexture : UiNode<TileSetEditorTerrainPanel, Godot.NinePatchRect, SelectTexture>
    {
        public SelectTexture(TileSetEditorTerrainPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override SelectTexture Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer.TerrainTab.ErrorIcon
    /// </summary>
    public class ErrorIcon : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, ErrorIcon>
    {
        public ErrorIcon(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override ErrorIcon Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer.TerrainTab
    /// </summary>
    public class TerrainTab : UiNode<TileSetEditorTerrainPanel, Godot.Button, TerrainTab>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer.SelectTexture
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer.ErrorIcon
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

        public TerrainTab(TileSetEditorTerrainPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override TerrainTab Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<TileSetEditorTerrainPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.TerrainTab
        /// </summary>
        public TerrainTab L_TerrainTab
        {
            get
            {
                if (_L_TerrainTab == null) _L_TerrainTab = new TerrainTab(UiPanel, Instance.GetNode<Godot.Button>("TerrainTab"));
                return _L_TerrainTab;
            }
        }
        private TerrainTab _L_TerrainTab;

        public ScrollContainer(TileSetEditorTerrainPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<TileSetEditorTerrainPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.HBoxContainer
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.ScrollContainer
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

        public VBoxContainer(TileSetEditorTerrainPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab
    /// </summary>
    public class TopTab : UiNode<TileSetEditorTerrainPanel, Godot.Panel, TopTab>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.VBoxContainer
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

        public TopTab(TileSetEditorTerrainPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override TopTab Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.CellRoot.RightCell.CellTexture
    /// </summary>
    public class CellTexture : UiNode<TileSetEditorTerrainPanel, Godot.Sprite2D, CellTexture>
    {
        public CellTexture(TileSetEditorTerrainPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override CellTexture Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.CellRoot.RightCell
    /// </summary>
    public class RightCell : UiNode<TileSetEditorTerrainPanel, Godot.Control, RightCell>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.CellRoot.CellTexture
        /// </summary>
        public CellTexture L_CellTexture
        {
            get
            {
                if (_L_CellTexture == null) _L_CellTexture = new CellTexture(UiPanel, Instance.GetNode<Godot.Sprite2D>("CellTexture"));
                return _L_CellTexture;
            }
        }
        private CellTexture _L_CellTexture;

        public RightCell(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override RightCell Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.CellRoot
    /// </summary>
    public class CellRoot : UiNode<TileSetEditorTerrainPanel, Godot.Control, CellRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.RightCell
        /// </summary>
        public RightCell L_RightCell
        {
            get
            {
                if (_L_RightCell == null) _L_RightCell = new RightCell(UiPanel, Instance.GetNode<Godot.Control>("RightCell"));
                return _L_RightCell;
            }
        }
        private RightCell _L_RightCell;

        public CellRoot(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override CellRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture1.Label
    /// </summary>
    public class Label_1 : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label_1>
    {
        public Label_1(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_1 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture1
    /// </summary>
    public class TerrainTexture1 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.Label
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

        public TerrainTexture1(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TerrainTexture1 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture2.Label
    /// </summary>
    public class Label_2 : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label_2>
    {
        public Label_2(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture2
    /// </summary>
    public class TerrainTexture2 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.Label
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

        public TerrainTexture2(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TerrainTexture2 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture3.Label
    /// </summary>
    public class Label_3 : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label_3>
    {
        public Label_3(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_3 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture3
    /// </summary>
    public class TerrainTexture3 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.Label
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

        public TerrainTexture3(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TerrainTexture3 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture4.Label
    /// </summary>
    public class Label_4 : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label_4>
    {
        public Label_4(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_4 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture4
    /// </summary>
    public class TerrainTexture4 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture4>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.Label
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

        public TerrainTexture4(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TerrainTexture4 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.Brush
    /// </summary>
    public class Brush : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TerrainBrush, Brush>
    {
        public Brush(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TerrainBrush node) : base(uiPanel, node) {  }
        public override Brush Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TerrainBrush)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot
    /// </summary>
    public class TerrainRoot : UiNode<TileSetEditorTerrainPanel, Godot.Control, TerrainRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.CellRoot
        /// </summary>
        public CellRoot L_CellRoot
        {
            get
            {
                if (_L_CellRoot == null) _L_CellRoot = new CellRoot(UiPanel, Instance.GetNode<Godot.Control>("CellRoot"));
                return _L_CellRoot;
            }
        }
        private CellRoot _L_CellRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainTexture1
        /// </summary>
        public TerrainTexture1 L_TerrainTexture1
        {
            get
            {
                if (_L_TerrainTexture1 == null) _L_TerrainTexture1 = new TerrainTexture1(UiPanel, Instance.GetNode<Godot.TextureRect>("TerrainTexture1"));
                return _L_TerrainTexture1;
            }
        }
        private TerrainTexture1 _L_TerrainTexture1;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainTexture2
        /// </summary>
        public TerrainTexture2 L_TerrainTexture2
        {
            get
            {
                if (_L_TerrainTexture2 == null) _L_TerrainTexture2 = new TerrainTexture2(UiPanel, Instance.GetNode<Godot.TextureRect>("TerrainTexture2"));
                return _L_TerrainTexture2;
            }
        }
        private TerrainTexture2 _L_TerrainTexture2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainTexture3
        /// </summary>
        public TerrainTexture3 L_TerrainTexture3
        {
            get
            {
                if (_L_TerrainTexture3 == null) _L_TerrainTexture3 = new TerrainTexture3(UiPanel, Instance.GetNode<Godot.TextureRect>("TerrainTexture3"));
                return _L_TerrainTexture3;
            }
        }
        private TerrainTexture3 _L_TerrainTexture3;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainTexture4
        /// </summary>
        public TerrainTexture4 L_TerrainTexture4
        {
            get
            {
                if (_L_TerrainTexture4 == null) _L_TerrainTexture4 = new TerrainTexture4(UiPanel, Instance.GetNode<Godot.TextureRect>("TerrainTexture4"));
                return _L_TerrainTexture4;
            }
        }
        private TerrainTexture4 _L_TerrainTexture4;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.Brush
        /// </summary>
        public Brush L_Brush
        {
            get
            {
                if (_L_Brush == null) _L_Brush = new Brush(UiPanel, Instance.GetNode<UI.TileSetEditorTerrain.TerrainBrush>("Brush"));
                return _L_Brush;
            }
        }
        private Brush _L_Brush;

        public TerrainRoot(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override TerrainRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.Grid
    /// </summary>
    public class Grid : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, Grid>
    {
        public Grid(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.FocusBtn
    /// </summary>
    public class FocusBtn : UiNode<TileSetEditorTerrainPanel, Godot.TextureButton, FocusBtn>
    {
        public FocusBtn(TileSetEditorTerrainPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg
    /// </summary>
    public class TopBg : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TileEditTerrain, TopBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TerrainRoot
        /// </summary>
        public TerrainRoot L_TerrainRoot
        {
            get
            {
                if (_L_TerrainRoot == null) _L_TerrainRoot = new TerrainRoot(UiPanel, Instance.GetNode<Godot.Control>("TerrainRoot"));
                return _L_TerrainRoot;
            }
        }
        private TerrainRoot _L_TerrainRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.Grid
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.FocusBtn
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

        public TopBg(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TileEditTerrain node) : base(uiPanel, node) {  }
        public override TopBg Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TileEditTerrain)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HSplitContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer
    /// </summary>
    public class HSplitContainer : UiNode<TileSetEditorTerrainPanel, Godot.HSplitContainer, HSplitContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.TopTab
        /// </summary>
        public TopTab L_TopTab
        {
            get
            {
                if (_L_TopTab == null) _L_TopTab = new TopTab(UiPanel, Instance.GetNode<Godot.Panel>("TopTab"));
                return _L_TopTab;
            }
        }
        private TopTab _L_TopTab;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.TopBg
        /// </summary>
        public TopBg L_TopBg
        {
            get
            {
                if (_L_TopBg == null) _L_TopBg = new TopBg(UiPanel, Instance.GetNode<UI.TileSetEditorTerrain.TileEditTerrain>("TopBg"));
                return _L_TopBg;
            }
        }
        private TopBg _L_TopBg;

        public HSplitContainer(TileSetEditorTerrainPanel uiPanel, Godot.HSplitContainer node) : base(uiPanel, node) {  }
        public override HSplitContainer Clone() => new (UiPanel, (Godot.HSplitContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop
    /// </summary>
    public class PanelTop : UiNode<TileSetEditorTerrainPanel, Godot.Panel, PanelTop>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.HSplitContainer
        /// </summary>
        public HSplitContainer L_HSplitContainer
        {
            get
            {
                if (_L_HSplitContainer == null) _L_HSplitContainer = new HSplitContainer(UiPanel, Instance.GetNode<Godot.HSplitContainer>("HSplitContainer"));
                return _L_HSplitContainer;
            }
        }
        private HSplitContainer _L_HSplitContainer;

        public PanelTop(TileSetEditorTerrainPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override PanelTop Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.TileTexture.CellRoot.BottomCell
    /// </summary>
    public class BottomCell : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, BottomCell>
    {
        public BottomCell(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override BottomCell Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.TileTexture.CellRoot
    /// </summary>
    public class CellRoot_1 : UiNode<TileSetEditorTerrainPanel, Godot.Control, CellRoot_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.TileTexture.BottomCell
        /// </summary>
        public BottomCell L_BottomCell
        {
            get
            {
                if (_L_BottomCell == null) _L_BottomCell = new BottomCell(UiPanel, Instance.GetNode<Godot.ColorRect>("BottomCell"));
                return _L_BottomCell;
            }
        }
        private BottomCell _L_BottomCell;

        public CellRoot_1(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override CellRoot_1 Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="EditorMaskBrush"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.TileTexture.MaskBrush
    /// </summary>
    public class MaskBrush : UiNode<TileSetEditorTerrainPanel, EditorMaskBrush, MaskBrush>
    {
        public MaskBrush(TileSetEditorTerrainPanel uiPanel, EditorMaskBrush node) : base(uiPanel, node) {  }
        public override MaskBrush Clone() => new (UiPanel, (EditorMaskBrush)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.TileTexture
    /// </summary>
    public class TileTexture : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TileTexture>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.CellRoot
        /// </summary>
        public CellRoot_1 L_CellRoot
        {
            get
            {
                if (_L_CellRoot == null) _L_CellRoot = new CellRoot_1(UiPanel, Instance.GetNode<Godot.Control>("CellRoot"));
                return _L_CellRoot;
            }
        }
        private CellRoot_1 _L_CellRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="EditorMaskBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.MaskBrush
        /// </summary>
        public MaskBrush L_MaskBrush
        {
            get
            {
                if (_L_MaskBrush == null) _L_MaskBrush = new MaskBrush(UiPanel, Instance.GetNode<EditorMaskBrush>("MaskBrush"));
                return _L_MaskBrush;
            }
        }
        private MaskBrush _L_MaskBrush;

        public TileTexture(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TileTexture Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.Grid
    /// </summary>
    public class Grid_1 : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, Grid_1>
    {
        public Grid_1(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid_1 Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.FocusBtn
    /// </summary>
    public class FocusBtn_1 : UiNode<TileSetEditorTerrainPanel, Godot.TextureButton, FocusBtn_1>
    {
        public FocusBtn_1(TileSetEditorTerrainPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn_1 Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg
    /// </summary>
    public class BottomBg : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TileEditArea, BottomBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.TileTexture
        /// </summary>
        public TileTexture L_TileTexture
        {
            get
            {
                if (_L_TileTexture == null) _L_TileTexture = new TileTexture(UiPanel, Instance.GetNode<Godot.TextureRect>("TileTexture"));
                return _L_TileTexture;
            }
        }
        private TileTexture _L_TileTexture;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.Grid
        /// </summary>
        public Grid_1 L_Grid
        {
            get
            {
                if (_L_Grid == null) _L_Grid = new Grid_1(UiPanel, Instance.GetNode<Godot.ColorRect>("Grid"));
                return _L_Grid;
            }
        }
        private Grid_1 _L_Grid;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.FocusBtn
        /// </summary>
        public FocusBtn_1 L_FocusBtn
        {
            get
            {
                if (_L_FocusBtn == null) _L_FocusBtn = new FocusBtn_1(UiPanel, Instance.GetNode<Godot.TextureButton>("FocusBtn"));
                return _L_FocusBtn;
            }
        }
        private FocusBtn_1 _L_FocusBtn;

        public BottomBg(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TileEditArea node) : base(uiPanel, node) {  }
        public override BottomBg Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TileEditArea)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom
    /// </summary>
    public class PanelBottom : UiNode<TileSetEditorTerrainPanel, Godot.Panel, PanelBottom>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.BottomBg
        /// </summary>
        public BottomBg L_BottomBg
        {
            get
            {
                if (_L_BottomBg == null) _L_BottomBg = new BottomBg(UiPanel, Instance.GetNode<UI.TileSetEditorTerrain.TileEditArea>("BottomBg"));
                return _L_BottomBg;
            }
        }
        private BottomBg _L_BottomBg;

        public PanelBottom(TileSetEditorTerrainPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override PanelBottom Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VSplitContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer
    /// </summary>
    public class VSplitContainer : UiNode<TileSetEditorTerrainPanel, Godot.VSplitContainer, VSplitContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.PanelTop
        /// </summary>
        public PanelTop L_PanelTop
        {
            get
            {
                if (_L_PanelTop == null) _L_PanelTop = new PanelTop(UiPanel, Instance.GetNode<Godot.Panel>("PanelTop"));
                return _L_PanelTop;
            }
        }
        private PanelTop _L_PanelTop;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.PanelBottom
        /// </summary>
        public PanelBottom L_PanelBottom
        {
            get
            {
                if (_L_PanelBottom == null) _L_PanelBottom = new PanelBottom(UiPanel, Instance.GetNode<Godot.Panel>("PanelBottom"));
                return _L_PanelBottom;
            }
        }
        private PanelBottom _L_PanelBottom;

        public VSplitContainer(TileSetEditorTerrainPanel uiPanel, Godot.VSplitContainer node) : base(uiPanel, node) {  }
        public override VSplitContainer Clone() => new (UiPanel, (Godot.VSplitContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: TileSetEditorTerrain.DragSprite
    /// </summary>
    public class DragSprite : UiNode<TileSetEditorTerrainPanel, Godot.Sprite2D, DragSprite>
    {
        public DragSprite(TileSetEditorTerrainPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override DragSprite Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.HBoxContainer.AddButton
    /// </summary>
    public AddButton S_AddButton => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab.L_VBoxContainer.L_HBoxContainer.L_AddButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.HBoxContainer.EditButton
    /// </summary>
    public EditButton S_EditButton => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab.L_VBoxContainer.L_HBoxContainer.L_EditButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.HBoxContainer.DeleteButton
    /// </summary>
    public DeleteButton S_DeleteButton => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab.L_VBoxContainer.L_HBoxContainer.L_DeleteButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer.TerrainTab.SelectTexture
    /// </summary>
    public SelectTexture S_SelectTexture => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab.L_VBoxContainer.L_ScrollContainer.L_TerrainTab.L_SelectTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer.TerrainTab.ErrorIcon
    /// </summary>
    public ErrorIcon S_ErrorIcon => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab.L_VBoxContainer.L_ScrollContainer.L_TerrainTab.L_ErrorIcon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer.TerrainTab
    /// </summary>
    public TerrainTab S_TerrainTab => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab.L_VBoxContainer.L_ScrollContainer.L_TerrainTab;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab.L_VBoxContainer.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopTab
    /// </summary>
    public TopTab S_TopTab => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopTab;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.CellRoot.RightCell.CellTexture
    /// </summary>
    public CellTexture S_CellTexture => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg.L_TerrainRoot.L_CellRoot.L_RightCell.L_CellTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.CellRoot.RightCell
    /// </summary>
    public RightCell S_RightCell => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg.L_TerrainRoot.L_CellRoot.L_RightCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture1
    /// </summary>
    public TerrainTexture1 S_TerrainTexture1 => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg.L_TerrainRoot.L_TerrainTexture1;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture2
    /// </summary>
    public TerrainTexture2 S_TerrainTexture2 => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg.L_TerrainRoot.L_TerrainTexture2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture3
    /// </summary>
    public TerrainTexture3 S_TerrainTexture3 => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg.L_TerrainRoot.L_TerrainTexture3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.TerrainTexture4
    /// </summary>
    public TerrainTexture4 S_TerrainTexture4 => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg.L_TerrainRoot.L_TerrainTexture4;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot.Brush
    /// </summary>
    public Brush S_Brush => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg.L_TerrainRoot.L_Brush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg.TerrainRoot
    /// </summary>
    public TerrainRoot S_TerrainRoot => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg.L_TerrainRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer.TopBg
    /// </summary>
    public TopBg S_TopBg => L_VSplitContainer.L_PanelTop.L_HSplitContainer.L_TopBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.HSplitContainer
    /// </summary>
    public HSplitContainer S_HSplitContainer => L_VSplitContainer.L_PanelTop.L_HSplitContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop
    /// </summary>
    public PanelTop S_PanelTop => L_VSplitContainer.L_PanelTop;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.TileTexture.CellRoot.BottomCell
    /// </summary>
    public BottomCell S_BottomCell => L_VSplitContainer.L_PanelBottom.L_BottomBg.L_TileTexture.L_CellRoot.L_BottomCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="EditorMaskBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.TileTexture.MaskBrush
    /// </summary>
    public MaskBrush S_MaskBrush => L_VSplitContainer.L_PanelBottom.L_BottomBg.L_TileTexture.L_MaskBrush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg.TileTexture
    /// </summary>
    public TileTexture S_TileTexture => L_VSplitContainer.L_PanelBottom.L_BottomBg.L_TileTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg
    /// </summary>
    public BottomBg S_BottomBg => L_VSplitContainer.L_PanelBottom.L_BottomBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom
    /// </summary>
    public PanelBottom S_PanelBottom => L_VSplitContainer.L_PanelBottom;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VSplitContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer
    /// </summary>
    public VSplitContainer S_VSplitContainer => L_VSplitContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.DragSprite
    /// </summary>
    public DragSprite S_DragSprite => L_DragSprite;

}
