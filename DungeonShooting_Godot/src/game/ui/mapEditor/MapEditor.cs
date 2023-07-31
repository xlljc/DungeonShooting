namespace UI.MapEditor;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditor : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg
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


    public MapEditor() : base(nameof(MapEditor))
    {
    }

    public sealed override void OnInitNestedUi()
    {
        var inst1 = L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_CanvasLayer.L_MapEditorTools.Instance;
        RecordNestedUi(inst1, UiManager.RecordType.Open);
        inst1.OnCreateUi();
        inst1.OnInitNestedUi();

    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditor.Bg.VBoxContainer.Head.Back
    /// </summary>
    public class Back : UiNode<MapEditor, Godot.Button, Back>
    {
        public Back(MapEditor uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg.VBoxContainer.Head
    /// </summary>
    public class Head : UiNode<MapEditor, Godot.Panel, Head>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.Back
        /// </summary>
        public Back L_Back
        {
            get
            {
                if (_L_Back == null) _L_Back = new Back(UiPanel, Instance.GetNodeOrNull<Godot.Button>("Back"));
                return _L_Back;
            }
        }
        private Back _L_Back;

        public Head(MapEditor uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Head Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.AnimationPlayer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCell.ErrorCellAnimationPlayer
    /// </summary>
    public class ErrorCellAnimationPlayer : UiNode<MapEditor, Godot.AnimationPlayer, ErrorCellAnimationPlayer>
    {
        public ErrorCellAnimationPlayer(MapEditor uiPanel, Godot.AnimationPlayer node) : base(uiPanel, node) {  }
        public override ErrorCellAnimationPlayer Clone() => new (UiPanel, (Godot.AnimationPlayer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCell
    /// </summary>
    public class ErrorCell : UiNode<MapEditor, Godot.Sprite2D, ErrorCell>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.AnimationPlayer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCellAnimationPlayer
        /// </summary>
        public ErrorCellAnimationPlayer L_ErrorCellAnimationPlayer
        {
            get
            {
                if (_L_ErrorCellAnimationPlayer == null) _L_ErrorCellAnimationPlayer = new ErrorCellAnimationPlayer(UiPanel, Instance.GetNodeOrNull<Godot.AnimationPlayer>("ErrorCellAnimationPlayer"));
                return _L_ErrorCellAnimationPlayer;
            }
        }
        private ErrorCellAnimationPlayer _L_ErrorCellAnimationPlayer;

        public ErrorCell(MapEditor uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override ErrorCell Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node2D"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.TileMap.Brush
    /// </summary>
    public class Brush : UiNode<MapEditor, Godot.Node2D, Brush>
    {
        public Brush(MapEditor uiPanel, Godot.Node2D node) : base(uiPanel, node) {  }
        public override Brush Clone() => new (UiPanel, (Godot.Node2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditor.EditorTileMap"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.TileMap
    /// </summary>
    public class TileMap : UiNode<MapEditor, UI.MapEditor.EditorTileMap, TileMap>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.ErrorCell
        /// </summary>
        public ErrorCell L_ErrorCell
        {
            get
            {
                if (_L_ErrorCell == null) _L_ErrorCell = new ErrorCell(UiPanel, Instance.GetNodeOrNull<Godot.Sprite2D>("ErrorCell"));
                return _L_ErrorCell;
            }
        }
        private ErrorCell _L_ErrorCell;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.Brush
        /// </summary>
        public Brush L_Brush
        {
            get
            {
                if (_L_Brush == null) _L_Brush = new Brush(UiPanel, Instance.GetNodeOrNull<Godot.Node2D>("Brush"));
                return _L_Brush;
            }
        }
        private Brush _L_Brush;

        public TileMap(MapEditor uiPanel, UI.MapEditor.EditorTileMap node) : base(uiPanel, node) {  }
        public override TileMap Clone() => new (UiPanel, (UI.MapEditor.EditorTileMap)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorTools.MapEditorToolsPanel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.CanvasLayer.MapEditorTools
    /// </summary>
    public class MapEditorTools : UiNode<MapEditor, UI.MapEditorTools.MapEditorToolsPanel, MapEditorTools>
    {
        public MapEditorTools(MapEditor uiPanel, UI.MapEditorTools.MapEditorToolsPanel node) : base(uiPanel, node) {  }
        public override MapEditorTools Clone()
        {
            var uiNode = new MapEditorTools(UiPanel, (UI.MapEditorTools.MapEditorToolsPanel)Instance.Duplicate());
            UiPanel.RecordNestedUi(uiNode.Instance, UiManager.RecordType.Open);
            uiNode.Instance.OnCreateUi();
            uiNode.Instance.OnInitNestedUi();
            return uiNode;
        }
    }

    /// <summary>
    /// 类型: <see cref="Godot.CanvasLayer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.CanvasLayer
    /// </summary>
    public class CanvasLayer : UiNode<MapEditor, Godot.CanvasLayer, CanvasLayer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorTools.MapEditorToolsPanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.MapEditorTools
        /// </summary>
        public MapEditorTools L_MapEditorTools
        {
            get
            {
                if (_L_MapEditorTools == null) _L_MapEditorTools = new MapEditorTools(UiPanel, Instance.GetNodeOrNull<UI.MapEditorTools.MapEditorToolsPanel>("MapEditorTools"));
                return _L_MapEditorTools;
            }
        }
        private MapEditorTools _L_MapEditorTools;

        public CanvasLayer(MapEditor uiPanel, Godot.CanvasLayer node) : base(uiPanel, node) {  }
        public override CanvasLayer Clone() => new (UiPanel, (Godot.CanvasLayer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SubViewport"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport
    /// </summary>
    public class SubViewport : UiNode<MapEditor, Godot.SubViewport, SubViewport>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditor.EditorTileMap"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.TileMap
        /// </summary>
        public TileMap L_TileMap
        {
            get
            {
                if (_L_TileMap == null) _L_TileMap = new TileMap(UiPanel, Instance.GetNodeOrNull<UI.MapEditor.EditorTileMap>("TileMap"));
                return _L_TileMap;
            }
        }
        private TileMap _L_TileMap;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CanvasLayer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.CanvasLayer
        /// </summary>
        public CanvasLayer L_CanvasLayer
        {
            get
            {
                if (_L_CanvasLayer == null) _L_CanvasLayer = new CanvasLayer(UiPanel, Instance.GetNodeOrNull<Godot.CanvasLayer>("CanvasLayer"));
                return _L_CanvasLayer;
            }
        }
        private CanvasLayer _L_CanvasLayer;

        public SubViewport(MapEditor uiPanel, Godot.SubViewport node) : base(uiPanel, node) {  }
        public override SubViewport Clone() => new (UiPanel, (Godot.SubViewport)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SubViewportContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView
    /// </summary>
    public class MapView : UiNode<MapEditor, Godot.SubViewportContainer, MapView>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SubViewport"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.SubViewport
        /// </summary>
        public SubViewport L_SubViewport
        {
            get
            {
                if (_L_SubViewport == null) _L_SubViewport = new SubViewport(UiPanel, Instance.GetNodeOrNull<Godot.SubViewport>("SubViewport"));
                return _L_SubViewport;
            }
        }
        private SubViewport _L_SubViewport;

        public MapView(MapEditor uiPanel, Godot.SubViewportContainer node) : base(uiPanel, node) {  }
        public override MapView Clone() => new (UiPanel, (Godot.SubViewportContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditor, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SubViewportContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MapView
        /// </summary>
        public MapView L_MapView
        {
            get
            {
                if (_L_MapView == null) _L_MapView = new MapView(UiPanel, Instance.GetNodeOrNull<Godot.SubViewportContainer>("MapView"));
                return _L_MapView;
            }
        }
        private MapView _L_MapView;

        public MarginContainer(MapEditor uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left
    /// </summary>
    public class Left : UiNode<MapEditor, Godot.Panel, Left>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.MarginContainer
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

        public Left(MapEditor uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Left Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.LayerLabel
    /// </summary>
    public class LayerLabel : UiNode<MapEditor, Godot.Label, LayerLabel>
    {
        public LayerLabel(MapEditor uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override LayerLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.ScrollContainer.LayerButton.VisibleButton
    /// </summary>
    public class VisibleButton : UiNode<MapEditor, Godot.TextureButton, VisibleButton>
    {
        public VisibleButton(MapEditor uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override VisibleButton Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.ScrollContainer.LayerButton
    /// </summary>
    public class LayerButton : UiNode<MapEditor, Godot.Button, LayerButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.ScrollContainer.VisibleButton
        /// </summary>
        public VisibleButton L_VisibleButton
        {
            get
            {
                if (_L_VisibleButton == null) _L_VisibleButton = new VisibleButton(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("VisibleButton"));
                return _L_VisibleButton;
            }
        }
        private VisibleButton _L_VisibleButton;

        public LayerButton(MapEditor uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override LayerButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditor, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.LayerButton
        /// </summary>
        public LayerButton L_LayerButton
        {
            get
            {
                if (_L_LayerButton == null) _L_LayerButton = new LayerButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("LayerButton"));
                return _L_LayerButton;
            }
        }
        private LayerButton _L_LayerButton;

        public ScrollContainer(MapEditor uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer
    /// </summary>
    public class VBoxContainer_1 : UiNode<MapEditor, Godot.VBoxContainer, VBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.LayerLabel
        /// </summary>
        public LayerLabel L_LayerLabel
        {
            get
            {
                if (_L_LayerLabel == null) _L_LayerLabel = new LayerLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("LayerLabel"));
                return _L_LayerLabel;
            }
        }
        private LayerLabel _L_LayerLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.ScrollContainer
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

        public VBoxContainer_1(MapEditor uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer_1 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer
    /// </summary>
    public class MapLayer : UiNode<MapEditor, Godot.MarginContainer, MapLayer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.VBoxContainer
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

        public MapLayer(MapEditor uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MapLayer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MarkLayer.VBoxContainer.MarkLabel
    /// </summary>
    public class MarkLabel : UiNode<MapEditor, Godot.Label, MarkLabel>
    {
        public MarkLabel(MapEditor uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override MarkLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MarkLayer.VBoxContainer
    /// </summary>
    public class VBoxContainer_2 : UiNode<MapEditor, Godot.VBoxContainer, VBoxContainer_2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MarkLayer.MarkLabel
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

        public VBoxContainer_2(MapEditor uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer_2 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MarkLayer
    /// </summary>
    public class MarkLayer : UiNode<MapEditor, Godot.MarginContainer, MarkLayer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.VBoxContainer
        /// </summary>
        public VBoxContainer_2 L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer_2(UiPanel, Instance.GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer_2 _L_VBoxContainer;

        public MarkLayer(MapEditor uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarkLayer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TabContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer
    /// </summary>
    public class TabContainer : UiNode<MapEditor, Godot.TabContainer, TabContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.MapLayer
        /// </summary>
        public MapLayer L_MapLayer
        {
            get
            {
                if (_L_MapLayer == null) _L_MapLayer = new MapLayer(UiPanel, Instance.GetNodeOrNull<Godot.MarginContainer>("MapLayer"));
                return _L_MapLayer;
            }
        }
        private MapLayer _L_MapLayer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.MarkLayer
        /// </summary>
        public MarkLayer L_MarkLayer
        {
            get
            {
                if (_L_MarkLayer == null) _L_MarkLayer = new MarkLayer(UiPanel, Instance.GetNodeOrNull<Godot.MarginContainer>("MarkLayer"));
                return _L_MarkLayer;
            }
        }
        private MarkLayer _L_MarkLayer;

        public TabContainer(MapEditor uiPanel, Godot.TabContainer node) : base(uiPanel, node) {  }
        public override TabContainer Clone() => new (UiPanel, (Godot.TabContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer
    /// </summary>
    public class MarginContainer_1 : UiNode<MapEditor, Godot.MarginContainer, MarginContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TabContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.TabContainer
        /// </summary>
        public TabContainer L_TabContainer
        {
            get
            {
                if (_L_TabContainer == null) _L_TabContainer = new TabContainer(UiPanel, Instance.GetNodeOrNull<Godot.TabContainer>("TabContainer"));
                return _L_TabContainer;
            }
        }
        private TabContainer _L_TabContainer;

        public MarginContainer_1(MapEditor uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_1 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right
    /// </summary>
    public class Right : UiNode<MapEditor, Godot.Panel, Right>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.MarginContainer
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

        public Right(MapEditor uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Right Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HSplitContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer
    /// </summary>
    public class HSplitContainer : UiNode<MapEditor, Godot.HSplitContainer, HSplitContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.Left
        /// </summary>
        public Left L_Left
        {
            get
            {
                if (_L_Left == null) _L_Left = new Left(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Left"));
                return _L_Left;
            }
        }
        private Left _L_Left;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.Right
        /// </summary>
        public Right L_Right
        {
            get
            {
                if (_L_Right == null) _L_Right = new Right(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Right"));
                return _L_Right;
            }
        }
        private Right _L_Right;

        public HSplitContainer(MapEditor uiPanel, Godot.HSplitContainer node) : base(uiPanel, node) {  }
        public override HSplitContainer Clone() => new (UiPanel, (Godot.HSplitContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditor.Bg.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditor, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.Head
        /// </summary>
        public Head L_Head
        {
            get
            {
                if (_L_Head == null) _L_Head = new Head(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Head"));
                return _L_Head;
            }
        }
        private Head _L_Head;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: MapEditor.Bg.HSplitContainer
        /// </summary>
        public HSplitContainer L_HSplitContainer
        {
            get
            {
                if (_L_HSplitContainer == null) _L_HSplitContainer = new HSplitContainer(UiPanel, Instance.GetNodeOrNull<Godot.HSplitContainer>("HSplitContainer"));
                return _L_HSplitContainer;
            }
        }
        private HSplitContainer _L_HSplitContainer;

        public VBoxContainer(MapEditor uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg
    /// </summary>
    public class Bg : UiNode<MapEditor, Godot.Panel, Bg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditor.VBoxContainer
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

        public Bg(MapEditor uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.Head.Back
    /// </summary>
    public Back S_Back => L_Bg.L_VBoxContainer.L_Head.L_Back;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.Head
    /// </summary>
    public Head S_Head => L_Bg.L_VBoxContainer.L_Head;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.AnimationPlayer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCell.ErrorCellAnimationPlayer
    /// </summary>
    public ErrorCellAnimationPlayer S_ErrorCellAnimationPlayer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap.L_ErrorCell.L_ErrorCellAnimationPlayer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCell
    /// </summary>
    public ErrorCell S_ErrorCell => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap.L_ErrorCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.TileMap.Brush
    /// </summary>
    public Brush S_Brush => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap.L_Brush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditor.EditorTileMap"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.TileMap
    /// </summary>
    public TileMap S_TileMap => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorTools.MapEditorToolsPanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.CanvasLayer.MapEditorTools
    /// </summary>
    public MapEditorTools S_MapEditorTools => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_CanvasLayer.L_MapEditorTools;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CanvasLayer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport.CanvasLayer
    /// </summary>
    public CanvasLayer S_CanvasLayer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_CanvasLayer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SubViewport"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView.SubViewport
    /// </summary>
    public SubViewport S_SubViewport => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SubViewportContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left.MarginContainer.MapView
    /// </summary>
    public MapView S_MapView => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left.L_MarginContainer.L_MapView;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Left
    /// </summary>
    public Left S_Left => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Left;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.LayerLabel
    /// </summary>
    public LayerLabel S_LayerLabel => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Right.L_MarginContainer.L_TabContainer.L_MapLayer.L_VBoxContainer.L_LayerLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.ScrollContainer.LayerButton.VisibleButton
    /// </summary>
    public VisibleButton S_VisibleButton => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Right.L_MarginContainer.L_TabContainer.L_MapLayer.L_VBoxContainer.L_ScrollContainer.L_LayerButton.L_VisibleButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.ScrollContainer.LayerButton
    /// </summary>
    public LayerButton S_LayerButton => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Right.L_MarginContainer.L_TabContainer.L_MapLayer.L_VBoxContainer.L_ScrollContainer.L_LayerButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer.VBoxContainer.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Right.L_MarginContainer.L_TabContainer.L_MapLayer.L_VBoxContainer.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MapLayer
    /// </summary>
    public MapLayer S_MapLayer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Right.L_MarginContainer.L_TabContainer.L_MapLayer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MarkLayer.VBoxContainer.MarkLabel
    /// </summary>
    public MarkLabel S_MarkLabel => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Right.L_MarginContainer.L_TabContainer.L_MarkLayer.L_VBoxContainer.L_MarkLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer.MarkLayer
    /// </summary>
    public MarkLayer S_MarkLayer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Right.L_MarginContainer.L_TabContainer.L_MarkLayer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TabContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right.MarginContainer.TabContainer
    /// </summary>
    public TabContainer S_TabContainer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Right.L_MarginContainer.L_TabContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right
    /// </summary>
    public Right S_Right => L_Bg.L_VBoxContainer.L_HSplitContainer.L_Right;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer
    /// </summary>
    public HSplitContainer S_HSplitContainer => L_Bg.L_VBoxContainer.L_HSplitContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

}
