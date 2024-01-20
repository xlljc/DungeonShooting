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
            if (_L_Bg == null) _L_Bg = new Bg((MapEditorPanel)this, GetNode<Godot.Panel>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;


    public MapEditor() : base(nameof(MapEditor))
    {
    }

    public sealed override void OnInitNestedUi()
    {
        _ = L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap;

        var inst1 = L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_LayerPanel;
        RecordNestedUi(inst1.L_MapEditorMapLayer.Instance, inst1, UiManager.RecordType.Open);
        inst1.L_MapEditorMapLayer.Instance.OnCreateUi();
        inst1.L_MapEditorMapLayer.Instance.OnInitNestedUi();

        var inst2 = L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_CanvasLayer;
        RecordNestedUi(inst2.L_MapEditorTools.Instance, inst2, UiManager.RecordType.Open);
        inst2.L_MapEditorTools.Instance.OnCreateUi();
        inst2.L_MapEditorTools.Instance.OnInitNestedUi();

        var inst3 = L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_Right.L_MarginContainer.L_TabContainer.L_MapTile;
        RecordNestedUi(inst3.L_MapEditorMapTile.Instance, inst3, UiManager.RecordType.Open);
        inst3.L_MapEditorMapTile.Instance.OnCreateUi();
        inst3.L_MapEditorMapTile.Instance.OnInitNestedUi();

        var inst4 = L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_Right.L_MarginContainer.L_TabContainer.L_MapMark;
        RecordNestedUi(inst4.L_MapEditorMapMark.Instance, inst4, UiManager.RecordType.Open);
        inst4.L_MapEditorMapMark.Instance.OnCreateUi();
        inst4.L_MapEditorMapMark.Instance.OnInitNestedUi();

    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditor.Bg.VBoxContainer.Head.Back
    /// </summary>
    public class Back : UiNode<MapEditorPanel, Godot.Button, Back>
    {
        public Back(MapEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditor.Bg.VBoxContainer.Head.Save
    /// </summary>
    public class Save : UiNode<MapEditorPanel, Godot.Button, Save>
    {
        public Save(MapEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Save Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditor.Bg.VBoxContainer.Head.Title
    /// </summary>
    public class Title : UiNode<MapEditorPanel, Godot.Label, Title>
    {
        public Title(MapEditorPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditor.Bg.VBoxContainer.Head.Play
    /// </summary>
    public class Play : UiNode<MapEditorPanel, Godot.Button, Play>
    {
        public Play(MapEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Play Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditor.Bg.VBoxContainer.Head.PlaySetting
    /// </summary>
    public class PlaySetting : UiNode<MapEditorPanel, Godot.Button, PlaySetting>
    {
        public PlaySetting(MapEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override PlaySetting Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg.VBoxContainer.Head
    /// </summary>
    public class Head : UiNode<MapEditorPanel, Godot.Panel, Head>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.Back
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.Save
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditor.Bg.VBoxContainer.Title
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.Play
        /// </summary>
        public Play L_Play
        {
            get
            {
                if (_L_Play == null) _L_Play = new Play(UiPanel, Instance.GetNode<Godot.Button>("Play"));
                return _L_Play;
            }
        }
        private Play _L_Play;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.PlaySetting
        /// </summary>
        public PlaySetting L_PlaySetting
        {
            get
            {
                if (_L_PlaySetting == null) _L_PlaySetting = new PlaySetting(UiPanel, Instance.GetNode<Godot.Button>("PlaySetting"));
                return _L_PlaySetting;
            }
        }
        private PlaySetting _L_PlaySetting;

        public Head(MapEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Head Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorMapLayer.MapEditorMapLayerPanel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.LayerPanel.MapEditorMapLayer
    /// </summary>
    public class MapEditorMapLayer : UiNode<MapEditorPanel, UI.MapEditorMapLayer.MapEditorMapLayerPanel, MapEditorMapLayer>
    {
        public MapEditorMapLayer(MapEditorPanel uiPanel, UI.MapEditorMapLayer.MapEditorMapLayerPanel node) : base(uiPanel, node) {  }
        public override MapEditorMapLayer Clone()
        {
            var uiNode = new MapEditorMapLayer(UiPanel, (UI.MapEditorMapLayer.MapEditorMapLayerPanel)Instance.Duplicate());
            UiPanel.RecordNestedUi(uiNode.Instance, this, UiManager.RecordType.Open);
            uiNode.Instance.OnCreateUi();
            uiNode.Instance.OnInitNestedUi();
            return uiNode;
        }
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.LayerPanel
    /// </summary>
    public class LayerPanel : UiNode<MapEditorPanel, Godot.Panel, LayerPanel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorMapLayer.MapEditorMapLayerPanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.MapEditorMapLayer
        /// </summary>
        public MapEditorMapLayer L_MapEditorMapLayer
        {
            get
            {
                if (_L_MapEditorMapLayer == null) _L_MapEditorMapLayer = new MapEditorMapLayer(UiPanel, Instance.GetNode<UI.MapEditorMapLayer.MapEditorMapLayerPanel>("MapEditorMapLayer"));
                return _L_MapEditorMapLayer;
            }
        }
        private MapEditorMapLayer _L_MapEditorMapLayer;

        public LayerPanel(MapEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override LayerPanel Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NavigationRegion2D"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap.NavigationRegion
    /// </summary>
    public class NavigationRegion : UiNode<MapEditorPanel, Godot.NavigationRegion2D, NavigationRegion>
    {
        public NavigationRegion(MapEditorPanel uiPanel, Godot.NavigationRegion2D node) : base(uiPanel, node) {  }
        public override NavigationRegion Clone() => new (UiPanel, (Godot.NavigationRegion2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.AnimationPlayer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCell.ErrorCellAnimationPlayer
    /// </summary>
    public class ErrorCellAnimationPlayer : UiNode<MapEditorPanel, Godot.AnimationPlayer, ErrorCellAnimationPlayer>
    {
        public ErrorCellAnimationPlayer(MapEditorPanel uiPanel, Godot.AnimationPlayer node) : base(uiPanel, node) {  }
        public override ErrorCellAnimationPlayer Clone() => new (UiPanel, (Godot.AnimationPlayer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCell
    /// </summary>
    public class ErrorCell : UiNode<MapEditorPanel, Godot.Sprite2D, ErrorCell>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.AnimationPlayer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCellAnimationPlayer
        /// </summary>
        public ErrorCellAnimationPlayer L_ErrorCellAnimationPlayer
        {
            get
            {
                if (_L_ErrorCellAnimationPlayer == null) _L_ErrorCellAnimationPlayer = new ErrorCellAnimationPlayer(UiPanel, Instance.GetNode<Godot.AnimationPlayer>("ErrorCellAnimationPlayer"));
                return _L_ErrorCellAnimationPlayer;
            }
        }
        private ErrorCellAnimationPlayer _L_ErrorCellAnimationPlayer;

        public ErrorCell(MapEditorPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override ErrorCell Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node2D"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap.Brush
    /// </summary>
    public class Brush : UiNode<MapEditorPanel, Godot.Node2D, Brush>
    {
        public Brush(MapEditorPanel uiPanel, Godot.Node2D node) : base(uiPanel, node) {  }
        public override Brush Clone() => new (UiPanel, (Godot.Node2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditor.EditorTileMap"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap
    /// </summary>
    public class TileMap : UiNode<MapEditorPanel, UI.MapEditor.EditorTileMap, TileMap>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NavigationRegion2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.NavigationRegion
        /// </summary>
        public NavigationRegion L_NavigationRegion
        {
            get
            {
                if (_L_NavigationRegion == null) _L_NavigationRegion = new NavigationRegion(UiPanel, Instance.GetNode<Godot.NavigationRegion2D>("NavigationRegion"));
                return _L_NavigationRegion;
            }
        }
        private NavigationRegion _L_NavigationRegion;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.ErrorCell
        /// </summary>
        public ErrorCell L_ErrorCell
        {
            get
            {
                if (_L_ErrorCell == null) _L_ErrorCell = new ErrorCell(UiPanel, Instance.GetNode<Godot.Sprite2D>("ErrorCell"));
                return _L_ErrorCell;
            }
        }
        private ErrorCell _L_ErrorCell;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.Brush
        /// </summary>
        public Brush L_Brush
        {
            get
            {
                if (_L_Brush == null) _L_Brush = new Brush(UiPanel, Instance.GetNode<Godot.Node2D>("Brush"));
                return _L_Brush;
            }
        }
        private Brush _L_Brush;

        public TileMap(MapEditorPanel uiPanel, UI.MapEditor.EditorTileMap node) : base(uiPanel, node) {  }
        public override TileMap Clone() => new (UiPanel, (UI.MapEditor.EditorTileMap)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorTools.MapEditorToolsPanel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.CanvasLayer.MapEditorTools
    /// </summary>
    public class MapEditorTools : UiNode<MapEditorPanel, UI.MapEditorTools.MapEditorToolsPanel, MapEditorTools>
    {
        public MapEditorTools(MapEditorPanel uiPanel, UI.MapEditorTools.MapEditorToolsPanel node) : base(uiPanel, node) {  }
        public override MapEditorTools Clone()
        {
            var uiNode = new MapEditorTools(UiPanel, (UI.MapEditorTools.MapEditorToolsPanel)Instance.Duplicate());
            UiPanel.RecordNestedUi(uiNode.Instance, this, UiManager.RecordType.Open);
            uiNode.Instance.OnCreateUi();
            uiNode.Instance.OnInitNestedUi();
            return uiNode;
        }
    }

    /// <summary>
    /// 类型: <see cref="Godot.CanvasLayer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.CanvasLayer
    /// </summary>
    public class CanvasLayer : UiNode<MapEditorPanel, Godot.CanvasLayer, CanvasLayer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorTools.MapEditorToolsPanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.MapEditorTools
        /// </summary>
        public MapEditorTools L_MapEditorTools
        {
            get
            {
                if (_L_MapEditorTools == null) _L_MapEditorTools = new MapEditorTools(UiPanel, Instance.GetNode<UI.MapEditorTools.MapEditorToolsPanel>("MapEditorTools"));
                return _L_MapEditorTools;
            }
        }
        private MapEditorTools _L_MapEditorTools;

        public CanvasLayer(MapEditorPanel uiPanel, Godot.CanvasLayer node) : base(uiPanel, node) {  }
        public override CanvasLayer Clone() => new (UiPanel, (Godot.CanvasLayer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SubViewport"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport
    /// </summary>
    public class SubViewport : UiNode<MapEditorPanel, Godot.SubViewport, SubViewport>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditor.EditorTileMap"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.TileMap
        /// </summary>
        public TileMap L_TileMap
        {
            get
            {
                if (_L_TileMap == null) _L_TileMap = new TileMap(UiPanel, Instance.GetNode<UI.MapEditor.EditorTileMap>("TileMap"));
                return _L_TileMap;
            }
        }
        private TileMap _L_TileMap;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CanvasLayer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.CanvasLayer
        /// </summary>
        public CanvasLayer L_CanvasLayer
        {
            get
            {
                if (_L_CanvasLayer == null) _L_CanvasLayer = new CanvasLayer(UiPanel, Instance.GetNode<Godot.CanvasLayer>("CanvasLayer"));
                return _L_CanvasLayer;
            }
        }
        private CanvasLayer _L_CanvasLayer;

        public SubViewport(MapEditorPanel uiPanel, Godot.SubViewport node) : base(uiPanel, node) {  }
        public override SubViewport Clone() => new (UiPanel, (Godot.SubViewport)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SubViewportContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView
    /// </summary>
    public class MapView : UiNode<MapEditorPanel, Godot.SubViewportContainer, MapView>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SubViewport"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.SubViewport
        /// </summary>
        public SubViewport L_SubViewport
        {
            get
            {
                if (_L_SubViewport == null) _L_SubViewport = new SubViewport(UiPanel, Instance.GetNode<Godot.SubViewport>("SubViewport"));
                return _L_SubViewport;
            }
        }
        private SubViewport _L_SubViewport;

        public MapView(MapEditorPanel uiPanel, Godot.SubViewportContainer node) : base(uiPanel, node) {  }
        public override MapView Clone() => new (UiPanel, (Godot.SubViewportContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView2
    /// </summary>
    public class MapView2 : UiNode<MapEditorPanel, Godot.TextureRect, MapView2>
    {
        public MapView2(MapEditorPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override MapView2 Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SubViewportContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MapView
        /// </summary>
        public MapView L_MapView
        {
            get
            {
                if (_L_MapView == null) _L_MapView = new MapView(UiPanel, Instance.GetNode<Godot.SubViewportContainer>("MapView"));
                return _L_MapView;
            }
        }
        private MapView _L_MapView;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MapView2
        /// </summary>
        public MapView2 L_MapView2
        {
            get
            {
                if (_L_MapView2 == null) _L_MapView2 = new MapView2(UiPanel, Instance.GetNode<Godot.TextureRect>("MapView2"));
                return _L_MapView2;
            }
        }
        private MapView2 _L_MapView2;

        public MarginContainer(MapEditorPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left
    /// </summary>
    public class Left : UiNode<MapEditorPanel, Godot.Panel, Left>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.MarginContainer
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

        public Left(MapEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Left Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.LayerPanel
        /// </summary>
        public LayerPanel L_LayerPanel
        {
            get
            {
                if (_L_LayerPanel == null) _L_LayerPanel = new LayerPanel(UiPanel, Instance.GetNode<Godot.Panel>("LayerPanel"));
                return _L_LayerPanel;
            }
        }
        private LayerPanel _L_LayerPanel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Left
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

        public HBoxContainer(MapEditorPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorMapTile.MapEditorMapTilePanel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapTile.MapEditorMapTile
    /// </summary>
    public class MapEditorMapTile : UiNode<MapEditorPanel, UI.MapEditorMapTile.MapEditorMapTilePanel, MapEditorMapTile>
    {
        public MapEditorMapTile(MapEditorPanel uiPanel, UI.MapEditorMapTile.MapEditorMapTilePanel node) : base(uiPanel, node) {  }
        public override MapEditorMapTile Clone()
        {
            var uiNode = new MapEditorMapTile(UiPanel, (UI.MapEditorMapTile.MapEditorMapTilePanel)Instance.Duplicate());
            UiPanel.RecordNestedUi(uiNode.Instance, this, UiManager.RecordType.Open);
            uiNode.Instance.OnCreateUi();
            uiNode.Instance.OnInitNestedUi();
            return uiNode;
        }
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapTile
    /// </summary>
    public class MapTile : UiNode<MapEditorPanel, Godot.MarginContainer, MapTile>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorMapTile.MapEditorMapTilePanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapEditorMapTile
        /// </summary>
        public MapEditorMapTile L_MapEditorMapTile
        {
            get
            {
                if (_L_MapEditorMapTile == null) _L_MapEditorMapTile = new MapEditorMapTile(UiPanel, Instance.GetNode<UI.MapEditorMapTile.MapEditorMapTilePanel>("MapEditorMapTile"));
                return _L_MapEditorMapTile;
            }
        }
        private MapEditorMapTile _L_MapEditorMapTile;

        public MapTile(MapEditorPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MapTile Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorMapMark.MapEditorMapMarkPanel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapMark.MapEditorMapMark
    /// </summary>
    public class MapEditorMapMark : UiNode<MapEditorPanel, UI.MapEditorMapMark.MapEditorMapMarkPanel, MapEditorMapMark>
    {
        public MapEditorMapMark(MapEditorPanel uiPanel, UI.MapEditorMapMark.MapEditorMapMarkPanel node) : base(uiPanel, node) {  }
        public override MapEditorMapMark Clone()
        {
            var uiNode = new MapEditorMapMark(UiPanel, (UI.MapEditorMapMark.MapEditorMapMarkPanel)Instance.Duplicate());
            UiPanel.RecordNestedUi(uiNode.Instance, this, UiManager.RecordType.Open);
            uiNode.Instance.OnCreateUi();
            uiNode.Instance.OnInitNestedUi();
            return uiNode;
        }
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapMark
    /// </summary>
    public class MapMark : UiNode<MapEditorPanel, Godot.MarginContainer, MapMark>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorMapMark.MapEditorMapMarkPanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapEditorMapMark
        /// </summary>
        public MapEditorMapMark L_MapEditorMapMark
        {
            get
            {
                if (_L_MapEditorMapMark == null) _L_MapEditorMapMark = new MapEditorMapMark(UiPanel, Instance.GetNode<UI.MapEditorMapMark.MapEditorMapMarkPanel>("MapEditorMapMark"));
                return _L_MapEditorMapMark;
            }
        }
        private MapEditorMapMark _L_MapEditorMapMark;

        public MapMark(MapEditorPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MapMark Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TabContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer
    /// </summary>
    public class TabContainer : UiNode<MapEditorPanel, Godot.TabContainer, TabContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.MapTile
        /// </summary>
        public MapTile L_MapTile
        {
            get
            {
                if (_L_MapTile == null) _L_MapTile = new MapTile(UiPanel, Instance.GetNode<Godot.MarginContainer>("MapTile"));
                return _L_MapTile;
            }
        }
        private MapTile _L_MapTile;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.MapMark
        /// </summary>
        public MapMark L_MapMark
        {
            get
            {
                if (_L_MapMark == null) _L_MapMark = new MapMark(UiPanel, Instance.GetNode<Godot.MarginContainer>("MapMark"));
                return _L_MapMark;
            }
        }
        private MapMark _L_MapMark;

        public TabContainer(MapEditorPanel uiPanel, Godot.TabContainer node) : base(uiPanel, node) {  }
        public override TabContainer Clone() => new (UiPanel, (Godot.TabContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer
    /// </summary>
    public class MarginContainer_1 : UiNode<MapEditorPanel, Godot.MarginContainer, MarginContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TabContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.TabContainer
        /// </summary>
        public TabContainer L_TabContainer
        {
            get
            {
                if (_L_TabContainer == null) _L_TabContainer = new TabContainer(UiPanel, Instance.GetNode<Godot.TabContainer>("TabContainer"));
                return _L_TabContainer;
            }
        }
        private TabContainer _L_TabContainer;

        public MarginContainer_1(MapEditorPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer_1 Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right
    /// </summary>
    public class Right : UiNode<MapEditorPanel, Godot.Panel, Right>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.MarginContainer
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

        public Right(MapEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Right Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HSplitContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2
    /// </summary>
    public class HSplitContainer2 : UiNode<MapEditorPanel, Godot.HSplitContainer, HSplitContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HBoxContainer
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.Right
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

        public HSplitContainer2(MapEditorPanel uiPanel, Godot.HSplitContainer node) : base(uiPanel, node) {  }
        public override HSplitContainer2 Clone() => new (UiPanel, (Godot.HSplitContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditor.Bg.VBoxContainer.HSplitContainer
    /// </summary>
    public class HSplitContainer : UiNode<MapEditorPanel, Godot.HBoxContainer, HSplitContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer2
        /// </summary>
        public HSplitContainer2 L_HSplitContainer2
        {
            get
            {
                if (_L_HSplitContainer2 == null) _L_HSplitContainer2 = new HSplitContainer2(UiPanel, Instance.GetNode<Godot.HSplitContainer>("HSplitContainer2"));
                return _L_HSplitContainer2;
            }
        }
        private HSplitContainer2 _L_HSplitContainer2;

        public HSplitContainer(MapEditorPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HSplitContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditor.Bg.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.Head
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditor.Bg.HSplitContainer
        /// </summary>
        public HSplitContainer L_HSplitContainer
        {
            get
            {
                if (_L_HSplitContainer == null) _L_HSplitContainer = new HSplitContainer(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HSplitContainer"));
                return _L_HSplitContainer;
            }
        }
        private HSplitContainer _L_HSplitContainer;

        public VBoxContainer(MapEditorPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg
    /// </summary>
    public class Bg : UiNode<MapEditorPanel, Godot.Panel, Bg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditor.VBoxContainer
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

        public Bg(MapEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.Head.Back
    /// </summary>
    public Back S_Back => L_Bg.L_VBoxContainer.L_Head.L_Back;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.Head.Save
    /// </summary>
    public Save S_Save => L_Bg.L_VBoxContainer.L_Head.L_Save;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditor.Bg.VBoxContainer.Head.Title
    /// </summary>
    public Title S_Title => L_Bg.L_VBoxContainer.L_Head.L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.Head.Play
    /// </summary>
    public Play S_Play => L_Bg.L_VBoxContainer.L_Head.L_Play;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditor.Bg.VBoxContainer.Head.PlaySetting
    /// </summary>
    public PlaySetting S_PlaySetting => L_Bg.L_VBoxContainer.L_Head.L_PlaySetting;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.Head
    /// </summary>
    public Head S_Head => L_Bg.L_VBoxContainer.L_Head;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorMapLayer.MapEditorMapLayerPanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.LayerPanel.MapEditorMapLayer
    /// </summary>
    public MapEditorMapLayer S_MapEditorMapLayer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_LayerPanel.L_MapEditorMapLayer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.LayerPanel
    /// </summary>
    public LayerPanel S_LayerPanel => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_LayerPanel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NavigationRegion2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap.NavigationRegion
    /// </summary>
    public NavigationRegion S_NavigationRegion => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap.L_NavigationRegion;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.AnimationPlayer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCell.ErrorCellAnimationPlayer
    /// </summary>
    public ErrorCellAnimationPlayer S_ErrorCellAnimationPlayer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap.L_ErrorCell.L_ErrorCellAnimationPlayer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap.ErrorCell
    /// </summary>
    public ErrorCell S_ErrorCell => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap.L_ErrorCell;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap.Brush
    /// </summary>
    public Brush S_Brush => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap.L_Brush;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditor.EditorTileMap"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.TileMap
    /// </summary>
    public TileMap S_TileMap => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_TileMap;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorTools.MapEditorToolsPanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.CanvasLayer.MapEditorTools
    /// </summary>
    public MapEditorTools S_MapEditorTools => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_CanvasLayer.L_MapEditorTools;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CanvasLayer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport.CanvasLayer
    /// </summary>
    public CanvasLayer S_CanvasLayer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport.L_CanvasLayer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SubViewport"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView.SubViewport
    /// </summary>
    public SubViewport S_SubViewport => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView.L_SubViewport;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SubViewportContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView
    /// </summary>
    public MapView S_MapView => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left.MarginContainer.MapView2
    /// </summary>
    public MapView2 S_MapView2 => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left.L_MarginContainer.L_MapView2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer.Left
    /// </summary>
    public Left S_Left => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer.L_Left;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorMapTile.MapEditorMapTilePanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapTile.MapEditorMapTile
    /// </summary>
    public MapEditorMapTile S_MapEditorMapTile => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_Right.L_MarginContainer.L_TabContainer.L_MapTile.L_MapEditorMapTile;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapTile
    /// </summary>
    public MapTile S_MapTile => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_Right.L_MarginContainer.L_TabContainer.L_MapTile;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorMapMark.MapEditorMapMarkPanel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapMark.MapEditorMapMark
    /// </summary>
    public MapEditorMapMark S_MapEditorMapMark => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_Right.L_MarginContainer.L_TabContainer.L_MapMark.L_MapEditorMapMark;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer.MapMark
    /// </summary>
    public MapMark S_MapMark => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_Right.L_MarginContainer.L_TabContainer.L_MapMark;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TabContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right.MarginContainer.TabContainer
    /// </summary>
    public TabContainer S_TabContainer => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_Right.L_MarginContainer.L_TabContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2.Right
    /// </summary>
    public Right S_Right => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2.L_Right;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer.HSplitContainer2
    /// </summary>
    public HSplitContainer2 S_HSplitContainer2 => L_Bg.L_VBoxContainer.L_HSplitContainer.L_HSplitContainer2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer.HSplitContainer
    /// </summary>
    public HSplitContainer S_HSplitContainer => L_Bg.L_VBoxContainer.L_HSplitContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditor.Bg.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_Bg.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

}
