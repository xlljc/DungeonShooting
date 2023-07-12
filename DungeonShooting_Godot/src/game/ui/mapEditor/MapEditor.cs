namespace UI.MapEditor;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditor : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg
    /// </summary>
    public MapEditor_Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new MapEditor_Bg(this, GetNodeOrNull<Godot.Panel>("Bg"));
            return _L_Bg;
        }
    }
    private MapEditor_Bg _L_Bg;


    public MapEditor() : base(nameof(MapEditor))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node2D"/>, 路径: MapEditor.Bg.HSplitContainer.Left.MapView.SubViewport.TileMap.Draw
    /// </summary>
    public class MapEditor_Draw : UiNode<MapEditor, Godot.Node2D, MapEditor_Draw>
    {
        public MapEditor_Draw(MapEditor uiPanel, Godot.Node2D node) : base(uiPanel, node) {  }
        public override MapEditor_Draw Clone() => new (UiPanel, (Godot.Node2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditor.EditorTileMap"/>, 路径: MapEditor.Bg.HSplitContainer.Left.MapView.SubViewport.TileMap
    /// </summary>
    public class MapEditor_TileMap : UiNode<MapEditor, UI.MapEditor.EditorTileMap, MapEditor_TileMap>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left.MapView.SubViewport.Draw
        /// </summary>
        public MapEditor_Draw L_Draw
        {
            get
            {
                if (_L_Draw == null) _L_Draw = new MapEditor_Draw(UiPanel, Instance.GetNodeOrNull<Godot.Node2D>("Draw"));
                return _L_Draw;
            }
        }
        private MapEditor_Draw _L_Draw;

        public MapEditor_TileMap(MapEditor uiPanel, UI.MapEditor.EditorTileMap node) : base(uiPanel, node) {  }
        public override MapEditor_TileMap Clone() => new (UiPanel, (UI.MapEditor.EditorTileMap)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.CanvasLayer"/>, 路径: MapEditor.Bg.HSplitContainer.Left.MapView.SubViewport.CanvasLayer
    /// </summary>
    public class MapEditor_CanvasLayer : UiNode<MapEditor, Godot.CanvasLayer, MapEditor_CanvasLayer>
    {
        public MapEditor_CanvasLayer(MapEditor uiPanel, Godot.CanvasLayer node) : base(uiPanel, node) {  }
        public override MapEditor_CanvasLayer Clone() => new (UiPanel, (Godot.CanvasLayer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SubViewport"/>, 路径: MapEditor.Bg.HSplitContainer.Left.MapView.SubViewport
    /// </summary>
    public class MapEditor_SubViewport : UiNode<MapEditor, Godot.SubViewport, MapEditor_SubViewport>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditor.EditorTileMap"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left.MapView.TileMap
        /// </summary>
        public MapEditor_TileMap L_TileMap
        {
            get
            {
                if (_L_TileMap == null) _L_TileMap = new MapEditor_TileMap(UiPanel, Instance.GetNodeOrNull<UI.MapEditor.EditorTileMap>("TileMap"));
                return _L_TileMap;
            }
        }
        private MapEditor_TileMap _L_TileMap;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CanvasLayer"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left.MapView.CanvasLayer
        /// </summary>
        public MapEditor_CanvasLayer L_CanvasLayer
        {
            get
            {
                if (_L_CanvasLayer == null) _L_CanvasLayer = new MapEditor_CanvasLayer(UiPanel, Instance.GetNodeOrNull<Godot.CanvasLayer>("CanvasLayer"));
                return _L_CanvasLayer;
            }
        }
        private MapEditor_CanvasLayer _L_CanvasLayer;

        public MapEditor_SubViewport(MapEditor uiPanel, Godot.SubViewport node) : base(uiPanel, node) {  }
        public override MapEditor_SubViewport Clone() => new (UiPanel, (Godot.SubViewport)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SubViewportContainer"/>, 路径: MapEditor.Bg.HSplitContainer.Left.MapView
    /// </summary>
    public class MapEditor_MapView : UiNode<MapEditor, Godot.SubViewportContainer, MapEditor_MapView>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SubViewport"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left.SubViewport
        /// </summary>
        public MapEditor_SubViewport L_SubViewport
        {
            get
            {
                if (_L_SubViewport == null) _L_SubViewport = new MapEditor_SubViewport(UiPanel, Instance.GetNodeOrNull<Godot.SubViewport>("SubViewport"));
                return _L_SubViewport;
            }
        }
        private MapEditor_SubViewport _L_SubViewport;

        public MapEditor_MapView(MapEditor uiPanel, Godot.SubViewportContainer node) : base(uiPanel, node) {  }
        public override MapEditor_MapView Clone() => new (UiPanel, (Godot.SubViewportContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg.HSplitContainer.Left
    /// </summary>
    public class MapEditor_Left : UiNode<MapEditor, Godot.Panel, MapEditor_Left>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SubViewportContainer"/>, 节点路径: MapEditor.Bg.HSplitContainer.MapView
        /// </summary>
        public MapEditor_MapView L_MapView
        {
            get
            {
                if (_L_MapView == null) _L_MapView = new MapEditor_MapView(UiPanel, Instance.GetNodeOrNull<Godot.SubViewportContainer>("MapView"));
                return _L_MapView;
            }
        }
        private MapEditor_MapView _L_MapView;

        public MapEditor_Left(MapEditor uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override MapEditor_Left Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg.HSplitContainer.Right
    /// </summary>
    public class MapEditor_Right : UiNode<MapEditor, Godot.Panel, MapEditor_Right>
    {
        public MapEditor_Right(MapEditor uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override MapEditor_Right Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HSplitContainer"/>, 路径: MapEditor.Bg.HSplitContainer
    /// </summary>
    public class MapEditor_HSplitContainer : UiNode<MapEditor, Godot.HSplitContainer, MapEditor_HSplitContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.Left
        /// </summary>
        public MapEditor_Left L_Left
        {
            get
            {
                if (_L_Left == null) _L_Left = new MapEditor_Left(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Left"));
                return _L_Left;
            }
        }
        private MapEditor_Left _L_Left;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.Right
        /// </summary>
        public MapEditor_Right L_Right
        {
            get
            {
                if (_L_Right == null) _L_Right = new MapEditor_Right(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Right"));
                return _L_Right;
            }
        }
        private MapEditor_Right _L_Right;

        public MapEditor_HSplitContainer(MapEditor uiPanel, Godot.HSplitContainer node) : base(uiPanel, node) {  }
        public override MapEditor_HSplitContainer Clone() => new (UiPanel, (Godot.HSplitContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditor.Bg
    /// </summary>
    public class MapEditor_Bg : UiNode<MapEditor, Godot.Panel, MapEditor_Bg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: MapEditor.HSplitContainer
        /// </summary>
        public MapEditor_HSplitContainer L_HSplitContainer
        {
            get
            {
                if (_L_HSplitContainer == null) _L_HSplitContainer = new MapEditor_HSplitContainer(UiPanel, Instance.GetNodeOrNull<Godot.HSplitContainer>("HSplitContainer"));
                return _L_HSplitContainer;
            }
        }
        private MapEditor_HSplitContainer _L_HSplitContainer;

        public MapEditor_Bg(MapEditor uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override MapEditor_Bg Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditor.EditorTileMap"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left.MapView.SubViewport.TileMap.Draw
    /// </summary>
    public MapEditor_Draw S_Draw => L_Bg.L_HSplitContainer.L_Left.L_MapView.L_SubViewport.L_TileMap.L_Draw;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SubViewport"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left.MapView.SubViewport.TileMap
    /// </summary>
    public MapEditor_TileMap S_TileMap => L_Bg.L_HSplitContainer.L_Left.L_MapView.L_SubViewport.L_TileMap;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SubViewport"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left.MapView.SubViewport.CanvasLayer
    /// </summary>
    public MapEditor_CanvasLayer S_CanvasLayer => L_Bg.L_HSplitContainer.L_Left.L_MapView.L_SubViewport.L_CanvasLayer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SubViewportContainer"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left.MapView.SubViewport
    /// </summary>
    public MapEditor_SubViewport S_SubViewport => L_Bg.L_HSplitContainer.L_Left.L_MapView.L_SubViewport;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left.MapView
    /// </summary>
    public MapEditor_MapView S_MapView => L_Bg.L_HSplitContainer.L_Left.L_MapView;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: MapEditor.Bg.HSplitContainer.Left
    /// </summary>
    public MapEditor_Left S_Left => L_Bg.L_HSplitContainer.L_Left;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HSplitContainer"/>, 节点路径: MapEditor.Bg.HSplitContainer.Right
    /// </summary>
    public MapEditor_Right S_Right => L_Bg.L_HSplitContainer.L_Right;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditor.Bg.HSplitContainer
    /// </summary>
    public MapEditor_HSplitContainer S_HSplitContainer => L_Bg.L_HSplitContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditor.MapEditorPanel"/>, 节点路径: MapEditor.Bg
    /// </summary>
    public MapEditor_Bg S_Bg => L_Bg;

}
