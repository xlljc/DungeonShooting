using Godot;

namespace UI.TileSetEditor;

public partial class TileSetEditorPanel : TileSetEditor
{
    public override void OnCreateUi()
    {
        S_Back.Instance.Visible = PrevUi != null;
        S_Back.Instance.Pressed += OnBackClick;
    }

    public void InitData(TileSetInfo tileSetInfo)
    {
        S_Title.Instance.Text = "正在编辑：" + tileSetInfo.Name;
    }

    //返回上一级按钮点击
    private void OnBackClick()
    {
        OpenPrevUi();
    }
}
