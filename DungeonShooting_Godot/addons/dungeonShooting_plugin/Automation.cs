#if TOOLS
using Generator;
using Godot;

[Tool]
public partial class Automation : Control
{
	/// <summary>
	/// 更新 ResourcePath
	/// </summary>
	private void _on_Button_pressed()
	{
		ResourcePathGenerator.Generate();
	}

	/// <summary>
	/// 重新打包房间配置
	/// </summary>
	private void _on_Button2_pressed()
	{
		RoomPackGenerator.Generate();
	}
}
#endif