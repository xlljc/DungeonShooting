using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

public partial class TestMask : Node2D
{
	[Export]
	public Polygon2D PolygonNode;

	public List<Vector2[]> Result = new List<Vector2[]>();
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
		//if (Input.IsActionJustPressed("fire"))
		if (Input.IsActionPressed("fire") || Input.IsActionJustPressed("roll"))
		{
			var time = DateTime.Now;
			RunTest();
			Debug.Log("用时: " + (DateTime.Now - time).TotalMilliseconds);
		}

		if (Input.IsActionJustPressed("meleeAttack"))
		{
			for (var i = 0; i < Result.Count; i++)
			{
				var temp = Result[i];
				Result[i] = StreamlineVertices3(temp, 10, Mathf.DegToRad(15));
				Debug.Log($"优化前: {temp.Length}, 优化后: {Result[i].Length}");
			}
		}

		if (Input.IsActionJustPressed("exchangeWeapon"))
		{
			Result.Clear();
		}
		QueueRedraw();
	}

	private void RunTest()
	{
		var position = GetGlobalMousePosition();
		var circle = GetCircle(position, 50, 10);
		//先检测有没有碰撞
		var flag = false;
		for (var i = 0; i < Result.Count; i++)
		{
			var p = Result[i];
			if (CollisionPolygon(p, circle))
			{
				flag = true;
				var mergePolygons = Geometry2D.MergePolygons(p, circle);
				Result.RemoveAt(i);
				for (var j = 0; j < mergePolygons.Count; j++)
				{
					Result.Add(mergePolygons[j]);
					//Result.Add(StreamlineVertices(mergePolygons[j], 50));
					//Result.Add(StreamlineVertices2(mergePolygons[j], Mathf.DegToRad(5)));
					//Result.Add(StreamlineVertices3(mergePolygons[j], 20, Mathf.DegToRad(15)));
				}
				break;
			}
		}

		if (!flag)
		{
			Result.Add(circle);
			//Result.Add(StreamlineVertices2(circle, Mathf.DegToRad(5)));
		}
	}
	
	private Vector2[] StreamlineVertices3(Vector2[] polygon, float d, float r)
	{
		if (polygon.Length <= 3)
		{
			return polygon;
		}
		
		float v = d * d;
		List<Vector2> list = new List<Vector2>();
		Vector2 tempPoint = polygon[0];
		Vector2 tempPoint2 = polygon[1];
		list.Add(tempPoint);
		float tr = tempPoint.AngleToPoint(tempPoint2);
		float delta = 0;
		for (var i = 1; i < polygon.Length; i++)
		{
			var curr = polygon[i];
			var prev = polygon[i > 0 ? i - 1 : polygon.Length - 1];
			var next = polygon[i < polygon.Length - 1 ? i + 1 : 0];
			if (prev.DistanceSquaredTo(next) > v)
			{
				//list.Add(curr);
				
				var angle = curr.AngleToPoint(next);
				delta += angle - tr;
				if (Mathf.Abs(delta) >= r)
				{
					Debug.Log(i + ", 偏差角度: " + Mathf.RadToDeg(delta));
					tr = angle;
					delta = 0;
					//var result = Geometry2D.GetClosestPointToSegmentUncapped(curr, tempPoint, tempPoint2);
					tempPoint = curr;
					//tempPoint2 = next;
					list.Add(tempPoint);
				}
			}
		}

		return list.ToArray();
	}

	private bool CollisionPolygon(Vector2[] polygon1, Vector2[] polygon2)
	{
		for (int shape = 0; shape < 2; shape++)
		{
			if (shape == 1)
			{
				Vector2[] tmp = polygon1;
				polygon1 = polygon2;
				polygon2 = tmp;
			}

			for (int a = 0; a < polygon1.Length; a++)
			{
				int b = (a + 1) % polygon1.Length;
				float axisX = -(polygon1[b].Y - polygon1[a].Y);
				float axisY = polygon1[b].X - polygon1[a].X;

				float mina = float.MaxValue;
				float maxa = float.MinValue;
				for (int p = 0; p < polygon1.Length; p++)
				{
					float t = polygon1[p].X * axisX + polygon1[p].Y * axisY;
					mina = Math.Min(mina, t);
					maxa = Math.Max(maxa, t);
				}

				float minb = float.MaxValue;
				float maxb = float.MinValue;
				for (int p = 0; p < polygon2.Length; p++)
				{
					float t = polygon2[p].X * axisX + polygon2[p].Y * axisY;
					minb = Math.Min(minb, t);
					maxb = Math.Max(maxb, t);
				}

				if (maxa < minb || mina > maxb)
					return false;
			}
		}

		return true;
	}

	private Vector2[] GetCircle(Vector2 position, float radius, int vertexCount)
    {
        Vector2[] vertices = new Vector2[vertexCount];

        for (int i = 0; i < vertexCount; i++)
        {
            float angle = i * 2.0f * (float)Math.PI / vertexCount;
            Vector2 vertex = position + new Vector2((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius);
            vertices[i] = vertex;
        }

        return vertices;
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
				foreach (var vector2 in list)
				{
					DrawCircle(vector2, 2, Colors.Green);
				}
			}
			//DrawColoredPolygon(Result[1], Colors.Red);
		}
		
	}
}
