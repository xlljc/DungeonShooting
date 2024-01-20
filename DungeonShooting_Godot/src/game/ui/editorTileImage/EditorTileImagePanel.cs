using Godot;

namespace UI.EditorTileImage;

public partial class EditorTileImagePanel : EditorTileImage
{
    private Image _image;
    private ImageTexture _texture;
    
    public override void OnCreateUi()
    {
        
    }

    public override void OnDestroyUi()
    {
        if (_image != null)
        {
            _image.Dispose();
            _image = null;
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
        _image = image;
        _texture = ImageTexture.CreateFromImage(image);
        S_TileSprite.Instance.Texture = _texture;
        S_Bg.Instance.DoFocus();
    }

}
