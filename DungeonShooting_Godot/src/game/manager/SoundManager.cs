using System;
using System.Collections.Generic;
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
public partial class SoundManager
{
    private static Stack<AudioPlayer2D> _streamPlayer2DStack = new Stack<AudioPlayer2D>();
    private static Stack<AudioPlayer> _streamPlayerStack = new Stack<AudioPlayer>();

    /// <summary>
    /// 2D音频播放节点
    /// </summary>
    private partial class AudioPlayer2D : AudioStreamPlayer2D
    {
        public override void _Ready()
        {
            Finished += OnPlayFinish;
        }

        public void OnPlayFinish()
        {
            RecycleAudioPlayer2D(this);
        }
    }

    /// <summary>
    /// 音频播放节点
    /// </summary>
    private partial class AudioPlayer : AudioStreamPlayer
    {
        public override void _Ready()
        {
            Finished += OnPlayFinish;
        }

        public void OnPlayFinish()
        {
            GetParent().RemoveChild(this);
            Stream = null;
            Playing = false;
            RecycleAudioPlayer(this);
        }
    }

    /// <summary>
    /// 播放声音 用于bgm
    /// </summary>
    /// <param name="soundName">bgm路径</param>
    /// <param name="volume">音量</param>
    public static AudioStreamPlayer PlayMusic(string soundName, float volume = 0.5f)
    {
        var sound = ResourceManager.Load<AudioStream>(soundName);
        var soundNode = GetAudioPlayerInstance();
        GameApplication.Instance.GlobalNodeRoot.AddChild(soundNode);
        soundNode.Stream = sound;
        soundNode.Bus = Enum.GetName(typeof(BUS), 0);
        soundNode.VolumeDb = volume;
        soundNode.Play();
        return soundNode;
    }

    /// <summary>
    /// 添加并播放音效 用于音效
    /// </summary>
    /// <param name="soundName">音效文件路径</param>
    /// <param name="volume">音量</param>
    public static AudioStreamPlayer PlaySoundEffect(string soundName, float volume = 0.5f)
    {
        var sound = ResourceManager.Load<AudioStream>(soundName);
        var soundNode = GetAudioPlayerInstance();
        GameApplication.Instance.GlobalNodeRoot.AddChild(soundNode);
        soundNode.Stream = sound;
        soundNode.Bus = Enum.GetName(typeof(BUS), 1);
        soundNode.VolumeDb = volume;
        soundNode.Play();
        return soundNode;
    }

    /// <summary>
    /// 在指定的节点下播放音效 用于音效
    /// </summary>
    /// <param name="soundName">音效文件路径</param>
    /// <param name="pos">发声节点所在全局坐标</param>
    /// <param name="volume">音量</param>
    /// <param name="target">挂载节点, 为null则挂载到房间根节点下</param>
    public static AudioStreamPlayer2D PlaySoundEffectPosition(string soundName, Vector2 pos, float volume = 0.5f, Node2D target = null)
    {
        var sound = ResourceManager.Load<AudioStream>(soundName);
        var soundNode = GetAudioPlayer2DInstance();
        if (target != null)
        {
            target.AddChild(soundNode);
        }
        else
        {
            GameApplication.Instance.GlobalNodeRoot.AddChild(soundNode);
        }
        
        soundNode.GlobalPosition = pos;
        soundNode.Stream = sound;
        soundNode.Bus = Enum.GetName(typeof(BUS), 1);
        soundNode.VolumeDb = volume;
        soundNode.Play();
        return soundNode;
    }

    /// <summary>
    /// 获取2D音频播放节点
    /// </summary>
    private static AudioPlayer2D GetAudioPlayer2DInstance()
    {
        if (_streamPlayer2DStack.Count > 0)
        {
            return _streamPlayer2DStack.Pop();
        }

        var inst = new AudioPlayer2D();
        inst.AreaMask = 0;
        return inst;
    }

    /// <summary>
    /// 获取音频播放节点
    /// </summary>
    private static AudioPlayer GetAudioPlayerInstance()
    {
        if (_streamPlayerStack.Count > 0)
        {
            return _streamPlayerStack.Pop();
        }

        return new AudioPlayer();
    }

    /// <summary>
    /// 回收2D音频播放节点
    /// </summary>
    private static void RecycleAudioPlayer2D(AudioPlayer2D inst)
    {
        var parent = inst.GetParent();
        if (parent != null)
        {
            parent.RemoveChild(inst);
        }

        inst.Stream = null;
        _streamPlayer2DStack.Push(inst);
    }

    /// <summary>
    /// 回收音频播放节点
    /// </summary>
    private static void RecycleAudioPlayer(AudioPlayer inst)
    {
        _streamPlayerStack.Push(inst);
    }
}