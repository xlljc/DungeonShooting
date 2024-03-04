namespace UI.WeaponRoulette;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class WeaponRoulette : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: WeaponRoulette.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg((WeaponRoulettePanel)this, GetNode<Godot.ColorRect>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: WeaponRoulette.Control
    /// </summary>
    public Control L_Control
    {
        get
        {
            if (_L_Control == null) _L_Control = new Control((WeaponRoulettePanel)this, GetNode<Godot.Control>("Control"));
            return _L_Control;
        }
    }
    private Control _L_Control;


    public WeaponRoulette() : base(nameof(WeaponRoulette))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: WeaponRoulette.Bg
    /// </summary>
    public class Bg : UiNode<WeaponRoulettePanel, Godot.ColorRect, Bg>
    {
        public Bg(WeaponRoulettePanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlot.Sprite
    /// </summary>
    public class Sprite : UiNode<WeaponRoulettePanel, Godot.Sprite2D, Sprite>
    {
        public Sprite(WeaponRoulettePanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override Sprite Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.CollisionPolygon2D"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlot.Area2D.CollisionPolygon2D
    /// </summary>
    public class CollisionPolygon2D : UiNode<WeaponRoulettePanel, Godot.CollisionPolygon2D, CollisionPolygon2D>
    {
        public CollisionPolygon2D(WeaponRoulettePanel uiPanel, Godot.CollisionPolygon2D node) : base(uiPanel, node) {  }
        public override CollisionPolygon2D Clone() => new (UiPanel, (Godot.CollisionPolygon2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Area2D"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlot.Area2D
    /// </summary>
    public class Area2D : UiNode<WeaponRoulettePanel, Godot.Area2D, Area2D>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CollisionPolygon2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlot.CollisionPolygon2D
        /// </summary>
        public CollisionPolygon2D L_CollisionPolygon2D
        {
            get
            {
                if (_L_CollisionPolygon2D == null) _L_CollisionPolygon2D = new CollisionPolygon2D(UiPanel, Instance.GetNode<Godot.CollisionPolygon2D>("CollisionPolygon2D"));
                return _L_CollisionPolygon2D;
            }
        }
        private CollisionPolygon2D _L_CollisionPolygon2D;

        public Area2D(WeaponRoulettePanel uiPanel, Godot.Area2D node) : base(uiPanel, node) {  }
        public override Area2D Clone() => new (UiPanel, (Godot.Area2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node2D"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlot
    /// </summary>
    public class WeaponSlot : UiNode<WeaponRoulettePanel, Godot.Node2D, WeaponSlot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.Sprite
        /// </summary>
        public Sprite L_Sprite
        {
            get
            {
                if (_L_Sprite == null) _L_Sprite = new Sprite(UiPanel, Instance.GetNode<Godot.Sprite2D>("Sprite"));
                return _L_Sprite;
            }
        }
        private Sprite _L_Sprite;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Area2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.Area2D
        /// </summary>
        public Area2D L_Area2D
        {
            get
            {
                if (_L_Area2D == null) _L_Area2D = new Area2D(UiPanel, Instance.GetNode<Godot.Area2D>("Area2D"));
                return _L_Area2D;
            }
        }
        private Area2D _L_Area2D;

        public WeaponSlot(WeaponRoulettePanel uiPanel, Godot.Node2D node) : base(uiPanel, node) {  }
        public override WeaponSlot Clone() => new (UiPanel, (Godot.Node2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: WeaponRoulette.Control.RouletteBg
    /// </summary>
    public class RouletteBg : UiNode<WeaponRoulettePanel, Godot.Sprite2D, RouletteBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: WeaponRoulette.Control.WeaponSlot
        /// </summary>
        public WeaponSlot L_WeaponSlot
        {
            get
            {
                if (_L_WeaponSlot == null) _L_WeaponSlot = new WeaponSlot(UiPanel, Instance.GetNode<Godot.Node2D>("WeaponSlot"));
                return _L_WeaponSlot;
            }
        }
        private WeaponSlot _L_WeaponSlot;

        public RouletteBg(WeaponRoulettePanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override RouletteBg Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: WeaponRoulette.Control
    /// </summary>
    public class Control : UiNode<WeaponRoulettePanel, Godot.Control, Control>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.RouletteBg
        /// </summary>
        public RouletteBg L_RouletteBg
        {
            get
            {
                if (_L_RouletteBg == null) _L_RouletteBg = new RouletteBg(UiPanel, Instance.GetNode<Godot.Sprite2D>("RouletteBg"));
                return _L_RouletteBg;
            }
        }
        private RouletteBg _L_RouletteBg;

        public Control(WeaponRoulettePanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override Control Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: WeaponRoulette.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlot.Sprite
    /// </summary>
    public Sprite S_Sprite => L_Control.L_RouletteBg.L_WeaponSlot.L_Sprite;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CollisionPolygon2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlot.Area2D.CollisionPolygon2D
    /// </summary>
    public CollisionPolygon2D S_CollisionPolygon2D => L_Control.L_RouletteBg.L_WeaponSlot.L_Area2D.L_CollisionPolygon2D;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Area2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlot.Area2D
    /// </summary>
    public Area2D S_Area2D => L_Control.L_RouletteBg.L_WeaponSlot.L_Area2D;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlot
    /// </summary>
    public WeaponSlot S_WeaponSlot => L_Control.L_RouletteBg.L_WeaponSlot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg
    /// </summary>
    public RouletteBg S_RouletteBg => L_Control.L_RouletteBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: WeaponRoulette.Control
    /// </summary>
    public Control S_Control => L_Control;

}
