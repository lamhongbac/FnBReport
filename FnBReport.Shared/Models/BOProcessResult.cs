using System.Collections.Generic;

namespace FnBReport.Shared.Models
{
    public class BOProcessResult
    {
        public bool OK { get; set; } = false;
        public object? Result { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; } = string.Empty;

        // Pattern matching Rule Engine:
        public bool IsSuccess => OK;
        public string Code { get; set; } = string.Empty;
        public Dictionary<string, object> Args { get; set; } = new();

        public static BOProcessResult Success() => new BOProcessResult { OK = true };
        public static BOProcessResult Failed(string code, Dictionary<string, object>? args = null)
        {
            return new BOProcessResult { OK = false, Code = code, Args = args ?? new() };
        }
    }
}
