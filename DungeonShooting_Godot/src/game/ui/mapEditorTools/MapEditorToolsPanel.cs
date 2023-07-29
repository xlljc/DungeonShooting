using System;
using System.Collections.Generic;
using Godot;

namespace UI.MapEditorTools;

public partial class MapEditorToolsPanel : MapEditorTools
{
    /// <summary>
    /// 鼠标悬停区域
    /// </summary>
    public DoorHoverArea ActiveHoverArea { get; private set; }

    /// <summary>
    /// 所属编辑器Tile对象
    /// </summary>
    public MapEditor.MapEditor.TileMap EditorMap { get; set; }

    private List<DoorToolTemplate> _doorTools = new List<DoorToolTemplate>();

    public override void OnCreateUi()
    {
        S_N_HoverArea.Instance.Init(this, DoorDirection.N);
        S_S_HoverArea.Instance.Init(this, DoorDirection.S);
        S_W_HoverArea.Instance.Init(this, DoorDirection.W);
        S_E_HoverArea.Instance.Init(this, DoorDirection.E);
        S_DoorToolRoot.Instance.RemoveChild(S_DoorToolTemplate.Instance);
    }

    public override void OnShowUi()
    {
        S_PenTool.Instance.EmitSignal(BaseButton.SignalName.Pressed);
    }

    public override void OnDestroyUi()
    {
        S_DoorToolTemplate.Instance.QueueFree();
    }

    public override void Process(float delta)
    {
        S_HoverPreviewRoot.Instance.Visible = ActiveHoverArea != null && !DoorHoverArea.IsDrag;
    }

    public DoorHoverArea GetDoorHoverAreaByDir(DoorDirection direction)
    {
        switch (direction)
        {
            case DoorDirection.E: return S_E_HoverArea.Instance;
            case DoorDirection.N: return S_N_HoverArea.Instance;
            case DoorDirection.W: return S_W_HoverArea.Instance;
            case DoorDirection.S: return S_S_HoverArea.Instance;
        }
        return null;
    }

    /// <summary>
    /// 设置活动的鼠标悬停的区域
    /// </summary>
    public void SetActiveHoverArea(DoorHoverArea hoverArea)
    {
        ActiveHoverArea = hoverArea;
        if (hoverArea != null)
        {
            S_HoverPreviewRoot.Instance.Reparent(hoverArea.GetParent(), false);
        }
        else
        {
            S_HoverPreviewRoot.Instance.Reparent(S_DoorToolRoot.Instance, false);
        }
    }
    
    /// <summary>
    /// 创建门区域设置工具
    /// </summary>
    /// <param name="position">原点坐标, 单位: 像素</param>
    /// <param name="direction">方向</param>
    /// <param name="start">起始位置, 单位: 像素</param>
    /// <param name="size">区域大小, 单位: 像素</param>
    public DoorToolTemplate CreateDoorTool(Vector2 position, DoorDirection direction, int start, int size)
    {
        var inst = CreateDoorToolInstance(GetDoorHoverAreaByDir(direction));
        inst.Instance.SetDoorAreaPosition(position);
        inst.Instance.SetDoorAreaDirection(direction);
        inst.Instance.SetDoorAreaRange(start, size);
        return inst;
    }

    /// <summary>
    /// 创建拖拽状态下的门区域工具, 用于创建门区域
    /// </summary>
    /// <param name="position">原点坐标, 单位: 像素</param>
    /// <param name="direction">方向</param>
    /// <param name="start">起始位置, 单位: 像素</param>
    /// <param name="onSubmit">成功提交时回调, 参数1为方向, 参数2为起始点, 参数3为大小</param>
    /// <param name="onCancel">取消提交时调用</param>
    public DoorToolTemplate CreateDragDoorTool(Vector2 position, DoorDirection direction, int start,
        Action<DoorDirection, int, int> onSubmit, Action onCancel)
    {
        var inst = CreateDoorToolInstance(GetDoorHoverAreaByDir(direction));
        inst.Instance.SetDoorAreaPosition(position);
        inst.Instance.SetDoorAreaDirection(direction);
        inst.Instance.SetDoorAreaRange(start, 0);
        inst.Instance.MakeDragMode(onSubmit, () =>
        {
            RemoveDoorTool(inst);
            onCancel();
        });
        return inst;
    }

    /// <summary>
    /// 移除门区域设置工具
    /// </summary>
    public void RemoveDoorTool(DoorToolTemplate toolInstance)
    {
        _doorTools.Remove(toolInstance);
        toolInstance.Instance.QueueFree();
    }

    /// <summary>
    /// 设置门区域工具的大小和缩放
    /// </summary>
    /// <param name="pos">坐标</param>
    /// <param name="scale">缩放</param>
    public void SetDoorToolTransform(Vector2 pos, Vector2 scale)
    {
        S_DoorToolRoot.Instance.Position = pos;
        S_DoorToolRoot.Instance.Scale = scale;
    }

    /// <summary>
    /// 设置鼠标悬停区域位置和大小
    /// </summary>
    /// <param name="position">房间起始点, 单位: 格</param>
    /// <param name="size">房间大小, 单位: 格</param>
    public void SetDoorHoverToolTransform(Vector2I position, Vector2I size)
    {
        position *= GameConfig.TileCellSize;
        size *= GameConfig.TileCellSize;
        S_N_HoverRoot.Instance.Position = position + GameConfig.TileCellSizeVector2I;
        S_E_HoverRoot.Instance.Position = new Vector2(position.X + size.X - GameConfig.TileCellSize, position.Y + GameConfig.TileCellSize);
        S_S_HoverRoot.Instance.Position = new Vector2(position.X + GameConfig.TileCellSize, position.Y + size.Y - GameConfig.TileCellSize);
        S_W_HoverRoot.Instance.Position = position + GameConfig.TileCellSizeVector2I;
        
        S_N_HoverArea.Instance.Size = new Vector2(size.X - GameConfig.TileCellSize * 2, S_N_HoverArea.Instance.Size.Y);
        S_E_HoverArea.Instance.Size = new Vector2(size.Y - GameConfig.TileCellSize * 2, S_E_HoverArea.Instance.Size.Y);
        S_S_HoverArea.Instance.Size = new Vector2(size.X - GameConfig.TileCellSize * 2, S_S_HoverArea.Instance.Size.Y);
        S_W_HoverArea.Instance.Size = new Vector2(size.Y - GameConfig.TileCellSize * 2, S_W_HoverArea.Instance.Size.Y);
    }
    
    private DoorToolTemplate CreateDoorToolInstance(DoorHoverArea doorHoverArea)
    {
        var doorTool = S_DoorToolTemplate.Clone();
        S_DoorToolRoot.Instance.AddChild(doorTool.Instance);
        doorTool.Instance.SetDoorDragAreaNode(doorTool);
        doorTool.L_StartBtn.Instance.SetMapEditorToolsPanel(this);
        doorTool.L_EndBtn.Instance.SetMapEditorToolsPanel(this);
        _doorTools.Add(doorTool);
        return doorTool;
    }
}
