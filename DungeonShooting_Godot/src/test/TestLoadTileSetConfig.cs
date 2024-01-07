using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public partial class TestLoadTileSetConfig : Node2D
{
    private Dictionary<string, TileSetSplit> _tileSetConfig;
    private TileMap _tileMap;
    
    public override void _Ready()
    {
        InitTileSetConfig();
        _tileMap = GetNode<TileMap>("TileMap");

        var tileSetSplit = _tileSetConfig.First().Value;
        var tileSet = tileSetSplit.GetTileSet();
        _tileMap.TileSet = tileSet;
        
        _tileMap.SetCell(0, new Vector2I(5, 5), 0, new Vector2I(0, 0));
    }
    
    //初始化TileSet配置
    private void InitTileSetConfig()
    {
        //加载房间配置信息
        var asText = ResourceManager.LoadText("res://" + GameConfig.RoomTileSetDir + GameConfig.TileSetConfigFile);
        _tileSetConfig = JsonSerializer.Deserialize<Dictionary<string, TileSetSplit>>(asText);
		
        //加载所有数据
        foreach (var tileSetSplit in _tileSetConfig)
        {
            tileSetSplit.Value.ReloadTileSetInfo();
        }
    }
    
}
