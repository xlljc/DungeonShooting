using System.Collections.Generic;
using System.Text.RegularExpressions;
using Godot;

namespace DScript.GodotEditor
{
	/// <summary>
	/// 代码补全提示
	/// </summary>
	public class CodeHintPanel : Popup
	{
		private class CodeHintItemData
		{
			public CodeHintItem CodeHintItem;
			public bool VisibleFlag;
		}
		
		//补全选项
		[Export] private PackedScene CodeHintItem;

		/// <summary>
		/// 获取实例
		/// </summary>
		public static CodeHintPanel Instance { get; private set; }

		/// <summary>
		/// 当前选提示组件活动的项
		/// </summary>
		public int ActiveIndex
		{
			get => _activeIndex;
			set => SetActiveIndex(value);
		}
		private int _activeIndex = -1;

		//滑动容器
		private ScrollContainer _scrollContainer;
		//提示项父容器
		private VBoxContainer _itemContainer;

		//包含提示数据
		private List<CodeHintData> _hintDataList = new List<CodeHintData>();
		//当前已启用项的列表
		private List<CodeHintItemData> _activeItemList = new List<CodeHintItemData>();
		
		//长按计时器
		private float _clickTimer = 0;
		private bool _continuousFlag = false;

		//当前的文本编辑器对象
		private CodeTextEdit _textEdit;
		private int startLine;
		private int startcColumn;
		
		public CodeHintPanel()
		{
			Instance = this;
		}

		public override void _Ready()
		{
			_scrollContainer = GetNode<ScrollContainer>("ScrollContainer");
			_itemContainer = _scrollContainer.GetNode<VBoxContainer>("VBoxContainer");

			for (int i = 0; i < CodeTextEdit.KeyCodes.Length; i++)
			{
				_hintDataList.Add(new CodeHintData() { Text = CodeTextEdit.KeyCodes[i] });
				// var item = CreateItem();
				// item.CodeHintItem.CodeText = CodeTextEdit.KeyCodes[i];
			}
		}

		public override void _Process(float delta)
		{
			if (!Visible) return;
			if (_textEdit == null)
			{
				Visible = false;
				_textEdit = null;
				return;
			}
			
			//长按判定
			var down = Input.IsActionPressed("ui_down");
			var up = Input.IsActionPressed("ui_up");
			//是否触发选项移动
			bool clickFlag = false;
			if (down || up)
			{
				_clickTimer += delta;
				if ((!_continuousFlag && _clickTimer > 0.5f) || (_continuousFlag && _clickTimer > 0.06f))
				{
					_continuousFlag = true;
					_clickTimer %= 0.06f;
					clickFlag = true;
				}
			}
			else
			{
				_clickTimer = 0;
				_continuousFlag = false;
			}
			
			//选项移动判定
			if ((Input.IsActionJustPressed("ui_down") || (clickFlag && down)) && _activeItemList.Count > 1) //按下下键
			{
				var index = ActiveIndex;
				index += 1;
				if (index >= _activeItemList.Count)
				{
					index = 0;
				}

				ActiveIndex = index;
			}
			else if ((Input.IsActionJustPressed("ui_up") || (clickFlag && up)) && _activeItemList.Count > 1) //按下上键
			{
				var index = ActiveIndex;
				index -= 1;
				if (index < 0)
				{
					index = _activeItemList.Count - 1;
				}

				ActiveIndex = index;
			}
		}

		public override void _Input(InputEvent @event)
		{
			if (!Visible) return;
			if (@event is InputEventKey eventKey)
			{
				if (eventKey.IsPressed())
				{
					//按下左,右,空格时隐藏提示框
					if (eventKey.Scancode == (int)KeyList.Left || eventKey.Scancode == (int)KeyList.Right ||
					    eventKey.Scancode == (int)KeyList.Space)
					{
						HidePanel();
					}
					//按下 enter 或者 tab 确认输入
					else if (eventKey.Scancode == (int)KeyList.Enter || eventKey.Scancode == (int)KeyList.Tab)
					{
						ConfirmInput(ActiveIndex);
					}
				}
			}
		}

		/// <summary>
		/// 确认输入选中的项, 补全代码
		/// </summary>
		internal void ConfirmInput(int index)
		{
			if (index >= 0 && _activeItemList.Count > 0 && index < _activeItemList.Count && _textEdit != null)
			{
				var line = _textEdit.CursorGetLine();
				var column = _textEdit.CursorGetColumn();
				var lineStr = _textEdit.GetLine(line);

				var beforeStr = lineStr.Substring(0, column);

				var result = Regex.Match(beforeStr, "[\\w]+$");
				if (result.Success)
				{
					var item = _activeItemList[index];
					var text = item.CodeHintItem.CodeText;
					lineStr = beforeStr.Substring(0,
						result.Index) + text + lineStr.Substring(column);
					_textEdit.SetLine(line, lineStr);
					_textEdit.CursorSetColumn(result.Index + text.Length);
					_textEdit.TriggerTextChanged();
				}
				else
				{
					var item = _activeItemList[index];
					_textEdit.InsertTextAtCursor(item.CodeHintItem.CodeText);
				}
			}

			Hide();
			CodeHintManager.EnterInput = true;
		}

		/// <summary>
		/// 显示提示面板
		/// </summary>
		public void ShowPanel(CodeTextEdit textEdit, Vector2 pos)
		{
			_textEdit = textEdit;
			RectPosition = pos;
			if (!Visible)
			{
				GD.Print("call ShowPanel()");
				Popup_();
				_textEdit.GrabFocus();
				ActiveIndex = 0;
			}
		}

		/// <summary>
		/// 隐藏面板
		/// </summary>
		public void HidePanel()
		{
			GD.Print("call HidePanel()");
			Hide();
			_textEdit = null;
		}
		
		/// <summary>
		/// 设置活动项
		/// </summary>
		private void SetActiveIndex(int index)
		{
			//禁用之前的
			if (_activeIndex >= 0 && _activeIndex < _activeItemList.Count)
			{
				var item = _activeItemList[_activeIndex];
				item.CodeHintItem.SetActive(false);
			}

			_activeIndex = index;

			//启用现在的
			if (index >= 0 && index < _activeItemList.Count)
			{
				var item = _activeItemList[index];
				item.CodeHintItem.SetActive(true);

				//矫正滑动组件y轴值, 使其选中项不会跑到视野外
				var vertical = _scrollContainer.ScrollVertical;
				var scrollSize = _scrollContainer.GetVScrollbar().RectSize;
				var itemPos = item.CodeHintItem.RectPosition;
				var itemSize = item.CodeHintItem.RectSize;
				itemPos.y -= vertical;
				if (itemPos.y < 0)
				{
					_scrollContainer.ScrollVertical = (int)(index * itemSize.y);
				}
				else if (itemPos.y + itemSize.y > scrollSize.y)
				{
					_scrollContainer.ScrollVertical = (int)((index + 1) * itemSize.y - scrollSize.y);
				}
			}
		}

		/// <summary>
		/// 创建提示项
		/// </summary>
		private CodeHintItemData CreateItem()
		{
			var codeHintItem = CodeHintItem.Instance<CodeHintItem>();
			_itemContainer.AddChild(codeHintItem);
			codeHintItem.Index = _activeItemList.Count;
			var item = new CodeHintItemData();
			item.CodeHintItem = codeHintItem;
			_activeItemList.Add(item);
			return item;
		}
	}
}