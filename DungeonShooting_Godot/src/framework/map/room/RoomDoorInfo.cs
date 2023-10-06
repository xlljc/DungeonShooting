
using System;
using Godot;

/// <summary>
/// 房间的门
/// </summary>
public class RoomDoorInfo : IDestroy
{
    public bool IsDestroyed { get; private set; }
    
    /// <summary>
    /// 所在墙面方向
    /// </summary>
    public DoorDirection Direction;

    /// <summary>
    /// 所在的房间
    /// </summary>
    public RoomInfo RoomInfo;

    /// <summary>
    /// 连接的门
    /// </summary>
    public RoomDoorInfo ConnectDoor;

    /// <summary>
    /// 连接的房间
    /// </summary>
    public RoomInfo ConnectRoom;

    /// <summary>
    /// 原点坐标, 单位: 格
    /// </summary>
    public Vector2I OriginPosition;

    /// <summary>
    /// 与下一道门是否有交叉点
    /// </summary>
    public bool HasCross;

    /// <summary>
    /// 与下一道门的交叉点, 单位: 格
    /// </summary>
    public Vector2I Cross;

    /// <summary>
    /// 占位导航网格
    /// </summary>
    public DoorNavigationInfo Navigation;

    /// <summary>
    /// 门实例
    /// </summary>
    public RoomDoor Door;

    /// <summary>
    /// 过道的迷雾
    /// </summary>
    public FogMask FogMask;

    /// <summary>
    /// 世界坐标下的原点坐标, 单位: 像素
    /// </summary>
    public Vector2I GetWorldOriginPosition()
    {
        return new Vector2I(
            OriginPosition.X * GameConfig.TileCellSize,
            OriginPosition.Y * GameConfig.TileCellSize
        );
    }

    /// <summary>
    /// 获取直连门过道区域数据, 单位: 格, 如果当前门连接区域带交叉点, 则报错
    /// </summary>
    public Rect2I GetAisleRect()
    {
        if (HasCross)
        {
            throw new Exception("当前门连接的过道包含交叉点, 请改用 GetCrossAisleRect() 函数!");
        }
        
        var rect = Utils.CalcRect(
            OriginPosition.X,
            OriginPosition.Y,
            ConnectDoor.OriginPosition.X,
            ConnectDoor.OriginPosition.Y
        );
    
        switch (Direction)
        {
            case DoorDirection.E:
                rect.Size = new Vector2I(rect.Size.X, GameConfig.CorridorWidth);
                break;
            case DoorDirection.W:
                rect.Size = new Vector2I(rect.Size.X, GameConfig.CorridorWidth);
                break;
                    
            case DoorDirection.S:
                rect.Size = new Vector2I(GameConfig.CorridorWidth, rect.Size.Y);
                break;
            case DoorDirection.N:
                rect.Size = new Vector2I(GameConfig.CorridorWidth, rect.Size.Y);
                break;
        }

        return rect;
    }

    /// <summary>
    /// 获取交叉门过道区域数据, 单位: 格, 如果当前门连接区域不带交叉点, 则报错
    /// </summary>
    public CrossAisleRectData GetCrossAisleRect()
    {
        if (!HasCross)
        {
            throw new Exception("当前门连接的过道不包含交叉点, 请改用 GetAisleRect() 函数!");
        }

        Rect2I rect;
        Rect2I rect2;

        //计算范围
        switch (Direction)
        {
            case DoorDirection.E: //→
                rect = new Rect2I(
                    OriginPosition.X,
                    OriginPosition.Y,
                    Cross.X - OriginPosition.X,
                    GameConfig.CorridorWidth
                );
                break;
            case DoorDirection.W: //←
                rect = new Rect2I(
                    Cross.X + GameConfig.CorridorWidth,
                    Cross.Y,
                    OriginPosition.X - (Cross.X + GameConfig.CorridorWidth),
                    GameConfig.CorridorWidth
                );
                break;
            case DoorDirection.S: //↓
                rect = new Rect2I(
                    OriginPosition.X,
                    OriginPosition.Y,
                    GameConfig.CorridorWidth,
                    Cross.Y - OriginPosition.Y
                );
                break;
            case DoorDirection.N: //↑
                rect = new Rect2I(
                    Cross.X,
                    Cross.Y + GameConfig.CorridorWidth,
                    GameConfig.CorridorWidth,
                    OriginPosition.Y - (Cross.Y + GameConfig.CorridorWidth)
                );
                break;
            default:
                rect = new Rect2I();
                break;
        }

        switch (ConnectDoor.Direction)
        {
            case DoorDirection.E: //→
                rect2 = new Rect2I(
                    ConnectDoor.OriginPosition.X,
                    ConnectDoor.OriginPosition.Y,
                    Cross.X - ConnectDoor.OriginPosition.X,
                    GameConfig.CorridorWidth
                );
                break;
            case DoorDirection.W: //←
                rect2 = new Rect2I(
                    Cross.X + GameConfig.CorridorWidth,
                    Cross.Y,
                    ConnectDoor.OriginPosition.X -
                    (Cross.X + GameConfig.CorridorWidth),
                    GameConfig.CorridorWidth
                );
                break;
            case DoorDirection.S: //↓
                rect2 = new Rect2I(
                    ConnectDoor.OriginPosition.X,
                    ConnectDoor.OriginPosition.Y,
                    GameConfig.CorridorWidth,
                    Cross.Y - ConnectDoor.OriginPosition.Y
                );
                break;
            case DoorDirection.N: //↑
                rect2 = new Rect2I(
                    Cross.X,
                    Cross.Y + GameConfig.CorridorWidth,
                    GameConfig.CorridorWidth,
                    ConnectDoor.OriginPosition.Y -
                    (Cross.Y + GameConfig.CorridorWidth)
                );
                break;
            default:
                rect2 = new Rect2I();
                break;
        }

        return new CrossAisleRectData()
        {
            Rect1 = rect,
            Rect2 = rect2,
            Cross = new Rect2I(Cross + Vector2I.One, new Vector2I(GameConfig.CorridorWidth - 2, GameConfig.CorridorWidth - 2))
        };
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        if (FogMask != null)
        {
            FogMask.Destroy();
        }
    }
}