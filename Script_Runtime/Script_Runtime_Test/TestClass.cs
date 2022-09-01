public class Vector2Cs
{
    public double x;
    public double y;

    public Vector2Cs(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
        
    public Vector2Cs add(Vector2Cs other)
    {
        return new Vector2Cs(x + other.x, y + other.y);
    }

    public double squareLengtn()
    {
        double num = x + y;
        return num * num;
    }
}
    
public class Vector2 : SObject
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
        return new Vector2(x + other.__GetValue__("x"), y + other.__GetValue__("y"));
    }

    public SValue squareLengtn()
    {
        SValue num = x + y;
        return num * num;
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

    public override SValue __InvokeMethod__(string funcName, params SValue[] ps)
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
            case "squareLengtn":
                switch (ps.Length)
                {
                    case 0:
                        return squareLengtn();
                }

                break;
        }

        //报错, 没有找到对应的函数
        return SValue.Null;
    }
}