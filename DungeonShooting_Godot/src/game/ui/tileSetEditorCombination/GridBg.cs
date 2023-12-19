using Godot;

namespace UI.TileSetEditorCombination;

public abstract partial class GridBg<T> : ColorRect, IUiNodeScript where T : IUiNode
{
    public ColorRect Grid { get; protected set; }
    public Control ContainerRoot { get; protected set; }
    public T UiNode { get; private set; }
    
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
                Shrink();
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
            {
                //放大
                Magnify();
            }
        }
    }
    
    //缩小
    private void Shrink()
    {
        if (Utils.DoShrinkByMousePosition(ContainerRoot, 0.2f))
        {
            SetGridTransform(ContainerRoot.Position, ContainerRoot.Scale.X);
        }
    }
    //放大
    private void Magnify()
    {
        if (Utils.DoMagnifyByMousePosition(ContainerRoot, 20))
        {
            SetGridTransform(ContainerRoot.Position, ContainerRoot.Scale.X);
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
        Grid.Material.SetShaderMaterialParameter(ShaderParamNames.Size, Size);
        SetGridTransform(ContainerRoot.Position, ContainerRoot.Scale.X);
    }
    
    //设置网格位置和缩放
    private void SetGridTransform(Vector2 pos, float scale)
    {
        Grid.Material.SetShaderMaterialParameter(ShaderParamNames.GridSize, GameConfig.TileCellSize * scale);
        Grid.Material.SetShaderMaterialParameter(ShaderParamNames.Offset, -pos);
    }
    
}