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

		//提示项父容器
		private VBoxContainer _itemContainer;

		//当前已启用项的列表
		private List<CodeHintItem> _activeItemList = new List<CodeHintItem>();
		
		public CodeHintPanel()
		{
			Instance = this;
		}

		public override void _Ready()
		{
			_itemContainer = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer");

			for (int i = 0; i < 10; i++)
			{
				var item = CreateItem();
				item.CodeText = i.ToString();
			}
		}

		public override void _Process(float delta)
		{
			if (Input.IsActionJustPressed("ui_down") && _activeItemList.Count > 1) //按下下键
			{
				var index = ActiveIndex;
				index += 1;
				if (index >= _activeItemList.Count)
				{
					index = 0;
				}

				ActiveIndex = index;	
			}
			else if (Input.IsActionJustPressed("ui_up") && _activeItemList.Count > 1) //按下上键
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

		/// <summary>
		/// 显示提示面板
		/// </summary>
		public void ShowPanel(Vector2 pos)
		{
			RectPosition = pos;
			Popup_();
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
				_activeItemList[index].SetActive(true);
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