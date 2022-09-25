using System.Text.RegularExpressions;
using Godot;

namespace DScript.GodotEditor
{
    /// <summary>
    /// 代码提示管理类
    /// </summary>
    public static class CodeHintManager
    {
        /// <summary>
        /// 是否使用 enter 输入过代码
        /// </summary>
        internal static bool EnterInput = false;

        /// <summary>
        /// 按下快捷键 ctrl + / 触发显示提示面板
        /// </summary>
        [EditorShortcutKey(KeyList.Slash, Ctrl = true)]
        private static void TriggerInput()
        {
            var textEditor = CodeTextEdit.CurrentTextEdit;
            if (textEditor != null && textEditor.HasFocus())
            {
                RequestSyntaxTree(textEditor);
            }
        }

        /// <summary>
        /// 触发编辑器输入
        /// </summary>
        /// <param name="textEdit">当前活动的编辑器</param>
        public static void TriggerInput(CodeTextEdit textEdit)
        {
            var column = textEdit.CursorGetColumn();
            if (column > 0)
            {
                var line = textEdit.CursorGetLine();
                var str = textEdit.GetTextInRange(line, column - 1, line, column);
                //判断前一个字符串是否能触发提示
                if (Regex.IsMatch(str, "[\\.\\w]"))
                {
                    RequestSyntaxTree(textEdit);
                }
            }
        }

        /// <summary>
        /// 结束输入, 关闭提示弹窗
        /// </summary>
        public static void OverInput()
        {
            CodeHintPanel.Instance.HidePanel();
        }

        /// <summary>
        /// 返回提示面板是否显示
        /// </summary>
        public static bool IsShowPanel()
        {
            return CodeHintPanel.Instance.Visible;
        }

        /// <summary>
        /// 显示提示面板
        /// </summary>
        private static void ShowPanel(CodeTextEdit textEdit)
        {
            //先确定面板位置
            var line = textEdit.CursorGetLine();
            var column = textEdit.CursorGetColumn();

            Vector2 pos =
                textEdit.EditPainter.ToPainterPosition(
                    textEdit.GetPosAtLineColumn(line, column == 0 ? 0 : (column - 1)));
            CodeHintPanel.Instance.ShowPanel(textEdit, pos);
        }

        /// <summary>
        /// 请求语法树, 结合上下文, 判断是否能弹出
        /// </summary>
        private static void RequestSyntaxTree(CodeTextEdit textEdit)
        {
            ShowPanel(textEdit);
        }
    }
}