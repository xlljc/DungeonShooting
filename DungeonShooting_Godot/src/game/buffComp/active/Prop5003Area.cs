
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
        var tween = CreateTween();
        tween.SetParallel();
        tween.TweenCallback(Callable.From(() =>
        {
            SetEnable(true);
            Modulate = Colors.White;
        }));
        tween.Chain();
        
        tween.TweenMethod(Callable.From<int>(SetRadius), startRadius, endRadius * 0.75f, time * 0.2f);
        tween.Chain();

        tween.TweenInterval(time * 0.55f);
        tween.Chain();
        
        tween.TweenMethod(Callable.From<int>(SetRadius), endRadius * 0.75f, endRadius, time * 0.25f);
        tween.TweenProperty(this, "modulate", new Color(1, 1, 1, 0), time * 0.25f);
        tween.Chain();
        
        tween.TweenCallback(Callable.From(() =>
        {
            SetEnable(false);
        }));
        tween.Play();
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