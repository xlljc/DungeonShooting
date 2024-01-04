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


    public TileSetEditorTerrain() : base(nameof(TileSetEditorTerrain))
    {
    }

    public sealed override void OnInitNestedUi()
    {
        _ = L_VSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg;
        _ = L_VSplitContainer.L_LeftBottom.L_MarginContainer.L_LeftBg;

    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.CellRoot.RightCell.CellTexture
    /// </summary>
    public class CellTexture : UiNode<TileSetEditorTerrainPanel, Godot.Sprite2D, CellTexture>
    {
        public CellTexture(TileSetEditorTerrainPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override CellTexture Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDropHandler"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.CellRoot.RightCell
    /// </summary>
    public class RightCell : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TerrainCellDropHandler, RightCell>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.CellRoot.CellTexture
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

        public RightCell(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TerrainCellDropHandler node) : base(uiPanel, node) {  }
        public override RightCell Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TerrainCellDropHandler)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.CellRoot
    /// </summary>
    public class CellRoot : UiNode<TileSetEditorTerrainPanel, Godot.Control, CellRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDropHandler"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.RightCell
        /// </summary>
        public RightCell L_RightCell
        {
            get
            {
                if (_L_RightCell == null) _L_RightCell = new RightCell(UiPanel, Instance.GetNode<UI.TileSetEditorTerrain.TerrainCellDropHandler>("RightCell"));
                return _L_RightCell;
            }
        }
        private RightCell _L_RightCell;

        public CellRoot(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override CellRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.TerrainTexture1.Label
    /// </summary>
    public class Label : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label>
    {
        public Label(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.TerrainTexture1
    /// </summary>
    public class TerrainTexture1 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.Label
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
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.TerrainTexture2.Label
    /// </summary>
    public class Label_1 : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label_1>
    {
        public Label_1(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_1 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.TerrainTexture2
    /// </summary>
    public class TerrainTexture2 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.Label
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
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.TerrainTexture3.Label
    /// </summary>
    public class Label_2 : UiNode<TileSetEditorTerrainPanel, Godot.Label, Label_2>
    {
        public Label_2(TileSetEditorTerrainPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.TerrainTexture3
    /// </summary>
    public class TerrainTexture3 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TerrainTexture3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.Label
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
    /// 类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.Brush
    /// </summary>
    public class Brush : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TerrainBrush, Brush>
    {
        public Brush(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TerrainBrush node) : base(uiPanel, node) {  }
        public override Brush Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TerrainBrush)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot
    /// </summary>
    public class TerrainRoot : UiNode<TileSetEditorTerrainPanel, Godot.Control, TerrainRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.CellRoot
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainTexture1
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainTexture2
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainTexture3
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.Brush
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
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.Grid
    /// </summary>
    public class Grid : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, Grid>
    {
        public Grid(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.FocusBtn
    /// </summary>
    public class FocusBtn : UiNode<TileSetEditorTerrainPanel, Godot.TextureButton, FocusBtn>
    {
        public FocusBtn(TileSetEditorTerrainPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg
    /// </summary>
    public class LeftBottomBg : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TileEditTerrain, LeftBottomBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.TerrainRoot
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.Grid
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.FocusBtn
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

        public LeftBottomBg(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TileEditTerrain node) : base(uiPanel, node) {  }
        public override LeftBottomBg Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TileEditTerrain)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<TileSetEditorTerrainPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.LeftBottomBg
        /// </summary>
        public LeftBottomBg L_LeftBottomBg
        {
            get
            {
                if (_L_LeftBottomBg == null) _L_LeftBottomBg = new LeftBottomBg(UiPanel, Instance.GetNode<UI.TileSetEditorTerrain.TileEditTerrain>("LeftBottomBg"));
                return _L_LeftBottomBg;
            }
        }
        private LeftBottomBg _L_LeftBottomBg;

        public MarginContainer(TileSetEditorTerrainPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2
    /// </summary>
    public class LeftBottom2 : UiNode<TileSetEditorTerrainPanel, Godot.Panel, LeftBottom2>
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

        public LeftBottom2(TileSetEditorTerrainPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override LeftBottom2 Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDragHandler"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.CellRoot.LeftCell
    /// </summary>
    public class LeftCell : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TerrainCellDragHandler, LeftCell>
    {
        public LeftCell(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TerrainCellDragHandler node) : base(uiPanel, node) {  }
        public override LeftCell Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TerrainCellDragHandler)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.CellRoot
    /// </summary>
    public class CellRoot_1 : UiNode<TileSetEditorTerrainPanel, Godot.Control, CellRoot_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDragHandler"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.LeftCell
        /// </summary>
        public LeftCell L_LeftCell
        {
            get
            {
                if (_L_LeftCell == null) _L_LeftCell = new LeftCell(UiPanel, Instance.GetNode<UI.TileSetEditorTerrain.TerrainCellDragHandler>("LeftCell"));
                return _L_LeftCell;
            }
        }
        private LeftCell _L_LeftCell;

        public CellRoot_1(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override CellRoot_1 Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="EditorMaskBrush"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.MaskBrush
    /// </summary>
    public class MaskBrush : UiNode<TileSetEditorTerrainPanel, EditorMaskBrush, MaskBrush>
    {
        public MaskBrush(TileSetEditorTerrainPanel uiPanel, EditorMaskBrush node) : base(uiPanel, node) {  }
        public override MaskBrush Clone() => new (UiPanel, (EditorMaskBrush)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture
    /// </summary>
    public class TileTexture : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TileTexture>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.CellRoot
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="EditorMaskBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.MaskBrush
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
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.Grid
    /// </summary>
    public class Grid_1 : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, Grid_1>
    {
        public Grid_1(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid_1 Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.FocusBtn
    /// </summary>
    public class FocusBtn_1 : UiNode<TileSetEditorTerrainPanel, Godot.TextureButton, FocusBtn_1>
    {
        public FocusBtn_1(TileSetEditorTerrainPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn_1 Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg
    /// </summary>
    public class LeftBg : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TileEditArea, LeftBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.TileTexture
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.Grid
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.FocusBtn
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

        public LeftBg(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TileEditArea node) : base(uiPanel, node) {  }
        public override LeftBg Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TileEditArea)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer
    /// </summary>
    public class MarginContainer_1 : UiNode<TileSetEditorTerrainPanel, Godot.MarginContainer, MarginContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.LeftBg
        /// </summary>
        public LeftBg L_LeftBg
        {
            get
            {
                if (_L_LeftBg == null) _L_LeftBg = new LeftBg(UiPanel, Instance.GetNode<UI.TileSetEditorTerrain.TileEditArea>("LeftBg"));
                return _L_LeftBg;
            }
        }
        private LeftBg _L_LeftBg;

        public MarginContainer_1(TileSetEditorTerrainPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_1 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorTerrain.VSplitContainer.LeftBottom
    /// </summary>
    public class LeftBottom : UiNode<TileSetEditorTerrainPanel, Godot.Panel, LeftBottom>
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

        public LeftBottom(TileSetEditorTerrainPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override LeftBottom Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VSplitContainer"/>, 路径: TileSetEditorTerrain.VSplitContainer
    /// </summary>
    public class VSplitContainer : UiNode<TileSetEditorTerrainPanel, Godot.VSplitContainer, VSplitContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.LeftBottom2
        /// </summary>
        public LeftBottom2 L_LeftBottom2
        {
            get
            {
                if (_L_LeftBottom2 == null) _L_LeftBottom2 = new LeftBottom2(UiPanel, Instance.GetNode<Godot.Panel>("LeftBottom2"));
                return _L_LeftBottom2;
            }
        }
        private LeftBottom2 _L_LeftBottom2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.LeftBottom
        /// </summary>
        public LeftBottom L_LeftBottom
        {
            get
            {
                if (_L_LeftBottom == null) _L_LeftBottom = new LeftBottom(UiPanel, Instance.GetNode<Godot.Panel>("LeftBottom"));
                return _L_LeftBottom;
            }
        }
        private LeftBottom _L_LeftBottom;

        public VSplitContainer(TileSetEditorTerrainPanel uiPanel, Godot.VSplitContainer node) : base(uiPanel, node) {  }
        public override VSplitContainer Clone() => new (UiPanel, (Godot.VSplitContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.CellRoot.RightCell.CellTexture
    /// </summary>
    public CellTexture S_CellTexture => L_VSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TerrainRoot.L_CellRoot.L_RightCell.L_CellTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDropHandler"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.CellRoot.RightCell
    /// </summary>
    public RightCell S_RightCell => L_VSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TerrainRoot.L_CellRoot.L_RightCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.TerrainTexture1
    /// </summary>
    public TerrainTexture1 S_TerrainTexture1 => L_VSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TerrainRoot.L_TerrainTexture1;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.TerrainTexture2
    /// </summary>
    public TerrainTexture2 S_TerrainTexture2 => L_VSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TerrainRoot.L_TerrainTexture2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.TerrainTexture3
    /// </summary>
    public TerrainTexture3 S_TerrainTexture3 => L_VSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TerrainRoot.L_TerrainTexture3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot.Brush
    /// </summary>
    public Brush S_Brush => L_VSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TerrainRoot.L_Brush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TerrainRoot
    /// </summary>
    public TerrainRoot S_TerrainRoot => L_VSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TerrainRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg
    /// </summary>
    public LeftBottomBg S_LeftBottomBg => L_VSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom2
    /// </summary>
    public LeftBottom2 S_LeftBottom2 => L_VSplitContainer.L_LeftBottom2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDragHandler"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.CellRoot.LeftCell
    /// </summary>
    public LeftCell S_LeftCell => L_VSplitContainer.L_LeftBottom.L_MarginContainer.L_LeftBg.L_TileTexture.L_CellRoot.L_LeftCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="EditorMaskBrush"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.MaskBrush
    /// </summary>
    public MaskBrush S_MaskBrush => L_VSplitContainer.L_LeftBottom.L_MarginContainer.L_LeftBg.L_TileTexture.L_MaskBrush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture
    /// </summary>
    public TileTexture S_TileTexture => L_VSplitContainer.L_LeftBottom.L_MarginContainer.L_LeftBg.L_TileTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom.MarginContainer.LeftBg
    /// </summary>
    public LeftBg S_LeftBg => L_VSplitContainer.L_LeftBottom.L_MarginContainer.L_LeftBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.VSplitContainer.LeftBottom
    /// </summary>
    public LeftBottom S_LeftBottom => L_VSplitContainer.L_LeftBottom;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VSplitContainer"/>, 节点路径: TileSetEditorTerrain.VSplitContainer
    /// </summary>
    public VSplitContainer S_VSplitContainer => L_VSplitContainer;

}
