
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
    //提前加载列表
    private List<PreloadData> _readyList;

    private class PreloadData
    {
        /// <summary>
        /// 实例对象
        /// </summary>
        public ActivityObject ActivityObject;
        /// <summary>
        /// 所在层级
        /// </summary>
        public RoomLayerEnum Layer;

        public PreloadData(ActivityObject activityObject, RoomLayerEnum layer)
        {
            ActivityObject = activityObject;
            Layer = layer;
        }
    }

    public RoomPreinstall(RoomInfo roomInfo, RoomPreinstallInfo roomPreinstallInfo)
    {
        RoomInfo = roomInfo;
        RoomPreinstallInfo = roomPreinstallInfo;
    }

    /// <summary>
    /// 预处理操作
    /// </summary>
    public void Pretreatment(World world)
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
                    if (HandlerNormalMark(world, markInfo, mark)) continue;
                }
                else if (markInfo.SpecialMarkType == SpecialMarkType.BirthPoint) //玩家出生标记
                {
                    
                }
                else if (markInfo.SpecialMarkType == SpecialMarkType.OutPoint) //出口标记
                {
                    
                }
                else if (markInfo.SpecialMarkType == SpecialMarkType.ShopBoss) //商店老板标记
                {
                    
                }
                else if (markInfo.SpecialMarkType == SpecialMarkType.Treasure) //奖励宝箱标记
                {
                    HandlerBoxMark(world, markInfo, mark);
                }
                else
                {
                    Debug.LogError("暂未支持的类型: " + markInfo.SpecialMarkType);
                    continue;
                }

                mark.DelayTime = markInfo.DelayTime;
                mark.MarkType = markInfo.SpecialMarkType;
                //随机刷新坐标
                var pos = markInfo.Position.AsVector2();
                var birthRect = markInfo.Size.AsVector2();
                var tempPos = new Vector2(
                    world.Random.RandomRangeInt((int)(pos.X - birthRect.X / 2), (int)(pos.X + birthRect.X / 2)),
                    world.Random.RandomRangeInt((int)(pos.Y - birthRect.Y / 2), (int)(pos.Y + birthRect.Y / 2))
                );
                mark.Position = RoomInfo.ToGlobalPosition(tempPos);
                wave.Add(mark);
            }
        }
        
        //自动填充操作
        if (RoomPreinstallInfo.AutoFill)
        {
            world.RandomPool.FillAutoWave(this);
        }
        
        //排序操作
        foreach (var wave in WaveList)
        {
            wave.Sort((a, b) => (int)(a.DelayTime * 1000 - b.DelayTime * 1000));
        }
        //判断是否有敌人
        CheckHasEnemy();
    }

    private static bool HandlerNormalMark(World world, MarkInfo markInfo, ActivityMark mark)
    {
        MarkInfoItem markInfoItem;
        if (markInfo.MarkList.Count == 0)
        {
            return true;
        }
        else if (markInfo.MarkList.Count == 1)
        {
            markInfoItem = markInfo.MarkList[0];
        }
        else
        {
            var tempArray = markInfo.MarkList.Select(item => item.Weight).ToArray();
            var index = world.Random.RandomWeight(tempArray);
            markInfoItem = markInfo.MarkList[index];
        }
                    
        var activityBase = PreinstallMarkManager.GetMarkConfig(markInfoItem.Id);
        mark.Attr = markInfoItem.Attr;
        mark.VerticalSpeed = markInfoItem.VerticalSpeed;
        mark.Altitude = markInfoItem.Altitude;
                    
        if (activityBase is RandomActivityBase) //随机物体
        {
            if (markInfoItem.Id == PreinstallMarkManager.RandomWeapon.Id) //随机武器
            {
                mark.Id = world.RandomPool.GetRandomWeapon()?.Id;
                mark.ActivityType = ActivityType.Weapon;
            }
            else if (markInfoItem.Id == PreinstallMarkManager.RandomEnemy.Id) //随机敌人
            {
                mark.Id = world.RandomPool.GetRandomEnemy()?.Id;
                mark.ActivityType = ActivityType.Enemy;
            }
            else if (markInfoItem.Id == PreinstallMarkManager.RandomProp.Id) //随机道具
            {
                mark.Id = world.RandomPool.GetRandomProp()?.Id;
                mark.ActivityType = ActivityType.Prop;
            }
            else //非随机物体
            {
                Debug.LogError("未知的随机物体：" + markInfoItem.Id);
                return true;
            }
        }
        else
        {
            mark.Id = markInfoItem.Id;
            mark.ActivityType = (ActivityType)activityBase.Type;
            if (mark.ActivityType == ActivityType.Enemy) //敌人类型
            {
                mark.DerivedAttr = new Dictionary<string, string>();
                if (!mark.Attr.TryGetValue("Face", out var face) || face == "0") //随机方向
                {
                    mark.DerivedAttr.Add("Face",
                        world.Random.RandomChoose(
                            ((int)FaceDirection.Left).ToString(),
                            ((int)FaceDirection.Right).ToString()
                        )
                    );
                }
                else //指定方向
                {
                    mark.DerivedAttr.Add("Face", face);
                }
            }
        }

        return false;
    }
    
    private void HandlerBoxMark(World world, MarkInfo markInfo, ActivityMark mark)
    {
        mark.Id = ActivityObject.Ids.Id_treasure_box0001;
        mark.ActivityType = ActivityType.Treasure;
        mark.DelayTime = 0;
        mark.Altitude = 0;
        mark.Attr = new Dictionary<string, string>();
        mark.Position = RoomInfo.ToGlobalPosition(markInfo.Position.AsVector2());
    }

    private void CheckHasEnemy()
    {
        foreach (var marks in WaveList)
        {
            foreach (var activityMark in marks)
            {
                if (activityMark.ActivityType == ActivityType.Enemy)
                {
                    _hsaEnemy = true;
                    return;
                }
            }
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
        //加载提前生成的物体
        if (WaveList.Count > 0)
        {
            var activityMarks = WaveList[0];
            foreach (var activityMark in activityMarks)
            {
                if (activityMark.MarkType == SpecialMarkType.Normal || activityMark.MarkType == SpecialMarkType.Treasure)
                {
                    var activityObject = CreateItem(activityMark);
                    //初始化属性
                    InitAttr(activityObject, activityMark);
                    if (_readyList == null)
                    {
                        _readyList = new List<PreloadData>();
                    }
                    _readyList.Add(new PreloadData(activityObject, GetDefaultLayer(activityMark)));
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
        //判断房间内是否已经有敌人了
        var hasEnemy = false;
        if (_readyList != null && _readyList.Count > 0)
        {
            foreach (var preloadData in _readyList)
            {
                //有敌人
                if (!hasEnemy && preloadData.ActivityObject.CollisionWithMask(PhysicsLayer.Enemy))
                {
                    hasEnemy = true;
                }

                preloadData.ActivityObject.PutDown(preloadData.Layer);
            }

            _readyList.Clear();
            _readyList = null;
        }

        if (!hasEnemy)
        {
            hasEnemy = RoomInfo.AffiliationArea.ExistIncludeItem(
                activityObject => activityObject.CollisionWithMask(PhysicsLayer.Enemy)
            );
        }

        if (!hasEnemy) //没有敌人才能执行第1波
        {
            if (_currWaveIndex < WaveList.Count)
            {
                Debug.Log($"执行第{_currWaveIndex}波");
                _coroutineId = World.Current.StartCoroutine(RunMark(WaveList[_currWaveIndex]));
                _currWaveIndex++;
            }
        }
    }

    /// <summary>
    /// 执行下一波
    /// </summary>
    public void NextWave()
    {
        if (!IsRunWave)
        {
            return;
        }
        
        Debug.Log($"执行第{_currWaveIndex}波");
        _coroutineId = World.Current.StartCoroutine(RunMark(WaveList[_currWaveIndex]));
        _currWaveIndex++;
    }

    /// <summary>
    /// 结束生成标记
    /// </summary>
    public void OverWave()
    {
        IsRunWave = false;
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
                    //初始化属性
                    InitAttr(activityObject, activityMark);
                    //播放出生动画
                    activityObject.StartCoroutine(OnActivityObjectBirth(activityObject));
                    activityObject.PutDown(GetDefaultLayer(activityMark));
                    activityObject.UpdateFall((float)GameApplication.Instance.GetProcessDeltaTime());

                    if (activityObject is Enemy enemy)
                    {
                        //出生调用
                        enemy.OnBornFromMark();
                    }
                    
                    var effect = ObjectManager.GetPoolItem<IEffect>(ResourcePath.prefab_effect_common_Effect1_tscn);
                    var node = (Node2D)effect;
                    node.Position = activityObject.Position + new Vector2(0, -activityMark.Altitude);
                    node.AddToActivityRoot(RoomLayerEnum.YSortLayer);
                    effect.PlayEffect();
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

        instance.SetBlendSchedule(a);
        yield return new WaitForFixedProcess(10);

        while (a > 0)
        {
            instance.SetBlendSchedule(a);
            a -= 0.05f;
            yield return 0;
        }
        instance.SetBlendSchedule(0);

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
        return _coroutineId < 0 || World.Current.IsCoroutineOver(_coroutineId);
    }
    
    //创建物体
    private ActivityObject CreateItem(ActivityMark activityMark)
    {
        var activityObject = ActivityObject.Create(activityMark.Id);
        activityObject.Position = activityMark.Position;
        activityObject.VerticalSpeed = activityMark.VerticalSpeed;
        activityObject.Altitude = activityMark.Altitude;
        return activityObject;
    }

    //获取物体默认所在层级
    private RoomLayerEnum GetDefaultLayer(ActivityMark activityMark)
    {
        if (activityMark.ActivityType == ActivityType.Player || activityMark.ActivityType == ActivityType.Enemy || activityMark.ActivityType == ActivityType.Treasure)
        {
            return RoomLayerEnum.YSortLayer;
        }

        return RoomLayerEnum.NormalLayer;
    }
    
    /// <summary>
    /// 获取房间内的特殊标记
    /// </summary>
    public ActivityMark GetSpecialMark(SpecialMarkType specialMarkType)
    {
        if (WaveList.Count == 0)
        {
            return null;
        }

        var activityMarks = WaveList[0];
        var activityMark = activityMarks.FirstOrDefault(mark => mark.MarkType == specialMarkType);
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
            World.Current.StopCoroutine(_coroutineId);
        }

        WaveList.Clear();
        if (_readyList != null)
        {
            foreach (var preloadData in _readyList)
            {
                preloadData.ActivityObject.Destroy();
            }

            _readyList.Clear();
        }
    }
    
    /// <summary>
    /// 获取或创建指定波数数据
    /// </summary>
    public List<ActivityMark> GetOrCreateWave(int waveIndex)
    {
        while (WaveList.Count <= waveIndex)
        {
            WaveList.Add(new List<ActivityMark>());
        }
        
        return WaveList[waveIndex];
    }

    //初始化物体属性
    private void InitAttr(ActivityObject activityObject, ActivityMark activityMark)
    {
        if (activityMark.Attr == null)
        {
            return;
        }
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
            var role = (Role)activityObject;
            if (role.WeaponPack.Capacity > 0 && role is Enemy enemy && activityMark.Attr.TryGetValue("Weapon", out var weaponId)) //使用的武器
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

            if (activityMark.DerivedAttr != null && activityMark.DerivedAttr.TryGetValue("Face", out var face)) //脸朝向, 应该只有 -1 和 1
            {
                var faceDir = int.Parse(face);
                role.Face = (FaceDirection)faceDir;
            }
        }
    }
}