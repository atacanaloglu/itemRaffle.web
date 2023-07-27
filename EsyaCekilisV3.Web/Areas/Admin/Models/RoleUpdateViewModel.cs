using System.ComponentModel.DataAnnotations;

namespace EsyaCekilisV3.Web.Areas.Admin.Models
{
    public class RoleUpdateViewModel
    {
        public string Id { get; set; } = null!;
        [Required(ErrorMessage = "Rol ismi alanı boş bırakılamaz.")]
        [Display(Name = "Rol ismi")] 
        public string Name { get; set; } = null!;
    }
}
