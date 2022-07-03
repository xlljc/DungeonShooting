using Godot;

/// <summary>
/// 声音管理 背景音乐管理 音效在特定位置 处理
/// </summary>
public class SoundManager
{
    public static SoundManager Instance { get => SingleTon.singleTon; }

    private static class SingleTon
    {
        internal static SoundManager singleTon = new SoundManager();
    }

    private AudioStreamPlayer audioStreamPlayer = new AudioStreamPlayer();
}