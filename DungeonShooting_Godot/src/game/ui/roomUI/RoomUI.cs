namespace UI.RoomUI;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class RoomUI : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.InteractiveTipBar
    /// </summary>
    public RoomUI_InteractiveTipBar L_InteractiveTipBar
    {
        get
        {
            if (_L_InteractiveTipBar == null) _L_InteractiveTipBar = new RoomUI_InteractiveTipBar(GetNodeOrNull<Godot.Control>("InteractiveTipBar"));
            return _L_InteractiveTipBar;
        }
    }
    private RoomUI_InteractiveTipBar _L_InteractiveTipBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.ReloadBar
    /// </summary>
    public RoomUI_ReloadBar L_ReloadBar
    {
        get
        {
            if (_L_ReloadBar == null) _L_ReloadBar = new RoomUI_ReloadBar(GetNodeOrNull<Godot.Control>("ReloadBar"));
            return _L_ReloadBar;
        }
    }
    private RoomUI_ReloadBar _L_ReloadBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control
    /// </summary>
    public RoomUI_Control L_Control
    {
        get
        {
            if (_L_Control == null) _L_Control = new RoomUI_Control(GetNodeOrNull<Godot.Control>("Control"));
            return _L_Control;
        }
    }
    private RoomUI_Control _L_Control;


    public RoomUI() : base(nameof(RoomUI))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.InteractiveTipBar.Icon
    /// </summary>
    public class RoomUI_Icon : IUiNode<Godot.TextureRect, RoomUI_Icon>
    {
        public RoomUI_Icon(Godot.TextureRect node) : base(node) {  }
        public override RoomUI_Icon Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.InteractiveTipBar.InteractiveIcon
    /// </summary>
    public class RoomUI_InteractiveIcon : IUiNode<Godot.TextureRect, RoomUI_InteractiveIcon>
    {
        public RoomUI_InteractiveIcon(Godot.TextureRect node) : base(node) {  }
        public override RoomUI_InteractiveIcon Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Line2D"/>, 路径: RoomUI.InteractiveTipBar.Line2D
    /// </summary>
    public class RoomUI_Line2D : IUiNode<Godot.Line2D, RoomUI_Line2D>
    {
        public RoomUI_Line2D(Godot.Line2D node) : base(node) {  }
        public override RoomUI_Line2D Clone() => new ((Godot.Line2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.InteractiveTipBar.NameLabel
    /// </summary>
    public class RoomUI_NameLabel : IUiNode<Godot.Label, RoomUI_NameLabel>
    {
        public RoomUI_NameLabel(Godot.Label node) : base(node) {  }
        public override RoomUI_NameLabel Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.InteractiveTipBar
    /// </summary>
    public class RoomUI_InteractiveTipBar : IUiNode<Godot.Control, RoomUI_InteractiveTipBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Icon
        /// </summary>
        public RoomUI_Icon L_Icon
        {
            get
            {
                if (_L_Icon == null) _L_Icon = new RoomUI_Icon(Instance.GetNodeOrNull<Godot.TextureRect>("Icon"));
                return _L_Icon;
            }
        }
        private RoomUI_Icon _L_Icon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.InteractiveIcon
        /// </summary>
        public RoomUI_InteractiveIcon L_InteractiveIcon
        {
            get
            {
                if (_L_InteractiveIcon == null) _L_InteractiveIcon = new RoomUI_InteractiveIcon(Instance.GetNodeOrNull<Godot.TextureRect>("InteractiveIcon"));
                return _L_InteractiveIcon;
            }
        }
        private RoomUI_InteractiveIcon _L_InteractiveIcon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Line2D"/>, 节点路径: RoomUI.Line2D
        /// </summary>
        public RoomUI_Line2D L_Line2D
        {
            get
            {
                if (_L_Line2D == null) _L_Line2D = new RoomUI_Line2D(Instance.GetNodeOrNull<Godot.Line2D>("Line2D"));
                return _L_Line2D;
            }
        }
        private RoomUI_Line2D _L_Line2D;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.NameLabel
        /// </summary>
        public RoomUI_NameLabel L_NameLabel
        {
            get
            {
                if (_L_NameLabel == null) _L_NameLabel = new RoomUI_NameLabel(Instance.GetNodeOrNull<Godot.Label>("NameLabel"));
                return _L_NameLabel;
            }
        }
        private RoomUI_NameLabel _L_NameLabel;

        public RoomUI_InteractiveTipBar(Godot.Control node) : base(node) {  }
        public override RoomUI_InteractiveTipBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ReloadBar.Slot.Block
    /// </summary>
    public class RoomUI_Block : IUiNode<Godot.Sprite2D, RoomUI_Block>
    {
        public RoomUI_Block(Godot.Sprite2D node) : base(node) {  }
        public override RoomUI_Block Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.ReloadBar.Slot
    /// </summary>
    public class RoomUI_Slot : IUiNode<Godot.TextureRect, RoomUI_Slot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ReloadBar.Block
        /// </summary>
        public RoomUI_Block L_Block
        {
            get
            {
                if (_L_Block == null) _L_Block = new RoomUI_Block(Instance.GetNodeOrNull<Godot.Sprite2D>("Block"));
                return _L_Block;
            }
        }
        private RoomUI_Block _L_Block;

        public RoomUI_Slot(Godot.TextureRect node) : base(node) {  }
        public override RoomUI_Slot Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.ReloadBar
    /// </summary>
    public class RoomUI_ReloadBar : IUiNode<Godot.Control, RoomUI_ReloadBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Slot
        /// </summary>
        public RoomUI_Slot L_Slot
        {
            get
            {
                if (_L_Slot == null) _L_Slot = new RoomUI_Slot(Instance.GetNodeOrNull<Godot.TextureRect>("Slot"));
                return _L_Slot;
            }
        }
        private RoomUI_Slot _L_Slot;

        public RoomUI_ReloadBar(Godot.Control node) : base(node) {  }
        public override RoomUI_ReloadBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.LifeBar.Life
    /// </summary>
    public class RoomUI_Life : IUiNode<Godot.TextureRect, RoomUI_Life>
    {
        public RoomUI_Life(Godot.TextureRect node) : base(node) {  }
        public override RoomUI_Life Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.LifeBar
    /// </summary>
    public class RoomUI_LifeBar : IUiNode<Godot.Control, RoomUI_LifeBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.Life
        /// </summary>
        public RoomUI_Life L_Life
        {
            get
            {
                if (_L_Life == null) _L_Life = new RoomUI_Life(Instance.GetNodeOrNull<Godot.TextureRect>("Life"));
                return _L_Life;
            }
        }
        private RoomUI_Life _L_Life;

        public RoomUI_LifeBar(Godot.Control node) : base(node) {  }
        public override RoomUI_LifeBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.MapBar
    /// </summary>
    public class RoomUI_MapBar : IUiNode<Godot.TextureRect, RoomUI_MapBar>
    {
        public RoomUI_MapBar(Godot.TextureRect node) : base(node) {  }
        public override RoomUI_MapBar Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropBg
    /// </summary>
    public class RoomUI_ActivePropBg : IUiNode<Godot.NinePatchRect, RoomUI_ActivePropBg>
    {
        public RoomUI_ActivePropBg(Godot.NinePatchRect node) : base(node) {  }
        public override RoomUI_ActivePropBg Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropSprite
    /// </summary>
    public class RoomUI_ActivePropSprite : IUiNode<Godot.TextureRect, RoomUI_ActivePropSprite>
    {
        public RoomUI_ActivePropSprite(Godot.TextureRect node) : base(node) {  }
        public override RoomUI_ActivePropSprite Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Control.ActivePropBar.CooldownProgress
    /// </summary>
    public class RoomUI_CooldownProgress : IUiNode<Godot.Sprite2D, RoomUI_CooldownProgress>
    {
        public RoomUI_CooldownProgress(Godot.Sprite2D node) : base(node) {  }
        public override RoomUI_CooldownProgress Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropCount
    /// </summary>
    public class RoomUI_ActivePropCount : IUiNode<Godot.Label, RoomUI_ActivePropCount>
    {
        public RoomUI_ActivePropCount(Godot.Label node) : base(node) {  }
        public override RoomUI_ActivePropCount Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropPanel
    /// </summary>
    public class RoomUI_ActivePropPanel : IUiNode<Godot.NinePatchRect, RoomUI_ActivePropPanel>
    {
        public RoomUI_ActivePropPanel(Godot.NinePatchRect node) : base(node) {  }
        public override RoomUI_ActivePropPanel Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.ActivePropBar.ChargeProgressBar
    /// </summary>
    public class RoomUI_ChargeProgressBar : IUiNode<Godot.NinePatchRect, RoomUI_ChargeProgressBar>
    {
        public RoomUI_ChargeProgressBar(Godot.NinePatchRect node) : base(node) {  }
        public override RoomUI_ChargeProgressBar Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Control.ActivePropBar.ChargeProgress
    /// </summary>
    public class RoomUI_ChargeProgress : IUiNode<Godot.Sprite2D, RoomUI_ChargeProgress>
    {
        public RoomUI_ChargeProgress(Godot.Sprite2D node) : base(node) {  }
        public override RoomUI_ChargeProgress Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.ActivePropBar
    /// </summary>
    public class RoomUI_ActivePropBar : IUiNode<Godot.Control, RoomUI_ActivePropBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropBg
        /// </summary>
        public RoomUI_ActivePropBg L_ActivePropBg
        {
            get
            {
                if (_L_ActivePropBg == null) _L_ActivePropBg = new RoomUI_ActivePropBg(Instance.GetNodeOrNull<Godot.NinePatchRect>("ActivePropBg"));
                return _L_ActivePropBg;
            }
        }
        private RoomUI_ActivePropBg _L_ActivePropBg;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.ActivePropSprite
        /// </summary>
        public RoomUI_ActivePropSprite L_ActivePropSprite
        {
            get
            {
                if (_L_ActivePropSprite == null) _L_ActivePropSprite = new RoomUI_ActivePropSprite(Instance.GetNodeOrNull<Godot.TextureRect>("ActivePropSprite"));
                return _L_ActivePropSprite;
            }
        }
        private RoomUI_ActivePropSprite _L_ActivePropSprite;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Control.CooldownProgress
        /// </summary>
        public RoomUI_CooldownProgress L_CooldownProgress
        {
            get
            {
                if (_L_CooldownProgress == null) _L_CooldownProgress = new RoomUI_CooldownProgress(Instance.GetNodeOrNull<Godot.Sprite2D>("CooldownProgress"));
                return _L_CooldownProgress;
            }
        }
        private RoomUI_CooldownProgress _L_CooldownProgress;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.ActivePropCount
        /// </summary>
        public RoomUI_ActivePropCount L_ActivePropCount
        {
            get
            {
                if (_L_ActivePropCount == null) _L_ActivePropCount = new RoomUI_ActivePropCount(Instance.GetNodeOrNull<Godot.Label>("ActivePropCount"));
                return _L_ActivePropCount;
            }
        }
        private RoomUI_ActivePropCount _L_ActivePropCount;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropPanel
        /// </summary>
        public RoomUI_ActivePropPanel L_ActivePropPanel
        {
            get
            {
                if (_L_ActivePropPanel == null) _L_ActivePropPanel = new RoomUI_ActivePropPanel(Instance.GetNodeOrNull<Godot.NinePatchRect>("ActivePropPanel"));
                return _L_ActivePropPanel;
            }
        }
        private RoomUI_ActivePropPanel _L_ActivePropPanel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ChargeProgressBar
        /// </summary>
        public RoomUI_ChargeProgressBar L_ChargeProgressBar
        {
            get
            {
                if (_L_ChargeProgressBar == null) _L_ChargeProgressBar = new RoomUI_ChargeProgressBar(Instance.GetNodeOrNull<Godot.NinePatchRect>("ChargeProgressBar"));
                return _L_ChargeProgressBar;
            }
        }
        private RoomUI_ChargeProgressBar _L_ChargeProgressBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Control.ChargeProgress
        /// </summary>
        public RoomUI_ChargeProgress L_ChargeProgress
        {
            get
            {
                if (_L_ChargeProgress == null) _L_ChargeProgress = new RoomUI_ChargeProgress(Instance.GetNodeOrNull<Godot.Sprite2D>("ChargeProgress"));
                return _L_ChargeProgress;
            }
        }
        private RoomUI_ChargeProgress _L_ChargeProgress;

        public RoomUI_ActivePropBar(Godot.Control node) : base(node) {  }
        public override RoomUI_ActivePropBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.WeaponBar.WeaponPanel.WeaponSprite
    /// </summary>
    public class RoomUI_WeaponSprite : IUiNode<Godot.TextureRect, RoomUI_WeaponSprite>
    {
        public RoomUI_WeaponSprite(Godot.TextureRect node) : base(node) {  }
        public override RoomUI_WeaponSprite Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.WeaponBar.WeaponPanel
    /// </summary>
    public class RoomUI_WeaponPanel : IUiNode<Godot.NinePatchRect, RoomUI_WeaponPanel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.WeaponBar.WeaponSprite
        /// </summary>
        public RoomUI_WeaponSprite L_WeaponSprite
        {
            get
            {
                if (_L_WeaponSprite == null) _L_WeaponSprite = new RoomUI_WeaponSprite(Instance.GetNodeOrNull<Godot.TextureRect>("WeaponSprite"));
                return _L_WeaponSprite;
            }
        }
        private RoomUI_WeaponSprite _L_WeaponSprite;

        public RoomUI_WeaponPanel(Godot.NinePatchRect node) : base(node) {  }
        public override RoomUI_WeaponPanel Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.Control.WeaponBar.AmmoCount
    /// </summary>
    public class RoomUI_AmmoCount : IUiNode<Godot.Label, RoomUI_AmmoCount>
    {
        public RoomUI_AmmoCount(Godot.Label node) : base(node) {  }
        public override RoomUI_AmmoCount Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.WeaponBar
    /// </summary>
    public class RoomUI_WeaponBar : IUiNode<Godot.Control, RoomUI_WeaponBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.WeaponPanel
        /// </summary>
        public RoomUI_WeaponPanel L_WeaponPanel
        {
            get
            {
                if (_L_WeaponPanel == null) _L_WeaponPanel = new RoomUI_WeaponPanel(Instance.GetNodeOrNull<Godot.NinePatchRect>("WeaponPanel"));
                return _L_WeaponPanel;
            }
        }
        private RoomUI_WeaponPanel _L_WeaponPanel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.AmmoCount
        /// </summary>
        public RoomUI_AmmoCount L_AmmoCount
        {
            get
            {
                if (_L_AmmoCount == null) _L_AmmoCount = new RoomUI_AmmoCount(Instance.GetNodeOrNull<Godot.Label>("AmmoCount"));
                return _L_AmmoCount;
            }
        }
        private RoomUI_AmmoCount _L_AmmoCount;

        public RoomUI_WeaponBar(Godot.Control node) : base(node) {  }
        public override RoomUI_WeaponBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control
    /// </summary>
    public class RoomUI_Control : IUiNode<Godot.Control, RoomUI_Control>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.LifeBar
        /// </summary>
        public RoomUI_LifeBar L_LifeBar
        {
            get
            {
                if (_L_LifeBar == null) _L_LifeBar = new RoomUI_LifeBar(Instance.GetNodeOrNull<Godot.Control>("LifeBar"));
                return _L_LifeBar;
            }
        }
        private RoomUI_LifeBar _L_LifeBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.MapBar
        /// </summary>
        public RoomUI_MapBar L_MapBar
        {
            get
            {
                if (_L_MapBar == null) _L_MapBar = new RoomUI_MapBar(Instance.GetNodeOrNull<Godot.TextureRect>("MapBar"));
                return _L_MapBar;
            }
        }
        private RoomUI_MapBar _L_MapBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.ActivePropBar
        /// </summary>
        public RoomUI_ActivePropBar L_ActivePropBar
        {
            get
            {
                if (_L_ActivePropBar == null) _L_ActivePropBar = new RoomUI_ActivePropBar(Instance.GetNodeOrNull<Godot.Control>("ActivePropBar"));
                return _L_ActivePropBar;
            }
        }
        private RoomUI_ActivePropBar _L_ActivePropBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.WeaponBar
        /// </summary>
        public RoomUI_WeaponBar L_WeaponBar
        {
            get
            {
                if (_L_WeaponBar == null) _L_WeaponBar = new RoomUI_WeaponBar(Instance.GetNodeOrNull<Godot.Control>("WeaponBar"));
                return _L_WeaponBar;
            }
        }
        private RoomUI_WeaponBar _L_WeaponBar;

        public RoomUI_Control(Godot.Control node) : base(node) {  }
        public override RoomUI_Control Clone() => new ((Godot.Control)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.InteractiveTipBar.Icon
    /// </summary>
    public RoomUI_Icon S_Icon => L_InteractiveTipBar.L_Icon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.InteractiveTipBar.InteractiveIcon
    /// </summary>
    public RoomUI_InteractiveIcon S_InteractiveIcon => L_InteractiveTipBar.L_InteractiveIcon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.InteractiveTipBar.Line2D
    /// </summary>
    public RoomUI_Line2D S_Line2D => L_InteractiveTipBar.L_Line2D;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.InteractiveTipBar.NameLabel
    /// </summary>
    public RoomUI_NameLabel S_NameLabel => L_InteractiveTipBar.L_NameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.RoomUI.RoomUIPanel"/>, 节点路径: RoomUI.InteractiveTipBar
    /// </summary>
    public RoomUI_InteractiveTipBar S_InteractiveTipBar => L_InteractiveTipBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.ReloadBar.Slot.Block
    /// </summary>
    public RoomUI_Block S_Block => L_ReloadBar.L_Slot.L_Block;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.ReloadBar.Slot
    /// </summary>
    public RoomUI_Slot S_Slot => L_ReloadBar.L_Slot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.RoomUI.RoomUIPanel"/>, 节点路径: RoomUI.ReloadBar
    /// </summary>
    public RoomUI_ReloadBar S_ReloadBar => L_ReloadBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.LifeBar.Life
    /// </summary>
    public RoomUI_Life S_Life => L_Control.L_LifeBar.L_Life;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.LifeBar
    /// </summary>
    public RoomUI_LifeBar S_LifeBar => L_Control.L_LifeBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.MapBar
    /// </summary>
    public RoomUI_MapBar S_MapBar => L_Control.L_MapBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.ActivePropBar.ActivePropBg
    /// </summary>
    public RoomUI_ActivePropBg S_ActivePropBg => L_Control.L_ActivePropBar.L_ActivePropBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.ActivePropBar.ActivePropSprite
    /// </summary>
    public RoomUI_ActivePropSprite S_ActivePropSprite => L_Control.L_ActivePropBar.L_ActivePropSprite;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.ActivePropBar.CooldownProgress
    /// </summary>
    public RoomUI_CooldownProgress S_CooldownProgress => L_Control.L_ActivePropBar.L_CooldownProgress;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.ActivePropBar.ActivePropCount
    /// </summary>
    public RoomUI_ActivePropCount S_ActivePropCount => L_Control.L_ActivePropBar.L_ActivePropCount;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.ActivePropBar.ActivePropPanel
    /// </summary>
    public RoomUI_ActivePropPanel S_ActivePropPanel => L_Control.L_ActivePropBar.L_ActivePropPanel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.ActivePropBar.ChargeProgressBar
    /// </summary>
    public RoomUI_ChargeProgressBar S_ChargeProgressBar => L_Control.L_ActivePropBar.L_ChargeProgressBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.ActivePropBar.ChargeProgress
    /// </summary>
    public RoomUI_ChargeProgress S_ChargeProgress => L_Control.L_ActivePropBar.L_ChargeProgress;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.ActivePropBar
    /// </summary>
    public RoomUI_ActivePropBar S_ActivePropBar => L_Control.L_ActivePropBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.WeaponBar.WeaponPanel.WeaponSprite
    /// </summary>
    public RoomUI_WeaponSprite S_WeaponSprite => L_Control.L_WeaponBar.L_WeaponPanel.L_WeaponSprite;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.WeaponBar.WeaponPanel
    /// </summary>
    public RoomUI_WeaponPanel S_WeaponPanel => L_Control.L_WeaponBar.L_WeaponPanel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.WeaponBar.AmmoCount
    /// </summary>
    public RoomUI_AmmoCount S_AmmoCount => L_Control.L_WeaponBar.L_AmmoCount;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.WeaponBar
    /// </summary>
    public RoomUI_WeaponBar S_WeaponBar => L_Control.L_WeaponBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.RoomUI.RoomUIPanel"/>, 节点路径: RoomUI.Control
    /// </summary>
    public RoomUI_Control S_Control => L_Control;

}
