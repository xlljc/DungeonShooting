public class Program
{
    public static void Main(string[] args)
    {
        SValue v1 = "111";
        SValue v2 = true;
        SValue v3 = "aaabbb";

        SValue v4 = v1 + (v2 + v3);
        Console.WriteLine(v4.Value);

        SValue vect1 = new Vector2(1, 1);
        SValue vect2 = new Vector2(2, 3);
        SValue vect3 = vect1.__Invoke__("add", vect2);
        Console.WriteLine(vect3.Value);
    }

    public class Vector2 : SObjectBase
    {
        public SValue x;
        public SValue y;

        public Vector2(SValue x, SValue y)
        {
            this.x = x;
            this.y = y;
        }

        public SValue add(SValue other)
        {
            return new SValue(new Vector2(x + other.__GetValue__("x"), y + other.__GetValue__("y")));
        }

        public override SValue __GetValue__(string key)
        {
            switch (key)
            {
                case "x":
                    return x;
                case "y":
                    return y;
            }

            return SValue.Null;
        }

        public override SValue __Invoke__(string funcName, params SValue[] ps)
        {
            switch (funcName)
            {
                case "add":
                    switch (ps.Length)
                    {
                        case 1:
                            return add(ps[0]);
                    }

                    break;
            }

            //报错, 没有找到对应的函数
            return SValue.Null;
        }
    }

}