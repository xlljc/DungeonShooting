
public class Program
{
    public static int Main(string[] args)
    {
        Console.WriteLine("准备导出excel表...");
        bool success;
        if (args.Length >= 3)
        {
            success = ExcelGenerator.ExportExcel(args[0], args[1], args[2]);
        }
        else
        {
            success = ExcelGenerator.ExportExcel();
        }

        if (success)
        {
            Console.WriteLine("excel表导出成功!");
            return 0;
        }

        Console.WriteLine("excel表导出失败!");
        return -1;
    }
}