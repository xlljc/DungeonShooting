using System;
using Config;

namespace UI.MapEditorCreateMark;

public class MarkObjectCell : UiCell<MapEditorCreateMark.MarkObject, ExcelConfig.ActivityObject>
{
    //是否展开
    private bool _isExpand = false;

    private MapEditorCreateMark.ExpandPanel _expandPanel;
    
    public override void OnInit()
    {
        CellNode.L_HBoxContainer.L_ExpandButton.Instance.Pressed += OnExpandClick;
        CellNode.L_HBoxContainer.L_CenterContainer.L_DeleteButton.Instance.Pressed += OnDeleteClick;
    }

    public override void OnSetData(ExcelConfig.ActivityObject data)
    {
        if (string.IsNullOrEmpty(data.Icon))
        {
            CellNode.L_HBoxContainer.L_Icon.Instance.Visible = false;
        }
        else
        {
            CellNode.L_HBoxContainer.L_Icon.Instance.Visible = true;
            CellNode.L_HBoxContainer.L_Icon.Instance.Texture = ResourceManager.LoadTexture2D(data.Icon);
        }
        //物体Id
        CellNode.L_HBoxContainer.L_IdLabel.Instance.Text = data.Id;
        //物体名称
        CellNode.L_HBoxContainer.L_NameLabel.Instance.Text = data.Name;
        
        // 包含额外属性
        if (data.Type == 5)
        {
            if (_expandPanel == null)
            {
                CreateExpandPanel(data.Type);
            }
        }
    }

    public override void OnDisable()
    {
        if (_expandPanel != null)
        {
            _expandPanel.Instance.QueueFree();
            _expandPanel = null;
        }

        SetExpandState(false);
    }

    //点击删除按钮
    private void OnDeleteClick()
    {
        Grid.RemoveByIndex(Index);
    }

    //点击展开按钮
    private void OnExpandClick()
    {
        //展开图标
        SetExpandState(!_isExpand);
    }

    //设置展开状态
    private void SetExpandState(bool flag)
    {
        _isExpand = flag;
        if (_isExpand)
        {
            CellNode.L_HBoxContainer.L_ExpandButton.Instance.Icon =
                ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Down_png);
        }
        else
        {
            CellNode.L_HBoxContainer.L_ExpandButton.Instance.Icon =
                ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Right_png);
        }

        if (_expandPanel != null)
        {
            _expandPanel.Instance.Visible = _isExpand;
        }
    }

    private void CreateExpandPanel(int type)
    {
        if (_expandPanel != null)
        {
            throw new Exception("已经创建过ExpandPanel, 不能重复创建!");
        }
        
        _expandPanel = CellNode.UiPanel.S_ExpandPanel.Clone();
        _expandPanel.Instance.Visible = _isExpand;
        CellNode.AddChild(_expandPanel);

        if (type == 5) //武器类型
        {
            var numberBar1 = CellNode.UiPanel.S_NumberBar.Clone();
            numberBar1.L_AttName.Instance.Text = "弹夹弹药量：";
            numberBar1.Instance.Visible = true;
            var numberBar2 = CellNode.UiPanel.S_NumberBar.Clone();
            numberBar2.L_AttName.Instance.Text = "剩余弹药量：";
            numberBar2.Instance.Visible = true;
            _expandPanel.L_ExpandGrid.AddChild(numberBar1);
            _expandPanel.L_ExpandGrid.AddChild(numberBar2);
        }
    }
}