using Godot;

/// <summary>
/// 游戏主管理器, 自动加载
/// </summary>
public class GameManager : Node2D
{
    public static GameManager Instance { get; private set; }
    public GameManager()
    {
        GameManager.Instance = this;
    }
}