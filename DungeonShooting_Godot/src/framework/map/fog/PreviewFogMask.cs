
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

    private float _previewAisleAngle;
    private float _previewRoomAngle;
    private Vector2 _previewAislePosition;
    private Vector2 _previewRoomPosition;
    
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
            _previewAislePosition = doorInfo.Door.GlobalPosition + new Vector2(16, 0);
            _previewAisleAngle = 90;
        }
        else if (doorInfo.Direction == DoorDirection.W)
        {
            _previewRoomPosition = doorInfo.Door.GlobalPosition + new Vector2(16, 0);
            _previewRoomAngle = 90;
        }

        RefreshPreviewFogType(PreviewFogType.Aisle);
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