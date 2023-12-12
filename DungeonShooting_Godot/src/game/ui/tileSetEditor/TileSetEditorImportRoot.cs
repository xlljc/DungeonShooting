using System.IO;
using Godot;

namespace UI.TileSetEditor;

public partial class TileSetEditorImportRoot : TextureRect, IUiNodeScript
{
    private TileSetEditor.ImportRoot _importRoot;
    private DragBinder _dragBinder;
    //是否打开了颜色选择器
    private bool _isOpenColorPicker;

    public void SetUiNode(IUiNode uiNode)
    {
        _importRoot = (TileSetEditor.ImportRoot)uiNode;
        _dragBinder = DragUiManager.BindDrag(_importRoot.L_ImportButton.Instance, OnDragCallback);
        GetTree().Root.FilesDropped += OnFilesDropped;
        _importRoot.L_ImportButton.Instance.Pressed += OnImportButtonClick;
        _importRoot.L_ReimportButton.Instance.Pressed += OnReimportButtonClick;
        _importRoot.L_ImportColorPicker.Instance.Pressed += OnColorPickerClick;
        
        _importRoot.L_ImportPreviewBg.Instance.Visible = false;
        _importRoot.L_ReimportButton.Instance.Visible = false;
    }

    public void OnDestroy()
    {
        GetTree().Root.FilesDropped -= OnFilesDropped;
        _dragBinder.UnBind();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            var textureRect = _importRoot.L_Control.L_ImportPreview.Instance;
            if (textureRect.Visible)
            {
                if (mouseButton.ButtonIndex == MouseButton.WheelDown)
                {
                    //缩小
                    var scale = textureRect.Scale;
                    scale = new Vector2(Mathf.Max(0.1f, scale.X / 1.1f), Mathf.Max(0.1f, scale.Y / 1.1f));
                    textureRect.Scale = scale;
                }
                else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
                {
                    //放大
                    var scale = textureRect.Scale;
                    scale = new Vector2(Mathf.Min(20f, scale.X * 1.1f), Mathf.Min(20f, scale.Y * 1.1f));
                    textureRect.Scale = scale;
                }
            }
        }
    }

    //点击导入按钮
    private void OnImportButtonClick()
    {
        if (_importRoot.UiPanel.Texture != null)
        {
            return;
        }

        OnReimportButtonClick();
    }

    //重新导入
    private void OnReimportButtonClick()
    {
        EditorWindowManager.ShowOpenFileDialog(["*.png"], (path) =>
        {
            if (path != null)
            {
                SetImportTexture(path);
            }
        });
    }
    
    //点击调整背景颜色
    private void OnColorPickerClick()
    {
        if (!_isOpenColorPicker)
        {
            _isOpenColorPicker = true;
            EditorWindowManager.ShowColorPicker(
                _importRoot.L_ImportColorPicker.Instance.GlobalPosition,
                _importRoot.L_ImportPreviewBg.Instance.Color,
                //设置颜色
                color => { _importRoot.L_ImportPreviewBg.Instance.Color = color; },
                //关闭窗口
                () => { _isOpenColorPicker = false; }
            );
        }
    }
    
    //拖拽区域回调
    private void OnDragCallback(DragState state, Vector2 position)
    {
        var sprite2D = _importRoot.L_Control.L_ImportPreview.Instance;
        if (state == DragState.DragMove && sprite2D.Visible)
        {
            sprite2D.Position += position;
        }
    }
    
    //拖拽文件进入区域
    private void OnFilesDropped(string[] files)
    {
        if (files.Length == 0)
        {
            return;
        }
        var flag = GetGlobalRect().HasPoint(GetGlobalMousePosition());
        if (flag)
        {
            var file = files[0];
            if (Path.GetExtension(file) != ".png")
            {
                EditorWindowManager.ShowTips("警告", "只能导入'.png'格式的文件！");
                return;
            }

            SetImportTexture(file);
        }
        
    }

    /// <summary>
    /// 导入纹理
    /// </summary>
    /// <param name="file">纹理路径</param>
    private void SetImportTexture(string file)
    {
        Debug.Log("导入文件: " + file);
        var imageTexture = ImageTexture.CreateFromImage(Image.LoadFromFile(file));
        _importRoot.UiPanel.TexturePath = file;
        _importRoot.UiPanel.Texture = imageTexture;
        
        var textureRect = _importRoot.L_Control.L_ImportPreview.Instance;
        if (textureRect.Texture != null)
        {
            textureRect.Texture.Dispose();
        }

        textureRect.Texture = imageTexture;
        _importRoot.L_ImportPreviewBg.Instance.Visible = true;
        _importRoot.L_ReimportButton.Instance.Visible = true;    
        
        //隐藏导入文本和icon
        _importRoot.L_ImportLabel.Instance.Visible = false;
        _importRoot.L_ImportIcon.Instance.Visible = false;
    }
}