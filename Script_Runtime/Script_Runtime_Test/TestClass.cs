using System.Text;

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
    public OldSValue x = OldSValue.Null;
    public OldSValue y = OldSValue.Null;

    public Vector2(OldSValue x, OldSValue y)
    {
        this.x = x;
        this.y = y;
    }

    public OldSValue add(OldSValue other)
    {
        return new Vector2(x + other.GetValue("x"), y + other.GetValue("y"));
    }

    public OldSValue squareLengtn()
    {
        OldSValue num = x + y;
        return num * num;
    }

    /*public override SValue toString()
    {
        //return $"[x :{x}, y: {y}]";
        //return $"[x :{x}, y: {y}]";
        return base.toString();
    }*/
    
    public override OldSValue __GetValue(string key)
    {
        switch (key)
        {
            case "x":
                return x;
            case "y":
                return y;
        }

        return OldSValue.Null;
    }

    public override OldSValue __InvokeMethod(string funcName, params OldSValue[] ps)
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
        return OldSValue.Null;
    }
}