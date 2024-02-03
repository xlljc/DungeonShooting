
using Godot;

[Tool]
public partial class ColorBullet : Bullet
{
    public override void OnInit()
    {
        base.OnInit();
        SetRandomColor();
    }

    public override void OnLeavePool()
    {
        base.OnLeavePool();
        SetRandomColor();
    }

    private void SetRandomColor()
    {
        //随机颜色
        var color = new Color(Utils.Random.RandomRangeFloat(0, 1), Utils.Random.RandomRangeFloat(0, 1), Utils.Random.RandomRangeFloat(0, 1));
        color.R += 0.6f;
        color.G += 0.6f;
        color.B += 0.6f;
        Modulate = color;
    }
}