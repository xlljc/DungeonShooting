using Godot;
using System;

public partial class TestMask : Node2D
{
	private Sprite2D Sprite;
	private Image _image;
	private ImageTexture _texture;

	private Image _maskImage;
	private Grid<int> _grid;
	
	public override void _Ready()
	{
		Sprite = GetNode<Sprite2D>("Sprite2D");
		var size = DisplayServer.WindowGetSize();
		_image = Image.Create(size.X, size.Y, false, Image.Format.Rgba8);
		//_image.Fill(Colors.Black);
		_texture = ImageTexture.CreateFromImage(_image);
		Sprite.Texture = _texture;
		_maskImage = ResourceManager.Load<Texture2D>(ResourcePath.icon_png).GetImage();
		//Godot.c
		//_grid.set
	}


	public override void _Process(double delta)
	{
		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
			var usedRect = _maskImage.GetUsedRect();
			_image.BlendRect(_maskImage, usedRect, GetLocalMousePosition().AsVector2I() - usedRect.Size / 2);
			_texture.Update(_image);
		}
	}
}
