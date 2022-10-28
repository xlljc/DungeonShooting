using Godot;

//测试导航
public class TestNavigation : Node2D
{

    private Navigation2D Navigation2D;
    private Vector2[] points = new Vector2[0];

    public override void _Ready()
    {
        Navigation2D = GetNode<Navigation2D>("Position2D/Navigation2D");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton ieb) {
            if (ieb.ButtonIndex == (int)ButtonList.Left && ieb.Pressed)
            {
                points = Navigation2D.GetSimplePath(Vector2.Zero, Navigation2D.ToLocal(ieb.Position));
                Update();
                string str = "";
                foreach (var item in points)
                {
                    str += item;
                }
                GD.Print("路径: " + points.Length + ", " + str);
            }
        }
    }

    public override void _Draw()
    {
        if (points.Length >= 2)
        {
            GD.Print("绘制线段...");
            DrawPolyline(points, Colors.Red);
            // DrawMultiline(points, Colors.Red);
        }
    }
}