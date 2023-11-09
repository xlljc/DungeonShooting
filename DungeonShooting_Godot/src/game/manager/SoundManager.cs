using System;
using System.Collections;
using System.Collections.Generic;
using Config;
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
    /// <summary>
    /// 全局音效音量, 范围 0 - 1
    /// </summary>
    public static float SoundVolume { get; set; } = 0.4f;
    
    private static Stack<GameAudioPlayer2D> _streamPlayer2DStack = new Stack<GameAudioPlayer2D>();
    private static Stack<GameAudioPlayer> _streamPlayerStack = new Stack<GameAudioPlayer>();
    private static HashSet<string> _playingSoundResourceList = new HashSet<string>();

    /// <summary>
    /// 2D音频播放节点
    /// </summary>
    public partial class GameAudioPlayer2D : AudioStreamPlayer2D
    {
        public override void _Ready()
        {
            Finished += OnPlayFinish;
        }

        public void PlaySoundByResource(string path, float delayTime)
        {
            if (delayTime <= 0)
            {
                PlaySoundByResource(path);
            }
            else
            {
                GameApplication.Instance.StartCoroutine(DelayPlay(path, delayTime));
            }
        }

        public void PlaySoundByResource(string path)
        {
            if (_playingSoundResourceList.Contains(path))
            {
                Debug.Log("重复播放: " + path);
            }
            else
            {
                _playingSoundResourceList.Add(path);
                var sound = ResourceManager.Load<AudioStream>(path);
                Stream = sound;
                Bus = Enum.GetName(typeof(BUS), 1);
                Play();
            }
        }

        /// <summary>
        /// 停止播放, 并回收节点
        /// </summary>
        public void StopPlay()
        {
            Stop();
            OnPlayFinish();
        }

        private void OnPlayFinish()
        {
            RecycleAudioPlayer2D(this);
        }

        private IEnumerator DelayPlay(string path, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            PlaySoundByResource(path);
        }
    }

    /// <summary>
    /// 音频播放节点
    /// </summary>
    public partial class GameAudioPlayer : AudioStreamPlayer
    {
        public string SoundResourceName { get; set; }
        public override void _Ready()
        {
            Finished += OnPlayFinish;
        }

        /// <summary>
        /// 停止播放, 并回收节点
        /// </summary>
        public void StopPlay()
        {
            Stop();
            OnPlayFinish();
        }
        
        private void OnPlayFinish()
        {
            GetParent().RemoveChild(this);
            Stream = null;
            Playing = false;
            RecycleAudioPlayer(this);
        }
    }

    public static void Update(float delta)
    {
        _playingSoundResourceList.Clear();
    }
    
    /// <summary>
    /// 播放声音 用于bgm
    /// </summary>
    /// <param name="soundName">bgm路径</param>
    /// <param name="volume">音量</param>
    public static GameAudioPlayer PlayMusic(string soundName, float volume = 0.5f)
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
    /// <param name="volume">音量 (0 - 1)</param>
    public static GameAudioPlayer PlaySoundEffect(string soundName, float volume = 1f)
    {
        var sound = ResourceManager.Load<AudioStream>(soundName);
        var soundNode = GetAudioPlayerInstance();
        GameApplication.Instance.GlobalNodeRoot.AddChild(soundNode);
        soundNode.Stream = sound;
        soundNode.Bus = Enum.GetName(typeof(BUS), 1);
        soundNode.VolumeDb = Mathf.LinearToDb(Mathf.Clamp(volume, 0, 1));
        soundNode.Play();
        return soundNode;
    }

    /// <summary>
    /// 在指定的节点下播放音效 用于音效
    /// </summary>
    /// <param name="soundName">音效文件路径</param>
    /// <param name="pos">发声节点所在全局坐标</param>
    /// <param name="volume">音量 (0 - 1)</param>
    /// <param name="target">挂载节点, 为null则挂载到房间根节点下</param>
    public static GameAudioPlayer2D PlaySoundEffectPosition(string soundName, Vector2 pos, float volume = 1f, Node2D target = null)
    {
        return PlaySoundEffectPositionDelay(soundName, pos, 0, volume, target);
    }

    /// <summary>
    /// 在指定的节点下延时播放音效 用于音效
    /// </summary>
    /// <param name="soundName">音效文件路径</param>
    /// <param name="pos">发声节点所在全局坐标</param>
    /// <param name="delayTime">延时时间</param>
    /// <param name="volume">音量 (0 - 1)</param>
    /// <param name="target">挂载节点, 为null则挂载到房间根节点下</param>
    public static GameAudioPlayer2D PlaySoundEffectPositionDelay(string soundName, Vector2 pos, float delayTime, float volume = 1f, Node2D target = null)
    {
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
        soundNode.VolumeDb = Mathf.LinearToDb(Mathf.Clamp(volume * SoundVolume, 0, 1));
        soundNode.PlaySoundByResource(soundName, delayTime);
        return soundNode;
    }
    
    /// <summary>
    /// 根据音效配置表Id播放音效
    /// </summary>
    /// <param name="id">音效Id</param>
    /// <param name="viewPosition">播放音效的位置, 该位置为 SubViewport 下的坐标, 也就是 <see cref="ActivityObject"/> 使用的坐标</param>
    /// <param name="triggerRole">触发播放音效的角色, 因为 Npc 产生的音效声音更小, 可以传 null</param>
    public static GameAudioPlayer2D PlaySoundByConfig(string id, Vector2 viewPosition, AdvancedRole triggerRole = null)
    {
        var sound = ExcelConfig.Sound_Map[id];
        return PlaySoundByConfig(sound, viewPosition, triggerRole);
    }

    /// <summary>
    /// 根据音效配置播放音效
    /// </summary>
    /// <param name="sound">音效数据</param>
    /// <param name="viewPosition">播放音效的位置, 该位置为 SubViewport 下的坐标, 也就是 <see cref="ActivityObject"/> 使用的坐标</param>
    /// <param name="triggerRole">触发播放音效的角色, 因为 Npc 产生的音效声音更小, 可以传 null</param>
    public static GameAudioPlayer2D PlaySoundByConfig(ExcelConfig.Sound sound, Vector2 viewPosition, AdvancedRole triggerRole = null)
    {
        return PlaySoundEffectPosition(
            sound.Path,
            GameApplication.Instance.ViewToGlobalPosition(viewPosition),
            CalcRoleVolume(sound.Volume, triggerRole)
        );
    }

    /// <summary>
    /// 根据音效配置表Id延时播放音效
    /// </summary>
    /// <param name="id">音效Id</param>
    /// <param name="viewPosition">播放音效的位置, 该位置为 SubViewport 下的坐标, 也就是 <see cref="ActivityObject"/> 使用的坐标</param>
    /// <param name="delayTime">延时时间</param>
    /// <param name="triggerRole">触发播放音效的角色, 因为 Npc 产生的音效声音更小, 可以传 null</param>
    public static GameAudioPlayer2D PlaySoundByConfigDelay(string id, Vector2 viewPosition, float delayTime, AdvancedRole triggerRole = null)
    {
        var sound = ExcelConfig.Sound_Map[id];
        return PlaySoundByConfigDelay(sound, viewPosition, delayTime, triggerRole);
    }
    
    /// <summary>
    /// 根据音效配置延时播放音效
    /// </summary>
    /// <param name="sound">音效数据</param>
    /// <param name="viewPosition">播放音效的位置, 该位置为 SubViewport 下的坐标, 也就是 <see cref="ActivityObject"/> 使用的坐标</param>
    /// <param name="delayTime">延时时间</param>
    /// <param name="triggerRole">触发播放音效的角色, 因为 Npc 产生的音效声音更小, 可以传 null</param>
    public static GameAudioPlayer2D PlaySoundByConfigDelay(ExcelConfig.Sound sound, Vector2 viewPosition, float delayTime, AdvancedRole triggerRole = null)
    {
        return PlaySoundEffectPositionDelay(
            sound.Path,
            GameApplication.Instance.ViewToGlobalPosition(viewPosition),
            delayTime,
            CalcRoleVolume(sound.Volume, triggerRole)
        );
    }

    /// <summary>
    /// 获取2D音频播放节点
    /// </summary>
    private static GameAudioPlayer2D GetAudioPlayer2DInstance()
    {
        if (_streamPlayer2DStack.Count > 0)
        {
            return _streamPlayer2DStack.Pop();
        }

        var inst = new GameAudioPlayer2D();
        inst.AreaMask = 0;
        return inst;
    }

    /// <summary>
    /// 获取音频播放节点
    /// </summary>
    private static GameAudioPlayer GetAudioPlayerInstance()
    {
        if (_streamPlayerStack.Count > 0)
        {
            return _streamPlayerStack.Pop();
        }

        return new GameAudioPlayer();
    }

    /// <summary>
    /// 回收2D音频播放节点
    /// </summary>
    private static void RecycleAudioPlayer2D(GameAudioPlayer2D inst)
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
    private static void RecycleAudioPlayer(GameAudioPlayer inst)
    {
        _streamPlayerStack.Push(inst);
    }

    /// <summary>
    /// 计算指定角色播放音效使用的音量
    /// </summary>
    public static float CalcRoleVolume(float volume, AdvancedRole role)
    {
        if (role is not Player)
        {
            return volume * 0.4f;
        }

        return volume;
    }
}