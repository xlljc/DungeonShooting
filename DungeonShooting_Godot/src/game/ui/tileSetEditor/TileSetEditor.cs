namespace UI.TileSetEditor;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class TileSetEditor : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg((TileSetEditorPanel)this, GetNode<Godot.Panel>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;


    public TileSetEditor() : base(nameof(TileSetEditor))
    {
    }

    public sealed override void OnInitNestedUi()
    {
        _ = L_Bg.L_VBoxContainer.L_HBoxContainer.L_RightRoot.L_ImportRoot;

    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.Head.Back
    /// </summary>
    public class Back : UiNode<TileSetEditorPanel, Godot.Button, Back>
    {
        public Back(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.Head.Save
    /// </summary>
    public class Save : UiNode<TileSetEditorPanel, Godot.Button, Save>
    {
        public Save(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Save Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditor.Bg.VBoxContainer.Head.Title
    /// </summary>
    public class Title : UiNode<TileSetEditorPanel, Godot.Label, Title>
    {
        public Title(TileSetEditorPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditor.Bg.VBoxContainer.Head
    /// </summary>
    public class Head : UiNode<TileSetEditorPanel, Godot.Panel, Head>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Back
        /// </summary>
        public Back L_Back
        {
            get
            {
                if (_L_Back == null) _L_Back = new Back(UiPanel, Instance.GetNode<Godot.Button>("Back"));
                return _L_Back;
            }
        }
        private Back _L_Back;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Save
        /// </summary>
        public Save L_Save
        {
            get
            {
                if (_L_Save == null) _L_Save = new Save(UiPanel, Instance.GetNode<Godot.Button>("Save"));
                return _L_Save;
            }
        }
        private Save _L_Save;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Title
        /// </summary>
        public Title L_Title
        {
            get
            {
                if (_L_Title == null) _L_Title = new Title(UiPanel, Instance.GetNode<Godot.Label>("Title"));
                return _L_Title;
            }
        }
        private Title _L_Title;

        public Head(TileSetEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Head Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.ImportTab
    /// </summary>
    public class ImportTab : UiNode<TileSetEditorPanel, Godot.Button, ImportTab>
    {
        public ImportTab(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ImportTab Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.TileCellTab
    /// </summary>
    public class TileCellTab : UiNode<TileSetEditorPanel, Godot.Button, TileCellTab>
    {
        public TileCellTab(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override TileCellTab Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.TerrainTab
    /// </summary>
    public class TerrainTab : UiNode<TileSetEditorPanel, Godot.Button, TerrainTab>
    {
        public TerrainTab(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override TerrainTab Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.PatternTab
    /// </summary>
    public class PatternTab : UiNode<TileSetEditorPanel, Godot.Button, PatternTab>
    {
        public PatternTab(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override PatternTab Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.ObjectTab
    /// </summary>
    public class ObjectTab : UiNode<TileSetEditorPanel, Godot.Button, ObjectTab>
    {
        public ObjectTab(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ObjectTab Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.TrapBtn
    /// </summary>
    public class TrapBtn : UiNode<TileSetEditorPanel, Godot.Button, TrapBtn>
    {
        public TrapBtn(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override TrapBtn Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer_1 : UiNode<TileSetEditorPanel, Godot.VBoxContainer, VBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.ImportTab
        /// </summary>
        public ImportTab L_ImportTab
        {
            get
            {
                if (_L_ImportTab == null) _L_ImportTab = new ImportTab(UiPanel, Instance.GetNode<Godot.Button>("ImportTab"));
                return _L_ImportTab;
            }
        }
        private ImportTab _L_ImportTab;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.TileCellTab
        /// </summary>
        public TileCellTab L_TileCellTab
        {
            get
            {
                if (_L_TileCellTab == null) _L_TileCellTab = new TileCellTab(UiPanel, Instance.GetNode<Godot.Button>("TileCellTab"));
                return _L_TileCellTab;
            }
        }
        private TileCellTab _L_TileCellTab;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.TerrainTab
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.PatternTab
        /// </summary>
        public PatternTab L_PatternTab
        {
            get
            {
                if (_L_PatternTab == null) _L_PatternTab = new PatternTab(UiPanel, Instance.GetNode<Godot.Button>("PatternTab"));
                return _L_PatternTab;
            }
        }
        private PatternTab _L_PatternTab;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.ObjectTab
        /// </summary>
        public ObjectTab L_ObjectTab
        {
            get
            {
                if (_L_ObjectTab == null) _L_ObjectTab = new ObjectTab(UiPanel, Instance.GetNode<Godot.Button>("ObjectTab"));
                return _L_ObjectTab;
            }
        }
        private ObjectTab _L_ObjectTab;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.TrapBtn
        /// </summary>
        public TrapBtn L_TrapBtn
        {
            get
            {
                if (_L_TrapBtn == null) _L_TrapBtn = new TrapBtn(UiPanel, Instance.GetNode<Godot.Button>("TrapBtn"));
                return _L_TrapBtn;
            }
        }
        private TrapBtn _L_TrapBtn;

        public VBoxContainer_1(TileSetEditorPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer_1 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer
    /// </summary>
    public class GridContainer : UiNode<TileSetEditorPanel, Godot.ScrollContainer, GridContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.VBoxContainer
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

        public GridContainer(TileSetEditorPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override GridContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<TileSetEditorPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.GridContainer
        /// </summary>
        public GridContainer L_GridContainer
        {
            get
            {
                if (_L_GridContainer == null) _L_GridContainer = new GridContainer(UiPanel, Instance.GetNode<Godot.ScrollContainer>("GridContainer"));
                return _L_GridContainer;
            }
        }
        private GridContainer _L_GridContainer;

        public MarginContainer(TileSetEditorPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot
    /// </summary>
    public class LeftRoot : UiNode<TileSetEditorPanel, Godot.Panel, LeftRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.MarginContainer
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

        public LeftRoot(TileSetEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override LeftRoot Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.ImportButton
    /// </summary>
    public class ImportButton : UiNode<TileSetEditorPanel, Godot.Button, ImportButton>
    {
        public ImportButton(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ImportButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.ImportLabel
    /// </summary>
    public class ImportLabel : UiNode<TileSetEditorPanel, Godot.Label, ImportLabel>
    {
        public ImportLabel(TileSetEditorPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override ImportLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.ImportIcon
    /// </summary>
    public class ImportIcon : UiNode<TileSetEditorPanel, Godot.TextureRect, ImportIcon>
    {
        public ImportIcon(TileSetEditorPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override ImportIcon Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.Control.ImportPreview
    /// </summary>
    public class ImportPreview : UiNode<TileSetEditorPanel, Godot.Sprite2D, ImportPreview>
    {
        public ImportPreview(TileSetEditorPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override ImportPreview Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.Control
    /// </summary>
    public class Control : UiNode<TileSetEditorPanel, Godot.Control, Control>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.ImportPreview
        /// </summary>
        public ImportPreview L_ImportPreview
        {
            get
            {
                if (_L_ImportPreview == null) _L_ImportPreview = new ImportPreview(UiPanel, Instance.GetNode<Godot.Sprite2D>("ImportPreview"));
                return _L_ImportPreview;
            }
        }
        private ImportPreview _L_ImportPreview;

        public Control(TileSetEditorPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override Control Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditor.TileSetEditorImportRoot"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot
    /// </summary>
    public class ImportRoot : UiNode<TileSetEditorPanel, UI.TileSetEditor.TileSetEditorImportRoot, ImportRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportButton
        /// </summary>
        public ImportButton L_ImportButton
        {
            get
            {
                if (_L_ImportButton == null) _L_ImportButton = new ImportButton(UiPanel, Instance.GetNode<Godot.Button>("ImportButton"));
                return _L_ImportButton;
            }
        }
        private ImportButton _L_ImportButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportLabel
        /// </summary>
        public ImportLabel L_ImportLabel
        {
            get
            {
                if (_L_ImportLabel == null) _L_ImportLabel = new ImportLabel(UiPanel, Instance.GetNode<Godot.Label>("ImportLabel"));
                return _L_ImportLabel;
            }
        }
        private ImportLabel _L_ImportLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportIcon
        /// </summary>
        public ImportIcon L_ImportIcon
        {
            get
            {
                if (_L_ImportIcon == null) _L_ImportIcon = new ImportIcon(UiPanel, Instance.GetNode<Godot.TextureRect>("ImportIcon"));
                return _L_ImportIcon;
            }
        }
        private ImportIcon _L_ImportIcon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.Control
        /// </summary>
        public Control L_Control
        {
            get
            {
                if (_L_Control == null) _L_Control = new Control(UiPanel, Instance.GetNode<Godot.Control>("Control"));
                return _L_Control;
            }
        }
        private Control _L_Control;

        public ImportRoot(TileSetEditorPanel uiPanel, UI.TileSetEditor.TileSetEditorImportRoot node) : base(uiPanel, node) {  }
        public override ImportRoot Clone() => new (UiPanel, (UI.TileSetEditor.TileSetEditorImportRoot)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot
    /// </summary>
    public class RightRoot : UiNode<TileSetEditorPanel, Godot.Panel, RightRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditor.TileSetEditorImportRoot"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.ImportRoot
        /// </summary>
        public ImportRoot L_ImportRoot
        {
            get
            {
                if (_L_ImportRoot == null) _L_ImportRoot = new ImportRoot(UiPanel, Instance.GetNode<UI.TileSetEditor.TileSetEditorImportRoot>("ImportRoot"));
                return _L_ImportRoot;
            }
        }
        private ImportRoot _L_ImportRoot;

        public RightRoot(TileSetEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override RightRoot Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<TileSetEditorPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.LeftRoot
        /// </summary>
        public LeftRoot L_LeftRoot
        {
            get
            {
                if (_L_LeftRoot == null) _L_LeftRoot = new LeftRoot(UiPanel, Instance.GetNode<Godot.Panel>("LeftRoot"));
                return _L_LeftRoot;
            }
        }
        private LeftRoot _L_LeftRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.RightRoot
        /// </summary>
        public RightRoot L_RightRoot
        {
            get
            {
                if (_L_RightRoot == null) _L_RightRoot = new RightRoot(UiPanel, Instance.GetNode<Godot.Panel>("RightRoot"));
                return _L_RightRoot;
            }
        }
        private RightRoot _L_RightRoot;

        public HBoxContainer(TileSetEditorPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: TileSetEditor.Bg.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<TileSetEditorPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.Head
        /// </summary>
        public Head L_Head
        {
            get
            {
                if (_L_Head == null) _L_Head = new Head(UiPanel, Instance.GetNode<Godot.Panel>("Head"));
                return _L_Head;
            }
        }
        private Head _L_Head;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: TileSetEditor.Bg.HBoxContainer
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

        public VBoxContainer(TileSetEditorPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditor.Bg
    /// </summary>
    public class Bg : UiNode<TileSetEditorPanel, Godot.Panel, Bg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: TileSetEditor.VBoxContainer
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

        public Bg(TileSetEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Head.Back
    /// </summary>
    public Back S_Back => L_Bg.L_VBoxContainer.L_Head.L_Back;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Head.Save
    /// </summary>
    public Save S_Save => L_Bg.L_VBoxContainer.L_Head.L_Save;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Head.Title
    /// </summary>
    public Title S_Title => L_Bg.L_VBoxContainer.L_Head.L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Head
    /// </summary>
    public Head S_Head => L_Bg.L_VBoxContainer.L_Head;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.ImportTab
    /// </summary>
    public ImportTab S_ImportTab => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_GridContainer.L_VBoxContainer.L_ImportTab;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.TileCellTab
    /// </summary>
    public TileCellTab S_TileCellTab => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_GridContainer.L_VBoxContainer.L_TileCellTab;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.TerrainTab
    /// </summary>
    public TerrainTab S_TerrainTab => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_GridContainer.L_VBoxContainer.L_TerrainTab;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.PatternTab
    /// </summary>
    public PatternTab S_PatternTab => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_GridContainer.L_VBoxContainer.L_PatternTab;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.ObjectTab
    /// </summary>
    public ObjectTab S_ObjectTab => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_GridContainer.L_VBoxContainer.L_ObjectTab;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer.VBoxContainer.TrapBtn
    /// </summary>
    public TrapBtn S_TrapBtn => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_GridContainer.L_VBoxContainer.L_TrapBtn;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.GridContainer
    /// </summary>
    public GridContainer S_GridContainer => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_GridContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot
    /// </summary>
    public LeftRoot S_LeftRoot => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.ImportButton
    /// </summary>
    public ImportButton S_ImportButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_RightRoot.L_ImportRoot.L_ImportButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.ImportLabel
    /// </summary>
    public ImportLabel S_ImportLabel => L_Bg.L_VBoxContainer.L_HBoxContainer.L_RightRoot.L_ImportRoot.L_ImportLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.ImportIcon
    /// </summary>
    public ImportIcon S_ImportIcon => L_Bg.L_VBoxContainer.L_HBoxContainer.L_RightRoot.L_ImportRoot.L_ImportIcon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.Control.ImportPreview
    /// </summary>
    public ImportPreview S_ImportPreview => L_Bg.L_VBoxContainer.L_HBoxContainer.L_RightRoot.L_ImportRoot.L_Control.L_ImportPreview;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot.Control
    /// </summary>
    public Control S_Control => L_Bg.L_VBoxContainer.L_HBoxContainer.L_RightRoot.L_ImportRoot.L_Control;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditor.TileSetEditorImportRoot"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot.ImportRoot
    /// </summary>
    public ImportRoot S_ImportRoot => L_Bg.L_VBoxContainer.L_HBoxContainer.L_RightRoot.L_ImportRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot
    /// </summary>
    public RightRoot S_RightRoot => L_Bg.L_VBoxContainer.L_HBoxContainer.L_RightRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_Bg.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

}
