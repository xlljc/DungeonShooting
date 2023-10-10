
using System;
using System.Collections.Generic;
using Godot;

/// <summary>
/// 房间的数据描述
/// </summary>
public class RoomInfo : IDestroy
{
    public RoomInfo(int id, DungeonRoomType type, DungeonRoomSplit roomSplit)
    {
        Id = id;
        RoomType = type;
        RoomSplit = roomSplit;
    }

    /// <summary>
    /// 房间 id
    /// </summary>
    public int Id;

    /// <summary>
    /// 房间类型
    /// </summary>
    public DungeonRoomType RoomType;

    /// <summary>
    /// 层级, 也就是离初始房间间隔多少个房间
    /// </summary>
    public int Layer;
    
    /// <summary>
    /// 生成该房间使用的配置数据
    /// </summary>
    public DungeonRoomSplit RoomSplit;
    
    /// <summary>
    /// 房间大小, 单位: 格
    /// </summary>
    public Vector2I Size;

    /// <summary>
    /// 房间位置, 单位: 格
    /// </summary>
    public Vector2I Position;
    
    /// <summary>
    /// 门
    /// </summary>
    public List<RoomDoorInfo> Doors = new List<RoomDoorInfo>();

    /// <summary>
    /// 连接该房间的过道占用区域信息
    /// </summary>
    public List<Rect2I> AisleArea = new List<Rect2I>();

    /// <summary>
    /// 下一个房间
    /// </summary>
    public List<RoomInfo> Next = new List<RoomInfo>();
    
    /// <summary>
    /// 上一个房间
    /// </summary>
    public RoomInfo Prev;

    /// <summary>
    /// 当前房间使用的预设
    /// </summary>
    public RoomPreinstall RoomPreinstall;

    /// <summary>
    /// 当前房间归属区域
    /// </summary>
    public AffiliationArea AffiliationArea;

    /// <summary>
    /// 静态精灵绘制画布
    /// </summary>
    public RoomStaticImageCanvas StaticImageCanvas;

    /// <summary>
    /// 房间迷雾
    /// </summary>
    public FogMask RoomFogMask;
    
    /// <summary>
    /// 房间算上连接通道所占用的区域
    /// </summary>
    public Rect2I OuterRange { get; private set; }

    /// <summary>
    /// 是否处于闭关状态, 也就是房间门没有主动打开
    /// </summary>
    public bool IsSeclusion { get; private set; } = false;
    
    public bool IsDestroyed { get; private set; }
    private bool _openDoorFlag = true;
    
    // private bool _beReady = false;
    // private bool _waveStart = false;
    // private int _currWaveIndex = 0;
    // private int _currWaveNumber = 0;
    //private List<ActivityMark> _currActivityMarks = new List<ActivityMark>();

    /// <summary>
    /// 重新计算占用的区域
    /// </summary>
    public void CalcOuterRange()
    {
        var worldPos = GetWorldPosition();
        var pos = new Vector2I(worldPos.X, worldPos.Y);
        var minX = pos.X;
        var minY = pos.Y;
        var maxX = minX + GetWidth();
        var maxY = minY + GetHeight();

        //遍历每一个连接的门, 计算计算canvas覆盖范围
        foreach (var doorInfo in Doors)
        {
            var connectDoor = doorInfo.ConnectDoor;
            switch (connectDoor.Direction)
            {
                case DoorDirection.E:
                case DoorDirection.W:
                {
                    var (px1, py1) = connectDoor.GetWorldOriginPosition();
                    var py2 = py1 + 4 * GameConfig.TileCellSize;
                    if (px1 < minX)
                    {
                        minX = px1;
                    }
                    else if (px1 > maxX)
                    {
                        maxX = px1;
                    }

                    if (py1 < minY)
                    {
                        minY = py1;
                    }
                    else if (py1 > maxY)
                    {
                        maxY = py1;
                    }
                    
                    if (py2 < minY)
                    {
                        minY = py2;
                    }
                    else if (py2 > maxY)
                    {
                        maxY = py2;
                    }
                }
                    break;
                case DoorDirection.S:
                case DoorDirection.N:
                {
                    var (px1, py1) = connectDoor.GetWorldOriginPosition();
                    var px2 = px1 + 4 * GameConfig.TileCellSize;
                    if (px1 < minX)
                    {
                        minX = px1;
                    }
                    else if (px1 > maxX)
                    {
                        maxX = px1;
                    }

                    if (py1 < minY)
                    {
                        minY = py1;
                    }
                    else if (py1 > maxY)
                    {
                        maxY = py1;
                    }
                    
                    if (px2 < minX)
                    {
                        minX = px2;
                    }
                    else if (px2 > maxX)
                    {
                        maxX = px2;
                    }
                }
                    break;
            }
        }

        OuterRange = new Rect2I(minX, minY, maxX - minX, maxY - minY);
    }
    
    /// <summary>
    /// 获取房间的全局坐标, 单位: 像素
    /// </summary>
    public Vector2I GetWorldPosition()
    {
        return new Vector2I(
            Position.X * GameConfig.TileCellSize,
            Position.Y * GameConfig.TileCellSize
        );
    }

    /// <summary>
    /// 获取房间左上角的 Tile 距离全局坐标原点的偏移, 单位: 像素
    /// </summary>
    /// <returns></returns>
    public Vector2I GetOffsetPosition()
    {
        return RoomSplit.RoomInfo.Position.AsVector2I() * GameConfig.TileCellSize;
    }
    
    /// <summary>
    /// 获取房间横轴结束位置, 单位: 格
    /// </summary>
    public int GetHorizontalEnd()
    {
        return Position.X + Size.X;
    }

    /// <summary>
    /// 获取房间纵轴结束位置, 单位: 格
    /// </summary>
    public int GetVerticalEnd()
    {
        return Position.Y + Size.Y;
    }
    
    /// <summary>
    /// 获取房间横轴开始位置, 单位: 格
    /// </summary>
    public int GetHorizontalStart()
    {
        return Position.X;
    }

    /// <summary>
    /// 获取房间纵轴开始位置, 单位: 格
    /// </summary>
    public int GetVerticalStart()
    {
        return Position.Y;
    }

    /// <summary>
    /// 获取房间宽度, 单位: 像素
    /// </summary>
    public int GetWidth()
    {
        return Size.X * GameConfig.TileCellSize;
    }
    
    
    /// <summary>
    /// 获取房间高度, 单位: 像素
    /// </summary>
    public int GetHeight()
    {
        return Size.Y * GameConfig.TileCellSize;
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        //递归销毁下一个房间
        foreach (var nextRoom in Next)
        {
            nextRoom.Destroy();
        }
        Next.Clear();
        
        //销毁连接的门
        foreach (var roomDoorInfo in Doors)
        {
            roomDoorInfo.Destroy();
        }
        
        //销毁预设
        if (RoomPreinstall != null)
        {
            RoomPreinstall.Destroy();
            RoomPreinstall = null;
        }
        
        //销毁画布
        if (StaticImageCanvas != null)
        {
            StaticImageCanvas.Destroy();
        }

        //销毁迷雾
        if (RoomFogMask != null)
        {
            RoomFogMask.Destroy();
        }

        //销毁所属区域对象
        if (AffiliationArea != null)
        {
            AffiliationArea.Destroy();
        }
    }
    
    /// <summary>
    /// 加载地牢完成
    /// </summary>
    public void OnReady()
    {
        //提前加载波
        RoomPreinstall.OnReady();
    }
    
    /// <summary>
    /// 玩家第一次进入房间, 房间准备好了, 准备刷敌人, 并且关闭所有<br/>
    /// 当清完每一波刷新的敌人后即可开门
    /// </summary>
    public void OnFirstEnter()
    {
        if (RoomPreinstall.IsRunWave)
        {
            return;
        }
        
        //房间内有敌人, 或者会刷新敌人才会关门
        var hasEnemy = false;
        if (AffiliationArea.ExistEnterItem(activityObject => activityObject.CollisionWithMask(PhysicsLayer.Enemy))) //先判断房间里面是否有敌人
        {
            hasEnemy = true;
        }
        else if (RoomPreinstall.HasEnemy()) //在判断是否会刷出敌人
        {
            hasEnemy = true;
        }
        
        if (!hasEnemy) //没有敌人, 不关门
        {
            IsSeclusion = false;
            //执行第一波生成
            RoomPreinstall.StartWave();
            return;
        }

        //关门
        CloseDoor();
        IsSeclusion = true;

        //执行第一波生成
        RoomPreinstall.StartWave();
    }

    /// <summary>
    /// 当前房间所有敌人都被清除了
    /// </summary>
    public void OnClearRoom()
    {
        if (RoomPreinstall.IsLastWave) //所有 mark 都走完了
        {
            IsSeclusion = false;
            if (RoomPreinstall.HasEnemy())
            {
                //开门
                OpenDoor();
            }
            //所有标记执行完成
            RoomPreinstall.OverWave();
        }
        else //执行下一波
        {
            RoomPreinstall.NextWave();
        }
    }

    /// <summary>
    /// 打开所有门
    /// </summary>
    public void OpenDoor()
    {
        if (!_openDoorFlag)
        {
            _openDoorFlag = true;
            AffiliationArea.RestoreRegion();
        }
        
        foreach (var doorInfo in Doors)
        {
            doorInfo.Door.OpenDoor();
            
            //过道迷雾
            doorInfo.AisleFogArea.Monitoring = true;
            if (doorInfo.IsExplored)
            {
                doorInfo.ClearFog();
            }
        }
    }

    /// <summary>
    /// 关闭所有门
    /// </summary>
    public void CloseDoor()
    {
        if (_openDoorFlag)
        {
            _openDoorFlag = false;
            AffiliationArea.ExtendedRegion(new Vector2(GameConfig.TileCellSize * 4, GameConfig.TileCellSize * 4));
        }

        foreach (var doorInfo in Doors)
        {
            doorInfo.Door.CloseDoor();
            
            //过道迷雾
            doorInfo.AisleFogArea.Monitoring = false;
            if (doorInfo.IsExplored)
            {
                doorInfo.DarkFog();
            }
        }
    }

    /// <summary>
    /// 清除迷雾
    /// </summary>
    public void ClearFog()
    {
        if (RoomFogMask != null && Math.Abs(RoomFogMask.Color.A - 1) > 0.001f)
        {
            RoomFogMask.TransitionAlpha(1, GameConfig.FogTransitionTime);
            RefreshPreviewFog(1);
        }
    }
    
    /// <summary>
    /// 将迷雾变暗, nextRoom: 下一个房间
    /// </summary>
    public void DarkFog(RoomInfo nextRoom = null)
    {
        if (RoomFogMask != null && Math.Abs(RoomFogMask.Color.A - GameConfig.DarkFogAlpha) > 0.001f)
        {
            RoomFogMask.TransitionAlpha(GameConfig.DarkFogAlpha, GameConfig.FogTransitionTime);
            RefreshPreviewFog(GameConfig.DarkFogAlpha, nextRoom);
        }
    }

    /// <summary>
    /// 刷新房间中的预览迷雾
    /// </summary>
    /// <param name="roomFogAlpha">当前房间的迷雾透明度</param>
    public void RefreshPreviewFog(float roomFogAlpha)
    {
        RefreshPreviewFog(roomFogAlpha, null);
    }
    
    private void RefreshPreviewFog(float roomFogAlpha, RoomInfo nextRoom)
    {
        var isToDark = Math.Abs(roomFogAlpha - GameConfig.DarkFogAlpha) < 0.001f && nextRoom != null;
        //预览迷雾
        foreach (var roomDoorInfo in Doors)
        {
            if (isToDark && roomDoorInfo.ConnectDoor.RoomInfo != nextRoom && roomDoorInfo.AisleFogMask.IsExplored)
            {
                //roomDoorInfo.AisleFogMask.Color = Colors.Red;
                if (roomDoorInfo.PreviewAisleFogMask.TargetAlpha > GameConfig.DarkFogAlpha)
                {
                    roomDoorInfo.PreviewAisleFogMask.TransitionAlpha(GameConfig.DarkFogAlpha, GameConfig.FogTransitionTime);
                }
                if (roomDoorInfo.PreviewRoomFogMask.TargetAlpha > GameConfig.DarkFogAlpha)
                {
                    roomDoorInfo.PreviewRoomFogMask.TransitionAlpha(GameConfig.DarkFogAlpha, GameConfig.FogTransitionTime);
                }
                var connectDoor = roomDoorInfo.ConnectDoor;
                if (connectDoor.PreviewAisleFogMask != null && connectDoor.PreviewAisleFogMask.TargetAlpha > GameConfig.DarkFogAlpha)
                {
                    connectDoor.PreviewAisleFogMask.TransitionAlpha(GameConfig.DarkFogAlpha, GameConfig.FogTransitionTime);
                }
                if (connectDoor.PreviewRoomFogMask != null && connectDoor.PreviewRoomFogMask.TargetAlpha > GameConfig.DarkFogAlpha)
                {
                    connectDoor.PreviewRoomFogMask.TransitionAlpha(GameConfig.DarkFogAlpha, GameConfig.FogTransitionTime);
                }
            }
            else
            {
                var aisleAlpha = roomDoorInfo.AisleFogMask.TargetAlpha;
                if (aisleAlpha < 1 && roomDoorInfo.AisleFogMask.IsExplored)
                {
                    roomDoorInfo.AisleFogMask.TransitionAlpha(1, GameConfig.FogTransitionTime);
                    aisleAlpha = 1;
                }
                if ((roomFogAlpha < 1 && aisleAlpha < 1) || Math.Abs(roomFogAlpha - aisleAlpha) < 0.001f) //隐藏预览
                {
                    roomDoorInfo.PreviewRoomFogMask.TransitionToHide(GameConfig.FogTransitionTime);
                    roomDoorInfo.PreviewAisleFogMask.TransitionToHide(GameConfig.FogTransitionTime);
                }
                else if (aisleAlpha >= 1) //预览房间
                {
                    roomDoorInfo.PreviewAisleFogMask.TransitionToHide(GameConfig.FogTransitionTime);
                    roomDoorInfo.PreviewRoomFogMask.SetActive(true);

                    //播放过渡动画
                    var targetAlpha = 1 - roomFogAlpha;
                    roomDoorInfo.PreviewRoomFogMask.Color = new Color(1, 1, 1, 0);
                    roomDoorInfo.PreviewRoomFogMask.TransitionAlpha(targetAlpha, GameConfig.FogTransitionTime);
                }
                else if (roomFogAlpha >= 1) //预览过道
                {
                    roomDoorInfo.PreviewRoomFogMask.TransitionToHide(GameConfig.FogTransitionTime);
                    roomDoorInfo.PreviewAisleFogMask.SetActive(true);

                    //播放过渡动画
                    var targetAlpha = 1 - aisleAlpha;
                    roomDoorInfo.PreviewAisleFogMask.Color = new Color(1, 1, 1, 0.2f);
                    roomDoorInfo.PreviewAisleFogMask.TransitionAlpha(targetAlpha, GameConfig.FogTransitionTime);
                }
            }
        }
    }
}