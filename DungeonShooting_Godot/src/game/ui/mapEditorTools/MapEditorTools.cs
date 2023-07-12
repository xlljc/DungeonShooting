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
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.TextureButton
    /// </summary>
    public class MapEditorTools_TextureButton : UiNode<MapEditorTools, Godot.TextureButton, MapEditorTools_TextureButton>
    {
        public MapEditorTools_TextureButton(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override MapEditorTools_TextureButton Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.TextureButton2
    /// </summary>
    public class MapEditorTools_TextureButton2 : UiNode<MapEditorTools, Godot.TextureButton, MapEditorTools_TextureButton2>
    {
        public MapEditorTools_TextureButton2(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override MapEditorTools_TextureButton2 Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.TextureButton3
    /// </summary>
    public class MapEditorTools_TextureButton3 : UiNode<MapEditorTools, Godot.TextureButton, MapEditorTools_TextureButton3>
    {
        public MapEditorTools_TextureButton3(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override MapEditorTools_TextureButton3 Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: MapEditorTools.HBoxContainer.TextureButton4
    /// </summary>
    public class MapEditorTools_TextureButton4 : UiNode<MapEditorTools, Godot.TextureButton, MapEditorTools_TextureButton4>
    {
        public MapEditorTools_TextureButton4(MapEditorTools uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override MapEditorTools_TextureButton4 Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorTools.HBoxContainer
    /// </summary>
    public class MapEditorTools_HBoxContainer : UiNode<MapEditorTools, Godot.HBoxContainer, MapEditorTools_HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.TextureButton
        /// </summary>
        public MapEditorTools_TextureButton L_TextureButton
        {
            get
            {
                if (_L_TextureButton == null) _L_TextureButton = new MapEditorTools_TextureButton(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("TextureButton"));
                return _L_TextureButton;
            }
        }
        private MapEditorTools_TextureButton _L_TextureButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.TextureButton2
        /// </summary>
        public MapEditorTools_TextureButton2 L_TextureButton2
        {
            get
            {
                if (_L_TextureButton2 == null) _L_TextureButton2 = new MapEditorTools_TextureButton2(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("TextureButton2"));
                return _L_TextureButton2;
            }
        }
        private MapEditorTools_TextureButton2 _L_TextureButton2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.TextureButton3
        /// </summary>
        public MapEditorTools_TextureButton3 L_TextureButton3
        {
            get
            {
                if (_L_TextureButton3 == null) _L_TextureButton3 = new MapEditorTools_TextureButton3(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("TextureButton3"));
                return _L_TextureButton3;
            }
        }
        private MapEditorTools_TextureButton3 _L_TextureButton3;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: MapEditorTools.TextureButton4
        /// </summary>
        public MapEditorTools_TextureButton4 L_TextureButton4
        {
            get
            {
                if (_L_TextureButton4 == null) _L_TextureButton4 = new MapEditorTools_TextureButton4(UiPanel, Instance.GetNodeOrNull<Godot.TextureButton>("TextureButton4"));
                return _L_TextureButton4;
            }
        }
        private MapEditorTools_TextureButton4 _L_TextureButton4;

        public MapEditorTools_HBoxContainer(MapEditorTools uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override MapEditorTools_HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorTools.HBoxContainer.TextureButton
    /// </summary>
    public MapEditorTools_TextureButton S_TextureButton => L_HBoxContainer.L_TextureButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorTools.HBoxContainer.TextureButton2
    /// </summary>
    public MapEditorTools_TextureButton2 S_TextureButton2 => L_HBoxContainer.L_TextureButton2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorTools.HBoxContainer.TextureButton3
    /// </summary>
    public MapEditorTools_TextureButton3 S_TextureButton3 => L_HBoxContainer.L_TextureButton3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorTools.HBoxContainer.TextureButton4
    /// </summary>
    public MapEditorTools_TextureButton4 S_TextureButton4 => L_HBoxContainer.L_TextureButton4;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorTools.MapEditorToolsPanel"/>, 节点路径: MapEditorTools.HBoxContainer
    /// </summary>
    public MapEditorTools_HBoxContainer S_HBoxContainer => L_HBoxContainer;

}
