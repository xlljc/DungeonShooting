namespace UI.WeaponRoulette;

/// <summary>
/// Ui代码, 该类是根据ui场景自动生成的, 请不要手动编辑该类, 以免造成代码丢失
/// </summary>
public abstract partial class WeaponRoulette : UiBase
{
    /// <summary>
    /// 使用 Instance 属性获取当前节点实例对象, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Icon
    /// </summary>
    public Icon L_Icon
    {
        get
        {
            if (_L_Icon == null) _L_Icon = new Icon((WeaponRoulettePanel)this, GetNode<Godot.Sprite2D>("Icon"));
            return _L_Icon;
        }
    }
    private Icon _L_Icon;


    public WeaponRoulette() : base(nameof(WeaponRoulette))
    {
    }

    public sealed override void OnInitNestedUi()
    {

    }

    /// <summary>
    /// 类型: <see cref="Godot.Sprite2D"/>, 路径: WeaponRoulette.Icon
    /// </summary>
    public class Icon : UiNode<WeaponRoulettePanel, Godot.Sprite2D, Icon>
    {
        public Icon(WeaponRoulettePanel uiPanel, Godot.Sprite2D node) : base(uiPanel, node) {  }
        public override Icon Clone() => new (UiPanel, (Godot.Sprite2D)Instance.Duplicate());
    }


    /// <summary>
    /// 场景中唯一名称的节点, 节点类型: <see cref="Godot.Sprite2D"/>, 节点路径: WeaponRoulette.Icon
    /// </summary>
    public Icon S_Icon => L_Icon;

}
