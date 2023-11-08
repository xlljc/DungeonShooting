
public static class ObjectManager
{
    public static Explode GetExplode(string resPath)
    {
        var explode = ObjectPool.GetItem<Explode>(resPath);
        if (explode == null)
        {
            explode = ResourceManager.LoadAndInstantiate<Explode>(resPath);
            explode.Logotype = resPath;
        }

        return explode;
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