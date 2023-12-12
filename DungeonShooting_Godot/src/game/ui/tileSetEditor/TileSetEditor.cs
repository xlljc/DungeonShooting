namespace UI.TileSetEditor;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class TileSetEditor : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg((TileSetEditorPanel)this, GetNode<Godot.Panel>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;


    public TileSetEditor() : base(nameof(TileSetEditor))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.Head.Back
    /// </summary>
    public class Back : UiNode<TileSetEditorPanel, Godot.Button, Back>
    {
        public Back(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.Head.Save
    /// </summary>
    public class Save : UiNode<TileSetEditorPanel, Godot.Button, Save>
    {
        public Save(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Save Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditor.Bg.VBoxContainer.Head.Title
    /// </summary>
    public class Title : UiNode<TileSetEditorPanel, Godot.Label, Title>
    {
        public Title(TileSetEditorPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditor.Bg.VBoxContainer.Head
    /// </summary>
    public class Head : UiNode<TileSetEditorPanel, Godot.Panel, Head>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Back
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Save
        /// </summary>
        public Save L_Save
        {
            get
            {
                if (_L_Save == null) _L_Save = new Save(UiPanel, Instance.GetNode<Godot.Button>("Save"));
                return _L_Save;
            }
        }
        private Save _L_Save;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Title
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

        public Head(TileSetEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Head Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.NinePatchRect"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.ScrollContainer.Tab.Select
    /// </summary>
    public class Select : UiNode<TileSetEditorPanel, Godot.NinePatchRect, Select>
    {
        public Select(TileSetEditorPanel uiPanel, Godot.NinePatchRect node) : base(uiPanel, node) {  }
        public override Select Clone() => new (UiPanel, (Godot.NinePatchRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.ScrollContainer.Tab
    /// </summary>
    public class Tab : UiNode<TileSetEditorPanel, Godot.Button, Tab>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.ScrollContainer.Select
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

        public Tab(TileSetEditorPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Tab Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.ScrollContainer"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.ScrollContainer
    /// </summary>
    public class ScrollContainer : UiNode<TileSetEditorPanel, Godot.ScrollContainer, ScrollContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.Tab
        /// </summary>
        public Tab L_Tab
        {
            get
            {
                if (_L_Tab == null) _L_Tab = new Tab(UiPanel, Instance.GetNode<Godot.Button>("Tab"));
                return _L_Tab;
            }
        }
        private Tab _L_Tab;

        public ScrollContainer(TileSetEditorPanel uiPanel, Godot.ScrollContainer node) : base(uiPanel, node) {  }
        public override ScrollContainer Clone() => new (UiPanel, (Godot.ScrollContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer
    /// </summary>
    public class MarginContainer : UiNode<TileSetEditorPanel, Godot.MarginContainer, MarginContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.ScrollContainer
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

        public MarginContainer(TileSetEditorPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override MarginContainer Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot
    /// </summary>
    public class LeftRoot : UiNode<TileSetEditorPanel, Godot.Panel, LeftRoot>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.MarginContainer
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

        public LeftRoot(TileSetEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override LeftRoot Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot
    /// </summary>
    public class RightRoot : UiNode<TileSetEditorPanel, Godot.Panel, RightRoot>
    {
        public RightRoot(TileSetEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override RightRoot Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.HBoxContainer"/>, 路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer
    /// </summary>
    public class HBoxContainer : UiNode<TileSetEditorPanel, Godot.HBoxContainer, HBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.LeftRoot
        /// </summary>
        public LeftRoot L_LeftRoot
        {
            get
            {
                if (_L_LeftRoot == null) _L_LeftRoot = new LeftRoot(UiPanel, Instance.GetNode<Godot.Panel>("LeftRoot"));
                return _L_LeftRoot;
            }
        }
        private LeftRoot _L_LeftRoot;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.RightRoot
        /// </summary>
        public RightRoot L_RightRoot
        {
            get
            {
                if (_L_RightRoot == null) _L_RightRoot = new RightRoot(UiPanel, Instance.GetNode<Godot.Panel>("RightRoot"));
                return _L_RightRoot;
            }
        }
        private RightRoot _L_RightRoot;

        public HBoxContainer(TileSetEditorPanel uiPanel, Godot.HBoxContainer node) : base(uiPanel, node) {  }
        public override HBoxContainer Clone() => new (UiPanel, (Godot.HBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: TileSetEditor.Bg.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<TileSetEditorPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.Head
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: TileSetEditor.Bg.HBoxContainer
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

        public VBoxContainer(TileSetEditorPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: TileSetEditor.Bg
    /// </summary>
    public class Bg : UiNode<TileSetEditorPanel, Godot.Panel, Bg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: TileSetEditor.VBoxContainer
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

        public Bg(TileSetEditorPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Head.Back
    /// </summary>
    public Back S_Back => L_Bg.L_VBoxContainer.L_Head.L_Back;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Head.Save
    /// </summary>
    public Save S_Save => L_Bg.L_VBoxContainer.L_Head.L_Save;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Head.Title
    /// </summary>
    public Title S_Title => L_Bg.L_VBoxContainer.L_Head.L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.Head
    /// </summary>
    public Head S_Head => L_Bg.L_VBoxContainer.L_Head;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.NinePatchRect"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.ScrollContainer.Tab.Select
    /// </summary>
    public Select S_Select => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_ScrollContainer.L_Tab.L_Select;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.ScrollContainer.Tab
    /// </summary>
    public Tab S_Tab => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_ScrollContainer.L_Tab;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ScrollContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer.ScrollContainer
    /// </summary>
    public ScrollContainer S_ScrollContainer => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer.L_ScrollContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot.MarginContainer
    /// </summary>
    public MarginContainer S_MarginContainer => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot.L_MarginContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.LeftRoot
    /// </summary>
    public LeftRoot S_LeftRoot => L_Bg.L_VBoxContainer.L_HBoxContainer.L_LeftRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer.RightRoot
    /// </summary>
    public RightRoot S_RightRoot => L_Bg.L_VBoxContainer.L_HBoxContainer.L_RightRoot;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.HBoxContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer.HBoxContainer
    /// </summary>
    public HBoxContainer S_HBoxContainer => L_Bg.L_VBoxContainer.L_HBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: TileSetEditor.Bg.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_Bg.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: TileSetEditor.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

}
