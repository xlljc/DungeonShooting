
using System.Collections;
using System.Collections.Generic;

public class CoroutineData
{
    private static long _id;
    
    public enum WaitTypeEnum
    {
        None,
        WaitForSeconds,
        WaitForFixedProcess,
    }

    public readonly long Id;
    public WaitTypeEnum WaitType = WaitTypeEnum.None;
    public IEnumerator Enumerator;
    public Stack<IEnumerator> EnumeratorStack;

    public WaitForSeconds WaitForSeconds;
    public WaitForFixedProcess WaitForFixedProcess;

    public CoroutineData(IEnumerator enumerator)
    {
        Id = _id++;
        Enumerator = enumerator;
    }

    public void WaitFor(WaitForSeconds seconds)
    {
        WaitType = WaitTypeEnum.WaitForSeconds;
        WaitForSeconds = seconds;
    }
    
    public void WaitFor(WaitForFixedProcess process)
    {
        WaitType = WaitTypeEnum.WaitForFixedProcess;
        WaitForFixedProcess = process;
    }
}