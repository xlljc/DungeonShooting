namespace Config;

public class Role
{
    /// <summary>
    /// 物体唯一id <br/>
    /// 不需要添加类型前缀
    /// </summary>
    public string Id;

    public Role Clone()
    {
        var inst = new Role();
        inst.Id = Id;
        return inst;
    }
}