namespace UI.MapEditorCreateMark;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class MapEditorCreateMark : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer
    /// </summary>
    public MarginContainer L_MarginContainer
    {
        get
        {
            if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer((MapEditorCreateMarkPanel)this, GetNodeOrNull<Godot.MarginContainer>("MarginContainer"));
            return _L_MarginContainer;
        }
    }
    private MarginContainer _L_MarginContainer;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreateMark.ExpandPanel
    /// </summary>
    public ExpandPanel L_ExpandPanel
    {
        get
        {
            if (_L_ExpandPanel == null) _L_ExpandPanel = new ExpandPanel((MapEditorCreateMarkPanel)this, GetNodeOrNull<Godot.MarginContainer>("ExpandPanel"));
            return _L_ExpandPanel;
        }
    }
    private ExpandPanel _L_ExpandPanel;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorCreateMark.NumberAttribute"/>, 节点路径: MapEditorCreateMark.NumberBar
    /// </summary>
    public NumberBar L_NumberBar
    {
        get
        {
            if (_L_NumberBar == null) _L_NumberBar = new NumberBar((MapEditorCreateMarkPanel)this, GetNodeOrNull<UI.MapEditorCreateMark.NumberAttribute>("NumberBar"));
            return _L_NumberBar;
        }
    }
    private NumberBar _L_NumberBar;


    public MapEditorCreateMark() : base(nameof(MapEditorCreateMark))
    {
    }

    public sealed override void OnInitNestedUi()
    {
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer2.WaveNameLabel
    /// </summary>
    public class WaveNameLabel : UiNode<MapEditorCreateMarkPanel, Godot.Label, WaveNameLabel>
    {
        public WaveNameLabel(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override WaveNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.OptionButton"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer2.WaveOption
    /// </summary>
    public class WaveOption : UiNode<MapEditorCreateMarkPanel, Godot.OptionButton, WaveOption>
    {
        public WaveOption(MapEditorCreateMarkPanel uiPanel, Godot.OptionButton node) : base(uiPanel, node) {  }
        public override WaveOption Clone() => new (UiPanel, (Godot.OptionButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer2
    /// </summary>
    public class HBoxContainer2 : UiNode<MapEditorCreateMarkPanel, Godot.HBoxContainer, HBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.WaveNameLabel
        /// </summary>
        public WaveNameLabel L_WaveNameLabel
        {
            get
            {
                if (_L_WaveNameLabel == null) _L_WaveNameLabel = new WaveNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("WaveNameLabel"));
                return _L_WaveNameLabel;
            }
        }
        private WaveNameLabel _L_WaveNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.WaveOption
        /// </summary>
        public WaveOption L_WaveOption
        {
            get
            {
                if (_L_WaveOption == null) _L_WaveOption = new WaveOption(UiPanel, Instance.GetNodeOrNull<Godot.OptionButton>("WaveOption"));
                return _L_WaveOption;
            }
        }
        private WaveOption _L_WaveOption;

        public HBoxContainer2(MapEditorCreateMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer2 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer.DelayNameLabel
    /// </summary>
    public class DelayNameLabel : UiNode<MapEditorCreateMarkPanel, Godot.Label, DelayNameLabel>
    {
        public DelayNameLabel(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override DelayNameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SpinBox"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer.DelayInput
    /// </summary>
    public class DelayInput : UiNode<MapEditorCreateMarkPanel, Godot.SpinBox, DelayInput>
    {
        public DelayInput(MapEditorCreateMarkPanel uiPanel, Godot.SpinBox node) : base(uiPanel, node) {  }
        public override DelayInput Clone() => new (UiPanel, (Godot.SpinBox)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<MapEditorCreateMarkPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.DelayNameLabel
        /// </summary>
        public DelayNameLabel L_DelayNameLabel
        {
            get
            {
                if (_L_DelayNameLabel == null) _L_DelayNameLabel = new DelayNameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("DelayNameLabel"));
                return _L_DelayNameLabel;
            }
        }
        private DelayNameLabel _L_DelayNameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.DelayInput
        /// </summary>
        public DelayInput L_DelayInput
        {
            get
            {
                if (_L_DelayInput == null) _L_DelayInput = new DelayInput(UiPanel, Instance.GetNodeOrNull<Godot.SpinBox>("DelayInput"));
                return _L_DelayInput;
            }
        }
        private DelayInput _L_DelayInput;

        public HBoxContainer(MapEditorCreateMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public class HBoxContainer3 : UiNode<MapEditorCreateMarkPanel, Godot.HBoxContainer, HBoxContainer3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer2
        /// </summary>
        public HBoxContainer2 L_HBoxContainer2
        {
            get
            {
                if (_L_HBoxContainer2 == null) _L_HBoxContainer2 = new HBoxContainer2(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer2"));
                return _L_HBoxContainer2;
            }
        }
        private HBoxContainer2 _L_HBoxContainer2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer
        /// </summary>
        public HBoxContainer L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer _L_HBoxContainer;

        public HBoxContainer3(MapEditorCreateMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer3 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.AddMark
    /// </summary>
    public class AddMark : UiNode<MapEditorCreateMarkPanel, Godot.Button, AddMark>
    {
        public AddMark(MapEditorCreateMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override AddMark Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.Control
    /// </summary>
    public class Control : UiNode<MapEditorCreateMarkPanel, Godot.Control, Control>
    {
        public Control(MapEditorCreateMarkPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override Control Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.IconTitle
    /// </summary>
    public class IconTitle : UiNode<MapEditorCreateMarkPanel, Godot.Label, IconTitle>
    {
        public IconTitle(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override IconTitle Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.IdTitle
    /// </summary>
    public class IdTitle : UiNode<MapEditorCreateMarkPanel, Godot.Label, IdTitle>
    {
        public IdTitle(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override IdTitle Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.NameTitle
    /// </summary>
    public class NameTitle : UiNode<MapEditorCreateMarkPanel, Godot.Label, NameTitle>
    {
        public NameTitle(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override NameTitle Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.TypeTitle
    /// </summary>
    public class TypeTitle : UiNode<MapEditorCreateMarkPanel, Godot.Label, TypeTitle>
    {
        public TypeTitle(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override TypeTitle Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.WeightTitle
    /// </summary>
    public class WeightTitle : UiNode<MapEditorCreateMarkPanel, Godot.Label, WeightTitle>
    {
        public WeightTitle(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override WeightTitle Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.ExtraTitle
    /// </summary>
    public class ExtraTitle : UiNode<MapEditorCreateMarkPanel, Godot.Label, ExtraTitle>
    {
        public ExtraTitle(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override ExtraTitle Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer_1 : UiNode<MapEditorCreateMarkPanel, Godot.HBoxContainer, HBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.Control
        /// </summary>
        public Control L_Control
        {
            get
            {
                if (_L_Control == null) _L_Control = new Control(UiPanel, Instance.GetNodeOrNull<Godot.Control>("Control"));
                return _L_Control;
            }
        }
        private Control _L_Control;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.IconTitle
        /// </summary>
        public IconTitle L_IconTitle
        {
            get
            {
                if (_L_IconTitle == null) _L_IconTitle = new IconTitle(UiPanel, Instance.GetNodeOrNull<Godot.Label>("IconTitle"));
                return _L_IconTitle;
            }
        }
        private IconTitle _L_IconTitle;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.IdTitle
        /// </summary>
        public IdTitle L_IdTitle
        {
            get
            {
                if (_L_IdTitle == null) _L_IdTitle = new IdTitle(UiPanel, Instance.GetNodeOrNull<Godot.Label>("IdTitle"));
                return _L_IdTitle;
            }
        }
        private IdTitle _L_IdTitle;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.NameTitle
        /// </summary>
        public NameTitle L_NameTitle
        {
            get
            {
                if (_L_NameTitle == null) _L_NameTitle = new NameTitle(UiPanel, Instance.GetNodeOrNull<Godot.Label>("NameTitle"));
                return _L_NameTitle;
            }
        }
        private NameTitle _L_NameTitle;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.TypeTitle
        /// </summary>
        public TypeTitle L_TypeTitle
        {
            get
            {
                if (_L_TypeTitle == null) _L_TypeTitle = new TypeTitle(UiPanel, Instance.GetNodeOrNull<Godot.Label>("TypeTitle"));
                return _L_TypeTitle;
            }
        }
        private TypeTitle _L_TypeTitle;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.WeightTitle
        /// </summary>
        public WeightTitle L_WeightTitle
        {
            get
            {
                if (_L_WeightTitle == null) _L_WeightTitle = new WeightTitle(UiPanel, Instance.GetNodeOrNull<Godot.Label>("WeightTitle"));
                return _L_WeightTitle;
            }
        }
        private WeightTitle _L_WeightTitle;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ExtraTitle
        /// </summary>
        public ExtraTitle L_ExtraTitle
        {
            get
            {
                if (_L_ExtraTitle == null) _L_ExtraTitle = new ExtraTitle(UiPanel, Instance.GetNodeOrNull<Godot.Label>("ExtraTitle"));
                return _L_ExtraTitle;
            }
        }
        private ExtraTitle _L_ExtraTitle;

        public HBoxContainer_1(MapEditorCreateMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer_1 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.ExpandButton
    /// </summary>
    public class ExpandButton : UiNode<MapEditorCreateMarkPanel, Godot.Button, ExpandButton>
    {
        public ExpandButton(MapEditorCreateMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ExpandButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.Icon
    /// </summary>
    public class Icon : UiNode<MapEditorCreateMarkPanel, Godot.TextureRect, Icon>
    {
        public Icon(MapEditorCreateMarkPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override Icon Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.IdLabel
    /// </summary>
    public class IdLabel : UiNode<MapEditorCreateMarkPanel, Godot.Label, IdLabel>
    {
        public IdLabel(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override IdLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.NameLabel
    /// </summary>
    public class NameLabel : UiNode<MapEditorCreateMarkPanel, Godot.Label, NameLabel>
    {
        public NameLabel(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override NameLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.TypeLabel
    /// </summary>
    public class TypeLabel : UiNode<MapEditorCreateMarkPanel, Godot.Label, TypeLabel>
    {
        public TypeLabel(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override TypeLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SpinBox"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.LineEdit
    /// </summary>
    public class LineEdit : UiNode<MapEditorCreateMarkPanel, Godot.SpinBox, LineEdit>
    {
        public LineEdit(MapEditorCreateMarkPanel uiPanel, Godot.SpinBox node) : base(uiPanel, node) {  }
        public override LineEdit Clone() => new (UiPanel, (Godot.SpinBox)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.CenterContainer.DeleteButton
    /// </summary>
    public class DeleteButton : UiNode<MapEditorCreateMarkPanel, Godot.Button, DeleteButton>
    {
        public DeleteButton(MapEditorCreateMarkPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override DeleteButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.CenterContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.CenterContainer
    /// </summary>
    public class CenterContainer : UiNode<MapEditorCreateMarkPanel, Godot.CenterContainer, CenterContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.DeleteButton
        /// </summary>
        public DeleteButton L_DeleteButton
        {
            get
            {
                if (_L_DeleteButton == null) _L_DeleteButton = new DeleteButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("DeleteButton"));
                return _L_DeleteButton;
            }
        }
        private DeleteButton _L_DeleteButton;

        public CenterContainer(MapEditorCreateMarkPanel uiPanel, Godot.CenterContainer node) : base(uiPanel, node) {  }
        public override CenterContainer Clone() => new (UiPanel, (Godot.CenterContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer
    /// </summary>
    public class HBoxContainer_2 : UiNode<MapEditorCreateMarkPanel, Godot.HBoxContainer, HBoxContainer_2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.ExpandButton
        /// </summary>
        public ExpandButton L_ExpandButton
        {
            get
            {
                if (_L_ExpandButton == null) _L_ExpandButton = new ExpandButton(UiPanel, Instance.GetNodeOrNull<Godot.Button>("ExpandButton"));
                return _L_ExpandButton;
            }
        }
        private ExpandButton _L_ExpandButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.Icon
        /// </summary>
        public Icon L_Icon
        {
            get
            {
                if (_L_Icon == null) _L_Icon = new Icon(UiPanel, Instance.GetNodeOrNull<Godot.TextureRect>("Icon"));
                return _L_Icon;
            }
        }
        private Icon _L_Icon;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.IdLabel
        /// </summary>
        public IdLabel L_IdLabel
        {
            get
            {
                if (_L_IdLabel == null) _L_IdLabel = new IdLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("IdLabel"));
                return _L_IdLabel;
            }
        }
        private IdLabel _L_IdLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.NameLabel
        /// </summary>
        public NameLabel L_NameLabel
        {
            get
            {
                if (_L_NameLabel == null) _L_NameLabel = new NameLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("NameLabel"));
                return _L_NameLabel;
            }
        }
        private NameLabel _L_NameLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.TypeLabel
        /// </summary>
        public TypeLabel L_TypeLabel
        {
            get
            {
                if (_L_TypeLabel == null) _L_TypeLabel = new TypeLabel(UiPanel, Instance.GetNodeOrNull<Godot.Label>("TypeLabel"));
                return _L_TypeLabel;
            }
        }
        private TypeLabel _L_TypeLabel;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.LineEdit
        /// </summary>
        public LineEdit L_LineEdit
        {
            get
            {
                if (_L_LineEdit == null) _L_LineEdit = new LineEdit(UiPanel, Instance.GetNodeOrNull<Godot.SpinBox>("LineEdit"));
                return _L_LineEdit;
            }
        }
        private LineEdit _L_LineEdit;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CenterContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.CenterContainer
        /// </summary>
        public CenterContainer L_CenterContainer
        {
            get
            {
                if (_L_CenterContainer == null) _L_CenterContainer = new CenterContainer(UiPanel, Instance.GetNodeOrNull<Godot.CenterContainer>("CenterContainer"));
                return _L_CenterContainer;
            }
        }
        private CenterContainer _L_CenterContainer;

        public HBoxContainer_2(MapEditorCreateMarkPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer_2 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject
    /// </summary>
    public class MarkObject : UiNode<MapEditorCreateMarkPanel, Godot.VBoxContainer, MarkObject>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.HBoxContainer
        /// </summary>
        public HBoxContainer_2 L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer_2(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer_2 _L_HBoxContainer;

        public MarkObject(MapEditorCreateMarkPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override MarkObject Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<MapEditorCreateMarkPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.MarkObject
        /// </summary>
        public MarkObject L_MarkObject
        {
            get
            {
                if (_L_MarkObject == null) _L_MarkObject = new MarkObject(UiPanel, Instance.GetNodeOrNull<Godot.VBoxContainer>("MarkObject"));
                return _L_MarkObject;
            }
        }
        private MarkObject _L_MarkObject;

        public ScrollContainer(MapEditorCreateMarkPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer
    /// </summary>
    public class VBoxContainer_1 : UiNode<MapEditorCreateMarkPanel, Godot.VBoxContainer, VBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.HBoxContainer
        /// </summary>
        public HBoxContainer_1 L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer_1(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer_1 _L_HBoxContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.ScrollContainer
        /// </summary>
        public ScrollContainer L_ScrollContainer
        {
            get
            {
                if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer(UiPanel, Instance.GetNodeOrNull<Godot.ScrollContainer>("ScrollContainer"));
                return _L_ScrollContainer;
            }
        }
        private ScrollContainer _L_ScrollContainer;

        public VBoxContainer_1(MapEditorCreateMarkPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer_1 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel
    /// </summary>
    public class Panel : UiNode<MapEditorCreateMarkPanel, Godot.Panel, Panel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.VBoxContainer
        /// </summary>
        public VBoxContainer_1 L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer_1(UiPanel, Instance.GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer_1 _L_VBoxContainer;

        public Panel(MapEditorCreateMarkPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Panel Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: MapEditorCreateMark.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<MapEditorCreateMarkPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.HBoxContainer3
        /// </summary>
        public HBoxContainer3 L_HBoxContainer3
        {
            get
            {
                if (_L_HBoxContainer3 == null) _L_HBoxContainer3 = new HBoxContainer3(UiPanel, Instance.GetNodeOrNull<Godot.HBoxContainer>("HBoxContainer3"));
                return _L_HBoxContainer3;
            }
        }
        private HBoxContainer3 _L_HBoxContainer3;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorCreateMark.MarginContainer.AddMark
        /// </summary>
        public AddMark L_AddMark
        {
            get
            {
                if (_L_AddMark == null) _L_AddMark = new AddMark(UiPanel, Instance.GetNodeOrNull<Godot.Button>("AddMark"));
                return _L_AddMark;
            }
        }
        private AddMark _L_AddMark;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorCreateMark.MarginContainer.Panel
        /// </summary>
        public Panel L_Panel
        {
            get
            {
                if (_L_Panel == null) _L_Panel = new Panel(UiPanel, Instance.GetNodeOrNull<Godot.Panel>("Panel"));
                return _L_Panel;
            }
        }
        private Panel _L_Panel;

        public VBoxContainer(MapEditorCreateMarkPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorCreateMark.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<MapEditorCreateMarkPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateMark.VBoxContainer
        /// </summary>
        public VBoxContainer L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer(UiPanel, Instance.GetNodeOrNull<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer _L_VBoxContainer;

        public MarginContainer(MapEditorCreateMarkPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.GridContainer"/>, 路径: MapEditorCreateMark.ExpandPanel.ExpandGrid
    /// </summary>
    public class ExpandGrid : UiNode<MapEditorCreateMarkPanel, Godot.GridContainer, ExpandGrid>
    {
        public ExpandGrid(MapEditorCreateMarkPanel uiPanel, Godot.GridContainer node) : base(uiPanel, node) {  }
        public override ExpandGrid Clone() => new (UiPanel, (Godot.GridContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: MapEditorCreateMark.ExpandPanel
    /// </summary>
    public class ExpandPanel : UiNode<MapEditorCreateMarkPanel, Godot.MarginContainer, ExpandPanel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.GridContainer"/>, 节点路径: MapEditorCreateMark.ExpandGrid
        /// </summary>
        public ExpandGrid L_ExpandGrid
        {
            get
            {
                if (_L_ExpandGrid == null) _L_ExpandGrid = new ExpandGrid(UiPanel, Instance.GetNodeOrNull<Godot.GridContainer>("ExpandGrid"));
                return _L_ExpandGrid;
            }
        }
        private ExpandGrid _L_ExpandGrid;

        public ExpandPanel(MapEditorCreateMarkPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override ExpandPanel Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: MapEditorCreateMark.NumberBar.AttrName
    /// </summary>
    public class AttrName : UiNode<MapEditorCreateMarkPanel, Godot.Label, AttrName>
    {
        public AttrName(MapEditorCreateMarkPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override AttrName Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.SpinBox"/>, 路径: MapEditorCreateMark.NumberBar.NumInput
    /// </summary>
    public class NumInput : UiNode<MapEditorCreateMarkPanel, Godot.SpinBox, NumInput>
    {
        public NumInput(MapEditorCreateMarkPanel uiPanel, Godot.SpinBox node) : base(uiPanel, node) {  }
        public override NumInput Clone() => new (UiPanel, (Godot.SpinBox)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorCreateMark.NumberAttribute"/>, 路径: MapEditorCreateMark.NumberBar
    /// </summary>
    public class NumberBar : UiNode<MapEditorCreateMarkPanel, UI.MapEditorCreateMark.NumberAttribute, NumberBar>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.AttrName
        /// </summary>
        public AttrName L_AttrName
        {
            get
            {
                if (_L_AttrName == null) _L_AttrName = new AttrName(UiPanel, Instance.GetNodeOrNull<Godot.Label>("AttrName"));
                return _L_AttrName;
            }
        }
        private AttrName _L_AttrName;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateMark.NumInput
        /// </summary>
        public NumInput L_NumInput
        {
            get
            {
                if (_L_NumInput == null) _L_NumInput = new NumInput(UiPanel, Instance.GetNodeOrNull<Godot.SpinBox>("NumInput"));
                return _L_NumInput;
            }
        }
        private NumInput _L_NumInput;

        public NumberBar(MapEditorCreateMarkPanel uiPanel, UI.MapEditorCreateMark.NumberAttribute node) : base(uiPanel, node) {  }
        public override NumberBar Clone() => new (UiPanel, (UI.MapEditorCreateMark.NumberAttribute)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer2.WaveNameLabel
    /// </summary>
    public WaveNameLabel S_WaveNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_HBoxContainer2.L_WaveNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.OptionButton"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer2.WaveOption
    /// </summary>
    public WaveOption S_WaveOption => L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_HBoxContainer2.L_WaveOption;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer2
    /// </summary>
    public HBoxContainer2 S_HBoxContainer2 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_HBoxContainer2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer.DelayNameLabel
    /// </summary>
    public DelayNameLabel S_DelayNameLabel => L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_HBoxContainer.L_DelayNameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3.HBoxContainer.DelayInput
    /// </summary>
    public DelayInput S_DelayInput => L_MarginContainer.L_VBoxContainer.L_HBoxContainer3.L_HBoxContainer.L_DelayInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.HBoxContainer3
    /// </summary>
    public HBoxContainer3 S_HBoxContainer3 => L_MarginContainer.L_VBoxContainer.L_HBoxContainer3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.AddMark
    /// </summary>
    public AddMark S_AddMark => L_MarginContainer.L_VBoxContainer.L_AddMark;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.Control
    /// </summary>
    public Control S_Control => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_HBoxContainer.L_Control;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.IconTitle
    /// </summary>
    public IconTitle S_IconTitle => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_HBoxContainer.L_IconTitle;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.IdTitle
    /// </summary>
    public IdTitle S_IdTitle => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_HBoxContainer.L_IdTitle;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.NameTitle
    /// </summary>
    public NameTitle S_NameTitle => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_HBoxContainer.L_NameTitle;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.TypeTitle
    /// </summary>
    public TypeTitle S_TypeTitle => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_HBoxContainer.L_TypeTitle;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.WeightTitle
    /// </summary>
    public WeightTitle S_WeightTitle => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_HBoxContainer.L_WeightTitle;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.HBoxContainer.ExtraTitle
    /// </summary>
    public ExtraTitle S_ExtraTitle => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_HBoxContainer.L_ExtraTitle;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.ExpandButton
    /// </summary>
    public ExpandButton S_ExpandButton => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer.L_MarkObject.L_HBoxContainer.L_ExpandButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.Icon
    /// </summary>
    public Icon S_Icon => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer.L_MarkObject.L_HBoxContainer.L_Icon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.IdLabel
    /// </summary>
    public IdLabel S_IdLabel => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer.L_MarkObject.L_HBoxContainer.L_IdLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.NameLabel
    /// </summary>
    public NameLabel S_NameLabel => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer.L_MarkObject.L_HBoxContainer.L_NameLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.TypeLabel
    /// </summary>
    public TypeLabel S_TypeLabel => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer.L_MarkObject.L_HBoxContainer.L_TypeLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.LineEdit
    /// </summary>
    public LineEdit S_LineEdit => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer.L_MarkObject.L_HBoxContainer.L_LineEdit;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.CenterContainer.DeleteButton
    /// </summary>
    public DeleteButton S_DeleteButton => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer.L_MarkObject.L_HBoxContainer.L_CenterContainer.L_DeleteButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CenterContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject.HBoxContainer.CenterContainer
    /// </summary>
    public CenterContainer S_CenterContainer => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer.L_MarkObject.L_HBoxContainer.L_CenterContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer.MarkObject
    /// </summary>
    public MarkObject S_MarkObject => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer.L_MarkObject;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel.VBoxContainer.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_MarginContainer.L_VBoxContainer.L_Panel.L_VBoxContainer.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: MapEditorCreateMark.MarginContainer.VBoxContainer.Panel
    /// </summary>
    public Panel S_Panel => L_MarginContainer.L_VBoxContainer.L_Panel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreateMark.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.GridContainer"/>, 节点路径: MapEditorCreateMark.ExpandPanel.ExpandGrid
    /// </summary>
    public ExpandGrid S_ExpandGrid => L_ExpandPanel.L_ExpandGrid;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: MapEditorCreateMark.ExpandPanel
    /// </summary>
    public ExpandPanel S_ExpandPanel => L_ExpandPanel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: MapEditorCreateMark.NumberBar.AttrName
    /// </summary>
    public AttrName S_AttrName => L_NumberBar.L_AttrName;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.SpinBox"/>, 节点路径: MapEditorCreateMark.NumberBar.NumInput
    /// </summary>
    public NumInput S_NumInput => L_NumberBar.L_NumInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorCreateMark.NumberAttribute"/>, 节点路径: MapEditorCreateMark.NumberBar
    /// </summary>
    public NumberBar S_NumberBar => L_NumberBar;

}
