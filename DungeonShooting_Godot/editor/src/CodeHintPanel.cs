using System.Collections.Generic;
using Godot;

namespace DScript.GodotEditor
{
	/// <summary>
	/// 代码补全提示
	/// </summary>
	public class CodeHintPanel : Popup
	{
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

		//当前已启用项的列表
		private List<CodeHintItem> _activeItemList = new List<CodeHintItem>();
		
		//长按计时器
		private float _clickTimer = 0;
		private bool _continuousFlag = false;

		//当前的文本编辑器对象
		private TextEdit _textEdit;
		
		public CodeHintPanel()
		{
			Instance = this;
		}

		public override void _Ready()
		{
			_scrollContainer = GetNode<ScrollContainer>("ScrollContainer");
			_itemContainer = _scrollContainer.GetNode<VBoxContainer>("VBoxContainer");

			for (int i = 0; i < CodeTextEditor.KeyCodes.Length; i++)
			{
				var item = CreateItem();
				item.CodeText = CodeTextEditor.KeyCodes[i];
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
				if ((!_continuousFlag && _clickTimer > 0.5f) || (_continuousFlag && _clickTimer > 0.1f))
				{
					_continuousFlag = true;
					_clickTimer = 0;
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
			
			//确认输入
			if (Input.IsKeyPressed((int)KeyList.Enter))
			{
				ConfirmInput(ActiveIndex);
			}
		}

		/// <summary>
		/// 确认输入选中的项, 补全代码
		/// </summary>
		internal void ConfirmInput(int index)
		{
			if (index >= 0 && _activeItemList.Count > 0 && index < _activeItemList.Count && _textEdit != null)
			{
				_textEdit.InsertTextAtCursor(_activeItemList[index].CodeText);
			}
			Hide();
		}
		
		/// <summary>
		/// 显示提示面板
		/// </summary>
		public void ShowPanel(TextEdit textEdit, Vector2 pos)
		{
			_textEdit = textEdit;
			RectPosition = pos;
			if (!Visible)
			{
				Popup_();
				GD.Print(_textEdit.GetWordUnderCursor());
			}
		}

		/// <summary>
		/// 隐藏面板
		/// </summary>
		public void HidePanel()
		{
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
				_activeItemList[_activeIndex].SetActive(false);
			}

			_activeIndex = index;

			//启用现在的
			if (index >= 0 && index < _activeItemList.Count)
			{
				var item = _activeItemList[index];
				item.SetActive(true);

				//矫正滑动组件滑y轴值, 使其选中项不会跑到视野外
				var vertical = _scrollContainer.ScrollVertical;
				var scrollSize = _scrollContainer.GetVScrollbar().RectSize;
				var itemPos = item.RectPosition;
				var itemSize = item.RectSize;
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
		private CodeHintItem CreateItem()
		{
			var item = CodeHintItem.Instance<CodeHintItem>();
			_itemContainer.AddChild(item);
			item.Index = _activeItemList.Count;
			_activeItemList.Add(item);
			return item;
		}
	}
}