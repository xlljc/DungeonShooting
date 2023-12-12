using System.IO;
using Godot;

namespace UI.TileSetEditor;

public partial class TileSetEditorImportRoot : TextureRect, IUiNodeScript
{
    private TileSetEditor.ImportRoot _importRoot;
    private DragBinder _dragBinder;

    public void SetUiNode(IUiNode uiNode)
    {
        _importRoot = (TileSetEditor.ImportRoot)uiNode;
        GetTree().Root.FilesDropped += OnFilesDropped;

        _importRoot.L_ImportButton.Instance.Pressed += () =>
        {
            Debug.Log("点击了...");
        };
        _dragBinder = DragUiManager.BindDrag(_importRoot.L_ImportButton.Instance, OnDragCallback);
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

    //拖拽区域回调
    private void OnDragCallback(DragState state, Vector2 position)
    {
        Debug.Log("state: ", state);
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
            Debug.Log("导入文件: " + file);
            if (Path.GetExtension(file) != ".png")
            {
                EditorWindowManager.ShowTips("警告", "只能导入'.png'格式的文件！");
                return;
            }

            var imageTexture = ImageTexture.CreateFromImage(Image.LoadFromFile(file));
            var textureRect = _importRoot.L_Control.L_ImportPreview.Instance;
            if (textureRect.Texture != null)
            {
                textureRect.Texture.Dispose();
            }

            textureRect.Texture = imageTexture;
            
            //隐藏导入文本和icon
            _importRoot.L_ImportLabel.Instance.Visible = false;
            _importRoot.L_ImportIcon.Instance.Visible = false;
        }
        
    }
}