
using System.Collections.Generic;
using Config;

/// <summary>
/// 液体笔刷管理类
/// </summary>
public static class LiquidBrushManager
{
    private static Dictionary<string, BrushImageData> _dictionary = new Dictionary<string, BrushImageData>();
    
    /// <summary>
    /// 根据 id 获取笔刷, 该 id 为 LiquidMaterial 表的 id
    /// </summary>
    public static BrushImageData GetBrush(string id)
    {
        if (!_dictionary.TryGetValue(id, out var brush))
        {
            brush = new BrushImageData(ExcelConfig.LiquidMaterial_Map[id]);
            _dictionary.Add(id, brush);
        }

        return brush;
    }

    /// <summary>
    /// 清除缓存笔刷数据
    /// </summary>
    public static void ClearData()
    {
        _dictionary.Clear();
    }
}