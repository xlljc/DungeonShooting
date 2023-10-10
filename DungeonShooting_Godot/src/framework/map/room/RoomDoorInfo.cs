
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
    /// 是否是正向的门
    /// </summary>
    public bool IsForward;

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
    public FogMask AisleFogMask;

    /// <summary>
    /// 过道迷雾区域
    /// </summary>
    public AisleFogArea AisleFogArea;

    /// <summary>
    /// 门区域预览房间迷雾
    /// </summary>
    public PreviewFogMask PreviewRoomFogMask;
    
    /// <summary>
    /// 门区域预览过道迷雾
    /// </summary>
    public PreviewFogMask PreviewAisleFogMask;

    /// <summary>
    /// 当前门连接的过道是否探索过
    /// </summary>
    public bool IsExplored { get; private set; } = false;

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
        if (AisleFogMask != null)
        {
            AisleFogMask.Destroy();
        }

        if (AisleFogArea != null)
        {
            AisleFogArea.Destroy();
        }

        if (PreviewRoomFogMask != null)
        {
            PreviewRoomFogMask.Destroy();
        }
        
        if (PreviewAisleFogMask != null)
        {
            PreviewAisleFogMask.Destroy();
        }
    }

    /// <summary>
    /// 清除过道迷雾
    /// </summary>
    public void ClearFog()
    {
        IsExplored = true;
        ConnectDoor.IsExplored = true;
        if (AisleFogMask != null && Math.Abs(AisleFogMask.Color.A - 1f) > 0.001f)
        {
            AisleFogMask.TransitionAlpha(1, GameConfig.FogTransitionTime);
            RefreshPreviewFog(1);
        }
    }

    /// <summary>
    /// 将过道迷雾变暗
    /// </summary>
    public void DarkFog()
    {
        IsExplored = true;
        ConnectDoor.IsExplored = true;
        if (AisleFogMask != null && Math.Abs(AisleFogMask.Color.A - GameConfig.DarkFogAlpha) > 0.001f)
        {
            AisleFogMask.TransitionAlpha(GameConfig.DarkFogAlpha, GameConfig.FogTransitionTime);
            RefreshPreviewFog(GameConfig.DarkFogAlpha);
        }
    }

    /// <summary>
    /// 刷新房间中的预览迷雾
    /// </summary>
    /// <param name="aisleFogAlpha">过道迷雾透明度</param>
    public void RefreshPreviewFog(float aisleFogAlpha)
    {
        var room1Alpha = RoomInfo.RoomFogMask.TargetAlpha;
        var room2Alpha = ConnectDoor.RoomInfo.RoomFogMask.TargetAlpha;
        //GD.Print($"aisleFogAlpha: {aisleFogAlpha}, room1Alpha: {room1Alpha}, room2Alpha{room2Alpha}");

        if ((aisleFogAlpha < 1 && room1Alpha < 1) || Math.Abs(aisleFogAlpha - room1Alpha) < 0.001f) //隐藏预览
        {
            PreviewRoomFogMask.TransitionToHide(GameConfig.FogTransitionTime);
            PreviewAisleFogMask.TransitionToHide(GameConfig.FogTransitionTime);
        }
        else if (aisleFogAlpha >= 1) //预览房间
        {
            PreviewAisleFogMask.TransitionToHide(GameConfig.FogTransitionTime);
            PreviewRoomFogMask.SetActive(true);

            //播放过渡动画
            var targetAlpha = 1 - room1Alpha;
            PreviewRoomFogMask.Color = new Color(1, 1, 1, 0);
            PreviewRoomFogMask.TransitionAlpha(targetAlpha, GameConfig.FogTransitionTime);
        }
        else if (room1Alpha >= 1) //预览过道
        {
            PreviewRoomFogMask.TransitionToHide(GameConfig.FogTransitionTime);
            PreviewAisleFogMask.SetActive(true);
        }

        if ((aisleFogAlpha < 1 && room2Alpha < 1) || Math.Abs(aisleFogAlpha - room2Alpha) < 0.001f) //隐藏预览
        {
            ConnectDoor.PreviewAisleFogMask.TransitionToHide(GameConfig.FogTransitionTime);
            ConnectDoor.PreviewRoomFogMask.TransitionToHide(GameConfig.FogTransitionTime);
        }
        else if (aisleFogAlpha >= 1) //预览房间
        {
            ConnectDoor.PreviewAisleFogMask.TransitionToHide(GameConfig.FogTransitionTime);
            ConnectDoor.PreviewRoomFogMask.SetActive(true);
            
            //播放过渡动画, 这里有问题
            var targetAlpha = 1 - room2Alpha;
            ConnectDoor.PreviewRoomFogMask.Color = new Color(1, 1, 1, 0);
            ConnectDoor.PreviewRoomFogMask.TransitionAlpha(targetAlpha, GameConfig.FogTransitionTime);
        }
        else if (room2Alpha >= 1) //预览过道
        {
            ConnectDoor.PreviewRoomFogMask.TransitionToHide(GameConfig.FogTransitionTime);
            ConnectDoor.PreviewAisleFogMask.SetActive(true);
        }
    }
}