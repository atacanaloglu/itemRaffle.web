using System.ComponentModel.DataAnnotations;

namespace EsyaCekilisV3.Web.ViewModels
{
    public class SignUpViewModel
    {

        public SignUpViewModel() { }

        public SignUpViewModel(string userName, string email, string phone, string password)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }

        [Required(ErrorMessage ="Kullanıcı Adı alanı boş bırakılamaz.")]
        [Display(Name="Kullanıcı Adı")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage ="Email adresi formatı uygun değil.")]
        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı boş bırakılamaz.")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Şifre")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Girmiş olduğunuz şifreler aynı değildir.")]
        [Required(ErrorMessage = "Şifre Tekrar alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar")]
        public string PasswordConfirm { get; set; }

        
    }
}
