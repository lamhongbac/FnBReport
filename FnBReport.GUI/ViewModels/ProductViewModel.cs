using System.ComponentModel.DataAnnotations;

namespace FnBReport.GUI.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã sản phẩm tối đa 20 ký tự")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [MaxLength(250, ErrorMessage = "Tên sản phẩm tối đa 250 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phải chọn Nhóm sản phẩm")]
        public string GroupCode { get; set; }

        [MaxLength(20)]
        public string Unit { get; set; }

        public decimal? Cosprice { get; set; }

        public decimal? SellingPrice { get; set; }
    }
}
