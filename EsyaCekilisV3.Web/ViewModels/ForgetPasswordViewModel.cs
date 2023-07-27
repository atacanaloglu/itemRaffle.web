using System.ComponentModel.DataAnnotations;

namespace EsyaCekilisV3.Web.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Email adresi formatı uygun değil.")]
        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
