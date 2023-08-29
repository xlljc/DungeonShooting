

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Config;
using Godot;

/// <summary>
/// 房间预设处理类
/// </summary>
public class RoomPreinstall : IDestroy
{
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// 所属房间对象
    /// </summary>
    public RoomInfo RoomInfo { get; }
    
    /// <summary>
    /// 绑定的预设数据
    /// </summary>
    public RoomPreinstallInfo RoomPreinstallInfo { get; }

    /// <summary>
    /// 总波数
    /// </summary>
    public int WaveCount => RoomPreinstallInfo.WaveList.Count;

    /// <summary>
    /// 波数和标记数据列表
    /// </summary>
    public List<List<ActivityMark>> WaveList { get; } = new List<List<ActivityMark>>();

    /// <summary>
    /// 是否正在执行生成波数操作
    /// </summary>
    public bool IsRunWave { get; private set; }

    /// <summary>
    /// 是否执行到最后一波了
    /// </summary>
    public bool IsLastWave => _currWaveIndex >= WaveList.Count;
    
    //是否运行过预处理
    private bool _runPretreatment = false;
    //当前房间是否会刷新敌人
    private bool _hsaEnemy = false;
    //当前波数索引
    private int _currWaveIndex = 0;
    //执行生成标记的协程id
    private long _coroutineId = -1;

    public RoomPreinstall(RoomInfo roomInfo, RoomPreinstallInfo roomPreinstallInfo)
    {
        RoomInfo = roomInfo;
        RoomPreinstallInfo = roomPreinstallInfo;
    }

    /// <summary>
    /// 预处理操作
    /// </summary>
    public void Pretreatment(SeedRandom random)
    {
        if (_runPretreatment)
        {
            return;
        }

        _runPretreatment = true;

        //确定房间内要生成写啥
        foreach (var markInfos in RoomPreinstallInfo.WaveList)
        {
            var wave = new List<ActivityMark>();
            WaveList.Add(wave);
            foreach (var markInfo in markInfos)
            {
                var mark = new ActivityMark();
                if (markInfo.SpecialMarkType == SpecialMarkType.Normal) //普通标记
                {
                    MarkInfoItem markInfoItem;
                    if (markInfo.MarkList.Count == 0)
                    {
                        continue;
                    }
                    else if (markInfo.MarkList.Count == 1)
                    {
                        markInfoItem = markInfo.MarkList[0];
                    }
                    else
                    {
                        var tempArray = markInfo.MarkList.Select(item => item.Weight).ToArray();
                        var index = random.RandomWeight(tempArray);
                        markInfoItem = markInfo.MarkList[index];
                    }

                    mark.Id = markInfoItem.Id;
                    mark.Attr = markInfoItem.Attr;
                    mark.VerticalSpeed = markInfoItem.VerticalSpeed;
                    mark.Altitude = markInfoItem.Altitude;
                    mark.ActivityType = (ActivityType)ExcelConfig.ActivityObject_Map[markInfoItem.Id].Type;
                    if (mark.ActivityType == ActivityType.Enemy)
                    {
                        _hsaEnemy = true;
                    }
                }
                else if (markInfo.SpecialMarkType == SpecialMarkType.BirthPoint) //玩家出生标记
                {

                }
                else
                {
                    GD.PrintErr("暂未支持的类型: " + markInfo.SpecialMarkType);
                    continue;
                }

                mark.DelayTime = markInfo.DelayTime;
                mark.MarkType = markInfo.SpecialMarkType;
                //随机刷新坐标
                var pos = markInfo.Position.AsVector2();
                var birthRect = markInfo.Size.AsVector2();
                var tempPos = new Vector2(
                    random.RandomRangeInt((int)(pos.X - birthRect.X / 2), (int)(pos.X + birthRect.X / 2)),
                    random.RandomRangeInt((int)(pos.Y - birthRect.Y / 2), (int)(pos.Y + birthRect.Y / 2))
                );
                var offset = RoomInfo.GetOffsetPosition();
                //var offset = RoomInfo.RoomSplit.RoomInfo.Position.AsVector2I();
                mark.Position = RoomInfo.GetWorldPosition() + (tempPos - offset);
                wave.Add(mark);
            }

            //排序操作
            wave.Sort((a, b) => (int)(a.DelayTime * 1000 - b.DelayTime * 1000));
        }
    }

    /// <summary>
    /// 预处理后才可以调用, 返回是否会生成敌人
    /// </summary>
    public bool HasEnemy()
    {
        return _hsaEnemy;
    }
    
    /// <summary>
    /// 地牢房间加载完成
    /// </summary>
    public void OnReady()
    {
        _currWaveIndex = 0;
        if (WaveList.Count > 0)
        {
            var activityMarks = WaveList[0];
            foreach (var activityMark in activityMarks)
            {
                if (activityMark.MarkType == SpecialMarkType.Normal)
                {
                    var activityObject = CreateItem(activityMark);
                    //初始化属性
                    InitAttr(activityObject, activityMark);
                    activityObject.PutDown(GetDefaultLayer(activityMark));
                }
            }
        }

        _currWaveIndex++;
    }

    /// <summary>
    /// 玩家进入房间, 开始执行生成物体
    /// </summary>
    public void StartWave()
    {
        if (IsRunWave)
        {
            return;
        }

        IsRunWave = true;
        _currWaveIndex = 1;
        GD.Print("执行第一波");
        _coroutineId = GameApplication.Instance.StartCoroutine(RunMark(WaveList[_currWaveIndex]));
        _currWaveIndex++;
    }

    public void NextWave()
    {
        GD.Print("执行下一波, 当前: " + _currWaveIndex);
        _coroutineId = GameApplication.Instance.StartCoroutine(RunMark(WaveList[_currWaveIndex]));
        _currWaveIndex++;
    }

    //执行实例化标记物体
    private IEnumerator RunMark(List<ActivityMark> activityMarks)
    {
        var timer = 0d;
        for (var i = 0; i < activityMarks.Count;)
        {
            var activityMark = activityMarks[i];
            if (timer >= activityMark.DelayTime)
            {
                if (activityMark.MarkType == SpecialMarkType.Normal)
                {
                    var activityObject = CreateItem(activityMark);
                    activityObject.VerticalSpeed = activityMark.VerticalSpeed;
                    activityObject.Altitude = activityMark.Altitude;
                    //初始化属性
                    InitAttr(activityObject, activityMark);
                    //播放出生动画
                    activityObject.StartCoroutine(OnActivityObjectBirth(activityObject));
                    activityObject.PutDown(GetDefaultLayer(activityMark));
                    
                    var effect1 = ResourceManager.LoadAndInstantiate<Effect1>(ResourcePath.prefab_effect_Effect1_tscn);
                    effect1.Position = activityObject.Position + new Vector2(0, -activityMark.Altitude);
                    effect1.AddToActivityRoot(RoomLayerEnum.YSortLayer);
                }
                
                i++;
            }
            else
            {
                timer += GameApplication.Instance.GetProcessDeltaTime();
                yield return 0;
            }
        }

        _coroutineId = -1;
    }

    //生成 ActivityObject 时调用, 用于出生时的动画效果
    private IEnumerator OnActivityObjectBirth(ActivityObject instance)
    {
        var a = 1.0f;
        instance.SetBlendColor(Colors.White);
        //禁用自定义行为
        instance.EnableCustomBehavior = false;
        //禁用下坠
        instance.EnableVerticalMotion = false;

        for (var i = 0; i < 10; i++)
        {
            instance.SetBlendSchedule(a);
            yield return 0;
        }

        while (a > 0)
        {
            instance.SetBlendSchedule(a);
            a -= 0.05f;
            yield return 0;
        }

        //启用自定义行为
        instance.EnableCustomBehavior = true;
        //启用下坠
        instance.EnableVerticalMotion = true;
    }

    /// <summary>
    /// 当前这一波是否执行完成
    /// </summary>
    public bool IsCurrWaveOver()
    {
        return _coroutineId < 0 || GameApplication.Instance.IsCoroutineOver(_coroutineId);
    }
    
    //创建物体
    private ActivityObject CreateItem(ActivityMark activityMark)
    {
        var activityObject = ActivityObject.Create(activityMark.Id);
        activityObject.Position = activityMark.Position;
        return activityObject;
    }

    //获取物体默认所在层级
    private RoomLayerEnum GetDefaultLayer(ActivityMark activityMark)
    {
        if (activityMark.ActivityType == ActivityType.Player || activityMark.ActivityType == ActivityType.Enemy)
        {
            return RoomLayerEnum.YSortLayer;
        }

        return RoomLayerEnum.NormalLayer;
    }
    
    /// <summary>
    /// 获取房间内的玩家生成标记
    /// </summary>
    public ActivityMark GetPlayerBirthMark()
    {
        if (WaveList.Count == 0)
        {
            return null;
        }

        var activityMarks = WaveList[0];
        var activityMark = activityMarks.FirstOrDefault(mark => mark.MarkType == SpecialMarkType.BirthPoint);
        return activityMark;
    }

    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        if (_coroutineId >= 0)
        {
            GameApplication.Instance.StopCoroutine(_coroutineId);
        }

        WaveList.Clear();
    }

    //初始化物体属性
    private void InitAttr(ActivityObject activityObject, ActivityMark activityMark)
    {
        if (activityMark.ActivityType == ActivityType.Weapon) //武器类型
        {
            var weapon = (Weapon)activityObject;
            if (activityMark.Attr.TryGetValue("CurrAmmon", out var currAmmon)) //当前弹夹弹药
            {
                weapon.SetCurrAmmo(int.Parse(currAmmon));
            }
            if (activityMark.Attr.TryGetValue("ResidueAmmo", out var residueAmmo)) //剩余弹药
            {
                weapon.SetResidueAmmo(int.Parse(residueAmmo));
            }
        }
        else if (activityMark.ActivityType == ActivityType.Enemy) //敌人类型
        {
            var enemy = (Enemy)activityObject;
            if (activityMark.Attr.TryGetValue("Weapon", out var weaponId)) //使用的武器
            {
                if (!string.IsNullOrEmpty(weaponId))
                {
                    var weapon = ActivityObject.Create<Weapon>(weaponId);
                    enemy.PickUpWeapon(weapon);
                    if (activityMark.Attr.TryGetValue("CurrAmmon", out var currAmmon)) //当前弹夹弹药
                    {
                        weapon.SetCurrAmmo(int.Parse(currAmmon));
                    }
                    if (activityMark.Attr.TryGetValue("ResidueAmmo", out var residueAmmo)) //剩余弹药
                    {
                        weapon.SetResidueAmmo(int.Parse(residueAmmo));
                    }
                }
            }
        }
    }
}