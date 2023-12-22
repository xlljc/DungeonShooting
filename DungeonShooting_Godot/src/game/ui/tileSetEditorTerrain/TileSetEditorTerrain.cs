namespace UI.TileSetEditorTerrain;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class TileSetEditorTerrain : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: TileSetEditorTerrain.HSplitContainer
    /// </summary>
    public HSplitContainer L_HSplitContainer
    {
        get
        {
            if (_L_HSplitContainer == null) _L_HSplitContainer = new HSplitContainer((TileSetEditorTerrainPanel)this, GetNode<Godot.HSplitContainer>("HSplitContainer"));
            return _L_HSplitContainer;
        }
    }
    private HSplitContainer _L_HSplitContainer;


    public TileSetEditorTerrain() : base(nameof(TileSetEditorTerrain))
    {
    }

    public sealed override void OnInitNestedUi()
    {
        _ = L_HSplitContainer.L_LeftBottom.L_MarginContainer.L_LeftBg;
        _ = L_HSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg;

    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDragHandler"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.CellRoot.LeftCell
    /// </summary>
    public class LeftCell : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TerrainCellDragHandler, LeftCell>
    {
        public LeftCell(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TerrainCellDragHandler node) : base(uiPanel, node) {  }
        public override LeftCell Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TerrainCellDragHandler)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.CellRoot
    /// </summary>
    public class CellRoot : UiNode<TileSetEditorTerrainPanel, Godot.Control, CellRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDragHandler"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.LeftCell
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

        public CellRoot(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override CellRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="EditorMaskBrush"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.MaskBrush
    /// </summary>
    public class MaskBrush : UiNode<TileSetEditorTerrainPanel, EditorMaskBrush, MaskBrush>
    {
        public MaskBrush(TileSetEditorTerrainPanel uiPanel, EditorMaskBrush node) : base(uiPanel, node) {  }
        public override MaskBrush Clone() => new (UiPanel, (EditorMaskBrush)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture
    /// </summary>
    public class TileTexture : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TileTexture>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.CellRoot
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="EditorMaskBrush"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.MaskBrush
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
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.Grid
    /// </summary>
    public class Grid : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, Grid>
    {
        public Grid(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.FocusBtn
    /// </summary>
    public class FocusBtn : UiNode<TileSetEditorTerrainPanel, Godot.TextureButton, FocusBtn>
    {
        public FocusBtn(TileSetEditorTerrainPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg
    /// </summary>
    public class LeftBg : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TileEditArea, LeftBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.TileTexture
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.Grid
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.FocusBtn
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

        public LeftBg(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TileEditArea node) : base(uiPanel, node) {  }
        public override LeftBg Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TileEditArea)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<TileSetEditorTerrainPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.LeftBg
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

        public MarginContainer(TileSetEditorTerrainPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom
    /// </summary>
    public class LeftBottom : UiNode<TileSetEditorTerrainPanel, Godot.Panel, LeftBottom>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.MarginContainer
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

        public LeftBottom(TileSetEditorTerrainPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override LeftBottom Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.CellRoot.RightCell.CellTexture
    /// </summary>
    public class CellTexture : UiNode<TileSetEditorTerrainPanel, Godot.Sprite2D, CellTexture>
    {
        public CellTexture(TileSetEditorTerrainPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override CellTexture Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDropHandler"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.CellRoot.RightCell
    /// </summary>
    public class RightCell : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TerrainCellDropHandler, RightCell>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.CellRoot.CellTexture
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
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.CellRoot
    /// </summary>
    public class CellRoot_1 : UiNode<TileSetEditorTerrainPanel, Godot.Control, CellRoot_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDropHandler"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.RightCell
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

        public CellRoot_1(TileSetEditorTerrainPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override CellRoot_1 Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.TileShadow
    /// </summary>
    public class TileShadow : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TileShadow>
    {
        public TileShadow(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TileShadow Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.Brush
    /// </summary>
    public class Brush : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TerrainBrush, Brush>
    {
        public Brush(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TerrainBrush node) : base(uiPanel, node) {  }
        public override Brush Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TerrainBrush)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture
    /// </summary>
    public class TileTexture_1 : UiNode<TileSetEditorTerrainPanel, Godot.TextureRect, TileTexture_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.CellRoot
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileShadow
        /// </summary>
        public TileShadow L_TileShadow
        {
            get
            {
                if (_L_TileShadow == null) _L_TileShadow = new TileShadow(UiPanel, Instance.GetNode<Godot.TextureRect>("TileShadow"));
                return _L_TileShadow;
            }
        }
        private TileShadow _L_TileShadow;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.Brush
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

        public TileTexture_1(TileSetEditorTerrainPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TileTexture_1 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.Grid
    /// </summary>
    public class Grid_1 : UiNode<TileSetEditorTerrainPanel, Godot.ColorRect, Grid_1>
    {
        public Grid_1(TileSetEditorTerrainPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid_1 Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.FocusBtn
    /// </summary>
    public class FocusBtn_1 : UiNode<TileSetEditorTerrainPanel, Godot.TextureButton, FocusBtn_1>
    {
        public FocusBtn_1(TileSetEditorTerrainPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn_1 Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg
    /// </summary>
    public class LeftBottomBg : UiNode<TileSetEditorTerrainPanel, UI.TileSetEditorTerrain.TileEditTerrain, LeftBottomBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.TileTexture
        /// </summary>
        public TileTexture_1 L_TileTexture
        {
            get
            {
                if (_L_TileTexture == null) _L_TileTexture = new TileTexture_1(UiPanel, Instance.GetNode<Godot.TextureRect>("TileTexture"));
                return _L_TileTexture;
            }
        }
        private TileTexture_1 _L_TileTexture;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.Grid
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.FocusBtn
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

        public LeftBottomBg(TileSetEditorTerrainPanel uiPanel, UI.TileSetEditorTerrain.TileEditTerrain node) : base(uiPanel, node) {  }
        public override LeftBottomBg Clone() => new (UiPanel, (UI.TileSetEditorTerrain.TileEditTerrain)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer
    /// </summary>
    public class MarginContainer_1 : UiNode<TileSetEditorTerrainPanel, Godot.MarginContainer, MarginContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.LeftBottomBg
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

        public MarginContainer_1(TileSetEditorTerrainPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_1 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2
    /// </summary>
    public class LeftBottom2 : UiNode<TileSetEditorTerrainPanel, Godot.Panel, LeftBottom2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.MarginContainer
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

        public LeftBottom2(TileSetEditorTerrainPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override LeftBottom2 Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HSplitContainer"/>, 路径: TileSetEditorTerrain.HSplitContainer
    /// </summary>
    public class HSplitContainer : UiNode<TileSetEditorTerrainPanel, Godot.HSplitContainer, HSplitContainer>
    {
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

        public HSplitContainer(TileSetEditorTerrainPanel uiPanel, Godot.HSplitContainer node) : base(uiPanel, node) {  }
        public override HSplitContainer Clone() => new (UiPanel, (Godot.HSplitContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDragHandler"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.CellRoot.LeftCell
    /// </summary>
    public LeftCell S_LeftCell => L_HSplitContainer.L_LeftBottom.L_MarginContainer.L_LeftBg.L_TileTexture.L_CellRoot.L_LeftCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="EditorMaskBrush"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg.TileTexture.MaskBrush
    /// </summary>
    public MaskBrush S_MaskBrush => L_HSplitContainer.L_LeftBottom.L_MarginContainer.L_LeftBg.L_TileTexture.L_MaskBrush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditArea"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom.MarginContainer.LeftBg
    /// </summary>
    public LeftBg S_LeftBg => L_HSplitContainer.L_LeftBottom.L_MarginContainer.L_LeftBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom
    /// </summary>
    public LeftBottom S_LeftBottom => L_HSplitContainer.L_LeftBottom;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.CellRoot.RightCell.CellTexture
    /// </summary>
    public CellTexture S_CellTexture => L_HSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TileTexture.L_CellRoot.L_RightCell.L_CellTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainCellDropHandler"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.CellRoot.RightCell
    /// </summary>
    public RightCell S_RightCell => L_HSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TileTexture.L_CellRoot.L_RightCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.TileShadow
    /// </summary>
    public TileShadow S_TileShadow => L_HSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TileTexture.L_TileShadow;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TerrainBrush"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg.TileTexture.Brush
    /// </summary>
    public Brush S_Brush => L_HSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg.L_TileTexture.L_Brush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorTerrain.TileEditTerrain"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2.MarginContainer.LeftBottomBg
    /// </summary>
    public LeftBottomBg S_LeftBottomBg => L_HSplitContainer.L_LeftBottom2.L_MarginContainer.L_LeftBottomBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorTerrain.HSplitContainer.LeftBottom2
    /// </summary>
    public LeftBottom2 S_LeftBottom2 => L_HSplitContainer.L_LeftBottom2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: TileSetEditorTerrain.HSplitContainer
    /// </summary>
    public HSplitContainer S_HSplitContainer => L_HSplitContainer;

}
