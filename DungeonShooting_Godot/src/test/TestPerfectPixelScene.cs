using Godot;
using System;

public partial class TestPerfectPixelScene : Node2D
{
	public enum HandlerType
	{
		UnHandler,
		NormalHandler,
		OffsetHandler
	}

	[Export]
	public CharacterBody2D Player;

	[Export]
	public Label FpsLabel;
	
	[Export]
	public Camera2D Camera2D;

	[Export]
	public float Speed = 50;

	[Export]
	public float CameraRecoveryScale = 5;

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
		InputManager.Update((float)delta);

		
	}

	public override void _PhysicsProcess(double delta)
	{
		FpsLabel.Text = "FPS: " + Engine.GetFramesPerSecond();
		Player.Velocity = InputManager.MoveAxis * Speed;
		Player.MoveAndSlide();

		var playerPos = Player.GlobalPosition;
		//_cameraPos = playerPos;
		_cameraPos = _cameraPos.MoveToward(playerPos, playerPos.DistanceTo(_cameraPos) * (float)delta * CameraRecoveryScale);

		if (Type == HandlerType.UnHandler)
		{
			Camera2D.GlobalPosition = _cameraPos;
		}
		else if (Type == HandlerType.NormalHandler)
		{
			Camera2D.GlobalPosition = _cameraPos.Round();
		}
		else if (Type == HandlerType.OffsetHandler)
		{
			if (_shaderMaterial != null)
			{
				var cameraPosition = _cameraPos;
				var offset = cameraPosition.Round() - cameraPosition;
				_shaderMaterial.SetShaderParameter("offset", offset);
				Camera2D.GlobalPosition = cameraPosition.Round();
			}
		}
	}
}
