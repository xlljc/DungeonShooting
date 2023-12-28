
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 地牢地砖管理类, 提供一些操作 TileMap 和计算导航的接口
/// </summary>
public class DungeonTileMap
{
    //--------------------- 导航 -------------------------
    
    //已经标记过的点
    private readonly HashSet<Vector2I> _usePoints = new HashSet<Vector2I>();

    //导航区域数据
    private readonly List<NavigationPolygonData> _polygonDataList = new List<NavigationPolygonData>();

    //连接门的导航区域
    private readonly List<DoorNavigationInfo> _connectNavigationItemList = new List<DoorNavigationInfo>();
    
    //----------------------------------------------------
    
    private TileMap _tileRoot;
    //地面地砖在 Atlas 的位置
    private List<Vector2I> _floorAtlasCoords;
    //生成导航的结果
    private GenerateNavigationResult _generateNavigationResult;

    private InfiniteGrid<bool> _tempAisleFloorGrid = new InfiniteGrid<bool>();

    public DungeonTileMap(TileMap tileRoot)
    {
        _tileRoot = tileRoot;
    }

    /// <summary>
    /// 根据 startRoom 和 config 数据自动填充 tileMap 参数中的地图数据, 该函数为协程函数
    /// </summary>
    public IEnumerator AutoFillRoomTile(AutoTileConfig config, RoomInfo startRoomInfo, SeedRandom random)
    {
        _connectNavigationItemList.Clear();
        yield return _AutoFillRoomTile(config, startRoomInfo, random);
    }
    
    private IEnumerator _AutoFillRoomTile(AutoTileConfig config, RoomInfo roomInfo, SeedRandom random)
    {
        foreach (var info in roomInfo.Next)
        {
            yield return _AutoFillRoomTile(config, info, random);
        }
        
        //铺房间
        if (roomInfo.RoomSplit == null) //自动填充的矩形房间, 现已经弃用
        {
            FillRect(GameConfig.FloorMapLayer, config.Floor, roomInfo.Position + Vector2.One,
                roomInfo.Size - new Vector2(2, 2));

            FillRect(GameConfig.TopMapLayer, config.Wall_In_LT, roomInfo.Position, Vector2.One);
            FillRect(GameConfig.TopMapLayer, config.Wall_Left, roomInfo.Position + new Vector2(0, 1),
                new Vector2(1, roomInfo.Size.Y - 2));
            FillRect(GameConfig.TopMapLayer, config.Wall_In_LB, roomInfo.Position + new Vector2(0, roomInfo.Size.Y - 1),
                new Vector2(1, 1));
            FillRect(GameConfig.TopMapLayer, config.Wall_Bottom, roomInfo.Position + new Vector2(1, roomInfo.Size.Y - 1),
                new Vector2(roomInfo.Size.X - 2, 1));
            FillRect(GameConfig.TopMapLayer, config.Wall_In_RB,
                roomInfo.Position + new Vector2(roomInfo.Size.X - 1, roomInfo.Size.Y - 1),
                Vector2.One);
            FillRect(GameConfig.TopMapLayer, config.Wall_Right, roomInfo.Position + new Vector2(roomInfo.Size.X - 1, 1),
                new Vector2(1, roomInfo.Size.Y - 2));
            FillRect(GameConfig.TopMapLayer, config.Wall_In_RT, roomInfo.Position + new Vector2(roomInfo.Size.X - 1, 0),
                Vector2.One);
            FillRect(GameConfig.MiddleMapLayer, config.Wall_Top, roomInfo.Position + Vector2.Right,
                new Vector2(roomInfo.Size.X - 2, 1));
        }
        else
        {
            var rectPos = roomInfo.RoomSplit.RoomInfo.Position.AsVector2I();
            var tileInfo = roomInfo.RoomSplit.TileInfo;
            
            //---------------------- 生成房间小地图预览 ----------------------
            //先计算范围
            var x = int.MaxValue;
            var y = int.MaxValue;
            var x2 = int.MinValue;
            var y2 = int.MinValue;
            for (var i = 0; i < tileInfo.Floor.Count; i += 5)
            {
                var posX = tileInfo.Floor[i];
                var posY = tileInfo.Floor[i + 1];
                x = Mathf.Min(x, posX);
                x2 = Mathf.Max(x2, posX);
                y = Mathf.Min(y, posY);
                y2 = Mathf.Max(y2, posY);
            }
            //创建image, 这里留两个像素宽高用于描边
            var image = Image.Create(x2 - x + 3, y2 - y + 3, false, Image.Format.Rgba8);
            //image.Fill(Colors.Green);
            //填充像素点
            for (var i = 0; i < tileInfo.Floor.Count; i += 5)
            {
                var posX = tileInfo.Floor[i] - x + 1;
                var posY = tileInfo.Floor[i + 1] - y + 1;
                image.SetPixel(posX, posY, new Color(0, 0, 0, 0.5882353F));
            }
            //创建texture
            var imageTexture = ImageTexture.CreateFromImage(image);
            roomInfo.PreviewTexture = imageTexture;

            //---------------------- 填充tile操作 ----------------------
            //底层
            for (var i = 0; i < tileInfo.Floor.Count; i += 5)
            {
                var posX = tileInfo.Floor[i];
                var posY = tileInfo.Floor[i + 1];
                var sourceId = tileInfo.Floor[i + 2];
                var atlasCoordsX = tileInfo.Floor[i + 3];
                var atlasCoordsY = tileInfo.Floor[i + 4];
                var pos = new Vector2I(roomInfo.Position.X + posX - rectPos.X, roomInfo.Position.Y + posY - rectPos.Y);
                _tileRoot.SetCell(GameConfig.FloorMapLayer, pos, sourceId, new Vector2I(atlasCoordsX, atlasCoordsY));
            }
            //中层
            for (var i = 0; i < tileInfo.Middle.Count; i += 5)
            {
                var posX = tileInfo.Middle[i];
                var posY = tileInfo.Middle[i + 1];
                var sourceId = tileInfo.Middle[i + 2];
                var atlasCoordsX = tileInfo.Middle[i + 3];
                var atlasCoordsY = tileInfo.Middle[i + 4];
                var pos = new Vector2I(roomInfo.Position.X + posX - rectPos.X, roomInfo.Position.Y + posY - rectPos.Y);
                _tileRoot.SetCell(GameConfig.MiddleMapLayer, pos, sourceId, new Vector2I(atlasCoordsX, atlasCoordsY));
            }
            //顶层
            for (var i = 0; i < tileInfo.Top.Count; i += 5)
            {
                var posX = tileInfo.Top[i];
                var posY = tileInfo.Top[i + 1];
                var sourceId = tileInfo.Top[i + 2];
                var atlasCoordsX = tileInfo.Top[i + 3];
                var atlasCoordsY = tileInfo.Top[i + 4];
                var pos = new Vector2I(roomInfo.Position.X + posX - rectPos.X, roomInfo.Position.Y + posY - rectPos.Y);
                _tileRoot.SetCell(GameConfig.TopMapLayer, pos, sourceId, new Vector2I(atlasCoordsX, atlasCoordsY));
            }
            
            //寻找可用传送点
            var maxCount = (roomInfo.Size.X - 2) * (roomInfo.Size.Y - 2);
            var startPosition = roomInfo.Position + roomInfo.Size / 2;
            for (int i = 0; i < maxCount; i++)
            {
                var pos = SpiralUtil.Screw(i) + startPosition;
                if (IsWayTile(GameConfig.FloorMapLayer, pos.X, pos.Y))
                {
                    roomInfo.Waypoints = pos;
                    break;
                }
            }
            
            //---------------------- 随机选择预设 ----------------------
            RoomPreinstallInfo preinstallInfo;
            if (EditorPlayManager.IsPlay && roomInfo.RoomType == GameApplication.Instance.DungeonManager.CurrConfig.DesignatedType) //编辑器模式, 指定预设
            {
                preinstallInfo = EditorManager.SelectPreinstall;
            }
            else //普通模式
            {
                if (roomInfo.RoomSplit.Preinstall.Count == 1)
                {
                    preinstallInfo = roomInfo.RoomSplit.Preinstall[0];
                }
                else
                {
                    var weights = roomInfo.RoomSplit.Preinstall.Select(info => info.Weight).ToArray();
                    var index = random.RandomWeight(weights);
                    preinstallInfo = roomInfo.RoomSplit.Preinstall[index];
                }
            }
            
            var roomPreinstall = new RoomPreinstall(roomInfo, preinstallInfo);
            roomInfo.RoomPreinstall = roomPreinstall;
            //执行预处理操作
            roomPreinstall.Pretreatment(random);
        }

        //铺过道
        foreach (var doorInfo in roomInfo.Doors)
        {
            //必须是正向门
            if (!doorInfo.IsForward)
            {
                continue;
            }
            
            //普通的直线连接
            var doorDir1 = doorInfo.Direction;
            var doorDir2 = doorInfo.ConnectDoor.Direction;
            if (!doorInfo.HasCross)
            {
                var rect = doorInfo.GetAisleRect();
                switch (doorDir1)
                {
                    case DoorDirection.E:
                        FullHorizontalAisle(config, rect);
                        FullHorizontalAisleLeft(config, rect, doorInfo);
                        FullHorizontalAisleRight(config, rect, doorInfo.ConnectDoor);
                        break;
                    case DoorDirection.W:
                        FullHorizontalAisle(config, rect);
                        FullHorizontalAisleLeft(config, rect, doorInfo.ConnectDoor);
                        FullHorizontalAisleRight(config, rect, doorInfo);
                        break;
                    
                    case DoorDirection.S:
                        FullVerticalAisle(config, rect);
                        FullVerticalAisleUp(config, rect, doorInfo);
                        FullVerticalAisleDown(config, rect, doorInfo.ConnectDoor);
                        break;
                    case DoorDirection.N:
                        FullVerticalAisle(config, rect);
                        FullVerticalAisleUp(config, rect, doorInfo.ConnectDoor);
                        FullVerticalAisleDown(config, rect, doorInfo);
                        break;
                }
            }
            else //带交叉点
            {
                //方向, 0横向, 1纵向
                var dir1 = 0;
                var dir2 = 0;

                var aisleRect = doorInfo.GetCrossAisleRect();
                var rect = aisleRect.Rect1;
                var rect2 = aisleRect.Rect2;
    
                //计算范围
                switch (doorDir1)
                {
                    case DoorDirection.E: //→
                        break;
                    case DoorDirection.W: //←
                        break;
                    case DoorDirection.S: //↓
                        dir1 = 1;
                        break;
                    case DoorDirection.N: //↑
                        dir1 = 1;
                        break;
                }
    
                switch (doorDir2)
                {
                    case DoorDirection.E: //→
                        break;
                    case DoorDirection.W: //←
                        break;
                    case DoorDirection.S: //↓
                        dir2 = 1;
                        break;
                    case DoorDirection.N: //↑
                        dir2 = 1;
                        break;
                }
    
                //填充交叉点
                FillRect(GameConfig.AisleFloorMapLayer, config.Floor, aisleRect.Cross.Position, aisleRect.Cross.Size);

                //墙壁, 0横向, 1纵向
                if (dir1 == 0)
                {
                    FullHorizontalAisle(config, rect);
                    FullHorizontalAisleLeft(config, rect, doorDir1 == DoorDirection.E ? doorInfo : null);
                    FullHorizontalAisleRight(config, rect, doorDir1 == DoorDirection.W ? doorInfo : null);
                }
                else
                {
                    FullVerticalAisle(config, rect);
                    FullVerticalAisleDown(config, rect, doorDir1 == DoorDirection.S ? doorInfo : null);
                    FullVerticalAisleUp(config, rect, doorDir1 == DoorDirection.N ? doorInfo : null);
                }
    
                if (dir2 == 0)
                {
                    FullHorizontalAisle(config, rect2);
                    FullHorizontalAisleLeft(config, rect2, doorDir2 == DoorDirection.E ? doorInfo.ConnectDoor : null);
                    FullHorizontalAisleRight(config, rect2, doorDir2 == DoorDirection.W ? doorInfo.ConnectDoor : null);
                }
                else
                {
                    FullVerticalAisle(config, rect2);
                    FullVerticalAisleDown(config, rect2, doorDir2 == DoorDirection.S ? doorInfo.ConnectDoor : null);
                    FullVerticalAisleUp(config, rect2, doorDir2 == DoorDirection.N ? doorInfo.ConnectDoor : null);
                }
    
                if ((doorDir1 == DoorDirection.N && doorDir2 == DoorDirection.E) || //↑→
                    (doorDir2 == DoorDirection.N && doorDir1 == DoorDirection.E))
                {
                    FillRect(GameConfig.TopMapLayer, config.Wall_Out_RT, doorInfo.Cross + new Vector2(0, GameConfig.CorridorWidth - 1), Vector2.One);
                    FillRect(GameConfig.TopMapLayer, config.Wall_In_RT, doorInfo.Cross + new Vector2(GameConfig.CorridorWidth - 1, -1), Vector2.One);
                    FillRect(GameConfig.MiddleMapLayer, config.Wall_Top, doorInfo.Cross + new Vector2I(0, -1), new Vector2(GameConfig.CorridorWidth - 1, 1));
                    FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Center, doorInfo.Cross, new Vector2(GameConfig.CorridorWidth - 1, 1));
                    FillRect(GameConfig.TopMapLayer, config.Wall_Right, doorInfo.Cross + new Vector2(GameConfig.CorridorWidth - 1, 0), new Vector2(1, GameConfig.CorridorWidth));
                    
                    FillRect(GameConfig.TopMapLayer, config.TopMask, doorInfo.Cross - new Vector2I(0, 2), new Vector2(GameConfig.CorridorWidth + 1, 1));
                    FillRect(GameConfig.TopMapLayer, config.TopMask, doorInfo.Cross + new Vector2I(GameConfig.CorridorWidth, -1), new Vector2(1, GameConfig.CorridorWidth + 1));
                }
                else if ((doorDir1 == DoorDirection.E && doorDir2 == DoorDirection.S) || //→↓
                         (doorDir2 == DoorDirection.E && doorDir1 == DoorDirection.S))
                {
                    FillRect(GameConfig.MiddleMapLayer, config.Wall_Out_RB, doorInfo.Cross + new Vector2I(0, -1), Vector2.One);
                    FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Right, doorInfo.Cross, Vector2.One);
                    FillRect(GameConfig.TopMapLayer, config.Wall_In_RB, doorInfo.Cross + new Vector2(GameConfig.CorridorWidth - 1, GameConfig.CorridorWidth - 1), Vector2.One);
                    FillRect(GameConfig.TopMapLayer, config.Wall_Right, doorInfo.Cross + new Vector2(GameConfig.CorridorWidth - 1, -1), new Vector2(1, GameConfig.CorridorWidth));
                    FillRect(GameConfig.TopMapLayer, config.Wall_Bottom, doorInfo.Cross + new Vector2(0, GameConfig.CorridorWidth - 1), new Vector2(GameConfig.CorridorWidth - 1, 1));
                    
                    FillRect(GameConfig.TopMapLayer, config.TopMask, doorInfo.Cross + new Vector2I(GameConfig.CorridorWidth, -1), new Vector2(1, GameConfig.CorridorWidth + 1));
                    FillRect(GameConfig.TopMapLayer, config.TopMask, doorInfo.Cross + new Vector2I(0, GameConfig.CorridorWidth), new Vector2(GameConfig.CorridorWidth + 1, 1));
                }
                else if ((doorDir1 == DoorDirection.S && doorDir2 == DoorDirection.W) || //↓←
                         (doorDir2 == DoorDirection.S && doorDir1 == DoorDirection.W))
                {
                    FillRect(GameConfig.MiddleMapLayer, config.Wall_Out_LB, doorInfo.Cross + new Vector2(GameConfig.CorridorWidth - 1, -1), Vector2.One);
                    FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Left, doorInfo.Cross + new Vector2(GameConfig.CorridorWidth - 1, 0), Vector2.One);
                    FillRect(GameConfig.TopMapLayer, config.Wall_In_LB, doorInfo.Cross + new Vector2(0, GameConfig.CorridorWidth - 1), Vector2.One);
                    FillRect(GameConfig.TopMapLayer, config.Wall_Left, doorInfo.Cross + new Vector2I(0, -1), new Vector2(1, GameConfig.CorridorWidth));
                    FillRect(GameConfig.TopMapLayer, config.Wall_Bottom, doorInfo.Cross + new Vector2(1, GameConfig.CorridorWidth - 1), new Vector2(GameConfig.CorridorWidth - 1, 1));
                    
                    FillRect(GameConfig.TopMapLayer, config.TopMask, doorInfo.Cross + new Vector2I(-1, -1), new Vector2(1, GameConfig.CorridorWidth + 1));
                    FillRect(GameConfig.TopMapLayer, config.TopMask, doorInfo.Cross + new Vector2I(-1, GameConfig.CorridorWidth), new Vector2(GameConfig.CorridorWidth + 1, 1));
                }
                else if ((doorDir1 == DoorDirection.W && doorDir2 == DoorDirection.N) || //←↑
                         (doorDir2 == DoorDirection.W && doorDir1 == DoorDirection.N))
                {
                    FillRect(GameConfig.TopMapLayer, config.Wall_Out_LT, doorInfo.Cross + new Vector2(GameConfig.CorridorWidth - 1, GameConfig.CorridorWidth - 1), Vector2.One);
                    FillRect(GameConfig.TopMapLayer, config.Wall_In_LT, doorInfo.Cross + new Vector2I(0, -1), Vector2.One);
                    FillRect(GameConfig.MiddleMapLayer, config.Wall_Top, doorInfo.Cross + new Vector2(1, -1), new Vector2(GameConfig.CorridorWidth - 1, 1));
                    FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Center, doorInfo.Cross + new Vector2(1, 0), new Vector2(GameConfig.CorridorWidth - 1, 1));
                    FillRect(GameConfig.TopMapLayer, config.Wall_Left, doorInfo.Cross + new Vector2(0, 0), new Vector2(1, GameConfig.CorridorWidth));
                    
                    FillRect(GameConfig.TopMapLayer, config.TopMask, doorInfo.Cross - new Vector2I(1, 2), new Vector2(GameConfig.CorridorWidth + 1, 1));
                    FillRect(GameConfig.TopMapLayer, config.TopMask, doorInfo.Cross - new Vector2I(1, 1), new Vector2(1, GameConfig.CorridorWidth + 1));
                }
            }
            
            //先计算范围
            var x = int.MaxValue;
            var y = int.MaxValue;
            var x2 = int.MinValue;
            var y2 = int.MinValue;
            _tempAisleFloorGrid.ForEach((gx, gy, data) =>
            {
                x = Mathf.Min(x, gx);
                x2 = Mathf.Max(x2, gx);
                y = Mathf.Min(y, gy);
                y2 = Mathf.Max(y2, gy);
                return true;
            });
            //创建image, 这里留两个像素宽高用于描边
            var image = Image.Create(x2 - x + 3, y2 - y + 3, false, Image.Format.Rgba8);
            //填充像素点
            _tempAisleFloorGrid.ForEach((gx, gy, data) =>
            {
                var posX = gx - x + 1;
                var posY = gy - y + 1;
                image.SetPixel(posX, posY, new Color(0, 0, 0, 0.5882353F));
                return true;
            });
            //创建texture
            var imageTexture = ImageTexture.CreateFromImage(image);
            doorInfo.AislePreviewTexture = imageTexture;
            doorInfo.ConnectDoor.AislePreviewTexture = imageTexture;
            
            _tempAisleFloorGrid.Clear();
        }
    }

    /// <summary>
    /// 给TileMap添加轮廓, 该函数为协程函数
    /// </summary>
    /// <param name="tileCellData">描轮廓的Tile</param>
    public IEnumerator AddOutlineTile(TileCellData tileCellData)
    {
        var c = 0;
        var rect = _tileRoot.GetUsedRect();
        var endX = rect.End.X + 1;
        var endY = rect.End.Y + 1;
        for (int x = rect.Position.X - 1; x <= endX; x++)
        {
            for (int y = rect.Position.Y - 1; y <= endY; y++)
            {
                if (c++ > 1000) //份帧处理, 不要挤在一帧
                {
                    c = 0;
                    yield return 0;
                }
                var pos = new Vector2I(x, y);
                var flag1 = _tileRoot.GetCellSourceId(GameConfig.FloorMapLayer, pos) != -1 ||
                            _tileRoot.GetCellSourceId(GameConfig.MiddleMapLayer, pos) != -1 ||
                            _tileRoot.GetCellSourceId(GameConfig.AisleFloorMapLayer, pos) != -1 ||
                            (_tileRoot.GetCellSourceId(GameConfig.TopMapLayer, pos) != -1 && _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, pos) != tileCellData.AutoTileCoords);
                if (!flag1) //空地
                {
                    var posDown = new Vector2I(pos.X, pos.Y + 1);
                    var posTop = new Vector2I(pos.X, pos.Y - 1);
                    var posLeft = new Vector2I(pos.X - 1, pos.Y);
                    var posRight = new Vector2I(pos.X + 1, pos.Y);
                    
                    var posLD = new Vector2I(pos.X - 1, pos.Y + 1);
                    var posLT = new Vector2I(pos.X - 1, pos.Y - 1);
                    var posRD = new Vector2I(pos.X + 1, pos.Y + 1);
                    var posRT = new Vector2I(pos.X + 1, pos.Y - 1);
                    
                    var flag2 = _tileRoot.GetCellSourceId(GameConfig.FloorMapLayer, posDown) != -1 ||
                                _tileRoot.GetCellSourceId(GameConfig.MiddleMapLayer, posDown) != -1 ||
                                (_tileRoot.GetCellSourceId(GameConfig.TopMapLayer, posDown) != -1 && _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, posDown) != tileCellData.AutoTileCoords) ||

                                _tileRoot.GetCellSourceId(GameConfig.FloorMapLayer, posTop) != -1 ||
                                _tileRoot.GetCellSourceId(GameConfig.MiddleMapLayer, posTop) != -1 ||
                                (_tileRoot.GetCellSourceId(GameConfig.TopMapLayer, posTop) != -1 && _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, posTop) != tileCellData.AutoTileCoords) ||

                                _tileRoot.GetCellSourceId(GameConfig.FloorMapLayer, posLeft) != -1 ||
                                _tileRoot.GetCellSourceId(GameConfig.MiddleMapLayer, posLeft) != -1 ||
                                (_tileRoot.GetCellSourceId(GameConfig.TopMapLayer, posLeft) != -1 && _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, posLeft) != tileCellData.AutoTileCoords) ||

                                _tileRoot.GetCellSourceId(GameConfig.FloorMapLayer, posRight) != -1 ||
                                _tileRoot.GetCellSourceId(GameConfig.MiddleMapLayer, posRight) != -1 ||
                                (_tileRoot.GetCellSourceId(GameConfig.TopMapLayer, posRight) != -1 && _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, posRight) != tileCellData.AutoTileCoords) ||
                                //
                                _tileRoot.GetCellSourceId(GameConfig.FloorMapLayer, posLD) != -1 ||
                                _tileRoot.GetCellSourceId(GameConfig.MiddleMapLayer, posLD) != -1 ||
                                (_tileRoot.GetCellSourceId(GameConfig.TopMapLayer, posLD) != -1 && _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, posLD) != tileCellData.AutoTileCoords) ||

                                _tileRoot.GetCellSourceId(GameConfig.FloorMapLayer, posLT) != -1 ||
                                _tileRoot.GetCellSourceId(GameConfig.MiddleMapLayer, posLT) != -1 ||
                                (_tileRoot.GetCellSourceId(GameConfig.TopMapLayer, posLT) != -1 && _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, posLT) != tileCellData.AutoTileCoords) ||

                                _tileRoot.GetCellSourceId(GameConfig.FloorMapLayer, posRD) != -1 ||
                                _tileRoot.GetCellSourceId(GameConfig.MiddleMapLayer, posRD) != -1 ||
                                (_tileRoot.GetCellSourceId(GameConfig.TopMapLayer, posRD) != -1 && _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, posRD) != tileCellData.AutoTileCoords) ||

                                _tileRoot.GetCellSourceId(GameConfig.FloorMapLayer, posRT) != -1 ||
                                _tileRoot.GetCellSourceId(GameConfig.MiddleMapLayer, posRT) != -1 ||
                                (_tileRoot.GetCellSourceId(GameConfig.TopMapLayer, posRT) != -1 && _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, posRT) != tileCellData.AutoTileCoords);
                    
                    if (flag2) //非空地, 那么说明这个点需要填充 WALL_BLOCK
                    {
                        _tileRoot.SetCell(GameConfig.TopMapLayer, pos, tileCellData.SourceId, tileCellData.AutoTileCoords);
                    }
                }
            }
        }
    }
    
    //填充tile区域
    private void FillRect(int layer, TileCellData data, Vector2 pos, Vector2 size)
    {
        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                var p = new Vector2I((int)pos.X + i, (int)pos.Y + j);
                if (layer == GameConfig.AisleFloorMapLayer)
                {
                    _tempAisleFloorGrid.Set(p, true);
                }
                _tileRoot.SetCell(layer, p, 0, data.AutoTileCoords);
            }
        }
    }

    //清除tile区域
    private void ClearRect(int layer, Vector2 pos, Vector2 size)
    {
        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                var p = new Vector2I((int)pos.X + i, (int)pos.Y + j);
                if (layer == GameConfig.AisleFloorMapLayer)
                {
                    _tempAisleFloorGrid.Remove(p.X, p.Y);
                }
                _tileRoot.SetCell(layer, p, 0);
            }
        }
    }

    //横向过道
    private void FullHorizontalAisle(AutoTileConfig config, Rect2 rect)
    {
        FillRect(GameConfig.AisleFloorMapLayer, config.Floor, rect.Position + new Vector2(0, 1), rect.Size - new Vector2(0, 2));
        FillRect(GameConfig.TopMapLayer, config.TopMask, rect.Position - new Vector2(0, 2), new Vector2(rect.Size.X, 1));
        FillRect(GameConfig.TopMapLayer, config.TopMask, rect.Position + new Vector2(0, rect.Size.Y), new Vector2(rect.Size.X, 1));
        FillRect(GameConfig.MiddleMapLayer, config.Wall_Top, rect.Position - new Vector2(0, 1), new Vector2(rect.Size.X, 1));
        FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Center, rect.Position, new Vector2(rect.Size.X, 1));
        FillRect(GameConfig.TopMapLayer, config.Wall_Bottom, rect.Position + new Vector2(0, rect.Size.Y - 1), new Vector2(rect.Size.X, 1));
    }

    //纵向过道
    private void FullVerticalAisle(AutoTileConfig config, Rect2 rect)
    {
        FillRect(GameConfig.AisleFloorMapLayer, config.Floor, rect.Position + new Vector2(1, 0), rect.Size - new Vector2(2, 0));
        FillRect(GameConfig.TopMapLayer, config.TopMask, rect.Position - new Vector2(1, 0), new Vector2(1, rect.Size.Y - 1));
        FillRect(GameConfig.TopMapLayer, config.TopMask, rect.Position + new Vector2(rect.Size.X, 0), new Vector2(1, rect.Size.Y - 1));
        FillRect(GameConfig.TopMapLayer, config.Wall_Left, rect.Position, new Vector2(1, rect.Size.Y - 1));
        FillRect(GameConfig.TopMapLayer, config.Wall_Right, rect.Position + new Vector2(rect.Size.X - 1, 0), new Vector2(1, rect.Size.Y - 1));
    }

    //横向过道, 门朝右, 连接方向向左
    private void FullHorizontalAisleLeft(AutoTileConfig config, Rect2 rect, RoomDoorInfo doorInfo = null)
    {
        //左
        ClearRect(GameConfig.TopMapLayer, rect.Position + new Vector2(-1, 1), new Vector2(1, rect.Size.Y - 2));
        if (doorInfo == null)
        {
            FillRect(GameConfig.AisleFloorMapLayer, config.Floor, rect.Position + new Vector2(-1, 1),
                new Vector2(1, rect.Size.Y - 2));
        }
        else
        {
            var atlasCoords = _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, (rect.Position - new Vector2(1, 1)).AsVector2I());
            ClearRect(GameConfig.TopMapLayer, rect.Position - new Vector2(1, 1), new Vector2(2, 4));

            if (atlasCoords == config.Wall_Right.AutoTileCoords)
            {
                FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Left, rect.Position - new Vector2(1, 0), Vector2.One);
                FillRect(GameConfig.TopMapLayer, config.Wall_Out_LB, rect.Position - new Vector2(1, 1), Vector2.One);
            }
            else
            {
                FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Center, rect.Position - new Vector2(1, 0), Vector2.One);
                FillRect(GameConfig.MiddleMapLayer, config.Wall_Top, rect.Position - new Vector2(1, 1), Vector2.One);
            }
            
            FillRect(GameConfig.TopMapLayer, config.Wall_Out_LT, rect.Position + new Vector2(-1, 3), Vector2.One);
            
            FillRect(GameConfig.FloorMapLayer, config.Floor, rect.Position + new Vector2(-1, 1), new Vector2(1, rect.Size.Y - 2));
            //生成门的导航区域
            var x = rect.Position.X * GameConfig.TileCellSize;
            var y = rect.Position.Y * GameConfig.TileCellSize;
            
            var op1 = new Vector2(x - GameConfig.TileCellSize * 1.5f, y + GameConfig.TileCellSize * 1.5f);
            var op2 = new Vector2(x + GameConfig.TileCellSize * 0.5f, y + GameConfig.TileCellSize * 1.5f);
            var op3 = new Vector2(x + GameConfig.TileCellSize * 0.5f, y + GameConfig.TileCellSize * 3f);
            var op4 = new Vector2(x - GameConfig.TileCellSize * 1.5f, y + GameConfig.TileCellSize * 3f);
            AddDoorNavigation(
                doorInfo, op1, op2, op3, op4,
                op1,
                new Vector2(op1.X + GameConfig.TileCellSize, op2.Y),
                new Vector2(op1.X + GameConfig.TileCellSize, op3.Y),
                op4
            );
        }
    }
    
    //横向过道, 门朝左, 连接方向向右
    private void FullHorizontalAisleRight(AutoTileConfig config, Rect2 rect, RoomDoorInfo doorInfo = null)
    {
        //右
        ClearRect(GameConfig.TopMapLayer, rect.Position + new Vector2(rect.Size.X, 1), new Vector2(1, rect.Size.Y - 2));
        if (doorInfo == null)
        {
            FillRect(GameConfig.AisleFloorMapLayer, config.Floor, rect.Position + new Vector2(rect.Size.X, 1), new Vector2(1, rect.Size.Y - 2));
        }
        else
        {
            var atlasCoords = _tileRoot.GetCellAtlasCoords(GameConfig.TopMapLayer, (rect.Position + new Vector2(rect.Size.X, -1)).AsVector2I());
            ClearRect(GameConfig.TopMapLayer, rect.Position + new Vector2(rect.Size.X - 1,  -1), new Vector2(2, 4));

            if (atlasCoords == config.Wall_Left.AutoTileCoords)
            {
                FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Right, rect.Position + new Vector2(rect.Size.X, 0), Vector2.One);
                FillRect(GameConfig.TopMapLayer, config.Wall_Out_RB, rect.Position + new Vector2(rect.Size.X, -1), Vector2.One);
            }
            else
            {
                FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Center, rect.Position + new Vector2(rect.Size.X, 0), Vector2.One);
                FillRect(GameConfig.MiddleMapLayer, config.Wall_Top, rect.Position + new Vector2(rect.Size.X, -1), Vector2.One);
            }
            
            FillRect(GameConfig.TopMapLayer, config.Wall_Out_RT, rect.Position + new Vector2(rect.Size.X, 3), Vector2.One);
            
            FillRect(GameConfig.FloorMapLayer, config.Floor, rect.Position + new Vector2(rect.Size.X, 1), new Vector2(1, rect.Size.Y - 2));
            //生成门的导航区域
            var x = rect.Position.X * GameConfig.TileCellSize;
            var y = rect.Position.Y * GameConfig.TileCellSize;
            
            var op1 = new Vector2(x - GameConfig.TileCellSize * 1.5f + (rect.Size.X + 1) * GameConfig.TileCellSize, y + GameConfig.TileCellSize * 1.5f);
            var op2 = new Vector2(x + GameConfig.TileCellSize * 0.5f + (rect.Size.X + 1) * GameConfig.TileCellSize, y + GameConfig.TileCellSize * 1.5f);
            var op3 = new Vector2(x + GameConfig.TileCellSize * 0.5f + (rect.Size.X + 1) * GameConfig.TileCellSize, y + GameConfig.TileCellSize * 3f);
            var op4 = new Vector2(x - GameConfig.TileCellSize * 1.5f + (rect.Size.X + 1) * GameConfig.TileCellSize, y + GameConfig.TileCellSize * 3f);
            AddDoorNavigation(
                doorInfo, op1, op2, op3, op4,
                new Vector2(op2.X - GameConfig.TileCellSize, op1.Y),
                op2,
                op3,
                new Vector2(op2.X - GameConfig.TileCellSize, op4.Y)
            );
        }
    }

    //纵向过道, 门朝下, 连接方向向上
    private void FullVerticalAisleUp(AutoTileConfig config, Rect2 rect, RoomDoorInfo doorInfo = null)
    {
        //上
        ClearRect(GameConfig.TopMapLayer, rect.Position + new Vector2(1, -1), new Vector2(rect.Size.X - 2, 1));
        if (doorInfo == null)
        {
            FillRect(GameConfig.AisleFloorMapLayer, config.Floor, rect.Position + new Vector2(1, -1),
                new Vector2(rect.Size.X - 2, 1));
        }
        else
        {
            ClearRect(GameConfig.TopMapLayer, rect.Position + new Vector2(1, rect.Size.Y - 2), new Vector2(2, 2));
            ClearRect(GameConfig.MiddleMapLayer, rect.Position + new Vector2(0, rect.Size.Y - 1), new Vector2(4, 2));
            
            FillRect(GameConfig.MiddleMapLayer, config.Wall_Out_RB, rect.Position + new Vector2(0, rect.Size.Y - 1), Vector2.One);
            FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Right, rect.Position + new Vector2(0, rect.Size.Y), Vector2.One);
            FillRect(GameConfig.MiddleMapLayer, config.Wall_Out_LB, rect.Position + new Vector2(rect.Size.X - 1, rect.Size.Y - 1), Vector2.One);
            FillRect(GameConfig.MiddleMapLayer, config.Wall_Vertical_Left, rect.Position + new Vector2(rect.Size.X - 1, rect.Size.Y), Vector2.One);
            
            FillRect(GameConfig.FloorMapLayer, config.Floor, rect.Position + new Vector2(1, -1), new Vector2(rect.Size.X - 2, 1));

            
            //生成门的导航区域
            var x = rect.Position.X * GameConfig.TileCellSize;
            var y = rect.Position.Y * GameConfig.TileCellSize;
            
            var op1 = new Vector2(x + GameConfig.TileCellSize * 1.5f, y - GameConfig.TileCellSize * 1f);
            var op2 = new Vector2(x + GameConfig.TileCellSize * 2.5f, y - GameConfig.TileCellSize * 1f);
            var op3 = new Vector2(x + GameConfig.TileCellSize * 2.5f, y + GameConfig.TileCellSize * 0.5f);
            var op4 = new Vector2(x + GameConfig.TileCellSize * 1.5f, y + GameConfig.TileCellSize * 0.5f);
            AddDoorNavigation(
                doorInfo, op1, op2, op3, op4,
                op1,
                op2,
                new Vector2(op3.X, op1.Y + GameConfig.TileCellSize),
                new Vector2(op4.X, op1.Y + GameConfig.TileCellSize)
            );
        }
    }
    
    //纵向过道, 门朝上, 连接方向向下
    private void FullVerticalAisleDown(AutoTileConfig config, Rect2 rect, RoomDoorInfo doorInfo = null)
    {
        //下
        ClearRect(GameConfig.MiddleMapLayer, rect.Position + new Vector2(1, rect.Size.Y), new Vector2(rect.Size.X - 2, 1));
        if (doorInfo == null)
        {
            FillRect(GameConfig.AisleFloorMapLayer, config.Floor, rect.Position + new Vector2(1, rect.Size.Y), new Vector2(rect.Size.X - 2, 1));
        }
        else
        {
            ClearRect(GameConfig.TopMapLayer, rect.Position + new Vector2(1, 0), new Vector2(2, 2));
            FillRect(GameConfig.TopMapLayer, config.Wall_Out_RT, rect.Position + new Vector2(0, -1), Vector2.One);
            FillRect(GameConfig.TopMapLayer, config.Wall_Out_LT, rect.Position + new Vector2(3, -1), Vector2.One);
            
            FillRect(GameConfig.FloorMapLayer, config.Floor, rect.Position + new Vector2(1, rect.Size.Y), new Vector2(rect.Size.X - 2, 1));
            //生成门的导航区域
            var x = rect.Position.X * GameConfig.TileCellSize;
            var y = rect.Position.Y * GameConfig.TileCellSize;
            
            var op1 = new Vector2(x + GameConfig.TileCellSize * 1.5f, y - GameConfig.TileCellSize * 1f + (rect.Size.Y + 1) * GameConfig.TileCellSize);
            var op2 = new Vector2(x + GameConfig.TileCellSize * 2.5f, y - GameConfig.TileCellSize * 1f + (rect.Size.Y + 1) * GameConfig.TileCellSize);
            var op3 = new Vector2(x + GameConfig.TileCellSize * 2.5f, y + GameConfig.TileCellSize * 0.5f + (rect.Size.Y + 1) * GameConfig.TileCellSize);
            var op4 = new Vector2(x + GameConfig.TileCellSize * 1.5f, y + GameConfig.TileCellSize * 0.5f + (rect.Size.Y + 1) * GameConfig.TileCellSize);
            AddDoorNavigation(
                doorInfo, op1, op2, op3, op4,
                new Vector2(op1.X, op3.Y - GameConfig.TileCellSize),
                new Vector2(op2.X, op3.Y - GameConfig.TileCellSize),
                op3,
                op4
            );
        }
    }

    /// <summary>
    /// 添加房间
    /// </summary>
    private void AddDoorNavigation(RoomDoorInfo doorInfo,
        Vector2 op1, Vector2 op2, Vector2 op3, Vector2 op4,
        Vector2 cp1, Vector2 cp2, Vector2 cp3, Vector2 cp4)
    {
        var openPolygonData = new NavigationPolygonData();
        openPolygonData.Type = NavigationPolygonType.Out;
        openPolygonData.SetPoints(new []{ op1, op2, op3, op4 });

        var closePolygonData = new NavigationPolygonData();
        closePolygonData.Type = NavigationPolygonType.Out;
        closePolygonData.SetPoints(new []{ cp1, cp2, cp3, cp4 });

        _connectNavigationItemList.Add(new DoorNavigationInfo(doorInfo, openPolygonData, closePolygonData));
    }

    /// <summary>
    /// 计算并动生成导航区域, layer 为需要计算的层级，如果没有设置 floorAtlasCoords，则该 layer 下不为空的地砖都将视为可行走区域
    /// </summary>
    public void GenerateNavigationPolygon(int layer)
    {
        _usePoints.Clear();
        _polygonDataList.Clear();

        try
        {
            var size = new Vector2(GameConfig.TileCellSize, GameConfig.TileCellSize);

            var rect = _tileRoot.GetUsedRect();

            var x = rect.Position.X;
            var y = rect.Position.Y;
            var w = rect.Size.X;
            var h = rect.Size.Y;

            for (int j = y; j < h; j++)
            {
                for (int i = x; i < w; i++)
                {
                    if (IsWayTile(layer, i, j))
                    {
                        if (!_usePoints.Contains(new Vector2I(i, j)))
                        {
                            NavigationPolygonData polygonData = null;

                            if (!IsWayTile(layer, i, j - 1))
                            {
                                polygonData = CalcOutline(layer, i, j, size);
                            }
                            else if (!IsWayTile(layer, i, j + 1))
                            {
                                polygonData = CalcInline(layer, i, j, size);
                            }

                            if (polygonData != null)
                            {
                                _polygonDataList.Add(polygonData);
                            }
                        }
                    }
                }
            }

            _generateNavigationResult = new GenerateNavigationResult(true);
        }
        catch (NavigationPointException e)
        {
            _usePoints.Clear();
            _polygonDataList.Clear();
            Debug.Log(e.Message);
            _generateNavigationResult = new GenerateNavigationResult(false, e);
        }
    }

    /// <summary>
    /// 获取生成导航区域操作的结果, 如果没有调用过 GenerateNavigationPolygon() 函数, 则返回 null
    /// </summary>
    /// <returns></returns>
    public GenerateNavigationResult GetGenerateNavigationResult()
    {
        return _generateNavigationResult;
    }
    
    /// <summary>
    /// 将导航区域挂载到 navigationRoot 上
    /// </summary>
    public void MountNavigationPolygon(Node2D navigationRoot)
    {
        //TestData();
        // 在 Godot4.0_rc6 中 如果将所有点都放在 NavigationPolygon 里面, 即使点是对的, 但调用 MakePolygonsFromOutlines 还是可能会报错, 这应该是个bug
        
        //通过 GenerateNavigationPolygon() 计算出来的导航区域
        for (var i = 0; i < _polygonDataList.Count; i++)
        {
            var polygonData = _polygonDataList[i];
            CreateNavigationRegion(navigationRoot, polygonData);
        }

        //门占用区域的导航区域
        for (var i = 0; i < _connectNavigationItemList.Count; i++)
        {
            var item = _connectNavigationItemList[i];
            item.CloseNavigationNode = CreateNavigationRegion(navigationRoot, item.CloseNavigationData);
            item.OpenNavigationNode = CreateNavigationRegion(navigationRoot, item.OpenNavigationData);
            item.CloseNavigationNode.Enabled = false;
            item.OpenNavigationNode.Enabled = false;
            item.DoorInfo.Navigation = item;
        }
    }

    //创建导航区域
    private NavigationRegion2D CreateNavigationRegion(Node2D navigationRoot, NavigationPolygonData polygonData)
    {
        var polygon = new NavigationPolygon();
        polygon.CellSize = GameConfig.NavigationCellSize;
        polygon.AddOutline(polygonData.GetPoints());
        polygon.MakePolygonsFromOutlines();
        //var param = new NavigationMeshSourceGeometryData2D();
        //NavigationServer2D.BakeFromSourceGeometryData(polygon, param);
        var navigationPolygon = new NavigationRegion2D();
        navigationPolygon.Name = "NavigationRegion" + (navigationRoot.GetChildCount() + 1);
        navigationPolygon.NavigationPolygon = polygon;
        navigationRoot.AddChild(navigationPolygon);
        return navigationPolygon;
    }

    /// <summary>
    /// 获取房间内的导航点数据
    /// </summary>
    public NavigationPolygonData[] GetPolygonData()
    {
        return _polygonDataList.ToArray();
    }

    /// <summary>
    /// 设置导航网格数据
    /// </summary>
    /// <param name="list"></param>
    public void SetPolygonData(List<NavigationPolygonData> list)
    {
        _polygonDataList.Clear();
        _polygonDataList.AddRange(list);
        _generateNavigationResult = new GenerateNavigationResult(true);
    }

    /// <summary>
    /// 清除生成的导航数据
    /// </summary>
    public void ClearPolygonData()
    {
        _polygonDataList.Clear();
        _generateNavigationResult = null;
    }
    
    /// <summary>
    /// 获取连接门导航数据, 必须要调用 AutoFillRoomTile() 函数才有数据
    /// </summary>
    public NavigationPolygonData[] GetConnectDoorPolygonData()
    {
        var array = new NavigationPolygonData[_connectNavigationItemList.Count];
        for (var i = 0; i < _connectNavigationItemList.Count; i++)
        {
            array[i] = _connectNavigationItemList[i].OpenNavigationData;
        }
        return array;
    }

    /// <summary>
    /// 设置地面的地砖，将影响导航网格计算
    /// </summary>
    public void SetFloorAtlasCoords(List<Vector2I> floorAtlasCoords)
    {
        _floorAtlasCoords = floorAtlasCoords;
    }
    
    /// <summary>
    /// 返回指定位置的Tile是否为可以行走
    /// </summary>
    private bool IsWayTile(int layer, int x, int y)
    {
        if (_floorAtlasCoords == null || _floorAtlasCoords.Count == 0)
        {
            return _tileRoot.GetCellSourceId(layer, new Vector2I(x, y)) != -1;
        }

        var result = _tileRoot.GetCellAtlasCoords(layer, new Vector2I(x, y));
        return _floorAtlasCoords.Contains(result);
    }

    //计算导航网格外轮廓
    private NavigationPolygonData CalcOutline(int layer, int i, int j, Vector2 size)
    {
        var polygonData = new NavigationPolygonData();
        polygonData.Type = NavigationPolygonType.Out;
        var points = new List<Vector2>();
        // 0:右, 1:下, 2:左, 3:上
        var dir = 0;
        var offset = new Vector2(size.X * 0.5f, size.Y * 0.5f);
        //找到路, 向右开始找边界
        var startPos = new Vector2I(i, j);

        var tempI = i;
        var tempJ = j;

        while (true)
        {
            switch (dir)
            {
                case 0: //右
                {
                    if (IsWayTile(layer, tempI, tempJ - 1)) //先向上找
                    {
                        dir = 3;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }
                    else if (IsWayTile(layer, tempI + 1, tempJ)) //再向右找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        }

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        PutUsePoint(new Vector2I(tempI, tempJ));
                        tempI++;
                        break;
                    }
                    else if (IsWayTile(layer, tempI, tempJ + 1)) //向下找
                    {
                        dir = 1;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }

                    throw new NavigationPointException(new Vector2I(tempI, tempJ), "生成导航多边形发生错误! 点: " + new Vector2I(tempI, tempJ) + "发生交错!");
                }
                case 1: //下
                {
                    if (IsWayTile(layer, tempI + 1, tempJ)) //先向右找
                    {
                        dir = 0;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }
                    else if (IsWayTile(layer, tempI, tempJ + 1)) //再向下找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        }

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        PutUsePoint(new Vector2I(tempI, tempJ));
                        tempJ++;
                        break;
                    }
                    else if (IsWayTile(layer, tempI - 1, tempJ)) //向左找
                    {
                        dir = 2;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        //points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }

                    throw new NavigationPointException(new Vector2I(tempI, tempJ), "生成导航多边形发生错误! 点: " + new Vector2I(tempI, tempJ) + "发生交错!");
                }
                case 2: //左
                {
                    if (IsWayTile(layer, tempI, tempJ + 1)) //先向下找
                    {
                        dir = 1;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        //points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }
                    else if (IsWayTile(layer, tempI - 1, tempJ)) //再向左找
                    {
                        if (points.Count == 0)
                        {
                            //points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                            points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        }

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        PutUsePoint(new Vector2I(tempI, tempJ));
                        tempI--;
                        break;
                    }
                    else if (IsWayTile(layer, tempI, tempJ - 1)) //向上找
                    {
                        dir = 3;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        //points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }

                    throw new NavigationPointException(new Vector2I(tempI, tempJ), "生成导航多边形发生错误! 点: " + new Vector2I(tempI, tempJ) + "发生交错!");
                }
                case 3: //上
                {
                    if (IsWayTile(layer, tempI - 1, tempJ)) //先向左找
                    {
                        dir = 2;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        //points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }
                    else if (IsWayTile(layer, tempI, tempJ - 1)) //再向上找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        }

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        PutUsePoint(new Vector2I(tempI, tempJ));
                        tempJ--;
                        break;
                    }
                    else if (IsWayTile(layer, tempI + 1, tempJ)) //向右找
                    {
                        dir = 0;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }

                    throw new NavigationPointException(new Vector2I(tempI, tempJ), "生成导航多边形发生错误! 点: " + new Vector2I(tempI, tempJ) + "发生交错!");
                }
            }
        }
    }

    //计算导航网格内轮廓
    private NavigationPolygonData CalcInline(int layer, int i, int j, Vector2 size)
    {
        var polygonData = new NavigationPolygonData();
        polygonData.Type = NavigationPolygonType.In;
        var points = new List<Vector2>();
        // 0:右, 1:下, 2:左, 3:上
        var dir = 0;
        var offset = new Vector2(size.X * 0.5f, size.Y * 0.5f);
        //找到路, 向右开始找边界
        var startPos = new Vector2I(i - 1, j);
        PutUsePoint(startPos);

        var tempI = i;
        var tempJ = j;

        while (true)
        {
            switch (dir)
            {
                case 0: //右
                {
                    if (IsWayTile(layer, tempI, tempJ + 1)) //向下找
                    {
                        dir = 1;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        //points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }
                    else if (IsWayTile(layer, tempI + 1, tempJ)) //再向右找
                    {
                        if (points.Count == 0)
                        {
                            //points.Add(new Vector2((tempI - 1) * size.X + offset.X, tempJ * size.Y + offset.Y));
                            points.Add(new Vector2((tempI - 1) * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        }

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        PutUsePoint(new Vector2I(tempI, tempJ));
                        tempI++;
                        break;
                    }
                    else if (IsWayTile(layer, tempI, tempJ - 1)) //先向上找
                    {
                        dir = 3;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        //points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }

                    throw new NavigationPointException(new Vector2I(tempI, tempJ), "生成导航多边形发生错误! 点: " + new Vector2I(tempI, tempJ) + "发生交错!");
                }
                case 1: //下
                {
                    if (IsWayTile(layer, tempI - 1, tempJ)) //向左找
                    {
                        dir = 2;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }
                    else if (IsWayTile(layer, tempI, tempJ + 1)) //再向下找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.X + offset.X, tempJ * size.Y + offset.Y));
                        }

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        PutUsePoint(new Vector2I(tempI, tempJ));
                        tempJ++;
                        break;
                    }
                    else if (IsWayTile(layer, tempI + 1, tempJ)) //先向右找
                    {
                        dir = 0;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        //points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }

                    throw new NavigationPointException(new Vector2I(tempI, tempJ), "生成导航多边形发生错误! 点: " + new Vector2I(tempI, tempJ) + "发生交错!");
                }
                case 2: //左
                {
                    if (IsWayTile(layer, tempI, tempJ - 1)) //向上找
                    {
                        dir = 3;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }
                    else if (IsWayTile(layer, tempI - 1, tempJ)) //再向左找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.X + offset.X, tempJ * size.Y + offset.Y));
                        }

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        PutUsePoint(new Vector2I(tempI, tempJ));
                        tempI--;
                        break;
                    }
                    else if (IsWayTile(layer, tempI, tempJ + 1)) //先向下找
                    {
                        dir = 1;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }

                    throw new NavigationPointException(new Vector2I(tempI, tempJ), "生成导航多边形发生错误! 点: " + new Vector2I(tempI, tempJ) + "发生交错!");
                }
                case 3: //上
                {
                    if (IsWayTile(layer, tempI + 1, tempJ)) //向右找
                    {
                        dir = 0;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        //points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y * 2));
                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }
                    else if (IsWayTile(layer, tempI, tempJ - 1)) //再向上找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.X + offset.X, tempJ * size.Y + offset.Y));
                        }

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        PutUsePoint(new Vector2I(tempI, tempJ));
                        tempJ--;
                        break;
                    }
                    else if (IsWayTile(layer, tempI - 1, tempJ)) //先向左找
                    {
                        dir = 2;

                        var pos = new Vector2I(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            polygonData.SetPoints(points.ToArray());
                            return polygonData;
                        }

                        points.Add(new Vector2(tempI * size.X + offset.X, tempJ * size.Y + offset.Y));
                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }

                    throw new NavigationPointException(new Vector2I(tempI, tempJ), "生成导航多边形发生错误! 点: " + new Vector2I(tempI, tempJ) + "发生交错!");
                }
            }
        }
    }

    //记录导航网格中已经使用过的坐标
    private void PutUsePoint(Vector2I pos)
    {
        if (_usePoints.Contains(pos))
        {
            throw new NavigationPointException(pos, "生成导航多边形发生错误! 点: " + pos + "发生交错!");
        }

        _usePoints.Add(pos);
    }
}