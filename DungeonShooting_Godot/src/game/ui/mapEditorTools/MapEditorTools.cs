namespace UI.MapEditorTools;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorTools : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorTools.HBoxContainer
    /// </summary>
    public MapEditorTools_HBoxContainer L_HBoxContainer
    {
        get
        {
            if (_L_HBoxContainer == null) _L_HBoxContainer = new MapEditorTools_HBoxContainer(this, GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
            return _L_HBoxContainer;
        }
    }
    private MapEditorTools_HBoxContainer _L_HBoxContainer;


    public MapEditorTools() : base(nameof(MapEditorTools))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.HandTool
    /// </summary>
    public class MapEditorTools_HandTool : UiNode<MapEditorTools, Godot.TextureButton, MapEditorTools_HandTool>
    {
        public MapEditorTools_HandTool(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override MapEditorTools_HandTool Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.PenTool
    /// </summary>
    public class MapEditorTools_PenTool : UiNode<MapEditorTools, Godot.TextureButton, MapEditorTools_PenTool>
    {
        public MapEditorTools_PenTool(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override MapEditorTools_PenTool Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.RectTool
    /// </summary>
    public class MapEditorTools_RectTool : UiNode<MapEditorTools, Godot.TextureButton, MapEditorTools_RectTool>
    {
        public MapEditorTools_RectTool(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override MapEditorTools_RectTool Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.CenterTool
    /// </summary>
    public class MapEditorTools_CenterTool : UiNode<MapEditorTools, Godot.TextureButton, MapEditorTools_CenterTool>
    {
        public MapEditorTools_CenterTool(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override MapEditorTools_CenterTool Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorTools.HBoxContainer
    /// </summary>
    public class MapEditorTools_HBoxContainer : UiNode<MapEditorTools, Godot.HBoxContainer, MapEditorTools_HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HandTool
        /// </summary>
        public MapEditorTools_HandTool L_HandTool
        {
            get
            {
                if (_L_HandTool == null) _L_HandTool = new MapEditorTools_HandTool(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("HandTool"));
                return _L_HandTool;
            }
        }
        private MapEditorTools_HandTool _L_HandTool;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.PenTool
        /// </summary>
        public MapEditorTools_PenTool L_PenTool
        {
            get
            {
                if (_L_PenTool == null) _L_PenTool = new MapEditorTools_PenTool(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("PenTool"));
                return _L_PenTool;
            }
        }
        private MapEditorTools_PenTool _L_PenTool;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.RectTool
        /// </summary>
        public MapEditorTools_RectTool L_RectTool
        {
            get
            {
                if (_L_RectTool == null) _L_RectTool = new MapEditorTools_RectTool(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("RectTool"));
                return _L_RectTool;
            }
        }
        private MapEditorTools_RectTool _L_RectTool;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.CenterTool
        /// </summary>
        public MapEditorTools_CenterTool L_CenterTool
        {
            get
            {
                if (_L_CenterTool == null) _L_CenterTool = new MapEditorTools_CenterTool(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("CenterTool"));
                return _L_CenterTool;
            }
        }
        private MapEditorTools_CenterTool _L_CenterTool;

        public MapEditorTools_HBoxContainer(MapEditorTools uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override MapEditorTools_HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HBoxContainer.HandTool
    /// </summary>
    public MapEditorTools_HandTool S_HandTool => L_HBoxContainer.L_HandTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HBoxContainer.PenTool
    /// </summary>
    public MapEditorTools_PenTool S_PenTool => L_HBoxContainer.L_PenTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HBoxContainer.RectTool
    /// </summary>
    public MapEditorTools_RectTool S_RectTool => L_HBoxContainer.L_RectTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.HBoxContainer.CenterTool
    /// </summary>
    public MapEditorTools_CenterTool S_CenterTool => L_HBoxContainer.L_CenterTool;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorTools.HBoxContainer
    /// </summary>
    public MapEditorTools_HBoxContainer S_HBoxContainer => L_HBoxContainer;

}
