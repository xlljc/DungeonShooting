
using System;
using Godot;

namespace EnemyState;

/// <summary>
/// Ai 寻找弹药, 进入该状态需要在参数中传入目标武器对象
/// </summary>
public class AiFindAmmoState : StateBase<Enemy, AIStateEnum>
{

    private Weapon _target;

    //导航目标点刷新计时器
    private float _navigationUpdateTimer = 0;
    private float _navigationInterval = 1f;

    private bool _isInTailAfterRange = false;
    private float _tailAfterTimer = 0;

    public AiFindAmmoState() : base(AIStateEnum.AiFindAmmo)
    {
    }

    public override void Enter(AIStateEnum prev, params object[] args)
    {
        if (Master.LookTarget == null)
        {
            throw new Exception("进入 AIAdvancedStateEnum.AiFindAmmo 状态时角色没有攻击目标!");
        }
        
        if (args.Length == 0)
        {
            throw new Exception("进入 AiStateEnum.AiFindAmmo 状态必须要把目标武器当成参数传过来");
        }

        SetTargetWeapon((Weapon)args[0]);
        _navigationUpdateTimer = 0;
        _isInTailAfterRange = false;
        _tailAfterTimer = 0;

        //标记武器
        _target.SetSign(SignNames.AiFindWeaponSign, Master);
    }

    public override void Process(float delta)
    {
        if (!Master.IsAllWeaponTotalAmmoEmpty()) //已经有弹药了
        {
            ChangeState(GetNextState());
            return;
        }

        //更新目标位置
        if (_navigationUpdateTimer <= 0)
        {
            //每隔一段时间秒更改目标位置
            _navigationUpdateTimer = _navigationInterval;
            var position = _target.GlobalPosition;
            Master.NavigationAgent2D.TargetPosition = position;
        }
        else
        {
            _navigationUpdateTimer -= delta;
        }

        if (_target.IsDestroyed || _target.IsTotalAmmoEmpty()) //已经被销毁, 或者弹药已经被其他角色捡走
        {
            //再去寻找其他武器
            SetTargetWeapon(Master.FindTargetWeapon());

            if (_target == null) //也没有其他可用的武器了
            {
                ChangeState(GetNextState());
            }
        }
        else if (_target.Master == Master) //已经被自己拾起
        {
            ChangeState(GetNextState());
        }
        else if (_target.Master != null) //武器已经被其他角色拾起!
        {
            //再去寻找其他武器
            SetTargetWeapon(Master.FindTargetWeapon());

            if (_target == null) //也没有其他可用的武器了
            {
                ChangeState(GetNextState());
            }
        }
        else
        {
            //检测目标没有超出跟随视野距离
            _isInTailAfterRange = Master.IsInTailAfterViewRange(Master.LookTarget.GetCenterPosition());
            if (_isInTailAfterRange)
            {
                _tailAfterTimer = 0;
            }
            else
            {
                _tailAfterTimer += delta;
            }

            //向武器移动
            if (!Master.NavigationAgent2D.IsNavigationFinished())
            {
                //移动
                Master.DoMove();
            }
            else
            {
                //站立
                Master.DoIdle();
            }
        }
    }

    private AIStateEnum GetNextState()
    {
        return _tailAfterTimer > 10 ? AIStateEnum.AiNormal : AIStateEnum.AiTailAfter;
    }

    private void SetTargetWeapon(Weapon weapon)
    {
        _target = weapon;
        //设置目标点
        if (_target != null)
        {
            Master.NavigationAgent2D.TargetPosition = _target.GlobalPosition;
        }
    }
    
    public override void DebugDraw()
    {
        if (_target != null)
        {
            Master.DrawLine(Vector2.Zero, Master.ToLocal(_target.GlobalPosition), Colors.Purple);

            if (_tailAfterTimer <= 0)
            {
                Master.DrawLine(Vector2.Zero, Master.ToLocal(Master.LookTarget.GetCenterPosition()), Colors.Orange);
            }
            else if (_tailAfterTimer <= 10)
            {
                Master.DrawLine(Vector2.Zero, Master.ToLocal(Master.LookTarget.GetCenterPosition()), Colors.Blue);
            }
            
        }
    }
}