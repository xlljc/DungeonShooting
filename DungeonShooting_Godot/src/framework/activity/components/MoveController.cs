
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
    public Vector2 Velocity => Master.Velocity;

    /// <summary>
    /// 物体的基础移动速率
    /// </summary>
    public Vector2 BasisVelocity
    {
        get => _basisVelocity;
        set => _basisVelocity = value;
    }

    private Vector2 _basisVelocity = Vector2.Zero;
    private float _flag = 0;

    /// <summary>
    /// 是否是静止状态
    /// </summary>
    public bool IsMotionless()
    {
        var v = Velocity;
        foreach (var externalForce in _forceList)
        {
            v += externalForce.Velocity;
        }

        return v == Vector2.Zero;
    }

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
        force.AutoDestroy = true;
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
        force.MoveController = this;
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
                _forceList[i].MoveController = null;
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
        foreach (var force in _forceList)
        {
            force.MoveController = null;
        }

        _forceList.Clear();
    }

    public override void PhysicsProcess(float delta)
    {
        if (_basisVelocity == Vector2.Zero && _forceList.Count == 0)
        {
            Master.Velocity = Vector2.Zero;
            return;
        }

        //外力总和
        var finallyEf = new Vector2();
        //旋转速率总和
        var rotationSpeed = 0f;
        
        //先调用更新
        if (_forceList.Count > 0)
        {
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
                        force.MoveController = null;
                        _forceList.Remove(force);
                        externalForces[i] = null;
                    }
                    else
                    {
                        finallyEf += force.Velocity;
                        rotationSpeed += force.RotationSpeed;
                    }
                }
            }
        }

        //处理旋转
        if (rotationSpeed != 0)
        {
            Rotation += rotationSpeed * delta;
        }
        var friction = !Master.IsThrowing && Master.Altitude <= 0 ? Master.GetCurrentFriction() : 0;
        var rotationFriction = !Master.IsThrowing && Master.Altitude <= 0 ? Mathf.DegToRad(Master.GetCurrentRotationFriction()) : 0;
        //衰减旋转速率
        for (var i = 0; i < _forceList.Count; i++)
        {
            var force = _forceList[i];
            var num = (force.EnableResistanceInTheAir || !Master.IsThrowing) ? force.RotationResistance : 0;
            num += rotationFriction;
            if (num != 0)
            {
                force.RotationSpeed = Mathf.MoveToward(force.RotationSpeed, 0, num * delta);
            }
        }

        //最终速率
        var finallyVelocity = _basisVelocity + finallyEf;
        //处理移动
        if (finallyVelocity != Vector2.Zero)
        {
            //计算移动
            Master.Velocity = finallyVelocity;
            Master.MoveAndSlide();
            //新速度
            var newVelocity = Master.Velocity;
            
            
            // if (newVelocity.X == 0f && _basisVelocity.X * finallyVelocity.X > 0)
            // {
            //     _basisVelocity.X = 0;
            // }
            //
            // if (newVelocity.Y == 0f && _basisVelocity.Y * finallyVelocity.Y > 0)
            // {
            //     _basisVelocity.Y = 0;
            // }
            
            //是否撞到物体
            KinematicCollision2D collision;
            _flag--;
            if (_flag <= 0 && (collision = Master.GetLastSlideCollision()) != null) //执行反弹操作
            {
                //调用移动碰撞函数
                Master.OnMoveCollision(collision);
                if (Master.IsDestroyed || (Master is IPoolItem poolItem && poolItem.IsRecycled))
                {
                    return;
                }
                //2帧内不能再触发第二次碰撞检测
                _flag = 2;
                var no = collision.GetNormal().Rotated(Mathf.Pi * 0.5f);
                newVelocity = finallyEf.Reflect(no);
                var rotation = newVelocity.Angle();

                if (Master.ActivityMaterial.RotationType == 1) //跟着反弹角度
                {
                    Rotation = rotation;
                }
                else if (Master.ActivityMaterial.RotationType == 2) //跟着反弹角度, 带垂直角度
                {
                    Rotation = rotation;
                    AnimatedSprite.Rotation = new Vector2(newVelocity.X, newVelocity.Y - Master.VerticalSpeed).Angle() - rotation;
                }
                
                var length = _forceList.Count;
                if (length != 0)
                {
                    var v = newVelocity / (length / Master.ActivityMaterial.BounceStrength);
                    for (var i = 0; i < _forceList.Count; i++)
                    {
                        _forceList[i].Velocity = v;
                    }
                }

                //调用反弹函数
                Master.OnBounce(rotation);
            }
            else //没有撞到物体
            {
                if (Master.ActivityMaterial.RotationType == 1) //跟着反弹角度
                {
                    Rotation = newVelocity.Angle();
                }
                else if (Master.ActivityMaterial.RotationType == 2) //跟着反弹角度, 带垂直角度
                {
                    var rotation = Rotation = newVelocity.Angle();
                    AnimatedSprite.Rotation = new Vector2(newVelocity.X, newVelocity.Y - Master.VerticalSpeed).Angle() - rotation;
                }
                
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
                        var num = (force.EnableResistanceInTheAir || !Master.IsThrowing) ? force.VelocityResistance : 0;
                        num += friction;
                        if (num != 0)
                        {
                            force.Velocity = force.Velocity.MoveToward(Vector2.Zero, num * delta);
                        }
                    }
                }
            }
        }
        else
        {
            Master.Velocity = Vector2.Zero;
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
        
        if (Master is Bullet) //不绘制子弹的力
        {
            return;
        }
        
        var globalRotation = GlobalRotation;
        var flag = Master.Scale.Y < 0;
        if (flag)
        {
            Master.DrawLine(Vector2.Zero, (BasisVelocity * new Vector2(1, -1)).Rotated(-globalRotation),
                Colors.Yellow);
        }
        else
        {
            Master.DrawLine(Vector2.Zero, BasisVelocity.Rotated(-globalRotation), Colors.Yellow);
        }

        foreach (var force in _forceList)
        {
            if (flag)
            {
                Master.DrawLine(Vector2.Zero, (force.Velocity * new Vector2(1, -1)).Rotated(globalRotation),
                    Colors.YellowGreen);
            }
            else
            {
                Master.DrawLine(Vector2.Zero, force.Velocity.Rotated(-globalRotation), Colors.YellowGreen);
            }
        }
    }
}