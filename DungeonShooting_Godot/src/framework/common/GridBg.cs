using Godot;

/// <summary>
/// 通用Ui网格背景组件, 包含可拖拽的容器根节点
/// </summary>
public abstract partial class GridBg<T> : ColorRect, IUiNodeScript where T : IUiNode
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
    }

    public virtual void OnDestroy()
    {
        
    }
    
    /// <summary>
    /// 当前Ui被显示出来时调用
    /// </summary>
    public void OnShow()
    {
        RefreshGridTrans();
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
        if (state == DragState.DragMove)
        {
            ContainerRoot.Position += pos;
            RefreshGridTrans();
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