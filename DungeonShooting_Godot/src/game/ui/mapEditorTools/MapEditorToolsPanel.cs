using System;
using System.Collections.Generic;
using Godot;
using UI.MapEditor;

namespace UI.MapEditorTools;

public partial class MapEditorToolsPanel : MapEditorTools
{
    public class ToolBtnData
    {
        public bool CanSelect = false;
        public string Icon;
        public Action OnClick;

        public ToolBtnData(bool canSelect, string icon, Action onClick)
        {
            CanSelect = canSelect;
            Icon = icon;
            OnClick = onClick;
        }
    }
    
    /// <summary>
    /// 鼠标悬停区域
    /// </summary>
    public DoorHoverArea ActiveHoverArea { get; private set; }

    /// <summary>
    /// 所属编辑器Tile对象
    /// </summary>
    public MapEditor.MapEditor.TileMap EditorMap { get; set; }

    private List<DoorToolTemplate> _doorTools = new List<DoorToolTemplate>();
    private UiGrid<ToolButton, ToolBtnData> _toolGrid;

    public override void OnCreateUi()
    {
        S_N_HoverArea.Instance.Init(this, DoorDirection.N);
        S_S_HoverArea.Instance.Init(this, DoorDirection.S);
        S_W_HoverArea.Instance.Init(this, DoorDirection.W);
        S_E_HoverArea.Instance.Init(this, DoorDirection.E);
        S_DoorToolRoot.Instance.RemoveChild(S_DoorToolTemplate.Instance);

        _toolGrid = new UiGrid<ToolButton, ToolBtnData>(S_ToolButton, typeof(ToolButtonCell));
        _toolGrid.SetColumns(10);
        //拖拽按钮
        _toolGrid.Add(new ToolBtnData(true, ResourcePath.resource_sprite_ui_commonIcon_DragTool_png, () =>
        {
            EventManager.EmitEvent(EventEnum.OnSelectDragTool);
        }));
        //画笔按钮
        _toolGrid.Add(new ToolBtnData(true, ResourcePath.resource_sprite_ui_commonIcon_PenTool_png, () =>
        {
            EventManager.EmitEvent(EventEnum.OnSelectPenTool);
        }));
        //绘制区域按钮
        _toolGrid.Add(new ToolBtnData(true, ResourcePath.resource_sprite_ui_commonIcon_AreaTool_png, () =>
        {
            EventManager.EmitEvent(EventEnum.OnSelectRectTool);
        }));
        //编辑门区域按钮
        _toolGrid.Add(new ToolBtnData(true, ResourcePath.resource_sprite_ui_commonIcon_DoorTool_png, () =>
        {
            EventManager.EmitEvent(EventEnum.OnSelectDoorTool);
        }));
        //聚焦按钮
        _toolGrid.Add(new ToolBtnData(false, ResourcePath.resource_sprite_ui_commonIcon_CenterTool_png, () =>
        {
            EventManager.EmitEvent(EventEnum.OnClickCenterTool);
        }));
        _toolGrid.SelectIndex = 1;
    }

    public override void OnShowUi()
    {
        EventManager.EmitEvent(EventEnum.OnClickCenterTool);
    }

    public override void OnDestroyUi()
    {
        S_DoorToolTemplate.Instance.QueueFree();
        _toolGrid.Destroy();
    }

    public override void Process(float delta)
    {
        S_HoverPreviewRoot.Instance.Visible = ActiveHoverArea != null && !DoorHoverArea.IsDrag;
        if (EditorMap.Instance.MouseType == EditorTileMap.MouseButtonType.Door)
        {
            S_DoorToolRoot.Instance.Modulate = new Color(1, 1, 1, 1);
        }
        else
        {
            S_DoorToolRoot.Instance.Modulate = new Color(1, 1, 1, 0.4f);
        }
    }

    public DoorHoverArea GetDoorHoverArea(DoorDirection direction)
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
    
    public Control GetDoorHoverAreaRoot(DoorDirection direction)
    {
        switch (direction)
        {
            case DoorDirection.E: return S_E_HoverRoot.Instance;
            case DoorDirection.N: return S_N_HoverRoot.Instance;
            case DoorDirection.W: return S_W_HoverRoot.Instance;
            case DoorDirection.S: return S_S_HoverRoot.Instance;
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
    /// <param name="doorAreaInfo">门区域数据</param>
    public DoorToolTemplate CreateDoorTool(DoorAreaInfo doorAreaInfo)
    {
        var doorHoverArea = GetDoorHoverArea(doorAreaInfo.Direction);
        var inst = CreateDoorToolInstance(doorHoverArea);
        inst.Instance.DoorAreaInfo = doorAreaInfo;
        inst.Instance.SetDoorAreaPosition(GetDoorHoverAreaRoot(doorAreaInfo.Direction).Position);
        inst.Instance.SetDoorAreaRange(doorAreaInfo.Start, doorAreaInfo.End);
        return inst;
    }

    /// <summary>
    /// 创建拖拽状态下的门区域工具, 用于创建门区域
    /// </summary>
    /// <param name="doorHoverArea">悬停区域</param>
    /// <param name="start">起始位置, 单位: 像素</param>
    /// <param name="onSubmit">成功提交时回调, 参数1为方向, 参数2为起始点, 参数3为大小</param>
    /// <param name="onCancel">取消提交时调用</param>
    public DoorToolTemplate CreateDragDoorTool(DoorHoverArea doorHoverArea, int start,
        Action<DoorDirection, int, int> onSubmit, Action onCancel)
    {
        var inst = CreateDoorToolInstance(doorHoverArea);
        inst.Instance.SetDoorAreaPosition(GetDoorHoverAreaRoot(doorHoverArea.Direction).Position);
        inst.Instance.SetDoorAreaRange(start, start);
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
        if (toolInstance.Instance.DoorAreaInfo != null)
        {
            EditorMap.Instance.RemoveDoorArea(toolInstance.Instance.DoorAreaInfo);
        }
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

        var nPos1 = S_N_HoverRoot.Instance.Position;
        var ePos1 = S_E_HoverRoot.Instance.Position;
        var sPos1 = S_S_HoverRoot.Instance.Position;
        var wPos1 = S_W_HoverRoot.Instance.Position;
        var nPos2 = position + GameConfig.TileCellSizeVector2I;
        var ePos2 = new Vector2(position.X + size.X - GameConfig.TileCellSize, position.Y + GameConfig.TileCellSize);
        var sPos2 = new Vector2(position.X + GameConfig.TileCellSize, position.Y + size.Y - GameConfig.TileCellSize);
        var wPos2 = position + GameConfig.TileCellSizeVector2I;

        var nSize2 = new Vector2(size.X - GameConfig.TileCellSize * 2, S_N_HoverArea.Instance.Size.Y);
        var eSize2 = new Vector2(size.Y - GameConfig.TileCellSize * 2, S_E_HoverArea.Instance.Size.Y);
        var sSize2 = new Vector2(size.X - GameConfig.TileCellSize * 2, S_S_HoverArea.Instance.Size.Y);
        var wSize2 = new Vector2(size.Y - GameConfig.TileCellSize * 2, S_W_HoverArea.Instance.Size.Y);
        
        S_N_HoverRoot.Instance.Position = nPos2;
        S_E_HoverRoot.Instance.Position = ePos2;
        S_S_HoverRoot.Instance.Position = sPos2;
        S_W_HoverRoot.Instance.Position = wPos2;
        
        S_N_HoverArea.Instance.Size = nSize2;
        S_E_HoverArea.Instance.Size = eSize2;
        S_S_HoverArea.Instance.Size = sSize2;
        S_W_HoverArea.Instance.Size = wSize2;
        
        //调整门区域
        for (var i = 0; i < _doorTools.Count; i++)
        {
            var doorTool = _doorTools[i];
            var direction = doorTool.Instance.Direction;
            var areaRoot = GetDoorHoverAreaRoot(direction);
            var doorAreaRange = doorTool.Instance.GetDoorAreaRange();
            doorTool.Instance.SetDoorAreaPosition(areaRoot.Position);

            if (direction == DoorDirection.N)
            {
                var hOffset = (int)(nPos2.X - nPos1.X);
                doorAreaRange.X -= hOffset;
                doorAreaRange.Y -= hOffset;

                if (doorAreaRange.X >= 0 && doorAreaRange.Y <= nSize2.X) //允许提交
                {
                    doorTool.Instance.SetDoorAreaRange(doorAreaRange.X, doorAreaRange.Y);
                    if (doorTool.Instance.DoorAreaInfo != null)
                    {
                        doorTool.Instance.DoorAreaInfo.Start = doorAreaRange.X;
                        doorTool.Instance.DoorAreaInfo.End = doorAreaRange.Y;
                    }
                }
                else //如果超出区域, 则删除
                {
                    RemoveDoorTool(doorTool);
                    i--;
                }
            }
            else if (direction == DoorDirection.S)
            {
                var hOffset = (int)(sPos2.X - sPos1.X);
                doorAreaRange.X -= hOffset;
                doorAreaRange.Y -= hOffset;

                if (doorAreaRange.X >= 0 && doorAreaRange.Y <= sSize2.X) //允许提交
                {
                    doorTool.Instance.SetDoorAreaRange(doorAreaRange.X, doorAreaRange.Y);
                    if (doorTool.Instance.DoorAreaInfo != null)
                    {
                        doorTool.Instance.DoorAreaInfo.Start = doorAreaRange.X;
                        doorTool.Instance.DoorAreaInfo.End = doorAreaRange.Y;
                    }
                }
                else //如果超出区域, 则删除
                {
                    RemoveDoorTool(doorTool);
                    i--;
                }
            }
            else if (direction == DoorDirection.E)
            {
                var vOffset = (int)(ePos2.Y - ePos1.Y);
                doorAreaRange.X -= vOffset;
                doorAreaRange.Y -= vOffset;

                if (doorAreaRange.X >= 0 && doorAreaRange.Y <= eSize2.X) //允许提交
                {
                    doorTool.Instance.SetDoorAreaRange(doorAreaRange.X, doorAreaRange.Y);
                    if (doorTool.Instance.DoorAreaInfo != null)
                    {
                        doorTool.Instance.DoorAreaInfo.Start = doorAreaRange.X;
                        doorTool.Instance.DoorAreaInfo.End = doorAreaRange.Y;
                    }
                }
                else //如果超出区域, 则删除
                {
                    RemoveDoorTool(doorTool);
                    i--;
                }
            }
            else if (direction == DoorDirection.W)
            {
                var vOffset = (int)(wPos2.Y - wPos1.Y);
                doorAreaRange.X -= vOffset;
                doorAreaRange.Y -= vOffset;

                if (doorAreaRange.X >= 0 && doorAreaRange.Y <= wSize2.X) //允许提交
                {
                    doorTool.Instance.SetDoorAreaRange(doorAreaRange.X, doorAreaRange.Y);
                    if (doorTool.Instance.DoorAreaInfo != null)
                    {
                        doorTool.Instance.DoorAreaInfo.Start = doorAreaRange.X;
                        doorTool.Instance.DoorAreaInfo.End = doorAreaRange.Y;
                    }
                }
                else //如果超出区域, 则删除
                {
                    RemoveDoorTool(doorTool);
                    i--;
                }
            }
        }
    }
    
    private DoorToolTemplate CreateDoorToolInstance(DoorHoverArea doorHoverArea)
    {
        var doorTool = S_DoorToolTemplate.Clone();
        S_DoorToolRoot.Instance.AddChild(doorTool.Instance);
        doorTool.Instance.SetDoorDragAreaNode(doorTool);
        doorTool.L_StartBtn.Instance.SetMapEditorToolsPanel(this);
        doorTool.L_EndBtn.Instance.SetMapEditorToolsPanel(this);
        doorTool.Instance.SetDoorHoverArea(doorHoverArea);
        _doorTools.Add(doorTool);
        return doorTool;
    }
}
