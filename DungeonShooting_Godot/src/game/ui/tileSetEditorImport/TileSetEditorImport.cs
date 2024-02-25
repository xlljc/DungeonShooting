namespace UI.TileSetEditorImport;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class TileSetEditorImport : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorImport.ImportPreviewBg
    /// </summary>
    public ImportPreviewBg L_ImportPreviewBg
    {
        get
        {
            if (_L_ImportPreviewBg == null) _L_ImportPreviewBg = new ImportPreviewBg((TileSetEditorImportPanel)this, GetNode<Godot.ColorRect>("ImportPreviewBg"));
            return _L_ImportPreviewBg;
        }
    }
    private ImportPreviewBg _L_ImportPreviewBg;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorImport.ImportButton
    /// </summary>
    public ImportButton L_ImportButton
    {
        get
        {
            if (_L_ImportButton == null) _L_ImportButton = new ImportButton((TileSetEditorImportPanel)this, GetNode<Godot.Button>("ImportButton"));
            return _L_ImportButton;
        }
    }
    private ImportButton _L_ImportButton;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorImport.ImportLabel
    /// </summary>
    public ImportLabel L_ImportLabel
    {
        get
        {
            if (_L_ImportLabel == null) _L_ImportLabel = new ImportLabel((TileSetEditorImportPanel)this, GetNode<Godot.Label>("ImportLabel"));
            return _L_ImportLabel;
        }
    }
    private ImportLabel _L_ImportLabel;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorImport.ImportIcon
    /// </summary>
    public ImportIcon L_ImportIcon
    {
        get
        {
            if (_L_ImportIcon == null) _L_ImportIcon = new ImportIcon((TileSetEditorImportPanel)this, GetNode<Godot.TextureRect>("ImportIcon"));
            return _L_ImportIcon;
        }
    }
    private ImportIcon _L_ImportIcon;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorImport.Control
    /// </summary>
    public Control L_Control
    {
        get
        {
            if (_L_Control == null) _L_Control = new Control((TileSetEditorImportPanel)this, GetNode<Godot.Control>("Control"));
            return _L_Control;
        }
    }
    private Control _L_Control;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorImport.ImportColorPicker
    /// </summary>
    public ImportColorPicker L_ImportColorPicker
    {
        get
        {
            if (_L_ImportColorPicker == null) _L_ImportColorPicker = new ImportColorPicker((TileSetEditorImportPanel)this, GetNode<Godot.Button>("ImportColorPicker"));
            return _L_ImportColorPicker;
        }
    }
    private ImportColorPicker _L_ImportColorPicker;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorImport.ReimportButton
    /// </summary>
    public ReimportButton L_ReimportButton
    {
        get
        {
            if (_L_ReimportButton == null) _L_ReimportButton = new ReimportButton((TileSetEditorImportPanel)this, GetNode<Godot.Button>("ReimportButton"));
            return _L_ReimportButton;
        }
    }
    private ReimportButton _L_ReimportButton;

    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorImport.FocusBtn
    /// </summary>
    public FocusBtn L_FocusBtn
    {
        get
        {
            if (_L_FocusBtn == null) _L_FocusBtn = new FocusBtn((TileSetEditorImportPanel)this, GetNode<Godot.TextureButton>("FocusBtn"));
            return _L_FocusBtn;
        }
    }
    private FocusBtn _L_FocusBtn;


    public TileSetEditorImport() : base(nameof(TileSetEditorImport))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.ColorRect"/>, 路径: TileSetEditorImport.ImportPreviewBg
    /// </summary>
    public class ImportPreviewBg : UiNode<TileSetEditorImportPanel, Godot.ColorRect, ImportPreviewBg>
    {
        public ImportPreviewBg(TileSetEditorImportPanel uiPanel, Godot.ColorRect node) : base(uiPanel, node) {  }
        public override ImportPreviewBg Clone() => new (UiPanel, (Godot.ColorRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorImport.ImportButton
    /// </summary>
    public class ImportButton : UiNode<TileSetEditorImportPanel, Godot.Button, ImportButton>
    {
        public ImportButton(TileSetEditorImportPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ImportButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Label"/>, 路径: TileSetEditorImport.ImportLabel
    /// </summary>
    public class ImportLabel : UiNode<TileSetEditorImportPanel, Godot.Label, ImportLabel>
    {
        public ImportLabel(TileSetEditorImportPanel uiPanel, Godot.Label node) : base(uiPanel, node) {  }
        public override ImportLabel Clone() => new (UiPanel, (Godot.Label)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureRect"/>, 路径: TileSetEditorImport.ImportIcon
    /// </summary>
    public class ImportIcon : UiNode<TileSetEditorImportPanel, Godot.TextureRect, ImportIcon>
    {
        public ImportIcon(TileSetEditorImportPanel uiPanel, Godot.TextureRect node) : base(uiPanel, node) {  }
        public override ImportIcon Clone() => new (UiPanel, (Godot.TextureRect)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: TileSetEditorImport.Control.ImportPreview
    /// </summary>
    public class ImportPreview : UiNode<TileSetEditorImportPanel, Godot.Sprite2D, ImportPreview>
    {
        public ImportPreview(TileSetEditorImportPanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override ImportPreview Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Control"/>, 路径: TileSetEditorImport.Control
    /// </summary>
    public class Control : UiNode<TileSetEditorImportPanel, Godot.Control, Control>
    {
        /// <summary>
        /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorImport.ImportPreview
        /// </summary>
        public ImportPreview L_ImportPreview
        {
            get
            {
                if (_L_ImportPreview == null) _L_ImportPreview = new ImportPreview(UiPanel, Instance.GetNode<Godot.Sprite2D>("ImportPreview"));
                return _L_ImportPreview;
            }
        }
        private ImportPreview _L_ImportPreview;

        public Control(TileSetEditorImportPanel uiPanel, Godot.Control node) : base(uiPanel, node) {  }
        public override Control Clone() => new (UiPanel, (Godot.Control)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorImport.ImportColorPicker
    /// </summary>
    public class ImportColorPicker : UiNode<TileSetEditorImportPanel, Godot.Button, ImportColorPicker>
    {
        public ImportColorPicker(TileSetEditorImportPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ImportColorPicker Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.Button"/>, 路径: TileSetEditorImport.ReimportButton
    /// </summary>
    public class ReimportButton : UiNode<TileSetEditorImportPanel, Godot.Button, ReimportButton>
    {
        public ReimportButton(TileSetEditorImportPanel uiPanel, Godot.Button node) : base(uiPanel, node) {  }
        public override ReimportButton Clone() => new (UiPanel, (Godot.Button)Instance.Duplicate());
    }

    /// <summary>
    /// 类型: <see cref="Godot.TextureButton"/>, 路径: TileSetEditorImport.FocusBtn
    /// </summary>
    public class FocusBtn : UiNode<TileSetEditorImportPanel, Godot.TextureButton, FocusBtn>
    {
        public FocusBtn(TileSetEditorImportPanel uiPanel, Godot.TextureButton node) : base(uiPanel, node) {  }
        public override FocusBtn Clone() => new (UiPanel, (Godot.TextureButton)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.ColorRect"/>, 节点路径: TileSetEditorImport.ImportPreviewBg
    /// </summary>
    public ImportPreviewBg S_ImportPreviewBg => L_ImportPreviewBg;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorImport.ImportButton
    /// </summary>
    public ImportButton S_ImportButton => L_ImportButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Label"/>, 节点路径: TileSetEditorImport.ImportLabel
    /// </summary>
    public ImportLabel S_ImportLabel => L_ImportLabel;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureRect"/>, 节点路径: TileSetEditorImport.ImportIcon
    /// </summary>
    public ImportIcon S_ImportIcon => L_ImportIcon;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: TileSetEditorImport.Control.ImportPreview
    /// </summary>
    public ImportPreview S_ImportPreview => L_Control.L_ImportPreview;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Control"/>, 节点路径: TileSetEditorImport.Control
    /// </summary>
    public Control S_Control => L_Control;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorImport.ImportColorPicker
    /// </summary>
    public ImportColorPicker S_ImportColorPicker => L_ImportColorPicker;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Button"/>, 节点路径: TileSetEditorImport.ReimportButton
    /// </summary>
    public ReimportButton S_ReimportButton => L_ReimportButton;

    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.TextureButton"/>, 节点路径: TileSetEditorImport.FocusBtn
    /// </summary>
    public FocusBtn S_FocusBtn => L_FocusBtn;

}
