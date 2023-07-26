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
    private Action<int, int> _onSubmit;
    //拖拽模式取消时回调
    private Action _onCancel;

    public void SetDoorDragAreaNode(MapEditorTools.DoorToolTemplate node)
    {
        _node = node;
        _defaultColor = _node.L_DoorArea.Instance.Color;
        _node.L_StartBtn.Instance.DragEvent += OnStartAreaDrag;
        _node.L_EndBtn.Instance.DragEvent += OnEndAreaDrag;
        
        SetDoorAreaSize(GameConfig.TileCellSize * 4);
        SetDoorAreaDirection(DoorDirection.N);
    }

    public override void _Process(double delta)
    {
        if (_isDragMode)
        {
            if (!Input.IsMouseButtonPressed(MouseButton.Left)) //松开了右键
            {
                _isDragMode = false;
                if (_canComment) //可以提交
                {
                    if (_onSubmit != null)
                    {
                        var doorAreaRange = GetDoorAreaRange();
                        _onSubmit(doorAreaRange.X, doorAreaRange.Y);
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
    /// 设置门区域的方向
    /// </summary>
    public void SetDoorAreaDirection(DoorDirection direction)
    {
        Direction = direction;
        if (direction == DoorDirection.N)
        {
            Scale = new Vector2(1, 1);
            RotationDegrees = 0;
        }
        else if (direction == DoorDirection.E)
        {
            Scale = new Vector2(1, 1);
            RotationDegrees = 90;
        }
        else if (direction == DoorDirection.S)
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
        return new Vector2I((int)(_node.L_StartBtn.Instance.Position.X + _node.L_StartBtn.Instance.Size.X), _areaSize);
    }

    /// <summary>
    /// 设置门区域所占范围, 单位: 像素
    /// </summary>
    public void SetDoorAreaRange(int start, int size)
    {
        var startPosition = _node.L_StartBtn.Instance.Position;
        startPosition.X = start - _node.L_StartBtn.Instance.Size.X;
        _node.L_StartBtn.Instance.Position = startPosition;
        
        SetDoorAreaSize(size);
    }

    /// <summary>
    /// 设置区域起始坐标
    /// </summary>
    public void SetDoorAreaStart(int start)
    {
        var startPosition = _node.L_StartBtn.Instance.Position;
        startPosition.X = start - _node.L_StartBtn.Instance.Size.X;
        _node.L_StartBtn.Instance.Position = startPosition;
        
        RefreshArea();
    }

    /// <summary>
    /// 设置区域大小, 单位: 像素
    /// </summary>
    public void SetDoorAreaSize(int value)
    {
        _areaSize = value;
        RefreshArea();
        GD.Print("size: " + GetDoorAreaRange());
    }

    //刷新区域位置
    private void RefreshArea()
    {
        var colorRect = _node.L_DoorArea.Instance;
        var position = colorRect.Position;
        position.X = _node.L_StartBtn.Instance.Position.X + _node.L_StartBtn.Instance.Size.X;
        colorRect.Position = position;
        colorRect.Size = AreaSize;

        var position2 = _node.L_EndBtn.Instance.Position;
        position2.X = position.X + AreaSize.X;
        _node.L_EndBtn.Instance.Position = position2;
    }
    
    //拖拽起始点
    private void OnStartAreaDrag(DragState dragState, Vector2 offset)
    {
        if (dragState == DragState.DragStart)
        {
            _canComment = true;
            _startTempPos = _node.L_StartBtn.Instance.Position;
            _startDragRange = GetDoorAreaRange();
        }
        else if (dragState == DragState.DragMove)
        {
            var position = _startTempPos;
            position.X = position.X += offset.X;
            var endPosition = _node.L_EndBtn.Instance.Position;
            
            //计算区域大小
            var areaSize = (int)(endPosition.X - position.X - _node.L_StartBtn.Instance.Size.X);
            
            _node.L_StartBtn.Instance.Position = position;
            //刷新区域位置
            SetDoorAreaSize(areaSize);
            
            //起始点坐标必须要小于终点坐标
            if (position.X < endPosition.X)
            {
                //区域必须大于等于 4 格宽度
                if (areaSize >= GameConfig.TileCellSize * 4)
                {
                    if (!_canComment)
                    {
                        _canComment = true;
                        _node.L_DoorArea.Instance.Color = _defaultColor;
                    }
                    return;
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
            //松手后如果不能提交, 则还原初始位置
            if (!_canComment)
            {
                _canComment = true;
                _node.L_DoorArea.Instance.Color = _defaultColor;
                SetDoorAreaRange(_startDragRange.X, _startDragRange.Y);
            }
        }
    }
    
    private void OnEndAreaDrag(DragState dragState, Vector2 offset)
    {
        if (dragState == DragState.DragStart)
        {
            _canComment = true;
            _endTempPos = _node.L_EndBtn.Instance.Position;
            _startDragRange = GetDoorAreaRange();
        }
        else if (dragState == DragState.DragMove)
        {
            var position = _endTempPos;
            position.X = position.X += offset.X;

            //区域大小
            var areaSize = (int)(position.X - _node.L_StartBtn.Instance.Position.X - _node.L_StartBtn.Instance.Size.X);
            //刷新区域位置
            SetDoorAreaSize(areaSize);
            
            //终点坐标必须要大于起始点坐标
            var startPosition = _node.L_StartBtn.Instance.Position;
            if (position.X > startPosition.X)
            {

                //区域必须大于等于 4 格宽度
                if (areaSize >= GameConfig.TileCellSize * 4)
                {
                    if (!_canComment)
                    {
                        _canComment = true;
                        _node.L_DoorArea.Instance.Color = _defaultColor;
                    }
                    return;
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
            //松手后如果不能提交, 则还原初始位置
            if (!_canComment)
            {
                _canComment = true;
                _node.L_DoorArea.Instance.Color = _defaultColor;
                SetDoorAreaRange(_startDragRange.X, _startDragRange.Y);
            }
        }
    }

    /// <summary>
    /// 将该区域变为拖拽模式, 用于创建门区域
    /// </summary>
    /// <param name="onSubmit">成功提交时回调, 参数1为起始点, 参数2为大小</param>
    /// <param name="onCancel">取消时调用</param>
    public void MakeDragMode(Action<int, int> onSubmit, Action onCancel)
    {
        _canComment = false;
        _isDragMode = true;
        _onSubmit = onSubmit;
        _onCancel = onCancel;
        _node.L_EndBtn.Instance.EmitSignal(BaseButton.SignalName.ButtonDown);
    }
}