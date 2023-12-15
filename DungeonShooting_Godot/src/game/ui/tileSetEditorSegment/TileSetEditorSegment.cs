namespace UI.TileSetEditorSegment;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class TileSetEditorSegment : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: TileSetEditorSegment.HSplitContainer
    /// </summary>
    public HSplitContainer L_HSplitContainer
    {
        get
        {
            if (_L_HSplitContainer == null) _L_HSplitContainer = new HSplitContainer((TileSetEditorSegmentPanel)this, GetNode<Godot.HSplitContainer>("HSplitContainer"));
            return _L_HSplitContainer;
        }
    }
    private HSplitContainer _L_HSplitContainer;


    public TileSetEditorSegment() : base(nameof(TileSetEditorSegment))
    {
    }

    public sealed override void OnInitNestedUi()
    {
        _ = L_HSplitContainer.L_Left.L_MarginContainer.L_LeftBg;

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.TileTexture.MaskRoot.MaskRect
    /// </summary>
    public class MaskRect : UiNode<TileSetEditorSegmentPanel, Godot.ColorRect, MaskRect>
    {
        public MaskRect(TileSetEditorSegmentPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override MaskRect Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.TileTexture.MaskRoot
    /// </summary>
    public class MaskRoot : UiNode<TileSetEditorSegmentPanel, Godot.Control, MaskRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.TileTexture.MaskRect
        /// </summary>
        public MaskRect L_MaskRect
        {
            get
            {
                if (_L_MaskRect == null) _L_MaskRect = new MaskRect(UiPanel, Instance.GetNode<Godot.ColorRect>("MaskRect"));
                return _L_MaskRect;
            }
        }
        private MaskRect _L_MaskRect;

        public MaskRoot(TileSetEditorSegmentPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override MaskRoot Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorSegment.MaskBrush"/>, 路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.TileTexture.MaskBrush
    /// </summary>
    public class MaskBrush : UiNode<TileSetEditorSegmentPanel, UI.TileSetEditorSegment.MaskBrush, MaskBrush>
    {
        public MaskBrush(TileSetEditorSegmentPanel uiPanel, UI.TileSetEditorSegment.MaskBrush node) : base(uiPanel, node) {  }
        public override MaskBrush Clone() => new (UiPanel, (UI.TileSetEditorSegment.MaskBrush)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.TileTexture
    /// </summary>
    public class TileTexture : UiNode<TileSetEditorSegmentPanel, Godot.TextureRect, TileTexture>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.MaskRoot
        /// </summary>
        public MaskRoot L_MaskRoot
        {
            get
            {
                if (_L_MaskRoot == null) _L_MaskRoot = new MaskRoot(UiPanel, Instance.GetNode<Godot.Control>("MaskRoot"));
                return _L_MaskRoot;
            }
        }
        private MaskRoot _L_MaskRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorSegment.MaskBrush"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.MaskBrush
        /// </summary>
        public MaskBrush L_MaskBrush
        {
            get
            {
                if (_L_MaskBrush == null) _L_MaskBrush = new MaskBrush(UiPanel, Instance.GetNode<UI.TileSetEditorSegment.MaskBrush>("MaskBrush"));
                return _L_MaskBrush;
            }
        }
        private MaskBrush _L_MaskBrush;

        public TileTexture(TileSetEditorSegmentPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TileTexture Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.Grid
    /// </summary>
    public class Grid : UiNode<TileSetEditorSegmentPanel, Godot.ColorRect, Grid>
    {
        public Grid(TileSetEditorSegmentPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Grid Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorSegment.TileEditArea"/>, 路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg
    /// </summary>
    public class LeftBg : UiNode<TileSetEditorSegmentPanel, UI.TileSetEditorSegment.TileEditArea, LeftBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.TileTexture
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.Grid
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

        public LeftBg(TileSetEditorSegmentPanel uiPanel, UI.TileSetEditorSegment.TileEditArea node) : base(uiPanel, node) {  }
        public override LeftBg Clone() => new (UiPanel, (UI.TileSetEditorSegment.TileEditArea)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<TileSetEditorSegmentPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorSegment.TileEditArea"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.LeftBg
        /// </summary>
        public LeftBg L_LeftBg
        {
            get
            {
                if (_L_LeftBg == null) _L_LeftBg = new LeftBg(UiPanel, Instance.GetNode<UI.TileSetEditorSegment.TileEditArea>("LeftBg"));
                return _L_LeftBg;
            }
        }
        private LeftBg _L_LeftBg;

        public MarginContainer(TileSetEditorSegmentPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorSegment.HSplitContainer.Left
    /// </summary>
    public class Left : UiNode<TileSetEditorSegmentPanel, Godot.Panel, Left>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditorSegment.HSplitContainer.MarginContainer
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

        public Left(TileSetEditorSegmentPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Left Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.Label
    /// </summary>
    public class Label : UiNode<TileSetEditorSegmentPanel, Godot.Label, Label>
    {
        public Label(TileSetEditorSegmentPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.CellButton.PreviewImage
    /// </summary>
    public class PreviewImage : UiNode<TileSetEditorSegmentPanel, Godot.TextureRect, PreviewImage>
    {
        public PreviewImage(TileSetEditorSegmentPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override PreviewImage Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.CellButton.CellId
    /// </summary>
    public class CellId : UiNode<TileSetEditorSegmentPanel, Godot.Label, CellId>
    {
        public CellId(TileSetEditorSegmentPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override CellId Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.CellButton.SelectTexture
    /// </summary>
    public class SelectTexture : UiNode<TileSetEditorSegmentPanel, Godot.NinePatchRect, SelectTexture>
    {
        public SelectTexture(TileSetEditorSegmentPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override SelectTexture Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.CellButton
    /// </summary>
    public class CellButton : UiNode<TileSetEditorSegmentPanel, Godot.Button, CellButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.PreviewImage
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.CellId
        /// </summary>
        public CellId L_CellId
        {
            get
            {
                if (_L_CellId == null) _L_CellId = new CellId(UiPanel, Instance.GetNode<Godot.Label>("CellId"));
                return _L_CellId;
            }
        }
        private CellId _L_CellId;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.SelectTexture
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

        public CellButton(TileSetEditorSegmentPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override CellButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<TileSetEditorSegmentPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.CellButton
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

        public ScrollContainer(TileSetEditorSegmentPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorSegment.TileSelected"/>, 路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg
    /// </summary>
    public class RightBg : UiNode<TileSetEditorSegmentPanel, UI.TileSetEditorSegment.TileSelected, RightBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.Label
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.ScrollContainer
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

        public RightBg(TileSetEditorSegmentPanel uiPanel, UI.TileSetEditorSegment.TileSelected node) : base(uiPanel, node) {  }
        public override RightBg Clone() => new (UiPanel, (UI.TileSetEditorSegment.TileSelected)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer
    /// </summary>
    public class MarginContainer_1 : UiNode<TileSetEditorSegmentPanel, Godot.MarginContainer, MarginContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorSegment.TileSelected"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.RightBg
        /// </summary>
        public RightBg L_RightBg
        {
            get
            {
                if (_L_RightBg == null) _L_RightBg = new RightBg(UiPanel, Instance.GetNode<UI.TileSetEditorSegment.TileSelected>("RightBg"));
                return _L_RightBg;
            }
        }
        private RightBg _L_RightBg;

        public MarginContainer_1(TileSetEditorSegmentPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_1 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorSegment.HSplitContainer.Right
    /// </summary>
    public class Right : UiNode<TileSetEditorSegmentPanel, Godot.Panel, Right>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditorSegment.HSplitContainer.MarginContainer
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

        public Right(TileSetEditorSegmentPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Right Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HSplitContainer"/>, 路径: TileSetEditorSegment.HSplitContainer
    /// </summary>
    public class HSplitContainer : UiNode<TileSetEditorSegmentPanel, Godot.HSplitContainer, HSplitContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorSegment.Left
        /// </summary>
        public Left L_Left
        {
            get
            {
                if (_L_Left == null) _L_Left = new Left(UiPanel, Instance.GetNode<Godot.Panel>("Left"));
                return _L_Left;
            }
        }
        private Left _L_Left;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorSegment.Right
        /// </summary>
        public Right L_Right
        {
            get
            {
                if (_L_Right == null) _L_Right = new Right(UiPanel, Instance.GetNode<Godot.Panel>("Right"));
                return _L_Right;
            }
        }
        private Right _L_Right;

        public HSplitContainer(TileSetEditorSegmentPanel uiPanel, Godot.HSplitContainer node) : base(uiPanel, node) {  }
        public override HSplitContainer Clone() => new (UiPanel, (Godot.HSplitContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.TileTexture.MaskRoot.MaskRect
    /// </summary>
    public MaskRect S_MaskRect => L_HSplitContainer.L_Left.L_MarginContainer.L_LeftBg.L_TileTexture.L_MaskRoot.L_MaskRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.TileTexture.MaskRoot
    /// </summary>
    public MaskRoot S_MaskRoot => L_HSplitContainer.L_Left.L_MarginContainer.L_LeftBg.L_TileTexture.L_MaskRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorSegment.MaskBrush"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.TileTexture.MaskBrush
    /// </summary>
    public MaskBrush S_MaskBrush => L_HSplitContainer.L_Left.L_MarginContainer.L_LeftBg.L_TileTexture.L_MaskBrush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.TileTexture
    /// </summary>
    public TileTexture S_TileTexture => L_HSplitContainer.L_Left.L_MarginContainer.L_LeftBg.L_TileTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg.Grid
    /// </summary>
    public Grid S_Grid => L_HSplitContainer.L_Left.L_MarginContainer.L_LeftBg.L_Grid;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorSegment.TileEditArea"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left.MarginContainer.LeftBg
    /// </summary>
    public LeftBg S_LeftBg => L_HSplitContainer.L_Left.L_MarginContainer.L_LeftBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Left
    /// </summary>
    public Left S_Left => L_HSplitContainer.L_Left;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.Label
    /// </summary>
    public Label S_Label => L_HSplitContainer.L_Right.L_MarginContainer.L_RightBg.L_Label;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.CellButton.PreviewImage
    /// </summary>
    public PreviewImage S_PreviewImage => L_HSplitContainer.L_Right.L_MarginContainer.L_RightBg.L_ScrollContainer.L_CellButton.L_PreviewImage;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.CellButton.CellId
    /// </summary>
    public CellId S_CellId => L_HSplitContainer.L_Right.L_MarginContainer.L_RightBg.L_ScrollContainer.L_CellButton.L_CellId;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.CellButton.SelectTexture
    /// </summary>
    public SelectTexture S_SelectTexture => L_HSplitContainer.L_Right.L_MarginContainer.L_RightBg.L_ScrollContainer.L_CellButton.L_SelectTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer.CellButton
    /// </summary>
    public CellButton S_CellButton => L_HSplitContainer.L_Right.L_MarginContainer.L_RightBg.L_ScrollContainer.L_CellButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_HSplitContainer.L_Right.L_MarginContainer.L_RightBg.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorSegment.TileSelected"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right.MarginContainer.RightBg
    /// </summary>
    public RightBg S_RightBg => L_HSplitContainer.L_Right.L_MarginContainer.L_RightBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorSegment.HSplitContainer.Right
    /// </summary>
    public Right S_Right => L_HSplitContainer.L_Right;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: TileSetEditorSegment.HSplitContainer
    /// </summary>
    public HSplitContainer S_HSplitContainer => L_HSplitContainer;

}
