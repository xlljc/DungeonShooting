
using System;
using System.IO;
using Godot;

/// <summary>
/// 用于在预制场景中创建 ActivityObject
/// </summary>
[Tool]
public partial class ActivityInstance : Node2D
{
    private const string GroupName = "Editor";
    
    /// <summary>
    /// 物体Id
    /// </summary>
    [Export]
    public string Id
    {
        get => _id;
        set
        {
            _id = value;
            _dirty = true;
        }
    }

    /// <summary>
    /// 默认所在层级
    /// </summary>
    [Export]
    public RoomLayerEnum DefaultLayer { get; set; } = RoomLayerEnum.NormalLayer;

    /// <summary>
    /// 是否显示阴影
    /// </summary>
    [Export]
    public bool ShowShadow
    {
        get => _showShadow;
        set
        {
            _showShadow = value;
            if (_activityObject != null)
            {
                if (value)
                {
                    _activityObject.ShowShadowSprite();
                }
                else
                {
                    _activityObject.HideShadowSprite();
                }
            }
        }
    }

    /// <summary>
    /// 阴影偏移
    /// </summary>
    [Export]
    public Vector2 ShowOffset
    {
        get => _showOffset;
        set
        {
            _showOffset = value;
            if (_activityObject != null)
            {
                _activityObject.ShadowOffset = value;
            }
        }
    }

    /// <summary>
    /// 初始海拔高度
    /// </summary>
    [Export]
    public float Altitude
    {
        get => _altitude;
        set
        {
            _altitude = value;
            if (_activityObject != null)
            {
                _activityObject.Altitude = value;
                _activityObject.Collision.Position = _collPos;
                _activityObject.UpdateShadowSprite((float)GetProcessDeltaTime());
                _activityObject.CalcThrowAnimatedPosition();
            }
        }
    }

    /// <summary>
    /// 动画精灵的z轴索引
    /// </summary>
    [Export]
    public int SpriteZIndex
    {
        get => _spriteZIndex;
        set
        {
            _spriteZIndex = value;
            if (_activityObject != null)
            {
                _activityObject.AnimatedSprite.ZIndex = value;
            }
        }
    }
    
    /// <summary>
    /// 阴影z轴索引
    /// </summary>
    [Export]
    public int ShadowZIndex
    {
        get => _shadowZIndex;
        set
        {
            _shadowZIndex = value;
            if (_activityObject != null)
            {
                _activityObject.ShadowSprite.ZIndex = value;
            }
        }
    }
    
    /// <summary>
    /// 是否启用垂直运动模拟
    /// </summary>
    [Export]
    public bool VerticalMotion { get; private set; } = true;

    /// <summary>
    /// 是否启用碰撞器
    /// </summary>
    [Export]
    public bool CollisionEnabled
    {
        get => _collisionEnabled;
        set
        {
            _collisionEnabled = value;
            if (_activityObject != null)
            {
                _activityObject.Collision.Disabled = !value;
            }
        }
    }
    
    /// <summary>
    /// 编辑器属性, 物体子碰撞器在编辑器中是否可见
    /// </summary>
    [Export]
    public bool CollisionVisible
    {
        get => _collisionVisible;
        set
        {
            _collisionVisible = value;
            OnChangeCollisionVisible();
        }
    }

    private bool _dirty = false;
    private bool _collisionVisible = true;
    private string _prevId;
    private string _id;
    private ActivityObject _activityObject;
    private Sprite2D _errorSprite;
    private bool _showShadow = true;
    private Vector2 _showOffset = new Vector2(0, 2);
    private float _altitude;
    private int _spriteZIndex = 0;
    private int _shadowZIndex = -1;
    private bool _collisionEnabled = true;

    private Vector2 _collPos;
    private bool _createFlag = false;
    
    //嵌套Instance相关
    private bool _isNested = false;
    private ActivityObject _activityInstance;
    
    private static string _jsonText;

    /// <summary>
    /// 清空缓存的json
    /// </summary>
    public static void ClearCacheJson()
    {
        _jsonText = null;
    }
    
    public override void _Ready()
    {
#if TOOLS
        if (!Engine.IsEditorHint())
        {
#endif
            var world = World.Current;
            if (world != null && world.YSortLayer != null && world.NormalLayer != null)
            {
                DoCreateObject();
            }
#if TOOLS
        }
#endif
    }

    public override void _Process(double delta)
    {
#if TOOLS
        if (Engine.IsEditorHint())
        {
            if (_dirty || (_activityObject != null && _activityObject.GetParent() != this))
            {
                _dirty = false;
                
                if (_prevId != _id)
                {
                    OnChangeActivityId(_id);
                }
                else if (string.IsNullOrEmpty(_id))
                {
                    ShowErrorSprite();
                }

                OnChangeCollisionVisible();
            }

            if (_activityObject != null)
            {
                _activityObject.Collision.Position = _collPos;
                _activityObject.UpdateShadowSprite((float)delta);
                _activityObject.CalcThrowAnimatedPosition();
            }
        }
        else
        {
#endif
            var world = World.Current;
            if (world != null && world.YSortLayer != null && world.NormalLayer != null)
            {
                DoCreateObject();
            }
#if TOOLS
        }
#endif
    }

    public override void _EnterTree()
    {
#if TOOLS
        if (Engine.IsEditorHint())
        {
            _dirty = true;
            if (_activityObject != null)
            {
                _activityObject.QueueFree();
            }
            _activityObject = null;
            _prevId = null;
        }
#endif
    }

    public override void _ExitTree()
    {
#if TOOLS
        if (Engine.IsEditorHint() && _activityObject != null)
        {
            _activityObject.QueueFree();
            _activityObject = null;
        }
#endif
    }

    private ActivityObject DoCreateObject()
    {
        if (_createFlag)
        {
            return _activityInstance;
        }

        _createFlag = true;
        var activityObject = ActivityObject.Create(Id);
        if (_isNested)
        {
            activityObject.Position = Position;
            activityObject.Scale = Scale;
            activityObject.Rotation = Rotation;
        }
        else
        {
            activityObject.Position = GlobalPosition;
            activityObject.Scale = GlobalScale;
            activityObject.Rotation = GlobalRotation;
        }
        
        activityObject.Visible = Visible;
        activityObject.ShadowOffset = _showOffset;
        activityObject.Altitude = _altitude;
        activityObject.AnimatedSprite.ZIndex = _spriteZIndex;
        activityObject.ShadowSprite.ZIndex = _shadowZIndex;
        activityObject.EnableVerticalMotion = VerticalMotion;
        activityObject.Collision.Disabled = !_collisionEnabled;
        if (!_isNested)
        {
            activityObject.PutDown(DefaultLayer, _showShadow);
        }
        else
        {
            activityObject.DefaultLayer = DefaultLayer;
            activityObject.ShowShadowSprite();
        }

        var children = GetChildren();
        foreach (var child in children)
        {
            if (!child.IsInGroup(GroupName))
            {
                if (child is ActivityInstance o)
                {
                    o._isNested = true;
                    var instance = o.DoCreateObject();
                    activityObject.AddChild(instance);
                    if (instance is IMountItem mountItem)
                    {
                        activityObject.AddMountObject(mountItem);
                    }
                    else if (instance is IDestroy destroy)
                    {
                        activityObject.AddDestroyObject(destroy);
                    }
                }
                else
                {
                    child.Reparent(activityObject);
                }
            }
        }

        QueueFree();
        _activityInstance = activityObject;
        return  activityObject;
    }

    private void OnChangeActivityId(string id)
    {
        _prevId = id;

        if (_activityObject != null)
        {
            _activityObject.QueueFree();
            _activityObject = null;
        }

        if (string.IsNullOrEmpty(id))
        {
            ShowErrorSprite();
            return;
        }

        if (_jsonText == null)
        {
            _jsonText = File.ReadAllText("resource/config/ActivityBase.json");
        }
        var str = $"\"Id\": \"{id}\",";
        var index = _jsonText.IndexOf(str, StringComparison.Ordinal);
        if (index > -1)
        {
            const string s = "\"Prefab\": \"";
            var startIndex = _jsonText.IndexOf(s, index, StringComparison.Ordinal);
            if (startIndex > -1)
            {
                var endIndex = _jsonText.IndexOf('"', startIndex + s.Length + 1);
                if (endIndex > -1)
                {
                    var prefab = _jsonText.Substring(startIndex + s.Length, endIndex - (startIndex + s.Length));
                    var instance = ResourceManager.LoadAndInstantiate<ActivityObject>(prefab);
                    _activityObject = instance;
                    _collPos = instance.Collision.Position - instance.AnimatedSprite.Position - instance.AnimatedSprite.Offset;
                    instance.IsCustomShadowSprite = instance.ShadowSprite.Texture != null;
                    instance.Altitude = _altitude;
                    instance.ShadowOffset = _showOffset;
                    if (_showShadow)
                    {
                        instance.ShowShadowSprite();
                        var shadowSpriteMaterial = instance.ShadowSprite.Material as ShaderMaterial;
                        if (shadowSpriteMaterial != null)
                        {
                            shadowSpriteMaterial.SetShaderParameter(
                                ShaderParamNames.ShowOutline,
                                ((ShaderMaterial)instance.AnimatedSprite.Material).GetShaderParameter(ShaderParamNames
                                    .ShowOutline)
                            );
                        }
                    }
                    AddChild(instance);
                    MoveChild(instance, 0);
                    HideErrorSprite();
                    return;
                }
            }
        }
        GD.PrintErr($"未找到Id为'{id}'的物体!");
        ShowErrorSprite();
    }

    private void OnChangeCollisionVisible()
    {
        if (_activityObject != null)
        {
            Utils.EachNode(_activityObject, node =>
            {
                if (node is CollisionShape2D collisionShape2D)
                {
                    collisionShape2D.Visible = _collisionVisible;
                }
                else if (node is CollisionPolygon2D collisionPolygon2D)
                {
                    collisionPolygon2D.Visible = _collisionVisible;
                }
            });
        }
    }

    private void ShowErrorSprite()
    {
        if (_errorSprite == null)
        {
            _errorSprite = new Sprite2D();
            _errorSprite.AddToGroup(GroupName);
            _errorSprite.Texture = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Error_mini_png);
            AddChild(_errorSprite);
            MoveChild(_errorSprite, GetChildCount() - 1);
        }
    }

    private void HideErrorSprite()
    {
        if (_errorSprite != null)
        {
            _errorSprite.QueueFree();
            _errorSprite = null;
        }
    }
}