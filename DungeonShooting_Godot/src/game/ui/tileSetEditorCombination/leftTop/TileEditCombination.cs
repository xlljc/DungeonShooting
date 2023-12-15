using System.Collections.Generic;
using Godot;

namespace UI.TileSetEditorCombination;

public partial class TileEditCombination : GridBg<TileSetEditorCombination.LeftTopBg>
{
    private Dictionary<Vector2I, Vector2I> _brushData;
    
    public override void SetUiNode(IUiNode uiNode)
    {
        base.SetUiNode(uiNode);
        Grid = UiNode.L_Grid.Instance;
        ContainerRoot = UiNode.L_CombinationRoot.Instance;
        
        UiNode.UiPanel.AddEventListener(EventEnum.OnSelectContainerCell, OnSelectContainerCell);
        UiNode.UiPanel.AddEventListener(EventEnum.OnClearContainerCell, OnClearContainerCell);
    }

    private void OnSelectContainerCell(object obj)
    {
        if (obj is Vector2I cell)
        {
            var src = UiNode.UiPanel.EditorPanel.TextureImage;
            var image = Image.Create(GameConfig.TileCellSize, GameConfig.TileCellSize, false, Image.Format.Rgba8);
            image.BlitRect(src,
                new Rect2I(cell * GameConfig.TileCellSizeVector2I, GameConfig.TileCellSize, GameConfig.TileCellSize),
                Vector2I.Zero);
            //UiNode.L_CombinationRoot.L_TextureRect.Instance.Texture = ImageTexture.CreateFromImage(image);;
        }
    }
    
    private void OnClearContainerCell(object obj)
    {
        
    }
}