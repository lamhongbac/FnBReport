using System;
using System.ComponentModel.DataAnnotations;

namespace FnBReport.GUI.ViewModels
{
    public class StoreViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã cửa hàng không được để trống")]
        public string Number { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên cửa hàng không được để trống")]
        public string Name { get; set; } = string.Empty;

        public int GroupId { get; set; }
        public int? NumOfTables { get; set; }
        public decimal? MinMenuSelling { get; set; }
        public decimal? MinSalesContribution { get; set; }
    }
}
