using Godot;

namespace UI.MapEditorTools;

public partial class DoorDragArea : Control
{
    /// <summary>
    /// 当前区域所占大小
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
    private int _areaSize;
    //拖拽松手后是否可以提交
    private bool _canComment = true;
    //开始拖拽时的区域
    private Vector2I _startDragRange;
    //原始缩放
    private Vector2 _originScale;

    public void SetDoorDragAreaNode(MapEditorTools.DoorToolTemplate node)
    {
        _node = node;
        _originScale = Scale;
        _defaultColor = _node.L_DoorArea.Instance.Color;
        _node.L_StartBtn.Instance.DragEvent += OnStartAreaDrag;
        _node.L_EndBtn.Instance.DragEvent += OnEndAreaDrag;
        
        SetDoorAreaSize(GameConfig.TileCellSize * 4);
        SetDoorAreaDirection(DoorDirection.N);
    }

    /// <summary>
    /// 设置门区域的方向
    /// </summary>
    public void SetDoorAreaDirection(DoorDirection direction)
    {
        Direction = direction;
        if (direction == DoorDirection.N)
        {
            _originScale = new Vector2(1, 1);
            RotationDegrees = 0;
        }
        else if (direction == DoorDirection.E)
        {
            _originScale = new Vector2(1, 1);
            RotationDegrees = 90;
        }
        else if (direction == DoorDirection.S)
        {
            _originScale = new Vector2(-1, 1);
            RotationDegrees = 180;
        }
        else
        {
            _originScale = new Vector2(-1, 1);
            RotationDegrees = 270;
        }
    }
    
    /// <summary>
    /// 设置位置和缩放, 用于跟随地图
    /// </summary>
    public void SetDoorAreaTransform(Vector2 pos, Vector2 scale)
    {
        Position = pos;
        Scale = _originScale * scale;
    }
    
    /// <summary>
    /// 获取门区域所占范围, 单位: 像素, 返回的 Vector2I 的 x 代表起始坐标, y 代表结束坐标
    /// </summary>
    /// <returns></returns>
    public Vector2I GetDoorAreaRange()
    {
        return new Vector2I((int)_node.L_StartBtn.Instance.Position.X, _areaSize);
    }

    /// <summary>
    /// 设置门区域所占范围,
    /// </summary>
    public void SetDoorAreaRange(int start, int size)
    {
        var startPosition = _node.L_StartBtn.Instance.Position;
        startPosition.X = start;
        _node.L_StartBtn.Instance.Position = startPosition;
        
        SetDoorAreaSize(size);
    }
    
    /// <summary>
    /// 设置区域大小
    /// </summary>
    public void SetDoorAreaSize(int value)
    {
        if (_areaSize != value)
        {
            _areaSize = value;
            RefreshArea();
        }
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
}