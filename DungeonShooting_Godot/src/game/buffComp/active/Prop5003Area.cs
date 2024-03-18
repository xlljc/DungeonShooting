
using System;
using Godot;

public partial class Prop5003Area : Area2D
{
    [Export]
    public Sprite2D CircleSprite;
    
    [Export]
    public CollisionShape2D Collision;

    public CircleShape2D CircleShape;
    public GradientTexture2D Gradient;
    
    public override void _Ready()
    {
        CircleShape = (CircleShape2D)Collision.Shape;
        Gradient = (GradientTexture2D)CircleSprite.Texture;
        SetEnable(false);
    }

    public void PlayEffect(int startRadius, int endRadius, float time)
    {
        var _tween = CreateTween();
        _tween.SetParallel();
        _tween.TweenCallback(Callable.From(() =>
        {
            SetEnable(true);
            Modulate = Colors.White;
        }));
        _tween.Chain();
        
        _tween.TweenMethod(Callable.From<int>(SetRadius), startRadius, endRadius * 0.75f, time * 0.2f);
        _tween.Chain();

        _tween.TweenInterval(time * 0.55f);
        _tween.Chain();
        
        _tween.TweenMethod(Callable.From<int>(SetRadius), endRadius * 0.75f, endRadius, time * 0.25f);
        _tween.TweenProperty(this, "modulate", new Color(1, 1, 1, 0), time * 0.25f);
        _tween.Chain();
        
        _tween.TweenCallback(Callable.From(() =>
        {
            SetEnable(false);
        }));
        _tween.Play();
    }

    private void SetRadius(int radius)
    {
        Gradient.Width = radius * 2;
        Gradient.Height = radius * 2;
        CircleShape.Radius = radius;
    }

    private void SetEnable(bool value)
    {
        Monitoring = value;
        CircleSprite.Visible = value;
    }
}