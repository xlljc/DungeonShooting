using Godot;
using System;
using System.Linq;
using Godot.Collections;

public partial class TestMask : Node2D
{
	[Export]
	public Polygon2D PolygonNode;

	public Array<Vector2[]> Result = new Array<Vector2[]>();
	public override void _Ready()
	{
		//Geometry2D.
		// var point = new Vector2[]
		// {
		// 	new Vector2(0, 0),
		// 	new Vector2(0, 500),
		// 	new Vector2(500, 500),
		// 	new Vector2(500, 0)
		// };
		// Result = Geometry2D.ClipPolygons(point, new Vector2[]
		// {
		// 	new Vector2(20, 20),
		// 	new Vector2(20, 100),
		// 	new Vector2(90, 50),
		// });
		//PolygonNode.Polygon = clipPolygons[0];
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("fire"))
		{
			var position = GetGlobalMousePosition();
			Result.Add(new Vector2[]
			{
				new Vector2(position.X, position.Y),
				new Vector2(position.X + 50, position.Y),
				new Vector2(position.X + 50, position.Y + 50),
				new Vector2(position.X, position.Y + 50),
			});
			// Geometry2D.IsPolygonClockwise()
			// Geometry2D.MergePolygons()
		}
		QueueRedraw();
	}

	public override void _Draw()
	{
		if (Result != null)
		{
			foreach (var vector2s in Result)
			{
				var list = vector2s.ToList();
				list.Add(vector2s[0]);
				DrawPolyline(list.ToArray(), Colors.Red);
			}
			//DrawColoredPolygon(Result[1], Colors.Red);
		}
		
	}
}
