using System;
using Godot;

namespace UI.MapEditorTools;

public partial class DoorDragArea : Control
{
    /// <summary>
    /// 当前区域所占大小, 单位: 像素
    /// </summary>
    public Vector2 AreaSize => new Vector2(_areaSize, GameConfig.TileCellSize * 2);

    //错误时的颜色
    private static Color _errorColor = new Color(1, 0, 0, 0.1882353F);

    /// <summary>
    /// 朝向
    /// </summary>
    public DoorDirection Direction { get; private set; }
    
    /// <summary>
    /// 绑定的数据
    /// </summary>
    public DoorAreaInfo DoorAreaInfo { get; set; }
    
    /// <summary>
    /// 所属悬停区域
    /// </summary>
    public DoorHoverArea DoorHoverArea { get; private set; }
    
    /// <summary>
    /// 所属 Ui 对象
    /// </summary>
    public MapEditorToolsPanel MapEditorToolsPanel { get; private set; }

    private DoorDragButton _startButton;
    private DoorDragButton _endButton;

    private bool _mouseHover = false;
    private MapEditorTools.DoorToolTemplate _node;
    private Vector2 _startTempPos;
    private Vector2 _endTempPos;
    //默认颜色
    private Color _defaultColor;
    //区域大小, 单位: 像素
    private int _areaSize = GameConfig.TileCellSize * 4;
    //拖拽松手后是否可以提交
    private bool _canComment = true;
    //开始拖拽时的区域
    private Vector2I _startDragRange;
    //是否是拖拽模式
    private bool _isDragMode = false;
    //拖拽模式提交回调
    private Action<DoorDirection, int, int> _onSubmit;
    //拖拽模式取消时回调
    private Action _onCancel;

    public void SetDoorDragAreaNode(MapEditorTools.DoorToolTemplate node)
    {
        _node = node;
        MapEditorToolsPanel = (MapEditorToolsPanel)node.UiPanel;
        _defaultColor = _node.L_DoorArea.Instance.Color;
        _startButton = _node.L_StartBtn.Instance;
        _endButton = _node.L_EndBtn.Instance;
        
        _startButton.DragEvent += OnStartAreaDrag;
        _endButton.DragEvent += OnEndAreaDrag;
        node.L_DoorArea.Instance.MouseEntered += OnMouseEntered;
        node.L_DoorArea.Instance.MouseExited += OnMouseExited;
        
        SetDoorAreaSize(GameConfig.TileCellSize * 4);
    }

    public override void _Process(double delta)
    {
        if (_mouseHover && !DoorHoverArea.IsDrag && Input.IsMouseButtonPressed(MouseButton.Right)) //右键删除区域
        {
            MapEditorToolsPanel.RemoveDoorTool(_node);
        }
        else if (_isDragMode)
        {
            if (!Input.IsMouseButtonPressed(MouseButton.Left)) //松开了右键
            {
                _isDragMode = false;
                if (_canComment) //可以提交
                {
                    _isDragMode = false;
                    _onCancel = null;
                    _startButton.EmitSignal(BaseButton.SignalName.ButtonUp);
                    _endButton.EmitSignal(BaseButton.SignalName.ButtonUp);
                    if (_onSubmit != null)
                    {
                        var doorAreaRange = GetDoorAreaRange();
                        _onSubmit(Direction, doorAreaRange.X, doorAreaRange.Y);
                        _onSubmit = null;
                    }
                }
                else //不能提交
                {
                    if (_onCancel != null)
                    {
                        _onCancel();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 设置门区域的位置, 单位: 像素
    /// </summary>
    public void SetDoorAreaPosition(Vector2 position)
    {
        Position = position;
    }

    /// <summary>
    /// 设置所属悬停区域
    /// </summary>
    public void SetDoorHoverArea(DoorHoverArea hoverArea)
    {
        DoorHoverArea = hoverArea;
        Direction = hoverArea.Direction;
        if (Direction == DoorDirection.N)
        {
            Scale = new Vector2(1, 1);
            RotationDegrees = 0;
        }
        else if (Direction == DoorDirection.E)
        {
            Scale = new Vector2(1, 1);
            RotationDegrees = 90;
        }
        else if (Direction == DoorDirection.S)
        {
            Scale = new Vector2(-1, 1);
            RotationDegrees = 180;
        }
        else
        {
            Scale = new Vector2(-1, 1);
            RotationDegrees = 270;
        }
    }

    /// <summary>
    /// 获取门区域所占范围, 单位: 像素, 返回的 Vector2I 的 x 代表起始坐标, y 代表结束坐标
    /// </summary>
    /// <returns></returns>
    public Vector2I GetDoorAreaRange()
    {
        var start = (int)(_startButton.Position.X + _startButton.Size.X);
        return new Vector2I(start, start + _areaSize);
    }

    /// <summary>
    /// 设置门区域所占范围, 单位: 像素
    /// </summary>
    public void SetDoorAreaRange(int start, int size)
    {
        var startPosition = _startButton.Position;
        startPosition.X = start - _startButton.Size.X;
        _startButton.Position = startPosition;
        
        SetDoorAreaSize(size);
    }

    /// <summary>
    /// 设置区域起始坐标
    /// </summary>
    public void SetDoorAreaStart(int start)
    {
        var startPosition = _startButton.Position;
        startPosition.X = start - _startButton.Size.X;
        _startButton.Position = startPosition;
        
        RefreshArea();
    }

    /// <summary>
    /// 设置区域大小, 单位: 像素
    /// </summary>
    public void SetDoorAreaSize(int value)
    {
        _areaSize = value;
        RefreshArea();
        //GD.Print("size: " + GetDoorAreaRange());
    }

    //刷新区域位置
    private void RefreshArea()
    {
        var colorRect = _node.L_DoorArea.Instance;
        var position = colorRect.Position;
        position.X = _startButton.Position.X + _startButton.Size.X;
        colorRect.Position = position;
        colorRect.Size = AreaSize;

        var position2 = _endButton.Position;
        position2.X = position.X + AreaSize.X;
        _endButton.Position = position2;
    }
    
    //拖拽起始点
    private void OnStartAreaDrag(DragState dragState, Vector2 offset)
    {
        if (dragState == DragState.DragStart)
        {
            DoorHoverArea.IsDrag = true;
            _canComment = true;
            _startTempPos = _startButton.Position;
            _startDragRange = GetDoorAreaRange();
        }
        else if (dragState == DragState.DragMove)
        {
            if (_isDragMode)
            {
                offset.X -= GameConfig.TileCellSize;
            }
            
            var position = _startTempPos;
            position.X += offset.X + _startButton.Size.X;
            var endPosition = _endButton.Position;

            //拖拽模式
            if (_isDragMode)
            {
                if (position.X > endPosition.X) //该换方向了
                {
                    _startButton.EmitSignal(BaseButton.SignalName.ButtonUp);
                    _endButton.EmitSignal(BaseButton.SignalName.ButtonDown);

                    if (Mathf.Abs(position.X - endPosition.X) >= GameConfig.TileCellSize * 4 && !_canComment)
                    {
                        _canComment = true;
                        _node.L_DoorArea.Instance.Color = _defaultColor;
                    }
                    return;
                }
            }
            
            //计算区域大小
            var areaSize = (int)(endPosition.X - position.X);
            _startButton.Position = position - new Vector2(_startButton.Size.X, 0);
            //刷新区域位置
            SetDoorAreaSize(areaSize);
            
            //起始点坐标必须要小于终点坐标, 且起点坐标大于0
            if (position.X < endPosition.X && (DoorHoverArea == null || position.X >= 0))
            {
                //区域必须大于等于 4 格宽度
                if (areaSize >= GameConfig.TileCellSize * 4)
                {
                    var doorAreaRange = GetDoorAreaRange();
                    //检测是否撞到其他区域
                    bool checkResult;
                    if (DoorAreaInfo == null)
                    {
                        checkResult = MapEditorToolsPanel.EditorMap.Instance.CheckDoorArea(Direction, doorAreaRange.X, doorAreaRange.Y);
                    }
                    else
                    {
                        checkResult = MapEditorToolsPanel.EditorMap.Instance.CheckDoorArea(DoorAreaInfo, doorAreaRange.X, doorAreaRange.Y);
                    }
                    if (checkResult)
                    {
                        //可以提交
                        _canComment = true;
                        _node.L_DoorArea.Instance.Color = _defaultColor;
                        return;
                    }
                }
            }

            //错误的位置
            if (_canComment)
            {
                _canComment = false;
                _node.L_DoorArea.Instance.Color = _errorColor;
            }
        }
        else
        {
            DoorHoverArea.IsDrag = false;
            //松手后如果不能提交, 则还原初始位置
            if (!_canComment)
            {
                _canComment = true;
                _node.L_DoorArea.Instance.Color = _defaultColor;
                SetDoorAreaRange(_startDragRange.X, _startDragRange.Y - _startDragRange.X);
            }
            else
            {
                //提交数据
                SubmitData();
            }
            if (DoorHoverArea != null)
            {
                DoorHoverArea.EmitSignal(Control.SignalName.MouseExited);
            }
        }
    }
    
    private void OnEndAreaDrag(DragState dragState, Vector2 offset)
    {
        if (dragState == DragState.DragStart)
        {
            DoorHoverArea.IsDrag = true;
            _canComment = true;
            _endTempPos = _endButton.Position;
            _startDragRange = GetDoorAreaRange();
        }
        else if (dragState == DragState.DragMove)
        {
            var position = _endTempPos;
            position.X = position.X += offset.X;
            var startPosition = _startButton.Position + new Vector2(_startButton.Size.X, 0);
            
            //拖拽模式
            if (_isDragMode)
            {
                if (position.X < startPosition.X) //该换方向了
                {
                    _endButton.EmitSignal(BaseButton.SignalName.ButtonUp);
                    _startButton.EmitSignal(BaseButton.SignalName.ButtonDown);

                    if (Mathf.Abs(position.X - startPosition.X) >= GameConfig.TileCellSize * 4 && !_canComment)
                    {
                        _canComment = true;
                        _node.L_DoorArea.Instance.Color = _defaultColor;
                    }
                    return;
                }
            }
            
            //区域大小
            var areaSize = (int)(position.X - startPosition.X);
            //刷新区域位置
            SetDoorAreaSize(areaSize);
            
            //终点坐标必须要大于起始点坐标, 且终点坐标必须小于宽度
            if (position.X > startPosition.X && (DoorHoverArea == null || position.X <= DoorHoverArea.Size.X))
            {
                //区域必须大于等于 4 格宽度
                if (areaSize >= GameConfig.TileCellSize * 4)
                {
                    var doorAreaRange = GetDoorAreaRange();
                    //检测是否撞到其他区域
                    bool checkResult;
                    if (DoorAreaInfo == null)
                    {
                        checkResult = MapEditorToolsPanel.EditorMap.Instance.CheckDoorArea(Direction, doorAreaRange.X, doorAreaRange.Y);
                    }
                    else
                    {
                        checkResult = MapEditorToolsPanel.EditorMap.Instance.CheckDoorArea(DoorAreaInfo, doorAreaRange.X, doorAreaRange.Y);
                    }
                    if (checkResult)
                    {
                        //可以提交
                        _canComment = true;
                        _node.L_DoorArea.Instance.Color = _defaultColor;
                        return;
                    }
                }
            }

            //错误的位置
            if (_canComment)
            {
                _canComment = false;
                _node.L_DoorArea.Instance.Color = _errorColor;
            }
        }
        else
        {
            DoorHoverArea.IsDrag = false;
            //松手后如果不能提交, 则还原初始位置
            if (!_canComment)
            {
                _canComment = true;
                _node.L_DoorArea.Instance.Color = _defaultColor;
                SetDoorAreaRange(_startDragRange.X, _startDragRange.Y - _startDragRange.X);
            }
            else
            {
                //提交数据
                SubmitData();
            }
            if (DoorHoverArea != null)
            {
                DoorHoverArea.EmitSignal(Control.SignalName.MouseExited);
            }
        }
    }

    /// <summary>
    /// 将该区域变为拖拽模式, 用于创建门区域
    /// </summary>
    /// <param name="onSubmit">成功提交时回调, 参数1为方向, 参数2为起始点, 参数3为大小</param>
    /// <param name="onCancel">取消时调用</param>
    public void MakeDragMode(Action<DoorDirection, int, int> onSubmit, Action onCancel)
    {
        _canComment = false;
        _isDragMode = true;
        _onSubmit = onSubmit;
        _onCancel = onCancel;
        _endButton.EmitSignal(BaseButton.SignalName.ButtonDown);
    }

    //提交数据
    private void SubmitData()
    {
        //保存数据
        if (DoorAreaInfo != null)
        {
            var doorAreaRange = GetDoorAreaRange();
            DoorAreaInfo.Start = doorAreaRange.X;
            DoorAreaInfo.End = doorAreaRange.Y;
            GD.Print("submit: " + doorAreaRange);
        }
    }

    private void OnMouseEntered()
    {
        _mouseHover = true;
    }
    
    private void OnMouseExited()
    {
        _mouseHover = false;
    }
}