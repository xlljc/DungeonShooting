using System.Linq;
using Godot;

namespace UI.MapEditor;

public partial class MapEditorPanel : MapEditor
{
    private class CheckResult
    {
        /// <summary>
        /// 是否存在错误
        /// </summary>
        public bool HasError;
        /// <summary>
        /// 房间异常类型
        /// </summary>
        public RoomErrorType ErrorType;

        public CheckResult(bool hasError, RoomErrorType errorType)
        {
            HasError = hasError;
            ErrorType = errorType;
        }
    }
    
    private string _title;
    
    public override void OnCreateUi()
    {
        S_TabContainer.Instance.SetTabTitle(0, "图层");
        S_TabContainer.Instance.SetTabTitle(1, "图块");
        S_TabContainer.Instance.SetTabTitle(2, "对象");
        //S_MapLayer.Instance.Init(S_MapLayer);
        S_Left.Instance.Resized += OnMapViewResized;
        S_Back.Instance.Pressed += OnBackClick;
        S_Save.Instance.Pressed += OnSave;
        S_Play.Instance.Pressed += OnPlay;
    }

    public override void OnShowUi()
    {
        OnMapViewResized();
    }

    public override void OnDestroyUi()
    {
        //清除选中的标记
        EditorTileMapManager.SetSelectMark(null);
        //清除选中的波
        EditorTileMapManager.SetSelectWaveIndex(-1);
        //清除选中的预设
        EditorTileMapManager.SetSelectPreinstallIndex(-1);
    }

    //点击播放按钮
    private void OnPlay()
    {
        S_TileMap.Instance.TryRunCheckHandler();
        var check = CheckError();
        //有错误数据
        if (check.HasError)
        {
            EditorWindowManager.ShowTips("提示", EditorTileMapManager.GetRoomErrorTypeMessage(check.ErrorType) + "，不能运行房间！");
            return;
        }
        //保存数据
        S_TileMap.Instance.TriggerSave(RoomErrorType.None, () =>
        {
            var groupName = EditorTileMapManager.SelectDungeonGroup.GroupName;
            var result = DungeonManager.CheckDungeon(groupName);
            if (result.HasError)
            {
                EditorWindowManager.ShowTips("警告", "当前组'" + groupName + "'" + result.ErrorMessage + ", 不能生成地牢!");
            }
            else
            {
                //执行运行
                EditorPlayManager.Play(this);
            }
        });
    }

    /// <summary>
    /// 加载地牢, 返回是否加载成功
    /// </summary>
    public bool LoadMap(DungeonRoomSplit roomSplit, TileSetSplit tileSetSplit)
    {
        _title = "正在编辑：" + roomSplit.RoomInfo.RoomName;
        S_Title.Instance.Text = _title;
        
        //加载MapTile面板
        S_MapEditorMapTile.Instance.InitData(tileSetSplit);
        
        //加载Tile
        var loadMap = S_TileMap.Instance.Load(roomSplit, tileSetSplit);
        S_MapEditorMapMark.Instance.RefreshPreinstallSelect();
        return loadMap;
    }

    /// <summary>
    /// 设置标题是否带 *
    /// </summary>
    public void SetTitleDirty(bool value)
    {
        if (value)
        {
            S_Title.Instance.Text = _title + "*";
        }
        else
        {
            S_Title.Instance.Text = _title;
        }
    }
    
    //调整地图显示区域大小
    private void OnMapViewResized()
    {
        S_SubViewport.Instance.Size = S_Left.Instance.Size.AsVector2I() - new Vector2I(4, 4);
    }
    
    //保存地图数据
    private void OnSave()
    {
        S_TileMap.Instance.TryRunCheckHandler();
        var check = CheckError();
        //有错误的数据
        if (check.HasError)
        {
            EditorWindowManager.ShowConfirm("提示", EditorTileMapManager.GetRoomErrorTypeMessage(check.ErrorType) + "，如果现在保存并退出，运行游戏将不会刷出该房间！\n是否仍要保存？", (v) =>
            {
                if (v)
                {
                    S_TileMap.Instance.TriggerSave(check.ErrorType, null);
                }
            });
        }
        else
        {
            S_TileMap.Instance.TriggerSave(check.ErrorType, null);
        }
    }

    //点击返回
    private void OnBackClick()
    {
        S_TileMap.Instance.TryRunCheckHandler();
        if (S_TileMap.Instance.IsDirty) //有修改的数据, 不能直接退出, 需要询问用户是否保存
        {
            EditorWindowManager.ShowConfirm("提示", "当前房间修改的数据还未保存，是否退出编辑房间？",
                "保存并退出", "直接退出", "取消"
                , index =>
            {
                if (index == 0) //保存并退出
                {
                    var check = CheckError();
                    if (check.HasError) //有错误
                    {
                        EditorWindowManager.ShowConfirm("提示", EditorTileMapManager.GetRoomErrorTypeMessage(check.ErrorType) + "，如果现在保存并退出，运行游戏将不会刷出该房间！\n是否仍要保存？", (v) =>
                        {
                            if (v)
                            {
                                S_TileMap.Instance.TriggerSave(check.ErrorType, () =>
                                {
                                    //返回上一个Ui
                                    OpenPrevUi();
                                });
                            }
                            else
                            {
                                //返回上一个Ui
                                OpenPrevUi();
                            }
                        });
                    }
                    else //没有错误
                    {
                        S_TileMap.Instance.TriggerSave(check.ErrorType, () =>
                        {
                            //返回上一个Ui
                            OpenPrevUi();
                        });
                    }
                }
                else if (index == 1)
                {
                    //返回上一个Ui
                    OpenPrevUi();
                }
            });
        }
        else //没有修改数据
        {
            var check = CheckError();
            if (check.HasError) //有错误
            {
                EditorWindowManager.ShowConfirm("提示", EditorTileMapManager.GetRoomErrorTypeMessage(check.ErrorType) + "，如果不解决错误就退出，运行游戏将不会刷出该房间！\n是否仍要退出？", (v) =>
                {
                    if (v)
                    {
                        //返回上一个Ui
                        OpenPrevUi();
                    }
                });
            }
            else //没有错误
            {
                //返回上一个Ui
                OpenPrevUi();
            }
        }
    }

    private CheckResult CheckError()
    {
        var editorTileMap = S_TileMap.Instance;
        if (editorTileMap.CurrRoomSize == Vector2I.Zero)
        {
            return new CheckResult(true, RoomErrorType.Empty);
        }
        else if (editorTileMap.HasTerrainError) //地图绘制错误
        {
            return new CheckResult(true, RoomErrorType.TileError);
        }

        if (editorTileMap.CurrDoorConfigs.Count > 0)
        {
            var flag = false;
            var dir = -1;
            foreach (var roomInfoDoorAreaInfo in editorTileMap.CurrDoorConfigs)
            {
                if (dir == -1)
                {
                    dir = (int)roomInfoDoorAreaInfo.Direction;
                }
                else if (dir != (int)roomInfoDoorAreaInfo.Direction)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                return new CheckResult(true, RoomErrorType.DoorAreaError);
            }
        }
        
        if (EditorTileMapManager.SelectRoom.Preinstall == null || EditorTileMapManager.SelectRoom.Preinstall.Count == 0)
        {
            return new CheckResult(true, RoomErrorType.NoPreinstallError);
        }
        
        return new CheckResult(false, RoomErrorType.None);
    }
}
