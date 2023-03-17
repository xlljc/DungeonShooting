namespace UI.RoomUI;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class RoomUI : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: RoomUI.InteractiveTipBar
    /// </summary>
    public UiNode_InteractiveTipBar L_InteractiveTipBar
    {
        get
        {
            if (_L_InteractiveTipBar == null) _L_InteractiveTipBar = new UiNode_InteractiveTipBar(GetNodeOrNull<Godot.Node2D>("InteractiveTipBar"));
            return _L_InteractiveTipBar;
        }
    }
    private UiNode_InteractiveTipBar _L_InteractiveTipBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Node2D"/>, 节点路径: RoomUI.ReloadBar
    /// </summary>
    public UiNode_ReloadBar L_ReloadBar
    {
        get
        {
            if (_L_ReloadBar == null) _L_ReloadBar = new UiNode_ReloadBar(GetNodeOrNull<Godot.Node2D>("ReloadBar"));
            return _L_ReloadBar;
        }
    }
    private UiNode_ReloadBar _L_ReloadBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control
    /// </summary>
    public UiNode_Control L_Control
    {
        get
        {
            if (_L_Control == null) _L_Control = new UiNode_Control(GetNodeOrNull<Godot.Control>("Control"));
            return _L_Control;
        }
    }
    private UiNode_Control _L_Control;



    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.InteractiveTipBar.Icon
    /// </summary>
    public class UiNode_Icon : IUiNode<Godot.Sprite2D, UiNode_Icon>
    {
        public UiNode_Icon(Godot.Sprite2D node) : base(node) {  }
        public override UiNode_Icon Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.InteractiveTipBar.InteractiveIcon
    /// </summary>
    public class UiNode_InteractiveIcon : IUiNode<Godot.Sprite2D, UiNode_InteractiveIcon>
    {
        public UiNode_InteractiveIcon(Godot.Sprite2D node) : base(node) {  }
        public override UiNode_InteractiveIcon Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Line2D"/>, 路径: RoomUI.InteractiveTipBar.Line2D
    /// </summary>
    public class UiNode_Line2D : IUiNode<Godot.Line2D, UiNode_Line2D>
    {
        public UiNode_Line2D(Godot.Line2D node) : base(node) {  }
        public override UiNode_Line2D Clone() => new ((Godot.Line2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node2D"/>, 路径: RoomUI.InteractiveTipBar
    /// </summary>
    public class UiNode_InteractiveTipBar : IUiNode<Godot.Node2D, UiNode_InteractiveTipBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Icon
        /// </summary>
        public UiNode_Icon L_Icon
        {
            get
            {
                if (_L_Icon == null) _L_Icon = new UiNode_Icon(Instance.GetNodeOrNull<Godot.Sprite2D>("Icon"));
                return _L_Icon;
            }
        }
        private UiNode_Icon _L_Icon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.InteractiveIcon
        /// </summary>
        public UiNode_InteractiveIcon L_InteractiveIcon
        {
            get
            {
                if (_L_InteractiveIcon == null) _L_InteractiveIcon = new UiNode_InteractiveIcon(Instance.GetNodeOrNull<Godot.Sprite2D>("InteractiveIcon"));
                return _L_InteractiveIcon;
            }
        }
        private UiNode_InteractiveIcon _L_InteractiveIcon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Line2D"/>, 节点路径: RoomUI.Line2D
        /// </summary>
        public UiNode_Line2D L_Line2D
        {
            get
            {
                if (_L_Line2D == null) _L_Line2D = new UiNode_Line2D(Instance.GetNodeOrNull<Godot.Line2D>("Line2D"));
                return _L_Line2D;
            }
        }
        private UiNode_Line2D _L_Line2D;

        public UiNode_InteractiveTipBar(Godot.Node2D node) : base(node) {  }
        public override UiNode_InteractiveTipBar Clone() => new ((Godot.Node2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ReloadBar.Slot.Block
    /// </summary>
    public class UiNode_Block : IUiNode<Godot.Sprite2D, UiNode_Block>
    {
        public UiNode_Block(Godot.Sprite2D node) : base(node) {  }
        public override UiNode_Block Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ReloadBar.Slot
    /// </summary>
    public class UiNode_Slot : IUiNode<Godot.Sprite2D, UiNode_Slot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ReloadBar.Block
        /// </summary>
        public UiNode_Block L_Block
        {
            get
            {
                if (_L_Block == null) _L_Block = new UiNode_Block(Instance.GetNodeOrNull<Godot.Sprite2D>("Block"));
                return _L_Block;
            }
        }
        private UiNode_Block _L_Block;

        public UiNode_Slot(Godot.Sprite2D node) : base(node) {  }
        public override UiNode_Slot Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Node2D"/>, 路径: RoomUI.ReloadBar
    /// </summary>
    public class UiNode_ReloadBar : IUiNode<Godot.Node2D, UiNode_ReloadBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Slot
        /// </summary>
        public UiNode_Slot L_Slot
        {
            get
            {
                if (_L_Slot == null) _L_Slot = new UiNode_Slot(Instance.GetNodeOrNull<Godot.Sprite2D>("Slot"));
                return _L_Slot;
            }
        }
        private UiNode_Slot _L_Slot;

        public UiNode_ReloadBar(Godot.Node2D node) : base(node) {  }
        public override UiNode_ReloadBar Clone() => new ((Godot.Node2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.HealthBar.HpSlot.HpBar
    /// </summary>
    public class UiNode_HpBar : IUiNode<Godot.TextureRect, UiNode_HpBar>
    {
        public UiNode_HpBar(Godot.TextureRect node) : base(node) {  }
        public override UiNode_HpBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.HealthBar.HpSlot
    /// </summary>
    public class UiNode_HpSlot : IUiNode<Godot.NinePatchRect, UiNode_HpSlot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.HpBar
        /// </summary>
        public UiNode_HpBar L_HpBar
        {
            get
            {
                if (_L_HpBar == null) _L_HpBar = new UiNode_HpBar(Instance.GetNodeOrNull<Godot.TextureRect>("HpBar"));
                return _L_HpBar;
            }
        }
        private UiNode_HpBar _L_HpBar;

        public UiNode_HpSlot(Godot.NinePatchRect node) : base(node) {  }
        public override UiNode_HpSlot Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.HealthBar.ShieldSlot.ShieldBar
    /// </summary>
    public class UiNode_ShieldBar : IUiNode<Godot.TextureRect, UiNode_ShieldBar>
    {
        public UiNode_ShieldBar(Godot.TextureRect node) : base(node) {  }
        public override UiNode_ShieldBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.HealthBar.ShieldSlot
    /// </summary>
    public class UiNode_ShieldSlot : IUiNode<Godot.NinePatchRect, UiNode_ShieldSlot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.HealthBar.ShieldBar
        /// </summary>
        public UiNode_ShieldBar L_ShieldBar
        {
            get
            {
                if (_L_ShieldBar == null) _L_ShieldBar = new UiNode_ShieldBar(Instance.GetNodeOrNull<Godot.TextureRect>("ShieldBar"));
                return _L_ShieldBar;
            }
        }
        private UiNode_ShieldBar _L_ShieldBar;

        public UiNode_ShieldSlot(Godot.NinePatchRect node) : base(node) {  }
        public override UiNode_ShieldSlot Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.HealthBar
    /// </summary>
    public class UiNode_HealthBar : IUiNode<Godot.TextureRect, UiNode_HealthBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.HpSlot
        /// </summary>
        public UiNode_HpSlot L_HpSlot
        {
            get
            {
                if (_L_HpSlot == null) _L_HpSlot = new UiNode_HpSlot(Instance.GetNodeOrNull<Godot.NinePatchRect>("HpSlot"));
                return _L_HpSlot;
            }
        }
        private UiNode_HpSlot _L_HpSlot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ShieldSlot
        /// </summary>
        public UiNode_ShieldSlot L_ShieldSlot
        {
            get
            {
                if (_L_ShieldSlot == null) _L_ShieldSlot = new UiNode_ShieldSlot(Instance.GetNodeOrNull<Godot.NinePatchRect>("ShieldSlot"));
                return _L_ShieldSlot;
            }
        }
        private UiNode_ShieldSlot _L_ShieldSlot;

        public UiNode_HealthBar(Godot.TextureRect node) : base(node) {  }
        public override UiNode_HealthBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.MapBar
    /// </summary>
    public class UiNode_MapBar : IUiNode<Godot.TextureRect, UiNode_MapBar>
    {
        public UiNode_MapBar(Godot.TextureRect node) : base(node) {  }
        public override UiNode_MapBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.GunBar.GunSprite
    /// </summary>
    public class UiNode_GunSprite : IUiNode<Godot.TextureRect, UiNode_GunSprite>
    {
        public UiNode_GunSprite(Godot.TextureRect node) : base(node) {  }
        public override UiNode_GunSprite Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.Control.GunBar.BulletText
    /// </summary>
    public class UiNode_BulletText : IUiNode<Godot.Label, UiNode_BulletText>
    {
        public UiNode_BulletText(Godot.Label node) : base(node) {  }
        public override UiNode_BulletText Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.GunBar
    /// </summary>
    public class UiNode_GunBar : IUiNode<Godot.Control, UiNode_GunBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.GunSprite
        /// </summary>
        public UiNode_GunSprite L_GunSprite
        {
            get
            {
                if (_L_GunSprite == null) _L_GunSprite = new UiNode_GunSprite(Instance.GetNodeOrNull<Godot.TextureRect>("GunSprite"));
                return _L_GunSprite;
            }
        }
        private UiNode_GunSprite _L_GunSprite;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.BulletText
        /// </summary>
        public UiNode_BulletText L_BulletText
        {
            get
            {
                if (_L_BulletText == null) _L_BulletText = new UiNode_BulletText(Instance.GetNodeOrNull<Godot.Label>("BulletText"));
                return _L_BulletText;
            }
        }
        private UiNode_BulletText _L_BulletText;

        public UiNode_GunBar(Godot.Control node) : base(node) {  }
        public override UiNode_GunBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control
    /// </summary>
    public class UiNode_Control : IUiNode<Godot.Control, UiNode_Control>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.HealthBar
        /// </summary>
        public UiNode_HealthBar L_HealthBar
        {
            get
            {
                if (_L_HealthBar == null) _L_HealthBar = new UiNode_HealthBar(Instance.GetNodeOrNull<Godot.TextureRect>("HealthBar"));
                return _L_HealthBar;
            }
        }
        private UiNode_HealthBar _L_HealthBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.MapBar
        /// </summary>
        public UiNode_MapBar L_MapBar
        {
            get
            {
                if (_L_MapBar == null) _L_MapBar = new UiNode_MapBar(Instance.GetNodeOrNull<Godot.TextureRect>("MapBar"));
                return _L_MapBar;
            }
        }
        private UiNode_MapBar _L_MapBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.GunBar
        /// </summary>
        public UiNode_GunBar L_GunBar
        {
            get
            {
                if (_L_GunBar == null) _L_GunBar = new UiNode_GunBar(Instance.GetNodeOrNull<Godot.Control>("GunBar"));
                return _L_GunBar;
            }
        }
        private UiNode_GunBar _L_GunBar;

        public UiNode_Control(Godot.Control node) : base(node) {  }
        public override UiNode_Control Clone() => new ((Godot.Control)Instance.Duplicate());
    }

}
