
using System;
using System.Collections;
using Godot;

/// <summary>
/// 迷雾遮罩
/// </summary>
public partial class FogMask : FogMaskBase
{
    /// <summary>
    /// 迷雾宽度
    /// </summary>
    public int FogWidth { get; private set; }
    /// <summary>
    /// 迷雾高度
    /// </summary>
    public int FogHeight { get; private set; }
    
    private bool _init = false;

    private static Image _leftTransition;
    private static Image _rightTransition;
    private static Image _topTransition;
    private static Image _downTransition;
    
    private static Image _leftTopTransition;
    private static Image _rightTopTransition;
    private static Image _leftDownTransition;
    private static Image _rightDownTransition;
    
    private static Image _inLeftTopTransition;
    private static Image _inRightTopTransition;
    private static Image _inLeftDownTransition;
    private static Image _inRightDownTransition;
    
    private static bool _initSprite = false;
    
    private static void InitSprite()
    {
        if (_initSprite)
        {
            return;
        }
        _initSprite = false;

        var temp = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_map_WallTransition1_png, false);
        _leftTransition = temp.GetImage();
        _rightTransition = temp.GetImage();
        _rightTransition.Rotate180();
        _topTransition = temp.GetImage();
        _topTransition.Rotate90(ClockDirection.Clockwise);
        _downTransition = temp.GetImage();
        _downTransition.Rotate90(ClockDirection.Counterclockwise);
        
        var temp2 = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_map_WallTransition2_png, false);
        _leftDownTransition = temp2.GetImage();
        _leftTopTransition = temp2.GetImage();
        _leftTopTransition.Rotate90(ClockDirection.Clockwise);
        _rightDownTransition = temp2.GetImage();
        _rightDownTransition.Rotate90(ClockDirection.Counterclockwise);
        _rightTopTransition = temp2.GetImage();
        _rightTopTransition.Rotate180();
        
        var temp3 = ResourceManager.Load<Texture2D>(ResourcePath.resource_sprite_map_WallTransition3_png, false);
        _inLeftDownTransition = temp3.GetImage();
        _inLeftTopTransition = temp3.GetImage();
        _inLeftTopTransition.Rotate90(ClockDirection.Clockwise);
        _inRightDownTransition = temp3.GetImage();
        _inRightDownTransition.Rotate90(ClockDirection.Counterclockwise);
        _inRightTopTransition = temp3.GetImage();
        _inRightTopTransition.Rotate180();
    }

    /// <summary>
    /// 初始化迷雾遮罩
    /// </summary>
    /// <param name="position">起始位置, 单位: 格</param>
    /// <param name="size">大小, 单位: 格</param>
    /// <param name="alpha">透明度</param>
    public void InitFog(Vector2I position, Vector2I size, float alpha = 0)
    {
        if (_init)
        {
            return;
        }
        InitSprite();
        GlobalPosition = new Vector2(
            (position.X + size.X / 2f) * GameConfig.TileCellSize,
            (position.Y + size.Y / 2f) * GameConfig.TileCellSize
        );
        
        //创建光纹理
        FogWidth = (size.X + 2) * GameConfig.TileCellSize;
        FogHeight = (size.Y + 2) * GameConfig.TileCellSize;
        var img = Image.Create(FogWidth, FogHeight, false, Image.Format.Rgba8);
        img.Fill(Colors.White);
        
        //处理边缘过渡
        HandlerTransition(position, size, img);
        Texture = ImageTexture.CreateFromImage(img);

        var c = Color;
        c.A = alpha;
        Color = c;
        TargetAlpha = alpha;
    }
    
    private void HandlerTransition(Vector2I position, Vector2I size, Image image)
    {
        var tileMap = GameApplication.Instance.World.TileRoot;
        var autoConfig = GameApplication.Instance.DungeonManager.AutoTileConfig;
        var wallCoord = autoConfig.WALL_BLOCK.AutoTileCoord;
        var (x, y) = position;
        var (width, height) = size;
        x -= 1;
        y -= 1;
        width += 2;
        height += 2;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var pos = new Vector2I(i + x, j + y);
                //说明是外层墙壁
                if (tileMap.GetCellAtlasCoords(GameConfig.TopMapLayer, pos) == wallCoord)
                {
                    var left = IsEmptyCell(tileMap, new Vector2I(pos.X - 1, pos.Y));
                    var right = IsEmptyCell(tileMap, new Vector2I(pos.X + 1, pos.Y));
                    var top = IsEmptyCell(tileMap, new Vector2I(pos.X, pos.Y - 1));
                    var down = IsEmptyCell(tileMap, new Vector2I(pos.X, pos.Y + 1));
                    
                    var leftTop = IsEmptyCell(tileMap, new Vector2I(pos.X - 1, pos.Y - 1));
                    var leftDown = IsEmptyCell(tileMap, new Vector2I(pos.X - 1, pos.Y + 1));
                    var rightTop = IsEmptyCell(tileMap, new Vector2I(pos.X + 1, pos.Y - 1));
                    var rightDown = IsEmptyCell(tileMap, new Vector2I(pos.X + 1, pos.Y + 1));

                    if (!left && !right && !top && !down && !leftTop && !leftDown && !rightTop && !rightDown)
                    {
                        continue;
                    }
                    else if (leftTop && left && top) //外轮廓, 左上
                    {
                        FillTransitionImage(i, j, image, _leftTopTransition);
                    }
                    else if (leftDown && left && down) //外轮廓, 左下
                    {
                        FillTransitionImage(i, j, image, _leftDownTransition);
                    }
                    else if (rightTop && right && top) //外轮廓, 右上
                    {
                        FillTransitionImage(i, j, image, _rightTopTransition);
                    }
                    else if (rightDown && right && down) //外轮廓, 右下
                    {
                        FillTransitionImage(i, j, image, _rightDownTransition);
                    }
                    //-------------------------
                    else if (left) //左
                    {
                        FillTransitionImage(i, j, image, _leftTransition);
                    }
                    else if (right) //右
                    {
                        FillTransitionImage(i, j, image, _rightTransition);
                    }
                    else if (top) //上
                    {
                        FillTransitionImage(i, j, image, _topTransition);
                    }
                    else if (down) //下
                    {
                        FillTransitionImage(i, j, image, _downTransition);
                    }
                    //--------------------------
                    else if (leftTop) //内轮廓, 左上
                    {
                        FillTransitionImage(i, j, image, _inLeftTopTransition);
                    }
                    else if (leftDown) //内轮廓, 左下
                    {
                        FillTransitionImage(i, j, image, _inLeftDownTransition);
                    }
                    else if (rightTop) //内轮廓, 右上
                    {
                        FillTransitionImage(i, j, image, _inRightTopTransition);
                    }
                    else if (rightDown) //内轮廓, 右下
                    {
                        FillTransitionImage(i, j, image, _inRightDownTransition);
                    }
                    //------------------------
                    else //全黑
                    {
                        FillBlock(i, j, image);
                    }
                }
            }
        }
    }

    //填充一个16*16像素的区域
    private void FillBlock(int x, int y, Image image)
    {
        var endX = (x + 1) * GameConfig.TileCellSize;
        var endY = (y + 1) * GameConfig.TileCellSize;
        for (int i = x * GameConfig.TileCellSize; i < endX; i++)
        {
            for (int j = y * GameConfig.TileCellSize; j < endY; j++)
            {
                image.SetPixel(i, j, new Color(1, 1, 1, 0));
            }
        }
    }

    private void FillTransitionImage(int x, int y, Image image, Image transitionImage)
    {
        image.BlitRect(transitionImage,
            new Rect2I(Vector2I.Zero, 16, 16),
            new Vector2I(x * GameConfig.TileCellSize, y * GameConfig.TileCellSize)
        );
    }

    private bool IsEmptyCell(TileMap tileMap, Vector2I pos)
    {
        return tileMap.GetCellSourceId(GameConfig.TopMapLayer, pos) == -1 &&
               tileMap.GetCellSourceId(GameConfig.MiddleMapLayer, pos) == -1;
    }
    
    //判断是否是墙壁
    private bool IsNotWallCell(TileMap tileMap, Vector2I pos, Vector2I wallCoord)
    {
        return tileMap.GetCellAtlasCoords(GameConfig.TopMapLayer, pos) != wallCoord &&
               tileMap.GetCellAtlasCoords(GameConfig.MiddleMapLayer, pos) != wallCoord &&
               (tileMap.GetCellSourceId(GameConfig.TopMapLayer, pos) != -1 ||
                tileMap.GetCellSourceId(GameConfig.MiddleMapLayer, pos) != -1);
    }

    //判断是否是任意类型的图块
    private bool IsAnyCell(TileMap tileMap, Vector2I pos)
    {
        return tileMap.GetCellSourceId(GameConfig.FloorMapLayer, pos) != -1 ||
               tileMap.GetCellSourceId(GameConfig.MiddleMapLayer, pos) != -1 ||
               tileMap.GetCellSourceId(GameConfig.TopMapLayer, pos) != -1 ||
               tileMap.GetCellSourceId(GameConfig.AisleFloorMapLayer, pos) != -1;
    }
}