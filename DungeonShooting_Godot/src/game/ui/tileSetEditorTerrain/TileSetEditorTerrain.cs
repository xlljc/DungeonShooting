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
        _ = L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg;
        _ = L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot.L_Brush;
        _ = L_VSplitContainer.L_PanelBottom.L_MarginContainer.L_BottomBg;

    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.CellRoot.RightCell.CellTexture
    /// </summary>
    public class CellTexture : UiNode<TileSetEditorTerrainPanel, Godot.Sprite2D, CellTexture>
    {
        public CellTexture(TileSetEditorTerrainPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override CellTexture Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.CellRoot.RightCell.ColorRect
    /// </summary>
    public class ColorRect : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, ColorRect>
    {
        public ColorRect(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override ColorRect Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.CellRoot.RightCell
    /// </summary>
    public class RightCell : UiNode<TileSetEditorTerrainPanel, Godot.Control, RightCell>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.CellRoot.CellTexture
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.CellRoot.ColorRect
        /// </summary>
        public ColorRect L_ColorRect
        {
            get
            {
                if (_L_ColorRect == null) _L_ColorRect = new ColorRect(UiPanel, Instance.GetNode<Godot.ColorRect>("ColorRect"));
                return _L_ColorRect;
            }
        }
        private ColorRect _L_ColorRect;

        public RightCell(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override RightCell Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.CellRoot
    /// </summary>
    public class CellRoot : UiNode<TileSetEditorTerrainPanel, Godot.Control, CellRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.RightCell
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
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture1.Label
    /// </summary>
    public class Label : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label>
    {
        public Label(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture1
    /// </summary>
    public class TerrainTexture1 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.Label
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

        public TerrainTexture1(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TerrainTexture1 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture2.Label
    /// </summary>
    public class Label_1 : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label_1>
    {
        public Label_1(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_1 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture2
    /// </summary>
    public class TerrainTexture2 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.Label
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

        public TerrainTexture2(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TerrainTexture2 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture3.Label
    /// </summary>
    public class Label_2 : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label_2>
    {
        public Label_2(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture3
    /// </summary>
    public class TerrainTexture3 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.Label
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

        public TerrainTexture3(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TerrainTexture3 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture4.Label
    /// </summary>
    public class Label_3 : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label_3>
    {
        public Label_3(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_3 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture4
    /// </summary>
    public class TerrainTexture4 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture4>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.Label
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

        public TerrainTexture4(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TerrainTexture4 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.Brush
    /// </summary>
    public class Brush : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TerrainBrush, Brush>
    {
        public Brush(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TerrainBrush node) : base(uiPanel, node) {  }
        public override Brush Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TerrainBrush)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot
    /// </summary>
    public class TerrainRoot : UiNode<TileSetEditorTerrainPanel, Godot.Control, TerrainRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.CellRoot
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainTexture1
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainTexture2
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainTexture3
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainTexture4
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.Brush
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
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.Grid
    /// </summary>
    public class Grid : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, Grid>
    {
        public Grid(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.FocusBtn
    /// </summary>
    public class FocusBtn : UiNode<TileSetEditorTerrainPanel, Godot.TextureButton, FocusBtn>
    {
        public FocusBtn(TileSetEditorTerrainPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainTypeButton
    /// </summary>
    public class TerrainTypeButton : UiNode<TileSetEditorTerrainPanel, Godot.OptionButton, TerrainTypeButton>
    {
        public TerrainTypeButton(TileSetEditorTerrainPanel uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override TerrainTypeButton Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg
    /// </summary>
    public class TopBg : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TileEditTerrain, TopBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TerrainRoot
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.Grid
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.FocusBtn
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TerrainTypeButton
        /// </summary>
        public TerrainTypeButton L_TerrainTypeButton
        {
            get
            {
                if (_L_TerrainTypeButton == null) _L_TerrainTypeButton = new TerrainTypeButton(UiPanel, Instance.GetNode<Godot.OptionButton>("TerrainTypeButton"));
                return _L_TerrainTypeButton;
            }
        }
        private TerrainTypeButton _L_TerrainTypeButton;

        public TopBg(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TileEditTerrain node) : base(uiPanel, node) {  }
        public override TopBg Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TileEditTerrain)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<TileSetEditorTerrainPanel, Godot.MarginContainer, MarginContainer>
    {
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

        public MarginContainer(TileSetEditorTerrainPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelTop
    /// </summary>
    public class PanelTop : UiNode<TileSetEditorTerrainPanel, Godot.Panel, PanelTop>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.MarginContainer
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

        public PanelTop(TileSetEditorTerrainPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override PanelTop Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.TileTexture.CellRoot.BottomCell
    /// </summary>
    public class BottomCell : UiNode<TileSetEditorTerrainPanel, Godot.Control, BottomCell>
    {
        public BottomCell(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override BottomCell Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.TileTexture.CellRoot
    /// </summary>
    public class CellRoot_1 : UiNode<TileSetEditorTerrainPanel, Godot.Control, CellRoot_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.TileTexture.BottomCell
        /// </summary>
        public BottomCell L_BottomCell
        {
            get
            {
                if (_L_BottomCell == null) _L_BottomCell = new BottomCell(UiPanel, Instance.GetNode<Godot.Control>("BottomCell"));
                return _L_BottomCell;
            }
        }
        private BottomCell _L_BottomCell;

        public CellRoot_1(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override CellRoot_1 Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="EditorMaskBrush"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.TileTexture.MaskBrush
    /// </summary>
    public class MaskBrush : UiNode<TileSetEditorTerrainPanel, EditorMaskBrush, MaskBrush>
    {
        public MaskBrush(TileSetEditorTerrainPanel uiPanel, EditorMaskBrush node) : base(uiPanel, node) {  }
        public override MaskBrush Clone() => new (UiPanel, (EditorMaskBrush)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.TileTexture
    /// </summary>
    public class TileTexture : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TileTexture>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.CellRoot
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="EditorMaskBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.MaskBrush
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
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.Grid
    /// </summary>
    public class Grid_1 : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, Grid_1>
    {
        public Grid_1(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid_1 Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.FocusBtn
    /// </summary>
    public class FocusBtn_1 : UiNode<TileSetEditorTerrainPanel, Godot.TextureButton, FocusBtn_1>
    {
        public FocusBtn_1(TileSetEditorTerrainPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn_1 Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg
    /// </summary>
    public class BottomBg : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TileEditArea, BottomBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.TileTexture
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.Grid
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.FocusBtn
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
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer
    /// </summary>
    public class MarginContainer_1 : UiNode<TileSetEditorTerrainPanel, Godot.MarginContainer, MarginContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.BottomBg
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

        public MarginContainer_1(TileSetEditorTerrainPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_1 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorTerrain.VSplitContainer.PanelBottom
    /// </summary>
    public class PanelBottom : UiNode<TileSetEditorTerrainPanel, Godot.Panel, PanelBottom>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.MarginContainer
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
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.CellRoot.RightCell.CellTexture
    /// </summary>
    public CellTexture S_CellTexture => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot.L_CellRoot.L_RightCell.L_CellTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.CellRoot.RightCell.ColorRect
    /// </summary>
    public ColorRect S_ColorRect => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot.L_CellRoot.L_RightCell.L_ColorRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.CellRoot.RightCell
    /// </summary>
    public RightCell S_RightCell => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot.L_CellRoot.L_RightCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture1
    /// </summary>
    public TerrainTexture1 S_TerrainTexture1 => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot.L_TerrainTexture1;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture2
    /// </summary>
    public TerrainTexture2 S_TerrainTexture2 => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot.L_TerrainTexture2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture3
    /// </summary>
    public TerrainTexture3 S_TerrainTexture3 => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot.L_TerrainTexture3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.TerrainTexture4
    /// </summary>
    public TerrainTexture4 S_TerrainTexture4 => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot.L_TerrainTexture4;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot.Brush
    /// </summary>
    public Brush S_Brush => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot.L_Brush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainRoot
    /// </summary>
    public TerrainRoot S_TerrainRoot => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg.TerrainTypeButton
    /// </summary>
    public TerrainTypeButton S_TerrainTypeButton => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg.L_TerrainTypeButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop.MarginContainer.TopBg
    /// </summary>
    public TopBg S_TopBg => L_VSplitContainer.L_PanelTop.L_MarginContainer.L_TopBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelTop
    /// </summary>
    public PanelTop S_PanelTop => L_VSplitContainer.L_PanelTop;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.TileTexture.CellRoot.BottomCell
    /// </summary>
    public BottomCell S_BottomCell => L_VSplitContainer.L_PanelBottom.L_MarginContainer.L_BottomBg.L_TileTexture.L_CellRoot.L_BottomCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="EditorMaskBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.TileTexture.MaskBrush
    /// </summary>
    public MaskBrush S_MaskBrush => L_VSplitContainer.L_PanelBottom.L_MarginContainer.L_BottomBg.L_TileTexture.L_MaskBrush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg.TileTexture
    /// </summary>
    public TileTexture S_TileTexture => L_VSplitContainer.L_PanelBottom.L_MarginContainer.L_BottomBg.L_TileTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.PanelBottom.MarginContainer.BottomBg
    /// </summary>
    public BottomBg S_BottomBg => L_VSplitContainer.L_PanelBottom.L_MarginContainer.L_BottomBg;

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
