using System;
using System.Linq;
using Config;
using Godot;

namespace UI.MapEditorSelectObject;

public partial class MapEditorSelectObjectPanel : MapEditorSelectObject
{
    /// <summary>
    /// 双击选中物体事件
    /// </summary>
    public event Action<ExcelConfig.ActivityObject> SelectObjectEvent;
    
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
    private UiGrid<ObjectButton, ExcelConfig.ActivityObject> _objectGrid;
    //允许出现在该面板中的物体类型
    private int[] _typeArray = new[] { 4, 5, 9 };
    
    public override void OnCreateUi()
    {
        S_Search.Instance.Pressed += OnSearch;
        
        _typeGrid = new UiGrid<TypeButton, TypeButtonData>(S_TypeButton, typeof(TypeButtonCell));
        _typeGrid.SetColumns(1);
        _typeGrid.SetHorizontalExpand(true);
        _typeGrid.SetCellOffset(new Vector2I(0, 5));
        
        _objectGrid = new UiGrid<ObjectButton, ExcelConfig.ActivityObject>(S_ObjectButton, typeof(ObjectButtonCell));
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
        var arr = ExcelConfig.ActivityObject_List.Where(
            o =>
            {
                return o.ShowInMapEditor &&
                       (string.IsNullOrEmpty(name) || o.Name.Contains(name) || o.Id.Contains(name)) &&
                       (type < 0 ? _typeArray.Contains(o.Type) : o.Type == type);
            }
        ).ToArray();
        _objectGrid.SetDataList(arr);
    }

    /// <summary>
    /// 选中对象
    /// </summary>
    public void SelectCell(ExcelConfig.ActivityObject activityObject)
    {
        if (SelectObjectEvent != null)
        {
            SelectObjectEvent(activityObject);
        }
    }

    /// <summary>
    /// 获取选中的数据
    /// </summary>
    public ExcelConfig.ActivityObject GetSelectData()
    {
        return _objectGrid.SelectData;
    }
}
