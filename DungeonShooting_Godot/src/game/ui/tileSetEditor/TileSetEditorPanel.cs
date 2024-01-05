using System.Linq;
using Godot;

namespace UI.TileSetEditor;

public partial class TileSetEditorPanel : TileSetEditor
{
    /// <summary>
    /// 编辑使用的 tileSetInfo 数据
    /// </summary>
    public TileSetInfo TileSetInfo { get; private set; }
    public TileSetInfo OriginTileSetInfo { get; private set; }
    
    /// <summary>
    /// 是否初始化过纹理
    /// </summary>
    public bool InitTexture { get; private set; }
    
    /// <summary>
    /// 纹理
    /// </summary>
    public ImageTexture Texture { get; private set; }
    
    /// <summary>
    /// 纹理的Image对象
    /// </summary>
    public Image TextureImage { get; private set; }

    /// <summary>
    /// 背景颜色
    /// </summary>
    public Color BgColor { get; private set; }
    
    /// <summary>
    /// Cell 横轴数量
    /// </summary>
    public int CellHorizontal { get; private set; }
    
    /// <summary>
    /// Cell 纵轴数量
    /// </summary>
    public int CellVertical { get; private set; }

    /// <summary>
    /// 页签对象
    /// </summary>
    public UiGrid<Tab, TileSetEditorTabData> TabGrid { get; private set; }

    public override void OnCreateUi()
    {
        Texture = new ImageTexture();
        S_Back.Instance.Visible = PrevUi != null;
        S_Back.Instance.Pressed += OnBackClick;

        TabGrid = new UiGrid<Tab, TileSetEditorTabData>(S_Tab, typeof(TileSetEditorTabCell));
        TabGrid.SetHorizontalExpand(true);
        TabGrid.SetCellOffset(new Vector2I(0, 5));
        TabGrid.Add(new TileSetEditorTabData()
        {
            Text = "纹理",
            UiName = UiManager.UiNames.TileSetEditorImport,
        });
        TabGrid.Add(new TileSetEditorTabData()
        {
            Text = "地形",
            UiName = UiManager.UiNames.TileSetEditorTerrain,
        });
        TabGrid.Add(new TileSetEditorTabData()
        {
            Text = "组合",
            UiName = UiManager.UiNames.TileSetEditorCombination,
        });
        TabGrid.Visible = false;

        S_DeleteButton.Instance.Pressed += OnDeleteSourceClick;
        S_AddButton.Instance.Pressed += OnAddSourceClick;
        S_OptionButton.Instance.ItemSelected += OnOptionChange;
    }

    public override void OnDestroyUi()
    {
        TabGrid.Destroy();
        if (Texture != null)
        {
            Texture.Dispose();
        }

        if (TextureImage != null)
        {
            TextureImage.Dispose();
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData(TileSetInfo tileSetInfo)
    {
        OriginTileSetInfo = tileSetInfo;
        TileSetInfo = tileSetInfo.Clone();
        S_Title.Instance.Text = "正在编辑：" + TileSetInfo.Name;
        
        //SetTextureData(Image.LoadFromFile("resource/tileSprite/map1/16x16 dungeon ii wall reconfig v04 spritesheet.png"));
        //TabGrid.SelectIndex = 0;
    }

    /// <summary>
    /// 设置纹理
    /// </summary>
    public void SetTextureData(Image image)
    {
        InitTexture = true;
        Texture.SetImage(image);
        if (TextureImage != null)
        {
            TextureImage.Dispose();
        }
        TextureImage = image;
        CellHorizontal = image.GetWidth() / GameConfig.TileCellSize;
        CellVertical = image.GetHeight() / GameConfig.TileCellSize;
        
        //派发事件
        EventManager.EmitEvent(EventEnum.OnSetTileTexture, Texture);
    }

    /// <summary>
    /// 设置背景颜色
    /// </summary>
    public void SetBgColor(Color color)
    {
        BgColor = color;
        
        //派发事件
        EventManager.EmitEvent(EventEnum.OnSetTileSetBgColor, color);
    }
    
    /// <summary>
    /// 将二维位置转换为索引的函数
    /// </summary>
    public int CellPositionToIndex(Vector2I pos)
    {
        return pos.Y * CellHorizontal + pos.X;
    }
    
    /// <summary>
    /// 返回Cell的坐标是否在纹理区域内
    /// </summary>
    public bool IsCellPositionInTexture(Vector2I cell)
    {
        return cell.X >= 0 && cell.Y >= 0 && cell.X < CellHorizontal && cell.Y < CellVertical;
    }

    //返回上一级按钮点击
    private void OnBackClick()
    {
        OpenPrevUi();
    }

    //删除资源
    private void OnDeleteSourceClick()
    {
        var optionButton = S_OptionButton.Instance;
        var selectIndex = optionButton.Selected;
        if (selectIndex >= 0)
        {
            EditorWindowManager.ShowConfirm("提示", "是否需要删除该资源!", v =>
            {
                if (v)
                {
                    var name = optionButton.GetItemText(selectIndex);
                    var findIndex = TileSetInfo.Sources.FindIndex(info => info.Name == name);
                    if (findIndex >= 0)
                    {
                        TileSetInfo.Sources.RemoveAt(findIndex);
                    }

                    var index = optionButton.ItemCount - 2;
                    optionButton.RemoveItem(selectIndex);
                    optionButton.Selected = index;
                    OnOptionChange(index);
                }
            });
        }
        else
        {
            EditorWindowManager.ShowTips("提示", "请选择需要删除的资源！");
        }
    }

    //创建资源
    private void OnAddSourceClick()
    {
        EditorWindowManager.ShowInput("创建资源", "资源名称：", null, (value, isSubmit) =>
        {
            if (isSubmit)
            {
                if (TileSetInfo.Sources.FindIndex(info => info.Name == value) >= 0)
                {
                    EditorWindowManager.ShowTips("错误", "该资源名称已存在！");
                    return false;
                }
                
                var source = new TileSetSourceInfo();
                source.InitData();
                source.Name = value;
                TileSetInfo.Sources.Add(source);
                
                EventManager.EmitEvent(EventEnum.OnCreateTileSetSource, source);
                var optionButton = S_OptionButton.Instance;
                optionButton.AddItem(value);
                var selectIndex = optionButton.ItemCount - 1;
                optionButton.Selected = selectIndex;
                OnOptionChange(selectIndex);
            }

            return true;
        });
    }

    //选中资源
    private void OnOptionChange(long index)
    {
        if (index >= 0)
        {
            TabGrid.Visible = true;
            TabGrid.SelectIndex = 0;
        }
        else
        {
            TabGrid.Visible = false;
            TabGrid.SelectIndex = -1;
        }
        EventManager.EmitEvent(EventEnum.OnSelectTileSetSource);
    }
}
