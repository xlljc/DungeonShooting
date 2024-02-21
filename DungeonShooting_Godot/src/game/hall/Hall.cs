
using Godot;

/// <summary>
/// 游戏大厅
/// </summary>
public partial class Hall : World
{
    /// <summary>
    /// 玩家出生标记
    /// </summary>
    [Export]
    public Marker2D BirthMark;
    
    /// <summary>
    /// 房间数据, 该数据时虚拟出来的, 并不是配置文件读取出来的
    /// </summary>
    public RoomInfo RoomInfo { get; set; }
    
    public override void _Ready()
    {
        base._Ready();
        Color = Colors.White;
    }
}