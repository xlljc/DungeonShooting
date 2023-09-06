using System.Collections.Generic;

namespace UI.MapEditorCreateMark;

public partial class OptionAttribute : AttributeBase
{
    private MapEditorCreateMark.OptionBar _optionBar;
    private int _index = 0;
    private Dictionary<int, int> _map = new Dictionary<int, int>();
    
    public override void SetUiNode(IUiNode uiNode)
    {
        _optionBar = (MapEditorCreateMark.OptionBar)uiNode;
    }

    public override void OnDestroy()
    {
        
    }
    
    public override string GetAttributeValue()
    {
        return _map[_optionBar.L_OptionInput.Instance.Selected].ToString();
    }

    /// <summary>
    /// 根据值选中选项
    /// </summary>
    public void SetSelectItem(int value)
    {
        foreach (var keyValuePair in _map)
        {
            if (keyValuePair.Value == value)
            {
                _optionBar.L_OptionInput.Instance.Select(keyValuePair.Key);
                return;
            }
        }
        _optionBar.L_OptionInput.Instance.Select(-1);
    }
    
    /// <summary>
    /// 添加选项
    /// </summary>
    /// <param name="label">选项显示文本</param>
    /// <param name="value">选项值</param>
    public void AddItem(string label, int value)
    {
        var index = _index++;
        _map.Add(index, value);
        _optionBar.L_OptionInput.Instance.AddItem(label, index);
    }
}