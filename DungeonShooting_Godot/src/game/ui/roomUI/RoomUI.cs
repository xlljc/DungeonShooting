namespace UI.RoomUI;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class RoomUI : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.InteractiveTipBar
    /// </summary>
    public InteractiveTipBar L_InteractiveTipBar
    {
        get
        {
            if (_L_InteractiveTipBar == null) _L_InteractiveTipBar = new InteractiveTipBar((RoomUIPanel)this, GetNode<Godot.Control>("InteractiveTipBar"));
            return _L_InteractiveTipBar;
        }
    }
    private InteractiveTipBar _L_InteractiveTipBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.ReloadBar
    /// </summary>
    public ReloadBar L_ReloadBar
    {
        get
        {
            if (_L_ReloadBar == null) _L_ReloadBar = new ReloadBar((RoomUIPanel)this, GetNode<Godot.Control>("ReloadBar"));
            return _L_ReloadBar;
        }
    }
    private ReloadBar _L_ReloadBar;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control
    /// </summary>
    public Control L_Control
    {
        get
        {
            if (_L_Control == null) _L_Control = new Control((RoomUIPanel)this, GetNode<Godot.Control>("Control"));
            return _L_Control;
        }
    }
    private Control _L_Control;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.RoomMap.RoomMapPanel"/>, 节点路径: RoomUI.RoomMap
    /// </summary>
    public RoomMap L_RoomMap
    {
        get
        {
            if (_L_RoomMap == null) _L_RoomMap = new RoomMap((RoomUIPanel)this, GetNode<UI.RoomMap.RoomMapPanel>("RoomMap"));
            return _L_RoomMap;
        }
    }
    private RoomMap _L_RoomMap;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: RoomUI.Mask
    /// </summary>
    public Mask L_Mask
    {
        get
        {
            if (_L_Mask == null) _L_Mask = new Mask((RoomUIPanel)this, GetNode<Godot.ColorRect>("Mask"));
            return _L_Mask;
        }
    }
    private Mask _L_Mask;


    public RoomUI() : base(nameof(RoomUI))
    {
    }

    public sealed override void OnInitNestedUi()
    {

        var inst1 = this;
        RecordNestedUi(inst1.L_RoomMap.Instance, null, UiManager.RecordType.Open);
        inst1.L_RoomMap.Instance.OnCreateUi();
        inst1.L_RoomMap.Instance.OnInitNestedUi();

    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.InteractiveTipBar.Icon
    /// </summary>
    public class Icon : UiNode<RoomUIPanel, Godot.TextureRect, Icon>
    {
        public Icon(RoomUIPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override Icon Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.InteractiveTipBar.InteractiveIcon
    /// </summary>
    public class InteractiveIcon : UiNode<RoomUIPanel, Godot.TextureRect, InteractiveIcon>
    {
        public InteractiveIcon(RoomUIPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override InteractiveIcon Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Line2D"/>, 路径: RoomUI.InteractiveTipBar.Line2D
    /// </summary>
    public class Line2D : UiNode<RoomUIPanel, Godot.Line2D, Line2D>
    {
        public Line2D(RoomUIPanel uiPanel, Godot.Line2D node) : base(uiPanel, node) {  }
        public override Line2D Clone() => new (UiPanel, (Godot.Line2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.InteractiveTipBar.NameLabel
    /// </summary>
    public class NameLabel : UiNode<RoomUIPanel, Godot.Label, NameLabel>
    {
        public NameLabel(RoomUIPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override NameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.InteractiveTipBar
    /// </summary>
    public class InteractiveTipBar : UiNode<RoomUIPanel, Godot.Control, InteractiveTipBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Icon
        /// </summary>
        public Icon L_Icon
        {
            get
            {
                if (_L_Icon == null) _L_Icon = new Icon(UiPanel, Instance.GetNode<Godot.TextureRect>("Icon"));
                return _L_Icon;
            }
        }
        private Icon _L_Icon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.InteractiveIcon
        /// </summary>
        public InteractiveIcon L_InteractiveIcon
        {
            get
            {
                if (_L_InteractiveIcon == null) _L_InteractiveIcon = new InteractiveIcon(UiPanel, Instance.GetNode<Godot.TextureRect>("InteractiveIcon"));
                return _L_InteractiveIcon;
            }
        }
        private InteractiveIcon _L_InteractiveIcon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Line2D"/>, 节点路径: RoomUI.Line2D
        /// </summary>
        public Line2D L_Line2D
        {
            get
            {
                if (_L_Line2D == null) _L_Line2D = new Line2D(UiPanel, Instance.GetNode<Godot.Line2D>("Line2D"));
                return _L_Line2D;
            }
        }
        private Line2D _L_Line2D;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.NameLabel
        /// </summary>
        public NameLabel L_NameLabel
        {
            get
            {
                if (_L_NameLabel == null) _L_NameLabel = new NameLabel(UiPanel, Instance.GetNode<Godot.Label>("NameLabel"));
                return _L_NameLabel;
            }
        }
        private NameLabel _L_NameLabel;

        public InteractiveTipBar(RoomUIPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override InteractiveTipBar Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.ReloadBar.Slot.Block
    /// </summary>
    public class Block : UiNode<RoomUIPanel, Godot.Sprite2D, Block>
    {
        public Block(RoomUIPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override Block Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.ReloadBar.Slot
    /// </summary>
    public class Slot : UiNode<RoomUIPanel, Godot.TextureRect, Slot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ReloadBar.Block
        /// </summary>
        public Block L_Block
        {
            get
            {
                if (_L_Block == null) _L_Block = new Block(UiPanel, Instance.GetNode<Godot.Sprite2D>("Block"));
                return _L_Block;
            }
        }
        private Block _L_Block;

        public Slot(RoomUIPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override Slot Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.ReloadBar
    /// </summary>
    public class ReloadBar : UiNode<RoomUIPanel, Godot.Control, ReloadBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Slot
        /// </summary>
        public Slot L_Slot
        {
            get
            {
                if (_L_Slot == null) _L_Slot = new Slot(UiPanel, Instance.GetNode<Godot.TextureRect>("Slot"));
                return _L_Slot;
            }
        }
        private Slot _L_Slot;

        public ReloadBar(RoomUIPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override ReloadBar Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.LifeBar.Life
    /// </summary>
    public class Life : UiNode<RoomUIPanel, Godot.TextureRect, Life>
    {
        public Life(RoomUIPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override Life Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.LifeBar
    /// </summary>
    public class LifeBar : UiNode<RoomUIPanel, Godot.Control, LifeBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.Life
        /// </summary>
        public Life L_Life
        {
            get
            {
                if (_L_Life == null) _L_Life = new Life(UiPanel, Instance.GetNode<Godot.TextureRect>("Life"));
                return _L_Life;
            }
        }
        private Life _L_Life;

        public LifeBar(RoomUIPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override LifeBar Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropBg
    /// </summary>
    public class ActivePropBg : UiNode<RoomUIPanel, Godot.NinePatchRect, ActivePropBg>
    {
        public ActivePropBg(RoomUIPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override ActivePropBg Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropSprite
    /// </summary>
    public class ActivePropSprite : UiNode<RoomUIPanel, Godot.TextureRect, ActivePropSprite>
    {
        public ActivePropSprite(RoomUIPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override ActivePropSprite Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Control.ActivePropBar.CooldownProgress
    /// </summary>
    public class CooldownProgress : UiNode<RoomUIPanel, Godot.Sprite2D, CooldownProgress>
    {
        public CooldownProgress(RoomUIPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override CooldownProgress Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropCount
    /// </summary>
    public class ActivePropCount : UiNode<RoomUIPanel, Godot.Label, ActivePropCount>
    {
        public ActivePropCount(RoomUIPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override ActivePropCount Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.ActivePropBar.ActivePropPanel
    /// </summary>
    public class ActivePropPanel : UiNode<RoomUIPanel, Godot.NinePatchRect, ActivePropPanel>
    {
        public ActivePropPanel(RoomUIPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override ActivePropPanel Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.ActivePropBar.ChargeProgressBar
    /// </summary>
    public class ChargeProgressBar : UiNode<RoomUIPanel, Godot.NinePatchRect, ChargeProgressBar>
    {
        public ChargeProgressBar(RoomUIPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override ChargeProgressBar Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: RoomUI.Control.ActivePropBar.ChargeProgress
    /// </summary>
    public class ChargeProgress : UiNode<RoomUIPanel, Godot.Sprite2D, ChargeProgress>
    {
        public ChargeProgress(RoomUIPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override ChargeProgress Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.ActivePropBar
    /// </summary>
    public class ActivePropBar : UiNode<RoomUIPanel, Godot.Control, ActivePropBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropBg
        /// </summary>
        public ActivePropBg L_ActivePropBg
        {
            get
            {
                if (_L_ActivePropBg == null) _L_ActivePropBg = new ActivePropBg(UiPanel, Instance.GetNode<Godot.NinePatchRect>("ActivePropBg"));
                return _L_ActivePropBg;
            }
        }
        private ActivePropBg _L_ActivePropBg;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.ActivePropSprite
        /// </summary>
        public ActivePropSprite L_ActivePropSprite
        {
            get
            {
                if (_L_ActivePropSprite == null) _L_ActivePropSprite = new ActivePropSprite(UiPanel, Instance.GetNode<Godot.TextureRect>("ActivePropSprite"));
                return _L_ActivePropSprite;
            }
        }
        private ActivePropSprite _L_ActivePropSprite;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Control.CooldownProgress
        /// </summary>
        public CooldownProgress L_CooldownProgress
        {
            get
            {
                if (_L_CooldownProgress == null) _L_CooldownProgress = new CooldownProgress(UiPanel, Instance.GetNode<Godot.Sprite2D>("CooldownProgress"));
                return _L_CooldownProgress;
            }
        }
        private CooldownProgress _L_CooldownProgress;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.ActivePropCount
        /// </summary>
        public ActivePropCount L_ActivePropCount
        {
            get
            {
                if (_L_ActivePropCount == null) _L_ActivePropCount = new ActivePropCount(UiPanel, Instance.GetNode<Godot.Label>("ActivePropCount"));
                return _L_ActivePropCount;
            }
        }
        private ActivePropCount _L_ActivePropCount;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropPanel
        /// </summary>
        public ActivePropPanel L_ActivePropPanel
        {
            get
            {
                if (_L_ActivePropPanel == null) _L_ActivePropPanel = new ActivePropPanel(UiPanel, Instance.GetNode<Godot.NinePatchRect>("ActivePropPanel"));
                return _L_ActivePropPanel;
            }
        }
        private ActivePropPanel _L_ActivePropPanel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ChargeProgressBar
        /// </summary>
        public ChargeProgressBar L_ChargeProgressBar
        {
            get
            {
                if (_L_ChargeProgressBar == null) _L_ChargeProgressBar = new ChargeProgressBar(UiPanel, Instance.GetNode<Godot.NinePatchRect>("ChargeProgressBar"));
                return _L_ChargeProgressBar;
            }
        }
        private ChargeProgressBar _L_ChargeProgressBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Control.ChargeProgress
        /// </summary>
        public ChargeProgress L_ChargeProgress
        {
            get
            {
                if (_L_ChargeProgress == null) _L_ChargeProgress = new ChargeProgress(UiPanel, Instance.GetNode<Godot.Sprite2D>("ChargeProgress"));
                return _L_ChargeProgress;
            }
        }
        private ChargeProgress _L_ChargeProgress;

        public ActivePropBar(RoomUIPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override ActivePropBar Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: RoomUI.Control.WeaponBar.WeaponPanel.WeaponSprite
    /// </summary>
    public class WeaponSprite : UiNode<RoomUIPanel, Godot.TextureRect, WeaponSprite>
    {
        public WeaponSprite(RoomUIPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override WeaponSprite Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: RoomUI.Control.WeaponBar.WeaponPanel
    /// </summary>
    public class WeaponPanel : UiNode<RoomUIPanel, Godot.NinePatchRect, WeaponPanel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.WeaponBar.WeaponSprite
        /// </summary>
        public WeaponSprite L_WeaponSprite
        {
            get
            {
                if (_L_WeaponSprite == null) _L_WeaponSprite = new WeaponSprite(UiPanel, Instance.GetNode<Godot.TextureRect>("WeaponSprite"));
                return _L_WeaponSprite;
            }
        }
        private WeaponSprite _L_WeaponSprite;

        public WeaponPanel(RoomUIPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override WeaponPanel Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: RoomUI.Control.WeaponBar.AmmoCount
    /// </summary>
    public class AmmoCount : UiNode<RoomUIPanel, Godot.Label, AmmoCount>
    {
        public AmmoCount(RoomUIPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override AmmoCount Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control.WeaponBar
    /// </summary>
    public class WeaponBar : UiNode<RoomUIPanel, Godot.Control, WeaponBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.WeaponPanel
        /// </summary>
        public WeaponPanel L_WeaponPanel
        {
            get
            {
                if (_L_WeaponPanel == null) _L_WeaponPanel = new WeaponPanel(UiPanel, Instance.GetNode<Godot.NinePatchRect>("WeaponPanel"));
                return _L_WeaponPanel;
            }
        }
        private WeaponPanel _L_WeaponPanel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.AmmoCount
        /// </summary>
        public AmmoCount L_AmmoCount
        {
            get
            {
                if (_L_AmmoCount == null) _L_AmmoCount = new AmmoCount(UiPanel, Instance.GetNode<Godot.Label>("AmmoCount"));
                return _L_AmmoCount;
            }
        }
        private AmmoCount _L_AmmoCount;

        public WeaponBar(RoomUIPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override WeaponBar Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: RoomUI.Control
    /// </summary>
    public class Control : UiNode<RoomUIPanel, Godot.Control, Control>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.LifeBar
        /// </summary>
        public LifeBar L_LifeBar
        {
            get
            {
                if (_L_LifeBar == null) _L_LifeBar = new LifeBar(UiPanel, Instance.GetNode<Godot.Control>("LifeBar"));
                return _L_LifeBar;
            }
        }
        private LifeBar _L_LifeBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.ActivePropBar
        /// </summary>
        public ActivePropBar L_ActivePropBar
        {
            get
            {
                if (_L_ActivePropBar == null) _L_ActivePropBar = new ActivePropBar(UiPanel, Instance.GetNode<Godot.Control>("ActivePropBar"));
                return _L_ActivePropBar;
            }
        }
        private ActivePropBar _L_ActivePropBar;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.WeaponBar
        /// </summary>
        public WeaponBar L_WeaponBar
        {
            get
            {
                if (_L_WeaponBar == null) _L_WeaponBar = new WeaponBar(UiPanel, Instance.GetNode<Godot.Control>("WeaponBar"));
                return _L_WeaponBar;
            }
        }
        private WeaponBar _L_WeaponBar;

        public Control(RoomUIPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override Control Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.RoomMap.RoomMapPanel"/>, 路径: RoomUI.RoomMap
    /// </summary>
    public class RoomMap : UiNode<RoomUIPanel, UI.RoomMap.RoomMapPanel, RoomMap>
    {
        public RoomMap(RoomUIPanel uiPanel, UI.RoomMap.RoomMapPanel node) : base(uiPanel, node) {  }
        public override RoomMap Clone()
        {
            var uiNode = new RoomMap(UiPanel, (UI.RoomMap.RoomMapPanel)Instance.Duplicate());
            UiPanel.RecordNestedUi(uiNode.Instance, this, UiManager.RecordType.Open);
            uiNode.Instance.OnCreateUi();
            uiNode.Instance.OnInitNestedUi();
            return uiNode;
        }
    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: RoomUI.Mask
    /// </summary>
    public class Mask : UiNode<RoomUIPanel, Godot.ColorRect, Mask>
    {
        public Mask(RoomUIPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override Mask Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.InteractiveTipBar.Icon
    /// </summary>
    public Icon S_Icon => L_InteractiveTipBar.L_Icon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.InteractiveTipBar.InteractiveIcon
    /// </summary>
    public InteractiveIcon S_InteractiveIcon => L_InteractiveTipBar.L_InteractiveIcon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Line2D"/>, 节点路径: RoomUI.InteractiveTipBar.Line2D
    /// </summary>
    public Line2D S_Line2D => L_InteractiveTipBar.L_Line2D;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.InteractiveTipBar.NameLabel
    /// </summary>
    public NameLabel S_NameLabel => L_InteractiveTipBar.L_NameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.InteractiveTipBar
    /// </summary>
    public InteractiveTipBar S_InteractiveTipBar => L_InteractiveTipBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.ReloadBar.Slot.Block
    /// </summary>
    public Block S_Block => L_ReloadBar.L_Slot.L_Block;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.ReloadBar.Slot
    /// </summary>
    public Slot S_Slot => L_ReloadBar.L_Slot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.ReloadBar
    /// </summary>
    public ReloadBar S_ReloadBar => L_ReloadBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.LifeBar.Life
    /// </summary>
    public Life S_Life => L_Control.L_LifeBar.L_Life;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.LifeBar
    /// </summary>
    public LifeBar S_LifeBar => L_Control.L_LifeBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropBar.ActivePropBg
    /// </summary>
    public ActivePropBg S_ActivePropBg => L_Control.L_ActivePropBar.L_ActivePropBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.ActivePropBar.ActivePropSprite
    /// </summary>
    public ActivePropSprite S_ActivePropSprite => L_Control.L_ActivePropBar.L_ActivePropSprite;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Control.ActivePropBar.CooldownProgress
    /// </summary>
    public CooldownProgress S_CooldownProgress => L_Control.L_ActivePropBar.L_CooldownProgress;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.ActivePropBar.ActivePropCount
    /// </summary>
    public ActivePropCount S_ActivePropCount => L_Control.L_ActivePropBar.L_ActivePropCount;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropBar.ActivePropPanel
    /// </summary>
    public ActivePropPanel S_ActivePropPanel => L_Control.L_ActivePropBar.L_ActivePropPanel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.ActivePropBar.ChargeProgressBar
    /// </summary>
    public ChargeProgressBar S_ChargeProgressBar => L_Control.L_ActivePropBar.L_ChargeProgressBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: RoomUI.Control.ActivePropBar.ChargeProgress
    /// </summary>
    public ChargeProgress S_ChargeProgress => L_Control.L_ActivePropBar.L_ChargeProgress;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.ActivePropBar
    /// </summary>
    public ActivePropBar S_ActivePropBar => L_Control.L_ActivePropBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: RoomUI.Control.WeaponBar.WeaponPanel.WeaponSprite
    /// </summary>
    public WeaponSprite S_WeaponSprite => L_Control.L_WeaponBar.L_WeaponPanel.L_WeaponSprite;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: RoomUI.Control.WeaponBar.WeaponPanel
    /// </summary>
    public WeaponPanel S_WeaponPanel => L_Control.L_WeaponBar.L_WeaponPanel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: RoomUI.Control.WeaponBar.AmmoCount
    /// </summary>
    public AmmoCount S_AmmoCount => L_Control.L_WeaponBar.L_AmmoCount;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control.WeaponBar
    /// </summary>
    public WeaponBar S_WeaponBar => L_Control.L_WeaponBar;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: RoomUI.Control
    /// </summary>
    public Control S_Control => L_Control;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.RoomMap.RoomMapPanel"/>, 节点路径: RoomUI.RoomMap
    /// </summary>
    public RoomMap S_RoomMap => L_RoomMap;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: RoomUI.Mask
    /// </summary>
    public Mask S_Mask => L_Mask;

}
