namespace UI.MapEditorSelectObject;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorSelectObject : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorSelectObject.ScrollContainer
    /// </summary>
    public ScrollContainer L_ScrollContainer
    {
        get
        {
            if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer(this, GetNodeOrNull<Godot.ScrollContainer>("ScrollContainer"));
            return _L_ScrollContainer;
        }
    }
    private ScrollContainer _L_ScrollContainer;


    public MapEditorSelectObject() : base(nameof(MapEditorSelectObject))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorSelectObject.ScrollContainer.ObjectButton.PreviewImage
    /// </summary>
    public class PreviewImage : UiNode<MapEditorSelectObject, Godot.TextureRect, PreviewImage>
    {
        public PreviewImage(MapEditorSelectObject uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override PreviewImage Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorSelectObject.ScrollContainer.ObjectButton.ObjectName
    /// </summary>
    public class ObjectName : UiNode<MapEditorSelectObject, Godot.Label, ObjectName>
    {
        public ObjectName(MapEditorSelectObject uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override ObjectName Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: MapEditorSelectObject.ScrollContainer.ObjectButton.SelectImage
    /// </summary>
    public class SelectImage : UiNode<MapEditorSelectObject, Godot.NinePatchRect, SelectImage>
    {
        public SelectImage(MapEditorSelectObject uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override SelectImage Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorSelectObject.ScrollContainer.ObjectButton
    /// </summary>
    public class ObjectButton : UiNode<MapEditorSelectObject, Godot.Button, ObjectButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorSelectObject.ScrollContainer.PreviewImage
        /// </summary>
        public PreviewImage L_PreviewImage
        {
            get
            {
                if (_L_PreviewImage == null) _L_PreviewImage = new PreviewImage(UiPanel, Instance.GetNodeOrNull<Godot.TextureRect>("PreviewImage"));
                return _L_PreviewImage;
            }
        }
        private PreviewImage _L_PreviewImage;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorSelectObject.ScrollContainer.ObjectName
        /// </summary>
        public ObjectName L_ObjectName
        {
            get
            {
                if (_L_ObjectName == null) _L_ObjectName = new ObjectName(UiPanel, Instance.GetNodeOrNull<Godot.Label>("ObjectName"));
                return _L_ObjectName;
            }
        }
        private ObjectName _L_ObjectName;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorSelectObject.ScrollContainer.SelectImage
        /// </summary>
        public SelectImage L_SelectImage
        {
            get
            {
                if (_L_SelectImage == null) _L_SelectImage = new SelectImage(UiPanel, Instance.GetNodeOrNull<Godot.NinePatchRect>("SelectImage"));
                return _L_SelectImage;
            }
        }
        private SelectImage _L_SelectImage;

        public ObjectButton(MapEditorSelectObject uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ObjectButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorSelectObject.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditorSelectObject, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorSelectObject.ObjectButton
        /// </summary>
        public ObjectButton L_ObjectButton
        {
            get
            {
                if (_L_ObjectButton == null) _L_ObjectButton = new ObjectButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("ObjectButton"));
                return _L_ObjectButton;
            }
        }
        private ObjectButton _L_ObjectButton;

        public ScrollContainer(MapEditorSelectObject uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorSelectObject.ScrollContainer.ObjectButton.PreviewImage
    /// </summary>
    public PreviewImage S_PreviewImage => L_ScrollContainer.L_ObjectButton.L_PreviewImage;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorSelectObject.ScrollContainer.ObjectButton.ObjectName
    /// </summary>
    public ObjectName S_ObjectName => L_ScrollContainer.L_ObjectButton.L_ObjectName;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: MapEditorSelectObject.ScrollContainer.ObjectButton.SelectImage
    /// </summary>
    public SelectImage S_SelectImage => L_ScrollContainer.L_ObjectButton.L_SelectImage;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorSelectObject.ScrollContainer.ObjectButton
    /// </summary>
    public ObjectButton S_ObjectButton => L_ScrollContainer.L_ObjectButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorSelectObject.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_ScrollContainer;

}
