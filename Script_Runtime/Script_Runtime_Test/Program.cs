public class Program
{
    public static void Main(string[] args)
    {

        new TestArray();

    }

    /// <summary>
    /// 对比包裹代码与原生C#代码运行效率对比
    /// </summary>
    public static void Test1()
    {
        var time = DateTime.Now.Ticks;
        for (int i = 0; i < 100000; i++)
        {
            SValue vect1 = new Vector2(1, 1);
            SValue vect2 = new Vector2(2, 3);
            SValue vect3 = vect1.__Invoke__("add", vect2);
            var v = vect3.__Invoke__("squareLengtn").Value;
            //Console.WriteLine();
        }
        var time2 = DateTime.Now.Ticks;
        Console.WriteLine("耗时1: " + (time2 - time) / 1000000f + "毫秒");
        
        var time3 = DateTime.Now.Ticks;
        for (int i = 0; i < 100000; i++)
        {
            Vector2Cs vect1 = new Vector2Cs(1, 1);
            Vector2Cs vect2 = new Vector2Cs(2, 3);
            Vector2Cs vect3 = vect1.add(vect2);
            var v = vect3.squareLengtn();
            //Console.WriteLine();
        }
        var time4 = DateTime.Now.Ticks;
        Console.WriteLine("耗时2: " + (time4 - time3) / 1000000f + "毫秒");
    }

}