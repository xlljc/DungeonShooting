using Godot;

namespace UI.EditorTileImage;

public partial class EditorTileImagePanel : EditorTileImage
{
    /// <summary>
    /// 起始X
    /// </summary>
    public int StartXValue { get; private set; }
    /// <summary>
    /// 起始Y
    /// </summary>
    public int StartYValue { get; private set; }
    /// <summary>
    /// 间距X
    /// </summary>
    public int OffsetXValue { get; private set; }
    /// <summary>
    /// 间距Y
    /// </summary>
    public int OffsetYValue { get; private set; }
    /// <summary>
    /// 横轴数量
    /// </summary>
    public int HCountValue { get; private set; }
    /// <summary>
    /// 纵轴数量
    /// </summary>
    public int VCountValue { get; private set; }
    /// <summary>
    /// 纹理大小
    /// </summary>
    public Vector2I ImageSize { get; private set; }
    /// <summary>
    /// 使用的Image对象
    /// </summary>
    public Image UseImage { get; private set; }
    
    private ImageTexture _texture;
    
    public override void OnCreateUi()
    {
        S_StartX.Instance.ValueChanged += (v) =>
        {
            StartXValue = (int)v;
            RefreshHVMaxCount();
        };
        S_StartY.Instance.ValueChanged += (v) =>
        {
            StartYValue = (int)v;
            RefreshHVMaxCount();
        };
        S_OffsetX.Instance.ValueChanged += (v) =>
        {
            OffsetXValue = (int)v;
            RefreshHVMaxCount();
        };
        S_OffsetY.Instance.ValueChanged += (v) =>
        {
            OffsetYValue = (int)v;
            RefreshHVMaxCount();
        };
        S_HCount.Instance.ValueChanged += (v) =>
        {
            HCountValue = (int)v;
        };
        S_VCount.Instance.ValueChanged += (v) =>
        {
            VCountValue = (int)v;
        };
    }

    public override void OnDestroyUi()
    {
        if (UseImage != null)
        {
            UseImage.Dispose();
            UseImage = null;
        }

        if (_texture != null)
        {
            _texture.Dispose();
            _texture = null;
        }
    }

    /// <summary>
    /// 初始化Ui数据
    /// </summary>
    public void InitData(Image image)
    {
        UseImage = image;
        ImageSize = image.GetSize();
        _texture = ImageTexture.CreateFromImage(image);
        S_TileSprite.Instance.Texture = _texture;
        S_Bg.Instance.DoFocus();
        S_TextureRoot.Instance.Size = image.GetSize();

        RefreshHVMaxCount();
        S_HCount.Instance.Value = S_HCount.Instance.MaxValue;
        S_VCount.Instance.Value = S_VCount.Instance.MaxValue;
    }

    /// <summary>
    /// 获取处理后的图像
    /// </summary>
    public Image GetImage()
    {
        var image = Image.Create(HCountValue * GameConfig.TileCellSize, VCountValue * GameConfig.TileCellSize, false, Image.Format.Rgba8);
        var start = new Vector2I(StartXValue, StartYValue);
        for (int i = 0; i < HCountValue; i++)
        {
            for (int j = 0; j < VCountValue; j++)
            {
                var offset = new Vector2I(i * (OffsetXValue + GameConfig.TileCellSize), j * (OffsetYValue + GameConfig.TileCellSize));
                image.BlitRect(UseImage,
                    new Rect2I(
                        start + offset,
                        GameConfig.TileCellSizeVector2I
                    ),
                    new Vector2I(i * GameConfig.TileCellSize, j * GameConfig.TileCellSize)
                );
            }
        }

        return image;
    }
    
    //更新最大计数的水平和垂直方向的值
    private void RefreshHVMaxCount()
    {
        var hv = Mathf.FloorToInt(((float)ImageSize.X - StartXValue + OffsetXValue) / (GameConfig.TileCellSize + OffsetXValue));
        var vv = Mathf.FloorToInt(((float)ImageSize.Y - StartYValue + OffsetYValue) / (GameConfig.TileCellSize + OffsetYValue));
        S_HCount.Instance.MaxValue = hv;
        S_VCount.Instance.MaxValue = vv;
    }
}
