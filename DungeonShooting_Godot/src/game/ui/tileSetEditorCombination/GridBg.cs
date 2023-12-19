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
        this.AddDragEventListener(DragButtonEnum.Middle, OnDrag);
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
        var offset = ContainerRoot.GetLocalMousePosition();
        var prevScale = ContainerRoot.Scale;
        var newScale = prevScale / 1.1f;
        if (newScale.LengthSquared() >= 0.5f)
        {
            ContainerRoot.Scale = newScale;
            var position = ContainerRoot.Position + offset * 0.1f * newScale;
            ContainerRoot.Position = position;
            SetGridTransform(position, newScale.X);
        }
    }
    //放大
    private void Magnify()
    {
        var offset = ContainerRoot.GetLocalMousePosition();
        var prevScale = ContainerRoot.Scale;
        var newScale = prevScale * 1.1f;
        if (newScale.LengthSquared() <= 2000)
        {
            ContainerRoot.Scale = newScale;
            var position = ContainerRoot.Position - offset * 0.1f * prevScale;
            ContainerRoot.Position = position;
            SetGridTransform(position, newScale.X);
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