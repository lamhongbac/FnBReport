using System;
using System.Collections.Generic;

namespace FnBReport.GUI.ViewModels
{
    public class MonthlySaleHeaderViewModel
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string StoreCode { get; set; } = string.Empty;
        public string StoreName { get; set; } = string.Empty;
        public int? Tables { get; set; }
        public int Tc { get; set; }
        public decimal Sales { get; set; }
        public decimal? Cos { get; set; }
        public decimal TotalMargin { get; set; }
        
        // For display
        public decimal? MinMenuSelling { get; set; }
        public decimal? MinSalesContribution { get; set; }

        public List<MonthlySaleViewModel> Details { get; set; } = new List<MonthlySaleViewModel>();
    }

    public class MonthlySaleViewModel
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string GroupCode { get; set; } = string.Empty;
        public decimal Cosprice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Margin { get; set; }
        public int SoldQuantity { get; set; }

        public decimal? CosPercentage { get; set; }
        public decimal? SaleMixPercentage { get; set; }
        public decimal? MarginMixPercentage { get; set; }
        public decimal? ProductMixPercentage { get; set; }
        public decimal? TcTable { get; set; }
        public decimal? Per100Tc { get; set; }
        public decimal? Per100Quality { get; set; }
        public decimal? Per100Selling { get; set; }
        public string Popularity { get; set; } = string.Empty;
        public string Profitability { get; set; } = string.Empty;
        public string ItemClass { get; set; } = string.Empty;
    }
}
