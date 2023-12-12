namespace UI.TileSetEditorProject;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class TileSetEditorProject : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorProject.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg((TileSetEditorProjectPanel)this, GetNode<Godot.Panel>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;


    public TileSetEditorProject() : base(nameof(TileSetEditorProject))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.Head.Back
    /// </summary>
    public class Back : UiNode<TileSetEditorProjectPanel, Godot.Button, Back>
    {
        public Back(TileSetEditorProjectPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.Head.Title
    /// </summary>
    public class Title : UiNode<TileSetEditorProjectPanel, Godot.Label, Title>
    {
        public Title(TileSetEditorProjectPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.Head
    /// </summary>
    public class Head : UiNode<TileSetEditorProjectPanel, Godot.Panel, Head>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.Back
        /// </summary>
        public Back L_Back
        {
            get
            {
                if (_L_Back == null) _L_Back = new Back(UiPanel, Instance.GetNode<Godot.Button>("Back"));
                return _L_Back;
            }
        }
        private Back _L_Back;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.Title
        /// </summary>
        public Title L_Title
        {
            get
            {
                if (_L_Title == null) _L_Title = new Title(UiPanel, Instance.GetNode<Godot.Label>("Title"));
                return _L_Title;
            }
        }
        private Title _L_Title;

        public Head(TileSetEditorProjectPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Head Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.LineEdit"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.TileSearchInput
    /// </summary>
    public class TileSearchInput : UiNode<TileSetEditorProjectPanel, Godot.LineEdit, TileSearchInput>
    {
        public TileSearchInput(TileSetEditorProjectPanel uiPanel, Godot.LineEdit node) : base(uiPanel, node) {  }
        public override TileSearchInput Clone() => new (UiPanel, (Godot.LineEdit)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.TileSearchButton
    /// </summary>
    public class TileSearchButton : UiNode<TileSetEditorProjectPanel, Godot.Button, TileSearchButton>
    {
        public TileSearchButton(TileSetEditorProjectPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override TileSearchButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.TileAddButton
    /// </summary>
    public class TileAddButton : UiNode<TileSetEditorProjectPanel, Godot.Button, TileAddButton>
    {
        public TileAddButton(TileSetEditorProjectPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override TileAddButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer_1 : UiNode<TileSetEditorProjectPanel, Godot.HBoxContainer, HBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.TileSearchInput
        /// </summary>
        public TileSearchInput L_TileSearchInput
        {
            get
            {
                if (_L_TileSearchInput == null) _L_TileSearchInput = new TileSearchInput(UiPanel, Instance.GetNode<Godot.LineEdit>("TileSearchInput"));
                return _L_TileSearchInput;
            }
        }
        private TileSearchInput _L_TileSearchInput;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.TileSearchButton
        /// </summary>
        public TileSearchButton L_TileSearchButton
        {
            get
            {
                if (_L_TileSearchButton == null) _L_TileSearchButton = new TileSearchButton(UiPanel, Instance.GetNode<Godot.Button>("TileSearchButton"));
                return _L_TileSearchButton;
            }
        }
        private TileSearchButton _L_TileSearchButton;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.TileAddButton
        /// </summary>
        public TileAddButton L_TileAddButton
        {
            get
            {
                if (_L_TileAddButton == null) _L_TileAddButton = new TileAddButton(UiPanel, Instance.GetNode<Godot.Button>("TileAddButton"));
                return _L_TileAddButton;
            }
        }
        private TileAddButton _L_TileAddButton;

        public HBoxContainer_1(TileSetEditorProjectPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer_1 Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.TileButton.PreviewImage
    /// </summary>
    public class PreviewImage : UiNode<TileSetEditorProjectPanel, Godot.TextureRect, PreviewImage>
    {
        public PreviewImage(TileSetEditorProjectPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override PreviewImage Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.TileButton.TileName
    /// </summary>
    public class TileName : UiNode<TileSetEditorProjectPanel, Godot.Label, TileName>
    {
        public TileName(TileSetEditorProjectPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override TileName Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.TileButton.SelectTexture
    /// </summary>
    public class SelectTexture : UiNode<TileSetEditorProjectPanel, Godot.NinePatchRect, SelectTexture>
    {
        public SelectTexture(TileSetEditorProjectPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override SelectTexture Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.TileButton
    /// </summary>
    public class TileButton : UiNode<TileSetEditorProjectPanel, Godot.Button, TileButton>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.PreviewImage
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.TileName
        /// </summary>
        public TileName L_TileName
        {
            get
            {
                if (_L_TileName == null) _L_TileName = new TileName(UiPanel, Instance.GetNode<Godot.Label>("TileName"));
                return _L_TileName;
            }
        }
        private TileName _L_TileName;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.SelectTexture
        /// </summary>
        public SelectTexture L_SelectTexture
        {
            get
            {
                if (_L_SelectTexture == null) _L_SelectTexture = new SelectTexture(UiPanel, Instance.GetNode<Godot.NinePatchRect>("SelectTexture"));
                return _L_SelectTexture;
            }
        }
        private SelectTexture _L_SelectTexture;

        public TileButton(TileSetEditorProjectPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override TileButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<TileSetEditorProjectPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.TileButton
        /// </summary>
        public TileButton L_TileButton
        {
            get
            {
                if (_L_TileButton == null) _L_TileButton = new TileButton(UiPanel, Instance.GetNode<Godot.Button>("TileButton"));
                return _L_TileButton;
            }
        }
        private TileButton _L_TileButton;

        public ScrollContainer(TileSetEditorProjectPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer
    /// </summary>
    public class VBoxContainer_1 : UiNode<TileSetEditorProjectPanel, Godot.VBoxContainer, VBoxContainer_1>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.HBoxContainer
        /// </summary>
        public HBoxContainer_1 L_HBoxContainer
        {
            get
            {
                if (_L_HBoxContainer == null) _L_HBoxContainer = new HBoxContainer_1(UiPanel, Instance.GetNode<Godot.HBoxContainer>("HBoxContainer"));
                return _L_HBoxContainer;
            }
        }
        private HBoxContainer_1 _L_HBoxContainer;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.ScrollContainer
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

        public VBoxContainer_1(TileSetEditorProjectPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer_1 Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<TileSetEditorProjectPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.VBoxContainer
        /// </summary>
        public VBoxContainer_1 L_VBoxContainer
        {
            get
            {
                if (_L_VBoxContainer == null) _L_VBoxContainer = new VBoxContainer_1(UiPanel, Instance.GetNode<Godot.VBoxContainer>("VBoxContainer"));
                return _L_VBoxContainer;
            }
        }
        private VBoxContainer_1 _L_VBoxContainer;

        public MarginContainer(TileSetEditorProjectPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel
    /// </summary>
    public class Panel : UiNode<TileSetEditorProjectPanel, Godot.Panel, Panel>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.MarginContainer
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

        public Panel(TileSetEditorProjectPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Panel Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<TileSetEditorProjectPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.Panel
        /// </summary>
        public Panel L_Panel
        {
            get
            {
                if (_L_Panel == null) _L_Panel = new Panel(UiPanel, Instance.GetNode<Godot.Panel>("Panel"));
                return _L_Panel;
            }
        }
        private Panel _L_Panel;

        public HBoxContainer(TileSetEditorProjectPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: TileSetEditorProject.Bg.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<TileSetEditorProjectPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorProject.Bg.Head
        /// </summary>
        public Head L_Head
        {
            get
            {
                if (_L_Head == null) _L_Head = new Head(UiPanel, Instance.GetNode<Godot.Panel>("Head"));
                return _L_Head;
            }
        }
        private Head _L_Head;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: TileSetEditorProject.Bg.HBoxContainer
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

        public VBoxContainer(TileSetEditorProjectPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditorProject.Bg
    /// </summary>
    public class Bg : UiNode<TileSetEditorProjectPanel, Godot.Panel, Bg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: TileSetEditorProject.VBoxContainer
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

        public Bg(TileSetEditorProjectPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.Head.Back
    /// </summary>
    public Back S_Back => L_Bg.L_VBoxContainer.L_Head.L_Back;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.Head.Title
    /// </summary>
    public Title S_Title => L_Bg.L_VBoxContainer.L_Head.L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.Head
    /// </summary>
    public Head S_Head => L_Bg.L_VBoxContainer.L_Head;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.LineEdit"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.TileSearchInput
    /// </summary>
    public TileSearchInput S_TileSearchInput => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_TileSearchInput;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.TileSearchButton
    /// </summary>
    public TileSearchButton S_TileSearchButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_TileSearchButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.HBoxContainer.TileAddButton
    /// </summary>
    public TileAddButton S_TileAddButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_HBoxContainer.L_TileAddButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.TileButton.PreviewImage
    /// </summary>
    public PreviewImage S_PreviewImage => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_ScrollContainer.L_TileButton.L_PreviewImage;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.TileButton.TileName
    /// </summary>
    public TileName S_TileName => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_ScrollContainer.L_TileButton.L_TileName;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.TileButton.SelectTexture
    /// </summary>
    public SelectTexture S_SelectTexture => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_ScrollContainer.L_TileButton.L_SelectTexture;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer.TileButton
    /// </summary>
    public TileButton S_TileButton => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_ScrollContainer.L_TileButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer.VBoxContainer.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer.L_VBoxContainer.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel.L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorProject.Bg.VBoxContainer.HBoxContainer.Panel
    /// </summary>
    public Panel S_Panel => L_Bg.L_VBoxContainer.L_HBoxContainer.L_Panel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditorProject.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

}
