namespace UI;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class RoomUI : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control
    /// </summary>
    public UiNode1_Control L_Control
    {
        get
        {
            if (_L_Control == null) _L_Control = new UiNode1_Control(GetNode<Godot.Control>("Control"));
            return _L_Control;
        }
    }
    private UiNode1_Control _L_Control;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node"/>, 节点路径: RoomUI.GlobalNode
    /// </summary>
    public UiNode11_GlobalNode L_GlobalNode
    {
        get
        {
            if (_L_GlobalNode == null) _L_GlobalNode = new UiNode11_GlobalNode(GetNode<Godot.Node>("GlobalNode"));
            return _L_GlobalNode;
        }
    }
    private UiNode11_GlobalNode _L_GlobalNode;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node"/>, 节点路径: RoomUI.ViewNode
    /// </summary>
    public UiNode12_ViewNode L_ViewNode
    {
        get
        {
            if (_L_ViewNode == null) _L_ViewNode = new UiNode12_ViewNode(GetNode<Godot.Node>("ViewNode"));
            return _L_ViewNode;
        }
    }
    private UiNode12_ViewNode _L_ViewNode;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Cursor"/>, 节点路径: RoomUI.Cursor
    /// </summary>
    public UiNode20_Cursor L_Cursor
    {
        get
        {
            if (_L_Cursor == null) _L_Cursor = new UiNode20_Cursor(GetNode<Cursor>("Cursor"));
            return _L_Cursor;
        }
    }
    private UiNode20_Cursor _L_Cursor;



    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.HealthBar.HpSlot.HpBar
    /// </summary>
    public class UiNode4_HpBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.HpSlot.HpBar
        /// </summary>
        public Godot.TextureRect Instance { get; }

        public UiNode4_HpBar(Godot.TextureRect node) => Instance = node;
        public UiNode4_HpBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.HealthBar.HpSlot
    /// </summary>
    public class UiNode3_HpSlot
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.HealthBar.HpSlot
        /// </summary>
        public Godot.NinePatchRect Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.HpBar
        /// </summary>
        public UiNode4_HpBar L_HpBar
        {
            get
            {
                if (_L_HpBar == null) _L_HpBar = new UiNode4_HpBar(Instance.GetNode<Godot.TextureRect>("HpBar"));
                return _L_HpBar;
            }
        }
        private UiNode4_HpBar _L_HpBar;

        public UiNode3_HpSlot(Godot.NinePatchRect node) => Instance = node;
        public UiNode3_HpSlot Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.HealthBar.ShieldSlot.ShieldBar
    /// </summary>
    public class UiNode6_ShieldBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.ShieldSlot.ShieldBar
        /// </summary>
        public Godot.TextureRect Instance { get; }

        public UiNode6_ShieldBar(Godot.TextureRect node) => Instance = node;
        public UiNode6_ShieldBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.HealthBar.ShieldSlot
    /// </summary>
    public class UiNode5_ShieldSlot
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.HealthBar.ShieldSlot
        /// </summary>
        public Godot.NinePatchRect Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.ShieldBar
        /// </summary>
        public UiNode6_ShieldBar L_ShieldBar
        {
            get
            {
                if (_L_ShieldBar == null) _L_ShieldBar = new UiNode6_ShieldBar(Instance.GetNode<Godot.TextureRect>("ShieldBar"));
                return _L_ShieldBar;
            }
        }
        private UiNode6_ShieldBar _L_ShieldBar;

        public UiNode5_ShieldSlot(Godot.NinePatchRect node) => Instance = node;
        public UiNode5_ShieldSlot Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.HealthBar
    /// </summary>
    public class UiNode2_HealthBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar
        /// </summary>
        public Godot.TextureRect Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.HpSlot
        /// </summary>
        public UiNode3_HpSlot L_HpSlot
        {
            get
            {
                if (_L_HpSlot == null) _L_HpSlot = new UiNode3_HpSlot(Instance.GetNode<Godot.NinePatchRect>("HpSlot"));
                return _L_HpSlot;
            }
        }
        private UiNode3_HpSlot _L_HpSlot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ShieldSlot
        /// </summary>
        public UiNode5_ShieldSlot L_ShieldSlot
        {
            get
            {
                if (_L_ShieldSlot == null) _L_ShieldSlot = new UiNode5_ShieldSlot(Instance.GetNode<Godot.NinePatchRect>("ShieldSlot"));
                return _L_ShieldSlot;
            }
        }
        private UiNode5_ShieldSlot _L_ShieldSlot;

        public UiNode2_HealthBar(Godot.TextureRect node) => Instance = node;
        public UiNode2_HealthBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.MapBar
    /// </summary>
    public class UiNode7_MapBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.MapBar
        /// </summary>
        public Godot.TextureRect Instance { get; }

        public UiNode7_MapBar(Godot.TextureRect node) => Instance = node;
        public UiNode7_MapBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.GunBar.GunSprite
    /// </summary>
    public class UiNode9_GunSprite
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.GunBar.GunSprite
        /// </summary>
        public Godot.TextureRect Instance { get; }

        public UiNode9_GunSprite(Godot.TextureRect node) => Instance = node;
        public UiNode9_GunSprite Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.Control.GunBar.BulletText
    /// </summary>
    public class UiNode10_BulletText
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.GunBar.BulletText
        /// </summary>
        public Godot.Label Instance { get; }

        public UiNode10_BulletText(Godot.Label node) => Instance = node;
        public UiNode10_BulletText Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.GunBar
    /// </summary>
    public class UiNode8_GunBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.GunBar
        /// </summary>
        public Godot.Control Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.GunSprite
        /// </summary>
        public UiNode9_GunSprite L_GunSprite
        {
            get
            {
                if (_L_GunSprite == null) _L_GunSprite = new UiNode9_GunSprite(Instance.GetNode<Godot.TextureRect>("GunSprite"));
                return _L_GunSprite;
            }
        }
        private UiNode9_GunSprite _L_GunSprite;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.BulletText
        /// </summary>
        public UiNode10_BulletText L_BulletText
        {
            get
            {
                if (_L_BulletText == null) _L_BulletText = new UiNode10_BulletText(Instance.GetNode<Godot.Label>("BulletText"));
                return _L_BulletText;
            }
        }
        private UiNode10_BulletText _L_BulletText;

        public UiNode8_GunBar(Godot.Control node) => Instance = node;
        public UiNode8_GunBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control
    /// </summary>
    public class UiNode1_Control
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control
        /// </summary>
        public Godot.Control Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.HealthBar
        /// </summary>
        public UiNode2_HealthBar L_HealthBar
        {
            get
            {
                if (_L_HealthBar == null) _L_HealthBar = new UiNode2_HealthBar(Instance.GetNode<Godot.TextureRect>("HealthBar"));
                return _L_HealthBar;
            }
        }
        private UiNode2_HealthBar _L_HealthBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.MapBar
        /// </summary>
        public UiNode7_MapBar L_MapBar
        {
            get
            {
                if (_L_MapBar == null) _L_MapBar = new UiNode7_MapBar(Instance.GetNode<Godot.TextureRect>("MapBar"));
                return _L_MapBar;
            }
        }
        private UiNode7_MapBar _L_MapBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.GunBar
        /// </summary>
        public UiNode8_GunBar L_GunBar
        {
            get
            {
                if (_L_GunBar == null) _L_GunBar = new UiNode8_GunBar(Instance.GetNode<Godot.Control>("GunBar"));
                return _L_GunBar;
            }
        }
        private UiNode8_GunBar _L_GunBar;

        public UiNode1_Control(Godot.Control node) => Instance = node;
        public UiNode1_Control Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node"/>, 路径: RoomUI.GlobalNode
    /// </summary>
    public class UiNode11_GlobalNode
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Node"/>, 节点路径: RoomUI.GlobalNode
        /// </summary>
        public Godot.Node Instance { get; }

        public UiNode11_GlobalNode(Godot.Node node) => Instance = node;
        public UiNode11_GlobalNode Clone() => new ((Godot.Node)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ViewNode.InteractiveTipBar.Icon
    /// </summary>
    public class UiNode14_Icon
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ViewNode.InteractiveTipBar.Icon
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode14_Icon(Godot.Sprite2D node) => Instance = node;
        public UiNode14_Icon Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ViewNode.InteractiveTipBar.InteractiveIcon
    /// </summary>
    public class UiNode15_InteractiveIcon
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ViewNode.InteractiveTipBar.InteractiveIcon
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode15_InteractiveIcon(Godot.Sprite2D node) => Instance = node;
        public UiNode15_InteractiveIcon Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Line2D"/>, 路径: RoomUI.ViewNode.InteractiveTipBar.Line2D
    /// </summary>
    public class UiNode16_Line2D
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Line2D"/>, 节点路径: RoomUI.ViewNode.InteractiveTipBar.Line2D
        /// </summary>
        public Godot.Line2D Instance { get; }

        public UiNode16_Line2D(Godot.Line2D node) => Instance = node;
        public UiNode16_Line2D Clone() => new ((Godot.Line2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="InteractiveTipBar"/>, 路径: RoomUI.ViewNode.InteractiveTipBar
    /// </summary>
    public class UiNode13_InteractiveTipBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="InteractiveTipBar"/>, 节点路径: RoomUI.ViewNode.InteractiveTipBar
        /// </summary>
        public InteractiveTipBar Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ViewNode.Icon
        /// </summary>
        public UiNode14_Icon L_Icon
        {
            get
            {
                if (_L_Icon == null) _L_Icon = new UiNode14_Icon(Instance.GetNode<Godot.Sprite2D>("Icon"));
                return _L_Icon;
            }
        }
        private UiNode14_Icon _L_Icon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ViewNode.InteractiveIcon
        /// </summary>
        public UiNode15_InteractiveIcon L_InteractiveIcon
        {
            get
            {
                if (_L_InteractiveIcon == null) _L_InteractiveIcon = new UiNode15_InteractiveIcon(Instance.GetNode<Godot.Sprite2D>("InteractiveIcon"));
                return _L_InteractiveIcon;
            }
        }
        private UiNode15_InteractiveIcon _L_InteractiveIcon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Line2D"/>, 节点路径: RoomUI.ViewNode.Line2D
        /// </summary>
        public UiNode16_Line2D L_Line2D
        {
            get
            {
                if (_L_Line2D == null) _L_Line2D = new UiNode16_Line2D(Instance.GetNode<Godot.Line2D>("Line2D"));
                return _L_Line2D;
            }
        }
        private UiNode16_Line2D _L_Line2D;

        public UiNode13_InteractiveTipBar(InteractiveTipBar node) => Instance = node;
        public UiNode13_InteractiveTipBar Clone() => new ((InteractiveTipBar)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ViewNode.ReloadBar.Slot.Block
    /// </summary>
    public class UiNode19_Block
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ViewNode.ReloadBar.Slot.Block
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode19_Block(Godot.Sprite2D node) => Instance = node;
        public UiNode19_Block Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ViewNode.ReloadBar.Slot
    /// </summary>
    public class UiNode18_Slot
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ViewNode.ReloadBar.Slot
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ViewNode.ReloadBar.Block
        /// </summary>
        public UiNode19_Block L_Block
        {
            get
            {
                if (_L_Block == null) _L_Block = new UiNode19_Block(Instance.GetNode<Godot.Sprite2D>("Block"));
                return _L_Block;
            }
        }
        private UiNode19_Block _L_Block;

        public UiNode18_Slot(Godot.Sprite2D node) => Instance = node;
        public UiNode18_Slot Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="ReloadBar"/>, 路径: RoomUI.ViewNode.ReloadBar
    /// </summary>
    public class UiNode17_ReloadBar
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="ReloadBar"/>, 节点路径: RoomUI.ViewNode.ReloadBar
        /// </summary>
        public ReloadBar Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ViewNode.Slot
        /// </summary>
        public UiNode18_Slot L_Slot
        {
            get
            {
                if (_L_Slot == null) _L_Slot = new UiNode18_Slot(Instance.GetNode<Godot.Sprite2D>("Slot"));
                return _L_Slot;
            }
        }
        private UiNode18_Slot _L_Slot;

        public UiNode17_ReloadBar(ReloadBar node) => Instance = node;
        public UiNode17_ReloadBar Clone() => new ((ReloadBar)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node"/>, 路径: RoomUI.ViewNode
    /// </summary>
    public class UiNode12_ViewNode
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Node"/>, 节点路径: RoomUI.ViewNode
        /// </summary>
        public Godot.Node Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="InteractiveTipBar"/>, 节点路径: RoomUI.InteractiveTipBar
        /// </summary>
        public UiNode13_InteractiveTipBar L_InteractiveTipBar
        {
            get
            {
                if (_L_InteractiveTipBar == null) _L_InteractiveTipBar = new UiNode13_InteractiveTipBar(Instance.GetNode<InteractiveTipBar>("InteractiveTipBar"));
                return _L_InteractiveTipBar;
            }
        }
        private UiNode13_InteractiveTipBar _L_InteractiveTipBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="ReloadBar"/>, 节点路径: RoomUI.ReloadBar
        /// </summary>
        public UiNode17_ReloadBar L_ReloadBar
        {
            get
            {
                if (_L_ReloadBar == null) _L_ReloadBar = new UiNode17_ReloadBar(Instance.GetNode<ReloadBar>("ReloadBar"));
                return _L_ReloadBar;
            }
        }
        private UiNode17_ReloadBar _L_ReloadBar;

        public UiNode12_ViewNode(Godot.Node node) => Instance = node;
        public UiNode12_ViewNode Clone() => new ((Godot.Node)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Cursor.LT
    /// </summary>
    public class UiNode21_LT
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Cursor.LT
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode21_LT(Godot.Sprite2D node) => Instance = node;
        public UiNode21_LT Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Cursor.LB
    /// </summary>
    public class UiNode22_LB
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Cursor.LB
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode22_LB(Godot.Sprite2D node) => Instance = node;
        public UiNode22_LB Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Cursor.RT
    /// </summary>
    public class UiNode23_RT
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Cursor.RT
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode23_RT(Godot.Sprite2D node) => Instance = node;
        public UiNode23_RT Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Cursor.RB
    /// </summary>
    public class UiNode24_RB
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Cursor.RB
        /// </summary>
        public Godot.Sprite2D Instance { get; }

        public UiNode24_RB(Godot.Sprite2D node) => Instance = node;
        public UiNode24_RB Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Cursor"/>, 路径: RoomUI.Cursor
    /// </summary>
    public class UiNode20_Cursor
    {
        /// <summary>
        /// Ui节点实例, 节点类型: <see cref="Cursor"/>, 节点路径: RoomUI.Cursor
        /// </summary>
        public Cursor Instance { get; }

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.LT
        /// </summary>
        public UiNode21_LT L_LT
        {
            get
            {
                if (_L_LT == null) _L_LT = new UiNode21_LT(Instance.GetNode<Godot.Sprite2D>("LT"));
                return _L_LT;
            }
        }
        private UiNode21_LT _L_LT;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.LB
        /// </summary>
        public UiNode22_LB L_LB
        {
            get
            {
                if (_L_LB == null) _L_LB = new UiNode22_LB(Instance.GetNode<Godot.Sprite2D>("LB"));
                return _L_LB;
            }
        }
        private UiNode22_LB _L_LB;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.RT
        /// </summary>
        public UiNode23_RT L_RT
        {
            get
            {
                if (_L_RT == null) _L_RT = new UiNode23_RT(Instance.GetNode<Godot.Sprite2D>("RT"));
                return _L_RT;
            }
        }
        private UiNode23_RT _L_RT;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.RB
        /// </summary>
        public UiNode24_RB L_RB
        {
            get
            {
                if (_L_RB == null) _L_RB = new UiNode24_RB(Instance.GetNode<Godot.Sprite2D>("RB"));
                return _L_RB;
            }
        }
        private UiNode24_RB _L_RB;

        public UiNode20_Cursor(Cursor node) => Instance = node;
        public UiNode20_Cursor Clone() => new ((Cursor)Instance.Duplicate());
    }

}
