
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
    public List<Rect2> AisleArea = new List<Rect2>();

    /// <summary>
    /// 下一个房间
    /// </summary>
    public List<RoomInfo> Next = new List<RoomInfo>();
    
    /// <summary>
    /// 上一个房间
    /// </summary>
    public RoomInfo Prev;

    /// <summary>
    /// 物体生成标记
    /// </summary>
    public List<ActivityMark> ActivityMarks = new List<ActivityMark>();

    /// <summary>
    /// 当前房间归属区域
    /// </summary>
    public AffiliationArea Affiliation;

    /// <summary>
    /// 静态精灵绘制画布
    /// </summary>
    public RoomStaticSpriteCanvas StaticSpriteCanvas;

    /// <summary>
    /// 是否处于闭关状态, 也就是房间门没有主动打开
    /// </summary>
    public bool IsSeclusion { get; private set; } = false;
    
    public bool IsDestroyed { get; private set; }
    
    private bool _beReady = false;
    private bool _waveStart = false;
    private int _currWaveIndex = 0;
    private int _currWaveNumber = 0;
    private List<ActivityMark> _currActivityMarks = new List<ActivityMark>();

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
        foreach (var nextRoom in Next)
        {
            nextRoom.Destroy();
        }
        Next.Clear();
        foreach (var activityMark in ActivityMarks)
        {
            activityMark.QueueFree();
        }
        ActivityMarks.Clear();
        
        if (StaticSpriteCanvas != null)
        {
            StaticSpriteCanvas.Destroy();
        }
    }
    
    /// <summary>
    /// 房间准备好了, 准备刷敌人, 并且关闭所有门，
    /// 当清完每一波刷新的敌人后即可开门
    /// </summary>
    public void BeReady()
    {
        //没有标记, 啥都不要做
        if (ActivityMarks.Count == 0)
        {
            _beReady = true;
            IsSeclusion = false;
            return;
        }
        IsSeclusion = true;
        _waveStart = false;

        if (!_beReady)
        {
            _beReady = true;
            //按照 WaveNumber 排序
            ActivityMarks.Sort((x, y) =>
            {
                return x.WaveNumber - y.WaveNumber;
            });
        }

        //不是初始房间才能关门
        if (RoomSplit.RoomInfo.RoomType != DungeonRoomType.Inlet)
        {
            //关门
            foreach (var doorInfo in Doors)
            {
                doorInfo.Door.CloseDoor();
            }
        }
        
        //执行第一波生成
        NextWave();
    }

    /// <summary>
    /// 当前房间所有敌人都被清除了
    /// </summary>
    public void OnClearRoom()
    {
        if (_currWaveIndex >= ActivityMarks.Count) //所有 mark 都走完了
        {
            IsSeclusion = false;
            _currActivityMarks.Clear();
            //开门
            if (RoomSplit.RoomInfo.RoomType != DungeonRoomType.Inlet)
            {
                foreach (var doorInfo in Doors)
                {
                    doorInfo.Door.OpenDoor();
                }
            }
        }
        else //执行下一波
        {
            NextWave();
        }
    }

    /// <summary>
    /// 返回当前这一波所有的标记的 Doing 函数是否执行完成
    /// </summary>
    public bool IsCurrWaveOver()
    {
        for (var i = 0; i < _currActivityMarks.Count; i++)
        {
            if (!_currActivityMarks[i].IsOver())
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 执行下一轮标记
    /// </summary>
    private void NextWave()
    {
        if (!_waveStart)
        {
            _waveStart = true;
            _currWaveIndex = 0;
            _currWaveNumber = ActivityMarks[0].WaveNumber;
        }
        GD.Print("执行下一波, 当前: " + _currWaveNumber);
        
        _currActivityMarks.Clear();
        //根据标记生成对象
        for (; _currWaveIndex < ActivityMarks.Count; _currWaveIndex++)
        {
            var mark = ActivityMarks[_currWaveIndex];
            if (mark.WaveNumber != _currWaveNumber) //当前这波已经执行完成了
            {
                _currWaveNumber = mark.WaveNumber;
                break;
            }
            else //生成操作
            {
                mark.BeReady(this);
                _currActivityMarks.Add(mark);
            }
        }
    }
}