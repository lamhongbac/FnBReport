using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FnBReport.GUI.Services
{
    public class ExcelService : IExcelService
    {
        public byte[] GenerateTemplate(string[] columns, IEnumerable<string[]>? mainSheetData = null, Dictionary<string, IEnumerable<string[]>>? referenceSheets = null, string mainSheetName = "Template")
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(mainSheetName);

            for (int i = 0; i < columns.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = columns[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            }

            if (mainSheetData != null)
            {
                int rowIndex = 2;
                foreach (var row in mainSheetData)
                {
                    for (int colIndex = 0; colIndex < row.Length; colIndex++)
                    {
                        var cellValue = row[colIndex];
                        if (cellValue != null && cellValue.StartsWith("="))
                        {
                            worksheet.Cell(rowIndex, colIndex + 1).FormulaA1 = cellValue.Substring(1);
                        }
                        else
                        {
                            worksheet.Cell(rowIndex, colIndex + 1).Value = cellValue;
                        }
                    }
                    rowIndex++;
                }
            }
            worksheet.Columns().AdjustToContents();

            if (referenceSheets != null)
            {
                foreach (var refSheet in referenceSheets)
                {
                    var sheet = workbook.Worksheets.Add(refSheet.Key);
                    int rowIndex = 1;
                    foreach (var row in refSheet.Value)
                    {
                        for (int colIndex = 0; colIndex < row.Length; colIndex++)
                        {
                            var cellValue = row[colIndex];
                            if (cellValue != null && cellValue.StartsWith("="))
                            {
                                sheet.Cell(rowIndex, colIndex + 1).FormulaA1 = cellValue.Substring(1);
                            }
                            else
                            {
                                sheet.Cell(rowIndex, colIndex + 1).Value = cellValue;
                            }
                            if (rowIndex == 1) // header row
                            {
                                sheet.Cell(rowIndex, colIndex + 1).Style.Font.Bold = true;
                                sheet.Cell(rowIndex, colIndex + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                            }
                        }
                        rowIndex++;
                    }
                    sheet.Columns().AdjustToContents();
                }
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public List<Dictionary<string, string>> ReadExcel(Stream stream, string[] expectedColumns, string? sheetName = null)
        {
            var result = new List<Dictionary<string, string>>();
            using var workbook = new XLWorkbook(stream);
            var worksheet = string.IsNullOrEmpty(sheetName) ? workbook.Worksheet(1) : workbook.Worksheet(sheetName);

            var firstRow = worksheet.FirstRowUsed();
            if (firstRow == null) throw new Exception("File Excel không có dữ liệu.");

            // Map column headers to column index (Normalized to handle Unicode/Spaces)
            var headers = new Dictionary<string, int>();
            foreach (var cell in firstRow.CellsUsed())
            {
                string colName = NormalizeHeader(cell.GetString());
                if (!string.IsNullOrEmpty(colName))
                {
                    headers[colName] = cell.Address.ColumnNumber;
                }
            }

            var rows = worksheet.RowsUsed().Skip(1); // skip header row
            foreach (var row in rows)
            {
                var rowData = new Dictionary<string, string>();
                bool hasData = false;
                foreach (var expectedCol in expectedColumns)
                {
                    string normalizedExpected = NormalizeHeader(expectedCol);
                    if (headers.TryGetValue(normalizedExpected, out int colIndex))
                    {
                        string val = row.Cell(colIndex).GetString().Trim();
                        rowData[expectedCol] = val; // Keep original key for the caller
                        if (!string.IsNullOrEmpty(val)) hasData = true;
                    }
                    else
                    {
                        rowData[expectedCol] = string.Empty;
                    }
                }
                if (hasData)
                {
                    result.Add(rowData);
                }
            }

            return result;
        }

        private string NormalizeHeader(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            
            var normalizedString = input.Normalize(System.Text.NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            
            return stringBuilder.ToString()
                .Normalize(System.Text.NormalizationForm.FormC)
                .ToLowerInvariant()
                .Replace(" ", "")
                .Replace("_", "")
                .Replace("-", "");
        }
    }
}
