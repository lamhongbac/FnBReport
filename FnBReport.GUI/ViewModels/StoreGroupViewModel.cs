using System.ComponentModel.DataAnnotations;

namespace FnBReport.GUI.ViewModels
{
    public class StoreGroupViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã nhóm không được để trống")]
        public string Number { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên nhóm không được để trống")]
        public string Name { get; set; } = string.Empty;
    }
}
