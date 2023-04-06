using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 带有权重的随机值处理类
/// </summary>
public class WeightRandom
{
    private (float, int)[] _prepareAdRewardWeight;

    /// <summary>
    /// 初始化权重列表
    /// </summary>
    public void InitAdRewardWeight(int[] weightList)
    {
        var total = weightList.Sum();
        var length = weightList.Length;
        var avg = 1f * total / length;
        var smallAvg = new List<(float, int)>();
        var bigAvg = new List<(float, int)>();
        for (int i = 0; i < weightList.Length; i++)
        {
            (weightList[i] > avg ? bigAvg : smallAvg).Add((weightList[i], i));
        }

        _prepareAdRewardWeight = new (float, int)[weightList.Length];
        for (int i = 0; i < weightList.Length; i++)
        {
            if (smallAvg.Count > 0)
            {
                if (bigAvg.Count > 0)
                {
                    _prepareAdRewardWeight[smallAvg[0].Item2] = (smallAvg[0].Item1 / avg, bigAvg[0].Item2);
                    bigAvg[0] = (bigAvg[0].Item1 - avg + smallAvg[0].Item1, bigAvg[0].Item2);
                    if (avg - bigAvg[0].Item1 > 0.0000001f)
                    {
                        smallAvg.Add(bigAvg[0]);
                        bigAvg.RemoveAt(0);
                    }
                }
                else
                {
                    _prepareAdRewardWeight[smallAvg[0].Item2] = (smallAvg[0].Item1 / avg, smallAvg[0].Item2);
                }

                smallAvg.RemoveAt(0);
            }
            else
            {
                _prepareAdRewardWeight[bigAvg[0].Item2] = (bigAvg[0].Item1 / avg, bigAvg[0].Item2);
                bigAvg.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// 获取下一个随机索引
    /// </summary>
    public int GetRandomIndex()
    {
        var randomNum = Utils.RandomDouble() * _prepareAdRewardWeight.Length;
        var intRan = (int)Math.Floor(randomNum);
        var p = _prepareAdRewardWeight[intRan];
        if (p.Item1 > randomNum - intRan)
        {
            return intRan;
        }
        else
        {
            return p.Item2;
        }
    }
}