using System.Linq;
using Godot;

namespace UI.TileSetEditor;

public partial class TileSetEditorPanel : TileSetEditor
{
    /// <summary>
    /// 数据是否脏了
    /// </summary>
    public bool IsDirty { get; private set; }

    /// <summary>
    /// 编辑使用的 tileSetSplit 数据
    /// </summary>
    public TileSetSplit TileSetSplit { get; private set; }

    /// <summary>
    /// 编辑使用的 tileSetInfo 数据
    /// </summary>
    public TileSetInfo TileSetInfo { get; private set; }
    
    /// <summary>
    /// 当前正在使用的 TileSetSourceInfo 数据
    /// </summary>
    public TileSetSourceInfo TileSetSourceInfo { get; private set; }
    
    /// <summary>
    /// 当前正在使用的 TileSetSourceInfo 的索引
    /// </summary>
    public int TileSetSourceIndex { get; private set; }
    
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

    private Image _emptyImage;

    public override void OnCreateUi()
    {
        _emptyImage = Image.Create(1, 1, false, Image.Format.Rgba8);
        Texture = new ImageTexture();
        Texture.SetImage(_emptyImage);
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
        S_Save.Instance.Pressed += OnSaveClick;
        AddEventListener(EventEnum.OnTileSetDirty, OnTileSetDirty);
    }

    public override void OnDestroyUi()
    {
        TabGrid.Destroy();
        if (Texture != null)
        {
            Texture.Dispose();
        }
        
        _emptyImage.Dispose();
        TextureImage = null;

        if (TileSetInfo != null)
        {
            TileSetInfo.Destroy();
        }
    }

    public override void Process(float delta)
    {
        if (TabGrid.Visible && TileSetSourceInfo != null)
        {
            //纹理红点
            ((TileSetEditorTabCell)TabGrid.GetCell(0)).CellNode.L_ErrorIcon.Instance.Visible = !InitTexture;
            //地形红点
            var terrainTab = (TileSetEditorTabCell)TabGrid.GetCell(1);
            var flag = true;
            foreach (var terrainInfo in TileSetSourceInfo.Terrain)
            {
                if (!terrainInfo.Ready)
                {
                    flag = false;
                    break;
                }
            }
            terrainTab.CellNode.L_ErrorIcon.Instance.Visible = !flag;
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData(TileSetSplit tileSetSplit)
    {
        IsDirty = false;
        tileSetSplit.ReloadTileSetInfo();
        TileSetSplit = tileSetSplit;
        TileSetInfo = tileSetSplit.TileSetInfo.Clone();
        RefreshTitle();
        
        //初始化下拉框
        if (TileSetInfo.Sources.Count > 0)
        {
            var optionButton = S_OptionButton.Instance;
            foreach (var tileSetSourceInfo in TileSetInfo.Sources)
            {
                optionButton.AddItem(tileSetSourceInfo.Name);
            }

            optionButton.Selected = 0;
            OnOptionChange(0);
        }
    }

    //设置标题显示内容
    private void RefreshTitle()
    {
        if (IsDirty)
        {
            S_Title.Instance.Text = "正在编辑：" + TileSetInfo.Name + "*";
        }
        else
        {
            S_Title.Instance.Text = "正在编辑：" + TileSetInfo.Name;
        }
    }

    /// <summary>
    /// 设置纹理使用的纹理
    /// </summary>
    public void SetTextureData(Image image)
    {
        if (image == null)
        {
            InitTexture = false;
            TextureImage = null;
            Texture.SetImage(_emptyImage);
            CellHorizontal = 0;
            CellVertical = 0;
        }
        else
        {
            if (TileSetSourceInfo == null)
            {
                return;
            }
        
            InitTexture = true;
            Texture.SetImage(image);
            TextureImage = image;
            CellHorizontal = image.GetWidth() / GameConfig.TileCellSize;
            CellVertical = image.GetHeight() / GameConfig.TileCellSize;
        }
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
        if (IsDirty)
        {
            EditorWindowManager.ShowConfirm("提示", "当前TileSet修改的数据还未保存，是否退出编辑？",
                "保存并退出", "直接退出", "取消"
                , index =>
                {
                    if (index == 0) //保存并退出
                    {
                        OnSaveClick();
                        OpenPrevUi();
                    }
                    else if (index == 1) //直接退出
                    {
                        OpenPrevUi();
                    }
                }
            );
        }
        else
        {
            OpenPrevUi();
        }
    }

    //删除资源
    private void OnDeleteSourceClick()
    {
        var optionButton = S_OptionButton.Instance;
        var selectIndex = optionButton.Selected;
        if (selectIndex == 0)
        {
            EditorWindowManager.ShowTips("警告", "不允许删除 Main Source！");
            return;
        }
        else if (selectIndex > 0)
        {
            EditorWindowManager.ShowConfirm("提示", "是否需要删除该资源!", v =>
            {
                if (v)
                {
                    var name = optionButton.GetItemText(selectIndex);
                    var findIndex = TileSetInfo.Sources.FindIndex(info => info.Name == name);
                    if (findIndex >= 0)
                    {
                        TileSetInfo.Sources[findIndex].Destroy();
                        TileSetInfo.Sources.RemoveAt(findIndex);
                    }

                    var index = optionButton.ItemCount - 2;
                    optionButton.RemoveItem(selectIndex);
                    optionButton.Selected = index;
                    OnOptionChange(index);
                    
                    //标记数据脏了
                    EventManager.EmitEvent(EventEnum.OnTileSetDirty);
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
            TileSetSourceInfo = TileSetInfo.Sources[(int)index];
            TileSetSourceIndex = (int)index;
            SetTextureData(TileSetSourceInfo.GetSourceImage());
        }
        else
        {
            TabGrid.Visible = false;
            TabGrid.SelectIndex = -1;
            TileSetSourceInfo = null;
            TileSetSourceIndex = -1;
            SetTextureData(null);
        }
        
        //派发选择资源事件
        EventManager.EmitEvent(EventEnum.OnSelectTileSetSource, TileSetSourceInfo);
    }

    //保存
    private void OnSaveClick()
    {
        TileSetSplit.SetTileSetInfo(TileSetInfo.Clone());
        EventManager.EmitEvent(EventEnum.OnTileSetSave, TileSetSplit);
        IsDirty = false;
        RefreshTitle();
    }
    
    //数据脏了
    private void OnTileSetDirty(object obj)
    {
        IsDirty = true;
        RefreshTitle();
    }
}
