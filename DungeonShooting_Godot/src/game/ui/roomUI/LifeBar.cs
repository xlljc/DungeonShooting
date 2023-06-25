using System.Collections.Generic;
using Godot;

namespace UI.RoomUI;

public class LifeBar
{

    private RoomUI.UiNode_LifeBar _bar;
    private UiGrid<TextureRect, RoomUI.UiNode_Life, LifeIconEnum> _grid;
    private EventFactory _eventFactory;
    private bool _refreshFlag = false;

    public LifeBar(RoomUI.UiNode_LifeBar lifeBar)
    {
        _bar = lifeBar;
        var uiNodeLife = lifeBar.L_Life;

        _grid = new UiGrid<TextureRect, RoomUI.UiNode_Life, LifeIconEnum>(uiNodeLife, typeof(LifeCell), 20, 1, 2);


        // _grid.Add(LifeIconEnum.FullHeart);
        // _grid.Add(LifeIconEnum.HalfHeart);
        // _grid.Add(LifeIconEnum.EmptyHeart);
        // _grid.Add(LifeIconEnum.FullShield);
        // _grid.Add(LifeIconEnum.EmptyShield);
    }

    public void OnShow()
    {
        _eventFactory = EventManager.CreateEventFactory();
        _eventFactory.AddEventListener(EventEnum.OnPlayerHpChange, o => RefreshLife());
        _eventFactory.AddEventListener(EventEnum.OnPlayerMaxHpChange, o => RefreshLife());
        _eventFactory.AddEventListener(EventEnum.OnPlayerShieldChange, o => RefreshLife());
        _eventFactory.AddEventListener(EventEnum.OnPlayerMaxShieldChange, o => RefreshLife());
    }

    public void OnHide()
    {
        _eventFactory.RemoveAllEventListener();
    }

    public void Process(float delta)
    {
        if (_refreshFlag)
        {
            _refreshFlag = false;
            HandlerRefreshLife();
        }
    }
    
    public void RefreshLife()
    {
        _refreshFlag = true;
    }

    private void HandlerRefreshLife()
    {
        var player = Player.Current;
        if (player.MaxHp % 2 != 0)
        {
            GD.PrintErr("玩家血量不是偶数!");
        }
        
        var list = new List<LifeIconEnum>();
        for (var i = 0; i < player.MaxHp / 2; i++)
        {
            if (player.Hp >= i * 2 + 2)
            {
                list.Add(LifeIconEnum.FullHeart);
            }
            else if (player.Hp >= i * 2 + 1)
            {
                list.Add(LifeIconEnum.HalfHeart);
            }
            else
            {
                list.Add(LifeIconEnum.EmptyHeart);
            }
        }

        for (var i = 0; i < player.MaxShield; i++)
        {
            if (player.Shield >= i + 1)
            {
                list.Add(LifeIconEnum.FullShield);
            }
            else
            {
                list.Add(LifeIconEnum.EmptyShield);
            }
        }
        
        //var maxHp
        _grid.SetDataList(list.ToArray());
    }

}