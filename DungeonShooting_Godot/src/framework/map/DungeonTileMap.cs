
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

/// <summary>
/// 地牢地砖管理类, 提供一些操作 TileMap 和计算导航的接口
/// </summary>
public class DungeonTileMap
{
    
    //----------------------------------------------------
    
    private TileMap _tileRoot;

    public DungeonTileMap(TileMap tileRoot)
    {
        _tileRoot = tileRoot;
    }

    /// <summary>
    /// 根据 startRoom 和 config 数据自动填充 tileMap 参数中的房间数据, 该函数为协程函数
    /// </summary>
    public IEnumerator AutoFillRoomTile(AutoTileConfig config, RoomInfo startRoomInfo, World world)
    {
        yield return _AutoFillRoomTile(config, startRoomInfo, world);
    }
    
    /// <summary>
    /// 根据 startRoom 和 config 数据自动填充 tileMap 参数中的过道数据, 该函数为协程函数
    /// </summary>
    public IEnumerator AutoFillAisleTile(AutoTileConfig config, RoomInfo roomInfo, World world)
    {
        yield return _AutoFillAisleTile(config, roomInfo, world);
    }
    
    private IEnumerator _AutoFillRoomTile(AutoTileConfig config, RoomInfo roomInfo, World world)
    {
        foreach (var info in roomInfo.Next)
        {
            yield return _AutoFillRoomTile(config, info, world);
        }
        
        //铺房间
        var rectPos = roomInfo.RoomSplit.RoomInfo.Position.AsVector2I();
        var tileInfo = roomInfo.RoomSplit.TileInfo;
        
        //---------------------- 生成房间小地图预览 ----------------------
        //先计算范围
        var x = int.MaxValue;
        var y = int.MaxValue;
        var x2 = int.MinValue;
        var y2 = int.MinValue;
        for (var i = 0; i < tileInfo.Floor.Count; i += 4)
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
        for (var i = 0; i < tileInfo.Floor.Count; i += 4)
        {
            var posX = tileInfo.Floor[i] - x + 1;
            var posY = tileInfo.Floor[i + 1] - y + 1;
            image.SetPixel(posX, posY, new Color(0, 0, 0, 0.5882353F));
        }
        //创建texture
        var imageTexture = ImageTexture.CreateFromImage(image);
        roomInfo.PreviewTexture = imageTexture;

        //---------------------- 填充tile操作 ----------------------
        var terrainInfo = config.TerrainInfo;
        
        SetAutoLayerDataFromList(MapLayer.AutoFloorLayer, config.SourceId, roomInfo, tileInfo.Floor, rectPos, terrainInfo);
        SetCustomLayerDataFromList(MapLayer.CustomFloorLayer1, roomInfo, tileInfo.CustomFloor1, rectPos);
        SetCustomLayerDataFromList(MapLayer.CustomFloorLayer2, roomInfo, tileInfo.CustomFloor2, rectPos);
        SetCustomLayerDataFromList(MapLayer.CustomFloorLayer3, roomInfo, tileInfo.CustomFloor3, rectPos);
        SetCustomLayerDataFromList(MapLayer.CustomMiddleLayer1, roomInfo, tileInfo.CustomMiddle1, rectPos);
        SetCustomLayerDataFromList(MapLayer.CustomMiddleLayer2, roomInfo, tileInfo.CustomMiddle2, rectPos);
        SetCustomLayerDataFromList(MapLayer.CustomTopLayer, roomInfo, tileInfo.CustomTop, rectPos);
        
        //寻找可用传送点
        var maxCount = (roomInfo.Size.X - 2) * (roomInfo.Size.Y - 2);
        var startPosition = roomInfo.Position + roomInfo.Size / 2;
        for (int i = 0; i < maxCount; i++)
        {
            var pos = SpiralUtil.Screw(i) + startPosition;
            if (IsWayTile(MapLayer.AutoFloorLayer, pos.X, pos.Y))
            {
                roomInfo.Waypoints = pos;
                break;
            }
        }
        
        //---------------------- 随机选择预设 ----------------------
        RoomPreinstallInfo preinstallInfo;
        if (EditorPlayManager.IsPlay && roomInfo.RoomType == GameApplication.Instance.DungeonManager.CurrConfig.DesignatedType) //编辑器模式, 指定预设
        {
            preinstallInfo = EditorTileMapManager.SelectPreinstall;
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
                var index = world.Random.RandomWeight(weights);
                preinstallInfo = roomInfo.RoomSplit.Preinstall[index];
            }
        }
        
        var roomPreinstall = new RoomPreinstall(roomInfo, preinstallInfo);
        roomInfo.RoomPreinstall = roomPreinstall;
        //执行预处理操作
        roomPreinstall.Pretreatment(world);
    }

    
    private IEnumerator _AutoFillAisleTile(AutoTileConfig config, RoomInfo roomInfo, World world)
    {
        foreach (var info in roomInfo.Next)
        {
            yield return _AutoFillAisleTile(config, info, world);
        }
        
        // yield break;
        //铺过道
        foreach (var doorInfo in roomInfo.Doors)
        {
            //必须是正向门
            if (!doorInfo.IsForward)
            {
                continue;
            }

            //铺过道
            if (doorInfo.AisleFloorCell != null)
            {
                yield return 0;

                //创建image, 这里留两个像素宽高用于描边
                var aisleImage = Image.Create(doorInfo.AisleFloorRect.Size.X, doorInfo.AisleFloorRect.Size.Y, false, Image.Format.Rgba8);
                //image.Fill(new Color(0, 1, 0, 0.2f));
                //填充像素点
                foreach (var p in doorInfo.AisleFloorCell)
                {
                    _tileRoot.SetCell(MapLayer.AutoFloorLayer, p, config.Floor.SourceId, config.Floor.AutoTileCoords);
                    //_tileRoot.SetCell(MapLayer.CustomTopLayer, p, config.Auto_000_010_000.SourceId, config.Auto_000_010_000.AutoTileCoords);
                    aisleImage.SetPixel(p.X - doorInfo.AisleFloorRect.Position.X, p.Y - doorInfo.AisleFloorRect.Position.Y, new Color(1, 1, 1, 0.5882353F));
                }
                //创建texture
                var aisleImageTexture = ImageTexture.CreateFromImage(aisleImage);
                doorInfo.AislePreviewTexture = aisleImageTexture;
                doorInfo.ConnectDoor.AislePreviewTexture = aisleImageTexture;
            }
        }
    }


    public void GenerateWallAndNavigation(World world, AutoTileConfig config)
    {
        var navigation = new NavigationRegion2D();
        navigation.Name = "Navigation";
        world.NavigationRoot.AddChild(navigation);
        TileMapUtils.GenerateTerrain(_tileRoot, navigation, config);
    }
    
    //设置自动地形层的数据
    private void SetAutoLayerDataFromList(int layer, int sourceId, RoomInfo roomInfo, List<int> data, Vector2I rectPos, TileSetTerrainInfo terrainInfo)
    {
        for (var i = 0; i < data.Count; i += 4)
        {
            var posX = data[i];
            var posY = data[i + 1];
            var pos = new Vector2I(roomInfo.Position.X + posX - rectPos.X, roomInfo.Position.Y + posY - rectPos.Y);
            var bit = (uint)data[i + 2];
            var type = (byte)data[i + 3];
            var index = terrainInfo.TerrainBitToIndex(bit, type);
            var terrainCell = terrainInfo.GetTerrainCell(index, type);
            var atlasCoords = terrainInfo.GetPosition(terrainCell);
            _tileRoot.SetCell(layer, pos, sourceId, atlasCoords);
        }
    }
    
    //设置自定义层的数据
    private void SetCustomLayerDataFromList(int layer, RoomInfo roomInfo,  List<int> data, Vector2I rectPos)
    {
        for (var i = 0; i < data.Count; i += 5)
        {
            var posX = data[i];
            var posY = data[i + 1];
            var sourceId = data[i + 2];
            var atlasCoordsX = data[i + 3];
            var atlasCoordsY = data[i + 4];
            var pos = new Vector2I(roomInfo.Position.X + posX - rectPos.X, roomInfo.Position.Y + posY - rectPos.Y);
            _tileRoot.SetCell(layer, pos, sourceId, new Vector2I(atlasCoordsX, atlasCoordsY));
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
                _tileRoot.SetCell(layer, p, data.SourceId, data.AutoTileCoords);
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
                _tileRoot.SetCell(layer, p, 0);
            }
        }
    }
    
    /// <summary>
    /// 返回指定位置的Tile是否为可以行走
    /// </summary>
    private bool IsWayTile(int layer, int x, int y)
    {
        return _tileRoot.GetCellSourceId(layer, new Vector2I(x, y)) != -1;
    }
}