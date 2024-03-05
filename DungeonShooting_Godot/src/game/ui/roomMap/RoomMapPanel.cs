using System.Collections.Generic;
using Godot;
using UI.RoomUI;

namespace UI.RoomMap;

/// <summary>
/// 房间的小地图
/// </summary>
public partial class RoomMapPanel : RoomMap
{
    private EventFactory _factory = EventManager.CreateEventFactory();
    //需要刷新的问号的房间队列
    private List<RoomDoorInfo> _needRefresh = new List<RoomDoorInfo>();
    //正在使用的敌人标记列表
    private List<Sprite2D> _enemySpriteList = new List<Sprite2D>();
    //已经回收的敌人标记
    private Stack<Sprite2D> _spriteStack = new Stack<Sprite2D>();
    //是否放大地图
    private bool _isMagnifyMap = false;

    private UiEventBinder _dragBinder;
    //放大地图后拖拽的偏移
    private Vector2 _mapOffset;
    //放大地图后鼠标悬停的房间
    private RoomInfo _mouseHoverRoom;
    private Color _originOutlineColor;
    //是否展开地图
    private bool _pressMapFlag = false;
    private Tween _transmissionTween;
    
    public override void OnCreateUi()
    {
        _ = S_Mark;
        S_Bg.Instance.Visible = false;
        S_MagnifyMapBar.Instance.Visible = false;
        InitMap();
        _factory.AddEventListener(EventEnum.OnPlayerFirstEnterRoom, OnPlayerFirstEnterRoom);
        _factory.AddEventListener(EventEnum.OnPlayerFirstEnterAisle, OnPlayerFirstEnterAisle);

        S_DrawContainer.Instance.Resized += OnDrawContainerResized;
    }

    public override void OnDestroyUi()
    {
        _factory.RemoveAllEventListener();

        if (_transmissionTween != null)
        {
            _transmissionTween.Dispose();
        }
    }

    public override void Process(float delta)
    {
        if (_transmissionTween == null) //不在传送过程中
        {
            if (!InputManager.Map)
            {
                _pressMapFlag = false;
            }
            //按下地图按键
            if (InputManager.Map && !_isMagnifyMap && !_pressMapFlag) //展开小地图
            {
                if (UiManager.GetUiInstanceCount(UiManager.UiNames.PauseMenu) == 0)
                {
                    ExpandMap();
                }
            }
            else if (!InputManager.Map && _isMagnifyMap) //还原小地图
            {
                ShrinkMap();
            }
        }
        
        //更新敌人位置
        {
            var enemyList = World.Current.Enemy_InstanceList;
            if (enemyList.Count == 0) //没有敌人
            {
                foreach (var sprite in _enemySpriteList)
                {
                    S_Root.RemoveChild(sprite);
                    _spriteStack.Push(sprite);
                }
                _enemySpriteList.Clear();
            }
            else //更新位置
            {
                var count = 0; //绘制数量
                for (var i = 0; i < enemyList.Count; i++)
                {
                    var enemy = enemyList[i];
                    if (!enemy.IsDestroyed && !enemy.IsDie && enemy.AffiliationArea != null && enemy.AffiliationArea.RoomInfo.RoomFogMask.IsExplored)
                    {
                        count++;
                        Sprite2D sprite;
                        if (i >= _enemySpriteList.Count)
                        {
                            if (_spriteStack.Count > 0)
                            {
                                sprite = _spriteStack.Pop();
                            }
                            else
                            {
                                sprite = new Sprite2D();
                                sprite.Texture = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Block_png);
                                sprite.Modulate = new Color(1, 0, 0);
                            }
                            _enemySpriteList.Add(sprite);
                            S_Root.AddChild(sprite);
                        }
                        else
                        {
                            sprite = _enemySpriteList[i];
                        }
                        //更新标记位置
                        sprite.Position = enemy.GetCenterPosition() / 16;
                    }
                }
                
                //回收多余的标记
                while (_enemySpriteList.Count > count)
                {
                    var index = _enemySpriteList.Count - 1;
                    var sprite = _enemySpriteList[index];
                    S_Root.RemoveChild(sprite);
                    _spriteStack.Push(sprite);
                    _enemySpriteList.RemoveAt(index);
                }
            }
        }
        
        //更新预览图标
        if (_needRefresh.Count > 0)
        {
            foreach (var roomDoorInfo in _needRefresh)
            {
                HandlerRefreshUnknownSprite(roomDoorInfo);
            }
            _needRefresh.Clear();
        }

        if (Player.Current != null)
        {
            //更新地图中心点位置
            var playPosition = Player.Current.GetCenterPosition();
            if (!_isMagnifyMap)
            {
                S_Root.Instance.Position = CalcRootPosition(playPosition);
            }
            else
            {
                S_Root.Instance.Position = CalcRootPosition(playPosition) + _mapOffset;
                S_Mark.Instance.Position = S_DrawContainer.Instance.Size / 2 + _mapOffset;
            }

            var area = Player.Current.AffiliationArea;
            //传送
            if (_pressMapFlag && _mouseHoverRoom != null &&
                area != null && !area.RoomInfo.IsSeclusion &&
                Input.IsMouseButtonPressed(MouseButton.Right))
            {
                //执行传送操作
                DoTransmission((_mouseHoverRoom.Waypoints + new Vector2(0.5f, 0.5f)) * GameConfig.TileCellSize);
                ResetMap();
                _isMagnifyMap = false;
                World.Current.Pause = false;
            }
        }
    }

    /// <summary>
    /// 执行展开地图
    /// </summary>
    public void ExpandMap()
    {
        World.Current.Pause = true;
        _pressMapFlag = true;
        _isMagnifyMap = true;
        MagnifyMap();
    }
    
    /// <summary>
    /// 执行收起地图
    /// </summary>
    public void ShrinkMap()
    {
        ResetMap();
        _isMagnifyMap = false;
        World.Current.Pause = false;
    }
    
    private void OnDrawContainerResized()
    {
        S_Mark.Instance.Position = S_DrawContainer.Instance.Size / 2;
    }

    //放大小地图
    private void MagnifyMap()
    {
        GameApplication.Instance.Cursor.SetGuiMode(true);
        S_DrawContainer.Reparent(S_MagnifyMapBar);
        S_DrawContainer.Instance.Position = new Vector2(1, 1);
        S_Bg.Instance.Visible = true;
        S_MagnifyMapBar.Instance.Visible = true;
        S_MapBar.Instance.Visible = false;
        _mapOffset = Vector2.Zero;

        _dragBinder = S_DrawContainer.Instance.AddDragListener((state, delta) =>
        {
            if (state == DragState.DragMove)
            {
                _mapOffset += delta;
            }
        });
    }

    //还原小地图
    private void ResetMap()
    {
        GameApplication.Instance.Cursor.SetGuiMode(false);
        S_DrawContainer.Reparent(S_MapBar);
        S_DrawContainer.Instance.Position = new Vector2(1, 1);
        S_Bg.Instance.Visible = false;
        S_MagnifyMapBar.Instance.Visible = false;
        S_MapBar.Instance.Visible = true;
        ResetOutlineColor();

        if (_dragBinder != null)
        {
            _dragBinder.UnBind();
            _dragBinder = null;
        }
    }

    private void ResetOutlineColor()
    {
        if (_mouseHoverRoom != null)
        {
            ((ShaderMaterial)_mouseHoverRoom.PreviewSprite.Material).SetShaderParameter("outline_color", _originOutlineColor);
            _mouseHoverRoom = null;
        }
    }
    
    //初始化小地图
    private void InitMap()
    {
        var startRoom = GameApplication.Instance.DungeonManager.StartRoomInfo;
        if (startRoom == null)
        {
            HideUi();
            return;
        }
        startRoom.EachRoom(roomInfo =>
        {
            //房间
            roomInfo.PreviewSprite.Visible = false;
            S_Root.AddChild(roomInfo.PreviewSprite);

            roomInfo.PreviewSprite.MouseEntered += () =>
            {
                if (!_pressMapFlag)
                {
                    return;
                }
                ResetOutlineColor();
                _mouseHoverRoom = roomInfo;
                var shaderMaterial = (ShaderMaterial)roomInfo.PreviewSprite.Material;
                _originOutlineColor = shaderMaterial.GetShaderParameter("outline_color").AsColor();
                //玩家所在的房间门是否打开
                var area = Player.Current.AffiliationArea;
                if (area != null)
                {
                    var isOpen = !area.RoomInfo.IsSeclusion;
                    if (isOpen)
                    {
                        shaderMaterial.SetShaderParameter("outline_color", new Color(0, 1, 0, 0.9f));
                    }
                    else
                    {
                        shaderMaterial.SetShaderParameter("outline_color", new Color(1, 0, 0, 0.9f));
                    }
                }
            };
            roomInfo.PreviewSprite.MouseExited += () =>
            {
                if (!_pressMapFlag)
                {
                    return;
                }
                ResetOutlineColor();
            };
            
            //过道
            if (roomInfo.Doors != null)
            {
                foreach (var roomInfoDoor in roomInfo.Doors)
                {
                    if (roomInfoDoor.IsForward)
                    {
                        roomInfoDoor.AislePreviewSprite.Visible = false;
                        S_Root.AddChild(roomInfoDoor.AislePreviewSprite);
                    }
                }
            }
        });
    }
    
    private void OnPlayerFirstEnterRoom(object data)
    {
        var roomInfo = (RoomInfo)data;
        if (roomInfo.PreviewSprite != null)
        {
            roomInfo.PreviewSprite.Visible = true;
        }

        if (roomInfo.Doors!= null)
        {
            foreach (var roomDoor in roomInfo.Doors)
            {
                RefreshUnknownSprite(roomDoor);
            }
        }
    }
    
    private void OnPlayerFirstEnterAisle(object data)
    {
        var roomDoorInfo = (RoomDoorInfo)data;
        roomDoorInfo.AislePreviewSprite.Visible = true;

        RefreshUnknownSprite(roomDoorInfo);
        RefreshUnknownSprite(roomDoorInfo.ConnectDoor);
    }

    //进入刷新问号队列
    private void RefreshUnknownSprite(RoomDoorInfo roomDoorInfo)
    {
        if (!_needRefresh.Contains(roomDoorInfo))
        {
            _needRefresh.Add(roomDoorInfo);
        }
    }

    //刷新问号
    private void HandlerRefreshUnknownSprite(RoomDoorInfo roomDoorInfo)
    {
        //是否探索房间
        var flag1 = roomDoorInfo.RoomInfo.RoomFogMask.IsExplored;
        //是否探索过道
        var flag2 = roomDoorInfo.AisleFogMask.IsExplored;
        if (flag1 == flag2) //不显示问号
        {
            if (roomDoorInfo.UnknownSprite != null)
            {
                roomDoorInfo.UnknownSprite.QueueFree();
                roomDoorInfo.UnknownSprite = null;
            }
        }
        else
        {
            var unknownSprite = roomDoorInfo.UnknownSprite ?? CreateUnknownSprite(roomDoorInfo);
            var pos = (roomDoorInfo.OriginPosition + roomDoorInfo.GetEndPosition()) / 2;
            if (!flag2) //偏向过道
            {
                if (roomDoorInfo.Direction == DoorDirection.N)
                    pos += new Vector2I(0, -1);
                else if (roomDoorInfo.Direction == DoorDirection.S)
                    pos += new Vector2I(0, 3);
                else if (roomDoorInfo.Direction == DoorDirection.E)
                    pos += new Vector2I(2, 1);
                else if (roomDoorInfo.Direction == DoorDirection.W)
                    pos += new Vector2I(-2, 1);
            }
            else //偏向房间
            {
                if (roomDoorInfo.Direction == DoorDirection.N)
                    pos -= new Vector2I(0, -4);
                else if (roomDoorInfo.Direction == DoorDirection.S)
                    pos -= new Vector2I(0, 1);
                else if (roomDoorInfo.Direction == DoorDirection.E)
                    pos -= new Vector2I(3, -1);
                else if (roomDoorInfo.Direction == DoorDirection.W)
                    pos -= new Vector2I(-3, -1);
            }
            unknownSprite.Position = pos;
        }
    }

    private Sprite2D CreateUnknownSprite(RoomDoorInfo roomInfoDoor)
    {
        var unknownSprite = new Sprite2D();
        unknownSprite.Texture = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Unknown_png);
        unknownSprite.Scale = new Vector2(0.25f, 0.25f);
        roomInfoDoor.UnknownSprite = unknownSprite;
        S_Root.AddChild(unknownSprite);
        return unknownSprite;
    }

    private Vector2 CalcRootPosition(Vector2 pos)
    {
        return S_DrawContainer.Instance.Size / 2 - pos / 16 * S_Root.Instance.Scale;
    }
    
    private void DoTransmission(Vector2 position)
    {
        var roomUI = (RoomUIPanel)ParentUi;
        roomUI.S_Mask.Instance.Visible = true;
        roomUI.S_Mask.Instance.Color = new Color(0, 0, 0, 0);
        _transmissionTween = CreateTween();
        _transmissionTween.TweenProperty(roomUI.S_Mask.Instance, "color", new Color(0, 0, 0), 0.3f);
        _transmissionTween.TweenCallback(Callable.From(() =>
        {
            Player.Current.Position = position;
        }));
        _transmissionTween.TweenInterval(0.2f);
        _transmissionTween.TweenProperty(roomUI.S_Mask.Instance, "color", new Color(0, 0, 0, 0), 0.3f);
        _transmissionTween.TweenCallback(Callable.From(() =>
        {
            _transmissionTween = null;
        }));
        _transmissionTween.Play();
    }
}
