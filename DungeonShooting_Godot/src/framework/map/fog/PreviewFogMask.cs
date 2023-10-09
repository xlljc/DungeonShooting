
using System;
using System.Collections;
using Godot;

/// <summary>
/// 绑定在门上的预览迷雾
/// </summary>
public partial class PreviewFogMask : PointLight2D, IDestroy
{
    public enum PreviewFogType
    {
        Aisle,
        Room
    }
    
    public bool IsDestroyed { get; private set; }

    private static bool _initTexture;
    private static Texture2D _previewAisle;
    private static Texture2D _previewRoom;
    
    /// <summary>
    /// 房间门
    /// </summary>
    public RoomDoorInfo DoorInfo;
    
    /// <summary>
    /// 迷雾类型
    /// </summary>
    public PreviewFogType FogType { get; private set; }

    /// <summary>
    /// 迷雾透明度值, 这个值在调用 TransitionAlpha() 时改变, 用于透明度判断
    /// </summary>
    public float TargetAlpha { get; private set; }
    
    private float _previewAisleAngle;
    private float _previewRoomAngle;
    private Vector2 _previewAislePosition;
    private Vector2 _previewRoomPosition;
    private long _cid;
    
    private static void InitTexture()
    {
        if (_initTexture)
        {
            return;
        }

        _initTexture = true;
        _previewAisle = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_map_PreviewTransition_png);
        _previewRoom = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_map_PreviewTransition2_png);
    }
    
    /// <summary>
    /// 初始化迷雾
    /// </summary>
    public void Init(RoomDoorInfo doorInfo)
    {
        InitTexture();

        DoorInfo = doorInfo;
        if (doorInfo.Direction == DoorDirection.E)
        {
            _previewAislePosition = doorInfo.Door.GlobalPosition + new Vector2(GameConfig.TileCellSize, 0);
            _previewAisleAngle = 90;
            
            _previewRoomPosition = doorInfo.Door.GlobalPosition + new Vector2(-GameConfig.TileCellSize, 0);
            _previewRoomAngle = 270;
        }
        else if (doorInfo.Direction == DoorDirection.W)
        {
            _previewAislePosition = doorInfo.Door.GlobalPosition + new Vector2(-GameConfig.TileCellSize, 0);
            _previewAisleAngle = 270;
            
            _previewRoomPosition = doorInfo.Door.GlobalPosition + new Vector2(GameConfig.TileCellSize, 0);
            _previewRoomAngle = 90;
        }
        else if (doorInfo.Direction == DoorDirection.N)
        {
            _previewAislePosition = doorInfo.Door.GlobalPosition + new Vector2(0, -GameConfig.TileCellSize);
            _previewAisleAngle = 0;
            
            _previewRoomPosition = doorInfo.Door.GlobalPosition + new Vector2(0, GameConfig.TileCellSize);
            _previewRoomAngle = 180;
        }
        else if (doorInfo.Direction == DoorDirection.S)
        {
            _previewAislePosition = doorInfo.Door.GlobalPosition;
            _previewAisleAngle = 180;
            
            _previewRoomPosition = doorInfo.Door.GlobalPosition + new Vector2(0, -GameConfig.TileCellSize * 2);
            _previewRoomAngle = 0;
        }

        RefreshPreviewFogType(PreviewFogType.Aisle);
    }
    
    /// <summary>
    /// 使颜色的 alpha 通道过渡到指定的值
    /// </summary>
    /// <param name="targetAlpha">透明度值</param>
    /// <param name="time">过渡时间</param>
    public void TransitionAlpha(float targetAlpha, float time)
    {
        TargetAlpha = targetAlpha;
        if (_cid >= 0)
        {
            World.Current.StopCoroutine(_cid);
        }
        
        _cid = World.Current.StartCoroutine(RunTransitionAlpha(targetAlpha, time, false));
    }

    public void TransitionToHide(float time)
    {
        TargetAlpha = 0;
        if (_cid >= 0)
        {
            World.Current.StopCoroutine(_cid);
        }
        
        _cid = World.Current.StartCoroutine(RunTransitionAlpha(TargetAlpha, time, true));
    }

    private IEnumerator RunTransitionAlpha(float targetAlpha, float time, bool hide)
    {
        var originColor = Color;
        var a = originColor.A;
        var delta = Mathf.Abs(a - targetAlpha) / time;
        while (Math.Abs(a - targetAlpha) > 0.001f)
        {
            a = Mathf.MoveToward(a, targetAlpha, delta * (float)World.Current.GetProcessDeltaTime());
            Color = new Color(1, 1, 1, a);
            yield return null;
        }
        _cid = -1;
        if (hide)
        {
            this.SetActive(false);
        }
    }

    /// <summary>
    /// 更新预览迷雾类型
    /// </summary>
    public void SetPreviewFogType(PreviewFogType fogType)
    {
        if (FogType != fogType)
        {
            RefreshPreviewFogType(fogType);
        }
    }

    private void RefreshPreviewFogType(PreviewFogType fogType)
    {
        FogType = fogType;
        if (fogType == PreviewFogType.Aisle)
        {
            Texture = _previewAisle;
            Position = _previewAislePosition;
            RotationDegrees = _previewAisleAngle;
        }
        else
        {
            Texture = _previewRoom;
            Position = _previewRoomPosition;
            RotationDegrees = _previewRoomAngle;
        }
    }
    
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }

        IsDestroyed = true;
        QueueFree();
    }
}