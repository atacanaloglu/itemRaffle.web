using System.ComponentModel.DataAnnotations;

namespace EsyaCekilisV3.Web.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Eski Şifre")]
        public string PasswordOld { get; set; } = null!;
       
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Yeni Şifre")]
        [MinLength(6,ErrorMessage ="Şifreniz en az 6 karakter olmalıdır.")]
        public string PasswordNew { get; set; } = null!;


        [DataType(DataType.Password)]
        [Compare(nameof(PasswordNew), ErrorMessage = "Girmiş olduğunuz şifreler aynı değildir.")]
        [Required(ErrorMessage = "Yeni Şifre Tekrar alanı boş bırakılamaz")]
        [Display(Name = "Yeni Şifre Tekrar")]
        public string PasswordNewConfirm { get; set; } = null!;
    }
}
