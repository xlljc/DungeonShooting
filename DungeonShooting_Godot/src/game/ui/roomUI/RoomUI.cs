namespace UI;

public abstract partial class RoomUI : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control
    /// </summary>
    public UiNode1_Control Control
    {
        get
        {
            if (_Control == null) _Control = new UiNode1_Control(GetNode<Godot.Control>("Control"));
            return _Control;
        }
    }
    private UiNode1_Control _Control;



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
        public UiNode4_HpBar HpBar
        {
            get
            {
                if (_HpBar == null) _HpBar = new UiNode4_HpBar(Instance.GetNode<Godot.TextureRect>("HpBar"));
                return _HpBar;
            }
        }
        private UiNode4_HpBar _HpBar;

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
        public UiNode6_ShieldBar ShieldBar
        {
            get
            {
                if (_ShieldBar == null) _ShieldBar = new UiNode6_ShieldBar(Instance.GetNode<Godot.TextureRect>("ShieldBar"));
                return _ShieldBar;
            }
        }
        private UiNode6_ShieldBar _ShieldBar;

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
        public UiNode3_HpSlot HpSlot
        {
            get
            {
                if (_HpSlot == null) _HpSlot = new UiNode3_HpSlot(Instance.GetNode<Godot.NinePatchRect>("HpSlot"));
                return _HpSlot;
            }
        }
        private UiNode3_HpSlot _HpSlot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ShieldSlot
        /// </summary>
        public UiNode5_ShieldSlot ShieldSlot
        {
            get
            {
                if (_ShieldSlot == null) _ShieldSlot = new UiNode5_ShieldSlot(Instance.GetNode<Godot.NinePatchRect>("ShieldSlot"));
                return _ShieldSlot;
            }
        }
        private UiNode5_ShieldSlot _ShieldSlot;

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
        public UiNode9_GunSprite GunSprite
        {
            get
            {
                if (_GunSprite == null) _GunSprite = new UiNode9_GunSprite(Instance.GetNode<Godot.TextureRect>("GunSprite"));
                return _GunSprite;
            }
        }
        private UiNode9_GunSprite _GunSprite;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.BulletText
        /// </summary>
        public UiNode10_BulletText BulletText
        {
            get
            {
                if (_BulletText == null) _BulletText = new UiNode10_BulletText(Instance.GetNode<Godot.Label>("BulletText"));
                return _BulletText;
            }
        }
        private UiNode10_BulletText _BulletText;

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
        public UiNode2_HealthBar HealthBar
        {
            get
            {
                if (_HealthBar == null) _HealthBar = new UiNode2_HealthBar(Instance.GetNode<Godot.TextureRect>("HealthBar"));
                return _HealthBar;
            }
        }
        private UiNode2_HealthBar _HealthBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.MapBar
        /// </summary>
        public UiNode7_MapBar MapBar
        {
            get
            {
                if (_MapBar == null) _MapBar = new UiNode7_MapBar(Instance.GetNode<Godot.TextureRect>("MapBar"));
                return _MapBar;
            }
        }
        private UiNode7_MapBar _MapBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.GunBar
        /// </summary>
        public UiNode8_GunBar GunBar
        {
            get
            {
                if (_GunBar == null) _GunBar = new UiNode8_GunBar(Instance.GetNode<Godot.Control>("GunBar"));
                return _GunBar;
            }
        }
        private UiNode8_GunBar _GunBar;

        public UiNode1_Control(Godot.Control node) => Instance = node;
        public UiNode1_Control Clone() => new ((Godot.Control)Instance.Duplicate());
    }

}
