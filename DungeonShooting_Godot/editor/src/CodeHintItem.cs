using Godot;

namespace DScript.GodotEditor
{
	/// <summary>
	/// 提示面板中的提示项
	/// </summary>
	public class CodeHintItem : Button
	{
		/// <summary>
		/// 活动时背景颜色
		/// </summary>
		private static Color ActiveColor = new Color(0.20784314F,0.30980393F,0.52156866F);
		/// <summary>
		/// 非活动时背景颜色
		/// </summary>
		private static Color UnActiveColor = new Color(0, 0, 0, 0);
		
		
		/// <summary>
		/// 当前项的索引
		/// </summary>
		public int Index { get; internal set; }
		
		/// <summary>
		/// 代码类型
		/// </summary>
		public CodeHintType CodeType
		{
			get => _codeType;
			set => SetCodeType(value);
		}
		private CodeHintType _codeType;

		/// <summary>
		/// 代码文本
		/// </summary>
		public string CodeText
		{
			get => _codeText;
			set => SetCodeText(value);
		}
		//显示的文本
		private string _codeText;

		private TextureRect _icon;
		private RichTextLabel _text;
		private ColorRect _bgColor;

		public override void _Ready()
		{
			_icon = GetNode<TextureRect>("Icon");
			_text = GetNode<RichTextLabel>("Text");
			_bgColor = GetNode<ColorRect>("BgColor");
		}

		public void SetText()
		{
			
		}

		/// <summary>
		/// 设置成活动状态
		/// </summary>
		internal void SetActive(bool active)
		{
			_bgColor.Color = active ? ActiveColor : UnActiveColor;
		}
		
		//设置代码类型
		private void SetCodeType(CodeHintType type)
		{
			_codeType = type;
		}
		
		//设置代码文本
		private void SetCodeText(string code)
		{
			_codeText = code;
			_text.BbcodeText = code;
		}

		//点击时调用
		private void _on_click()
		{
			CodeHintPanel.Instance.ActiveIndex = Index;
		}
	}
}
