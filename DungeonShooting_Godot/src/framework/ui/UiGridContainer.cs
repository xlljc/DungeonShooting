using System;
using Godot;

/// <summary>
/// 用于 UiGrid 类
/// </summary>
public partial class UiGridContainer : GridContainer
{
    private Action _onReady;
    private Action<float> _onProcess;
    
    public UiGridContainer(Action onReady, Action<float> onProcess)
    {
        _onReady = onReady;
        _onProcess = onProcess;
    }

    public override void _Ready()
    {
        _onReady();
    }

    public override void _Process(double delta)
    {
        _onProcess((float)delta);
    }
}