using Godot;

namespace UI.TileSetEditorProject;

public class PreviewCell : UiCell<TileSetEditorProject.Preview, TileSetSourceInfo>
{
    private ImageTexture _imageTexture;
    
    public override void OnInit()
    {
        _imageTexture = new ImageTexture();
        CellNode.L_PreviewImage.Instance.Texture = _imageTexture;
    }

    public override void OnSetData(TileSetSourceInfo data)
    {
        CellNode.L_Name.Instance.Text = data.Name;
        var sourceImage = data.GetSourceImage();
        if (sourceImage == null)
        {
            CellNode.L_PreviewImage.Instance.Visible = false;
        }
        else
        {
            CellNode.L_PreviewImage.Instance.Visible = true;
            _imageTexture.SetImage(sourceImage);
        }
    }

    public override void OnDestroy()
    {
        _imageTexture.Dispose();
        _imageTexture = null;
    }
}