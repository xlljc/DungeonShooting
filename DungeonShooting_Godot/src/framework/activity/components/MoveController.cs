
using System.Collections.Generic;
using Godot;

/// <summary>
/// 移动控制器, 物体运动由力来控制, 不同方向的力速度最终会汇总
/// </summary>
public class MoveController : Component
{
    private static long _index = 0;

    /// <summary>
    /// 物体受到的外力的集合
    /// </summary>
    private readonly List<ExternalForce> _forceList = new List<ExternalForce>();

    /// <summary>
    /// 这个速度就是物体当前物理帧移动的真实速率, 该速度由物理帧循环计算, 并不会马上更新
    /// 该速度就是 BasisVelocity + 外力总和
    /// </summary>
    public Vector2 Velocity => ActivityInstance.Velocity;

    /// <summary>
    /// 物体的基础移动速率
    /// </summary>
    public Vector2 BasisVelocity
    {
        get => _basisVelocity;
        set => _basisVelocity = value;
    }

    private Vector2 _basisVelocity = Vector2.Zero;

    /// <summary>
    /// 缩放所有外力对象的速率, 包括基础速率
    /// </summary>
    public void ScaleAllVelocity(float scale)
    {
        foreach (var externalForce in _forceList)
        {
            externalForce.Velocity *= scale;
        }

        BasisVelocity *= scale;
    }
    
    /// <summary>
    /// 缩放所有外力对象的旋转速率
    /// </summary>
    public void ScaleAllRotationSpeed(float scale)
    {
        foreach (var externalForce in _forceList)
        {
            externalForce.RotationSpeed *= scale;
        }
    }

    /// <summary>
    /// 添加外力速率, 并且平均分配给所有外力速率
    /// </summary>
    public void AddVelocity(Vector2 velocity)
    {
        if (velocity != Vector2.Zero)
        {
            var forceCount = GetForceCount();
            if (forceCount == 0)
            {
                AddForce(velocity);
            }
            else
            {
                var tempV = velocity / forceCount;
                for (var i = 0; i < _forceList.Count; i++)
                {
                    _forceList[i].Velocity += tempV;
                }
            }
        }
    }
    
    /// <summary>
    /// 添加外力旋转速率, 并且平均分配给所有外力旋转速率
    /// </summary>
    public void AddRotationSpeed(float speed)
    {
        if (speed != 0)
        {
            var forceCount = GetForceCount();
            if (forceCount > 0)
            {
                var tempS = speed / forceCount;
                for (var i = 0; i < _forceList.Count; i++)
                {
                    _forceList[i].RotationSpeed += tempS;
                }
            }
        }
    }
    
    /// <summary>
    /// 设置所有外力对象的速率
    /// </summary>
    public void SetAllVelocity(Vector2 value)
    {
        foreach (var externalForce in _forceList)
        {
            externalForce.Velocity = value;
        }

        BasisVelocity = value;
    }
    
    /// <summary>
    /// 设置所有外力对象的旋转速率
    /// </summary>
    public void SetAllRotationSpeed(float speed)
    {
        foreach (var externalForce in _forceList)
        {
            externalForce.RotationSpeed = speed;
        }
    }
    
    /// <summary>
    /// 获取所有外力对象
    /// </summary>
    public ExternalForce[] GetAllForce()
    {
        return _forceList.ToArray();
    }

    /// <summary>
    /// 获取所有外力的数量
    /// </summary>
    public int GetForceCount()
    {
        return _forceList.Count;
    }

    /// <summary>
    /// 快速创建一个速率外力, 该外力为匿名外力, 当速率变为 0 时自动销毁
    /// </summary>
    /// <param name="velocity">外力速率</param>
    /// <param name="resistance">阻力大小</param>
    public ExternalForce AddForce(Vector2 velocity, float resistance)
    {
        var force = AddForce("_anonymity_" + _index++);
        force.AutoDestroy = true;
        force.Velocity = velocity;
        force.VelocityResistance = resistance;
        return force;
    }
    
    /// <summary>
    /// 快速创建一个旋转外力, 该外力为匿名外力, 当速率变为 0 时自动销毁
    /// </summary>
    /// <param name="rotationSpeed">外力旋转速率, 弧度制</param>
    /// <param name="resistance">阻力大小</param>
    public ExternalForce AddForce(float rotationSpeed, float resistance)
    {
        var force = AddForce("_anonymity_" + _index++);
        force.AutoDestroy = true;
        force.RotationSpeed = rotationSpeed;
        force.RotationResistance = resistance;
        return force;
    }

    /// <summary>
    /// 快速创建一个外力, 该外力为匿名外力, 当速率变为 0 时自动销毁
    /// </summary>
    /// <param name="velocity">外力速率</param>
    public ExternalForce AddForce(Vector2 velocity)
    {
        var force = AddForce("_anonymity_" + _index++);
        force.Velocity = velocity;
        return force;
    }


    /// <summary>
    /// 根据名称添加一个外力, 并返回创建的外力的对象, 如果存在这个名称的外力, 移除之前的外力, 当速率变为 0 时不会自动销毁
    /// </summary>
    public ExternalForce AddForce(string name)
    {
        var f = new ExternalForce(name);
        f.AutoDestroy = false;
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

    public override void PhysicsProcess(float delta)
    {
        if (_basisVelocity == Vector2.Zero && _forceList.Count == 0)
        {
            ActivityInstance.Velocity = Vector2.Zero;
            return;
        }

        //先调用更新
        var externalForces = _forceList.ToArray();
        for (var i = 0; i < externalForces.Length; i++)
        {
            var force = externalForces[i];
            if (force.Enable)
            {
                force.PhysicsProcess(delta);
                //自动销毁
                if (CheckAutoDestroy(force))
                {
                    _forceList.Remove(force);
                    externalForces[i] = null;
                }
            }
        }

        //外力总和
        var finallyEf = new Vector2();
        //旋转速率总和
        var rotationSpeed = 0f;
        foreach (var force in externalForces)
        {
            if (force != null && force.Enable)
            {
                finallyEf += force.Velocity;
                rotationSpeed += force.RotationSpeed;
            }
        }

        //处理旋转
        if (rotationSpeed != 0)
        {
            ActivityInstance.Rotation += rotationSpeed * delta;
        }
        //衰减旋转速率
        for (var i = 0; i < _forceList.Count; i++)
        {
            var force = _forceList[i];
            if (force.RotationResistance != 0 && (force.EnableResistanceInTheAir || !ActivityInstance.IsThrowing))
            {
                force.RotationSpeed = Mathf.MoveToward(force.RotationSpeed, 0, force.RotationResistance * delta);
            }
        }

        //最终速率
        var finallyVelocity = _basisVelocity + finallyEf;
        //处理移动
        if (finallyVelocity != Vector2.Zero)
        {
            //计算移动
            ActivityInstance.Velocity = finallyVelocity;
            ActivityInstance.MoveAndSlide();
            //新速度
            var newVelocity = ActivityInstance.Velocity;
            
            if (newVelocity.X == 0f && _basisVelocity.X * finallyVelocity.X > 0)
            {
                _basisVelocity.X = 0;
            }

            if (newVelocity.Y == 0f && _basisVelocity.Y * finallyVelocity.Y > 0)
            {
                _basisVelocity.Y = 0;
            }
            
            //是否撞到物体
            var collision = ActivityInstance.GetLastSlideCollision();
            if (collision != null) //执行反弹操作
            {
                var no = collision.GetNormal().Rotated(Mathf.Pi * 0.5f);
                newVelocity = (finallyVelocity - _basisVelocity).Reflect(no);
                var length = _forceList.Count;
                var v = newVelocity / (length / ActivityInstance.BounceStrength);
                for (var i = 0; i < _forceList.Count; i++)
                {
                    _forceList[i].Velocity = v;
                }
            }
            else //没有撞到物体
            {
                //调整外力速率
                for (var i = 0; i < _forceList.Count; i++)
                {
                    var force = _forceList[i];
                    if (force.Enable)
                    {
                        var velocity = force.Velocity;
                        force.Velocity = new Vector2(
                            newVelocity.X == 0f && velocity.X * finallyVelocity.X > 0 ? 0 : velocity.X,
                            newVelocity.Y == 0f && velocity.Y * finallyVelocity.Y > 0 ? 0 : velocity.Y
                        );

                        //力速度衰减
                        if (force.VelocityResistance != 0 && (force.EnableResistanceInTheAir || !ActivityInstance.IsThrowing))
                        {
                            force.Velocity = force.Velocity.MoveToward(Vector2.Zero, force.VelocityResistance * delta);
                        }
                    }
                }
            }
        }
        else
        {
            ActivityInstance.Velocity = Vector2.Zero;
        }
    }

    //检测是否达到自动销毁的条件
    private bool CheckAutoDestroy(ExternalForce force)
    {
        return force.AutoDestroy && force.Velocity == Vector2.Zero && force.RotationSpeed == 0;
    }

    public override void DebugDraw()
    {
        //绘制力大小和方向
        
        if (ActivityInstance is Bullet) //不绘制子弹的力
        {
            return;
        }
        
        var globalRotation = GlobalRotation;
        var flag = ActivityInstance.Scale.Y < 0;
        if (flag)
        {
            ActivityInstance.DrawLine(Vector2.Zero, (BasisVelocity * new Vector2(1, -1)).Rotated(-globalRotation),
                Colors.Yellow);
        }
        else
        {
            ActivityInstance.DrawLine(Vector2.Zero, BasisVelocity.Rotated(-globalRotation), Colors.Yellow);
        }

        foreach (var force in _forceList)
        {
            if (flag)
            {
                ActivityInstance.DrawLine(Vector2.Zero, (force.Velocity * new Vector2(1, -1)).Rotated(globalRotation),
                    Colors.YellowGreen);
            }
            else
            {
                ActivityInstance.DrawLine(Vector2.Zero, force.Velocity.Rotated(-globalRotation), Colors.YellowGreen);
            }
        }
    }
}