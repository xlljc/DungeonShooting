using Godot;
using System;

/// <summary>
/// 房间内所有物体都必须实现该接口
/// </summary>
public interface IObject
{
    float GlobalHeight { get; set; }

    Vector2 GlobalPosition { get; set; }

    CollisionComponent Collision { get; }

    void OnOtherEnter(IObject other);

    void OnOtherExit(IObject other);

    void Destroy();
}