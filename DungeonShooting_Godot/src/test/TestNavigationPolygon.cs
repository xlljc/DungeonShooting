using Godot;

/// <summary>
/// 测试动态创建 NavigationPolygon
/// </summary>
public class TestNavigationPolygon : Navigation2D
{
	public override void _Ready()
	{
		var nv = GetNode<NavigationPolygonInstance>("NavigationPolygonInstance");

		var navpoy = nv.Navpoly;
		var outlines = navpoy.Outlines;
		var polygons = navpoy.Polygons;
		var vertices = navpoy.Vertices;

		var polygon = new NavigationPolygon();
		// polygon.Vertices = new Vector2[]
		// {
		// 	new Vector2(0,0), new Vector2(200,200), new Vector2(200, 0), new Vector2(0, 200),
		// 	new Vector2(50,50), new Vector2(150,150), new Vector2(150, 50), new Vector2(50, 150)
		// };
		// polygon.AddPolygon(new int[] { 0, 2, 1, 3 });
		// polygon.AddPolygon(new int[] { 4, 6, 5, 7 });

		polygon.AddOutline(new [] { new Vector2(0,0), new Vector2(200, 0), new Vector2(200,200), new Vector2(0, 200) });
		polygon.AddOutline(new [] { new Vector2(50,50), new Vector2(150, 50), new Vector2(150,150), new Vector2(50, 150) });
		polygon.MakePolygonsFromOutlines();
		
		nv.Navpoly = polygon;
	}
}