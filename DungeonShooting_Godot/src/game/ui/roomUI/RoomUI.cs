namespace UI.RoomUI;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class RoomUI : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.InteractiveTipBar
    /// </summary>
    public UiNode_InteractiveTipBar L_InteractiveTipBar
    {
        get
        {
            if (_L_InteractiveTipBar == null) _L_InteractiveTipBar = new UiNode_InteractiveTipBar(GetNodeOrNull<Godot.Control>("InteractiveTipBar"));
            return _L_InteractiveTipBar;
        }
    }
    private UiNode_InteractiveTipBar _L_InteractiveTipBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.ReloadBar
    /// </summary>
    public UiNode_ReloadBar L_ReloadBar
    {
        get
        {
            if (_L_ReloadBar == null) _L_ReloadBar = new UiNode_ReloadBar(GetNodeOrNull<Godot.Control>("ReloadBar"));
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


    public RoomUI() : base(nameof(RoomUI))
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.InteractiveTipBar.Icon
    /// </summary>
    public class UiNode_Icon : IUiNode<Godot.TextureRect, UiNode_Icon>
    {
        public UiNode_Icon(Godot.TextureRect node) : base(node) {  }
        public override UiNode_Icon Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.InteractiveTipBar.InteractiveIcon
    /// </summary>
    public class UiNode_InteractiveIcon : IUiNode<Godot.TextureRect, UiNode_InteractiveIcon>
    {
        public UiNode_InteractiveIcon(Godot.TextureRect node) : base(node) {  }
        public override UiNode_InteractiveIcon Clone() => new ((Godot.TextureRect)Instance.Duplicate());
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
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.InteractiveTipBar.NameLabel
    /// </summary>
    public class UiNode_NameLabel : IUiNode<Godot.Label, UiNode_NameLabel>
    {
        public UiNode_NameLabel(Godot.Label node) : base(node) {  }
        public override UiNode_NameLabel Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.InteractiveTipBar
    /// </summary>
    public class UiNode_InteractiveTipBar : IUiNode<Godot.Control, UiNode_InteractiveTipBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Icon
        /// </summary>
        public UiNode_Icon L_Icon
        {
            get
            {
                if (_L_Icon == null) _L_Icon = new UiNode_Icon(Instance.GetNodeOrNull<Godot.TextureRect>("Icon"));
                return _L_Icon;
            }
        }
        private UiNode_Icon _L_Icon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.InteractiveIcon
        /// </summary>
        public UiNode_InteractiveIcon L_InteractiveIcon
        {
            get
            {
                if (_L_InteractiveIcon == null) _L_InteractiveIcon = new UiNode_InteractiveIcon(Instance.GetNodeOrNull<Godot.TextureRect>("InteractiveIcon"));
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

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.NameLabel
        /// </summary>
        public UiNode_NameLabel L_NameLabel
        {
            get
            {
                if (_L_NameLabel == null) _L_NameLabel = new UiNode_NameLabel(Instance.GetNodeOrNull<Godot.Label>("NameLabel"));
                return _L_NameLabel;
            }
        }
        private UiNode_NameLabel _L_NameLabel;

        public UiNode_InteractiveTipBar(Godot.Control node) : base(node) {  }
        public override UiNode_InteractiveTipBar Clone() => new ((Godot.Control)Instance.Duplicate());
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
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.ReloadBar.Slot
    /// </summary>
    public class UiNode_Slot : IUiNode<Godot.TextureRect, UiNode_Slot>
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

        public UiNode_Slot(Godot.TextureRect node) : base(node) {  }
        public override UiNode_Slot Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.ReloadBar
    /// </summary>
    public class UiNode_ReloadBar : IUiNode<Godot.Control, UiNode_ReloadBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Slot
        /// </summary>
        public UiNode_Slot L_Slot
        {
            get
            {
                if (_L_Slot == null) _L_Slot = new UiNode_Slot(Instance.GetNodeOrNull<Godot.TextureRect>("Slot"));
                return _L_Slot;
            }
        }
        private UiNode_Slot _L_Slot;

        public UiNode_ReloadBar(Godot.Control node) : base(node) {  }
        public override UiNode_ReloadBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.LifeBar.Life
    /// </summary>
    public class UiNode_Life : IUiNode<Godot.TextureRect, UiNode_Life>
    {
        public UiNode_Life(Godot.TextureRect node) : base(node) {  }
        public override UiNode_Life Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.LifeBar
    /// </summary>
    public class UiNode_LifeBar : IUiNode<Godot.Control, UiNode_LifeBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.Life
        /// </summary>
        public UiNode_Life L_Life
        {
            get
            {
                if (_L_Life == null) _L_Life = new UiNode_Life(Instance.GetNodeOrNull<Godot.TextureRect>("Life"));
                return _L_Life;
            }
        }
        private UiNode_Life _L_Life;

        public UiNode_LifeBar(Godot.Control node) : base(node) {  }
        public override UiNode_LifeBar Clone() => new ((Godot.Control)Instance.Duplicate());
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
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropBg
    /// </summary>
    public class UiNode_ActivePropBg : IUiNode<Godot.NinePatchRect, UiNode_ActivePropBg>
    {
        public UiNode_ActivePropBg(Godot.NinePatchRect node) : base(node) {  }
        public override UiNode_ActivePropBg Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropSprite
    /// </summary>
    public class UiNode_ActivePropSprite : IUiNode<Godot.TextureRect, UiNode_ActivePropSprite>
    {
        public UiNode_ActivePropSprite(Godot.TextureRect node) : base(node) {  }
        public override UiNode_ActivePropSprite Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropGrey
    /// </summary>
    public class UiNode_ActivePropGrey : IUiNode<Godot.Sprite2D, UiNode_ActivePropGrey>
    {
        public UiNode_ActivePropGrey(Godot.Sprite2D node) : base(node) {  }
        public override UiNode_ActivePropGrey Clone() => new ((Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropCount
    /// </summary>
    public class UiNode_ActivePropCount : IUiNode<Godot.Label, UiNode_ActivePropCount>
    {
        public UiNode_ActivePropCount(Godot.Label node) : base(node) {  }
        public override UiNode_ActivePropCount Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropPanel
    /// </summary>
    public class UiNode_ActivePropPanel : IUiNode<Godot.NinePatchRect, UiNode_ActivePropPanel>
    {
        public UiNode_ActivePropPanel(Godot.NinePatchRect node) : base(node) {  }
        public override UiNode_ActivePropPanel Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropChargeProgress
    /// </summary>
    public class UiNode_ActivePropChargeProgress : IUiNode<Godot.NinePatchRect, UiNode_ActivePropChargeProgress>
    {
        public UiNode_ActivePropChargeProgress(Godot.NinePatchRect node) : base(node) {  }
        public override UiNode_ActivePropChargeProgress Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.ActivePropBar
    /// </summary>
    public class UiNode_ActivePropBar : IUiNode<Godot.Control, UiNode_ActivePropBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropBg
        /// </summary>
        public UiNode_ActivePropBg L_ActivePropBg
        {
            get
            {
                if (_L_ActivePropBg == null) _L_ActivePropBg = new UiNode_ActivePropBg(Instance.GetNodeOrNull<Godot.NinePatchRect>("ActivePropBg"));
                return _L_ActivePropBg;
            }
        }
        private UiNode_ActivePropBg _L_ActivePropBg;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.ActivePropSprite
        /// </summary>
        public UiNode_ActivePropSprite L_ActivePropSprite
        {
            get
            {
                if (_L_ActivePropSprite == null) _L_ActivePropSprite = new UiNode_ActivePropSprite(Instance.GetNodeOrNull<Godot.TextureRect>("ActivePropSprite"));
                return _L_ActivePropSprite;
            }
        }
        private UiNode_ActivePropSprite _L_ActivePropSprite;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Control.ActivePropGrey
        /// </summary>
        public UiNode_ActivePropGrey L_ActivePropGrey
        {
            get
            {
                if (_L_ActivePropGrey == null) _L_ActivePropGrey = new UiNode_ActivePropGrey(Instance.GetNodeOrNull<Godot.Sprite2D>("ActivePropGrey"));
                return _L_ActivePropGrey;
            }
        }
        private UiNode_ActivePropGrey _L_ActivePropGrey;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.ActivePropCount
        /// </summary>
        public UiNode_ActivePropCount L_ActivePropCount
        {
            get
            {
                if (_L_ActivePropCount == null) _L_ActivePropCount = new UiNode_ActivePropCount(Instance.GetNodeOrNull<Godot.Label>("ActivePropCount"));
                return _L_ActivePropCount;
            }
        }
        private UiNode_ActivePropCount _L_ActivePropCount;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropPanel
        /// </summary>
        public UiNode_ActivePropPanel L_ActivePropPanel
        {
            get
            {
                if (_L_ActivePropPanel == null) _L_ActivePropPanel = new UiNode_ActivePropPanel(Instance.GetNodeOrNull<Godot.NinePatchRect>("ActivePropPanel"));
                return _L_ActivePropPanel;
            }
        }
        private UiNode_ActivePropPanel _L_ActivePropPanel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropChargeProgress
        /// </summary>
        public UiNode_ActivePropChargeProgress L_ActivePropChargeProgress
        {
            get
            {
                if (_L_ActivePropChargeProgress == null) _L_ActivePropChargeProgress = new UiNode_ActivePropChargeProgress(Instance.GetNodeOrNull<Godot.NinePatchRect>("ActivePropChargeProgress"));
                return _L_ActivePropChargeProgress;
            }
        }
        private UiNode_ActivePropChargeProgress _L_ActivePropChargeProgress;

        public UiNode_ActivePropBar(Godot.Control node) : base(node) {  }
        public override UiNode_ActivePropBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.WeaponBar.WeaponPanel.WeaponSprite
    /// </summary>
    public class UiNode_WeaponSprite : IUiNode<Godot.TextureRect, UiNode_WeaponSprite>
    {
        public UiNode_WeaponSprite(Godot.TextureRect node) : base(node) {  }
        public override UiNode_WeaponSprite Clone() => new ((Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.WeaponBar.WeaponPanel
    /// </summary>
    public class UiNode_WeaponPanel : IUiNode<Godot.NinePatchRect, UiNode_WeaponPanel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.WeaponBar.WeaponSprite
        /// </summary>
        public UiNode_WeaponSprite L_WeaponSprite
        {
            get
            {
                if (_L_WeaponSprite == null) _L_WeaponSprite = new UiNode_WeaponSprite(Instance.GetNodeOrNull<Godot.TextureRect>("WeaponSprite"));
                return _L_WeaponSprite;
            }
        }
        private UiNode_WeaponSprite _L_WeaponSprite;

        public UiNode_WeaponPanel(Godot.NinePatchRect node) : base(node) {  }
        public override UiNode_WeaponPanel Clone() => new ((Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.Control.WeaponBar.AmmoCount
    /// </summary>
    public class UiNode_AmmoCount : IUiNode<Godot.Label, UiNode_AmmoCount>
    {
        public UiNode_AmmoCount(Godot.Label node) : base(node) {  }
        public override UiNode_AmmoCount Clone() => new ((Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.WeaponBar
    /// </summary>
    public class UiNode_WeaponBar : IUiNode<Godot.Control, UiNode_WeaponBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.WeaponPanel
        /// </summary>
        public UiNode_WeaponPanel L_WeaponPanel
        {
            get
            {
                if (_L_WeaponPanel == null) _L_WeaponPanel = new UiNode_WeaponPanel(Instance.GetNodeOrNull<Godot.NinePatchRect>("WeaponPanel"));
                return _L_WeaponPanel;
            }
        }
        private UiNode_WeaponPanel _L_WeaponPanel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.AmmoCount
        /// </summary>
        public UiNode_AmmoCount L_AmmoCount
        {
            get
            {
                if (_L_AmmoCount == null) _L_AmmoCount = new UiNode_AmmoCount(Instance.GetNodeOrNull<Godot.Label>("AmmoCount"));
                return _L_AmmoCount;
            }
        }
        private UiNode_AmmoCount _L_AmmoCount;

        public UiNode_WeaponBar(Godot.Control node) : base(node) {  }
        public override UiNode_WeaponBar Clone() => new ((Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control
    /// </summary>
    public class UiNode_Control : IUiNode<Godot.Control, UiNode_Control>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.LifeBar
        /// </summary>
        public UiNode_LifeBar L_LifeBar
        {
            get
            {
                if (_L_LifeBar == null) _L_LifeBar = new UiNode_LifeBar(Instance.GetNodeOrNull<Godot.Control>("LifeBar"));
                return _L_LifeBar;
            }
        }
        private UiNode_LifeBar _L_LifeBar;

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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.ActivePropBar
        /// </summary>
        public UiNode_ActivePropBar L_ActivePropBar
        {
            get
            {
                if (_L_ActivePropBar == null) _L_ActivePropBar = new UiNode_ActivePropBar(Instance.GetNodeOrNull<Godot.Control>("ActivePropBar"));
                return _L_ActivePropBar;
            }
        }
        private UiNode_ActivePropBar _L_ActivePropBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.WeaponBar
        /// </summary>
        public UiNode_WeaponBar L_WeaponBar
        {
            get
            {
                if (_L_WeaponBar == null) _L_WeaponBar = new UiNode_WeaponBar(Instance.GetNodeOrNull<Godot.Control>("WeaponBar"));
                return _L_WeaponBar;
            }
        }
        private UiNode_WeaponBar _L_WeaponBar;

        public UiNode_Control(Godot.Control node) : base(node) {  }
        public override UiNode_Control Clone() => new ((Godot.Control)Instance.Duplicate());
    }

}
