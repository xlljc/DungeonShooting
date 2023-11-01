
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
}