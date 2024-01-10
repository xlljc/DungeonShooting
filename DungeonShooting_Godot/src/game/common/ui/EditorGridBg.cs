using Godot;

/// <summary>
/// 通用Ui网格背景组件, 包含可拖拽的容器根节点
/// </summary>
public abstract partial class EditorGridBg<T> : ColorRect, IUiNodeScript where T : IUiNode
{
    /// <summary>
    /// 可拖拽容器根节点
    /// </summary>
    public Control ContainerRoot { get; private set; }
    /// <summary>
    /// 显示网格的节点
    /// </summary>
    public ColorRect Grid { get; private set; }
    /// <summary>
    /// 当前对象绑定的Ui节点
    /// </summary>
    public T UiNode { get; private set; }
    
    private ShaderMaterial _gridMaterial;
    private bool _dragMoveFlag = false;
    
    /// <summary>
    /// 初始化节点数据
    /// </summary>
    /// <param name="containerRoot">可拖拽容器根节点</param>
    /// <param name="grid">当前对象绑定的Ui节点</param>
    public void InitNode(Control containerRoot, ColorRect grid)
    {
        ContainerRoot = containerRoot;
        Grid = grid;
        grid.MouseFilter = MouseFilterEnum.Ignore;
        _gridMaterial = ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Grid_tres, false);
        grid.Material = _gridMaterial;
    }
    
    public virtual void SetUiNode(IUiNode uiNode)
    {
        UiNode = (T)uiNode;
        this.AddDragListener(DragButtonEnum.Middle, OnDrag);
        Resized += RefreshGridTrans;
        //打开Ui时刷新网格
        UiNode.GetUiPanel().OnShowUiEvent += RefreshGridTrans;
    }

    public virtual void OnDestroy()
    {
        
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            AcceptEvent();
            if (mouseButton.ButtonIndex == MouseButton.WheelDown)
            {
                //缩小
                if (Utils.DoShrinkByMousePosition(ContainerRoot, 0.4f))
                {
                    SetGridTransform(ContainerRoot.Position, ContainerRoot.Scale.X);
                }
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
            {
                //放大
                if (Utils.DoMagnifyByMousePosition(ContainerRoot, 20))
                {
                    SetGridTransform(ContainerRoot.Position, ContainerRoot.Scale.X);
                }
            }
        }
    }
    
    //拖拽回调
    private void OnDrag(DragState state, Vector2 pos)
    {
        if (state == DragState.DragStart)
        {
            if (this.IsMouseInRect())
            {
                _dragMoveFlag = true;
            }
        }
        else if (state == DragState.DragMove)
        {
            if (_dragMoveFlag)
            {
                ContainerRoot.Position += pos;
                RefreshGridTrans();
            }
        }
        else
        {
            _dragMoveFlag = false;
        }
    }
    
    
    /// <summary>
    /// 刷新背景网格位置和缩放
    /// </summary>
    public void RefreshGridTrans()
    {
        _gridMaterial.SetShaderMaterialParameter(ShaderParamNames.Size, Size);
        SetGridTransform(ContainerRoot.Position, ContainerRoot.Scale.X);
    }
    
    //设置网格位置和缩放
    private void SetGridTransform(Vector2 pos, float scale)
    {
        _gridMaterial.SetShaderMaterialParameter(ShaderParamNames.GridSize, GameConfig.TileCellSize * scale);
        _gridMaterial.SetShaderMaterialParameter(ShaderParamNames.Offset, -pos);
    }
    
}