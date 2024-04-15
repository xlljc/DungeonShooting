using Godot;

namespace UI.WeaponRoulette;

public partial class WeaponSlot : Node2D, IUiNodeScript
{
    private WeaponRoulette.WeaponSlotNode _node;
    private Weapon _weapon;
    
    public void SetUiNode(IUiNode uiNode)
    {
        _node = (WeaponRoulette.WeaponSlotNode)uiNode;
        _node.L_SlotAreaNode.Instance.AreaEntered += OnAreaEntered;
        _node.L_SlotAreaNode.Instance.AreaExited += OnAreaExited;
    }

    public void OnDestroy()
    {
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void ClearWeapon()
    {
        _weapon = null;
    }
    
    private void OnAreaEntered(Area2D other)
    {
        _node.UiPanel.ActiveWeapon = _weapon;
        _node.Instance.Scale = new Vector2(1.1f, 1.1f);
        _node.L_SlotUi.L_WeaponUi.L_WeaponIcon.Instance.Material.SetShaderMaterialParameter(ShaderParamNames.OutlineColor, Colors.White);
    }
    
    private void OnAreaExited(Area2D other)
    {
        if (_node.UiPanel.ActiveWeapon == _weapon)
        {
            _node.UiPanel.ActiveWeapon = null;
        }

        _node.Instance.Scale = Vector2.One;
        _node.L_SlotUi.L_WeaponUi.L_WeaponIcon.Instance.Material.SetShaderMaterialParameter(ShaderParamNames.OutlineColor, Colors.Black);
    }
}