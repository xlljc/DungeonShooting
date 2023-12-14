using Godot;

namespace UI.TileSetEditorSegment;

public class TileCell : UiCell<TileSetEditorSegment.CellButton, Vector2I>
{
    private Image _image;
    private ImageTexture _previewTexture;
    
    public override void OnInit()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
        _image = Image.Create(GameConfig.TileCellSize, GameConfig.TileCellSize, false, Image.Format.Rgba8);
        _previewTexture = ImageTexture.CreateFromImage(_image);
        CellNode.L_PreviewImage.Instance.Texture = _previewTexture;
    }
    

    public override void OnSetData(Vector2I data)
    {
        var image = CellNode.UiPanel.EditorPanel.TextureImage;
        _image.BlitRect(image, new Rect2I(data * GameConfig.TileCellSizeVector2I, GameConfig.TileCellSizeVector2I), Vector2I.Zero);
        _previewTexture.Update(_image);
        CellNode.L_CellId.Instance.Text = data.ToString();
    }

    public override void OnDestroy()
    {
        _previewTexture.Dispose();
    }

    public override void OnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = true;
    }

    public override void OnUnSelect()
    {
        CellNode.L_SelectTexture.Instance.Visible = false;
    }

    public override int OnSort(UiCell<TileSetEditorSegment.CellButton, Vector2I> other)
    {
        if (Data.Y != other.Data.Y)
        {
            return Data.Y - other.Data.Y;
        }
        return Data.X - other.Data.X;
    }
}