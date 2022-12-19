
using System.Collections.Generic;
using Godot;

/// <summary>
/// 移动控制器, 物体运动由力来控制, 不同方向的力速度最终会汇总
/// </summary>
public class MoveController : Component
{
    /// <summary>
    /// 玩家受到的外力的集合
    /// </summary>
    private readonly List<ExternalForce> _forceList = new List<ExternalForce>();

    /// <summary>
    /// 这个速度就是玩家当前物理帧移动的真实速率
    /// </summary>
    public Vector2 Velocity => _velocity;
    private Vector2 _velocity = Vector2.Zero;

    /// <summary>
    /// 根据名称添加一个外力, 并返回创建的外力的对象, 如果存在这个名称的外力, 移除之前的外力
    /// </summary>
    public ExternalForce AddForce(string name)
    {
        var f = new ExternalForce(name);
        AddForce(f);
        return f;
    }

    /// <summary>
    /// 根据对象添加一个外力力, 如果存在这个名称的外力, 移除之前的外力
    /// </summary>
    public T AddForce<T>(T force) where T : ExternalForce
    {
        RemoveForce(force.Name);
        _forceList.Add(force);
        return force;
    }

    /// <summary>
    /// 根据名称移除一个外力
    /// </summary>
    public void RemoveForce(string name)
    {
        for (var i = 0; i < _forceList.Count; i++)
        {
            if (_forceList[i].Name == name)
            {
                _forceList.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// 根据名称获取一个外力
    /// </summary>
    public ExternalForce GetForce(string name)
    {
        for (var i = 0; i < _forceList.Count; i++)
        {
            var externalForce = _forceList[i];
            if (externalForce.Name == name)
            {
                return externalForce;
            }
        }

        return null;
    }

    /// <summary>
    /// 检车是否有当前名称的外力对象
    /// </summary>
    public bool ContainsForce(string name)
    {
        for (var i = 0; i < _forceList.Count; i++)
        {
            var externalForce = _forceList[i];
            if (externalForce.Name == name)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 根据对象移除一个外力
    /// </summary>
    public void RemoveForce(ExternalForce force)
    {
        RemoveForce(force.Name);
    }

    /// <summary>
    /// 移除所有外力
    /// </summary>
    public void ClearForce()
    {
        _forceList.Clear();
    }

    /// <summary>
    /// 移除所有外力和基础力, 使物体静止
    /// </summary>
    public void Halt()
    {
        ClearForce();
        _velocity = Vector2.Zero;
    }

    public override void PhysicsProcess(float delta)
    {
        //先调用更新
        var externalForces = _forceList.ToArray();
        foreach (var fore in externalForces)
        {
            if (fore.Enable)
                fore.PhysicsProcess(delta);
        }

        //外力总和
        var finallyEf = Vector2.Zero;
        foreach (var fore in externalForces)
        {
            if (fore.Enable)
                finallyEf += fore.Velocity;
        }

        //最终速率
        var finallyVelocity = _velocity + finallyEf;

        //计算移动
        _velocity = ActivityObject.MoveAndSlide(finallyVelocity);

        //调整外力速率
        if (externalForces.Length > 0)
        {
            var scale = new Vector2();
            //x轴外力
            var efx = _velocity.x - _velocity.x;
            scale.x = finallyEf.x == 0f ? 0 : Mathf.Abs(efx / finallyEf.x);
            //y轴外力
            var efy = _velocity.y - _velocity.y;
            scale.y = finallyEf.y == 0f ? 0 : Mathf.Abs(efy / finallyEf.y);
            foreach (var force in externalForces)
            {
                if (force.Enable)
                {
                    var velocity = force.Velocity;
                    force.Velocity = new Vector2(velocity.x * scale.x, velocity.y * scale.y);
                }
            }
        }
    }
}