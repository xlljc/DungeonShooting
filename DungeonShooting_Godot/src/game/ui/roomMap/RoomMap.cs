namespace UI.RoomMap;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class RoomMap : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: RoomMap.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg((RoomMapPanel)this, GetNode<Godot.ColorRect>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomMap.MapBar
    /// </summary>
    public MapBar L_MapBar
    {
        get
        {
            if (_L_MapBar == null) _L_MapBar = new MapBar((RoomMapPanel)this, GetNode<Godot.NinePatchRect>("MapBar"));
            return _L_MapBar;
        }
    }
    private MapBar _L_MapBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomMap.MagnifyMapBar
    /// </summary>
    public MagnifyMapBar L_MagnifyMapBar
    {
        get
        {
            if (_L_MagnifyMapBar == null) _L_MagnifyMapBar = new MagnifyMapBar((RoomMapPanel)this, GetNode<Godot.NinePatchRect>("MagnifyMapBar"));
            return _L_MagnifyMapBar;
        }
    }
    private MagnifyMapBar _L_MagnifyMapBar;


    public RoomMap() : base(nameof(RoomMap))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: RoomMap.Bg
    /// </summary>
    public class Bg : UiNode<RoomMapPanel, Godot.ColorRect, Bg>
    {
        public Bg(RoomMapPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node2D"/>, 路径: RoomMap.MapBar.DrawContainer.Root
    /// </summary>
    public class Root : UiNode<RoomMapPanel, Godot.Node2D, Root>
    {
        public Root(RoomMapPanel uiPanel, Godot.Node2D node) : base(uiPanel, node) {  }
        public override Root Clone() => new (UiPanel, (Godot.Node2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomMap.MapBar.DrawContainer.Mark
    /// </summary>
    public class Mark : UiNode<RoomMapPanel, Godot.Sprite2D, Mark>
    {
        public Mark(RoomMapPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override Mark Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomMap.MapBar.DrawContainer
    /// </summary>
    public class DrawContainer : UiNode<RoomMapPanel, Godot.TextureRect, DrawContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: RoomMap.MapBar.Root
        /// </summary>
        public Root L_Root
        {
            get
            {
                if (_L_Root == null) _L_Root = new Root(UiPanel, Instance.GetNode<Godot.Node2D>("Root"));
                return _L_Root;
            }
        }
        private Root _L_Root;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomMap.MapBar.Mark
        /// </summary>
        public Mark L_Mark
        {
            get
            {
                if (_L_Mark == null) _L_Mark = new Mark(UiPanel, Instance.GetNode<Godot.Sprite2D>("Mark"));
                return _L_Mark;
            }
        }
        private Mark _L_Mark;

        public DrawContainer(RoomMapPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override DrawContainer Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomMap.MapBar
    /// </summary>
    public class MapBar : UiNode<RoomMapPanel, Godot.NinePatchRect, MapBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomMap.DrawContainer
        /// </summary>
        public DrawContainer L_DrawContainer
        {
            get
            {
                if (_L_DrawContainer == null) _L_DrawContainer = new DrawContainer(UiPanel, Instance.GetNode<Godot.TextureRect>("DrawContainer"));
                return _L_DrawContainer;
            }
        }
        private DrawContainer _L_DrawContainer;

        public MapBar(RoomMapPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override MapBar Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomMap.MagnifyMapBar
    /// </summary>
    public class MagnifyMapBar : UiNode<RoomMapPanel, Godot.NinePatchRect, MagnifyMapBar>
    {
        public MagnifyMapBar(RoomMapPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override MagnifyMapBar Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: RoomMap.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: RoomMap.MapBar.DrawContainer.Root
    /// </summary>
    public Root S_Root => L_MapBar.L_DrawContainer.L_Root;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomMap.MapBar.DrawContainer.Mark
    /// </summary>
    public Mark S_Mark => L_MapBar.L_DrawContainer.L_Mark;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomMap.MapBar.DrawContainer
    /// </summary>
    public DrawContainer S_DrawContainer => L_MapBar.L_DrawContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomMap.MapBar
    /// </summary>
    public MapBar S_MapBar => L_MapBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomMap.MagnifyMapBar
    /// </summary>
    public MagnifyMapBar S_MagnifyMapBar => L_MagnifyMapBar;

}
