using Godot;

namespace UI.RoomUI;

public class LifeCell : UiCell<TextureRect, RoomUI.UiNode_Life, LifeIconEnum>
{
    private int _type = -1;

    public override void OnSetData(LifeIconEnum data)
    {
        if (_type == (int)data)
        {
            return;
        }

        _type = (int)data;
        switch (data)
        {
            case LifeIconEnum.FullHeart:
                CellNode.Instance.Texture = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_ui_roomUI_Life_full_png);
                break;
            case LifeIconEnum.HalfHeart:
                CellNode.Instance.Texture = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_ui_roomUI_Life_half_png);
                break;
            case LifeIconEnum.EmptyHeart:
                CellNode.Instance.Texture = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_ui_roomUI_Life_empty_png);
                break;
            case LifeIconEnum.FullShield:
                CellNode.Instance.Texture = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_ui_roomUI_Shield_full_png);
                break;
            case LifeIconEnum.EmptyShield:
                CellNode.Instance.Texture = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_ui_roomUI_Shield_empty_png);
                break;
        }
    }
}