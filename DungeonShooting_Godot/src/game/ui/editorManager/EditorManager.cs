namespace UI.EditorManager;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class EditorManager : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: EditorManager.Bg
    /// </summary>
    public Bg L_Bg
    {
        get
        {
            if (_L_Bg == null) _L_Bg = new Bg((EditorManagerPanel)this, GetNode<Godot.Panel>("Bg"));
            return _L_Bg;
        }
    }
    private Bg _L_Bg;


    public EditorManager() : base(nameof(EditorManager))
    {
    }

    public sealed override void OnInitNestedUi()
    {

        var inst1 = L_Bg.L_VBoxContainer.L_TabContainer.L_Map;
        RecordNestedUi(inst1.L_MapEditorProject.Instance, inst1, UiManager.RecordType.Open);
        inst1.L_MapEditorProject.Instance.OnCreateUi();
        inst1.L_MapEditorProject.Instance.OnInitNestedUi();

        var inst2 = L_Bg.L_VBoxContainer.L_TabContainer.L_TileSet;
        RecordNestedUi(inst2.L_TileSetEditorProject.Instance, inst2, UiManager.RecordType.Open);
        inst2.L_TileSetEditorProject.Instance.OnCreateUi();
        inst2.L_TileSetEditorProject.Instance.OnInitNestedUi();

    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: EditorManager.Bg.VBoxContainer.Head.Back
    /// </summary>
    public class Back : UiNode<EditorManagerPanel, Godot.Button, Back>
    {
        public Back(EditorManagerPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override Back Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: EditorManager.Bg.VBoxContainer.Head.Title
    /// </summary>
    public class Title : UiNode<EditorManagerPanel, Godot.Label, Title>
    {
        public Title(EditorManagerPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override Title Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: EditorManager.Bg.VBoxContainer.Head
    /// </summary>
    public class Head : UiNode<EditorManagerPanel, Godot.Panel, Head>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorManager.Bg.VBoxContainer.Back
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorManager.Bg.VBoxContainer.Title
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

        public Head(EditorManagerPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Head Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.MapEditorProject.MapEditorProjectPanel"/>, 路径: EditorManager.Bg.VBoxContainer.TabContainer.Map.MapEditorProject
    /// </summary>
    public class MapEditorProject : UiNode<EditorManagerPanel, UI.MapEditorProject.MapEditorProjectPanel, MapEditorProject>
    {
        public MapEditorProject(EditorManagerPanel uiPanel, UI.MapEditorProject.MapEditorProjectPanel node) : base(uiPanel, node) {  }
        public override MapEditorProject Clone()
        {
            var uiNode = new MapEditorProject(UiPanel, (UI.MapEditorProject.MapEditorProjectPanel)Instance.Duplicate());
            UiPanel.RecordNestedUi(uiNode.Instance, this, UiManager.RecordType.Open);
            uiNode.Instance.OnCreateUi();
            uiNode.Instance.OnInitNestedUi();
            return uiNode;
        }
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: EditorManager.Bg.VBoxContainer.TabContainer.Map
    /// </summary>
    public class Map : UiNode<EditorManagerPanel, Godot.MarginContainer, Map>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.MapEditorProject.MapEditorProjectPanel"/>, 节点路径: EditorManager.Bg.VBoxContainer.TabContainer.MapEditorProject
        /// </summary>
        public MapEditorProject L_MapEditorProject
        {
            get
            {
                if (_L_MapEditorProject == null) _L_MapEditorProject = new MapEditorProject(UiPanel, Instance.GetNode<UI.MapEditorProject.MapEditorProjectPanel>("MapEditorProject"));
                return _L_MapEditorProject;
            }
        }
        private MapEditorProject _L_MapEditorProject;

        public Map(EditorManagerPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override Map Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="UI.TileSetEditorProject.TileSetEditorProjectPanel"/>, 路径: EditorManager.Bg.VBoxContainer.TabContainer.TileSet.TileSetEditorProject
    /// </summary>
    public class TileSetEditorProject : UiNode<EditorManagerPanel, UI.TileSetEditorProject.TileSetEditorProjectPanel, TileSetEditorProject>
    {
        public TileSetEditorProject(EditorManagerPanel uiPanel, UI.TileSetEditorProject.TileSetEditorProjectPanel node) : base(uiPanel, node) {  }
        public override TileSetEditorProject Clone()
        {
            var uiNode = new TileSetEditorProject(UiPanel, (UI.TileSetEditorProject.TileSetEditorProjectPanel)Instance.Duplicate());
            UiPanel.RecordNestedUi(uiNode.Instance, this, UiManager.RecordType.Open);
            uiNode.Instance.OnCreateUi();
            uiNode.Instance.OnInitNestedUi();
            return uiNode;
        }
    }

    /// <summary>
    /// 类型: <see cref="Godot.MarginContainer"/>, 路径: EditorManager.Bg.VBoxContainer.TabContainer.TileSet
    /// </summary>
    public class TileSet : UiNode<EditorManagerPanel, Godot.MarginContainer, TileSet>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="UI.TileSetEditorProject.TileSetEditorProjectPanel"/>, 节点路径: EditorManager.Bg.VBoxContainer.TabContainer.TileSetEditorProject
        /// </summary>
        public TileSetEditorProject L_TileSetEditorProject
        {
            get
            {
                if (_L_TileSetEditorProject == null) _L_TileSetEditorProject = new TileSetEditorProject(UiPanel, Instance.GetNode<UI.TileSetEditorProject.TileSetEditorProjectPanel>("TileSetEditorProject"));
                return _L_TileSetEditorProject;
            }
        }
        private TileSetEditorProject _L_TileSetEditorProject;

        public TileSet(EditorManagerPanel uiPanel, Godot.MarginContainer node) : base(uiPanel, node) {  }
        public override TileSet Clone() => new (UiPanel, (Godot.MarginContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TabContainer"/>, 路径: EditorManager.Bg.VBoxContainer.TabContainer
    /// </summary>
    public class TabContainer : UiNode<EditorManagerPanel, Godot.TabContainer, TabContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorManager.Bg.VBoxContainer.Map
        /// </summary>
        public Map L_Map
        {
            get
            {
                if (_L_Map == null) _L_Map = new Map(UiPanel, Instance.GetNode<Godot.MarginContainer>("Map"));
                return _L_Map;
            }
        }
        private Map _L_Map;

        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorManager.Bg.VBoxContainer.TileSet
        /// </summary>
        public TileSet L_TileSet
        {
            get
            {
                if (_L_TileSet == null) _L_TileSet = new TileSet(UiPanel, Instance.GetNode<Godot.MarginContainer>("TileSet"));
                return _L_TileSet;
            }
        }
        private TileSet _L_TileSet;

        public TabContainer(EditorManagerPanel uiPanel, Godot.TabContainer node) : base(uiPanel, node) {  }
        public override TabContainer Clone() => new (UiPanel, (Godot.TabContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.VBoxContainer"/>, 路径: EditorManager.Bg.VBoxContainer
    /// </summary>
    public class VBoxContainer : UiNode<EditorManagerPanel, Godot.VBoxContainer, VBoxContainer>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Panel"/>, 节点路径: EditorManager.Bg.Head
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
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TabContainer"/>, 节点路径: EditorManager.Bg.TabContainer
        /// </summary>
        public TabContainer L_TabContainer
        {
            get
            {
                if (_L_TabContainer == null) _L_TabContainer = new TabContainer(UiPanel, Instance.GetNode<Godot.TabContainer>("TabContainer"));
                return _L_TabContainer;
            }
        }
        private TabContainer _L_TabContainer;

        public VBoxContainer(EditorManagerPanel uiPanel, Godot.VBoxContainer node) : base(uiPanel, node) {  }
        public override VBoxContainer Clone() => new (UiPanel, (Godot.VBoxContainer)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Panel"/>, 路径: EditorManager.Bg
    /// </summary>
    public class Bg : UiNode<EditorManagerPanel, Godot.Panel, Bg>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorManager.VBoxContainer
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

        public Bg(EditorManagerPanel uiPanel, Godot.Panel node) : base(uiPanel, node) {  }
        public override Bg Clone() => new (UiPanel, (Godot.Panel)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: EditorManager.Bg.VBoxContainer.Head.Back
    /// </summary>
    public Back S_Back => L_Bg.L_VBoxContainer.L_Head.L_Back;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: EditorManager.Bg.VBoxContainer.Head.Title
    /// </summary>
    public Title S_Title => L_Bg.L_VBoxContainer.L_Head.L_Title;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: EditorManager.Bg.VBoxContainer.Head
    /// </summary>
    public Head S_Head => L_Bg.L_VBoxContainer.L_Head;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.MapEditorProject.MapEditorProjectPanel"/>, 节点路径: EditorManager.Bg.VBoxContainer.TabContainer.Map.MapEditorProject
    /// </summary>
    public MapEditorProject S_MapEditorProject => L_Bg.L_VBoxContainer.L_TabContainer.L_Map.L_MapEditorProject;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorManager.Bg.VBoxContainer.TabContainer.Map
    /// </summary>
    public Map S_Map => L_Bg.L_VBoxContainer.L_TabContainer.L_Map;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="UI.TileSetEditorProject.TileSetEditorProjectPanel"/>, 节点路径: EditorManager.Bg.VBoxContainer.TabContainer.TileSet.TileSetEditorProject
    /// </summary>
    public TileSetEditorProject S_TileSetEditorProject => L_Bg.L_VBoxContainer.L_TabContainer.L_TileSet.L_TileSetEditorProject;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.MarginContainer"/>, 节点路径: EditorManager.Bg.VBoxContainer.TabContainer.TileSet
    /// </summary>
    public TileSet S_TileSet => L_Bg.L_VBoxContainer.L_TabContainer.L_TileSet;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TabContainer"/>, 节点路径: EditorManager.Bg.VBoxContainer.TabContainer
    /// </summary>
    public TabContainer S_TabContainer => L_Bg.L_VBoxContainer.L_TabContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.VBoxContainer"/>, 节点路径: EditorManager.Bg.VBoxContainer
    /// </summary>
    public VBoxContainer S_VBoxContainer => L_Bg.L_VBoxContainer;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Panel"/>, 节点路径: EditorManager.Bg
    /// </summary>
    public Bg S_Bg => L_Bg;

}
