using System;
using Godot;

/// <summary>
/// 模拟抛出的物体, 使用时将对象挂载到该节点上即可
/// </summary>
public class ThrowNode : KinematicBody2D
{
    /// <summary>
    /// 是否已经结束
    /// </summary>
    /// <value></value>
    public bool IsOver { get; protected set; } = true;
    /// <summary>
    /// 物体大小
    /// </summary>
    public Vector2 Size { get; protected set; }
    /// <summary>
    /// 起始坐标
    /// </summary>
    public Vector2 StartPosition { get; protected set; }
    /// <summary>
    /// 移动方向, 0 - 360
    /// </summary>
    /// <value></value>
    public float Direction { get; protected set; }
    /// <summary>
    /// x速度, 也就是水平速度
    /// </summary>
    public float XSpeed { get; protected set; }
    /// <summary>
    /// y轴速度, 也就是竖直速度
    /// </summary>
    /// <value></value>
    public float YSpeed { get; protected set; }
    /// <summary>
    /// 初始x轴组队
    /// </summary>
    /// <value></value>
    public float StartXSpeed { get; protected set; }
    /// <summary>
    /// 初始y轴速度
    /// </summary>
    public float StartYSpeed { get; protected set; }
    /// <summary>
    /// 旋转速度
    /// </summary>
    public float RotateSpeed { get; protected set; }
    /// <summary>
    /// 挂载的对象
    /// </summary>
    public Node2D Mount { get; protected set; }
    /// <summary>
    /// 碰撞组件
    /// </summary>
    /// <value></value>
    public CollisionShape2D CollisionShape { get; protected set; }
    /// <summary>
    /// 绘制阴影的精灵
    /// </summary>
    public Sprite ShadowSprite { get; protected set; }

    public override void _Ready()
    {
        //只与墙壁碰撞
        CollisionMask = 1;
        CollisionLayer = 0;
        //创建碰撞器
        CollisionShape = new CollisionShape2D();
        var shape = new RectangleShape2D();
        shape.Extents = Size * 0.5f;
        CollisionShape.Shape = shape;
        AddChild(CollisionShape);


    }

    /// <summary>
    /// 初始化该抛物线对象的基础数据
    /// </summary>
    /// <param name="size">抛射的物体所占大小, 用于碰撞检测</param>
    /// <param name="start">起始点</param>
    /// <param name="startHeight">起始高度</param>
    /// <param name="direction">角度, 0 - 360</param>
    /// <param name="xSpeed">横轴速度</param>
    /// <param name="ySpeed">纵轴速度</param>
    /// <param name="rotate">旋转速度</param>
    /// <param name="mount">需要挂载的节点</param>
    /// <param name="texutre">抛射的节点显示的纹理, 用于渲染阴影用</param>
    public void InitThrow(Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate, Node2D mount)
    {
        IsOver = false;
        Size = size;
        GlobalPosition = StartPosition = start;
        Direction = direction;
        XSpeed = xSpeed;
        YSpeed = ySpeed;
        StartXSpeed = xSpeed;
        StartYSpeed = ySpeed;
        RotateSpeed = rotate;

        if (mount != null)
        {
            Mount = mount;
            AddChild(mount);
            mount.Position = new Vector2(0, -startHeight);
        }
    }

    /// <summary>
    /// 初始化该抛物线对象的基础数据, 并且渲染阴影
    /// </summary>
    /// <param name="size">抛射的物体所占大小, 用于碰撞检测</param>
    /// <param name="start">起始点</param>
    /// <param name="startHeight">起始高度</param>
    /// <param name="direction">角度, 0 - 360</param>
    /// <param name="xSpeed">横轴速度</param>
    /// <param name="ySpeed">纵轴速度</param>
    /// <param name="rotate">旋转速度</param>
    /// <param name="mount">需要挂载的节点</param>
    /// <param name="texutre">抛射的节点显示的纹理, 用于渲染阴影用</param>
    public void InitThrow(Vector2 size, Vector2 start, float startHeight, float direction, float xSpeed, float ySpeed, float rotate, Node2D mount, Texture texutre)
    {
        InitThrow(size, start, startHeight, direction, xSpeed, ySpeed, rotate, mount);
        if (texutre != null)
        {
            if (ShadowSprite == null)
            {
                //阴影
                ShadowSprite = new Sprite();
                ShadowSprite.ZIndex = -1;
                ShadowSprite.Material = ResourceManager.ShadowMaterial;
                ShadowSprite.Position = new Vector2(0, 1);
                AddChild(ShadowSprite);
            }
            ShadowSprite.Texture = texutre;
        }
        else if (ShadowSprite != null)
        {
            ShadowSprite.Texture = null;
        }
    }

    /// <summary>
    /// 初始化时调用
    /// </summary>
    protected virtual void OnInit()
    {

    }

    /// <summary>
    /// 结束的调用
    /// </summary>
    protected virtual void OnOver()
    {

    }

    public override void _Process(float delta)
    {
        if (!IsOver)
        {
            MoveAndSlide(new Vector2(XSpeed, 0).Rotated(Direction * Mathf.Pi / 180));
            Mount.Position = new Vector2(0, Mount.Position.y - YSpeed * delta);
            var rotate = Mount.GlobalRotationDegrees + RotateSpeed * delta;
            Mount.GlobalRotationDegrees = rotate;
            if (ShadowSprite != null)
            {
                ShadowSprite.GlobalRotationDegrees = rotate;
            }
            YSpeed -= GameConfig.G * delta;

            if (Mount.Position.y >= 0)
            {
                Mount.Position = new Vector2(0, 0);
                IsOver = true;
                OnOver();
            }
        }
    }

}