using ClosedXML.Excel;
using System;
using System.IO;

var path = @"D:\GIT PROJECT\FnBReport\Docs\ProductMixTemplate.xlsx";
using var wb = new XLWorkbook(path);
foreach (var ws in wb.Worksheets)
{
    Console.WriteLine("--- Sheet: " + ws.Name + " ---");
    for (int r = 1; r <= 15; r++)
    {
        var rowVals = new System.Collections.Generic.List<string>();
        bool hasData = false;
        for (int c = 1; c <= 10; c++)
        {
            var val = ws.Cell(r, c).GetString();
            if (!string.IsNullOrEmpty(val)) hasData = true;
            rowVals.Add(val);
        }
        if (hasData) {
            Console.WriteLine("R" + r + ": " + string.Join(" | ", rowVals));
        }
    }
}
