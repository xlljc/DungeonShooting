using System.Collections.Generic;
using Godot;

namespace UI.MapEditorMapTile;

/// <summary>
/// 自由笔刷页签
/// </summary>
public partial class FreeTileTab : EditorGridBg<MapEditorMapTile.Tab1>
{
    private ImageTexture _texture;
    private Sprite2D _sprite;
    private Control _brush;

    //图像宽度
    private int _width;
    //图像高度
    private int _height;
    private bool _leftPressed;
    private Vector2I _prevPos;
    private List<Vector2I> _selectCells = new List<Vector2I>();
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        InitNode(UiNode.L_TabRoot.Instance, UiNode.L_Grid.Instance);
        _texture = new ImageTexture();
        _sprite = UiNode.L_TabRoot.L_TileSprite.Instance;
        _sprite.Texture = _texture;
        
        _brush = UiNode.L_TabRoot.L_Brush.Instance;
        _brush.Draw += OnBrushDraw;
        
        //聚焦按钮
        UiNode.L_FocusBtn.Instance.Pressed += OnFocusClick;
    }

    protected override void Dispose(bool disposing)
    {
        _texture.Dispose();
    }

    public override void _Process(double delta)
    {
        if (Visible)
        {
            _brush.QueueRedraw();
        }
    }

    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);

        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left)
            {
                _leftPressed = mouseButton.Pressed;
                if (_leftPressed)
                {
                    //清理之前的格子
                    _selectCells.Clear();
                    UiNode.UiPanel.EditorTileMap.ClearCurrBrushAtlasCoords();
                    //当前格子
                    var atlasCoords = Utils.GetMouseCellPosition(UiNode.L_TabRoot.Instance);
                    _prevPos = atlasCoords * GameConfig.TileCellSize;
                    _selectCells.Add(_prevPos);
                    UiNode.UiPanel.EditorTileMap.AddCurrBrushAtlasCoords(atlasCoords);
                }
            }
        }
        else if (_leftPressed && @event is InputEventMouseMotion)
        {
            //多选格子
            var atlasCoords = Utils.GetMouseCellPosition(UiNode.L_TabRoot.Instance);
            var pos = atlasCoords * GameConfig.TileCellSize;
            if (pos != _prevPos)
            {
                _prevPos = pos;
                if (!_selectCells.Contains(pos))
                {
                    _selectCells.Add(pos);
                    UiNode.UiPanel.EditorTileMap.AddCurrBrushAtlasCoords(atlasCoords);
                }
            }
        }
    }

    /// <summary>
    /// 设置显示的纹理
    /// </summary>
    public void SetImage(Image image)
    {
        _texture.SetImage(image);
        var texture = UiNode.L_TabRoot.L_TileSprite.Instance.Texture;
        if (texture != null)
        {
            _width = texture.GetWidth();
            _height = texture.GetHeight();
        }
        else
        {
            _width = 0;
            _height = 0;
        }
        var root = UiNode.L_TabRoot.Instance;
        root.Size = new Vector2(_width, _height);
    }

    //聚焦按钮点击
    private void OnFocusClick()
    {
        Utils.DoFocusNode(ContainerRoot, Size, new Vector2(_width, _height));
        RefreshGridTrans();
    }

    //绘制辅助线
    private void OnBrushDraw()
    {
        var root = UiNode.L_TabRoot.Instance;
        //绘制区域
        _brush.DrawRect(
            new Rect2(Vector2.Zero, new Vector2(_width, _height)), new Color(1, 1, 0, 0.5f), false,
            2f / root.Scale.X
        );
        
        //绘制悬停的区域
        if (root.IsMouseInRect())
        {
            var position = Utils.GetMouseCellPosition(root) * GameConfig.TileCellSize;
            _brush.DrawRect(
                new Rect2(position, GameConfig.TileCellSizeVector2I),
                Colors.Green, false, 3f / root.Scale.X
            );
        }
        
        //绘制选中的格子
        foreach (var cell in _selectCells)
        {
            _brush.DrawRect(
                new Rect2(cell, GameConfig.TileCellSizeVector2I),
                Colors.White, false, 2f / root.Scale.X
            );
            _brush.DrawRect(
                new Rect2(cell, GameConfig.TileCellSizeVector2I),
                new Color(0, 1, 0, 0.1f), true
            );
        }
    }
}