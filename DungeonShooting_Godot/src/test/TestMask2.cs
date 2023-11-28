using Godot;
using System;
using System.Collections.Generic;
using Config;

public partial class TestMask2 : SubViewportContainer
{

    [Export]
    public Label Message;
    
    [Export]
    public Node2D Player;

    [Export]
    public Texture2D Brush1;
    [Export]
    public Texture2D Brush2;

    private BrushImageData _brushData1;
    private BrushImageData _brushData2;

    private Vector2I? _prevPosition = null;

    private LiquidCanvas _liquidCanvas;
    
    public override void _Ready()
    {
        Engine.MaxFps = (int)DisplayServer.ScreenGetRefreshRate();
        //Engine.MaxFps = 5;
        _brushData1 = new BrushImageData(ExcelConfig.LiquidMaterial_Map["0001"]);
        _brushData2 = new BrushImageData(ExcelConfig.LiquidMaterial_Map["0002"]);
        _liquidCanvas = new LiquidCanvas(null, (int)(Size.X / LiquidCanvas.CanvasScale), (int)(Size.X / LiquidCanvas.CanvasScale));
        GetNode("SubViewport").AddChild(_liquidCanvas);
    }

    public override void _Process(double delta)
    {
        InputManager.Update((float)delta);
       
        //玩家移动
        Player.Position += InputManager.MoveAxis * 120 * (float)delta;
        var pos = (Player.Position / LiquidCanvas.CanvasScale).AsVector2I();
        if (_prevPosition != null)
        {
            _liquidCanvas.DrawBrush(_brushData2, _prevPosition, pos, new Vector2(pos.X - _prevPosition.Value.X, pos.Y - _prevPosition.Value.Y).Angle());
        }
        else
        {
            _liquidCanvas.DrawBrush(_brushData2, _prevPosition, pos, 0);
        }
        
        //碰撞检测
        var mousePosition = (GetGlobalMousePosition() / (4 * LiquidCanvas.CanvasScale)).AsVector2I();
        Message.Text = "鼠标是否碰到毒液: " + _liquidCanvas.Collision(mousePosition.X, mousePosition.Y);
    }
}
