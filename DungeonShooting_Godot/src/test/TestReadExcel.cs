using Godot;
using System;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public partial class TestReadExcel : Node2D
{
    public override void _Ready()
    {
        string sourceFile = @"excel/Weapon.xlsx";
 
        IWorkbook workbook = new XSSFWorkbook(sourceFile);
        ISheet sheet1 = workbook.GetSheet("Sheet1");

        int columnCount = -1;
        foreach (IRow row in sheet1)
        {
            foreach (var cell in row)
            {
                if (columnCount >= 0 && cell.ColumnIndex >= columnCount)
                {
                    break;
                }
                var value = cell.StringCellValue;
                if (string.IsNullOrEmpty(value))
                {
                    if (columnCount < 0)
                    {
                        columnCount = cell.ColumnIndex;
                        break;
                    }
                    else if (cell.ColumnIndex == 0)
                    {
                        break;
                    }
                }
                GD.Print("row: " + row.RowNum + " , Column: " + cell.ColumnIndex + ", value: " + cell.StringCellValue);
            }
        }
        workbook.Close();
        // sheet1.CreateRow(0).CreateCell(0).SetCellValue(1);
        // sheet1.CreateRow(1).CreateCell(0).SetCellValue(2);
        // sheet1.CreateRow(2).CreateCell(0).SetCellValue(3);
        //
        // FileStream fs = new FileStream(targetFile, FileMode.Create);
        // workbook.Write(fs, true);
        // workbook.Close();
    }
}
