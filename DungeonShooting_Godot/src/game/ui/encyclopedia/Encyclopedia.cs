namespace UI.Encyclopedia;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class Encyclopedia : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Encyclopedia.ColorRect
    /// </summary>
    public ColorRect L_ColorRect
    {
        get
        {
            if (_L_ColorRect == null) _L_ColorRect = new ColorRect((EncyclopediaPanel)this, GetNode<Godot.ColorRect>("ColorRect"));
            return _L_ColorRect;
        }
    }
    private ColorRect _L_ColorRect;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.NinePatchRect
    /// </summary>
    public NinePatchRect L_NinePatchRect
    {
        get
        {
            if (_L_NinePatchRect == null) _L_NinePatchRect = new NinePatchRect((EncyclopediaPanel)this, GetNode<Godot.NinePatchRect>("NinePatchRect"));
            return _L_NinePatchRect;
        }
    }
    private NinePatchRect _L_NinePatchRect;


    public Encyclopedia() : base(nameof(Encyclopedia))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: Encyclopedia.ColorRect
    /// </summary>
    public class ColorRect : UiNode<EncyclopediaPanel, Godot.ColorRect, ColorRect>
    {
        public ColorRect(EncyclopediaPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override ColorRect Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Encyclopedia.NinePatchRect.TextureRect.Label
    /// </summary>
    public class Label : UiNode<EncyclopediaPanel, Godot.Label, Label>
    {
        public Label(EncyclopediaPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Label Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: Encyclopedia.NinePatchRect.TextureRect
    /// </summary>
    public class TextureRect : UiNode<EncyclopediaPanel, Godot.TextureRect, TextureRect>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Encyclopedia.NinePatchRect.Label
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

        public TextureRect(EncyclopediaPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override TextureRect Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.ObjectButton.Bg
    /// </summary>
    public class Bg : UiNode<EncyclopediaPanel, Godot.NinePatchRect, Bg>
    {
        public Bg(EncyclopediaPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.ObjectButton.PreviewImage
    /// </summary>
    public class PreviewImage : UiNode<EncyclopediaPanel, Godot.TextureRect, PreviewImage>
    {
        public PreviewImage(EncyclopediaPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override PreviewImage Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.ObjectButton.Select
    /// </summary>
    public class Select : UiNode<EncyclopediaPanel, Godot.NinePatchRect, Select>
    {
        public Select(EncyclopediaPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override Select Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.ObjectButton
    /// </summary>
    public class ObjectButton : UiNode<EncyclopediaPanel, Godot.TextureButton, ObjectButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.Bg
        /// </summary>
        public Bg L_Bg
        {
            get
            {
                if (_L_Bg == null) _L_Bg = new Bg(UiPanel, Instance.GetNode<Godot.NinePatchRect>("Bg"));
                return _L_Bg;
            }
        }
        private Bg _L_Bg;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.PreviewImage
        /// </summary>
        public PreviewImage L_PreviewImage
        {
            get
            {
                if (_L_PreviewImage == null) _L_PreviewImage = new PreviewImage(UiPanel, Instance.GetNode<Godot.TextureRect>("PreviewImage"));
                return _L_PreviewImage;
            }
        }
        private PreviewImage _L_PreviewImage;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.Select
        /// </summary>
        public Select L_Select
        {
            get
            {
                if (_L_Select == null) _L_Select = new Select(UiPanel, Instance.GetNode<Godot.NinePatchRect>("Select"));
                return _L_Select;
            }
        }
        private Select _L_Select;

        public ObjectButton(EncyclopediaPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override ObjectButton Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<EncyclopediaPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ObjectButton
        /// </summary>
        public ObjectButton L_ObjectButton
        {
            get
            {
                if (_L_ObjectButton == null) _L_ObjectButton = new ObjectButton(UiPanel, Instance.GetNode<Godot.TextureButton>("ObjectButton"));
                return _L_ObjectButton;
            }
        }
        private ObjectButton _L_ObjectButton;

        public ScrollContainer(EncyclopediaPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect
    /// </summary>
    public class NinePatchRect_1 : UiNode<EncyclopediaPanel, Godot.NinePatchRect, NinePatchRect_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.ScrollContainer
        /// </summary>
        public ScrollContainer L_ScrollContainer
        {
            get
            {
                if (_L_ScrollContainer == null) _L_ScrollContainer = new ScrollContainer(UiPanel, Instance.GetNode<Godot.ScrollContainer>("ScrollContainer"));
                return _L_ScrollContainer;
            }
        }
        private ScrollContainer _L_ScrollContainer;

        public NinePatchRect_1(EncyclopediaPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override NinePatchRect_1 Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2
    /// </summary>
    public class VBoxContainer2 : UiNode<EncyclopediaPanel, Godot.VBoxContainer, VBoxContainer2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.NinePatchRect
        /// </summary>
        public NinePatchRect_1 L_NinePatchRect
        {
            get
            {
                if (_L_NinePatchRect == null) _L_NinePatchRect = new NinePatchRect_1(UiPanel, Instance.GetNode<Godot.NinePatchRect>("NinePatchRect"));
                return _L_NinePatchRect;
            }
        }
        private NinePatchRect_1 _L_NinePatchRect;

        public VBoxContainer2(EncyclopediaPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer2 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer.ItemName
    /// </summary>
    public class ItemName : UiNode<EncyclopediaPanel, Godot.Label, ItemName>
    {
        public ItemName(EncyclopediaPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override ItemName Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer.NinePatchRect.ItemTexture
    /// </summary>
    public class ItemTexture : UiNode<EncyclopediaPanel, Godot.TextureRect, ItemTexture>
    {
        public ItemTexture(EncyclopediaPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override ItemTexture Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer.NinePatchRect
    /// </summary>
    public class NinePatchRect_3 : UiNode<EncyclopediaPanel, Godot.NinePatchRect, NinePatchRect_3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer.ItemTexture
        /// </summary>
        public ItemTexture L_ItemTexture
        {
            get
            {
                if (_L_ItemTexture == null) _L_ItemTexture = new ItemTexture(UiPanel, Instance.GetNode<Godot.TextureRect>("ItemTexture"));
                return _L_ItemTexture;
            }
        }
        private ItemTexture _L_ItemTexture;

        public NinePatchRect_3(EncyclopediaPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override NinePatchRect_3 Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.RichTextLabel"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer.ItemDes
    /// </summary>
    public class ItemDes : UiNode<EncyclopediaPanel, Godot.RichTextLabel, ItemDes>
    {
        public ItemDes(EncyclopediaPanel uiPanel, Godot.RichTextLabel node) : base(uiPanel, node) {  }
        public override ItemDes Clone() => new (UiPanel, (Godot.RichTextLabel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<EncyclopediaPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.ItemName
        /// </summary>
        public ItemName L_ItemName
        {
            get
            {
                if (_L_ItemName == null) _L_ItemName = new ItemName(UiPanel, Instance.GetNode<Godot.Label>("ItemName"));
                return _L_ItemName;
            }
        }
        private ItemName _L_ItemName;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.NinePatchRect
        /// </summary>
        public NinePatchRect_3 L_NinePatchRect
        {
            get
            {
                if (_L_NinePatchRect == null) _L_NinePatchRect = new NinePatchRect_3(UiPanel, Instance.GetNode<Godot.NinePatchRect>("NinePatchRect"));
                return _L_NinePatchRect;
            }
        }
        private NinePatchRect_3 _L_NinePatchRect;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.RichTextLabel"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.ItemDes
        /// </summary>
        public ItemDes L_ItemDes
        {
            get
            {
                if (_L_ItemDes == null) _L_ItemDes = new ItemDes(UiPanel, Instance.GetNode<Godot.RichTextLabel>("ItemDes"));
                return _L_ItemDes;
            }
        }
        private ItemDes _L_ItemDes;

        public VBoxContainer(EncyclopediaPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect
    /// </summary>
    public class NinePatchRect_2 : UiNode<EncyclopediaPanel, Godot.NinePatchRect, NinePatchRect_2>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.VBoxContainer
        /// </summary>
        public VBoxContainer L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer(UiPanel, Instance.GetNode<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer _L_VBoxContainer;

        public NinePatchRect_2(EncyclopediaPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override NinePatchRect_2 Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3
    /// </summary>
    public class VBoxContainer3 : UiNode<EncyclopediaPanel, Godot.VBoxContainer, VBoxContainer3>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.NinePatchRect
        /// </summary>
        public NinePatchRect_2 L_NinePatchRect
        {
            get
            {
                if (_L_NinePatchRect == null) _L_NinePatchRect = new NinePatchRect_2(UiPanel, Instance.GetNode<Godot.NinePatchRect>("NinePatchRect"));
                return _L_NinePatchRect;
            }
        }
        private NinePatchRect_2 _L_NinePatchRect;

        public VBoxContainer3(EncyclopediaPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer3 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<EncyclopediaPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.VBoxContainer2
        /// </summary>
        public VBoxContainer2 L_VBoxContainer2
        {
            get
            {
                if (_L_VBoxContainer2 == null) _L_VBoxContainer2 = new VBoxContainer2(UiPanel, Instance.GetNode<Godot.VBoxContainer>("VBoxContainer2"));
                return _L_VBoxContainer2;
            }
        }
        private VBoxContainer2 _L_VBoxContainer2;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.VBoxContainer3
        /// </summary>
        public VBoxContainer3 L_VBoxContainer3
        {
            get
            {
                if (_L_VBoxContainer3 == null) _L_VBoxContainer3 = new VBoxContainer3(UiPanel, Instance.GetNode<Godot.VBoxContainer>("VBoxContainer3"));
                return _L_VBoxContainer3;
            }
        }
        private VBoxContainer3 _L_VBoxContainer3;

        public HBoxContainer(EncyclopediaPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: Encyclopedia.NinePatchRect.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<EncyclopediaPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Encyclopedia.NinePatchRect.HBoxContainer
        /// </summary>
        public HBoxContainer L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer _L_HBoxContainer;

        public MarginContainer(EncyclopediaPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: Encyclopedia.NinePatchRect
    /// </summary>
    public class NinePatchRect : UiNode<EncyclopediaPanel, Godot.NinePatchRect, NinePatchRect>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: Encyclopedia.TextureRect
        /// </summary>
        public TextureRect L_TextureRect
        {
            get
            {
                if (_L_TextureRect == null) _L_TextureRect = new TextureRect(UiPanel, Instance.GetNode<Godot.TextureRect>("TextureRect"));
                return _L_TextureRect;
            }
        }
        private TextureRect _L_TextureRect;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: Encyclopedia.MarginContainer
        /// </summary>
        public MarginContainer L_MarginContainer
        {
            get
            {
                if (_L_MarginContainer == null) _L_MarginContainer = new MarginContainer(UiPanel, Instance.GetNode<Godot.MarginContainer>("MarginContainer"));
                return _L_MarginContainer;
            }
        }
        private MarginContainer _L_MarginContainer;

        public NinePatchRect(EncyclopediaPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override NinePatchRect Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: Encyclopedia.ColorRect
    /// </summary>
    public ColorRect S_ColorRect => L_ColorRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Encyclopedia.NinePatchRect.TextureRect.Label
    /// </summary>
    public Label S_Label => L_NinePatchRect.L_TextureRect.L_Label;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: Encyclopedia.NinePatchRect.TextureRect
    /// </summary>
    public TextureRect S_TextureRect => L_NinePatchRect.L_TextureRect;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.ObjectButton.Bg
    /// </summary>
    public Bg S_Bg => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer2.L_NinePatchRect.L_ScrollContainer.L_ObjectButton.L_Bg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.ObjectButton.PreviewImage
    /// </summary>
    public PreviewImage S_PreviewImage => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer2.L_NinePatchRect.L_ScrollContainer.L_ObjectButton.L_PreviewImage;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.ObjectButton.Select
    /// </summary>
    public Select S_Select => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer2.L_NinePatchRect.L_ScrollContainer.L_ObjectButton.L_Select;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer.ObjectButton
    /// </summary>
    public ObjectButton S_ObjectButton => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer2.L_NinePatchRect.L_ScrollContainer.L_ObjectButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2.NinePatchRect.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer2.L_NinePatchRect.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer2
    /// </summary>
    public VBoxContainer2 S_VBoxContainer2 => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer2;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer.ItemName
    /// </summary>
    public ItemName S_ItemName => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer3.L_NinePatchRect.L_VBoxContainer.L_ItemName;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer.NinePatchRect.ItemTexture
    /// </summary>
    public ItemTexture S_ItemTexture => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer3.L_NinePatchRect.L_VBoxContainer.L_NinePatchRect.L_ItemTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.RichTextLabel"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer.ItemDes
    /// </summary>
    public ItemDes S_ItemDes => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer3.L_NinePatchRect.L_VBoxContainer.L_ItemDes;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3.NinePatchRect.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer3.L_NinePatchRect.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer.VBoxContainer3
    /// </summary>
    public VBoxContainer3 S_VBoxContainer3 => L_NinePatchRect.L_MarginContainer.L_HBoxContainer.L_VBoxContainer3;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_NinePatchRect.L_MarginContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: Encyclopedia.NinePatchRect.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_NinePatchRect.L_MarginContainer;

}
