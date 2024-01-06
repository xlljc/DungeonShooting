using System.IO;
using Godot;

namespace UI.TileSetEditorImport;

public partial class TileSetEditorImportPanel : TileSetEditorImport
{
    //是否打开了颜色选择器
    private bool _isOpenColorPicker;

    private TileSetEditor.TileSetEditorPanel _tileSetEditor;

    public override void OnCreateUi()
    {
        _tileSetEditor = (TileSetEditor.TileSetEditorPanel)ParentUi;
        _tileSetEditor.SetBgColor(S_ImportPreviewBg.Instance.Color);
        S_ImportPreview.Instance.Texture = _tileSetEditor.Texture;
        
        GetTree().Root.FilesDropped += OnFilesDropped;
        S_ImportButton.Instance.Pressed += OnImportButtonClick;
        S_ReimportButton.Instance.Pressed += OnReimportButtonClick;
        S_ImportColorPicker.Instance.Pressed += OnColorPickerClick;
        S_FocusBtn.Instance.Pressed += OnFocusClick;
        
        S_ImportPreviewBg.Instance.Visible = false;
        S_ReimportButton.Instance.Visible = false;
        S_ImportColorPicker.Instance.Visible = false;
        S_FocusBtn.Instance.Visible = false;

        //监听TileSet纹理改变
        AddEventListener(EventEnum.OnSetTileTexture, OnSetTileTexture);
        //监听TileSet背景颜色改变
        AddEventListener(EventEnum.OnSetTileSetBgColor, OnSetTileSetBgColor);
        //监听拖拽
        S_ImportPreviewBg.Instance.AddDragListener(DragButtonEnum.Left | DragButtonEnum.Middle | DragButtonEnum.Right, OnDragCallback);
        //监听鼠标滚轮
        S_ImportPreviewBg.Instance.AddMouseWheelListener(OnMouseCallback);
    }

    public override void OnDestroyUi()
    {
        GetTree().Root.FilesDropped -= OnFilesDropped;
    }

    //TileSet纹理改变
    private void OnSetTileTexture(object arg)
    {
        //判断是否已经初始化好纹理了
        //_tileSetEditor.InitTexture
        S_ImportPreviewBg.Instance.Visible = _tileSetEditor.InitTexture;
        S_ReimportButton.Instance.Visible = _tileSetEditor.InitTexture;
        S_ImportColorPicker.Instance.Visible = _tileSetEditor.InitTexture;
        S_FocusBtn.Instance.Visible = _tileSetEditor.InitTexture;

        //隐藏导入文本和icon
        S_ImportLabel.Instance.Visible = !_tileSetEditor.InitTexture;
        S_ImportIcon.Instance.Visible = !_tileSetEditor.InitTexture;
        S_ImportButton.Instance.Visible = !_tileSetEditor.InitTexture;
    }

    //背景颜色改变
    private void OnSetTileSetBgColor(object arg)
    {
        if (arg is Color color)
        {
            S_ImportPreviewBg.Instance.Color = color;
        }
    }

    //点击导入按钮
    private void OnImportButtonClick()
    {
        if (_tileSetEditor.InitTexture)
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
                S_ImportColorPicker.Instance.GlobalPosition,
                S_ImportPreviewBg.Instance.Color,
                //设置颜色
                color =>
                {
                    _tileSetEditor.SetBgColor(color);
                },
                //关闭窗口
                () => { _isOpenColorPicker = false; }
            );
        }
    }

    //聚焦
    private void OnFocusClick()
    {
        S_ImportPreview.Instance.Position = Vector2.Zero;
    }
    
    //拖拽区域回调
    private void OnDragCallback(DragState state, Vector2 position)
    {
        var sprite2D = S_ImportPreview.Instance;
        if (state == DragState.DragMove && sprite2D.Visible)
        {
            sprite2D.Position += position;
        }
    }
    
    //鼠标滚轮
    private void OnMouseCallback(int v)
    {
        if (!_tileSetEditor.InitTexture)
        {
            return;
        }

        if (v < 0)
        {
            //缩小
            Utils.DoShrinkByMousePosition(S_Control.L_ImportPreview.Instance, 0.4f);
        }
        else
        {
            //放大
            Utils.DoMagnifyByMousePosition(S_Control.L_ImportPreview.Instance, 20);
        }
    }
    
    //拖拽文件进入区域
    private void OnFilesDropped(string[] files)
    {
        if (files.Length == 0 || !IsOpen)
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
        var tileSetSourceInfo = _tileSetEditor.TileSetSourceInfo;
        if (tileSetSourceInfo == null)
        {
            return;
        }

        Debug.Log("导入文件: " + file);
        var image = Image.LoadFromFile(file);
        tileSetSourceInfo.SourcePath = GameConfig.RoomTileSetDir + _tileSetEditor.TileSetInfo.Name + "/" + tileSetSourceInfo.Name + ".png";
        tileSetSourceInfo.SetSourceImage(image);
        _tileSetEditor.SetTextureData(image);
    }
}
