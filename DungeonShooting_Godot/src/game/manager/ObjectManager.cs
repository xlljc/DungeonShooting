
using Godot;

public static class ObjectManager
{
    /// <summary>
    /// 根据资源路径获取实例对象, 该对象必须实现 IPoolItem 接口
    /// </summary>
    /// <param name="resPath">资源路径</param>
    public static IPoolItem GetPoolItem(string resPath)
    {
        var item = ObjectPool.GetItem(resPath);
        if (item == null)
        {
            item = (IPoolItem)ResourceManager.LoadAndInstantiate<Node>(resPath);
            item.Logotype = resPath;
        }

        return item;
    }
    
    /// <summary>
    /// 根据资源路径获取实例对象, 该对象必须实现 IPoolItem 接口
    /// </summary>
    /// <param name="resPath">资源路径</param>
    public static T GetPoolItem<T>(string resPath) where T : IPoolItem
    {
        var item = ObjectPool.GetItem<T>(resPath);
        if (item == null)
        {
            item = (T)(IPoolItem)ResourceManager.LoadAndInstantiate(resPath);
            item.Logotype = resPath;
        }

        return item;
    }

    /// <summary>
    /// 根据类型直接获取实例对象
    /// </summary>
    public static T GetPoolItemByClass<T>() where T : IPoolItem, new()
    {
        var name = typeof(T).FullName;
        var item = ObjectPool.GetItem<T>(name);
        if (item == null)
        {
            item = new T();
            item.Logotype = name;
        }

        return item;
    }

    public static T GetActivityObject<T>(string id) where T : ActivityObject, IPoolItem
    {
        var item = ObjectPool.GetItem<T>(id);
        if (item == null)
        {
            item = ActivityObject.Create<T>(id);
            item.Logotype = id;
        }

        return item;
    }
    
    public static Bullet GetBullet(string id)
    {
        var bullet = ObjectPool.GetItem<Bullet>(id);
        if (bullet == null)
        {
            bullet = ActivityObject.Create<Bullet>(id);
            bullet.Logotype = id;
        }

        return bullet;
    }
    
    public static Laser GetLaser(string resPath)
    {
        var bullet = ObjectPool.GetItem<Laser>(resPath);
        if (bullet == null)
        {
            bullet = ResourceManager.LoadAndInstantiate<Laser>(resPath);
            bullet.Logotype = resPath;
        }

        return bullet;
    }
}