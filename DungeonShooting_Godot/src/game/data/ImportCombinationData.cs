
using System.IO;
using Godot;

/// <summary>
/// 导入的组合图块数据
/// </summary>
public class ImportCombinationData
{
    /// <summary>
    /// 预览图
    /// </summary>
    public ImageTexture PreviewTexture { get; set; }
    /// <summary>
    /// 组合图块数据
    /// </summary>
    public TileCombinationInfo CombinationInfo { get; set; }

    private static Image MissingImage;

    public ImportCombinationData(ImageTexture previewTexture, TileCombinationInfo combinationInfo)
    {
        PreviewTexture = previewTexture;
        CombinationInfo = combinationInfo;
    }
    
    /// <summary>
    /// 更新组合预览图
    /// </summary>
    public void UpdatePreviewTexture(Image src)
    {
        using (var image = GetPreviewTexture(src, CombinationInfo.Cells, CombinationInfo.Positions))
        {
            PreviewTexture.SetImage(image);
        }
    }
    
    /// <summary>
    /// 获取组合图块预览图数据
    /// </summary>
    /// <param name="src">地块纹理</param>
    /// <param name="cells">图块在地块中的位置</param>
    /// <param name="positions">图块位置</param>
    public static Image GetPreviewTexture(Image src, SerializeVector2[] cells, SerializeVector2[] positions)
    {
        var srcSize = src.GetSize();
        var rect = Utils.CalcTileRect(positions);
        var rectSize = rect.Size;
        var image = Image.Create(rectSize.X + 4, rectSize.Y + 4, false, Image.Format.Rgba8);
        for (var i = 0; i < cells.Length; i++)
        {
            var cell = cells[i].AsVector2I();
            var pos = positions[i].AsVector2I();
            //判断是否超出纹理范围
            if (cell.X + GameConfig.TileCellSize > srcSize.X || cell.Y + GameConfig.TileCellSize > srcSize.Y) //超出范围
            {
                if (MissingImage == null)
                {
                    MissingImage = ResourceManager.LoadTexture2D(ResourcePath.resource_sprite_ui_commonIcon_Missing_png).GetImage();
                    MissingImage.Convert(Image.Format.Rgba8);
                }
                image.BlendRect(MissingImage, new Rect2I(Vector2I.Zero, GameConfig.TileCellSizeVector2I), pos + new Vector2I(2, 2));
            }
            else
            {
                image.BlendRect(src, new Rect2I(cell, GameConfig.TileCellSizeVector2I), pos + new Vector2I(2, 2));
            }
        }
        return image;
    }
}