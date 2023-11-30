using System.Linq;
using Godot;

namespace UI.RoomMap;

public partial class RoomMapPanel : RoomMap
{

    public override void OnCreateUi()
    {
        DrawRoom();
    }

    public override void OnDestroyUi()
    {
    }

    public override void Process(float delta)
    {
        // //按下地图按键
        // if (InputManager.Map && !S_RoomMap.Instance.IsOpen)
        // {
        //     World.Current.Pause = true;
        //     S_RoomMap.Instance.ShowUi();
        // }
        // else if (!InputManager.Map && S_RoomMap.Instance.IsOpen)
        // {
        //     S_RoomMap.Instance.HideUi();
        //     World.Current.Pause = false;
        // }
        
        S_Root.Instance.Position = S_DrawContainer.Instance.Size / 2 - Player.Current.Position / 16 * S_Root.Instance.Scale;
    }
    
    private void DrawRoom()
    {
        var startRoom = GameApplication.Instance.DungeonManager.StartRoomInfo;
        startRoom.EachRoom(roomInfo =>
        {
            //房间区域
            var sprite = new Sprite2D();
            sprite.Centered = false;
            sprite.Texture = roomInfo.PreviewTexture;
            sprite.Position = roomInfo.Position;
            var material = ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Outline2_tres);
            material.SetShaderParameter("outline_color", new Color(1, 1, 1, 0.9f));
            material.SetShaderParameter("scale", 0.5f);
            sprite.Material = material;
            S_Root.AddChild(sprite);
            
            //过道
            if (roomInfo.Doors != null)
            {
                foreach (var doorInfo in roomInfo.Doors)
                {
                    if (doorInfo.IsForward)
                    {
                        var aisleSprite = new Sprite2D();
                        aisleSprite.Centered = false;
                        aisleSprite.Texture = doorInfo.AislePreviewTexture;
                        //调整过道预览位置
                        
                        if (!doorInfo.HasCross) //不含交叉点
                        {
                            if (doorInfo.Direction == DoorDirection.N)
                            {
                                aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(0, doorInfo.AislePreviewTexture.GetHeight() - 1);
                            }
                            else if (doorInfo.Direction == DoorDirection.S)
                            {
                                aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(0, 1);
                            }
                            else if (doorInfo.Direction == DoorDirection.E)
                            {
                                aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(1, 0);
                            }
                            else if (doorInfo.Direction == DoorDirection.W)
                            {
                                aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(doorInfo.AislePreviewTexture.GetWidth() - 1, 0);
                            }
                        }
                        else //包含交叉点
                        {
                            if (doorInfo.Direction == DoorDirection.S)
                            {
                                if (doorInfo.ConnectDoor.Direction == DoorDirection.E)
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(doorInfo.AislePreviewTexture.GetWidth() - 4, 1);
                                }
                                else if (doorInfo.ConnectDoor.Direction == DoorDirection.W)
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(0, 1);
                                }
                                else
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition;
                                }
                            }
                            else if (doorInfo.Direction == DoorDirection.N)
                            {
                                if (doorInfo.ConnectDoor.Direction == DoorDirection.W)
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(0, doorInfo.AislePreviewTexture.GetHeight() - 1);
                                }
                                else
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition;
                                }
                            }
                            else if (doorInfo.Direction == DoorDirection.W)
                            {
                                if (doorInfo.ConnectDoor.Direction == DoorDirection.N)
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(doorInfo.AislePreviewTexture.GetWidth() - 1, 0);
                                }
                                else
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition;
                                }
                            }
                            else if (doorInfo.Direction == DoorDirection.E)
                            {
                                if (doorInfo.ConnectDoor.Direction == DoorDirection.S)
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(1, doorInfo.AislePreviewTexture.GetHeight() - 4);
                                }
                                else if (doorInfo.ConnectDoor.Direction == DoorDirection.N)
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition - new Vector2I(1, 0);
                                }
                                else
                                {
                                    aisleSprite.Position = doorInfo.OriginPosition;
                                }
                            }
                        }

                        var aisleSpriteMaterial = ResourceManager.Load<ShaderMaterial>(ResourcePath.resource_material_Outline2_tres);
                        aisleSpriteMaterial.SetShaderParameter("outline_color", new Color(1, 1, 1, 0.9f));
                        aisleSpriteMaterial.SetShaderParameter("scale", 0.5f);
                        aisleSprite.Material = aisleSpriteMaterial;
                        S_Root.AddChild(aisleSprite);
                    }
                }
            }
        });

    }
}
