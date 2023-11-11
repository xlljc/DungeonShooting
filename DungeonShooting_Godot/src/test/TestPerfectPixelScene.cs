using Godot;
using System;

public partial class TestPerfectPixelScene : Node2D
{
    public enum HandlerType
    {
        Normal,
        UseHandler
    }

    [Export]
    public Label FpsLabel;
    
    [Export]
    public Camera2D Camera2D;

    [Export]
    public float Speed = 50;

    [Export]
    public SubViewportContainer SubViewportContainer;

    [Export]
    public HandlerType Type;
    
    private ShaderMaterial _shaderMaterial;
    private Vector2 _cameraPos;

    public override void _Ready()
    {
        if (SubViewportContainer != null)
        {
            _shaderMaterial = (ShaderMaterial)SubViewportContainer.Material;
        }
    }

    public override void _Process(double delta)
    {
        FpsLabel.Text = "FPS: " + Engine.GetFramesPerSecond();
        InputManager.Update((float)delta);
        var dir = InputManager.MoveAxis;
        if (dir != Vector2.Zero)
        {
            _cameraPos += dir * Speed * (float)delta;
        }

        if (Type == HandlerType.Normal)
        {
            Camera2D.GlobalPosition = _cameraPos.Round();
        }
        else if (Type == HandlerType.UseHandler)
        {
            if (_shaderMaterial != null)
            {
                var cameraPosition = _cameraPos;
                var offset = cameraPosition.Round() - cameraPosition;
                _shaderMaterial.SetShaderParameter("offset", offset);
                Camera2D.GlobalPosition = cameraPosition.Round();
            }
        }
        //Debug.Log("CameraPos: " + cameraPosition);
    }
}
