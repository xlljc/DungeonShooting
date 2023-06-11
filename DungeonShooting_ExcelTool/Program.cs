
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("准备导出excel表...");
        if (ExcelGenerator.ExportExcel())
        {
            Console.WriteLine("excel表导出成功!");
        }
        else
        {
            Console.WriteLine("excel表导出失败!");
        }
    }
}