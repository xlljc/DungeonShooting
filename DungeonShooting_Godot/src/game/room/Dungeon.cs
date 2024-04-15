
using Godot;

/// <summary>
/// 地牢类
/// </summary>
public partial class Dungeon : World
{
	public override void _Ready()
	{
		base._Ready();
		Color = Colors.Black;
	}

	/// <summary>
	/// 初始化 TileMap 中的层级
	/// </summary>
	public void InitLayer()
	{
		MapLayerManager.InitMapLayer(TileRoot);
	}
}
