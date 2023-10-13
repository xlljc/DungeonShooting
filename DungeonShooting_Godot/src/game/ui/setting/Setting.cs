namespace UI.Setting;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Setting : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Setting.ColorRect
    /// </summary>
    public ColorRect L_ColorRect
    {
        get
        {
            if (_L_ColorRect == null) _L_ColorRect = new ColorRect((SettingPanel)this, GetNode<Godot.ColorRect>("ColorRect"));
            return _L_ColorRect;
        }
    }
    private ColorRect _L_ColorRect;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Setting.Back
    /// </summary>
    public Back L_Back
    {
        get
        {
            if (_L_Back == null) _L_Back = new Back((SettingPanel)this, GetNode<Godot.Button>("Back"));
            return _L_Back;
        }
    }
    private Back _L_Back;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.Title
    /// </summary>
    public Title L_Title
    {
        get
        {
            if (_L_Title == null) _L_Title = new Title((SettingPanel)this, GetNode<Godot.Label>("Title"));
            return _L_Title;
        }
    }
    private Title _L_Title;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: Setting.ScrollContainer
    /// </summary>
    public ScrollContainer L_ScrollContainer
    {
        get
        {
            if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer((SettingPanel)this, GetNode<Godot.ScrollContainer>("ScrollContainer"));
            return _L_ScrollContainer;
        }
    }
    private ScrollContainer _L_ScrollContainer;


    public Setting() : base(nameof(Setting))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Setting.ColorRect
    /// </summary>
    public class ColorRect : UiNode<SettingPanel, Godot.ColorRect, ColorRect>
    {
        public ColorRect(SettingPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override ColorRect Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Setting.Back
    /// </summary>
    public class Back : UiNode<SettingPanel, Godot.Button, Back>
    {
        public Back(SettingPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.Title
    /// </summary>
    public class Title : UiNode<SettingPanel, Godot.Label, Title>
    {
        public Title(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Setting.ScrollContainer.SettingMenu.VideoItem
    /// </summary>
    public class VideoItem : UiNode<SettingPanel, Godot.Button, VideoItem>
    {
        public VideoItem(SettingPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override VideoItem Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Setting.ScrollContainer.SettingMenu.InputItem
    /// </summary>
    public class InputItem : UiNode<SettingPanel, Godot.Button, InputItem>
    {
        public InputItem(SettingPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override InputItem Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Setting.ScrollContainer.SettingMenu
    /// </summary>
    public class SettingMenu : UiNode<SettingPanel, Godot.VBoxContainer, SettingMenu>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Setting.ScrollContainer.VideoItem
        /// </summary>
        public VideoItem L_VideoItem
        {
            get
            {
                if (_L_VideoItem == null) _L_VideoItem = new VideoItem(UiPanel, Instance.GetNode<Godot.Button>("VideoItem"));
                return _L_VideoItem;
            }
        }
        private VideoItem _L_VideoItem;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Setting.ScrollContainer.InputItem
        /// </summary>
        public InputItem L_InputItem
        {
            get
            {
                if (_L_InputItem == null) _L_InputItem = new InputItem(UiPanel, Instance.GetNode<Godot.Button>("InputItem"));
                return _L_InputItem;
            }
        }
        private InputItem _L_InputItem;

        public SettingMenu(SettingPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override SettingMenu Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.VideoSetting.FullScreen.Name
    /// </summary>
    public class Name : UiNode<SettingPanel, Godot.Label, Name>
    {
        public Name(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.CheckBox"/>, 路径: Setting.ScrollContainer.VideoSetting.FullScreen.CheckBox
    /// </summary>
    public class CheckBox : UiNode<SettingPanel, Godot.CheckBox, CheckBox>
    {
        public CheckBox(SettingPanel uiPanel, Godot.CheckBox node) : base(uiPanel, node) {  }
        public override CheckBox Clone() => new (UiPanel, (Godot.CheckBox)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.VideoSetting.FullScreen
    /// </summary>
    public class FullScreen : UiNode<SettingPanel, Godot.HBoxContainer, FullScreen>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.VideoSetting.Name
        /// </summary>
        public Name L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.CheckBox"/>, 节点路径: Setting.ScrollContainer.VideoSetting.CheckBox
        /// </summary>
        public CheckBox L_CheckBox
        {
            get
            {
                if (_L_CheckBox == null) _L_CheckBox = new CheckBox(UiPanel, Instance.GetNode<Godot.CheckBox>("CheckBox"));
                return _L_CheckBox;
            }
        }
        private CheckBox _L_CheckBox;

        public FullScreen(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override FullScreen Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Setting.ScrollContainer.VideoSetting.Back
    /// </summary>
    public class Back_1 : UiNode<SettingPanel, Godot.Button, Back_1>
    {
        public Back_1(SettingPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back_1 Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Setting.ScrollContainer.VideoSetting
    /// </summary>
    public class VideoSetting : UiNode<SettingPanel, Godot.VBoxContainer, VideoSetting>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.FullScreen
        /// </summary>
        public FullScreen L_FullScreen
        {
            get
            {
                if (_L_FullScreen == null) _L_FullScreen = new FullScreen(UiPanel, Instance.GetNode<Godot.HBoxContainer>("FullScreen"));
                return _L_FullScreen;
            }
        }
        private FullScreen _L_FullScreen;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Setting.ScrollContainer.Back
        /// </summary>
        public Back_1 L_Back
        {
            get
            {
                if (_L_Back == null) _L_Back = new Back_1(UiPanel, Instance.GetNode<Godot.Button>("Back"));
                return _L_Back;
            }
        }
        private Back_1 _L_Back;

        public VideoSetting(SettingPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VideoSetting Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Tip
    /// </summary>
    public class Tip : UiNode<SettingPanel, Godot.Label, Tip>
    {
        public Tip(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Tip Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key.Name
    /// </summary>
    public class Name_1 : UiNode<SettingPanel, Godot.Label, Name_1>
    {
        public Name_1(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_1 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key.Value
    /// </summary>
    public class Value : UiNode<SettingPanel, Godot.Label, Value>
    {
        public Value(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key
    /// </summary>
    public class Key : UiNode<SettingPanel, Godot.HBoxContainer, Key>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_1 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_1(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_1 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value _L_Value;

        public Key(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key2.Name
    /// </summary>
    public class Name_2 : UiNode<SettingPanel, Godot.Label, Name_2>
    {
        public Name_2(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key2.Value
    /// </summary>
    public class Value_1 : UiNode<SettingPanel, Godot.Label, Value_1>
    {
        public Value_1(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_1 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key2
    /// </summary>
    public class Key2 : UiNode<SettingPanel, Godot.HBoxContainer, Key2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_2 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_2(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_2 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_1 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_1(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_1 _L_Value;

        public Key2(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key2 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key3.Name
    /// </summary>
    public class Name_3 : UiNode<SettingPanel, Godot.Label, Name_3>
    {
        public Name_3(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_3 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key3.Value
    /// </summary>
    public class Value_2 : UiNode<SettingPanel, Godot.Label, Value_2>
    {
        public Value_2(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_2 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key3
    /// </summary>
    public class Key3 : UiNode<SettingPanel, Godot.HBoxContainer, Key3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_3 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_3(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_3 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_2 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_2(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_2 _L_Value;

        public Key3(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key3 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key4.Name
    /// </summary>
    public class Name_4 : UiNode<SettingPanel, Godot.Label, Name_4>
    {
        public Name_4(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_4 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key4.Value
    /// </summary>
    public class Value_3 : UiNode<SettingPanel, Godot.Label, Value_3>
    {
        public Value_3(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_3 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key4
    /// </summary>
    public class Key4 : UiNode<SettingPanel, Godot.HBoxContainer, Key4>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_4 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_4(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_4 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_3 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_3(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_3 _L_Value;

        public Key4(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key4 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key5.Name
    /// </summary>
    public class Name_5 : UiNode<SettingPanel, Godot.Label, Name_5>
    {
        public Name_5(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_5 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key5.Value
    /// </summary>
    public class Value_4 : UiNode<SettingPanel, Godot.Label, Value_4>
    {
        public Value_4(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_4 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key5
    /// </summary>
    public class Key5 : UiNode<SettingPanel, Godot.HBoxContainer, Key5>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_5 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_5(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_5 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_4 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_4(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_4 _L_Value;

        public Key5(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key5 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key6.Name
    /// </summary>
    public class Name_6 : UiNode<SettingPanel, Godot.Label, Name_6>
    {
        public Name_6(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_6 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key6.Value
    /// </summary>
    public class Value_5 : UiNode<SettingPanel, Godot.Label, Value_5>
    {
        public Value_5(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_5 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key6
    /// </summary>
    public class Key6 : UiNode<SettingPanel, Godot.HBoxContainer, Key6>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_6 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_6(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_6 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_5 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_5(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_5 _L_Value;

        public Key6(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key6 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key7.Name
    /// </summary>
    public class Name_7 : UiNode<SettingPanel, Godot.Label, Name_7>
    {
        public Name_7(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_7 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key7.Value
    /// </summary>
    public class Value_6 : UiNode<SettingPanel, Godot.Label, Value_6>
    {
        public Value_6(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_6 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key7
    /// </summary>
    public class Key7 : UiNode<SettingPanel, Godot.HBoxContainer, Key7>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_7 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_7(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_7 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_6 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_6(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_6 _L_Value;

        public Key7(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key7 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key8.Name
    /// </summary>
    public class Name_8 : UiNode<SettingPanel, Godot.Label, Name_8>
    {
        public Name_8(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_8 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key8.Value
    /// </summary>
    public class Value_7 : UiNode<SettingPanel, Godot.Label, Value_7>
    {
        public Value_7(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_7 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key8
    /// </summary>
    public class Key8 : UiNode<SettingPanel, Godot.HBoxContainer, Key8>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_8 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_8(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_8 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_7 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_7(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_7 _L_Value;

        public Key8(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key8 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key11.Name
    /// </summary>
    public class Name_9 : UiNode<SettingPanel, Godot.Label, Name_9>
    {
        public Name_9(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_9 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key11.Value
    /// </summary>
    public class Value_8 : UiNode<SettingPanel, Godot.Label, Value_8>
    {
        public Value_8(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_8 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key11
    /// </summary>
    public class Key11 : UiNode<SettingPanel, Godot.HBoxContainer, Key11>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_9 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_9(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_9 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_8 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_8(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_8 _L_Value;

        public Key11(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key11 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key9.Name
    /// </summary>
    public class Name_10 : UiNode<SettingPanel, Godot.Label, Name_10>
    {
        public Name_10(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_10 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key9.Value
    /// </summary>
    public class Value_9 : UiNode<SettingPanel, Godot.Label, Value_9>
    {
        public Value_9(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_9 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key9
    /// </summary>
    public class Key9 : UiNode<SettingPanel, Godot.HBoxContainer, Key9>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_10 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_10(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_10 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_9 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_9(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_9 _L_Value;

        public Key9(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key9 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key10.Name
    /// </summary>
    public class Name_11 : UiNode<SettingPanel, Godot.Label, Name_11>
    {
        public Name_11(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_11 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key10.Value
    /// </summary>
    public class Value_10 : UiNode<SettingPanel, Godot.Label, Value_10>
    {
        public Value_10(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_10 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key10
    /// </summary>
    public class Key10 : UiNode<SettingPanel, Godot.HBoxContainer, Key10>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_11 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_11(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_11 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_10 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_10(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_10 _L_Value;

        public Key10(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key10 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key12.Name
    /// </summary>
    public class Name_12 : UiNode<SettingPanel, Godot.Label, Name_12>
    {
        public Name_12(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_12 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key12.Value
    /// </summary>
    public class Value_11 : UiNode<SettingPanel, Godot.Label, Value_11>
    {
        public Value_11(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_11 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key12
    /// </summary>
    public class Key12 : UiNode<SettingPanel, Godot.HBoxContainer, Key12>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_12 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_12(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_12 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_11 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_11(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_11 _L_Value;

        public Key12(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key12 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key13.Name
    /// </summary>
    public class Name_13 : UiNode<SettingPanel, Godot.Label, Name_13>
    {
        public Name_13(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Name_13 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Setting.ScrollContainer.KeySetting.Key13.Value
    /// </summary>
    public class Value_12 : UiNode<SettingPanel, Godot.Label, Value_12>
    {
        public Value_12(SettingPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Value_12 Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting.Key13
    /// </summary>
    public class Key13 : UiNode<SettingPanel, Godot.HBoxContainer, Key13>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Name
        /// </summary>
        public Name_13 L_Name
        {
            get
            {
                if (_L_Name == null) _L_Name = new Name_13(UiPanel, Instance.GetNode<Godot.Label>("Name"));
                return _L_Name;
            }
        }
        private Name_13 _L_Name;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Value
        /// </summary>
        public Value_12 L_Value
        {
            get
            {
                if (_L_Value == null) _L_Value = new Value_12(UiPanel, Instance.GetNode<Godot.Label>("Value"));
                return _L_Value;
            }
        }
        private Value_12 _L_Value;

        public Key13(SettingPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override Key13 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: Setting.ScrollContainer.KeySetting.Back
    /// </summary>
    public class Back_2 : UiNode<SettingPanel, Godot.Button, Back_2>
    {
        public Back_2(SettingPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back_2 Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Setting.ScrollContainer.KeySetting
    /// </summary>
    public class KeySetting : UiNode<SettingPanel, Godot.VBoxContainer, KeySetting>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.Tip
        /// </summary>
        public Tip L_Tip
        {
            get
            {
                if (_L_Tip == null) _L_Tip = new Tip(UiPanel, Instance.GetNode<Godot.Label>("Tip"));
                return _L_Tip;
            }
        }
        private Tip _L_Tip;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key
        /// </summary>
        public Key L_Key
        {
            get
            {
                if (_L_Key == null) _L_Key = new Key(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key"));
                return _L_Key;
            }
        }
        private Key _L_Key;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key2
        /// </summary>
        public Key2 L_Key2
        {
            get
            {
                if (_L_Key2 == null) _L_Key2 = new Key2(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key2"));
                return _L_Key2;
            }
        }
        private Key2 _L_Key2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key3
        /// </summary>
        public Key3 L_Key3
        {
            get
            {
                if (_L_Key3 == null) _L_Key3 = new Key3(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key3"));
                return _L_Key3;
            }
        }
        private Key3 _L_Key3;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key4
        /// </summary>
        public Key4 L_Key4
        {
            get
            {
                if (_L_Key4 == null) _L_Key4 = new Key4(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key4"));
                return _L_Key4;
            }
        }
        private Key4 _L_Key4;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key5
        /// </summary>
        public Key5 L_Key5
        {
            get
            {
                if (_L_Key5 == null) _L_Key5 = new Key5(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key5"));
                return _L_Key5;
            }
        }
        private Key5 _L_Key5;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key6
        /// </summary>
        public Key6 L_Key6
        {
            get
            {
                if (_L_Key6 == null) _L_Key6 = new Key6(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key6"));
                return _L_Key6;
            }
        }
        private Key6 _L_Key6;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key7
        /// </summary>
        public Key7 L_Key7
        {
            get
            {
                if (_L_Key7 == null) _L_Key7 = new Key7(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key7"));
                return _L_Key7;
            }
        }
        private Key7 _L_Key7;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key8
        /// </summary>
        public Key8 L_Key8
        {
            get
            {
                if (_L_Key8 == null) _L_Key8 = new Key8(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key8"));
                return _L_Key8;
            }
        }
        private Key8 _L_Key8;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key11
        /// </summary>
        public Key11 L_Key11
        {
            get
            {
                if (_L_Key11 == null) _L_Key11 = new Key11(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key11"));
                return _L_Key11;
            }
        }
        private Key11 _L_Key11;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key9
        /// </summary>
        public Key9 L_Key9
        {
            get
            {
                if (_L_Key9 == null) _L_Key9 = new Key9(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key9"));
                return _L_Key9;
            }
        }
        private Key9 _L_Key9;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key10
        /// </summary>
        public Key10 L_Key10
        {
            get
            {
                if (_L_Key10 == null) _L_Key10 = new Key10(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key10"));
                return _L_Key10;
            }
        }
        private Key10 _L_Key10;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key12
        /// </summary>
        public Key12 L_Key12
        {
            get
            {
                if (_L_Key12 == null) _L_Key12 = new Key12(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key12"));
                return _L_Key12;
            }
        }
        private Key12 _L_Key12;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.Key13
        /// </summary>
        public Key13 L_Key13
        {
            get
            {
                if (_L_Key13 == null) _L_Key13 = new Key13(UiPanel, Instance.GetNode<Godot.HBoxContainer>("Key13"));
                return _L_Key13;
            }
        }
        private Key13 _L_Key13;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: Setting.ScrollContainer.Back
        /// </summary>
        public Back_2 L_Back
        {
            get
            {
                if (_L_Back == null) _L_Back = new Back_2(UiPanel, Instance.GetNode<Godot.Button>("Back"));
                return _L_Back;
            }
        }
        private Back_2 _L_Back;

        public KeySetting(SettingPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override KeySetting Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: Setting.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<SettingPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Setting.SettingMenu
        /// </summary>
        public SettingMenu L_SettingMenu
        {
            get
            {
                if (_L_SettingMenu == null) _L_SettingMenu = new SettingMenu(UiPanel, Instance.GetNode<Godot.VBoxContainer>("SettingMenu"));
                return _L_SettingMenu;
            }
        }
        private SettingMenu _L_SettingMenu;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Setting.VideoSetting
        /// </summary>
        public VideoSetting L_VideoSetting
        {
            get
            {
                if (_L_VideoSetting == null) _L_VideoSetting = new VideoSetting(UiPanel, Instance.GetNode<Godot.VBoxContainer>("VideoSetting"));
                return _L_VideoSetting;
            }
        }
        private VideoSetting _L_VideoSetting;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Setting.KeySetting
        /// </summary>
        public KeySetting L_KeySetting
        {
            get
            {
                if (_L_KeySetting == null) _L_KeySetting = new KeySetting(UiPanel, Instance.GetNode<Godot.VBoxContainer>("KeySetting"));
                return _L_KeySetting;
            }
        }
        private KeySetting _L_KeySetting;

        public ScrollContainer(SettingPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Setting.ColorRect
    /// </summary>
    public ColorRect S_ColorRect => L_ColorRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.Title
    /// </summary>
    public Title S_Title => L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Setting.ScrollContainer.SettingMenu.VideoItem
    /// </summary>
    public VideoItem S_VideoItem => L_ScrollContainer.L_SettingMenu.L_VideoItem;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: Setting.ScrollContainer.SettingMenu.InputItem
    /// </summary>
    public InputItem S_InputItem => L_ScrollContainer.L_SettingMenu.L_InputItem;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Setting.ScrollContainer.SettingMenu
    /// </summary>
    public SettingMenu S_SettingMenu => L_ScrollContainer.L_SettingMenu;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.CheckBox"/>, 节点路径: Setting.ScrollContainer.VideoSetting.FullScreen.CheckBox
    /// </summary>
    public CheckBox S_CheckBox => L_ScrollContainer.L_VideoSetting.L_FullScreen.L_CheckBox;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.VideoSetting.FullScreen
    /// </summary>
    public FullScreen S_FullScreen => L_ScrollContainer.L_VideoSetting.L_FullScreen;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Setting.ScrollContainer.VideoSetting
    /// </summary>
    public VideoSetting S_VideoSetting => L_ScrollContainer.L_VideoSetting;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Setting.ScrollContainer.KeySetting.Tip
    /// </summary>
    public Tip S_Tip => L_ScrollContainer.L_KeySetting.L_Tip;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key
    /// </summary>
    public Key S_Key => L_ScrollContainer.L_KeySetting.L_Key;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key2
    /// </summary>
    public Key2 S_Key2 => L_ScrollContainer.L_KeySetting.L_Key2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key3
    /// </summary>
    public Key3 S_Key3 => L_ScrollContainer.L_KeySetting.L_Key3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key4
    /// </summary>
    public Key4 S_Key4 => L_ScrollContainer.L_KeySetting.L_Key4;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key5
    /// </summary>
    public Key5 S_Key5 => L_ScrollContainer.L_KeySetting.L_Key5;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key6
    /// </summary>
    public Key6 S_Key6 => L_ScrollContainer.L_KeySetting.L_Key6;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key7
    /// </summary>
    public Key7 S_Key7 => L_ScrollContainer.L_KeySetting.L_Key7;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key8
    /// </summary>
    public Key8 S_Key8 => L_ScrollContainer.L_KeySetting.L_Key8;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key11
    /// </summary>
    public Key11 S_Key11 => L_ScrollContainer.L_KeySetting.L_Key11;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key9
    /// </summary>
    public Key9 S_Key9 => L_ScrollContainer.L_KeySetting.L_Key9;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key10
    /// </summary>
    public Key10 S_Key10 => L_ScrollContainer.L_KeySetting.L_Key10;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key12
    /// </summary>
    public Key12 S_Key12 => L_ScrollContainer.L_KeySetting.L_Key12;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting.Key13
    /// </summary>
    public Key13 S_Key13 => L_ScrollContainer.L_KeySetting.L_Key13;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Setting.ScrollContainer.KeySetting
    /// </summary>
    public KeySetting S_KeySetting => L_ScrollContainer.L_KeySetting;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: Setting.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_ScrollContainer;

}
