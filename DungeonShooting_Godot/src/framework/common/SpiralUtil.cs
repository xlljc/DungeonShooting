using Godot;


public static class SpiralUtil
{
	/// <summary>
	/// 螺旋算法  顺时针
	/// 7   8  9  10
	/// 6   1  2
	/// 5   4  3
	/// </summary>
	private static float[][] SCREW_CLOCKWISE = new float[][] { new float[] { 1, 2, 3, 4 }, new float[] { 4, 1, 2, 3 } };

	/// <summary>
	/// 螺旋算法
	/// </summary>
	/// <param name="index">当前序列</param>
	/// <returns>返回当前序列应该所在的位置</returns>
	public static Vector2I Screw(int index)
	{
		//总体思路是先找到第几圈  然后再找到第几个拐角 然后用switch
		//因为一般序列都是从0开始的,所以此处加一以适应规则
		index++;
		//如果求的是中心点 直接返回就行了
		if (index <= 1) return new Vector2I(0, 0);

		//开平方得到当前序列在哪个阶段中(阶段=第几圈*2)
		var n = Mathf.Ceil(Mathf.Sqrt(index));
		var step = Mathf.FloorToInt(n / 2) * 2;
		//求出当前序列是当前阶段中的第几个数
		var stepIndex = index - (step - 1) * (step - 1);
		//求出当前序列在当前阶段中的第几条边上
		var stepStep = Mathf.CeilToInt((float)stepIndex / step);
		//当前序列是当前边上第几个数
		var ssi = stepIndex % step;
		if (ssi == 0) ssi = step;

		return new Vector2I(
			GetValue(step, ssi, SCREW_CLOCKWISE[0][stepStep - 1]),
			GetValue(step, ssi, SCREW_CLOCKWISE[1][stepStep - 1])
		);
	}

	private static int GetValue(int step, int ssi, float switchIndex)
	{
		switch (switchIndex)
		{
			case 1: return step / 2;
			case 2: return step / 2 - ssi;
			case 3: return -step / 2;
			case 4: return -step / 2 + ssi;
		}

		return 0;
	}
}
