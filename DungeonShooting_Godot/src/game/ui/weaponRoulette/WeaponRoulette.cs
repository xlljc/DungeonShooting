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
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: WeaponRoulette.Root
    /// </summary>
    public Root L_Root
    {
        get
        {
            if (_L_Root == null) _L_Root = new Root((WeaponRoulettePanel)this, GetNode<Godot.Control>("Root"));
            return _L_Root;
        }
    }
    private Root _L_Root;

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
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.WeaponUi.WeaponIcon
    /// </summary>
    public class WeaponIcon : UiNode<WeaponRoulettePanel, Godot.Sprite2D, WeaponIcon>
    {
        public WeaponIcon(WeaponRoulettePanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override WeaponIcon Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.WeaponUi.AmmoLabel
    /// </summary>
    public class AmmoLabel : UiNode<WeaponRoulettePanel, Godot.Label, AmmoLabel>
    {
        public AmmoLabel(WeaponRoulettePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override AmmoLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.WeaponUi
    /// </summary>
    public class WeaponUi : UiNode<WeaponRoulettePanel, Godot.Control, WeaponUi>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.WeaponIcon
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.AmmoLabel
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

        public WeaponUi(WeaponRoulettePanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override WeaponUi Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.LockSprite
    /// </summary>
    public class LockSprite : UiNode<WeaponRoulettePanel, Godot.Sprite2D, LockSprite>
    {
        public LockSprite(WeaponRoulettePanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override LockSprite Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi
    /// </summary>
    public class SlotUi : UiNode<WeaponRoulettePanel, Godot.Control, SlotUi>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.WeaponUi
        /// </summary>
        public WeaponUi L_WeaponUi
        {
            get
            {
                if (_L_WeaponUi == null) _L_WeaponUi = new WeaponUi(UiPanel, Instance.GetNode<Godot.Control>("WeaponUi"));
                return _L_WeaponUi;
            }
        }
        private WeaponUi _L_WeaponUi;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.LockSprite
        /// </summary>
        public LockSprite L_LockSprite
        {
            get
            {
                if (_L_LockSprite == null) _L_LockSprite = new LockSprite(UiPanel, Instance.GetNode<Godot.Sprite2D>("LockSprite"));
                return _L_LockSprite;
            }
        }
        private LockSprite _L_LockSprite;

        public SlotUi(WeaponRoulettePanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override SlotUi Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.CollisionPolygon2D"/>, 路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotAreaNode.CollisionPolygon2D
    /// </summary>
    public class CollisionPolygon2D : UiNode<WeaponRoulettePanel, Godot.CollisionPolygon2D, CollisionPolygon2D>
    {
        public CollisionPolygon2D(WeaponRoulettePanel uiPanel, Godot.CollisionPolygon2D node) : base(uiPanel, node) {  }
        public override CollisionPolygon2D Clone() => new (UiPanel, (Godot.CollisionPolygon2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Area2D"/>, 路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotAreaNode
    /// </summary>
    public class SlotAreaNode : UiNode<WeaponRoulettePanel, Godot.Area2D, SlotAreaNode>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CollisionPolygon2D"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.CollisionPolygon2D
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
    /// 类型: <see cref="UI.WeaponRoulette.WeaponSlot"/>, 路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode
    /// </summary>
    public class WeaponSlotNode : UiNode<WeaponRoulettePanel, UI.WeaponRoulette.WeaponSlot, WeaponSlotNode>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: WeaponRoulette.Root.RouletteBg.SlotUi
        /// </summary>
        public SlotUi L_SlotUi
        {
            get
            {
                if (_L_SlotUi == null) _L_SlotUi = new SlotUi(UiPanel, Instance.GetNode<Godot.Control>("SlotUi"));
                return _L_SlotUi;
            }
        }
        private SlotUi _L_SlotUi;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Area2D"/>, 节点路径: WeaponRoulette.Root.RouletteBg.SlotAreaNode
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
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: WeaponRoulette.Root.RouletteBg
    /// </summary>
    public class RouletteBg : UiNode<WeaponRoulettePanel, Godot.Sprite2D, RouletteBg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.WeaponRoulette.WeaponSlot"/>, 节点路径: WeaponRoulette.Root.WeaponSlotNode
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
    /// 类型: <see cref="Godot.Label"/>, 路径: WeaponRoulette.Root.UpBar.Label
    /// </summary>
    public class Label : UiNode<WeaponRoulettePanel, Godot.Label, Label>
    {
        public Label(WeaponRoulettePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: WeaponRoulette.Root.UpBar.Label2
    /// </summary>
    public class Label2 : UiNode<WeaponRoulettePanel, Godot.Label, Label2>
    {
        public Label2(WeaponRoulettePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: WeaponRoulette.Root.UpBar.PageLabel
    /// </summary>
    public class PageLabel : UiNode<WeaponRoulettePanel, Godot.Label, PageLabel>
    {
        public PageLabel(WeaponRoulettePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override PageLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: WeaponRoulette.Root.UpBar
    /// </summary>
    public class UpBar : UiNode<WeaponRoulettePanel, Godot.ColorRect, UpBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Root.Label
        /// </summary>
        public Label L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new Label(UiPanel, Instance.GetNode<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private Label _L_Label;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Root.Label2
        /// </summary>
        public Label2 L_Label2
        {
            get
            {
                if (_L_Label2 == null) _L_Label2 = new Label2(UiPanel, Instance.GetNode<Godot.Label>("Label2"));
                return _L_Label2;
            }
        }
        private Label2 _L_Label2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Root.PageLabel
        /// </summary>
        public PageLabel L_PageLabel
        {
            get
            {
                if (_L_PageLabel == null) _L_PageLabel = new PageLabel(UiPanel, Instance.GetNode<Godot.Label>("PageLabel"));
                return _L_PageLabel;
            }
        }
        private PageLabel _L_PageLabel;

        public UpBar(WeaponRoulettePanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override UpBar Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: WeaponRoulette.Root.DownBar.Label
    /// </summary>
    public class Label_1 : UiNode<WeaponRoulettePanel, Godot.Label, Label_1>
    {
        public Label_1(WeaponRoulettePanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label_1 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: WeaponRoulette.Root.DownBar
    /// </summary>
    public class DownBar : UiNode<WeaponRoulettePanel, Godot.ColorRect, DownBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Root.Label
        /// </summary>
        public Label_1 L_Label
        {
            get
            {
                if (_L_Label == null) _L_Label = new Label_1(UiPanel, Instance.GetNode<Godot.Label>("Label"));
                return _L_Label;
            }
        }
        private Label_1 _L_Label;

        public DownBar(WeaponRoulettePanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override DownBar Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: WeaponRoulette.Root
    /// </summary>
    public class Root : UiNode<WeaponRoulettePanel, Godot.Control, Root>
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: WeaponRoulette.UpBar
        /// </summary>
        public UpBar L_UpBar
        {
            get
            {
                if (_L_UpBar == null) _L_UpBar = new UpBar(UiPanel, Instance.GetNode<Godot.ColorRect>("UpBar"));
                return _L_UpBar;
            }
        }
        private UpBar _L_UpBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: WeaponRoulette.DownBar
        /// </summary>
        public DownBar L_DownBar
        {
            get
            {
                if (_L_DownBar == null) _L_DownBar = new DownBar(UiPanel, Instance.GetNode<Godot.ColorRect>("DownBar"));
                return _L_DownBar;
            }
        }
        private DownBar _L_DownBar;

        public Root(WeaponRoulettePanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override Root Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
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
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.WeaponUi.WeaponIcon
    /// </summary>
    public WeaponIcon S_WeaponIcon => L_Root.L_RouletteBg.L_WeaponSlotNode.L_SlotUi.L_WeaponUi.L_WeaponIcon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.WeaponUi.AmmoLabel
    /// </summary>
    public AmmoLabel S_AmmoLabel => L_Root.L_RouletteBg.L_WeaponSlotNode.L_SlotUi.L_WeaponUi.L_AmmoLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.WeaponUi
    /// </summary>
    public WeaponUi S_WeaponUi => L_Root.L_RouletteBg.L_WeaponSlotNode.L_SlotUi.L_WeaponUi;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi.LockSprite
    /// </summary>
    public LockSprite S_LockSprite => L_Root.L_RouletteBg.L_WeaponSlotNode.L_SlotUi.L_LockSprite;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotUi
    /// </summary>
    public SlotUi S_SlotUi => L_Root.L_RouletteBg.L_WeaponSlotNode.L_SlotUi;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CollisionPolygon2D"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotAreaNode.CollisionPolygon2D
    /// </summary>
    public CollisionPolygon2D S_CollisionPolygon2D => L_Root.L_RouletteBg.L_WeaponSlotNode.L_SlotAreaNode.L_CollisionPolygon2D;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Area2D"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode.SlotAreaNode
    /// </summary>
    public SlotAreaNode S_SlotAreaNode => L_Root.L_RouletteBg.L_WeaponSlotNode.L_SlotAreaNode;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.WeaponRoulette.WeaponSlot"/>, 节点路径: WeaponRoulette.Root.RouletteBg.WeaponSlotNode
    /// </summary>
    public WeaponSlotNode S_WeaponSlotNode => L_Root.L_RouletteBg.L_WeaponSlotNode;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Root.RouletteBg
    /// </summary>
    public RouletteBg S_RouletteBg => L_Root.L_RouletteBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Root.UpBar.Label2
    /// </summary>
    public Label2 S_Label2 => L_Root.L_UpBar.L_Label2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: WeaponRoulette.Root.UpBar.PageLabel
    /// </summary>
    public PageLabel S_PageLabel => L_Root.L_UpBar.L_PageLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: WeaponRoulette.Root.UpBar
    /// </summary>
    public UpBar S_UpBar => L_Root.L_UpBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: WeaponRoulette.Root.DownBar
    /// </summary>
    public DownBar S_DownBar => L_Root.L_DownBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: WeaponRoulette.Root
    /// </summary>
    public Root S_Root => L_Root;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CollisionShape2D"/>, 节点路径: WeaponRoulette.MouseArea.CollisionShape2D
    /// </summary>
    public CollisionShape2D S_CollisionShape2D => L_MouseArea.L_CollisionShape2D;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Area2D"/>, 节点路径: WeaponRoulette.MouseArea
    /// </summary>
    public MouseArea S_MouseArea => L_MouseArea;

}
