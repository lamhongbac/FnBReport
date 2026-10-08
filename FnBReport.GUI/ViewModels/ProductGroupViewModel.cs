using System.ComponentModel.DataAnnotations;

namespace FnBReport.GUI.ViewModels
{
    public class ProductGroupViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã nhóm không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã nhóm tối đa 20 ký tự")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Tên nhóm không được để trống")]
        [MaxLength(250, ErrorMessage = "Tên nhóm tối đa 250 ký tự")]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Category { get; set; } = "alacarte";
    }
}
