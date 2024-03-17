using Godot;

namespace UI.Encyclopedia;

public class TabCell : UiCell<Encyclopedia.TabButton, TabData>
{
    //选中时页签显示的纹理
    private const string SelectTexture = ResourcePath.resource_sprite_ui_encyclopedia_TabSelect_png;
    private static Texture2D _selectTexture;

    private static Texture2D GetSelectTexture()
    {
        if (_selectTexture == null)
        {
            _selectTexture = ResourceManager.LoadTexture2D(SelectTexture);
        }
        
        return _selectTexture;
    }
    
    
    private float _startY;
    private Texture2D _originTexture;
    
    public override void OnInit()
    {
        _originTexture = CellNode.Instance.TextureNormal;
    }

    public override void OnSetData(TabData data)
    {
        CellNode.L_Icon.Instance.Texture = ResourceManager.LoadTexture2D(data.Icon);
        var position = CellNode.L_Icon.Instance.Position;
        _startY = position.Y;
    }

    public override void OnClick()
    {
        Grid.SelectIndex = Index;
    }

    public override void OnSelect()
    {
        CellNode.Instance.TextureNormal = GetSelectTexture();
        CellNode.L_Icon.Instance.Position = new Vector2(CellNode.L_Icon.Instance.Position.X, _startY - 12);
        CellNode.UiPanel.SelectTab(Data.Type);
    }

    public override void OnUnSelect()
    {
        CellNode.Instance.TextureNormal = _originTexture;
        CellNode.L_Icon.Instance.Position = new Vector2(CellNode.L_Icon.Instance.Position.X, _startY);
    }
}