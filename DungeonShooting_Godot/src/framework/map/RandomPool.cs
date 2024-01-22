
using Config;

public class RandomPool
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public SeedRandom Random { get; }
    
    /// <summary>
    /// 所属世界
    /// </summary>
    public World World { get; }
    
    public RandomPool(World world)
    {
        World = world;
        Random = world.Random;
    }

    /// <summary>
    /// 获取随机武器
    /// </summary>
    public ExcelConfig.ActivityBase GetRandomWeapon()
    {
        return Random.RandomChoose(PreinstallMarkManager.GetMarkConfigsByType(ActivityType.Weapon));
    }

    /// <summary>
    /// 获取随机敌人
    /// </summary>
    public ExcelConfig.ActivityBase GetRandomEnemy()
    {
        return Random.RandomChoose(PreinstallMarkManager.GetMarkConfigsByType(ActivityType.Enemy));
    }

    /// <summary>
    /// 获取随机道具
    /// </summary>
    public ExcelConfig.ActivityBase GetRandomProp()
    {
        return Random.RandomChoose(PreinstallMarkManager.GetMarkConfigsByType(ActivityType.Prop));
    }
}