using System;
using Godot;

/// <summary>
/// 音频总线 区分不同的声音 添加声音效果 目前只有背景音乐 和音效 两个bus
/// </summary>
public enum BUS
{
    BGM = 0,
    SFX = 1
}

/// <summary>
/// 声音管理 背景音乐管理 音效
/// </summary>
public class SoundManager
{
    public static SoundManager Instance { get => SingleTon.singleTon; }

    private static class SingleTon
    {
        internal static SoundManager singleTon = new SoundManager();
    }

    private static AudioStreamPlayer audioStreamPlayer = new AudioStreamPlayer();


    /// <summary>
    /// 背景音乐路径
    /// </summary>
    public static string BGMPath = "res://resource/sound/bgm/";
    /// <summary>
    /// 音效路径
    /// </summary>
    public static string SFXPath = "res://resource/sound/sfx/";

    /// <summary>
    /// 播放声音 用于bgm
    /// </summary>
    /// <param name="soundPath">bgm名字 在resource/sound/bgm/目录下</param>
    /// <param name="node">需要播放声音得节点 将成为音频播放节点的父节点</param>
    /// <param name="volume">音量</param>
    public static void PlayeMusic(string soundName, Node node, float volume)
    {
        AudioStream sound = ResourceManager.Load<AudioStream>(BGMPath + soundName);
        if (sound != null)
        {
            AudioStreamPlayer soundNode = new AudioStreamPlayer();
            node.AddChild(soundNode);
            soundNode.Stream = sound;
            soundNode.Bus = Enum.GetName(typeof(BUS), 0);
            soundNode.VolumeDb = volume;
            soundNode.Play();
        }
        else
        {
            GD.Print("没有这个资源！！！");
        }
    }
    /// <summary>
    /// 添加并播放音效 用于音效
    /// </summary>
    /// <param name="soundName">音效文件名字 在resource/sound/sfx/目录下</param>
    /// <param name="node">需要播放声音得节点 将成为音频播放节点的父节点</param>
    /// <param name="volume">音量</param>
    public static void PlaySoundEffect(string soundName, Node node, float volume = 0f)
    {
        AudioStream sound = ResourceManager.Load<AudioStream>(SFXPath + soundName);
        if (sound != null)
        {
            AudioStreamPlayer soundNode = new AudioStreamPlayer();
            node.AddChild(soundNode);
            soundNode.Stream = sound;
            soundNode.Bus = Enum.GetName(typeof(BUS), 1);
            soundNode.VolumeDb = volume;
            soundNode.Play();
            GD.Print("bus:", soundNode.Bus);
        }
        else
        {
            GD.Print("没有这个资源！！！");
        }
    }
}