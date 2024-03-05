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

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Area2D"/>, 节点路径: WeaponRoulette.MouseArea
    /// </summary>
    public MouseArea L_MouseArea
    {
        get
        {
            if (_L_MouseArea == null) _L_MouseArea = new MouseArea((WeaponRoulettePanel)this, GetNode<Godot.Area2D>("MouseArea"));
            return _L_MouseArea;
        }
    }
    private MouseArea _L_MouseArea;


    public WeaponRoulette() : base(nameof(WeaponRoulette))
    {
    }

    public sealed override void OnInitNestedUi()
    {
        _ = L_Control.L_RouletteBg.L_WeaponSlotNode;

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
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.Control.WeaponIcon
    /// </summary>
    public class WeaponIcon : UiNode<WeaponRoulettePanel, Godot.Sprite2D, WeaponIcon>
    {
        public WeaponIcon(WeaponRoulettePanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override WeaponIcon Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.Control.AmmoLabel
    /// </summary>
    public class AmmoLabel : UiNode<WeaponRoulettePanel, Godot.Label, AmmoLabel>
    {
        public AmmoLabel(WeaponRoulettePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override AmmoLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.Control
    /// </summary>
    public class Control_1 : UiNode<WeaponRoulettePanel, Godot.Control, Control_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.WeaponIcon
        /// </summary>
        public WeaponIcon L_WeaponIcon
        {
            get
            {
                if (_L_WeaponIcon == null) _L_WeaponIcon = new WeaponIcon(UiPanel, Instance.GetNode<Godot.Sprite2D>("WeaponIcon"));
                return _L_WeaponIcon;
            }
        }
        private WeaponIcon _L_WeaponIcon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.AmmoLabel
        /// </summary>
        public AmmoLabel L_AmmoLabel
        {
            get
            {
                if (_L_AmmoLabel == null) _L_AmmoLabel = new AmmoLabel(UiPanel, Instance.GetNode<Godot.Label>("AmmoLabel"));
                return _L_AmmoLabel;
            }
        }
        private AmmoLabel _L_AmmoLabel;

        public Control_1(WeaponRoulettePanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override Control_1 Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.CollisionPolygon2D"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.SlotAreaNode.CollisionPolygon2D
    /// </summary>
    public class CollisionPolygon2D : UiNode<WeaponRoulettePanel, Godot.CollisionPolygon2D, CollisionPolygon2D>
    {
        public CollisionPolygon2D(WeaponRoulettePanel uiPanel, Godot.CollisionPolygon2D node) : base(uiPanel, node) {  }
        public override CollisionPolygon2D Clone() => new (UiPanel, (Godot.CollisionPolygon2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Area2D"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.SlotAreaNode
    /// </summary>
    public class SlotAreaNode : UiNode<WeaponRoulettePanel, Godot.Area2D, SlotAreaNode>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CollisionPolygon2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.CollisionPolygon2D
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

        public SlotAreaNode(WeaponRoulettePanel uiPanel, Godot.Area2D node) : base(uiPanel, node) {  }
        public override SlotAreaNode Clone() => new (UiPanel, (Godot.Area2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.WeaponRoulette.WeaponSlot"/>, 路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode
    /// </summary>
    public class WeaponSlotNode : UiNode<WeaponRoulettePanel, UI.WeaponRoulette.WeaponSlot, WeaponSlotNode>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: WeaponRoulette.Control.RouletteBg.Control
        /// </summary>
        public Control_1 L_Control
        {
            get
            {
                if (_L_Control == null) _L_Control = new Control_1(UiPanel, Instance.GetNode<Godot.Control>("Control"));
                return _L_Control;
            }
        }
        private Control_1 _L_Control;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Area2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.SlotAreaNode
        /// </summary>
        public SlotAreaNode L_SlotAreaNode
        {
            get
            {
                if (_L_SlotAreaNode == null) _L_SlotAreaNode = new SlotAreaNode(UiPanel, Instance.GetNode<Godot.Area2D>("SlotAreaNode"));
                return _L_SlotAreaNode;
            }
        }
        private SlotAreaNode _L_SlotAreaNode;

        public WeaponSlotNode(WeaponRoulettePanel uiPanel, UI.WeaponRoulette.WeaponSlot node) : base(uiPanel, node) {  }
        public override WeaponSlotNode Clone() => new (UiPanel, (UI.WeaponRoulette.WeaponSlot)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: WeaponRoulette.Control.RouletteBg
    /// </summary>
    public class RouletteBg : UiNode<WeaponRoulettePanel, Godot.Sprite2D, RouletteBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.WeaponRoulette.WeaponSlot"/>, 节点路径: WeaponRoulette.Control.WeaponSlotNode
        /// </summary>
        public WeaponSlotNode L_WeaponSlotNode
        {
            get
            {
                if (_L_WeaponSlotNode == null) _L_WeaponSlotNode = new WeaponSlotNode(UiPanel, Instance.GetNode<UI.WeaponRoulette.WeaponSlot>("WeaponSlotNode"));
                return _L_WeaponSlotNode;
            }
        }
        private WeaponSlotNode _L_WeaponSlotNode;

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
    /// 类型: <see cref="Godot.CollisionShape2D"/>, 路径: WeaponRoulette.MouseArea.CollisionShape2D
    /// </summary>
    public class CollisionShape2D : UiNode<WeaponRoulettePanel, Godot.CollisionShape2D, CollisionShape2D>
    {
        public CollisionShape2D(WeaponRoulettePanel uiPanel, Godot.CollisionShape2D node) : base(uiPanel, node) {  }
        public override CollisionShape2D Clone() => new (UiPanel, (Godot.CollisionShape2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Area2D"/>, 路径: WeaponRoulette.MouseArea
    /// </summary>
    public class MouseArea : UiNode<WeaponRoulettePanel, Godot.Area2D, MouseArea>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CollisionShape2D"/>, 节点路径: WeaponRoulette.CollisionShape2D
        /// </summary>
        public CollisionShape2D L_CollisionShape2D
        {
            get
            {
                if (_L_CollisionShape2D == null) _L_CollisionShape2D = new CollisionShape2D(UiPanel, Instance.GetNode<Godot.CollisionShape2D>("CollisionShape2D"));
                return _L_CollisionShape2D;
            }
        }
        private CollisionShape2D _L_CollisionShape2D;

        public MouseArea(WeaponRoulettePanel uiPanel, Godot.Area2D node) : base(uiPanel, node) {  }
        public override MouseArea Clone() => new (UiPanel, (Godot.Area2D)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: WeaponRoulette.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.Control.WeaponIcon
    /// </summary>
    public WeaponIcon S_WeaponIcon => L_Control.L_RouletteBg.L_WeaponSlotNode.L_Control.L_WeaponIcon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.Control.AmmoLabel
    /// </summary>
    public AmmoLabel S_AmmoLabel => L_Control.L_RouletteBg.L_WeaponSlotNode.L_Control.L_AmmoLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CollisionPolygon2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.SlotAreaNode.CollisionPolygon2D
    /// </summary>
    public CollisionPolygon2D S_CollisionPolygon2D => L_Control.L_RouletteBg.L_WeaponSlotNode.L_SlotAreaNode.L_CollisionPolygon2D;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Area2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode.SlotAreaNode
    /// </summary>
    public SlotAreaNode S_SlotAreaNode => L_Control.L_RouletteBg.L_WeaponSlotNode.L_SlotAreaNode;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.WeaponRoulette.WeaponSlot"/>, 节点路径: WeaponRoulette.Control.RouletteBg.WeaponSlotNode
    /// </summary>
    public WeaponSlotNode S_WeaponSlotNode => L_Control.L_RouletteBg.L_WeaponSlotNode;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Control.RouletteBg
    /// </summary>
    public RouletteBg S_RouletteBg => L_Control.L_RouletteBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CollisionShape2D"/>, 节点路径: WeaponRoulette.MouseArea.CollisionShape2D
    /// </summary>
    public CollisionShape2D S_CollisionShape2D => L_MouseArea.L_CollisionShape2D;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Area2D"/>, 节点路径: WeaponRoulette.MouseArea
    /// </summary>
    public MouseArea S_MouseArea => L_MouseArea;

}
