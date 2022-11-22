using System;
using System.Collections.Generic;
using System.Globalization;
using Godot;

/// <summary>
/// 房间管理器
/// </summary>
public class RoomManager : Navigation2D
{
    /// <summary>
    /// 玩家对象
    /// </summary>
    public Role Player { get; private set; }

    //对象根节点
    private Node2D _objectRoot;

    //对象根节点, 带y轴排序功能
    private YSort _sortRoot;

    private Node2D _mapRoot;

    private NavigationPolygonInstance _navigationPolygon;
    private Enemy _enemy;

    //可行走区域的tileId
    private List<int> _wayIds = new List<int>(new[] { 129 });

    //已经标记过的点
    private HashSet<Vector2> _usePoints = new HashSet<Vector2>();

    //导航区域数据
    private List<NavigationPolygonData> _polygonDataList = new List<NavigationPolygonData>();

    public override void _EnterTree()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;

        _sortRoot = GetNode<YSort>("SortRoot");
        _objectRoot = GetNode<Node2D>("ObjectRoot");

        _navigationPolygon = GetNode<NavigationPolygonInstance>("NavigationPolygonInstance");

        //初始化地图
        _mapRoot = GetNode<Node2D>("MapRoot");
        var node = _mapRoot.GetChild(0).GetNode("Config");
        Color color = (Color)node.GetMeta("ClearColor");
        VisualServer.SetDefaultClearColor(color);

        //创建玩家
        Player = new Player();
        Player.Position = new Vector2(100, 100);
        Player.Name = "Player";
        Player.PutDown();

        _enemy = new Enemy();
        _enemy.Name = "Enemy";
        _enemy.PutDown(new Vector2(150, 150));
    }

    public override void _Ready()
    {
        GenerateNavigationPolygon();

        //播放bgm
        SoundManager.PlayMusic(ResourcePath.resource_sound_bgm_Intro_ogg, -17f);
        _enemy.PickUpWeapon(WeaponManager.GetGun("1001"));

        WeaponManager.GetGun("1001").PutDown(new Vector2(80, 100));
        WeaponManager.GetGun("1001").PutDown(new Vector2(80, 80));
        WeaponManager.GetGun("1002").PutDown(new Vector2(80, 120));
        WeaponManager.GetGun("1003").PutDown(new Vector2(120, 80));

        WeaponManager.GetGun("1003").PutDown(new Vector2(180, 80));
        WeaponManager.GetGun("1003").PutDown(new Vector2(180, 180));
        WeaponManager.GetGun("1002").PutDown(new Vector2(180, 120));

        WeaponManager.GetGun("1004").PutDown(new Vector2(220, 120));
    }

    public override void _Process(float delta)
    {
        if (GameApplication.Instance.Debug)
        {
            Update();
        }
    }

    public override void _Draw()
    {
        if (GameApplication.Instance.Debug)
        {
            for (var i = 0; i < _polygonDataList.Count; i++)
            {
                var item = _polygonDataList[i];
                if (item.Points.Count >= 2)
                {
                    DrawPolyline(item.Points.ToArray(), Colors.Red);
                }
            }
        }
    }

    /// <summary>
    /// 获取房间根节点
    /// </summary>
    /// <param name="useYSort">是否获取 YSort 节点</param>
    /// <returns></returns>
    public Node2D GetRoot(bool useYSort = false)
    {
        return useYSort ? _sortRoot : _objectRoot;
    }

    /// <summary>
    /// 自动生成导航区域
    /// </summary>
    private void GenerateNavigationPolygon()
    {
        var tileMap = _mapRoot.GetChild(0).GetNode<TileMap>("Wall");
        var size = tileMap.CellSize;

        var rect = tileMap.GetUsedRect();

        var x = (int)rect.Position.x;
        var y = (int)rect.Position.y;
        var w = (int)rect.Size.x;
        var h = (int)rect.Size.y;

        for (int j = y; j < h; j++)
        {
            for (int i = x; i < w; i++)
            {
                var tileId = tileMap.GetCell(i, j);
                if (IsWayCell(tileId))
                {
                    if (!_usePoints.Contains(new Vector2(i, j)))
                    {
                        NavigationPolygonData polygonData = null;

                        if (!IsWayCell(tileMap.GetCell(i, j - 1)))
                        {
                            polygonData = CalcOutline(i, j, tileMap, size);
                        }
                        else if (!IsWayCell(tileMap.GetCell(i, j + 1)))
                        {
                            polygonData = CalcInline(i, j, tileMap, size);
                        }

                        if (polygonData != null)
                        {
                            _polygonDataList.Add(polygonData);
                            //return;
                        }
                    }
                }
            }
        }
    }

    private NavigationPolygonData CalcOutline(int i, int j, TileMap tileMap, Vector2 size)
    {
        var polygonData = new NavigationPolygonData();
        var points = polygonData.Points;
        // 0:右, 1:下, 2:左, 3:上
        var dir = 0;
        var offset = new Vector2(size.x * 0.5f, size.y * 0.5f);
        //找到路, 向右开始找边界
        var startPos = new Vector2(i, j);

        var tempI = i;
        var tempJ = j;

        while (true)
        {
            switch (dir)
            {
                case 0: //右
                {
                    if (IsWayCell(tileMap.GetCell(tempI, tempJ - 1))) //先向上找
                    {
                        dir = 3;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI + 1, tempJ))) //再向右找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempI++;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI, tempJ + 1))) //向下找
                    {
                        dir = 1;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }

                    return null;
                }
                case 1: //下
                {
                    if (IsWayCell(tileMap.GetCell(tempI + 1, tempJ))) //先向右找
                    {
                        dir = 0;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI, tempJ + 1))) //再向下找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempJ++;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI - 1, tempJ))) //向左找
                    {
                        dir = 2;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }

                    return null;
                }
                case 2: //左
                {
                    if (IsWayCell(tileMap.GetCell(tempI, tempJ + 1))) //先向下找
                    {
                        dir = 1;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI - 1, tempJ))) //再向左找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempI--;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI, tempJ - 1))) //向上找
                    {
                        dir = 3;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }

                    return null;
                }
                case 3: //上
                {
                    if (IsWayCell(tileMap.GetCell(tempI - 1, tempJ))) //先向左找
                    {
                        dir = 2;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI, tempJ - 1))) //再向上找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempJ--;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI + 1, tempJ))) //向右找
                    {
                        dir = 0;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }

                    return null;
                }
            }
        }
    }

    private NavigationPolygonData CalcInline(int i, int j, TileMap tileMap, Vector2 size)
    {
        var polygonData = new NavigationPolygonData();
        var points = polygonData.Points;
        // 0:右, 1:下, 2:左, 3:上
        var dir = 0;
        var offset = new Vector2(size.x * 0.5f, size.y * 0.5f);
        //找到路, 向右开始找边界
        var startPos = new Vector2(i, j);

        var tempI = i;
        var tempJ = j;

        while (true)
        {
            switch (dir)
            {
                case 0: //右
                {
                    if (IsWayCell(tileMap.GetCell(tempI, tempJ + 1))) //向下找
                    {
                        dir = 1;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI + 1, tempJ))) //再向右找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.x, tempJ * size.y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempI++;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI, tempJ - 1))) //先向上找
                    {
                        dir = 3;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }

                    return null;
                }
                case 1: //下
                {
                    if (IsWayCell(tileMap.GetCell(tempI - 1, tempJ))) //向左找
                    {
                        dir = 2;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI, tempJ + 1))) //再向下找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.x, tempJ * size.y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempJ++;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI + 1, tempJ))) //先向右找
                    {
                        dir = 0;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }

                    return null;
                }
                case 2: //左
                {
                    if (IsWayCell(tileMap.GetCell(tempI, tempJ - 1))) //向上找
                    {
                        dir = 3;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempJ--;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI - 1, tempJ))) //再向左找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.x, tempJ * size.y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempI--;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI, tempJ + 1))) //先向下找
                    {
                        dir = 1;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempJ++;
                        break;
                    }

                    return null;
                }
                case 3: //上
                {
                    if (IsWayCell(tileMap.GetCell(tempI + 1, tempJ))) //向右找
                    {
                        dir = 0;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempI++;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI, tempJ - 1))) //再向上找
                    {
                        if (points.Count == 0)
                        {
                            points.Add(new Vector2((tempI - 1) * size.x, tempJ * size.y) + offset);
                        }

                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(new Vector2(tempI, tempJ));
                        tempJ--;
                        break;
                    }
                    else if (IsWayCell(tileMap.GetCell(tempI - 1, tempJ))) //先向左找
                    {
                        dir = 2;

                        points.Add(new Vector2(tempI * size.x, tempJ * size.y) + offset);
                        var pos = new Vector2(tempI, tempJ);
                        if (points.Count > 1 && pos == startPos)
                        {
                            return polygonData;
                        }

                        PutUsePoint(pos);

                        tempI--;
                        break;
                    }

                    return null;
                }
            }
        }
    }

    private void PutUsePoint(Vector2 pos)
    {
        if (_usePoints.Contains(pos))
        {
            throw new Exception("生成导航多边形发生错误! 点: " + pos + "发生交错!");
        }

        _usePoints.Add(pos);
    }

    private bool IsWayCell(int cellId)
    {
        return cellId != -1 && _wayIds.Contains(cellId);
    }
}