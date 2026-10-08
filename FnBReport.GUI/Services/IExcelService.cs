using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FnBReport.GUI.Services
{
    public interface IExcelService
    {
        byte[] GenerateTemplate(string[] columns, IEnumerable<string[]>? mainSheetData = null, Dictionary<string, IEnumerable<string[]>>? referenceSheets = null, string mainSheetName = "Template");
        List<Dictionary<string, string>> ReadExcel(Stream stream, string[] expectedColumns, string? sheetName = null);
    }
}
