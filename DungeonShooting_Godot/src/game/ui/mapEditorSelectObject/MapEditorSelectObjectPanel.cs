
using System;
using System.Collections.Generic;
using System.Linq;
using Config;
using Godot;

namespace UI.MapEditorSelectObject;

public partial class MapEditorSelectObjectPanel : MapEditorSelectObject
{
    /// <summary>
    /// 双击选中物体事件
    /// </summary>
    public event Action<ExcelConfig.ActivityBase> SelectObjectEvent;
    
    public class TypeButtonData
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 类型值
        /// </summary>
        public int Type;

        public TypeButtonData(string name, int type)
        {
            Name = name;
            Type = type;
        }
    }
    //类型网格组件
    private UiGrid<TypeButton, TypeButtonData> _typeGrid;
    //物体网格组件
    private UiGrid<ObjectButton, ExcelConfig.ActivityBase> _objectGrid;
    //允许出现在该面板中的物体类型
    private int[] _typeArray = new[] { 4, 5, 9 };
    
    public override void OnCreateUi()
    {
        S_Search.Instance.Pressed += OnSearch;
        
        _typeGrid = new UiGrid<TypeButton, TypeButtonData>(S_TypeButton, typeof(TypeButtonCell));
        _typeGrid.SetColumns(1);
        _typeGrid.SetHorizontalExpand(true);
        _typeGrid.SetCellOffset(new Vector2I(0, 5));
        
        _objectGrid = new UiGrid<ObjectButton, ExcelConfig.ActivityBase>(S_ObjectButton, typeof(ObjectButtonCell));
        _objectGrid.SetAutoColumns(true);
        _objectGrid.SetHorizontalExpand(true);
        _objectGrid.SetCellOffset(new Vector2I(10, 10));
    }

    public override void OnDestroyUi()
    {
        _typeGrid.Destroy();
        _objectGrid.Destroy();
    }

    /// <summary>
    /// 设置显示的物体类型
    /// </summary>
    public void SetShowType(ActivityType activityType)
    {
        _typeGrid.RemoveAll();
        if (activityType == ActivityType.None)
        {
            _typeGrid.Add(new TypeButtonData("所有", -1));
            _typeGrid.Add(new TypeButtonData(ActivityId.GetTypeName(ActivityType.Weapon), (int)ActivityType.Weapon));
            _typeGrid.Add(new TypeButtonData(ActivityId.GetTypeName(ActivityType.Prop), (int)ActivityType.Prop));
            _typeGrid.Add(new TypeButtonData(ActivityId.GetTypeName(ActivityType.Enemy), (int)ActivityType.Enemy));
        }
        else
        {
            _typeGrid.Add(new TypeButtonData(ActivityId.GetTypeName(activityType), (int)activityType));
        }
        _typeGrid.SelectIndex = 0;
    }

    /// <summary>
    /// 搜索对象
    /// </summary>
    public void OnSearch()
    {
        //类型
        int type;
        //名称
        var name = S_LineEdit.Instance.Text;
        var buttonData = _typeGrid.GetData(_typeGrid.SelectIndex);
        if (buttonData != null)
        {
            type = buttonData.Type;
        }
        else
        {
            type = -1;
        }

        //搜索结果
        var arr = new List<ExcelConfig.ActivityBase>();
        switch (type)
        {
            //全部类型
            case -1:
                arr.Add(PreinstallMarkManager.Enemy);
                arr.Add(PreinstallMarkManager.Prop);
                arr.Add(PreinstallMarkManager.Weapon);
                break;
            //随机武器
            case (int)ActivityType.Weapon:
                arr.Add(PreinstallMarkManager.Weapon);
                break;
            //随机道具
            case (int)ActivityType.Prop:
                arr.Add(PreinstallMarkManager.Prop);
                break;
            //随机敌人
            case (int)ActivityType.Enemy:
                arr.Add(PreinstallMarkManager.Enemy);
                break;
        }
        foreach (var o in ExcelConfig.ActivityBase_List)
        {
            if (o.ShowInMapEditor &&
                (string.IsNullOrEmpty(name) || o.Name.Contains(name) || o.Id.Contains(name)) &&
                (type < 0 ? _typeArray.Contains(o.Type) : o.Type == type))
            {
                arr.Add(o);
            }
        }
        
        _objectGrid.SetDataList(arr.ToArray());
    }

    /// <summary>
    /// 选中对象
    /// </summary>
    public void SelectCell(ExcelConfig.ActivityBase activityObject)
    {
        if (SelectObjectEvent != null)
        {
            SelectObjectEvent(activityObject);
        }
    }

    /// <summary>
    /// 获取选中的数据
    /// </summary>
    public ExcelConfig.ActivityBase GetSelectData()
    {
        return _objectGrid.SelectData;
    }
}
