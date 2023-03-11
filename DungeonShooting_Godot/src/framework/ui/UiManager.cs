
using Godot;

public static partial class UiManager
{
    private static bool _init = false;
    
    public static void Init()
    {
        if (_init)
        {
            return;
        }

        _init = true;
    }

    public static UiBase OpenUi(string resourcePath)
    {
        return null;
    }

    public static T OpenUi<T>(string resourcePath) where T : UiBase
    {
        return (T)OpenUi(resourcePath);
    }
}