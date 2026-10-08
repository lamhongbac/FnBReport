using System;
using System.ComponentModel.DataAnnotations;

namespace FnBReport.GUI.ViewModels
{
    public class MonthlySaleInitViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn cửa hàng")]
        public string? StoreCode { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tháng/năm")]
        public DateTime? ReportMonth { get; set; } = DateTime.Now;
    }
}
