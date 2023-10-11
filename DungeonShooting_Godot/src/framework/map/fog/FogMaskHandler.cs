
using System;
using Godot;

public static class FogMaskHandler
{
    private static RoomInfo _prevRoomInfo;
    private static RoomInfo _tempRoomInfo;
    private static RoomDoorInfo _tempDoorInfo;

    public static void RefreshRoomFog(RoomInfo roomInfo)
    {
        _tempRoomInfo = roomInfo;
    }

    public static void RefreshAisleFog(RoomDoorInfo doorInfo)
    {
        if (_prevRoomInfo != null)
        {
            if (doorInfo.RoomInfo == _prevRoomInfo)
            {
                _tempDoorInfo = doorInfo;
            }
            else if (doorInfo.ConnectDoor.RoomInfo == _prevRoomInfo)
            {
                _tempDoorInfo = doorInfo.ConnectDoor;
            }
            return;
        }
        _tempDoorInfo = doorInfo;
    }

    public static void Update()
    {
        if (_tempRoomInfo != null)
        {
            //刷新房间会附带刷新过道
            _RefreshRoomFog(_tempRoomInfo);
        }
        else if (_tempDoorInfo != null)
        {
            //刷新过道
            _RefreshAisleFog(_tempDoorInfo);
        }

        _tempRoomInfo = null;
        _tempDoorInfo = null;
    }
    
    private static void _RefreshRoomFog(RoomInfo roomInfo)
    {
        if (_prevRoomInfo != roomInfo)
        {
            GD.Print($"切换房间: {_prevRoomInfo?.Id} => {roomInfo.Id}");
            if (_prevRoomInfo != null)
            {
                //房间变暗
                _prevRoomInfo.RoomFogMask.TransitionAlpha(GameConfig.DarkFogAlpha);
                //刷新预览区域
                foreach (var roomInfoDoor in _prevRoomInfo.Doors)
                {
                    //不是连接到当前房间的, 过道通通变暗
                    if (roomInfoDoor.ConnectDoor.RoomInfo != roomInfo)
                    {
                        var prevAisleFog = roomInfoDoor.AisleFogMask;
                        if (!prevAisleFog.IsExplored)
                        {
                            roomInfoDoor.PreviewAisleFogMask.TransitionToDark();
                        }
                        else
                        {
                            prevAisleFog.TransitionToDark();
                            if (roomInfoDoor.ConnectDoor.PreviewRoomFogMask.Visible)
                            {
                                if (roomInfoDoor.ConnectDoor.RoomInfo.RoomFogMask.IsExplored)
                                {
                                    roomInfoDoor.ConnectDoor.PreviewRoomFogMask.TransitionToHide();
                                }
                                else
                                {
                                    roomInfoDoor.ConnectDoor.PreviewRoomFogMask.TransitionToDark();
                                }
                            }
                        }
                    }
                }
            }

            _prevRoomInfo = roomInfo;
        }
        GD.Print("RefreshRoomFog: " + roomInfo.Id);
        var fogMask = roomInfo.RoomFogMask;
        
        if (!fogMask.IsExplored) //未探索该区域
        {
            fogMask.IsExplored = true;
            fogMask.TransitionAlpha(0, 1);
            
            //刷新预览区域
            foreach (var roomInfoDoor in roomInfo.Doors)
            {
                if (roomInfoDoor.AisleFogMask.IsExplored) //探索过, 执行过道刷新逻辑
                {
                    _RefreshAisleFog(roomInfoDoor);
                }
                else //未探索
                {
                    //显示预览过道
                    roomInfoDoor.PreviewRoomFogMask.SetActive(false);
                    roomInfoDoor.PreviewAisleFogMask.SetActive(true);
                    roomInfoDoor.PreviewAisleFogMask.TransitionAlpha(1);
                }
            }
        }
        else //已经探索过
        {
            //变亮
            fogMask.TransitionAlpha(GameConfig.DarkFogAlpha, 1);
            
            foreach (var roomInfoDoor in roomInfo.Doors)
            {
                if (roomInfoDoor.AisleFogMask.IsExplored) //探索过, 执行过道刷新逻辑
                {
                    _RefreshAisleFog(roomInfoDoor);
                }
                else //未探索
                {
                    //显示预览过道
                    roomInfoDoor.PreviewRoomFogMask.SetActive(false);
                    roomInfoDoor.PreviewAisleFogMask.SetActive(true);
                    roomInfoDoor.PreviewAisleFogMask.TransitionAlpha(1);
                }
            }
        }
    }

    private static void _RefreshAisleFog(RoomDoorInfo doorInfo)
    {
        GD.Print("RefreshAisleFog: " + doorInfo.RoomInfo.Id + doorInfo.Direction);
        var fogMask = doorInfo.AisleFogMask;

        var connectDoor = doorInfo.ConnectDoor;
        if (!fogMask.IsExplored) //未探索该区域
        {
            fogMask.IsExplored = true;
            doorInfo.PreviewAisleFogMask.IsExplored = true;
            doorInfo.PreviewRoomFogMask.IsExplored = true;
            doorInfo.ConnectDoor.PreviewAisleFogMask.IsExplored = true;
            doorInfo.ConnectDoor.PreviewRoomFogMask.IsExplored = true;
            fogMask.TransitionAlpha(1);
            
            //隐藏预览
            if (doorInfo.PreviewAisleFogMask.Visible)
            {
                doorInfo.PreviewAisleFogMask.TransitionToHide();
            }
            
            //显示下一个房间预览
            if (!connectDoor.RoomInfo.RoomFogMask.IsExplored)
            {
                connectDoor.PreviewRoomFogMask.SetActive(true);
                connectDoor.PreviewRoomFogMask.TransitionAlpha(0, 1);
            }
        }
        else //已经探索过
        {
            if (doorInfo.Door.IsClose) //房间已经关门
            {
                //过道迷雾变暗
                fogMask.TransitionToDark();
                //隐藏房间预览
                if (doorInfo.PreviewRoomFogMask.Visible)
                {
                    doorInfo.PreviewRoomFogMask.TransitionToHide();
                }
                //显示过道预览
                if (!doorInfo.PreviewAisleFogMask.Visible)
                {
                    doorInfo.PreviewAisleFogMask.SetActive(true);
                    doorInfo.PreviewAisleFogMask.TransitionAlpha(0, 1 - doorInfo.AisleFogMask.TargetAlpha);
                }
            }
            else
            {
                //过道迷雾变亮
                fogMask.TransitionToLight();
                
                //隐藏房间预览
                if (doorInfo.PreviewRoomFogMask.Visible)
                {
                    doorInfo.PreviewRoomFogMask.TransitionToHide();
                }
                //隐藏过道预览
                if (doorInfo.PreviewAisleFogMask.Visible)
                {
                    doorInfo.PreviewAisleFogMask.TransitionToHide();
                }
                //连接的房间显示房间预览
                if (!connectDoor.PreviewRoomFogMask.Visible)
                {
                    connectDoor.PreviewRoomFogMask.SetActive(true);
                }

                var tempA = 1 - connectDoor.RoomInfo.RoomFogMask.TargetAlpha;
                if (Math.Abs(connectDoor.PreviewRoomFogMask.TargetAlpha - tempA) > 0.001f)
                {
                    if (connectDoor.RoomInfo.RoomFogMask.IsExplored)
                    {
                        connectDoor.PreviewRoomFogMask.TransitionAlpha(0, tempA);
                    }
                    else
                    {
                        connectDoor.PreviewRoomFogMask.TransitionAlpha(tempA);
                    }
                }

                //连接的房间隐藏过道预览
                if (connectDoor.PreviewAisleFogMask.Visible)
                {
                    connectDoor.PreviewAisleFogMask.TransitionToHide();
                }
            }
        }
    }

    public static void ClearRecordRoom()
    {
        _prevRoomInfo = null;
        _tempDoorInfo = null;
        _tempRoomInfo = null;
    }
}