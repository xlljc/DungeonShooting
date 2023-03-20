
/// <summary>
/// 用于协程返回, 表示当前协程等待指定帧数
/// </summary>
public class WaitForFixedProcess
{
    private float _frames;
    private float _current = 0;

    public WaitForFixedProcess(int frames)
    {
        _frames = frames;
    }

    public bool NextStep()
    {
        return _current++ >= _frames;
    }
}