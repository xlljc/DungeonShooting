
/// <summary>
/// 用于协程返回, 表示当前协程等待指定秒数
/// </summary>
public class WaitForSeconds
{
    private float _timer;
    private float _current = 0;
    
    public WaitForSeconds(float time)
    {
        _timer = time;
    }

    public bool NextStep(float delta)
    {
        if (_current >= _timer)
        {
            return true;
        }

        _current += delta;
        return false;
    }
}