
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Godot;

/// <summary>
/// 物体生成标记
/// </summary>
[Tool, GlobalClass]
public partial class ActivityMark : Node2D
{
    /// <summary>
    /// 物体类型
    /// </summary>
    [Export]
    public ActivityIdPrefix.ActivityPrefixType Type = ActivityIdPrefix.ActivityPrefixType.NonePrefix;

    /// <summary>
    /// 创建物体的表达式, 该表达式计算出的id会自动加上 Type 前缀
    /// 例如: 0001(w:100,ca:15,ra:30);0002(w:120,ca:10,ra:20)
    /// </summary>
    [Export(PropertyHint.Expression), ActivityExpression]
    public string ItemExpression;

    /// <summary>
    /// 所在层级
    /// </summary>
    [Export]
    public RoomLayerEnum Layer = RoomLayerEnum.NormalLayer;

    /// <summary>
    /// 该标记在第几波调用 BeReady，
    /// 一个房间内所以敌人清完即可进入下一波
    /// </summary>
    [Export]
    public int WaveNumber = 1;

    /// <summary>
    /// 延时执行时间，单位：秒
    /// </summary>
    [Export]
    public float DelayTime = 0;

    /// <summary>
    /// 物体会在该矩形区域内随机位置生成
    /// </summary>
    [Export]
    public Vector2I BirthRect = Vector2I.Zero;
    
    /// <summary>
    /// 绘制的颜色
    /// </summary>
    [Export]
    public Color DrawColor = new Color(1, 1, 1, 1);

    /// <summary>
    /// 物体初始海拔高度
    /// </summary>
    [ExportGroup("Vertical")]
    [Export(PropertyHint.Range, "0, 128")]
    public int Altitude = 8;

    /// <summary>
    /// 物体初始纵轴速度
    /// </summary>
    [Export(PropertyHint.Range, "-1000,1000,0.1")]
    public float VerticalSpeed = 0;

    /// <summary>
    /// 当前标记所在Tile节点
    /// </summary>
    public TileMap TileRoot;
    
    /// <summary>
    /// 随机数对象
    /// </summary>
    public SeedRandom Random { get; private set; }

    //是否已经结束
    private bool _isOver = true;
    private float _overTimer = 1;
    private float _timer = 0;
    private RoomInfo _tempRoom;

    //已经计算好要生成的物体
    private Dictionary<string, ActivityExpressionData> _currentExpression = new Dictionary<string, ActivityExpressionData>();

    //存储所有 ActivityMark 和子类中被 [ActivityExpression] 标记的字段名称
    private static Dictionary<Type, List<string>> _activityExpressionMap = new Dictionary<Type, List<string>>();

    /// <summary>
    /// 对生成的物体执行后续操作
    /// </summary>
    public virtual void Doing(ActivityObjectResult<ActivityObject> result, RoomInfo roomInfo)
    {
    }

    public ActivityMark()
    {
        //扫描所有 ActivityExpression
        var type = GetType();
        if (!_activityExpressionMap.ContainsKey(type))
        {
            // 获取类型信息
            var fieldInfos = new List<string>();
            var tempList = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var s in tempList)
            {
                if (s.GetCustomAttribute<ActivityExpressionAttribute>() != null)
                {
                    fieldInfos.Add(s.Name);
                }
            }

            _activityExpressionMap.Add(type, fieldInfos);
        }
    }
    
    public override void _Process(double delta)
    {
#if TOOLS
        if (Engine.IsEditorHint())
        {
            QueueRedraw();
            return;
        }
#endif
        if (_isOver)
        {
            _overTimer += (float)delta;
            if (_overTimer >= 1)
            {
                SetActive(false);
            }
        }
        else
        {
            if (DelayTime > 0)
            {
                _timer += (float)delta;
                if (_timer >= DelayTime)
                {
                    Doing(_tempRoom);
                    _tempRoom = null;
                    _isOver = true;
                }
            }
        }
    }

    /// <summary>
    /// 标记准备好了
    /// </summary>
    public void BeReady(RoomInfo roomInfo)
    {
        if (_currentExpression == null || Type == ActivityIdPrefix.ActivityPrefixType.Player)
        {
            return;
        }
        _isOver = false;
        _overTimer = 0;
        SetActive(true);
        if (DelayTime <= 0)
        {
            Doing(roomInfo);
            _isOver = true;
        }
        else
        {
            _timer = 0;
            _tempRoom = roomInfo;
        }
    }

    /// <summary>
    /// 是否已经结束
    /// </summary>
    public bool IsOver()
    {
        return _isOver && _overTimer >= 1;
    }

    private void Doing(RoomInfo roomInfo)
    {
        var result = CreateActivityObjectFromExpression<ActivityObject>(Type, nameof(ItemExpression));

        if (result == null || result.ActivityObject == null)
        {
            return;
        }
        
        result.ActivityObject.VerticalSpeed = VerticalSpeed;
        result.ActivityObject.Altitude = Altitude;
        var pos = Position;
        if (BirthRect != Vector2I.Zero)
        {
            result.ActivityObject.Position = new Vector2(
                Random.RandomRangeInt((int)pos.X - BirthRect.X / 2, (int)pos.X + BirthRect.X / 2),
                Random.RandomRangeInt((int)pos.Y - BirthRect.Y / 2, (int)pos.Y + BirthRect.Y / 2)
            );
        }
        else
        {
            result.ActivityObject.Position = pos;
        }

        result.ActivityObject.StartCoroutine(OnActivityObjectBirth(result.ActivityObject));
        result.ActivityObject.PutDown(Layer);
        
        var effect1 = ResourceManager.LoadAndInstantiate<Effect1>(ResourcePath.prefab_effect_Effect1_tscn);
        effect1.Position = result.ActivityObject.Position + new Vector2(0, -Altitude);
        effect1.AddToActivityRoot(RoomLayerEnum.NormalLayer);
        
        Doing(result, roomInfo);
    }

    /// <summary>
    /// 生成 ActivityObject 时调用, 用于出生时的动画效果
    /// </summary>
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

#if TOOLS
    public override void _Draw()
    {
        if (Engine.IsEditorHint() || GameApplication.Instance.Debug)
        {
            var drawColor = DrawColor;

            //如果在编辑器中选中了该节点, 则绘更改绘制颜色的透明度
            var selectedNodes = Plugin.Plugin.Instance?.GetEditorInterface()?.GetSelection()?.GetSelectedNodes();
            if (selectedNodes != null && selectedNodes.Contains(this))
            {
                drawColor.A = 1f;
            }
            else
            {
                drawColor.A = 0.5f;
            }
            
            DrawLine(new Vector2(-2, -2), new Vector2(2, 2), drawColor, 1f);
            DrawLine(new Vector2(-2, 2), new Vector2(2, -2), drawColor, 1f);

            if (BirthRect != Vector2.Zero)
            {
                DrawRect(new Rect2(-BirthRect / 2, BirthRect), drawColor, false, 0.5f);
            }
            
            DrawString(ResourceManager.DefaultFont12Px, new Vector2(-14, 12), WaveNumber.ToString(), HorizontalAlignment.Center, 28, 12);
        }
    }
#endif

    /// <summary>
    /// 设置当前节点是否是活动状态
    /// </summary>
    private void SetActive(bool flag)
    {
        // SetProcess(flag);
        // SetPhysicsProcess(flag);
        // SetProcessInput(flag);
        // Visible = flag;
        
        var parent = GetParent();
        if (flag)
        {
            if (parent == null)
            {
                TileRoot.AddChild(this);
            }
            else if (parent != TileRoot)
            {
                parent.RemoveChild(this);
                TileRoot.AddChild(this);
            }
            Owner = TileRoot;
        }
        else
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
                Owner = null;
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------

    /// <summary>
    /// 执行预处理操作
    /// </summary>
    public void Pretreatment(SeedRandom random)
    {
        Random = random;
        if (_activityExpressionMap.TryGetValue(GetType(), out var list))
        {
            foreach (var field in list)
            {
                Pretreatment(field);
            }
        }
    }

    private void Pretreatment(string field)
    {
        var expressionStr = GetType().GetField(field)?.GetValue(this) as string;
        if (string.IsNullOrEmpty(expressionStr))
        {
            _currentExpression.Add(field, new ActivityExpressionData(""));
            return;
        }
        var activityExpression = Parse(expressionStr);
        if (activityExpression.Count > 0)
        {
            //权重列表
            var list = new List<int>();
            for (var i = 0; i < activityExpression.Count; i++)
            {
                var item = activityExpression[i];
                if (item.Args.TryGetValue("weight", out var weight)) //获取自定义权重值
                {
                    list.Add(int.Parse(weight));
                }
                else //默认权重100
                {
                    item.Args.Add("weight", "100");
                    list.Add(100);
                }
            }
            //根据权重随机值
            var index = Random.RandomWeight(list);
            _currentExpression.Add(field, activityExpression[index]);
        }
        else
        {
            _currentExpression.Add(field, new ActivityExpressionData(""));
        }
    }
    
    private List<ActivityExpressionData> Parse(string str)
    {
        var list = new List<ActivityExpressionData>();
        var exps = str.Split(';');
        
        for (var i = 0; i < exps.Length; i++)
        {
            var exp = exps[i];
            //去除空格
            exp = Regex.Replace(exp, "\\s", "");
            if (string.IsNullOrEmpty(exp))
            {
                continue;
            }
            
            //验证语法
            if (Regex.IsMatch(exp, "^\\w+(\\((\\w+:\\w+)*(,\\w+:\\w+)*\\))?$"))
            {
                if (!exp.Contains('(')) //没有参数
                {
                    list.Add(new ActivityExpressionData(exp));
                }
                else
                {
                    var name = Regex.Match(exp, "^\\w+").Value;
                    var activityExpression = new ActivityExpressionData(name);
                    var paramsResult = Regex.Matches(exp, "\\w+:\\w+");
                    if (paramsResult.Count > 0)
                    {
                        foreach (Match result in paramsResult)
                        {
                            var valSplit = result.Value.Split(':');
                            activityExpression.Args.Add(valSplit[0], valSplit[1]);
                        }
                    }
                    list.Add(activityExpression);
                }
            }
            else //语法异常
            {
                throw new Exception("表达式语法错误: " + exp);
            }
        }

        return list;
    }
}