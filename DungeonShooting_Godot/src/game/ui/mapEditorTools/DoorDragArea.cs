using Godot;

namespace UI.MapEditorTools;

public partial class DoorDragArea : Control
{
    /// <summary>
    /// 当前区域所占大小
    /// </summary>
    public Vector2 AreaSize => new Vector2(_areaSize, GameConfig.TileCellSize * 2);

    private MapEditorTools.DoorToolTemplate _node;
    private Vector2 _startTempPos;
    private Vector2 _endTempPos;
    //区域大小
    private float _areaSize;
    
    public void SetDoorDragAreaNode(MapEditorTools.DoorToolTemplate node)
    {
        _node = node;
        _areaSize = node.L_DoorArea.Instance.Size.X;
        _node.L_StartBtn.Instance.DragEvent += OnStartAreaDrag;
        _node.L_EndBtn.Instance.DragEvent += OnEndAreaDrag;
    }

    public void SetDoorAreaTransform(Vector2 pos, Vector2 scale)
    {
        Position = pos;
        Scale = scale;
    }
    
    /// <summary>
    /// 设置区域大小
    /// </summary>
    public void SetDoorAreaSize(float value)
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
        position.X = _node.L_StartBtn.Instance.Position.X;
        colorRect.Position = position;
        colorRect.Size = AreaSize;

        var position2 = _node.L_EndBtn.Instance.Position;
        position2.X = position.X + AreaSize.X;
        _node.L_EndBtn.Instance.Position = position2;
    }
    
    private void OnStartAreaDrag(DragState dragState, Vector2 offset)
    {
        if (dragState == DragState.DragStart)
        {
            _startTempPos = _node.L_StartBtn.Instance.Position;
        }
        else if (dragState == DragState.DragMove)
        {
            var position = _startTempPos;
            position.X = position.X += offset.X;
            _node.L_StartBtn.Instance.Position = position;

            //刷新区域位置
            SetDoorAreaSize(_node.L_EndBtn.Instance.Position.X - position.X);
        }
    }
    
    private void OnEndAreaDrag(DragState dragState, Vector2 offset)
    {
        if (dragState == DragState.DragStart)
        {
            _endTempPos = _node.L_EndBtn.Instance.Position;
        }
        else if (dragState == DragState.DragMove)
        {
            var position = _endTempPos;
            position.X = position.X += offset.X;
            //_node.L_EndBtn.Instance.Position = position;
            
            //刷新区域位置
            SetDoorAreaSize(position.X - _node.L_StartBtn.Instance.Position.X);
        }
    }
}