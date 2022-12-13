using Godot;

public class Blood : Particles2D
{
	public override void _Ready()
	{
		Emitting = true;
		Life();
	}

	private async void Life()
	{
		var timer = GetTree().CreateTimer(0.4f);
		await ToSignal(timer, "timeout");
		Emitting = false;
		GD.Print("冻结");
		SetProcess(false);
		SetPhysicsProcess(false);
		SetProcessInput(false);
		SetProcessInternal(false);
		SetProcessUnhandledInput(false);
		SetProcessUnhandledKeyInput(false);
	}
}