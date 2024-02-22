using Godot;

/// <summary>
/// 地牢入口节点
/// </summary>
public partial class DungeonEntrance : Area2D
{
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            UiManager.Open_Loading();
            GameApplication.Instance.DungeonManager.ExitHall(() =>
            {
                // 验证该组是否满足生成地牢的条件
                var config = GameApplication.Instance.DungeonConfig;
                var result = DungeonManager.CheckDungeon(config.GroupName);
                if (result.HasError)
                {
                    UiManager.Destroy_Loading();
                    EditorWindowManager.ShowTips("警告", "当前组'" + config.GroupName + "'" + result.ErrorMessage + ", 不能生成地牢!");
                }
                else
                {
                    GameApplication.Instance.DungeonManager.LoadDungeon(config, () =>
                    {
                        UiManager.Destroy_Loading();
                    });
                }
            });
        }
    }
}